using System;
using System.Runtime.CompilerServices;
using RoR2.ConVar;
using UnityEngine;

namespace RoR2
{
	// Token: 0x02000958 RID: 2392
	public class LocalNavigator
	{
		// Token: 0x0600362D RID: 13869 RVA: 0x000E4E38 File Offset: 0x000E3038
		public void SetBody(CharacterBody newBody)
		{
			this.bodyComponents = new LocalNavigator.BodyComponents(newBody);
			this.PushSnapshot();
			this.PushSnapshot();
		}

		// Token: 0x170004FF RID: 1279
		// (get) Token: 0x0600362E RID: 13870 RVA: 0x000E4E52 File Offset: 0x000E3052
		// (set) Token: 0x0600362F RID: 13871 RVA: 0x000E4E5A File Offset: 0x000E305A
		public Vector3 moveVector { get; private set; }

		// Token: 0x17000500 RID: 1280
		// (get) Token: 0x06003630 RID: 13872 RVA: 0x000E4E63 File Offset: 0x000E3063
		// (set) Token: 0x06003631 RID: 13873 RVA: 0x000E4E6B File Offset: 0x000E306B
		public float jumpSpeed { get; private set; }

		// Token: 0x06003632 RID: 13874 RVA: 0x000E4E74 File Offset: 0x000E3074
		private void PushSnapshot()
		{
			this.previousSnapshot = this.currentSnapshot;
			this.currentSnapshot = new LocalNavigator.BodySnapshot(ref this.bodyComponents, this.localTime);
			this.previousSnapshotDelta = this.currentSnapshotDelta;
			this.currentSnapshotDelta = new LocalNavigator.SnapshotDelta(ref this.previousSnapshot, ref this.currentSnapshot);
		}

		// Token: 0x06003633 RID: 13875 RVA: 0x000E4EC8 File Offset: 0x000E30C8
		public void Update(float deltaTime)
		{
			this.localTime += deltaTime;
			this.PushSnapshot();
			if (LocalNavigator.cvLocalNavigatorDebugDraw.value)
			{
				Debug.DrawLine(this.currentSnapshot.position, this.targetPosition, new Color32(byte.MaxValue, 127, 39, 127), deltaTime);
			}
			this.wasObstructedLastUpdate = false;
			this.jumpSpeed = 0f;
			this.previousMoveVector = this.moveVector;
			Vector3 a = this.targetPosition - this.currentSnapshot.position;
			if (!this.currentSnapshot.isFlying)
			{
				a.y = 0f;
			}
			float magnitude = a.magnitude;
			Vector3 vector = a / magnitude;
			Vector3 moveVector = this.moveVector;
			this.CompensateForVelocityByRefinement(deltaTime, ref moveVector, vector);
			this.moveVector = moveVector;
			if (magnitude == 0f)
			{
				this.moveVector = Vector3.zero;
			}
			else
			{
				this.raycastResults = this.GetRaycasts(this.currentSnapshot, this.moveVector, deltaTime);
				if (this.raycastResults.forwardObstructed)
				{
					int num = (this.raycastResults.leftObstructed ? 0 : -1) + (this.raycastResults.rightObstructed ? 0 : 1);
					if (num == 0)
					{
						num = ((UnityEngine.Random.Range(0, 1) == 1) ? -1 : 1);
					}
					this.moveVector = Quaternion.Euler(0f, -LocalNavigator.avoidanceAngle * (float)num, 0f) * this.moveVector;
					this.wasObstructedLastUpdate = (this.raycastResults.leftObstructed || this.raycastResults.rightObstructed);
				}
				float deltaTime2 = deltaTime + 0.4f;
				Vector3 moveVector2 = this.moveVector;
				if (this.CheckCliffAhead(this.currentSnapshot, this.currentSnapshotDelta, moveVector2, deltaTime2))
				{
					this.wasObstructedLastUpdate = true;
					this.moveVector = -this.moveVector;
					moveVector2 = this.moveVector;
					if (this.CheckCliffAhead(this.currentSnapshot, this.currentSnapshotDelta, moveVector2, deltaTime2))
					{
						this.moveVector *= 0.25f;
					}
				}
				this.CalculateFrustration(deltaTime, ref this.walkFrustration);
				if (this.walkFrustration >= 1f)
				{
					this.jumpSpeed = this.currentSnapshot.maxJumpSpeed;
				}
			}
			this.avoidanceTimer -= deltaTime;
			this.walkFrustration = Mathf.Clamp(this.walkFrustration - deltaTime * 0.25f, 0f, 1f);
			if (LocalNavigator.cvLocalNavigatorDebugDraw.value)
			{
				Debug.DrawRay(this.currentSnapshot.position, this.moveVector * 5f, Color.green, deltaTime, false);
			}
		}

