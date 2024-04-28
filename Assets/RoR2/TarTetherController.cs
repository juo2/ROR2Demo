using System;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Networking;

namespace RoR2
{
	// Token: 0x020008B2 RID: 2226
	[RequireComponent(typeof(BezierCurveLine))]
	public class TarTetherController : NetworkBehaviour
	{
		// Token: 0x0600316B RID: 12651 RVA: 0x000D1B79 File Offset: 0x000CFD79
		private void Awake()
		{
			this.bezierCurveLine = base.GetComponent<BezierCurveLine>();
		}

		// Token: 0x0600316C RID: 12652 RVA: 0x000D1B88 File Offset: 0x000CFD88
		private void DoDamageTick(bool mulch)
		{
			if (!this.targetHealthComponent)
			{
				this.targetHealthComponent = this.targetRoot.GetComponent<HealthComponent>();
			}
			if (!this.ownerHealthComponent)
			{
				this.ownerHealthComponent = this.ownerRoot.GetComponent<HealthComponent>();
			}
			if (!this.ownerBody)
			{
				this.ownerBody = this.ownerRoot.GetComponent<CharacterBody>();
			}
			if (this.ownerRoot)
			{
				DamageInfo damageInfo = new DamageInfo
				{
					position = this.targetRoot.transform.position,
					attacker = null,
					inflictor = null,
					damage = (mulch ? (this.damageCoefficientPerTick * this.mulchDamageScale) : this.damageCoefficientPerTick) * this.ownerBody.damage,
					damageColorIndex = DamageColorIndex.Default,
					damageType = DamageType.Generic,
					crit = false,
					force = Vector3.zero,
					procChainMask = default(ProcChainMask),
					procCoefficient = 0f
				};
				this.targetHealthComponent.TakeDamage(damageInfo);
				if (!damageInfo.rejected)
				{
					this.ownerHealthComponent.Heal(damageInfo.damage, default(ProcChainMask), true);
				}
				if (!this.targetHealthComponent.alive)
				{
					this.NetworktargetRoot = null;
				}
			}
		}

		// Token: 0x0600316D RID: 12653 RVA: 0x000D1CD0 File Offset: 0x000CFED0
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

		// Token: 0x0600316E RID: 12654 RVA: 0x000D1D28 File Offset: 0x000CFF28
		private void Update()
		{
			this.age += Time.deltaTime;
			Vector3 position = this.ownerRoot.transform.position;
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

		// Token: 0x0600316F RID: 12655 RVA: 0x000D1DBC File Offset: 0x000CFFBC
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
				Vector3 vector = this.ownerRoot.transform.position - targetRootPosition;
				if (NetworkServer.active)
				{
					float sqrMagnitude = vector.sqrMagnitude;
					bool flag = sqrMagnitude < this.mulchDistanceSqr;
					this.tickTimer -= Time.fixedDeltaTime;
					if (this.tickTimer <= 0f)
					{
						this.tickTimer += (flag ? (this.tickInterval * this.mulchTickIntervalScale) : this.tickInterval);
						this.DoDamageTick(flag);
					}
					if (sqrMagnitude > this.breakDistanceSqr)
					{
						UnityEngine.Object.Destroy(base.gameObject);
						return;
					}
				}
				if (Util.HasEffectiveAuthority(this.targetRoot))
				{
					Vector3 b = vector.normalized * (this.reelSpeed * Time.fixedDeltaTime);
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

		// Token: 0x06003171 RID: 12657 RVA: 0x000026ED File Offset: 0x000008ED
		private void UNetVersion()
		{
		}

		// Token: 0x1700047C RID: 1148
		// (get) Token: 0x06003172 RID: 12658 RVA: 0x000D1F50 File Offset: 0x000D0150
		// (set) Token: 0x06003173 RID: 12659 RVA: 0x000D1F63 File Offset: 0x000D0163
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

		// Token: 0x1700047D RID: 1149
		// (get) Token: 0x06003174 RID: 12660 RVA: 0x000D1F80 File Offset: 0x000D0180
		// (set) Token: 0x06003175 RID: 12661 RVA: 0x000D1F93 File Offset: 0x000D0193
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

		// Token: 0x06003176 RID: 12662 RVA: 0x000D1FB0 File Offset: 0x000D01B0
		public override bool OnSerialize(NetworkWriter writer, bool forceAll)
		{
			if (forceAll)
			{
				writer.Write(this.targetRoot);
				writer.Write(this.ownerRoot);
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
			if (!flag)
			{
				writer.WritePackedUInt32(base.syncVarDirtyBits);
			}
			return flag;
		}

		// Token: 0x06003177 RID: 12663 RVA: 0x000D205C File Offset: 0x000D025C
		public override void OnDeserialize(NetworkReader reader, bool initialState)
		{
			if (initialState)
			{
				this.___targetRootNetId = reader.ReadNetworkId();
				this.___ownerRootNetId = reader.ReadNetworkId();
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
		}

		// Token: 0x06003178 RID: 12664 RVA: 0x000D20C4 File Offset: 0x000D02C4
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
		}

		// Token: 0x040032F2 RID: 13042
		[SyncVar]
		public GameObject targetRoot;

		// Token: 0x040032F3 RID: 13043
		[SyncVar]
		public GameObject ownerRoot;

		// Token: 0x040032F4 RID: 13044
		public float reelSpeed = 12f;

		// Token: 0x040032F5 RID: 13045
		[NonSerialized]
		public float mulchDistanceSqr;

		// Token: 0x040032F6 RID: 13046
		[NonSerialized]
		public float breakDistanceSqr;

		// Token: 0x040032F7 RID: 13047
		[NonSerialized]
		public float mulchDamageScale;

		// Token: 0x040032F8 RID: 13048
		[NonSerialized]
		public float mulchTickIntervalScale;

		// Token: 0x040032F9 RID: 13049
		[NonSerialized]
		public float damageCoefficientPerTick;

		// Token: 0x040032FA RID: 13050
		[NonSerialized]
		public float tickInterval;

		// Token: 0x040032FB RID: 13051
		[NonSerialized]
		public float tickTimer;

		// Token: 0x040032FC RID: 13052
		public float attachTime;

		// Token: 0x040032FD RID: 13053
		private float fixedAge;

		// Token: 0x040032FE RID: 13054
		private float age;

		// Token: 0x040032FF RID: 13055
		private bool beginSiphon;

		// Token: 0x04003300 RID: 13056
		private BezierCurveLine bezierCurveLine;

		// Token: 0x04003301 RID: 13057
		private HealthComponent targetHealthComponent;

		// Token: 0x04003302 RID: 13058
		private HealthComponent ownerHealthComponent;

		// Token: 0x04003303 RID: 13059
		private CharacterBody ownerBody;

		// Token: 0x04003304 RID: 13060
		private NetworkInstanceId ___targetRootNetId;

		// Token: 0x04003305 RID: 13061
		private NetworkInstanceId ___ownerRootNetId;
	}
}
