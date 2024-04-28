using System;
using System.Collections.Generic;
using System.Globalization;
using KinematicCharacterController;
using RoR2.ConVar;
using Unity;
using UnityEngine;
using UnityEngine.Networking;

namespace RoR2
{
	// Token: 0x02000646 RID: 1606
	[RequireComponent(typeof(CharacterBody))]
	public class CharacterMotor : BaseCharacterController, IPhysMotor, ILifeBehavior, IDisplacementReceiver, ICharacterGravityParameterProvider, ICharacterFlightParameterProvider
	{
		// Token: 0x1700025C RID: 604
		// (get) Token: 0x06001EE0 RID: 7904 RVA: 0x0008572B File Offset: 0x0008392B
		public float walkSpeed
		{
			get
			{
				return this.body.moveSpeed * this.walkSpeedPenaltyCoefficient;
			}
		}

		// Token: 0x1700025D RID: 605
		// (get) Token: 0x06001EE1 RID: 7905 RVA: 0x0008573F File Offset: 0x0008393F
		public float acceleration
		{
			get
			{
				return this.body.acceleration;
			}
		}

		// Token: 0x1700025E RID: 606
		// (get) Token: 0x06001EE2 RID: 7906 RVA: 0x0008574C File Offset: 0x0008394C
		public bool atRest
		{
			get
			{
				return this.restStopwatch > 1f;
			}
		}

		// Token: 0x1700025F RID: 607
		// (get) Token: 0x06001EE3 RID: 7907 RVA: 0x0008575B File Offset: 0x0008395B
		public bool hasEffectiveAuthority
		{
			get
			{
				return Util.HasEffectiveAuthority(this.networkIdentity);
			}
		}

		// Token: 0x17000260 RID: 608
		// (get) Token: 0x06001EE4 RID: 7908 RVA: 0x00085768 File Offset: 0x00083968
		public Vector3 estimatedGroundNormal
		{
			get
			{
				if (!this.hasEffectiveAuthority)
				{
					return this.netGroundNormal;
				}
				return base.Motor.GroundingStatus.GroundNormal;
			}
		}

		// Token: 0x06001EE5 RID: 7909 RVA: 0x00085789 File Offset: 0x00083989
		private void UpdateInnerMotorEnabled()
		{
			base.Motor.enabled = (CharacterMotor.enableMotorWithoutAuthority || this.hasEffectiveAuthority);
		}

		// Token: 0x06001EE6 RID: 7910 RVA: 0x000857A6 File Offset: 0x000839A6
		private void UpdateAuthority()
		{
			this.UpdateInnerMotorEnabled();
		}

		// Token: 0x06001EE7 RID: 7911 RVA: 0x000857B0 File Offset: 0x000839B0
		private void Awake()
		{
			this.networkIdentity = base.GetComponent<NetworkIdentity>();
			this.body = base.GetComponent<CharacterBody>();
			this.capsuleCollider = base.GetComponent<CapsuleCollider>();
			this.previousPosition = base.transform.position;
			base.Motor.Rigidbody.mass = this.mass;
			base.Motor.MaxStableSlopeAngle = 70f;
			base.Motor.MaxStableDenivelationAngle = 55f;
			base.Motor.RebuildCollidableLayers();
			if (this.generateParametersOnAwake)
			{
				this.GenerateParameters();
			}
			this.useGravity = this.gravityParameters.CheckShouldUseGravity();
			this.isFlying = this.flightParameters.CheckShouldUseFlight();
		}

		// Token: 0x06001EE8 RID: 7912 RVA: 0x00085869 File Offset: 0x00083A69
		private void Start()
		{
			this.UpdateAuthority();
		}

		// Token: 0x06001EE9 RID: 7913 RVA: 0x00085869 File Offset: 0x00083A69
		public override void OnStartAuthority()
		{
			this.UpdateAuthority();
		}

		// Token: 0x06001EEA RID: 7914 RVA: 0x00085869 File Offset: 0x00083A69
		public override void OnStopAuthority()
		{
			this.UpdateAuthority();
		}

		// Token: 0x06001EEB RID: 7915 RVA: 0x00085871 File Offset: 0x00083A71
		private void OnEnable()
		{
			CharacterMotor.instancesList.Add(this);
			this.UpdateInnerMotorEnabled();
		}

		// Token: 0x06001EEC RID: 7916 RVA: 0x00085884 File Offset: 0x00083A84
		private void OnDisable()
		{
			base.Motor.enabled = false;
			CharacterMotor.instancesList.Remove(this);
		}