		// Token: 0x06003634 RID: 13876 RVA: 0x000E5164 File Offset: 0x000E3364
		private void CompensateForVelocityByRefinement(float nextExpectedDeltaTime, ref Vector3 moveVector, in Vector3 positionToTargetNormalized)
		{
			Vector3 estimatedVelocity = this.currentSnapshotDelta.estimatedVelocity;
			float acceleration = this.currentSnapshot.acceleration;
			float num = Vector3.Dot(estimatedVelocity, positionToTargetNormalized);
			Vector3 vector = positionToTargetNormalized;
			Vector3 vector2 = vector;
			if (!this.currentSnapshot.isJumping)
			{
				for (int i = 0; i < 8; i++)
				{
					Vector3 b;
					Vector3 lhs;
					LocalNavigator.EstimateNextMovement(this.currentSnapshot, this.currentSnapshotDelta, vector2, nextExpectedDeltaTime, out b, out lhs);
					Vector3 vector3 = this.targetPosition - b;
					if (!this.currentSnapshot.isFlying)
					{
						vector3.y = 0f;
					}
					Vector3 normalized = vector3.normalized;
					vector2 = normalized;
					if (Vector3.Dot(lhs, normalized) > num)
					{
						vector = normalized;
					}
				}
			}
			moveVector = vector;
		}

		// Token: 0x06003635 RID: 13877 RVA: 0x000E5218 File Offset: 0x000E3418
		private void CompensateForVelocityByBruteForce(float nextExpectedDeltaTime, ref Vector3 moveVector, in Vector3 positionToTargetNormalized)
		{
			Vector3 estimatedVelocity = this.currentSnapshotDelta.estimatedVelocity;
			LocalNavigator.BodySnapshot bodySnapshot = this.currentSnapshot;
			float num = Vector3.Dot(estimatedVelocity.normalized, positionToTargetNormalized);
			Vector3 vector = moveVector;
			int num2 = 16;
			float num3 = 360f / (float)num2;
			for (int i = 0; i < num2; i++)
			{
				Vector3 vector2 = Quaternion.AngleAxis((float)i * num3, Vector3.up) * Vector3.forward;
				Vector3 b;
				Vector3 vector3;
				LocalNavigator.EstimateNextMovement(this.currentSnapshot, this.currentSnapshotDelta, vector2, nextExpectedDeltaTime, out b, out vector3);
				float num4 = Vector3.Dot((this.targetPosition - b).normalized, vector3.normalized);
				if (num4 > num)
				{
					num = num4;
					vector = vector2;
				}
			}
			moveVector = vector;
		}

		// Token: 0x06003636 RID: 13878 RVA: 0x000E52DC File Offset: 0x000E34DC
		private static void EstimateNextMovement(in LocalNavigator.BodySnapshot currentSnapshot, in LocalNavigator.SnapshotDelta currentSnapshotDelta, in Vector3 moveVector, float nextDeltaTime, out Vector3 nextPosition, out Vector3 nextVelocity)
		{
			Vector3 estimatedVelocity = currentSnapshotDelta.estimatedVelocity;
			float acceleration = currentSnapshot.acceleration;
			nextVelocity = Vector3.MoveTowards(estimatedVelocity, moveVector * currentSnapshot.maxMoveSpeed, acceleration * nextDeltaTime);
			float num = Mathf.Min((nextVelocity - estimatedVelocity).magnitude / acceleration, nextDeltaTime);
			float d = nextDeltaTime - num;
			Vector3 a = (nextVelocity - estimatedVelocity) / num;
			Vector3 b = nextVelocity * num + a * (0.5f * num * num);
			Vector3 b2 = nextVelocity * d;
			nextPosition = currentSnapshot.position + b + b2;
		}

