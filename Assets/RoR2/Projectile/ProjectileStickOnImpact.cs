using System;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;

namespace RoR2.Projectile
{
	// Token: 0x02000BB6 RID: 2998
	[RequireComponent(typeof(Rigidbody))]
	[RequireComponent(typeof(ProjectileController))]
	[RequireComponent(typeof(ProjectileNetworkTransform))]
	public class ProjectileStickOnImpact : NetworkBehaviour, IProjectileImpactBehavior
	{
		// Token: 0x17000642 RID: 1602
		// (get) Token: 0x06004444 RID: 17476 RVA: 0x0011C581 File Offset: 0x0011A781
		// (set) Token: 0x06004445 RID: 17477 RVA: 0x0011C589 File Offset: 0x0011A789
		public Transform stuckTransform { get; private set; }

		// Token: 0x17000643 RID: 1603
		// (get) Token: 0x06004446 RID: 17478 RVA: 0x0011C592 File Offset: 0x0011A792
		// (set) Token: 0x06004447 RID: 17479 RVA: 0x0011C59A File Offset: 0x0011A79A
		public CharacterBody stuckBody { get; private set; }

		// Token: 0x06004448 RID: 17480 RVA: 0x0011C5A3 File Offset: 0x0011A7A3
		private void Awake()
		{
			this.projectileController = base.GetComponent<ProjectileController>();
			this.rigidbody = base.GetComponent<Rigidbody>();
		}

		// Token: 0x06004449 RID: 17481 RVA: 0x0011C5BD File Offset: 0x0011A7BD
		public void FixedUpdate()
		{
			this.UpdateSticking();
		}

		// Token: 0x0600444A RID: 17482 RVA: 0x0011C5C5 File Offset: 0x0011A7C5
		private void OnEnable()
		{
			if (this.wasEverEnabled)
			{
				Collider component = base.GetComponent<Collider>();
				component.enabled = false;
				component.enabled = true;
			}
			this.wasEverEnabled = true;
		}

		// Token: 0x0600444B RID: 17483 RVA: 0x0011C5E9 File Offset: 0x0011A7E9
		private void OnDisable()
		{
			if (NetworkServer.active)
			{
				this.Detach();
			}
		}

		// Token: 0x17000644 RID: 1604
		// (get) Token: 0x0600444C RID: 17484 RVA: 0x0011C5F8 File Offset: 0x0011A7F8
		// (set) Token: 0x0600444D RID: 17485 RVA: 0x0011C600 File Offset: 0x0011A800
		public GameObject victim
		{
			get
			{
				return this._victim;
			}
			private set
			{
				this._victim = value;
				this.NetworksyncVictim = value;
			}
		}

		// Token: 0x17000645 RID: 1605
		// (get) Token: 0x0600444E RID: 17486 RVA: 0x0011C610 File Offset: 0x0011A810
		public bool stuck
		{
			get
			{
				return this.hitHurtboxIndex != -1;
			}
		}

