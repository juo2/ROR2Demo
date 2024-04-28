using System;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Networking;

namespace RoR2
{
	// Token: 0x020005AA RID: 1450
	[RequireComponent(typeof(BezierCurveLine))]
	public class JailerTetherController : NetworkBehaviour
	{
		// Token: 0x06001A2E RID: 6702 RVA: 0x00070DEA File Offset: 0x0006EFEA
		private void Awake()
		{
			this.bezierCurveLine = base.GetComponent<BezierCurveLine>();
		}

		// Token: 0x06001A2F RID: 6703 RVA: 0x00070DF8 File Offset: 0x0006EFF8
		private void Start()
		{
			if (!this.targetHealthComponent)
			{
				this.targetHealthComponent = this.targetRoot.GetComponent<HealthComponent>();
			}
			if (!this.ownerBody)
			{
				this.ownerBody = this.ownerRoot.GetComponent<CharacterBody>();
			}
			this.Networkorigin = ((this.origin != null) ? this.origin : this.ownerRoot);
		}

		// Token: 0x06001A30 RID: 6704 RVA: 0x00070E64 File Offset: 0x0006F064
		private void LateUpdate()
		{
			this.age += Time.deltaTime;
			Vector3 position = this.origin.transform.position;
			if (!this.beginSiphon)
			{
				Vector3 position2 = Vector3.Lerp(position, this.GetTargetRootPosition(), this.age / this.attachTime);
				this.bezierCurveLine.endTransform.position = position2;
				return;
			}
			if (this.targetRoot)
			{
				this.bezierCurveLine.endTransform.position = this.targetRoot.transform.position;
			}
		}

		// Token: 0x06001A31 RID: 6705 RVA: 0x00070EF8 File Offset: 0x0006F0F8
		private void FixedUpdate()
		{
			this.fixedAge += Time.fixedDeltaTime;
			if (this.targetRoot && this.ownerRoot)
			{
				Vector3 targetRootPosition = this.GetTargetRootPosition();
				if (!this.beginSiphon && this.fixedAge >= this.attachTime)
				{
					this.beginSiphon = true;
					return;
				}
				Vector3 vector = this.origin.transform.position - targetRootPosition;
				if (NetworkServer.active)
				{
					float sqrMagnitude = vector.sqrMagnitude;
					this.tickTimer -= Time.fixedDeltaTime;
					if (this.tickTimer <= 0f)
					{
						this.tickTimer += this.tickInterval;
						this.DoDamageTick();
					}
					if (sqrMagnitude > this.breakDistanceSqr)
					{
						UnityEngine.Object.Destroy(base.gameObject);
						return;
					}
				}
				if (Util.HasEffectiveAuthority(this.targetRoot))
				{
					Vector3 b = this.reelSpeed * Time.fixedDeltaTime * vector.normalized;
					CharacterMotor component = this.targetRoot.GetComponent<CharacterMotor>();
					if (component)
					{
						component.rootMotion += b;
						return;
					}
					Rigidbody component2 = this.targetRoot.GetComponent<Rigidbody>();
					if (component2)
					{
						component2.velocity += b;
						return;
					}
				}
			}
			else if (NetworkServer.active)
			{
				UnityEngine.Object.Destroy(base.gameObject);
			}
		}

		// Token: 0x06001A32 RID: 6706 RVA: 0x00071058 File Offset: 0x0006F258
		private void DoDamageTick()
		{
			DamageInfo damageInfo = new DamageInfo
			{
				position = this.targetRoot.transform.position,
				attacker = null,
				inflictor = null,
				damage = this.damageCoefficientPerTick * this.ownerBody.damage,
				damageColorIndex = DamageColorIndex.Default,
				damageType = DamageType.Generic,
				crit = false,
				force = Vector3.zero,
				procChainMask = default(ProcChainMask),
				procCoefficient = 0f
			};
			this.targetHealthComponent.TakeDamage(damageInfo);
			if (!this.targetHealthComponent.alive)
			{
				this.NetworktargetRoot = null;
			}
		}

		// Token: 0x06001A33 RID: 6707 RVA: 0x000710FE File Offset: 0x0006F2FE
		public override void OnNetworkDestroy()
		{
			if (NetworkServer.active)
			{
				this.RemoveBuff();
			}
			base.OnNetworkDestroy();
		}