		// Token: 0x06003637 RID: 13879 RVA: 0x000E53A0 File Offset: 0x000E35A0
		private LocalNavigator.RaycastResults GetRaycasts(in LocalNavigator.BodySnapshot currentSnapshot, Vector3 positionToTargetNormalized, float lookaheadTime)
		{
			LocalNavigator.RaycastResults raycastResults = default(LocalNavigator.RaycastResults);
			Vector3 chestPosition = currentSnapshot.chestPosition;
			LayerMask layerMask = LayerIndex.world.mask | LayerIndex.defaultLayer.mask;
			float maxDistance = currentSnapshot.bodyRadius + currentSnapshot.maxMoveSpeed * lookaheadTime;
			RaycastHit raycastHit;
			raycastResults.forwardObstructed = this.Raycast(chestPosition, positionToTargetNormalized, out raycastHit, maxDistance, layerMask);
			if (raycastResults.forwardObstructed)
			{
				Vector3 vector = Quaternion.Euler(0f, LocalNavigator.avoidanceAngle, 0f) * positionToTargetNormalized;
				raycastResults.leftObstructed = this.Raycast(chestPosition, vector, out raycastHit, maxDistance, layerMask);
				vector = Quaternion.Euler(0f, -LocalNavigator.avoidanceAngle, 0f) * positionToTargetNormalized;
				raycastResults.rightObstructed = this.Raycast(chestPosition, vector, out raycastHit, maxDistance, layerMask);
			}
			return raycastResults;
		}

		// Token: 0x06003638 RID: 13880 RVA: 0x000E547C File Offset: 0x000E367C
		private bool Raycast(in Vector3 origin, in Vector3 direction, out RaycastHit hitInfo, float maxDistance, LayerMask layerMask)
		{
			bool flag = Physics.Raycast(origin, direction, out hitInfo, maxDistance, layerMask);
			if (LocalNavigator.cvLocalNavigatorDebugDraw.value)
			{
				Vector3 start = origin;
				Vector3 vector = direction;
				Debug.DrawRay(start, vector.normalized * maxDistance, Color.yellow, LocalNavigator.raycastUpdateInterval);
				if (flag)
				{
					Util.DebugCross(hitInfo.point, 1f, Color.red, LocalNavigator.raycastUpdateInterval);
				}
			}
			return flag;
		}

		// Token: 0x06003639 RID: 13881 RVA: 0x000E54FC File Offset: 0x000E36FC
		private bool Linecast(in Vector3 start, in Vector3 end, out RaycastHit hitInfo, LayerMask layerMask)
		{
			bool flag = Physics.Linecast(start, end, out hitInfo, layerMask);
			if (LocalNavigator.cvLocalNavigatorDebugDraw.value)
			{
				Debug.DrawLine(start, end, Color.yellow, LocalNavigator.raycastUpdateInterval);
				if (flag)
				{
					Util.DebugCross(hitInfo.point, 1f, Color.red, LocalNavigator.raycastUpdateInterval);
				}
			}
			return flag;
		}

		// Token: 0x0600363A RID: 13882 RVA: 0x000E5568 File Offset: 0x000E3768
		private bool CheckCliffAhead(in LocalNavigator.BodySnapshot currentSnapshot, in LocalNavigator.SnapshotDelta currentSnapshotDelta, in Vector3 currentMoveVector, float deltaTime)
		{
			if (!this.allowWalkOffCliff && currentSnapshot.isGrounded)
			{
				Vector3 vector;
				Vector3 vector2;
				LocalNavigator.EstimateNextMovement(currentSnapshot, currentSnapshotDelta, currentMoveVector, deltaTime, out vector, out vector2);
				float num = currentSnapshot.chestPosition.y - currentSnapshot.position.y;
				float num2 = currentSnapshot.footPosition.y - currentSnapshot.position.y;
				Vector3 vector3 = vector;
				vector3.y += num;
				Vector3 vector4 = vector3;
				vector4.y += num2;
				vector4.y -= 4f;
				RaycastHit raycastHit;
				return !this.Linecast(vector3, vector4, out raycastHit, LayerIndex.world.mask);
			}
			return false;
		}