		// Token: 0x06001EED RID: 7917 RVA: 0x000858A0 File Offset: 0x00083AA0
		private void PreMove(float deltaTime)
		{
			if (this.hasEffectiveAuthority)
			{
				float num = this.acceleration;
				if (this.isAirControlForced || !this.isGrounded)
				{
					num *= (this.disableAirControlUntilCollision ? 0f : this.airControl);
				}
				Vector3 a = this.moveDirection;
				if (!this.isFlying)
				{
					a.y = 0f;
				}
				if (this.body.isSprinting)
				{
					float magnitude = a.magnitude;
					if (magnitude < 1f && magnitude > 0f)
					{
						float d = 1f / a.magnitude;
						a *= d;
					}
				}
				Vector3 target = a * this.walkSpeed;
				if (!this.isFlying)
				{
					target.y = this.velocity.y;
				}
				this.velocity = Vector3.MoveTowards(this.velocity, target, num * deltaTime);
				if (this.useGravity)
				{
					ref float ptr = ref this.velocity.y;
					ptr += Physics.gravity.y * deltaTime;
					if (this.isGrounded)
					{
						ptr = Mathf.Max(ptr, 0f);
					}
				}
			}
		}

		// Token: 0x06001EEE RID: 7918 RVA: 0x000859BB File Offset: 0x00083BBB
		public void OnDeathStart()
		{
			this.alive = false;
		}

		// Token: 0x06001EEF RID: 7919 RVA: 0x000859C4 File Offset: 0x00083BC4
		private void FixedUpdate()
		{
			float fixedDeltaTime = Time.fixedDeltaTime;
			if (fixedDeltaTime == 0f)
			{
				return;
			}
			Vector3 position = base.transform.position;
			if ((this.previousPosition - position).sqrMagnitude < 0.00062500004f * fixedDeltaTime)
			{
				this.restStopwatch += fixedDeltaTime;
			}
			else
			{
				this.restStopwatch = 0f;
			}
			this.previousPosition = position;
			if (this.netIsGrounded)
			{
				this.lastGroundedTime = Run.FixedTimeStamp.now;
			}
		}

		// Token: 0x06001EF0 RID: 7920 RVA: 0x00085A3E File Offset: 0x00083C3E
		private void GenerateParameters()
		{
			this.slopeLimit = 70f;
			this.stepOffset = Mathf.Min(this.capsuleHeight * 0.1f, 0.2f);
			this.stepHandlingMethod = StepHandlingMethod.None;
			this.ledgeHandling = false;
			this.interactiveRigidbodyHandling = true;
		}

		// Token: 0x17000261 RID: 609
		// (get) Token: 0x06001EF1 RID: 7921 RVA: 0x00085A7C File Offset: 0x00083C7C
		private bool canWalk
		{
			get
			{
				return !this.muteWalkMotion && this.alive;
			}
		}

		// Token: 0x17000262 RID: 610
		// (get) Token: 0x06001EF2 RID: 7922 RVA: 0x00085A8E File Offset: 0x00083C8E
		public bool isGrounded
		{
			get
			{
				if (!this.hasEffectiveAuthority)
				{
					return this.netIsGrounded;
				}
				return base.Motor.GroundingStatus.IsStableOnGround;
			}
		}

		// Token: 0x06001EF3 RID: 7923 RVA: 0x00085AB0 File Offset: 0x00083CB0
		public void ApplyForce(Vector3 force, bool alwaysApply = false, bool disableAirControlUntilCollision = false)
		{
			PhysForceInfo physForceInfo = PhysForceInfo.Create();
			physForceInfo.force = force;
			physForceInfo.ignoreGroundStick = alwaysApply;
			physForceInfo.disableAirControlUntilCollision = disableAirControlUntilCollision;
			physForceInfo.massIsOne = false;
			this.ApplyForceImpulse(physForceInfo);
		}

		// Token: 0x17000263 RID: 611
		// (get) Token: 0x06001EF4 RID: 7924 RVA: 0x00085AEB File Offset: 0x00083CEB
		float IPhysMotor.mass
		{
			get
			{
				return this.mass;
			}
		}

		// Token: 0x17000264 RID: 612
		// (get) Token: 0x06001EF5 RID: 7925 RVA: 0x00085AF3 File Offset: 0x00083CF3
		Vector3 IPhysMotor.velocity
		{
			get
			{
				return this.velocity;
			}
		}