		// Token: 0x06001A34 RID: 6708 RVA: 0x00071113 File Offset: 0x0006F313
		private void RemoveBuff()
		{
			if (this.tetheredBuff && this.targetBody)
			{
				this.targetBody.RemoveBuff(this.tetheredBuff);
			}
		}

		// Token: 0x06001A35 RID: 6709 RVA: 0x00071140 File Offset: 0x0006F340
		public CharacterBody GetTargetBody()
		{
			if (this.targetBody)
			{
				return this.targetBody;
			}
			if (this.targetRoot)
			{
				this.targetBody = this.targetRoot.GetComponent<CharacterBody>();
				return this.targetBody;
			}
			return null;
		}

		// Token: 0x06001A36 RID: 6710 RVA: 0x0007117C File Offset: 0x0006F37C
		private Vector3 GetTargetRootPosition()
		{
			if (this.targetRoot)
			{
				Vector3 result = this.targetRoot.transform.position;
				if (this.targetHealthComponent)
				{
					result = this.targetHealthComponent.body.corePosition;
				}
				return result;
			}
			return base.transform.position;
		}

		// Token: 0x06001A37 RID: 6711 RVA: 0x000711D4 File Offset: 0x0006F3D4
		public void SetTetheredBuff(BuffDef buffDef)
		{
			if (buffDef != null)
			{
				CharacterBody characterBody = this.GetTargetBody();
				if (characterBody)
				{
					this.tetheredBuff = buffDef;
					characterBody.AddBuff(this.tetheredBuff);
				}
			}
		}

		// Token: 0x06001A39 RID: 6713 RVA: 0x000026ED File Offset: 0x000008ED
		private void UNetVersion()
		{
		}

		// Token: 0x170001B3 RID: 435
		// (get) Token: 0x06001A3A RID: 6714 RVA: 0x00071220 File Offset: 0x0006F420
		// (set) Token: 0x06001A3B RID: 6715 RVA: 0x00071233 File Offset: 0x0006F433
		public GameObject NetworktargetRoot
		{
			get
			{
				return this.targetRoot;
			}
			[param: In]
			set
			{
				base.SetSyncVarGameObject(value, ref this.targetRoot, 1U, ref this.___targetRootNetId);
			}
		}

		// Token: 0x170001B4 RID: 436
		// (get) Token: 0x06001A3C RID: 6716 RVA: 0x00071250 File Offset: 0x0006F450
		// (set) Token: 0x06001A3D RID: 6717 RVA: 0x00071263 File Offset: 0x0006F463
		public GameObject NetworkownerRoot
		{
			get
			{
				return this.ownerRoot;
			}
			[param: In]
			set
			{
				base.SetSyncVarGameObject(value, ref this.ownerRoot, 2U, ref this.___ownerRootNetId);
			}
		}

		// Token: 0x170001B5 RID: 437
		// (get) Token: 0x06001A3E RID: 6718 RVA: 0x00071280 File Offset: 0x0006F480
		// (set) Token: 0x06001A3F RID: 6719 RVA: 0x00071293 File Offset: 0x0006F493
		public GameObject Networkorigin
		{
			get
			{
				return this.origin;
			}
			[param: In]
			set
			{
				base.SetSyncVarGameObject(value, ref this.origin, 4U, ref this.___originNetId);
			}
		}

		// Token: 0x170001B6 RID: 438
		// (get) Token: 0x06001A40 RID: 6720 RVA: 0x000712B0 File Offset: 0x0006F4B0
		// (set) Token: 0x06001A41 RID: 6721 RVA: 0x000712C3 File Offset: 0x0006F4C3
		public float NetworkreelSpeed
		{
			get
			{
				return this.reelSpeed;
			}
			[param: In]
			set
			{
				base.SetSyncVar<float>(value, ref this.reelSpeed, 8U);
			}
		}