		// Token: 0x0600363B RID: 13883 RVA: 0x000E5618 File Offset: 0x000E3818
		private void CalculateFrustration(float deltaTime, ref float frustration)
		{
			if (!this.currentSnapshot.canJump)
			{
				frustration = 0f;
				return;
			}
			if (!this.currentSnapshot.isValid || !this.currentSnapshotDelta.isValid || !this.currentSnapshotDelta.isValid || !this.previousSnapshotDelta.isValid)
			{
				return;
			}
			if (true)
			{
				Vector3 rhs = LocalNavigator.<CalculateFrustration>g__FlattenDirection|53_0(this.moveVector);
				Vector3 vector = LocalNavigator.<CalculateFrustration>g__FlattenDirection|53_0(this.currentSnapshotDelta.estimatedVelocity);
				Vector3 b = LocalNavigator.<CalculateFrustration>g__FlattenDirection|53_0(this.previousSnapshotDelta.estimatedVelocity);
				float num = Vector3.Dot(vector, rhs);
				this.velocityDelta = vector - b;
				this.estimatedAcceleration = this.velocityDelta / deltaTime;
				float num2 = Vector3.Dot(this.estimatedAcceleration, rhs);
				float num3 = this.estimatedAcceleration.magnitude - num2;
				this.speedAsFractionOfTopSpeed = num / this.currentSnapshot.maxMoveSpeed;
				this.isAlreadyMovingAtSufficientSpeed = (this.speedAsFractionOfTopSpeed >= 0.45f);
				if (this.isAlreadyMovingAtSufficientSpeed)
				{
					frustration = 0f;
					return;
				}
				if (num3 > num2 * 2f)
				{
					frustration += 1.25f * deltaTime;
					return;
				}
			}
			else
			{
				float num4 = this.currentSnapshot.motorVelocity.magnitude + this.bodyComponents.motor.rootMotion.magnitude / deltaTime;
				float magnitude = (this.targetPosition - this.currentSnapshot.position).magnitude;
				if (this.currentSnapshot.maxMoveSpeed != 0f && num4 != 0f && magnitude > Mathf.Epsilon)
				{
					float magnitude2 = this.currentSnapshotDelta.estimatedVelocity.magnitude;
					if (magnitude2 <= num4 || magnitude2 <= 0.1f)
					{
						frustration += 1.25f * deltaTime;
					}
				}
			}
		}

		// Token: 0x0600363E RID: 13886 RVA: 0x000E5824 File Offset: 0x000E3A24
		[CompilerGenerated]
		internal static Vector3 <CalculateFrustration>g__FlattenDirection|53_0(Vector3 vector)
		{
			if (Mathf.Abs(vector.y) > 0f)
			{
				vector.y = 0f;
			}
			return vector;
		}

		// Token: 0x040036B5 RID: 14005
		private Vector3 previousMoveVector;

		// Token: 0x040036B6 RID: 14006
		public Vector3 targetPosition;

		// Token: 0x040036B7 RID: 14007
		public float avoidanceDuration = 0.5f;

		// Token: 0x040036B8 RID: 14008
		private float avoidanceTimer;

		// Token: 0x040036B9 RID: 14009
		public bool allowWalkOffCliff;

		// Token: 0x040036BA RID: 14010
		public bool wasObstructedLastUpdate;

		// Token: 0x040036BB RID: 14011
		private float localTime;

		// Token: 0x040036BC RID: 14012
		private float raycastTimer;

		// Token: 0x040036BD RID: 14013
		private static readonly float raycastUpdateInterval = 0.2f;

		// Token: 0x040036BE RID: 14014
		private static readonly float avoidanceAngle = 45f;

		// Token: 0x040036BF RID: 14015
		private const bool enableFrustration = true;

		// Token: 0x040036C0 RID: 14016
		private const bool enableWhiskers = true;

		// Token: 0x040036C1 RID: 14017
		private const bool enableCliffAvoidance = true;