		// Token: 0x17000265 RID: 613
		// (get) Token: 0x06001EF6 RID: 7926 RVA: 0x00085AF3 File Offset: 0x00083CF3
		// (set) Token: 0x06001EF7 RID: 7927 RVA: 0x00085AFB File Offset: 0x00083CFB
		Vector3 IPhysMotor.velocityAuthority
		{
			get
			{
				return this.velocity;
			}
			set
			{
				this.velocity = value;
			}
		}

		// Token: 0x06001EF8 RID: 7928 RVA: 0x00085B04 File Offset: 0x00083D04
		public void ApplyForceImpulse(in PhysForceInfo forceInfo)
		{
			if (NetworkServer.active && !this.hasEffectiveAuthority)
			{
				this.CallRpcApplyForceImpulse(forceInfo);
				return;
			}
			Vector3 vector = forceInfo.force;
			PhysForceInfo physForceInfo = forceInfo;
			if (!physForceInfo.massIsOne)
			{
				float magnitude = vector.magnitude;
				vector *= 1f / this.mass;
			}
			else
			{
				Debug.Log(string.Format("addedVelocity.magnitude = {0}", vector.magnitude));
			}
			if (this.mass == 0f)
			{
				return;
			}
			if (vector.y < 6f && this.isGrounded)
			{
				physForceInfo = forceInfo;
				if (!physForceInfo.ignoreGroundStick)
				{
					vector.y = 0f;
				}
			}
			if (vector.y > 0f)
			{
				base.Motor.ForceUnground();
			}
			this.velocity += vector;
			physForceInfo = forceInfo;
			if (physForceInfo.disableAirControlUntilCollision)
			{
				this.disableAirControlUntilCollision = true;
			}
		}

		// Token: 0x06001EF9 RID: 7929 RVA: 0x00085BFC File Offset: 0x00083DFC
		[ClientRpc]
		private void RpcApplyForceImpulse(PhysForceInfo physForceInfo)
		{
			if (!NetworkServer.active)
			{
				this.ApplyForceImpulse(physForceInfo);
			}
		}

		// Token: 0x17000266 RID: 614
		// (get) Token: 0x06001EFA RID: 7930 RVA: 0x00085C0D File Offset: 0x00083E0D
		// (set) Token: 0x06001EFB RID: 7931 RVA: 0x00085C15 File Offset: 0x00083E15
		public Vector3 moveDirection
		{
			get
			{
				return this._moveDirection;
			}
			set
			{
				this._moveDirection = value;
			}
		}

		// Token: 0x17000267 RID: 615
		// (get) Token: 0x06001EFC RID: 7932 RVA: 0x00085C1E File Offset: 0x00083E1E
		// (set) Token: 0x06001EFD RID: 7933 RVA: 0x00085C2B File Offset: 0x00083E2B
		private float slopeLimit
		{
			get
			{
				return base.Motor.MaxStableSlopeAngle;
			}
			set
			{
				base.Motor.MaxStableSlopeAngle = value;
			}
		}

		// Token: 0x17000268 RID: 616
		// (get) Token: 0x06001EFE RID: 7934 RVA: 0x00085C39 File Offset: 0x00083E39
		// (set) Token: 0x06001EFF RID: 7935 RVA: 0x00085C46 File Offset: 0x00083E46
		public float stepOffset
		{
			get
			{
				return base.Motor.MaxStepHeight;
			}
			set
			{
				base.Motor.MaxStepHeight = value;
			}
		}

		// Token: 0x17000269 RID: 617
		// (get) Token: 0x06001F00 RID: 7936 RVA: 0x00085C54 File Offset: 0x00083E54
		public float capsuleHeight
		{
			get
			{
				return this.capsuleCollider.height;
			}
		}

		// Token: 0x1700026A RID: 618
		// (get) Token: 0x06001F01 RID: 7937 RVA: 0x00085C61 File Offset: 0x00083E61
		public float capsuleRadius
		{
			get
			{
				return this.capsuleCollider.radius;
			}
		}

		// Token: 0x1700026B RID: 619
		// (get) Token: 0x06001F02 RID: 7938 RVA: 0x00085C70 File Offset: 0x00083E70
		// (set) Token: 0x06001F03 RID: 7939 RVA: 0x00085C8C File Offset: 0x00083E8C
		public StepHandlingMethod stepHandlingMethod
		{
			get
			{
				return base.Motor.StepHandling = StepHandlingMethod.None;
			}
			set
			{
				base.Motor.StepHandling = value;
			}
		}