		// Token: 0x06001A42 RID: 6722 RVA: 0x000712D8 File Offset: 0x0006F4D8
		public override bool OnSerialize(NetworkWriter writer, bool forceAll)
		{
			if (forceAll)
			{
				writer.Write(this.targetRoot);
				writer.Write(this.ownerRoot);
				writer.Write(this.origin);
				writer.Write(this.reelSpeed);
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
				writer.Write(this.targetRoot);
			}
			if ((base.syncVarDirtyBits & 2U) != 0U)
			{
				if (!flag)
				{
					writer.WritePackedUInt32(base.syncVarDirtyBits);
					flag = true;
				}
				writer.Write(this.ownerRoot);
			}
			if ((base.syncVarDirtyBits & 4U) != 0U)
			{
				if (!flag)
				{
					writer.WritePackedUInt32(base.syncVarDirtyBits);
					flag = true;
				}
				writer.Write(this.origin);
			}
			if ((base.syncVarDirtyBits & 8U) != 0U)
			{
				if (!flag)
				{
					writer.WritePackedUInt32(base.syncVarDirtyBits);
					flag = true;
				}
				writer.Write(this.reelSpeed);
			}
			if (!flag)
			{
				writer.WritePackedUInt32(base.syncVarDirtyBits);
			}
			return flag;
		}

		// Token: 0x06001A43 RID: 6723 RVA: 0x00071400 File Offset: 0x0006F600
		public override void OnDeserialize(NetworkReader reader, bool initialState)
		{
			if (initialState)
			{
				this.___targetRootNetId = reader.ReadNetworkId();
				this.___ownerRootNetId = reader.ReadNetworkId();
				this.___originNetId = reader.ReadNetworkId();
				this.reelSpeed = reader.ReadSingle();
				return;
			}
			int num = (int)reader.ReadPackedUInt32();
			if ((num & 1) != 0)
			{
				this.targetRoot = reader.ReadGameObject();
			}
			if ((num & 2) != 0)
			{
				this.ownerRoot = reader.ReadGameObject();
			}
			if ((num & 4) != 0)
			{
				this.origin = reader.ReadGameObject();
			}
			if ((num & 8) != 0)
			{
				this.reelSpeed = reader.ReadSingle();
			}
		}

		// Token: 0x06001A44 RID: 6724 RVA: 0x000714B0 File Offset: 0x0006F6B0
		public override void PreStartClient()
		{
			if (!this.___targetRootNetId.IsEmpty())
			{
				this.NetworktargetRoot = ClientScene.FindLocalObject(this.___targetRootNetId);
			}
			if (!this.___ownerRootNetId.IsEmpty())
			{
				this.NetworkownerRoot = ClientScene.FindLocalObject(this.___ownerRootNetId);
			}
			if (!this.___originNetId.IsEmpty())
			{
				this.Networkorigin = ClientScene.FindLocalObject(this.___originNetId);
			}
		}

		// Token: 0x04002062 RID: 8290
		[SyncVar]
		public GameObject targetRoot;

		// Token: 0x04002063 RID: 8291
		[SyncVar]
		public GameObject ownerRoot;

		// Token: 0x04002064 RID: 8292
		[SyncVar]
		public GameObject origin;

		// Token: 0x04002065 RID: 8293
		[SyncVar]
		public float reelSpeed = 12f;

		// Token: 0x04002066 RID: 8294
		[NonSerialized]
		public float breakDistanceSqr;

		// Token: 0x04002067 RID: 8295
		[NonSerialized]
		public float damageCoefficientPerTick;

		// Token: 0x04002068 RID: 8296
		[NonSerialized]
		public float tickInterval;

		// Token: 0x04002069 RID: 8297
		[NonSerialized]
		public float tickTimer;

		// Token: 0x0400206A RID: 8298
		public float attachTime;

		// Token: 0x0400206B RID: 8299
		private float fixedAge;

		// Token: 0x0400206C RID: 8300
		private float age;

		// Token: 0x0400206D RID: 8301
		private bool beginSiphon;

		// Token: 0x0400206E RID: 8302
		private BezierCurveLine bezierCurveLine;

		// Token: 0x0400206F RID: 8303
		private HealthComponent targetHealthComponent;

		// Token: 0x04002070 RID: 8304
		private CharacterBody ownerBody;

		// Token: 0x04002071 RID: 8305
		private CharacterBody targetBody;

		// Token: 0x04002072 RID: 8306
		private BuffDef tetheredBuff;

		// Token: 0x04002073 RID: 8307
		private NetworkInstanceId ___targetRootNetId;

		// Token: 0x04002074 RID: 8308
		private NetworkInstanceId ___ownerRootNetId;

		// Token: 0x04002075 RID: 8309
		private NetworkInstanceId ___originNetId;
	}
}
