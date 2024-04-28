using System;
using Unity;
using UnityEngine;
using UnityEngine.Networking;

namespace RoR2
{
	// Token: 0x0200084A RID: 2122
	[RequireComponent(typeof(VectorPID))]
	[RequireComponent(typeof(CharacterBody))]
	[RequireComponent(typeof(InputBankTest))]
	public class RigidbodyMotor : NetworkBehaviour, IPhysMotor, IDisplacementReceiver
	{
		// Token: 0x17000428 RID: 1064
		// (get) Token: 0x06002E39 RID: 11833 RVA: 0x000C5195 File Offset: 0x000C3395
		public bool hasEffectiveAuthority
		{
			get
			{
				return Util.HasEffectiveAuthority(this.networkIdentity);
			}
		}

		// Token: 0x17000429 RID: 1065
		// (get) Token: 0x06002E3A RID: 11834 RVA: 0x000C51A2 File Offset: 0x000C33A2
		public float mass
		{
			get
			{
				return this.rigid.mass;
			}
		}

		// Token: 0x06002E3B RID: 11835 RVA: 0x000C51B0 File Offset: 0x000C33B0
		private void Awake()
		{
			this.networkIdentity = base.GetComponent<NetworkIdentity>();
			this.characterBody = base.GetComponent<CharacterBody>();
			this.inputBank = base.GetComponent<InputBankTest>();
			this.modelLocator = base.GetComponent<ModelLocator>();
			this.healthComponent = base.GetComponent<HealthComponent>();
			this.bodyAnimatorSmoothingParameters = base.GetComponent<BodyAnimatorSmoothingParameters>();
		}

		// Token: 0x06002E3C RID: 11836 RVA: 0x000C5208 File Offset: 0x000C3408
		private void Start()
		{
			this.UpdateAuthority();
			Vector3 vector = this.rigid.centerOfMass;
			vector += this.centerOfMassOffset;
			this.rigid.centerOfMass = vector;
			if (this.modelLocator)
			{
				Transform modelTransform = this.modelLocator.modelTransform;
				if (modelTransform)
				{
					this.animator = modelTransform.GetComponent<Animator>();
				}
			}
		}

		// Token: 0x06002E3D RID: 11837 RVA: 0x000C526D File Offset: 0x000C346D
		private void OnDrawGizmosSelected()
		{
			Gizmos.color = Color.red;
			Gizmos.DrawSphere(base.transform.position + this.rigid.centerOfMass, 0.5f);
		}

		// Token: 0x06002E3E RID: 11838 RVA: 0x000C52A0 File Offset: 0x000C34A0
		public static float GetPitch(Vector3 v)
		{
			float x = Mathf.Sqrt(v.x * v.x + v.z * v.z);
			return -Mathf.Atan2(v.y, x);
		}

		// Token: 0x06002E3F RID: 11839 RVA: 0x000C52DC File Offset: 0x000C34DC
		private void Update()
		{
			if (this.animator)
			{
				Vector3 vector = base.transform.InverseTransformVector(this.moveVector) / Mathf.Max(1f, this.moveVector.magnitude);
				BodyAnimatorSmoothingParameters.SmoothingParameters smoothingParameters = this.bodyAnimatorSmoothingParameters ? this.bodyAnimatorSmoothingParameters.smoothingParameters : BodyAnimatorSmoothingParameters.defaultParameters;
				if (this.animatorForward.Length > 0)
				{
					this.animator.SetFloat(this.animatorForward, vector.z, smoothingParameters.forwardSpeedSmoothDamp, Time.deltaTime);
				}
				if (this.animatorRight.Length > 0)
				{
					this.animator.SetFloat(this.animatorRight, vector.x, smoothingParameters.rightSpeedSmoothDamp, Time.deltaTime);
				}
				if (this.animatorUp.Length > 0)
				{
					this.animator.SetFloat(this.animatorUp, vector.y, smoothingParameters.forwardSpeedSmoothDamp, Time.deltaTime);
				}
			}
		}

