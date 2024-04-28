using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using HG;
using UnityEngine;
using UnityEngine.Networking;

namespace RoR2
{
	// Token: 0x020005F4 RID: 1524
	public class BloodSiphonNearbyController : NetworkBehaviour
	{
		// Token: 0x06001BD2 RID: 7122 RVA: 0x00076670 File Offset: 0x00074870
		protected void Awake()
		{
			this.transform = base.transform;
			this.sphereSearch = new SphereSearch();
			this.timer = 0f;
			this.holdoutZone = base.GetComponentInParent<HoldoutZoneController>();
		}

		// Token: 0x06001BD3 RID: 7123 RVA: 0x000766A0 File Offset: 0x000748A0
		protected void FixedUpdate()
		{
			this.timer -= Time.fixedDeltaTime;
			if (this.timer <= 0f)
			{
				this.timer += 1f / this.tickRate;
				this.Tick();
			}
		}

		// Token: 0x06001BD4 RID: 7124 RVA: 0x000766E0 File Offset: 0x000748E0
		private void OnTransformParentChanged()
		{
			this.holdoutZone = base.GetComponentInParent<HoldoutZoneController>();
		}

		// Token: 0x06001BD5 RID: 7125 RVA: 0x000766F0 File Offset: 0x000748F0
		protected void Tick()
		{
			if (this.holdoutZone)
			{
				this.currentRadius = this.holdoutZone.currentRadius;
			}
			List<HurtBox> list = CollectionPool<HurtBox, List<HurtBox>>.RentCollection();
			this.SearchForTargets(list);
			float num = this.minHealthFractionCoefficient;
			if (this.holdoutZone)
			{
				num = Mathf.Lerp(this.minHealthFractionCoefficient, this.maxHealthFractionCoefficient, this.holdoutZone.charge);
			}
			float num2 = num / this.tickRate;
			List<Transform> list2 = CollectionPool<Transform, List<Transform>>.RentCollection();
			for (int i = 0; i < list.Count; i++)
			{
				HurtBox hurtBox = list[i];
				if (hurtBox && hurtBox.healthComponent && hurtBox.healthComponent.alive)
				{
					HealthComponent healthComponent = hurtBox.healthComponent;
					Transform transform = healthComponent.body.coreTransform ?? hurtBox.transform;
					list2.Add(transform);
					if (NetworkServer.active)
					{
						DamageInfo damageInfo = new DamageInfo();
						damageInfo.attacker = null;
						damageInfo.inflictor = base.gameObject;
						damageInfo.position = transform.position;
						damageInfo.crit = false;
						damageInfo.damage = num2 * healthComponent.fullCombinedHealth;
						damageInfo.damageColorIndex = DamageColorIndex.Bleed;
						damageInfo.damageType = this.damageType;
						damageInfo.force = Vector3.zero;
						damageInfo.procCoefficient = 0f;
						damageInfo.procChainMask = default(ProcChainMask);
						hurtBox.healthComponent.TakeDamage(damageInfo);
					}
				}
				if (list2.Count >= this.maxTargets)
				{
					break;
				}
			}
			this.isTetheredToAtLeastOneObject = ((float)list2.Count > 0f);
			if (this.tetherVfxOrigin)
			{
				this.tetherVfxOrigin.SetTetheredTransforms(list2);
			}
			if (this.activeVfx)
			{
				this.activeVfx.SetActive(this.isTetheredToAtLeastOneObject);
			}
			CollectionPool<Transform, List<Transform>>.ReturnCollection(list2);
			CollectionPool<HurtBox, List<HurtBox>>.ReturnCollection(list);
		}

		// Token: 0x06001BD6 RID: 7126 RVA: 0x000768E0 File Offset: 0x00074AE0
		protected void SearchForTargets(List<HurtBox> dest)
		{
			if (this.currentRadius > 0f)
			{
				TeamMask mask = default(TeamMask);
				mask.AddTeam(TeamIndex.Player);
				this.sphereSearch.mask = LayerIndex.entityPrecise.mask;
				this.sphereSearch.origin = this.transform.position;
				this.sphereSearch.radius = this.currentRadius;
				this.sphereSearch.queryTriggerInteraction = QueryTriggerInteraction.UseGlobal;
				this.sphereSearch.RefreshCandidates();
				this.sphereSearch.FilterCandidatesByHurtBoxTeam(mask);
				this.sphereSearch.OrderCandidatesByDistance();
				this.sphereSearch.FilterCandidatesByDistinctHurtBoxEntities();
				this.sphereSearch.GetHurtBoxes(dest);
				this.sphereSearch.ClearCandidates();
			}
		}

		// Token: 0x06001BD8 RID: 7128 RVA: 0x000026ED File Offset: 0x000008ED
		private void UNetVersion()
		{
		}

		// Token: 0x170001DA RID: 474
		// (get) Token: 0x06001BD9 RID: 7129 RVA: 0x000769B4 File Offset: 0x00074BB4
		// (set) Token: 0x06001BDA RID: 7130 RVA: 0x000769C7 File Offset: 0x00074BC7
		public int NetworkmaxTargets
		{
			get
			{
				return this.maxTargets;
			}
			[param: In]
			set
			{
				base.SetSyncVar<int>(value, ref this.maxTargets, 1U);
			}
		}

		// Token: 0x06001BDB RID: 7131 RVA: 0x000769DC File Offset: 0x00074BDC
		public override bool OnSerialize(NetworkWriter writer, bool forceAll)
		{
			if (forceAll)
			{
				writer.WritePackedUInt32((uint)this.maxTargets);
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
				writer.WritePackedUInt32((uint)this.maxTargets);
			}
			if (!flag)
			{
				writer.WritePackedUInt32(base.syncVarDirtyBits);
			}
			return flag;
		}

		// Token: 0x06001BDC RID: 7132 RVA: 0x00076A48 File Offset: 0x00074C48
		public override void OnDeserialize(NetworkReader reader, bool initialState)
		{
			if (initialState)
			{
				this.maxTargets = (int)reader.ReadPackedUInt32();
				return;
			}
			int num = (int)reader.ReadPackedUInt32();
			if ((num & 1) != 0)
			{
				this.maxTargets = (int)reader.ReadPackedUInt32();
			}
		}

		// Token: 0x06001BDD RID: 7133 RVA: 0x000026ED File Offset: 0x000008ED
		public override void PreStartClient()
		{
		}

		// Token: 0x0400219B RID: 8603
		[Min(1E-45f)]
		public float minHealthFractionCoefficient;

		// Token: 0x0400219C RID: 8604
		public float maxHealthFractionCoefficient;

		// Token: 0x0400219D RID: 8605
		public float tickRate = 1f;

		// Token: 0x0400219E RID: 8606
		public DamageType damageType;

		// Token: 0x0400219F RID: 8607
		[SyncVar]
		public int maxTargets;

		// Token: 0x040021A0 RID: 8608
		public TetherVfxOrigin tetherVfxOrigin;

		// Token: 0x040021A1 RID: 8609
		public GameObject activeVfx;

		// Token: 0x040021A2 RID: 8610
		protected new Transform transform;

		// Token: 0x040021A3 RID: 8611
		private HoldoutZoneController holdoutZone;

		// Token: 0x040021A4 RID: 8612
		protected SphereSearch sphereSearch;

		// Token: 0x040021A5 RID: 8613
		protected float timer;

		// Token: 0x040021A6 RID: 8614
		protected float currentRadius;

		// Token: 0x040021A7 RID: 8615
		private bool isTetheredToAtLeastOneObject;
	}
}