		// Token: 0x0600444F RID: 17487 RVA: 0x0011C61E File Offset: 0x0011A81E
		[Server]
		public void Detach()
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Void RoR2.Projectile.ProjectileStickOnImpact::Detach()' called on client");
				return;
			}
			this.victim = null;
			this.stuckTransform = null;
			this.NetworkhitHurtboxIndex = -1;
			this.UpdateSticking();
		}

		// Token: 0x06004450 RID: 17488 RVA: 0x0011C650 File Offset: 0x0011A850
		public void OnProjectileImpact(ProjectileImpactInfo impactInfo)
		{
			if (!base.enabled)
			{
				return;
			}
			this.TrySticking(impactInfo.collider, impactInfo.estimatedImpactNormal);
		}

		// Token: 0x06004451 RID: 17489 RVA: 0x0011C670 File Offset: 0x0011A870
		private bool TrySticking(Collider hitCollider, Vector3 impactNormal)
		{
			if (this.victim)
			{
				return false;
			}
			GameObject gameObject = null;
			sbyte networkhitHurtboxIndex = -1;
			HurtBox component = hitCollider.GetComponent<HurtBox>();
			if (component)
			{
				HealthComponent healthComponent = component.healthComponent;
				if (healthComponent)
				{
					gameObject = healthComponent.gameObject;
				}
				networkhitHurtboxIndex = (sbyte)component.indexInGroup;
			}
			if (!gameObject && !this.ignoreWorld)
			{
				gameObject = hitCollider.gameObject;
				networkhitHurtboxIndex = -2;
			}
			if (gameObject == this.projectileController.owner || (this.ignoreCharacters && component))
			{
				gameObject = null;
				networkhitHurtboxIndex = -1;
			}
			if (gameObject)
			{
				this.stickEvent.Invoke();
				ParticleSystem[] array = this.stickParticleSystem;
				for (int i = 0; i < array.Length; i++)
				{
					array[i].Play();
				}
				if (this.stickSoundString.Length > 0)
				{
					Util.PlaySound(this.stickSoundString, base.gameObject);
				}
				if (this.alignNormals && impactNormal != Vector3.zero)
				{
					base.transform.rotation = Util.QuaternionSafeLookRotation(impactNormal, base.transform.up);
				}
				Transform transform = hitCollider.transform;
				this.NetworklocalPosition = transform.InverseTransformPoint(base.transform.position);
				this.NetworklocalRotation = Quaternion.Inverse(transform.rotation) * base.transform.rotation;
				this.victim = gameObject;
				this.NetworkhitHurtboxIndex = networkhitHurtboxIndex;
				return true;
			}
			return false;
		}

		// Token: 0x06004452 RID: 17490 RVA: 0x0011C7E0 File Offset: 0x0011A9E0
		private void UpdateSticking()
		{
			bool flag = this.stuckTransform;
			if (flag)
			{
				base.transform.SetPositionAndRotation(this.stuckTransform.TransformPoint(this.localPosition), this.alignNormals ? (this.stuckTransform.rotation * this.localRotation) : base.transform.rotation);
			}
			else
			{
				GameObject gameObject = NetworkServer.active ? this.victim : this.syncVictim;
				if (gameObject)
				{
					this.stuckTransform = gameObject.transform;
					flag = true;
					if (this.hitHurtboxIndex >= 0)
					{
						this.stuckBody = this.stuckTransform.GetComponent<CharacterBody>();
						if (this.stuckBody && this.stuckBody.hurtBoxGroup)
						{
							HurtBox hurtBox = this.stuckBody.hurtBoxGroup.hurtBoxes[(int)this.hitHurtboxIndex];
							this.stuckTransform = (hurtBox ? hurtBox.transform : null);
						}
					}
				}
				else if (this.hitHurtboxIndex == -2 && !NetworkServer.active)
				{
					flag = true;
				}
			}
			if (NetworkServer.active)
			{
				if (this.rigidbody.isKinematic != flag)
				{
					if (flag)
					{
						this.rigidbody.collisionDetectionMode = CollisionDetectionMode.Discrete;
						this.rigidbody.isKinematic = true;
					}
					else
					{
						this.rigidbody.isKinematic = false;
						this.rigidbody.collisionDetectionMode = CollisionDetectionMode.Continuous;
					}
				}
				if (!flag)
				{
					this.NetworkhitHurtboxIndex = -1;
				}
			}
		}

		// Token: 0x06004454 RID: 17492 RVA: 0x000026ED File Offset: 0x000008ED
		private void UNetVersion()
		{
		}

		// Token: 0x17000646 RID: 1606
		// (get) Token: 0x06004455 RID: 17493 RVA: 0x0011C95C File Offset: 0x0011AB5C
		// (set) Token: 0x06004456 RID: 17494 RVA: 0x0011C96F File Offset: 0x0011AB6F
		public GameObject NetworksyncVictim
		{
			get
			{
				return this.syncVictim;
			}
			[param: In]
			set
			{
				base.SetSyncVarGameObject(value, ref this.syncVictim, 1U, ref this.___syncVictimNetId);
			}
		}

		// Token: 0x17000647 RID: 1607
		// (get) Token: 0x06004457 RID: 17495 RVA: 0x0011C98C File Offset: 0x0011AB8C
		// (set) Token: 0x06004458 RID: 17496 RVA: 0x0011C99F File Offset: 0x0011AB9F
		public sbyte NetworkhitHurtboxIndex
		{
			get
			{
				return this.hitHurtboxIndex;
			}
			[param: In]
			set
			{
				base.SetSyncVar<sbyte>(value, ref this.hitHurtboxIndex, 2U);
			}
		}

		// Token: 0x17000648 RID: 1608
		// (get) Token: 0x06004459 RID: 17497 RVA: 0x0011C9B4 File Offset: 0x0011ABB4
		// (set) Token: 0x0600445A RID: 17498 RVA: 0x0011C9C7 File Offset: 0x0011ABC7
		public Vector3 NetworklocalPosition
		{
			get
			{
				return this.localPosition;
			}
			[param: In]
			set
			{
				base.SetSyncVar<Vector3>(value, ref this.localPosition, 4U);
			}
		}

		// Token: 0x17000649 RID: 1609
		// (get) Token: 0x0600445B RID: 17499 RVA: 0x0011C9DC File Offset: 0x0011ABDC
		// (set) Token: 0x0600445C RID: 17500 RVA: 0x0011C9EF File Offset: 0x0011ABEF
		public Quaternion NetworklocalRotation
		{
			get
			{
				return this.localRotation;
			}
			[param: In]
			set
			{
				base.SetSyncVar<Quaternion>(value, ref this.localRotation, 8U);
			}
		}

		// Token: 0x0600445D RID: 17501 RVA: 0x0011CA04 File Offset: 0x0011AC04
		public override bool OnSerialize(NetworkWriter writer, bool forceAll)
		{
			if (forceAll)
			{
				writer.Write(this.syncVictim);
				writer.WritePackedUInt32((uint)this.hitHurtboxIndex);
				writer.Write(this.localPosition);
				writer.Write(this.localRotation);
				return true;
			}
			bool flag = false;
			if ((base.syncVarDirtyBits & 1U) != 0U)
			{
				if (!flag)
				{
					writer.WritePackedUInt32(base.syncVarDirtyBits);
					flag = true;
				}
				writer.Write(this.syncVictim);
			}
			if ((base.syncVarDirtyBits & 2U) != 0U)
			{
				if (!flag)
				{
					writer.WritePackedUInt32(base.syncVarDirtyBits);
					flag = true;
				}
				writer.WritePackedUInt32((uint)this.hitHurtboxIndex);
			}
			if ((base.syncVarDirtyBits & 4U) != 0U)
			{
				if (!flag)
				{
					writer.WritePackedUInt32(base.syncVarDirtyBits);
					flag = true;
				}
				writer.Write(this.localPosition);
			}
			if ((base.syncVarDirtyBits & 8U) != 0U)
			{
				if (!flag)
				{
					writer.WritePackedUInt32(base.syncVarDirtyBits);
					flag = true;
				}
				writer.Write(this.localRotation);
			}
			if (!flag)
			{
				writer.WritePackedUInt32(base.syncVarDirtyBits);
			}
			return flag;
		}

		// Token: 0x0600445E RID: 17502 RVA: 0x0011CB2C File Offset: 0x0011AD2C
		public override void OnDeserialize(NetworkReader reader, bool initialState)
		{
			if (initialState)
			{
				this.___syncVictimNetId = reader.ReadNetworkId();
				this.hitHurtboxIndex = (sbyte)reader.ReadPackedUInt32();
				this.localPosition = reader.ReadVector3();
				this.localRotation = reader.ReadQuaternion();
				return;
			}
			int num = (int)reader.ReadPackedUInt32();
			if ((num & 1) != 0)
			{
				this.syncVictim = reader.ReadGameObject();
			}
			if ((num & 2) != 0)
			{
				this.hitHurtboxIndex = (sbyte)reader.ReadPackedUInt32();
			}
			if ((num & 4) != 0)
			{
				this.localPosition = reader.ReadVector3();
			}
			if ((num & 8) != 0)
			{
				this.localRotation = reader.ReadQuaternion();
			}
		}

		// Token: 0x0600445F RID: 17503 RVA: 0x0011CBDC File Offset: 0x0011ADDC
		public override void PreStartClient()
		{
			if (!this.___syncVictimNetId.IsEmpty())
			{
				this.NetworksyncVictim = ClientScene.FindLocalObject(this.___syncVictimNetId);
			}
		}

		// Token: 0x040042CB RID: 17099
		public string stickSoundString;

		// Token: 0x040042CC RID: 17100
		public ParticleSystem[] stickParticleSystem;

		// Token: 0x040042CD RID: 17101
		public bool ignoreCharacters;

		// Token: 0x040042CE RID: 17102
		public bool ignoreWorld;

		// Token: 0x040042CF RID: 17103
		public bool alignNormals = true;

		// Token: 0x040042D0 RID: 17104
		public UnityEvent stickEvent;

		// Token: 0x040042D1 RID: 17105
		private ProjectileController projectileController;

		// Token: 0x040042D2 RID: 17106
		private Rigidbody rigidbody;

		// Token: 0x040042D5 RID: 17109
		private bool wasEverEnabled;

		// Token: 0x040042D6 RID: 17110
		private GameObject _victim;

		// Token: 0x040042D7 RID: 17111
		[SyncVar]
		private GameObject syncVictim;

		// Token: 0x040042D8 RID: 17112
		[SyncVar]
		private sbyte hitHurtboxIndex = -1;

		// Token: 0x040042D9 RID: 17113
		[SyncVar]
		private Vector3 localPosition;

		// Token: 0x040042DA RID: 17114
		[SyncVar]
		private Quaternion localRotation;

		// Token: 0x040042DB RID: 17115
		private NetworkInstanceId ___syncVictimNetId;
	}
}