		// Token: 0x06002E40 RID: 11840 RVA: 0x000C53D8 File Offset: 0x000C35D8
		private void FixedUpdate()
		{
			if (this.inputBank && this.rigid)
			{
				if (this.forcePID)
				{
					if (this.enableOverrideMoveVectorInLocalSpace)
					{
						this.moveVector = base.transform.TransformDirection(this.overrideMoveVectorInLocalSpace) * this.characterBody.moveSpeed;
					}
					Vector3 aimDirection = this.inputBank.aimDirection;
					Vector3 targetVector = this.moveVector;
					this.forcePID.inputVector = this.rigid.velocity;
					this.forcePID.targetVector = targetVector;
					Debug.DrawLine(base.transform.position, base.transform.position + this.forcePID.targetVector, Color.red, 0.1f);
					Vector3 a = this.forcePID.UpdatePID();
					this.rigid.AddForceAtPosition(Vector3.ClampMagnitude(a * (this.characterBody.acceleration / 3f), this.characterBody.acceleration), base.transform.position, ForceMode.Acceleration);
				}
				if (this.rootMotion != Vector3.zero)
				{
					this.rigid.MovePosition(this.rigid.position + this.rootMotion);
					this.rootMotion = Vector3.zero;
				}
			}
		}

		// Token: 0x06002E41 RID: 11841 RVA: 0x000C5538 File Offset: 0x000C3738
		private void OnCollisionEnter(Collision collision)
		{
			if (this.canTakeImpactDamage && collision.gameObject.layer == LayerIndex.world.intVal)
			{
				float num = Mathf.Max(this.characterBody.moveSpeed, this.characterBody.baseMoveSpeed) * 4f;
				float magnitude = collision.relativeVelocity.magnitude;
				if (magnitude >= num)
				{
					float num2 = magnitude / this.characterBody.moveSpeed * 0.07f;
					DamageInfo damageInfo = new DamageInfo();
					damageInfo.damage = Mathf.Min(this.healthComponent.fullCombinedHealth, this.healthComponent.fullCombinedHealth * num2);
					damageInfo.procCoefficient = 0f;
					damageInfo.position = collision.contacts[0].point;
					damageInfo.attacker = this.healthComponent.lastHitAttacker;
					this.healthComponent.TakeDamage(damageInfo);
				}
			}
		}

		// Token: 0x06002E42 RID: 11842 RVA: 0x000C5624 File Offset: 0x000C3824
		public override void OnStartAuthority()
		{
			this.UpdateAuthority();
		}

		// Token: 0x06002E43 RID: 11843 RVA: 0x000C5624 File Offset: 0x000C3824
		public override void OnStopAuthority()
		{
			this.UpdateAuthority();
		}

		// Token: 0x06002E44 RID: 11844 RVA: 0x000C562C File Offset: 0x000C382C
		public void AddDisplacement(Vector3 displacement)
		{
			this.rootMotion += displacement;
		}

		// Token: 0x1700042A RID: 1066
		// (get) Token: 0x06002E45 RID: 11845 RVA: 0x000C51A2 File Offset: 0x000C33A2
		float IPhysMotor.mass
		{
			get
			{
				return this.rigid.mass;
			}
		}

		// Token: 0x1700042B RID: 1067
		// (get) Token: 0x06002E46 RID: 11846 RVA: 0x000C5640 File Offset: 0x000C3840
		Vector3 IPhysMotor.velocity
		{
			get
			{
				return this.rigid.velocity;
			}
		}

		// Token: 0x1700042C RID: 1068
		// (get) Token: 0x06002E47 RID: 11847 RVA: 0x000C5640 File Offset: 0x000C3840
		// (set) Token: 0x06002E48 RID: 11848 RVA: 0x000C564D File Offset: 0x000C384D
		Vector3 IPhysMotor.velocityAuthority
		{
			get
			{
				return this.rigid.velocity;
			}
			set
			{
				this.rigid.velocity = value;
			}
		}

		// Token: 0x06002E49 RID: 11849 RVA: 0x000C565C File Offset: 0x000C385C
		public void ApplyForceImpulse(in PhysForceInfo forceInfo)
		{
			if (NetworkServer.active && !this.hasEffectiveAuthority)
			{
				this.CallRpcApplyForceImpulse(forceInfo);
				return;
			}
			Rigidbody rigidbody = this.rigid;
			Vector3 force = forceInfo.force;
			PhysForceInfo physForceInfo = forceInfo;
			rigidbody.AddForce(force, physForceInfo.massIsOne ? ForceMode.VelocityChange : ForceMode.Impulse);
		}

		// Token: 0x06002E4A RID: 11850 RVA: 0x000C56AA File Offset: 0x000C38AA
		[ClientRpc]
		private void RpcApplyForceImpulse(PhysForceInfo physForceInfo)
		{
			if (!NetworkServer.active)
			{
				this.ApplyForceImpulse(physForceInfo);
			}
		}

		// Token: 0x06002E4B RID: 11851 RVA: 0x000C56BB File Offset: 0x000C38BB
		private void UpdateAuthority()
		{
			base.enabled = this.hasEffectiveAuthority;
		}