		// Token: 0x040036C2 RID: 14018
		private LocalNavigator.BodyComponents bodyComponents;

		// Token: 0x040036C5 RID: 14021
		private float walkFrustration;

		// Token: 0x040036C6 RID: 14022
		private const float frustrationLimit = 1f;

		// Token: 0x040036C7 RID: 14023
		private const float frustrationDecayRate = 0.25f;

		// Token: 0x040036C8 RID: 14024
		private const float frustrationMinimumSpeed = 0.1f;

		// Token: 0x040036C9 RID: 14025
		private float aiStopwatch;

		// Token: 0x040036CA RID: 14026
		private Vector3 velocityDelta;

		// Token: 0x040036CB RID: 14027
		private Vector3 estimatedAcceleration;

		// Token: 0x040036CC RID: 14028
		private float accelerationAccuracy;

		// Token: 0x040036CD RID: 14029
		private float moveDirectionAccuracy;

		// Token: 0x040036CE RID: 14030
		private bool hasMadeSharpTurn;

		// Token: 0x040036CF RID: 14031
		private float speedAsFractionOfTopSpeed;

		// Token: 0x040036D0 RID: 14032
		private bool isAlreadyMovingAtSufficientSpeed;

		// Token: 0x040036D1 RID: 14033
		private LocalNavigator.BodySnapshot currentSnapshot;

		// Token: 0x040036D2 RID: 14034
		private LocalNavigator.BodySnapshot previousSnapshot;

		// Token: 0x040036D3 RID: 14035
		private LocalNavigator.SnapshotDelta currentSnapshotDelta;

		// Token: 0x040036D4 RID: 14036
		private LocalNavigator.SnapshotDelta previousSnapshotDelta;

		// Token: 0x040036D5 RID: 14037
		private LocalNavigator.RaycastResults raycastResults;

		// Token: 0x040036D6 RID: 14038
		private static readonly BoolConVar cvLocalNavigatorDebugDraw = new BoolConVar("local_navigator_debug_draw", ConVarFlags.None, "0", "Enables debug drawing of LocalNavigator (drawing visible in editor only).\n  Orange Line: Current position to target position\n  Yellow Line: Raycasts\n  Red Point: Raycast hit position\n  Green Line: Final chosen move vector");

		// Token: 0x02000959 RID: 2393
		private readonly struct BodyComponents
		{
			// Token: 0x0600363F RID: 13887 RVA: 0x000E5848 File Offset: 0x000E3A48
			public BodyComponents(CharacterBody body)
			{
				this = default(LocalNavigator.BodyComponents);
				this.body = body;
				if (body)
				{
					this.transform = body.transform;
					this.motor = body.characterMotor;
					if (this.motor)
					{
						this.bodyCollider = body.characterMotor.Motor.Capsule;
					}
				}
			}

			// Token: 0x040036D7 RID: 14039
			public readonly CharacterBody body;

			// Token: 0x040036D8 RID: 14040
			public readonly Transform transform;

			// Token: 0x040036D9 RID: 14041
			public readonly CharacterMotor motor;

			// Token: 0x040036DA RID: 14042
			public readonly Collider bodyCollider;
		}

