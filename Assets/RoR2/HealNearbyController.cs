using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using HG;
using UnityEngine;
using UnityEngine.Networking;

namespace RoR2
{
	// Token: 0x02000718 RID: 1816
	[RequireComponent(typeof(NetworkedBodyAttachment))]
	public class HealNearbyController : NetworkBehaviour
	{
		// Token: 0x06002590 RID: 9616 RVA: 0x000A2721 File Offset: 0x000A0921
		protected void Awake()
		{
			this.transform = base.transform;
			this.networkedBodyAttachment = base.GetComponent<NetworkedBodyAttachment>();
			this.sphereSearch = new SphereSearch();
			this.timer = 0f;
		}

		// Token: 0x06002591 RID: 9617 RVA: 0x000A2751 File Offset: 0x000A0951
		protected void FixedUpdate()
		{
			this.timer -= Time.fixedDeltaTime;
			if (this.timer <= 0f)
			{
				this.timer += 1f / this.tickRate;
				this.Tick();
			}
		}

		// Token: 0x06002592 RID: 9618 RVA: 0x000A2794 File Offset: 0x000A0994
		protected void Tick()
		{
			if (!this.networkedBodyAttachment || !this.networkedBodyAttachment.attachedBody || !this.networkedBodyAttachment.attachedBodyObject)
			{
				return;
			}
			List<HurtBox> list = CollectionPool<HurtBox, List<HurtBox>>.RentCollection();
			this.SearchForTargets(list);
			float amount = this.damagePerSecondCoefficient * this.networkedBodyAttachment.attachedBody.damage / this.tickRate;
			List<Transform> list2 = CollectionPool<Transform, List<Transform>>.RentCollection();
			int i = 0;
			while (i < list.Count)
			{
				HurtBox hurtBox = list[i];
				if (!hurtBox || !hurtBox.healthComponent || !this.networkedBodyAttachment.attachedBody.healthComponent.alive || hurtBox.healthComponent.health >= hurtBox.healthComponent.fullHealth || hurtBox.healthComponent.body.HasBuff(DLC1Content.Buffs.EliteEarth))
				{
					goto IL_14A;
				}
				HealthComponent healthComponent = hurtBox.healthComponent;
				if (!(hurtBox.healthComponent.body == this.networkedBodyAttachment.attachedBody))
				{
					CharacterBody body = healthComponent.body;
					Transform item = ((body != null) ? body.coreTransform : null) ?? hurtBox.transform;
					list2.Add(item);
					if (NetworkServer.active)
					{
						healthComponent.Heal(amount, default(ProcChainMask), true);
						goto IL_14A;
					}
					goto IL_14A;
				}
				IL_158:
				i++;
				continue;
				IL_14A:
				if (list2.Count < this.maxTargets)
				{
					goto IL_158;
				}
				break;
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

		// Token: 0x06002593 RID: 9619 RVA: 0x000A2964 File Offset: 0x000A0B64
		protected void SearchForTargets(List<HurtBox> dest)
		{
			TeamMask none = TeamMask.none;
			none.AddTeam(this.networkedBodyAttachment.attachedBody.teamComponent.teamIndex);
			this.sphereSearch.mask = LayerIndex.entityPrecise.mask;
			this.sphereSearch.origin = this.transform.position;
			this.sphereSearch.radius = this.radius + this.networkedBodyAttachment.attachedBody.radius;
			this.sphereSearch.queryTriggerInteraction = QueryTriggerInteraction.UseGlobal;
			this.sphereSearch.RefreshCandidates();
			this.sphereSearch.FilterCandidatesByHurtBoxTeam(none);
			this.sphereSearch.OrderCandidatesByDistance();
			this.sphereSearch.FilterCandidatesByDistinctHurtBoxEntities();
			this.sphereSearch.GetHurtBoxes(dest);
			this.sphereSearch.ClearCandidates();
		}

		// Token: 0x06002595 RID: 9621 RVA: 0x000026ED File Offset: 0x000008ED
		private void UNetVersion()
		{
		}

		// Token: 0x1700032F RID: 815
		// (get) Token: 0x06002596 RID: 9622 RVA: 0x000A2A4C File Offset: 0x000A0C4C
		// (set) Token: 0x06002597 RID: 9623 RVA: 0x000A2A5F File Offset: 0x000A0C5F
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

		// Token: 0x17000330 RID: 816
		// (get) Token: 0x06002598 RID: 9624 RVA: 0x000A2A74 File Offset: 0x000A0C74
		// (set) Token: 0x06002599 RID: 9625 RVA: 0x000A2A87 File Offset: 0x000A0C87
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

		// Token: 0x0600259A RID: 9626 RVA: 0x000A2A9C File Offset: 0x000A0C9C
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

		// Token: 0x0600259B RID: 9627 RVA: 0x000A2B48 File Offset: 0x000A0D48
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

		// Token: 0x0600259C RID: 9628 RVA: 0x000026ED File Offset: 0x000008ED
		public override void PreStartClient()
		{
		}

		// Token: 0x04002963 RID: 10595
		[SyncVar]
		public float radius;

		// Token: 0x04002964 RID: 10596
		public float damagePerSecondCoefficient;

		// Token: 0x04002965 RID: 10597
		[Min(1E-45f)]
		public float tickRate = 1f;

		// Token: 0x04002966 RID: 10598
		[SyncVar]
		public int maxTargets;

		// Token: 0x04002967 RID: 10599
		public TetherVfxOrigin tetherVfxOrigin;

		// Token: 0x04002968 RID: 10600
		public GameObject activeVfx;

		// Token: 0x04002969 RID: 10601
		protected new Transform transform;

		// Token: 0x0400296A RID: 10602
		protected NetworkedBodyAttachment networkedBodyAttachment;

		// Token: 0x0400296B RID: 10603
		protected SphereSearch sphereSearch;

		// Token: 0x0400296C RID: 10604
		protected float timer;

		// Token: 0x0400296D RID: 10605
		private bool isTetheredToAtLeastOneObject;
	}
}
