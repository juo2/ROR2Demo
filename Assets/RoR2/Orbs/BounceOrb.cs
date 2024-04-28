using System;
using System.Collections.Generic;
using HG;
using JetBrains.Annotations;
using UnityEngine;

namespace RoR2.Orbs
{
	// Token: 0x02000B08 RID: 2824
	public class BounceOrb : Orb
	{
		// Token: 0x0600409A RID: 16538 RVA: 0x0010AF04 File Offset: 0x00109104
		public override void Begin()
		{
			base.duration = base.distanceToTarget / 70f;
			EffectData effectData = new EffectData
			{
				scale = this.scale,
				origin = this.origin,
				genericFloat = base.duration
			};
			effectData.SetHurtBoxReference(this.target);
			EffectManager.SpawnEffect(LegacyResourcesAPI.Load<GameObject>("Prefabs/Effects/OrbEffects/BounceOrbEffect"), effectData, true);
		}

		// Token: 0x0600409B RID: 16539 RVA: 0x0010AF6C File Offset: 0x0010916C
		public override void OnArrival()
		{
			if (this.target)
			{
				HealthComponent healthComponent = this.target.healthComponent;
				if (healthComponent)
				{
					Vector3 position = this.target.transform.position;
					GameObject gameObject = healthComponent.gameObject;
					DamageInfo damageInfo = new DamageInfo();
					damageInfo.damage = this.damageValue;
					damageInfo.attacker = this.attacker;
					damageInfo.inflictor = null;
					damageInfo.force = (position - this.origin).normalized * -1000f;
					damageInfo.crit = this.isCrit;
					damageInfo.procChainMask = this.procChainMask;
					damageInfo.procCoefficient = this.procCoefficient;
					damageInfo.position = position;
					damageInfo.damageColorIndex = this.damageColorIndex;
					healthComponent.TakeDamage(damageInfo);
					GlobalEventManager.instance.OnHitEnemy(damageInfo, gameObject);
					GlobalEventManager.instance.OnHitAll(damageInfo, gameObject);
				}
			}
		}

		// Token: 0x0600409C RID: 16540 RVA: 0x0010B058 File Offset: 0x00109258
		public static void SearchForTargets([NotNull] BullseyeSearch search, TeamIndex teamIndex, Vector3 position, float range, int maxTargets, [NotNull] List<HurtBox> dest, [NotNull] List<HealthComponent> exclusions)
		{
			search.searchOrigin = position;
			search.searchDirection = Vector3.zero;
			search.teamMaskFilter = TeamMask.GetEnemyTeams(teamIndex);
			search.filterByLoS = false;
			search.sortMode = BullseyeSearch.SortMode.None;
			search.maxDistanceFilter = range;
			search.RefreshCandidates();
			List<HurtBox> list = CollectionPool<HurtBox, List<HurtBox>>.RentCollection();
			foreach (HurtBox item in search.GetResults())
			{
				list.Add(item);
			}
			Util.ShuffleList<HurtBox>(list);
			int i = 0;
			int count = list.Count;
			while (i < count)
			{
				HurtBox hurtBox = list[i];
				if (hurtBox.healthComponent && !exclusions.Contains(hurtBox.healthComponent))
				{
					if (exclusions.Count >= maxTargets)
					{
						break;
					}
					exclusions.Add(hurtBox.healthComponent);
					dest.Add(hurtBox);
				}
				i++;
			}
			CollectionPool<HurtBox, List<HurtBox>>.ReturnCollection(list);
		}

		// Token: 0x04003EDF RID: 16095
		private const float speed = 70f;

		// Token: 0x04003EE0 RID: 16096
		public float damageValue;

		// Token: 0x04003EE1 RID: 16097
		public GameObject attacker;

		// Token: 0x04003EE2 RID: 16098
		public TeamIndex teamIndex;

		// Token: 0x04003EE3 RID: 16099
		public List<HealthComponent> bouncedObjects;

		// Token: 0x04003EE4 RID: 16100
		public bool isCrit;

		// Token: 0x04003EE5 RID: 16101
		public float scale;

		// Token: 0x04003EE6 RID: 16102
		public ProcChainMask procChainMask;

		// Token: 0x04003EE7 RID: 16103
		public float procCoefficient = 0.2f;

		// Token: 0x04003EE8 RID: 16104
		public DamageColorIndex damageColorIndex;
	}
}