		// Token: 0x06002E4D RID: 11853 RVA: 0x000C56D8 File Offset: 0x000C38D8
		void IPhysMotor.ApplyForceImpulse(in PhysForceInfo physForceInfo)
		{
			this.ApplyForceImpulse(physForceInfo);
		}

		// Token: 0x06002E4E RID: 11854 RVA: 0x000026ED File Offset: 0x000008ED
		private void UNetVersion()
		{
		}

		// Token: 0x06002E4F RID: 11855 RVA: 0x000C56E1 File Offset: 0x000C38E1
		protected static void InvokeRpcRpcApplyForceImpulse(NetworkBehaviour obj, NetworkReader reader)
		{
			if (!NetworkClient.active)
			{
				Debug.LogError("RPC RpcApplyForceImpulse called on server.");
				return;
			}
			((RigidbodyMotor)obj).RpcApplyForceImpulse(GeneratedNetworkCode._ReadPhysForceInfo_None(reader));
		}

		// Token: 0x06002E50 RID: 11856 RVA: 0x000C570C File Offset: 0x000C390C
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
			networkWriter.WritePackedUInt32((uint)RigidbodyMotor.kRpcRpcApplyForceImpulse);
			networkWriter.Write(base.GetComponent<NetworkIdentity>().netId);
			GeneratedNetworkCode._WritePhysForceInfo_None(networkWriter, physForceInfo);
			this.SendRPCInternal(networkWriter, 0, "RpcApplyForceImpulse");
		}

		// Token: 0x06002E51 RID: 11857 RVA: 0x000C577F File Offset: 0x000C397F
		static RigidbodyMotor()
		{
			NetworkBehaviour.RegisterRpcDelegate(typeof(RigidbodyMotor), RigidbodyMotor.kRpcRpcApplyForceImpulse, new NetworkBehaviour.CmdDelegate(RigidbodyMotor.InvokeRpcRpcApplyForceImpulse));
			NetworkCRC.RegisterBehaviour("RigidbodyMotor", 0);
		}

		// Token: 0x06002E52 RID: 11858 RVA: 0x000C57BC File Offset: 0x000C39BC
		public override bool OnSerialize(NetworkWriter writer, bool forceAll)
		{
			bool result;
			return result;
		}

		// Token: 0x06002E53 RID: 11859 RVA: 0x000026ED File Offset: 0x000008ED
		public override void OnDeserialize(NetworkReader reader, bool initialState)
		{
		}

		// Token: 0x06002E54 RID: 11860 RVA: 0x000026ED File Offset: 0x000008ED
		public override void PreStartClient()
		{
		}

		// Token: 0x04003047 RID: 12359
		[HideInInspector]
		public Vector3 moveVector;

		// Token: 0x04003048 RID: 12360
		public Rigidbody rigid;

		// Token: 0x04003049 RID: 12361
		public VectorPID forcePID;

		// Token: 0x0400304A RID: 12362
		public Vector3 centerOfMassOffset;

		// Token: 0x0400304B RID: 12363
		public string animatorForward;

		// Token: 0x0400304C RID: 12364
		public string animatorRight;

		// Token: 0x0400304D RID: 12365
		public string animatorUp;

		// Token: 0x0400304E RID: 12366
		public bool enableOverrideMoveVectorInLocalSpace;

		// Token: 0x0400304F RID: 12367
		public bool canTakeImpactDamage = true;

		// Token: 0x04003050 RID: 12368
		public Vector3 overrideMoveVectorInLocalSpace;

		// Token: 0x04003051 RID: 12369
		private NetworkIdentity networkIdentity;

		// Token: 0x04003052 RID: 12370
		private CharacterBody characterBody;

		// Token: 0x04003053 RID: 12371
		private InputBankTest inputBank;

		// Token: 0x04003054 RID: 12372
		private ModelLocator modelLocator;

		// Token: 0x04003055 RID: 12373
		private Animator animator;

		// Token: 0x04003056 RID: 12374
		private BodyAnimatorSmoothingParameters bodyAnimatorSmoothingParameters;

		// Token: 0x04003057 RID: 12375
		private HealthComponent healthComponent;

		// Token: 0x04003058 RID: 12376
		private Vector3 rootMotion;

		// Token: 0x04003059 RID: 12377
		private const float impactDamageStrength = 0.07f;

		// Token: 0x0400305A RID: 12378
		private static int kRpcRpcApplyForceImpulse = 1386350170;
	}
}
