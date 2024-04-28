using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using HG;
using UnityEngine;
using UnityEngine.Networking;

namespace RoR2
{
	// Token: 0x02000898 RID: 2200
	[RequireComponent(typeof(NetworkedBodyAttachment))]
	public class SiphonNearbyController : NetworkBehaviour
	{
		// Token: 0x06003092 RID: 12434 RVA: 0x000CE3C9 File Offset: 0x000CC5C9
		protected void Awake()
		{
			this.transform = base.transform;
			this.networkedBodyAttachment = base.GetComponent<NetworkedBodyAttachment>();
			this.sphereSearch = new SphereSearch();
			this.timer = 0f;
		}

		// Token: 0x06003093 RID: 12435 RVA: 0x000CE3F9 File Offset: 0x000CC5F9
		protected void FixedUpdate()
		{
			this.timer -= Time.fixedDeltaTime;
			if (this.timer <= 0f)
			{
				this.timer += 1f / this.tickRate;
				this.Tick();
			}
		}

		// Token: 0x06003094 RID: 12436 RVA: 0x000CE43C File Offset: 0x000CC63C
		protected void Tick()
		{
			if (!this.networkedBodyAttachment || !this.networkedBodyAttachment.attachedBody || !this.networkedBodyAttachment.attachedBodyObject)
			{
				return;
			}
			List<HurtBox> list = CollectionPool<HurtBox, List<HurtBox>>.RentCollection();
			if (!this.networkedBodyAttachment.attachedBody.outOfCombat)
			{
				this.SearchForTargets(list);
			}
			float damage = this.damagePerSecondCoefficient * this.networkedBodyAttachment.attachedBody.damage / this.tickRate;
			float num = 0f;
			List<Transform> list2 = CollectionPool<Transform, List<Transform>>.RentCollection();
			int i = 0;
			while (i < list.Count)
			{
				HurtBox hurtBox = list[i];
				if (!hurtBox || !hurtBox.healthComponent || !hurtBox.healthComponent.alive)
				{
					goto IL_1E8;
				}
				HealthComponent healthComponent = hurtBox.healthComponent;
				if (!(hurtBox.healthComponent.body == this.networkedBodyAttachment.attachedBody))
				{
					CharacterBody body = healthComponent.body;
					Transform transform = ((body != null) ? body.coreTransform : null) ?? hurtBox.transform;
					list2.Add(transform);
					if (!NetworkServer.active)
					{
						goto IL_1E8;
					}
					float combinedHealth = healthComponent.combinedHealth;
					DamageInfo damageInfo = new DamageInfo();
					damageInfo.attacker = this.networkedBodyAttachment.attachedBodyObject;
					damageInfo.inflictor = base.gameObject;
					damageInfo.position = transform.position;
					damageInfo.crit = this.networkedBodyAttachment.attachedBody.RollCrit();
					damageInfo.damage = damage;
					damageInfo.damageColorIndex = DamageColorIndex.Item;
					damageInfo.force = Vector3.zero;
					damageInfo.procCoefficient = 0f;
					damageInfo.damageType = DamageType.ClayGoo;
					damageInfo.procChainMask = default(ProcChainMask);
					healthComponent.TakeDamage(damageInfo);
					if (!damageInfo.rejected)
					{
						float num2 = Mathf.Max(healthComponent.combinedHealth, 0f);
						num += Mathf.Max(new float[]
						{
							combinedHealth - num2
						});
						goto IL_1E8;
					}
					goto IL_1E8;
				}
				IL_1F6:
				i++;
				continue;
				IL_1E8:
				if (list2.Count < this.maxTargets)
				{
					goto IL_1F6;
				}
				break;
			}
			this.isTetheredToAtLeastOneObject = ((float)list2.Count > 0f);
			if (NetworkServer.active && num > 0f && this.networkedBodyAttachment.attachedBody.healthComponent)
			{
				this.networkedBodyAttachment.attachedBody.healthComponent.Heal(num, default(ProcChainMask), true);
			}
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

		// Token: 0x06003095 RID: 12437 RVA: 0x000CE6F4 File Offset: 0x000CC8F4
		protected void SearchForTargets(List<HurtBox> dest)
		{
			this.sphereSearch.mask = LayerIndex.entityPrecise.mask;
			this.sphereSearch.origin = this.transform.position;
			this.sphereSearch.radius = this.radius + this.networkedBodyAttachment.attachedBody.radius;
			this.sphereSearch.queryTriggerInteraction = QueryTriggerInteraction.UseGlobal;
			this.sphereSearch.RefreshCandidates();
			this.sphereSearch.FilterCandidatesByHurtBoxTeam(TeamMask.GetEnemyTeams(this.networkedBodyAttachment.attachedBody.teamComponent.teamIndex));
			this.sphereSearch.OrderCandidatesByDistance();
			this.sphereSearch.FilterCandidatesByDistinctHurtBoxEntities();
			this.sphereSearch.GetHurtBoxes(dest);
			this.sphereSearch.ClearCandidates();
		}

		// Token: 0x06003097 RID: 12439 RVA: 0x000026ED File Offset: 0x000008ED
		private void UNetVersion()
		{
		}

		// Token: 0x1700045E RID: 1118
		// (get) Token: 0x06003098 RID: 12440 RVA: 0x000CE7D4 File Offset: 0x000CC9D4
		// (set) Token: 0x06003099 RID: 12441 RVA: 0x000CE7E7 File Offset: 0x000CC9E7
		public float Networkradius
		{
			get
			{
				return this.radius;
			}
			[param: In]
			set
			{
				base.SetSyncVar<float>(value, ref this.radius, 1U);
			}
		}

		// Token: 0x1700045F RID: 1119
		// (get) Token: 0x0600309A RID: 12442 RVA: 0x000CE7FC File Offset: 0x000CC9FC
		// (set) Token: 0x0600309B RID: 12443 RVA: 0x000CE80F File Offset: 0x000CCA0F
		public int NetworkmaxTargets
		{
			get
			{
				return this.maxTargets;
			}
			[param: In]
			set
			{
				base.SetSyncVar<int>(value, ref this.maxTargets, 2U);
			}
		}

		// Token: 0x0600309C RID: 12444 RVA: 0x000CE824 File Offset: 0x000CCA24
		public override bool OnSerialize(NetworkWriter writer, bool forceAll)
		{
			if (forceAll)
			{
				writer.Write(this.radius);
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
				writer.Write(this.radius);
			}
			if ((base.syncVarDirtyBits & 2U) != 0U)
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

		// Token: 0x0600309D RID: 12445 RVA: 0x000CE8D0 File Offset: 0x000CCAD0
		public override void OnDeserialize(NetworkReader reader, bool initialState)
		{
			if (initialState)
			{
				this.radius = reader.ReadSingle();
				this.maxTargets = (int)reader.ReadPackedUInt32();
				return;
			}
			int num = (int)reader.ReadPackedUInt32();
			if ((num & 1) != 0)
			{
				this.radius = reader.ReadSingle();
			}
			if ((num & 2) != 0)
			{
				this.maxTargets = (int)reader.ReadPackedUInt32();
			}
		}

		// Token: 0x0600309E RID: 12446 RVA: 0x000026ED File Offset: 0x000008ED
		public override void PreStartClient()
		{
		}

		// Token: 0x04003237 RID: 12855
		[SyncVar]
		public float radius;

		// Token: 0x04003238 RID: 12856
		public float damagePerSecondCoefficient;

		// Token: 0x04003239 RID: 12857
		[Min(1E-45f)]
		public float tickRate = 1f;

		// Token: 0x0400323A RID: 12858
		[SyncVar]
		public int maxTargets;

		// Token: 0x0400323B RID: 12859
		public TetherVfxOrigin tetherVfxOrigin;

		// Token: 0x0400323C RID: 12860
		public GameObject activeVfx;

		// Token: 0x0400323D RID: 12861
		protected new Transform transform;

		// Token: 0x0400323E RID: 12862
		protected NetworkedBodyAttachment networkedBodyAttachment;

		// Token: 0x0400323F RID: 12863
		protected SphereSearch sphereSearch;

		// Token: 0x04003240 RID: 12864
		protected float timer;

		// Token: 0x04003241 RID: 12865
		private bool isTetheredToAtLeastOneObject;
	}
}