		// Token: 0x1700026C RID: 620
		// (get) Token: 0x06001F04 RID: 7940 RVA: 0x00085C9A File Offset: 0x00083E9A
		// (set) Token: 0x06001F05 RID: 7941 RVA: 0x00085CA7 File Offset: 0x00083EA7
		public bool ledgeHandling
		{
			get
			{
				return base.Motor.LedgeHandling;
			}
			set
			{
				base.Motor.LedgeHandling = value;
			}
		}

		// Token: 0x1700026D RID: 621
		// (get) Token: 0x06001F06 RID: 7942 RVA: 0x00085CB5 File Offset: 0x00083EB5
		// (set) Token: 0x06001F07 RID: 7943 RVA: 0x00085CC2 File Offset: 0x00083EC2
		public bool interactiveRigidbodyHandling
		{
			get
			{
				return base.Motor.InteractiveRigidbodyHandling;
			}
			set
			{
				base.Motor.InteractiveRigidbodyHandling = value;
			}
		}

		// Token: 0x1700026E RID: 622
		// (get) Token: 0x06001F08 RID: 7944 RVA: 0x00085CD0 File Offset: 0x00083ED0
		// (set) Token: 0x06001F09 RID: 7945 RVA: 0x00085CD8 File Offset: 0x00083ED8
		public Run.FixedTimeStamp lastGroundedTime { get; private set; } = Run.FixedTimeStamp.negativeInfinity;

		// Token: 0x06001F0A RID: 7946 RVA: 0x00085CE1 File Offset: 0x00083EE1
		public override void UpdateRotation(ref Quaternion currentRotation, float deltaTime)
		{
			currentRotation = Quaternion.identity;
		}

		// Token: 0x06001F0B RID: 7947 RVA: 0x00085CEE File Offset: 0x00083EEE
		public override void UpdateVelocity(ref Vector3 currentVelocity, float deltaTime)
		{
			currentVelocity = this.velocity;
		}

		// Token: 0x06001F0C RID: 7948 RVA: 0x00085CFC File Offset: 0x00083EFC
		public override void BeforeCharacterUpdate(float deltaTime)
		{
			float num = CharacterMotor.cvCMotorSafeCollisionStepThreshold.value * CharacterMotor.cvCMotorSafeCollisionStepThreshold.value;
			if (this.rootMotion != Vector3.zero)
			{
				Vector3 b = this.rootMotion;
				this.rootMotion = Vector3.zero;
				base.Motor.SafeMovement = (b.sqrMagnitude >= num);
				base.Motor.MoveCharacter(base.transform.position + b);
			}
			this.PreMove(deltaTime);
			float sqrMagnitude = (this.velocity * Time.fixedDeltaTime).sqrMagnitude;
			base.Motor.SafeMovement = (sqrMagnitude >= num);
		}

		// Token: 0x06001F0D RID: 7949 RVA: 0x00085DAC File Offset: 0x00083FAC
		public override void PostGroundingUpdate(float deltaTime)
		{
			if (base.Motor.GroundingStatus.IsStableOnGround != base.Motor.LastGroundingStatus.IsStableOnGround)
			{
				this.netIsGrounded = base.Motor.GroundingStatus.IsStableOnGround;
				if (base.Motor.GroundingStatus.IsStableOnGround)
				{
					this.OnLanded();
					return;
				}
				this.OnLeaveStableGround();
			}
		}

		// Token: 0x06001F0E RID: 7950 RVA: 0x00085E10 File Offset: 0x00084010
		private void OnLanded()
		{
			this.jumpCount = 0;
			CharacterMotor.HitGroundInfo hitGroundInfo = new CharacterMotor.HitGroundInfo
			{
				velocity = this.lastVelocity,
				position = base.Motor.GroundingStatus.GroundPoint
			};
			if (this.hasEffectiveAuthority)
			{
				try
				{
					CharacterMotor.HitGroundDelegate hitGroundDelegate = this.onHitGroundAuthority;
					if (hitGroundDelegate != null)
					{
						hitGroundDelegate(ref hitGroundInfo);
					}
				}
				catch (Exception message)
				{
					Debug.LogError(message);
				}
				if (NetworkServer.active)
				{
					this.OnHitGroundServer(hitGroundInfo);
					return;
				}
				this.CallCmdReportHitGround(hitGroundInfo);
			}
		}

