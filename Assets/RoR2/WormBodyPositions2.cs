using System;
using System.Collections.Generic;
using EntityStates.MagmaWorm;
using RoR2.Projectile;
using Unity;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Networking;

namespace RoR2
{
	// Token: 0x02000909 RID: 2313
	[RequireComponent(typeof(CharacterBody))]
	public class WormBodyPositions2 : NetworkBehaviour, ITeleportHandler, IEventSystemHandler, ILifeBehavior, IPainAnimationHandler
	{
		// Token: 0x0600342F RID: 13359 RVA: 0x000DC024 File Offset: 0x000DA224
		private void Awake()
		{
			this.characterBody = base.GetComponent<CharacterBody>();
			this.characterDirection = base.GetComponent<CharacterDirection>();
			this.boneTransformationBuffer = new WormBodyPositions2.PositionRotationPair[this.bones.Length + 1];
			this.totalLength = 0f;
			for (int i = 0; i < this.segmentLengths.Length; i++)
			{
				this.totalLength += this.segmentLengths[i];
			}
			if (NetworkServer.active)
			{
				this.travelCallbacks = new List<WormBodyPositions2.TravelCallback>();
			}
			this.boneDisplacements = new Vector3[this.bones.Length];
		}

		// Token: 0x06003430 RID: 13360 RVA: 0x000DC0B8 File Offset: 0x000DA2B8
		private void Start()
		{
			if (NetworkServer.active)
			{
				this.PopulateInitialKeyFrames();
				this.previousSurfaceTestEnd = this.newestKeyFrame.curve.Evaluate(1f);
			}
		}

		// Token: 0x06003431 RID: 13361 RVA: 0x000DC0F0 File Offset: 0x000DA2F0
		private void OnDestroy()
		{
			this.travelCallbacks = null;
			this._playingBurrowSound = false;
		}

		// Token: 0x06003432 RID: 13362 RVA: 0x000DC100 File Offset: 0x000DA300
		private void BakeSegmentLengths()
		{
			this.segmentLengths = new float[this.bones.Length];
			Vector3 a = this.bones[0].position;
			for (int i = 1; i < this.bones.Length; i++)
			{
				Vector3 position = this.bones[i].position;
				float magnitude = (a - position).magnitude;
				this.segmentLengths[i - 1] = magnitude;
				a = position;
			}
			this.segmentLengths[this.bones.Length - 1] = 2f;
		}

		// Token: 0x170004D7 RID: 1239
		// (get) Token: 0x06003433 RID: 13363 RVA: 0x000DC184 File Offset: 0x000DA384
		// (set) Token: 0x06003434 RID: 13364 RVA: 0x000DC18C File Offset: 0x000DA38C
		public WormBodyPositions2.KeyFrame newestKeyFrame { get; private set; }