		// Token: 0x0200095A RID: 2394
		private readonly struct BodySnapshot
		{
			// Token: 0x17000501 RID: 1281
			// (get) Token: 0x06003640 RID: 13888 RVA: 0x000E58A6 File Offset: 0x000E3AA6
			public bool isValid
			{
				get
				{
					return this.hasBody;
				}
			}

			// Token: 0x17000502 RID: 1282
			// (get) Token: 0x06003641 RID: 13889 RVA: 0x000E58AE File Offset: 0x000E3AAE
			public bool canJump
			{
				get
				{
					return this.isGrounded && this.maxJumpSpeed > 0f;
				}
			}

			// Token: 0x06003642 RID: 13890 RVA: 0x000E58C8 File Offset: 0x000E3AC8
			public BodySnapshot(in LocalNavigator.BodyComponents bodyComponents, float time)
			{
				this = default(LocalNavigator.BodySnapshot);
				this.time = time;
				this.hasBody = bodyComponents.body;
				this.hasBodyCollider = bodyComponents.bodyCollider;
				this.hasMotor = bodyComponents.motor;
				if (bodyComponents.body)
				{
					this.position = bodyComponents.transform.position;
					this.chestPosition = this.position;
					this.footPosition = this.position;
					this.maxMoveSpeed = bodyComponents.body.moveSpeed;
					this.acceleration = bodyComponents.body.acceleration;
					this.maxJumpHeight = bodyComponents.body.maxJumpHeight;
					this.maxJumpSpeed = bodyComponents.body.jumpPower;
					this.bodyRadius = bodyComponents.body.radius;
					this.isFlying = bodyComponents.body.isFlying;
				}
				if (bodyComponents.bodyCollider)
				{
					Bounds bounds = bodyComponents.bodyCollider.bounds;
					bounds.center = Vector3.zero;
					this.chestPosition.y = this.chestPosition.y + bounds.max.y * 0.5f;
					this.footPosition.y = this.footPosition.y + bounds.min.y;
				}
				if (bodyComponents.motor)
				{
					this.isGrounded = bodyComponents.motor.isGrounded;
					this.motorVelocity = bodyComponents.motor.velocity;
					this.isJumping = !this.isGrounded;
				}
			}

			// Token: 0x040036DB RID: 14043
			public readonly Vector3 position;

			// Token: 0x040036DC RID: 14044
			public readonly Vector3 chestPosition;

			// Token: 0x040036DD RID: 14045
			public readonly Vector3 footPosition;

			// Token: 0x040036DE RID: 14046
			public readonly Vector3 motorMoveDirection;

			// Token: 0x040036DF RID: 14047
			public readonly Vector3 motorVelocity;

			// Token: 0x040036E0 RID: 14048
			public readonly float maxMoveSpeed;

			// Token: 0x040036E1 RID: 14049
			public readonly float acceleration;

			// Token: 0x040036E2 RID: 14050
			public readonly float maxJumpHeight;

			// Token: 0x040036E3 RID: 14051
			public readonly float maxJumpSpeed;

			// Token: 0x040036E4 RID: 14052
			public readonly bool isGrounded;

			// Token: 0x040036E5 RID: 14053
			public readonly float time;

			// Token: 0x040036E6 RID: 14054
			public readonly float bodyRadius;

			// Token: 0x040036E7 RID: 14055
			public readonly bool isFlying;

			// Token: 0x040036E8 RID: 14056
			public readonly bool isJumping;

			// Token: 0x040036E9 RID: 14057
			private readonly bool hasBody;

			// Token: 0x040036EA RID: 14058
			private readonly bool hasBodyCollider;

			// Token: 0x040036EB RID: 14059
			private readonly bool hasMotor;
		}

		// Token: 0x0200095B RID: 2395
		private readonly struct SnapshotDelta
		{
			// Token: 0x06003643 RID: 13891 RVA: 0x000E5A54 File Offset: 0x000E3C54
			public SnapshotDelta(in LocalNavigator.BodySnapshot oldSnapshot, in LocalNavigator.BodySnapshot newSnapshot)
			{
				this = default(LocalNavigator.SnapshotDelta);
				if (oldSnapshot.isValid && newSnapshot.isValid)
				{
					this.deltaTime = newSnapshot.time - oldSnapshot.time;
					this.isValid = (this.deltaTime > 0f);
					if (this.isValid)
					{
						this.estimatedVelocity = (newSnapshot.position - oldSnapshot.position) / this.deltaTime;
					}
				}
			}

			// Token: 0x040036EC RID: 14060
			public readonly float deltaTime;

			// Token: 0x040036ED RID: 14061
			public readonly Vector3 estimatedVelocity;

			// Token: 0x040036EE RID: 14062
			public readonly bool isValid;
		}

		// Token: 0x0200095C RID: 2396
		private struct RaycastResults
		{
			// Token: 0x040036EF RID: 14063
			public bool forwardObstructed;

			// Token: 0x040036F0 RID: 14064
			public bool leftObstructed;

			// Token: 0x040036F1 RID: 14065
			public bool rightObstructed;
		}
	}
}