		// Token: 0x06001F0F RID: 7951 RVA: 0x00085E9C File Offset: 0x0008409C
		[Server]
		private void OnHitGroundServer(CharacterMotor.HitGroundInfo hitGroundInfo)
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Void RoR2.CharacterMotor::OnHitGroundServer(RoR2.CharacterMotor/HitGroundInfo)' called on client");
				return;
			}
			GlobalEventManager.instance.OnCharacterHitGroundServer(this.body, hitGroundInfo.velocity);
			try
			{
				CharacterMotor.HitGroundDelegate hitGroundDelegate = this.onHitGroundServer;
				if (hitGroundDelegate != null)
				{
					hitGroundDelegate(ref hitGroundInfo);
				}
			}
			catch (Exception message)
			{
				Debug.LogError(message);
			}
		}

		// Token: 0x06001F10 RID: 7952 RVA: 0x00085F00 File Offset: 0x00084100
		[Command]
		private void CmdReportHitGround(CharacterMotor.HitGroundInfo hitGroundInfo)
		{
			this.OnHitGroundServer(hitGroundInfo);
		}

		// Token: 0x06001F11 RID: 7953 RVA: 0x00085F09 File Offset: 0x00084109
		private void OnLeaveStableGround()
		{
			if (this.jumpCount < 1)
			{
				this.jumpCount = 1;
			}
		}

		// Token: 0x06001F12 RID: 7954 RVA: 0x00085F1B File Offset: 0x0008411B
		public override void AfterCharacterUpdate(float deltaTime)
		{
			this.lastVelocity = this.velocity;
			this.velocity = base.Motor.BaseVelocity;
		}

		// Token: 0x06001F13 RID: 7955 RVA: 0x00085F3A File Offset: 0x0008413A
		public override bool IsColliderValidForCollisions(Collider coll)
		{
			return !coll.isTrigger && coll != base.Motor.Capsule;
		}

		// Token: 0x06001F14 RID: 7956 RVA: 0x00085F58 File Offset: 0x00084158
		public override void OnGroundHit(Collider hitCollider, Vector3 hitNormal, Vector3 hitPoint, ref HitStabilityReport hitStabilityReport)
		{
			this.isAirControlForced = false;
			SurfaceDef objectSurfaceDef = SurfaceDefProvider.GetObjectSurfaceDef(hitCollider, hitPoint);
			if (objectSurfaceDef)
			{
				this.isAirControlForced = objectSurfaceDef.isSlippery;
			}
		}

		// Token: 0x14000035 RID: 53
		// (add) Token: 0x06001F15 RID: 7957 RVA: 0x00085F88 File Offset: 0x00084188
		// (remove) Token: 0x06001F16 RID: 7958 RVA: 0x00085F91 File Offset: 0x00084191
		[Obsolete("Use '.onHitGroundServer' instead, which this is just a backwards-compatibility wrapper for. Or, use '.onHitGroundAuthority' if that is more appropriate to your use case.", false)]
		public event CharacterMotor.HitGroundDelegate onHitGround
		{
			add
			{
				this.onHitGroundServer += value;
			}
			remove
			{
				this.onHitGroundServer -= value;
			}
		}

		// Token: 0x14000036 RID: 54
		// (add) Token: 0x06001F17 RID: 7959 RVA: 0x00085F9C File Offset: 0x0008419C
		// (remove) Token: 0x06001F18 RID: 7960 RVA: 0x00085FD4 File Offset: 0x000841D4
		public event CharacterMotor.HitGroundDelegate onHitGroundServer;

		// Token: 0x14000037 RID: 55
		// (add) Token: 0x06001F19 RID: 7961 RVA: 0x0008600C File Offset: 0x0008420C
		// (remove) Token: 0x06001F1A RID: 7962 RVA: 0x00086044 File Offset: 0x00084244
		public event CharacterMotor.HitGroundDelegate onHitGroundAuthority;

		// Token: 0x06001F1B RID: 7963 RVA: 0x0008607C File Offset: 0x0008427C
		public override void OnMovementHit(Collider hitCollider, Vector3 hitNormal, Vector3 hitPoint, ref HitStabilityReport hitStabilityReport)
		{
			this.disableAirControlUntilCollision = false;
			if (this.onMovementHit != null)
			{
				CharacterMotor.MovementHitInfo movementHitInfo = new CharacterMotor.MovementHitInfo
				{
					velocity = this.velocity,
					hitCollider = hitCollider
				};
				this.onMovementHit(ref movementHitInfo);
			}
		}

		// Token: 0x14000038 RID: 56
		// (add) Token: 0x06001F1C RID: 7964 RVA: 0x000860C4 File Offset: 0x000842C4
		// (remove) Token: 0x06001F1D RID: 7965 RVA: 0x000860FC File Offset: 0x000842FC
		public event CharacterMotor.MovementHitDelegate onMovementHit;

		// Token: 0x06001F1E RID: 7966 RVA: 0x000026ED File Offset: 0x000008ED
		public override void ProcessHitStabilityReport(Collider hitCollider, Vector3 hitNormal, Vector3 hitPoint, Vector3 atCharacterPosition, Quaternion atCharacterRotation, ref HitStabilityReport hitStabilityReport)
		{
		}

		// Token: 0x06001F1F RID: 7967 RVA: 0x00086134 File Offset: 0x00084334
		public void Jump(float horizontalMultiplier, float verticalMultiplier, bool vault = false)
		{
			Vector3 a = this.moveDirection;
			if (vault)
			{
				this.velocity = a;
			}
			else
			{
				a.y = 0f;
				float magnitude = a.magnitude;
				if (magnitude > 0f)
				{
					a /= magnitude;
				}
				Vector3 vector = a * this.body.moveSpeed * horizontalMultiplier;
				vector.y = this.body.jumpPower * verticalMultiplier;
				this.velocity = vector;
			}
			base.Motor.ForceUnground();
		}

		// Token: 0x06001F20 RID: 7968 RVA: 0x000861B6 File Offset: 0x000843B6
		public void AddDisplacement(Vector3 displacement)
		{
			this.rootMotion += displacement;
		}

		// Token: 0x1700026F RID: 623
		// (get) Token: 0x06001F21 RID: 7969 RVA: 0x000861CA File Offset: 0x000843CA
		// (set) Token: 0x06001F22 RID: 7970 RVA: 0x000861D2 File Offset: 0x000843D2
		public CharacterGravityParameters gravityParameters
		{
			get
			{
				return this._gravityParameters;
			}
			set
			{
				if (this._gravityParameters.Equals(value))
				{
					return;
				}
				this._gravityParameters = value;
				this.useGravity = this._gravityParameters.CheckShouldUseGravity();
			}
		}

		// Token: 0x17000270 RID: 624
		// (get) Token: 0x06001F23 RID: 7971 RVA: 0x000861FB File Offset: 0x000843FB
		// (set) Token: 0x06001F24 RID: 7972 RVA: 0x00086203 File Offset: 0x00084403
		public bool useGravity { get; private set; }

		// Token: 0x17000271 RID: 625
		// (get) Token: 0x06001F25 RID: 7973 RVA: 0x0008620C File Offset: 0x0008440C
		// (set) Token: 0x06001F26 RID: 7974 RVA: 0x00086214 File Offset: 0x00084414
		public CharacterFlightParameters flightParameters
		{
			get
			{
				return this._flightParameters;
			}
			set
			{
				if (this._flightParameters.Equals(value))
				{
					return;
				}
				this._flightParameters = value;
				this.isFlying = this._flightParameters.CheckShouldUseFlight();
			}
		}

		// Token: 0x17000272 RID: 626
		// (get) Token: 0x06001F27 RID: 7975 RVA: 0x0008623D File Offset: 0x0008443D
		// (set) Token: 0x06001F28 RID: 7976 RVA: 0x00086245 File Offset: 0x00084445
		public bool isFlying { get; private set; }

		// Token: 0x06001F2A RID: 7978 RVA: 0x000862A0 File Offset: 0x000844A0
		static CharacterMotor()
		{
			NetworkBehaviour.RegisterCommandDelegate(typeof(CharacterMotor), CharacterMotor.kCmdCmdReportHitGround, new NetworkBehaviour.CmdDelegate(CharacterMotor.InvokeCmdCmdReportHitGround));
			CharacterMotor.kRpcRpcApplyForceImpulse = 1042934326;
			NetworkBehaviour.RegisterRpcDelegate(typeof(CharacterMotor), CharacterMotor.kRpcRpcApplyForceImpulse, new NetworkBehaviour.CmdDelegate(CharacterMotor.InvokeRpcRpcApplyForceImpulse));
			NetworkCRC.RegisterBehaviour("CharacterMotor", 0);
		}

		// Token: 0x06001F2B RID: 7979 RVA: 0x00086347 File Offset: 0x00084547
		void IPhysMotor.ApplyForceImpulse(in PhysForceInfo physForceInfo)
		{
			this.ApplyForceImpulse(physForceInfo);
		}

		// Token: 0x06001F2C RID: 7980 RVA: 0x000026ED File Offset: 0x000008ED
		private void UNetVersion()
		{
		}

		// Token: 0x06001F2D RID: 7981 RVA: 0x00086350 File Offset: 0x00084550
		protected static void InvokeCmdCmdReportHitGround(NetworkBehaviour obj, NetworkReader reader)
		{
			if (!NetworkServer.active)
			{
				Debug.LogError("Command CmdReportHitGround called on client.");
				return;
			}
			((CharacterMotor)obj).CmdReportHitGround(GeneratedNetworkCode._ReadHitGroundInfo_CharacterMotor(reader));
		}

		// Token: 0x06001F2E RID: 7982 RVA: 0x0008637C File Offset: 0x0008457C
		public void CallCmdReportHitGround(CharacterMotor.HitGroundInfo hitGroundInfo)
		{
			if (!NetworkClient.active)
			{
				Debug.LogError("Command function CmdReportHitGround called on server.");
				return;
			}
			if (base.isServer)
			{
				this.CmdReportHitGround(hitGroundInfo);
				return;
			}
			NetworkWriter networkWriter = new NetworkWriter();
			networkWriter.Write(0);
			networkWriter.Write((short)((ushort)5));
			networkWriter.WritePackedUInt32((uint)CharacterMotor.kCmdCmdReportHitGround);
			networkWriter.Write(base.GetComponent<NetworkIdentity>().netId);
			GeneratedNetworkCode._WriteHitGroundInfo_CharacterMotor(networkWriter, hitGroundInfo);
			base.SendCommandInternal(networkWriter, 0, "CmdReportHitGround");
		}

		// Token: 0x06001F2F RID: 7983 RVA: 0x00086406 File Offset: 0x00084606
		protected static void InvokeRpcRpcApplyForceImpulse(NetworkBehaviour obj, NetworkReader reader)
		{
			if (!NetworkClient.active)
			{
				Debug.LogError("RPC RpcApplyForceImpulse called on server.");
				return;
			}
			((CharacterMotor)obj).RpcApplyForceImpulse(GeneratedNetworkCode._ReadPhysForceInfo_None(reader));
		}

		// Token: 0x06001F30 RID: 7984 RVA: 0x00086430 File Offset: 0x00084630
		public void CallRpcApplyForceImpulse(PhysForceInfo physForceInfo)
		{
			if (!NetworkServer.active)
			{
				Debug.LogError("RPC Function RpcApplyForceImpulse called on client.");
				return;
			}
			NetworkWriter networkWriter = new NetworkWriter();
			networkWriter.Write(0);
			networkWriter.Write((short)((ushort)2));
			networkWriter.WritePackedUInt32((uint)CharacterMotor.kRpcRpcApplyForceImpulse);
			networkWriter.Write(base.GetComponent<NetworkIdentity>().netId);
			GeneratedNetworkCode._WritePhysForceInfo_None(networkWriter, physForceInfo);
			this.SendRPCInternal(networkWriter, 0, "RpcApplyForceImpulse");
		}

		// Token: 0x06001F31 RID: 7985 RVA: 0x000864A4 File Offset: 0x000846A4
		public override bool OnSerialize(NetworkWriter writer, bool forceAll)
		{
			bool flag = base.OnSerialize(writer, forceAll);
			bool flag2;
			return flag2 || flag;
		}

		// Token: 0x06001F32 RID: 7986 RVA: 0x000864BD File Offset: 0x000846BD
		public override void OnDeserialize(NetworkReader reader, bool initialState)
		{
			base.OnDeserialize(reader, initialState);
		}

		// Token: 0x06001F33 RID: 7987 RVA: 0x000864C7 File Offset: 0x000846C7
		public override void PreStartClient()
		{
			base.PreStartClient();
		}

		// Token: 0x0400249C RID: 9372
		public static readonly List<CharacterMotor> instancesList = new List<CharacterMotor>();

		// Token: 0x0400249D RID: 9373
		[HideInInspector]
		public float walkSpeedPenaltyCoefficient = 1f;

		// Token: 0x0400249E RID: 9374
		[Tooltip("The character direction component to supply a move vector to.")]
		public CharacterDirection characterDirection;

		// Token: 0x0400249F RID: 9375
		[Tooltip("Whether or not a move vector supplied to this component can cause movement. Use this when the object is driven by root motion.")]
		public bool muteWalkMotion;

		// Token: 0x040024A0 RID: 9376
		[Tooltip("The mass of this character.")]
		public float mass = 1f;

		// Token: 0x040024A1 RID: 9377
		[Tooltip("The air control value of this character as a fraction of ground control.")]
		public float airControl = 0.25f;

		// Token: 0x040024A2 RID: 9378
		[Tooltip("Disables Air Control for things like jumppads")]
		public bool disableAirControlUntilCollision;

		// Token: 0x040024A3 RID: 9379
		[Tooltip("Auto-assigns parameters skin width, slope angle, and step offset as a function of the Character Motor's radius and height")]
		public bool generateParametersOnAwake = true;

		// Token: 0x040024A4 RID: 9380
		private NetworkIdentity networkIdentity;

		// Token: 0x040024A5 RID: 9381
		private CharacterBody body;

		// Token: 0x040024A6 RID: 9382
		private CapsuleCollider capsuleCollider;

		// Token: 0x040024A7 RID: 9383
		private static readonly bool enableMotorWithoutAuthority = false;

		// Token: 0x040024A8 RID: 9384
		private bool alive = true;

		// Token: 0x040024A9 RID: 9385
		private const float restDuration = 1f;

		// Token: 0x040024AA RID: 9386
		private const float restVelocityThreshold = 0.025f;

		// Token: 0x040024AB RID: 9387
		private const float restVelocityThresholdSqr = 0.00062500004f;

		// Token: 0x040024AC RID: 9388
		public const float slipStartAngle = 70f;

		// Token: 0x040024AD RID: 9389
		public const float slipEndAngle = 55f;

		// Token: 0x040024AE RID: 9390
		private float restStopwatch;

		// Token: 0x040024AF RID: 9391
		private Vector3 previousPosition;

		// Token: 0x040024B0 RID: 9392
		private bool isAirControlForced;

		// Token: 0x040024B1 RID: 9393
		[NonSerialized]
		public int jumpCount;

		// Token: 0x040024B2 RID: 9394
		[NonSerialized]
		public bool netIsGrounded;

		// Token: 0x040024B3 RID: 9395
		[NonSerialized]
		public Vector3 netGroundNormal;

		// Token: 0x040024B4 RID: 9396
		[NonSerialized]
		public Vector3 velocity;

		// Token: 0x040024B5 RID: 9397
		private Vector3 lastVelocity;

		// Token: 0x040024B6 RID: 9398
		[NonSerialized]
		public Vector3 rootMotion;

		// Token: 0x040024B7 RID: 9399
		private Vector3 _moveDirection;

		// Token: 0x040024B9 RID: 9401
		private static readonly FloatConVar cvCMotorSafeCollisionStepThreshold = new FloatConVar("cmotor_safe_collision_step_threshold", ConVarFlags.Cheat, 1.0833334f.ToString(CultureInfo.InvariantCulture), "How large of a movement in meters/fixedTimeStep is needed to trigger more expensive \"safe\" collisions to prevent tunneling.");

		// Token: 0x040024BA RID: 9402
		private int _safeCollisionEnableCount;

		// Token: 0x040024BE RID: 9406
		[SerializeField]
		[Tooltip("Determins how gravity affects this character.")]
		private CharacterGravityParameters _gravityParameters;

		// Token: 0x040024C0 RID: 9408
		[SerializeField]
		[Tooltip("Determines whether this character has three-dimensional or two-dimensional movement capabilities.")]
		private CharacterFlightParameters _flightParameters;

		// Token: 0x040024C2 RID: 9410
		private static int kRpcRpcApplyForceImpulse;

		// Token: 0x040024C3 RID: 9411
		private static int kCmdCmdReportHitGround = 1796547162;

		// Token: 0x02000647 RID: 1607
		[Serializable]
		public struct HitGroundInfo
		{
			// Token: 0x06001F34 RID: 7988 RVA: 0x000864CF File Offset: 0x000846CF
			public override string ToString()
			{
				return string.Format("velocity={0} position={1}", this.velocity, this.position);
			}

			// Token: 0x040024C4 RID: 9412
			public Vector3 velocity;

			// Token: 0x040024C5 RID: 9413
			public Vector3 position;
		}

		// Token: 0x02000648 RID: 1608
		// (Invoke) Token: 0x06001F36 RID: 7990
		public delegate void HitGroundDelegate(ref CharacterMotor.HitGroundInfo hitGroundInfo);

		// Token: 0x02000649 RID: 1609
		public struct MovementHitInfo
		{
			// Token: 0x040024C6 RID: 9414
			public Vector3 velocity;

			// Token: 0x040024C7 RID: 9415
			public Collider hitCollider;
		}

		// Token: 0x0200064A RID: 1610
		// (Invoke) Token: 0x06001F3A RID: 7994
		public delegate void MovementHitDelegate(ref CharacterMotor.MovementHitInfo movementHitInfo);
	}
}