		// Token: 0x06003435 RID: 13365 RVA: 0x000DC198 File Offset: 0x000DA398
		[Server]
		private void PopulateInitialKeyFrames()
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Void RoR2.WormBodyPositions2::PopulateInitialKeyFrames()' called on client");
				return;
			}
			bool flag = this.enableSurfaceTests;
			this.enableSurfaceTests = false;
			Vector3 vector = base.transform.position + Vector3.down * this.spawnDepth;
			float synchronizedTimeStamp = this.GetSynchronizedTimeStamp();
			this.newestKeyFrame = new WormBodyPositions2.KeyFrame
			{
				curve = CubicBezier3.FromVelocities(vector + Vector3.down * 2f, Vector3.up, vector + Vector3.down * 1f, Vector3.down),
				time = synchronizedTimeStamp - 2f,
				length = 1f
			};
			this.AttemptToGenerateKeyFrame(synchronizedTimeStamp - 1f, vector + Vector3.down, Vector3.up);
			this.AttemptToGenerateKeyFrame(synchronizedTimeStamp, vector, Vector3.up);
			this.headDistance = 0f;
			this.enableSurfaceTests = flag;
		}

		// Token: 0x06003436 RID: 13366 RVA: 0x000DC298 File Offset: 0x000DA498
		private Vector3 EvaluatePositionAlongCurve(float positionDownBody)
		{
			float num = 0f;
			foreach (WormBodyPositions2.KeyFrame keyFrame in this.keyFrames)
			{
				float b = num;
				num += keyFrame.length;
				if (num >= positionDownBody)
				{
					float t = Mathf.InverseLerp(num, b, positionDownBody);
					CubicBezier3 curve = keyFrame.curve;
					return curve.Evaluate(t);
				}
			}
			if (this.keyFrames.Count > 0)
			{
				return this.keyFrames[this.keyFrames.Count - 1].curve.Evaluate(1f);
			}
			return Vector3.zero;
		}

		// Token: 0x06003437 RID: 13367 RVA: 0x000DC35C File Offset: 0x000DA55C
		private void UpdateBones()
		{
			float num = this.totalLength;
			this.boneTransformationBuffer[this.boneTransformationBuffer.Length - 1] = new WormBodyPositions2.PositionRotationPair
			{
				position = this.EvaluatePositionAlongCurve(this.headDistance + num),
				rotation = Quaternion.identity
			};
			for (int i = this.boneTransformationBuffer.Length - 2; i >= 0; i--)
			{
				num -= this.segmentLengths[i];
				Vector3 vector = this.EvaluatePositionAlongCurve(this.headDistance + num);
				Quaternion rotation = Util.QuaternionSafeLookRotation(vector - this.boneTransformationBuffer[i + 1].position, Vector3.up);
				this.boneTransformationBuffer[i] = new WormBodyPositions2.PositionRotationPair
				{
					position = vector,
					rotation = rotation
				};
			}
			if (this.bones.Length != 0 && this.bones[0])
			{
				Vector3 forward = this.bones[0].forward;
				for (int j = 0; j < this.bones.Length; j++)
				{
					Transform transform = this.bones[j];
					if (transform)
					{
						transform.position = this.boneTransformationBuffer[j].position + this.boneDisplacements[j];
						transform.forward = forward;
						transform.up = this.boneTransformationBuffer[j].rotation * -Vector3.forward;
						forward = transform.forward;
					}
				}
			}
		}

		// Token: 0x06003438 RID: 13368 RVA: 0x000DC4EC File Offset: 0x000DA6EC
		public void AddKeyFrame(in WormBodyPositions2.KeyFrame newKeyFrame)
		{
			this.newestKeyFrame = newKeyFrame;
			this.keyFrames.Insert(0, newKeyFrame);
			this.keyFramesTotalLength += newKeyFrame.length;
			this.headDistance += newKeyFrame.length;
			bool flag = false;
			float num = this.keyFramesTotalLength;
			float num2 = this.totalLength + this.headDistance + 4f;
			while (this.keyFrames.Count > 0 && (num -= this.keyFrames[this.keyFrames.Count - 1].length) > num2)
			{
				this.keyFrames.RemoveAt(this.keyFrames.Count - 1);
				flag = true;
			}
			if (flag)
			{
				this.keyFramesTotalLength = 0f;
				foreach (WormBodyPositions2.KeyFrame keyFrame in this.keyFrames)
				{
					this.keyFramesTotalLength += keyFrame.length;
				}
			}
			if (NetworkServer.active)
			{
				this.CallRpcSendKeyFrame(newKeyFrame);
				if (this.enableSurfaceTests)
				{
					CubicBezier3 curve = newKeyFrame.curve;
					WormBodyPositions2.SurfaceTest(curve.p1, ref this.previousSurfaceTestEnd, newKeyFrame.time, new WormBodyPositions2.BurrowExpectedCallback(this.OnPredictedBurrowDiscovered), new WormBodyPositions2.BreachExpectedCallback(this.OnPredictedBreachDiscovered));
				}
			}
		}

		// Token: 0x06003439 RID: 13369 RVA: 0x000DC660 File Offset: 0x000DA860
		[Server]
		public void AttemptToGenerateKeyFrame(float arrivalTime, Vector3 position, Vector3 velocity)
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Void RoR2.WormBodyPositions2::AttemptToGenerateKeyFrame(System.Single,UnityEngine.Vector3,UnityEngine.Vector3)' called on client");
				return;
			}
			WormBodyPositions2.KeyFrame newestKeyFrame = this.newestKeyFrame;
			float num = arrivalTime - newestKeyFrame.time;
			CubicBezier3 curve = CubicBezier3.FromVelocities(newestKeyFrame.curve.p1, -newestKeyFrame.curve.v1, position, -velocity * (num * 0.25f));
			float length = curve.ApproximateLength(50);
			WormBodyPositions2.KeyFrame keyFrame = new WormBodyPositions2.KeyFrame
			{
				curve = curve,
				length = length,
				time = arrivalTime
			};
			if (keyFrame.length >= 0f)
			{
				this.AddKeyFrame(keyFrame);
			}
		}

		// Token: 0x0600343A RID: 13370 RVA: 0x000DC70C File Offset: 0x000DA90C
		[ClientRpc]
		private void RpcSendKeyFrame(WormBodyPositions2.KeyFrame newKeyFrame)
		{
			if (!NetworkServer.active)
			{
				this.AddKeyFrame(newKeyFrame);
			}
		}

		// Token: 0x0600343B RID: 13371 RVA: 0x000DC71D File Offset: 0x000DA91D
		private void Update()
		{
			this.UpdateBoneDisplacements(Time.deltaTime);
			this.UpdateHeadOffset();
			if (this.animateJaws)
			{
				this.UpdateJaws();
			}
		}

		// Token: 0x0600343C RID: 13372 RVA: 0x000DC740 File Offset: 0x000DA940
		private void UpdateJaws()
		{
			if (this.animator)
			{
				float value = Mathf.Clamp01(Util.Remap((this.bones[0].position - base.transform.position).magnitude, this.jawClosedDistance, this.jawOpenDistance, 0f, 1f));
				this.animator.SetFloat(this.jawMecanimCycleParameter, value, this.jawMecanimDampTime, Time.deltaTime);
			}
		}

		// Token: 0x0600343D RID: 13373 RVA: 0x000DC7C0 File Offset: 0x000DA9C0
		private void UpdateHeadOffset()
		{
			float num = this.headDistance;
			int num2 = this.keyFrames.Count - 1;
			float num3 = 0f;
			float synchronizedTimeStamp = this.GetSynchronizedTimeStamp();
			for (int i = 0; i < num2; i++)
			{
				float time = this.keyFrames[i + 1].time;
				float length = this.keyFrames[i].length;
				if (time < synchronizedTimeStamp)
				{
					num = num3 + length * Mathf.InverseLerp(this.keyFrames[i].time, time, synchronizedTimeStamp);
					break;
				}
				num3 += length;
			}
			this.OnTravel(this.headDistance - num);
		}

		// Token: 0x170004D8 RID: 1240
		// (get) Token: 0x0600343E RID: 13374 RVA: 0x000DC85F File Offset: 0x000DAA5F
		// (set) Token: 0x0600343F RID: 13375 RVA: 0x000DC867 File Offset: 0x000DAA67
		public bool underground { get; private set; }

		// Token: 0x06003440 RID: 13376 RVA: 0x000DC870 File Offset: 0x000DAA70
		private void OnTravel(float distance)
		{
			this.headDistance -= distance;
			this.UpdateBones();
		}

		// Token: 0x140000B1 RID: 177
		// (add) Token: 0x06003441 RID: 13377 RVA: 0x000DC888 File Offset: 0x000DAA88
		// (remove) Token: 0x06003442 RID: 13378 RVA: 0x000DC8C0 File Offset: 0x000DAAC0
		public event WormBodyPositions2.BurrowExpectedCallback onPredictedBurrowDiscovered;

		// Token: 0x140000B2 RID: 178
		// (add) Token: 0x06003443 RID: 13379 RVA: 0x000DC8F8 File Offset: 0x000DAAF8
		// (remove) Token: 0x06003444 RID: 13380 RVA: 0x000DC930 File Offset: 0x000DAB30
		public event WormBodyPositions2.BreachExpectedCallback onPredictedBreachDiscovered;

		// Token: 0x06003445 RID: 13381 RVA: 0x000DC968 File Offset: 0x000DAB68
		private void OnPredictedBurrowDiscovered(float expectedTime, Vector3 point, Vector3 surfaceNormal)
		{
			this.AddTravelCallback(new WormBodyPositions2.TravelCallback
			{
				time = expectedTime,
				callback = delegate()
				{
					this.OnEnterSurface(point, surfaceNormal);
				}
			});
			this.AddTravelCallback(new WormBodyPositions2.TravelCallback
			{
				time = expectedTime - 0.5f,
				callback = new Action(this.RpcPlaySurfaceImpactSound)
			});
			WormBodyPositions2.BurrowExpectedCallback burrowExpectedCallback = this.onPredictedBurrowDiscovered;
			if (burrowExpectedCallback == null)
			{
				return;
			}
			burrowExpectedCallback(expectedTime, point, surfaceNormal);
		}

		// Token: 0x06003446 RID: 13382 RVA: 0x000DCA08 File Offset: 0x000DAC08
		private void OnPredictedBreachDiscovered(float expectedTime, Vector3 point, Vector3 surfaceNormal)
		{
			if (this.warningEffectPrefab)
			{
				EffectManager.SpawnEffect(this.warningEffectPrefab, new EffectData
				{
					origin = point,
					rotation = Util.QuaternionSafeLookRotation(surfaceNormal)
				}, true);
			}
			this.AddTravelCallback(new WormBodyPositions2.TravelCallback
			{
				time = expectedTime,
				callback = delegate()
				{
					this.OnExitSurface(point, surfaceNormal);
				}
			});
			this.AddTravelCallback(new WormBodyPositions2.TravelCallback
			{
				time = expectedTime - 0.5f,
				callback = new Action(this.RpcPlaySurfaceImpactSound)
			});
			WormBodyPositions2.BreachExpectedCallback breachExpectedCallback = this.onPredictedBreachDiscovered;
			if (breachExpectedCallback == null)
			{
				return;
			}
			breachExpectedCallback(expectedTime, point, surfaceNormal);
		}

		// Token: 0x06003447 RID: 13383 RVA: 0x000DCAE4 File Offset: 0x000DACE4
		[Server]
		private static void SurfaceTest(Vector3 currentPosition, ref Vector3 previousPosition, float arrivalTime, WormBodyPositions2.BurrowExpectedCallback onPredictedBurrowDiscovered, WormBodyPositions2.BreachExpectedCallback onPredictedBreachDiscovered)
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Void RoR2.WormBodyPositions2::SurfaceTest(UnityEngine.Vector3,UnityEngine.Vector3&,System.Single,RoR2.WormBodyPositions2/BurrowExpectedCallback,RoR2.WormBodyPositions2/BreachExpectedCallback)' called on client");
				return;
			}
			Vector3 vector = currentPosition - previousPosition;
			float magnitude = vector.magnitude;
			RaycastHit raycastHit;
			if (Physics.Raycast(previousPosition, vector, out raycastHit, magnitude, LayerIndex.world.mask, QueryTriggerInteraction.Ignore) && onPredictedBurrowDiscovered != null)
			{
				onPredictedBurrowDiscovered(arrivalTime, raycastHit.point, raycastHit.normal);
			}
			if (Physics.Raycast(currentPosition, -vector, out raycastHit, magnitude, LayerIndex.world.mask, QueryTriggerInteraction.Ignore) && onPredictedBreachDiscovered != null)
			{
				onPredictedBreachDiscovered(arrivalTime, raycastHit.point, raycastHit.normal);
			}
			previousPosition = currentPosition;
		}

		// Token: 0x06003448 RID: 13384 RVA: 0x000DCB9C File Offset: 0x000DAD9C
		public void AddTravelCallback(WormBodyPositions2.TravelCallback newTravelCallback)
		{
			int index = this.travelCallbacks.Count;
			float time = newTravelCallback.time;
			for (int i = 0; i < this.travelCallbacks.Count; i++)
			{
				if (time < this.travelCallbacks[i].time)
				{
					index = i;
					break;
				}
			}
			this.travelCallbacks.Insert(index, newTravelCallback);
		}

		// Token: 0x06003449 RID: 13385 RVA: 0x000026ED File Offset: 0x000008ED
		[ClientRpc]
		private void RpcPlaySurfaceImpactSound()
		{
		}

		// Token: 0x0600344A RID: 13386 RVA: 0x000DCBF8 File Offset: 0x000DADF8
		[Server]
		private void OnEnterSurface(Vector3 point, Vector3 surfaceNormal)
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Void RoR2.WormBodyPositions2::OnEnterSurface(UnityEngine.Vector3,UnityEngine.Vector3)' called on client");
				return;
			}
			if (this.enterTriggerCooldownTimer > 0f)
			{
				return;
			}
			if (this.shouldTriggerDeathEffectOnNextImpact && Run.instance.fixedTime - this.deathTime >= DeathState.duration - 3f)
			{
				this.shouldTriggerDeathEffectOnNextImpact = false;
				return;
			}
			this.enterTriggerCooldownTimer = this.impactCooldownDuration;
			EffectManager.SpawnEffect(this.burrowEffectPrefab, new EffectData
			{
				origin = point,
				rotation = Util.QuaternionSafeLookRotation(surfaceNormal),
				scale = 1f
			}, true);
			if (this.shouldFireMeatballsOnImpact)
			{
				this.FireMeatballs(surfaceNormal, point + surfaceNormal * 3f, this.characterDirection.forward, this.meatballCount, this.meatballAngle, this.meatballForce);
			}
			if (this.shouldFireBlastAttackOnImpact)
			{
				this.FireImpactBlastAttack(point + surfaceNormal);
			}
		}

		// Token: 0x0600344B RID: 13387 RVA: 0x000DCCE3 File Offset: 0x000DAEE3
		public void OnDeathStart()
		{
			this.deathTime = Run.instance.fixedTime;
			this.shouldTriggerDeathEffectOnNextImpact = true;
		}

		// Token: 0x0600344C RID: 13388 RVA: 0x000DCCFC File Offset: 0x000DAEFC
		[Server]
		private void PerformDeath()
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Void RoR2.WormBodyPositions2::PerformDeath()' called on client");
				return;
			}
			for (int i = 0; i < this.bones.Length; i++)
			{
				if (this.bones[i])
				{
					EffectManager.SpawnEffect(LegacyResourcesAPI.Load<GameObject>("Prefabs/Effects/MagmaWormDeathDust"), new EffectData
					{
						origin = this.bones[i].position,
						rotation = UnityEngine.Random.rotation,
						scale = 1f
					}, true);
				}
			}
			UnityEngine.Object.Destroy(base.gameObject);
		}

		// Token: 0x0600344D RID: 13389 RVA: 0x000DCD8C File Offset: 0x000DAF8C
		[Server]
		private void OnExitSurface(Vector3 point, Vector3 surfaceNormal)
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Void RoR2.WormBodyPositions2::OnExitSurface(UnityEngine.Vector3,UnityEngine.Vector3)' called on client");
				return;
			}
			if (this.exitTriggerCooldownTimer > 0f)
			{
				return;
			}
			this.exitTriggerCooldownTimer = this.impactCooldownDuration;
			EffectManager.SpawnEffect(this.burrowEffectPrefab, new EffectData
			{
				origin = point,
				rotation = Util.QuaternionSafeLookRotation(surfaceNormal),
				scale = 1f
			}, true);
			if (this.shouldFireMeatballsOnImpact)
			{
				this.FireMeatballs(surfaceNormal, point + surfaceNormal * 3f, this.characterDirection.forward, this.meatballCount, this.meatballAngle, this.meatballForce);
			}
		}

		// Token: 0x0600344E RID: 13390 RVA: 0x000DCE34 File Offset: 0x000DB034
		private void FireMeatballs(Vector3 impactNormal, Vector3 impactPosition, Vector3 forward, int meatballCount, float meatballAngle, float meatballForce)
		{
			float num = 360f / (float)meatballCount;
			Vector3 normalized = Vector3.ProjectOnPlane(forward, impactNormal).normalized;
			Vector3 point = Vector3.RotateTowards(impactNormal, normalized, meatballAngle * 0.017453292f, float.PositiveInfinity);
			for (int i = 0; i < meatballCount; i++)
			{
				Vector3 forward2 = Quaternion.AngleAxis(num * (float)i, impactNormal) * point;
				ProjectileManager.instance.FireProjectile(this.meatballProjectile, impactPosition, Util.QuaternionSafeLookRotation(forward2), base.gameObject, this.characterBody.damage * this.meatballDamageCoefficient, meatballForce, Util.CheckRoll(this.characterBody.crit, this.characterBody.master), DamageColorIndex.Default, null, -1f);
			}
		}

		// Token: 0x0600344F RID: 13391 RVA: 0x000DCEE8 File Offset: 0x000DB0E8
		private void FireImpactBlastAttack(Vector3 impactPosition)
		{
			BlastAttack blastAttack = new BlastAttack();
			blastAttack.baseDamage = this.characterBody.damage * this.blastAttackDamageCoefficient;
			blastAttack.procCoefficient = this.blastAttackProcCoefficient;
			blastAttack.baseForce = this.blastAttackForce;
			blastAttack.bonusForce = Vector3.up * this.blastAttackBonusVerticalForce;
			blastAttack.crit = Util.CheckRoll(this.characterBody.crit, this.characterBody.master);
			blastAttack.radius = this.blastAttackRadius;
			blastAttack.damageType = DamageType.IgniteOnHit;
			blastAttack.falloffModel = BlastAttack.FalloffModel.SweetSpot;
			blastAttack.attacker = base.gameObject;
			blastAttack.teamIndex = TeamComponent.GetObjectTeam(blastAttack.attacker);
			blastAttack.position = impactPosition;
			blastAttack.attackerFiltering = AttackerFiltering.NeverHitSelf;
			blastAttack.Fire();
			if (NetworkServer.active)
			{
				EffectManager.SpawnEffect(this.blastAttackEffect, new EffectData
				{
					origin = impactPosition,
					scale = this.blastAttackRadius
				}, true);
			}
		}

		// Token: 0x06003450 RID: 13392 RVA: 0x000DCFDC File Offset: 0x000DB1DC
		private void FixedUpdate()
		{
			if (NetworkServer.active)
			{
				this.enterTriggerCooldownTimer -= Time.fixedDeltaTime;
				this.exitTriggerCooldownTimer -= Time.fixedDeltaTime;
				float synchronizedTimeStamp = this.GetSynchronizedTimeStamp();
				while (this.travelCallbacks.Count > 0 && this.travelCallbacks[0].time <= synchronizedTimeStamp)
				{
					ref WormBodyPositions2.TravelCallback ptr = this.travelCallbacks[0];
					this.travelCallbacks.RemoveAt(0);
					ptr.callback();
				}
			}
			if (this.bones.Length != 0 && this.bones[0] && this.bones[0].transform && base.transform)
			{
				bool playingBurrowSound = this.bones[0].transform.position.y - base.transform.position.y < this.undergroundTestYOffset;
				this.playingBurrowSound = playingBurrowSound;
			}
		}

		// Token: 0x170004D9 RID: 1241
		// (get) Token: 0x06003451 RID: 13393 RVA: 0x000DD0D2 File Offset: 0x000DB2D2
		// (set) Token: 0x06003452 RID: 13394 RVA: 0x000DD0DA File Offset: 0x000DB2DA
		private bool playingBurrowSound
		{
			get
			{
				return this._playingBurrowSound;
			}
			set
			{
				if (value == this._playingBurrowSound)
				{
					return;
				}
				this._playingBurrowSound = value;
				Util.PlaySound(value ? "Play_magmaWorm_burrowed_loop" : "Stop_magmaWorm_burrowed_loop", this.bones[0].gameObject);
			}
		}

		// Token: 0x06003453 RID: 13395 RVA: 0x000DD110 File Offset: 0x000DB310
		private void DrawKeyFrame(WormBodyPositions2.KeyFrame keyFrame)
		{
			Gizmos.color = Color.Lerp(Color.green, Color.black, 0.5f);
			Gizmos.DrawRay(keyFrame.curve.p0, keyFrame.curve.v0);
			Gizmos.color = Color.Lerp(Color.red, Color.black, 0.5f);
			Gizmos.DrawRay(keyFrame.curve.p1, keyFrame.curve.v1);
			for (int i = 1; i <= 20; i++)
			{
				float num = (float)i * 0.05f;
				Gizmos.color = Color.Lerp(Color.red, Color.green, num);
				Vector3 vector = keyFrame.curve.Evaluate(num - 0.05f);
				Vector3 a = keyFrame.curve.Evaluate(num);
				Gizmos.DrawRay(vector, a - vector);
			}
		}

		// Token: 0x06003454 RID: 13396 RVA: 0x000DD1E4 File Offset: 0x000DB3E4
		private void OnDrawGizmos()
		{
			foreach (WormBodyPositions2.KeyFrame keyFrame in this.keyFrames)
			{
				this.DrawKeyFrame(keyFrame);
			}
			for (int i = 0; i < this.boneTransformationBuffer.Length; i++)
			{
				Gizmos.matrix = Matrix4x4.TRS(this.boneTransformationBuffer[i].position, this.boneTransformationBuffer[i].rotation, Vector3.one * 3f);
				Gizmos.DrawRay(-Vector3.forward, Vector3.forward * 2f);
				Gizmos.DrawRay(-Vector3.right, Vector3.right * 2f);
				Gizmos.DrawRay(-Vector3.up, Vector3.up * 2f);
			}
		}

		// Token: 0x06003455 RID: 13397 RVA: 0x000DD2E8 File Offset: 0x000DB4E8
		public void OnTeleport(Vector3 oldPosition, Vector3 newPosition)
		{
			Vector3 b = newPosition - oldPosition;
			for (int i = 0; i < this.keyFrames.Count; i++)
			{
				WormBodyPositions2.KeyFrame keyFrame = this.keyFrames[i];
				CubicBezier3 curve = keyFrame.curve;
				curve.a += b;
				curve.b += b;
				curve.c += b;
				curve.d += b;
				keyFrame.curve = curve;
				this.keyFrames[i] = keyFrame;
			}
			this.previousSurfaceTestEnd += b;
		}

		// Token: 0x06003456 RID: 13398 RVA: 0x000DD3B4 File Offset: 0x000DB5B4
		private int FindNearestBone(Vector3 worldPosition)
		{
			int result = -1;
			float num = float.PositiveInfinity;
			for (int i = 0; i < this.bones.Length; i++)
			{
				float sqrMagnitude = (this.bones[i].transform.position - worldPosition).sqrMagnitude;
				if (sqrMagnitude < num)
				{
					num = sqrMagnitude;
					result = i;
				}
			}
			return result;
		}

		// Token: 0x06003457 RID: 13399 RVA: 0x000DD408 File Offset: 0x000DB608
		private void UpdateBoneDisplacements(float deltaTime)
		{
			int i = 0;
			int num = this.boneDisplacements.Length;
			while (i < num)
			{
				this.boneDisplacements[i] = Vector3.MoveTowards(this.boneDisplacements[i], Vector3.zero, this.painDisplacementRecoverySpeed * deltaTime);
				i++;
			}
		}

		// Token: 0x06003458 RID: 13400 RVA: 0x000DD454 File Offset: 0x000DB654
		void IPainAnimationHandler.HandlePain(float damage, Vector3 damagePosition)
		{
			int num = this.FindNearestBone(damagePosition);
			if (num != -1)
			{
				this.boneDisplacements[num] = UnityEngine.Random.onUnitSphere * this.maxPainDisplacementMagnitude;
			}
		}

		// Token: 0x06003459 RID: 13401 RVA: 0x000DD489 File Offset: 0x000DB689
		public float GetSynchronizedTimeStamp()
		{
			return Run.instance.time;
		}

		// Token: 0x0600345A RID: 13402 RVA: 0x000DD498 File Offset: 0x000DB698
		private static void WriteKeyFrame(NetworkWriter writer, WormBodyPositions2.KeyFrame keyFrame)
		{
			writer.Write(keyFrame.curve.a);
			writer.Write(keyFrame.curve.b);
			writer.Write(keyFrame.curve.c);
			writer.Write(keyFrame.curve.d);
			writer.Write(keyFrame.length);
			writer.Write(keyFrame.time);
		}

		// Token: 0x0600345B RID: 13403 RVA: 0x000DD504 File Offset: 0x000DB704
		private static WormBodyPositions2.KeyFrame ReadKeyFrame(NetworkReader reader)
		{
			WormBodyPositions2.KeyFrame result = default(WormBodyPositions2.KeyFrame);
			result.curve.a = reader.ReadVector3();
			result.curve.b = reader.ReadVector3();
			result.curve.c = reader.ReadVector3();
			result.curve.d = reader.ReadVector3();
			result.length = reader.ReadSingle();
			result.time = reader.ReadSingle();
			return result;
		}

		// Token: 0x0600345C RID: 13404 RVA: 0x000DD57C File Offset: 0x000DB77C
		public override bool OnSerialize(NetworkWriter writer, bool initialState)
		{
			uint syncVarDirtyBits = base.syncVarDirtyBits;
			if (initialState)
			{
				writer.Write((ushort)this.keyFrames.Count);
				for (int i = 0; i < this.keyFrames.Count; i++)
				{
					WormBodyPositions2.WriteKeyFrame(writer, this.keyFrames[i]);
				}
			}
			return !initialState && syncVarDirtyBits > 0U;
		}

		// Token: 0x0600345D RID: 13405 RVA: 0x000DD5D8 File Offset: 0x000DB7D8
		public override void OnDeserialize(NetworkReader reader, bool initialState)
		{
			if (initialState)
			{
				this.keyFrames.Clear();
				int num = (int)reader.ReadUInt16();
				for (int i = 0; i < num; i++)
				{
					this.keyFrames.Add(WormBodyPositions2.ReadKeyFrame(reader));
				}
			}
		}

		// Token: 0x0600345F RID: 13407 RVA: 0x000026ED File Offset: 0x000008ED
		private void UNetVersion()
		{
		}

		// Token: 0x06003460 RID: 13408 RVA: 0x000DD698 File Offset: 0x000DB898
		protected static void InvokeRpcRpcSendKeyFrame(NetworkBehaviour obj, NetworkReader reader)
		{
			if (!NetworkClient.active)
			{
				Debug.LogError("RPC RpcSendKeyFrame called on server.");
				return;
			}
			((WormBodyPositions2)obj).RpcSendKeyFrame(GeneratedNetworkCode._ReadKeyFrame_WormBodyPositions2(reader));
		}

		// Token: 0x06003461 RID: 13409 RVA: 0x000DD6C1 File Offset: 0x000DB8C1
		protected static void InvokeRpcRpcPlaySurfaceImpactSound(NetworkBehaviour obj, NetworkReader reader)
		{
			if (!NetworkClient.active)
			{
				Debug.LogError("RPC RpcPlaySurfaceImpactSound called on server.");
				return;
			}
			((WormBodyPositions2)obj).RpcPlaySurfaceImpactSound();
		}

		// Token: 0x06003462 RID: 13410 RVA: 0x000DD6E4 File Offset: 0x000DB8E4
		public void CallRpcSendKeyFrame(WormBodyPositions2.KeyFrame newKeyFrame)
		{
			if (!NetworkServer.active)
			{
				Debug.LogError("RPC Function RpcSendKeyFrame called on client.");
				return;
			}
			NetworkWriter networkWriter = new NetworkWriter();
			networkWriter.Write(0);
			networkWriter.Write((short)((ushort)2));
			networkWriter.WritePackedUInt32((uint)WormBodyPositions2.kRpcRpcSendKeyFrame);
			networkWriter.Write(base.GetComponent<NetworkIdentity>().netId);
			GeneratedNetworkCode._WriteKeyFrame_WormBodyPositions2(networkWriter, newKeyFrame);
			this.SendRPCInternal(networkWriter, 0, "RpcSendKeyFrame");
		}

		// Token: 0x06003463 RID: 13411 RVA: 0x000DD758 File Offset: 0x000DB958
		public void CallRpcPlaySurfaceImpactSound()
		{
			if (!NetworkServer.active)
			{
				Debug.LogError("RPC Function RpcPlaySurfaceImpactSound called on client.");
				return;
			}
			NetworkWriter networkWriter = new NetworkWriter();
			networkWriter.Write(0);
			networkWriter.Write((short)((ushort)2));
			networkWriter.WritePackedUInt32((uint)WormBodyPositions2.kRpcRpcPlaySurfaceImpactSound);
			networkWriter.Write(base.GetComponent<NetworkIdentity>().netId);
			this.SendRPCInternal(networkWriter, 0, "RpcPlaySurfaceImpactSound");
		}

		// Token: 0x06003464 RID: 13412 RVA: 0x000DD7C4 File Offset: 0x000DB9C4
		static WormBodyPositions2()
		{
			NetworkBehaviour.RegisterRpcDelegate(typeof(WormBodyPositions2), WormBodyPositions2.kRpcRpcSendKeyFrame, new NetworkBehaviour.CmdDelegate(WormBodyPositions2.InvokeRpcRpcSendKeyFrame));
			WormBodyPositions2.kRpcRpcPlaySurfaceImpactSound = 2010133795;
			NetworkBehaviour.RegisterRpcDelegate(typeof(WormBodyPositions2), WormBodyPositions2.kRpcRpcPlaySurfaceImpactSound, new NetworkBehaviour.CmdDelegate(WormBodyPositions2.InvokeRpcRpcPlaySurfaceImpactSound));
			NetworkCRC.RegisterBehaviour("WormBodyPositions2", 0);
		}

		// Token: 0x06003465 RID: 13413 RVA: 0x000026ED File Offset: 0x000008ED
		public override void PreStartClient()
		{
		}

		// Token: 0x0400352F RID: 13615
		public Transform[] bones;

		// Token: 0x04003530 RID: 13616
		public float[] segmentLengths;

		// Token: 0x04003531 RID: 13617
		[Tooltip("How far behind the chaser the head is, in seconds.")]
		public float followDelay = 2f;

		// Token: 0x04003532 RID: 13618
		[Tooltip("Whether or not the jaw will close/open.")]
		public bool animateJaws;

		// Token: 0x04003533 RID: 13619
		public Animator animator;

		// Token: 0x04003534 RID: 13620
		public string jawMecanimCycleParameter;

		// Token: 0x04003535 RID: 13621
		public float jawMecanimDampTime;

		// Token: 0x04003536 RID: 13622
		public float jawClosedDistance;

		// Token: 0x04003537 RID: 13623
		public float jawOpenDistance;

		// Token: 0x04003538 RID: 13624
		public GameObject warningEffectPrefab;

		// Token: 0x04003539 RID: 13625
		public GameObject burrowEffectPrefab;

		// Token: 0x0400353A RID: 13626
		public float maxPainDisplacementMagnitude = 2f;

		// Token: 0x0400353B RID: 13627
		public float painDisplacementRecoverySpeed = 8f;

		// Token: 0x0400353C RID: 13628
		public bool shouldFireMeatballsOnImpact = true;

		// Token: 0x0400353D RID: 13629
		public bool shouldFireBlastAttackOnImpact = true;

		// Token: 0x0400353E RID: 13630
		public bool enableSurfaceTests = true;

		// Token: 0x0400353F RID: 13631
		public float undergroundTestYOffset;

		// Token: 0x04003540 RID: 13632
		private CharacterBody characterBody;

		// Token: 0x04003541 RID: 13633
		private CharacterDirection characterDirection;

		// Token: 0x04003542 RID: 13634
		private WormBodyPositions2.PositionRotationPair[] boneTransformationBuffer;

		// Token: 0x04003543 RID: 13635
		private Vector3[] boneDisplacements;

		// Token: 0x04003544 RID: 13636
		private float headDistance;

		// Token: 0x04003545 RID: 13637
		private float totalLength;

		// Token: 0x04003546 RID: 13638
		private const float endBonusLength = 4f;

		// Token: 0x04003547 RID: 13639
		private const float fakeEndSegmentLength = 2f;

		// Token: 0x04003548 RID: 13640
		private readonly List<WormBodyPositions2.KeyFrame> keyFrames = new List<WormBodyPositions2.KeyFrame>();

		// Token: 0x04003549 RID: 13641
		private float keyFramesTotalLength;

		// Token: 0x0400354B RID: 13643
		public float spawnDepth = 30f;

		// Token: 0x0400354D RID: 13645
		private Collider entranceCollider;

		// Token: 0x0400354E RID: 13646
		private Collider exitCollider;

		// Token: 0x0400354F RID: 13647
		private Vector3 previousSurfaceTestEnd;

		// Token: 0x04003552 RID: 13650
		private List<WormBodyPositions2.TravelCallback> travelCallbacks;

		// Token: 0x04003553 RID: 13651
		private const float impactSoundPrestartDuration = 0.5f;

		// Token: 0x04003554 RID: 13652
		public float impactCooldownDuration = 0.1f;

		// Token: 0x04003555 RID: 13653
		private float enterTriggerCooldownTimer;

		// Token: 0x04003556 RID: 13654
		private float exitTriggerCooldownTimer;

		// Token: 0x04003557 RID: 13655
		private bool shouldTriggerDeathEffectOnNextImpact;

		// Token: 0x04003558 RID: 13656
		private float deathTime = float.NegativeInfinity;

		// Token: 0x04003559 RID: 13657
		public GameObject meatballProjectile;

		// Token: 0x0400355A RID: 13658
		public GameObject blastAttackEffect;

		// Token: 0x0400355B RID: 13659
		public int meatballCount;

		// Token: 0x0400355C RID: 13660
		public float meatballAngle;

		// Token: 0x0400355D RID: 13661
		public float meatballDamageCoefficient;

		// Token: 0x0400355E RID: 13662
		public float meatballProcCoefficient;

		// Token: 0x0400355F RID: 13663
		public float meatballForce;

		// Token: 0x04003560 RID: 13664
		public float blastAttackDamageCoefficient;

		// Token: 0x04003561 RID: 13665
		public float blastAttackProcCoefficient;

		// Token: 0x04003562 RID: 13666
		public float blastAttackRadius;

		// Token: 0x04003563 RID: 13667
		public float blastAttackForce;

		// Token: 0x04003564 RID: 13668
		public float blastAttackBonusVerticalForce;

		// Token: 0x04003565 RID: 13669
		public float speedMultiplier = 2f;

		// Token: 0x04003566 RID: 13670
		private bool _playingBurrowSound;

		// Token: 0x04003567 RID: 13671
		private static int kRpcRpcSendKeyFrame = 874152969;

		// Token: 0x04003568 RID: 13672
		private static int kRpcRpcPlaySurfaceImpactSound;

		// Token: 0x0200090A RID: 2314
		private struct PositionRotationPair
		{
			// Token: 0x04003569 RID: 13673
			public Vector3 position;

			// Token: 0x0400356A RID: 13674
			public Quaternion rotation;
		}

		// Token: 0x0200090B RID: 2315
		[Serializable]
		public struct KeyFrame
		{
			// Token: 0x0400356B RID: 13675
			public CubicBezier3 curve;

			// Token: 0x0400356C RID: 13676
			public float length;

			// Token: 0x0400356D RID: 13677
			public float time;
		}

		// Token: 0x0200090C RID: 2316
		// (Invoke) Token: 0x06003467 RID: 13415
		public delegate void BurrowExpectedCallback(float expectedTime, Vector3 hitPosition, Vector3 hitNormal);

		// Token: 0x0200090D RID: 2317
		// (Invoke) Token: 0x0600346B RID: 13419
		public delegate void BreachExpectedCallback(float expectedTime, Vector3 hitPosition, Vector3 hitNormal);

		// Token: 0x0200090E RID: 2318
		public struct TravelCallback
		{
			// Token: 0x0400356E RID: 13678
			public float time;

			// Token: 0x0400356F RID: 13679
			public Action callback;
		}
	}
}
