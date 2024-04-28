using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace RoR2.Orbs
{
	// Token: 0x02000B0D RID: 2829
	public class DevilOrb : Orb
	{
		// Token: 0x060040AA RID: 16554 RVA: 0x0010B708 File Offset: 0x00109908
		public override void Begin()
		{
			base.duration = base.distanceToTarget / 30f;
			EffectData effectData = new EffectData
			{
				scale = this.scale,
				origin = this.origin,
				genericFloat = base.duration
			};
			effectData.SetHurtBoxReference(this.target);
			GameObject effectPrefab = null;
			DevilOrb.EffectType effectType = this.effectType;
			if (effectType != DevilOrb.EffectType.Skull)
			{
				if (effectType == DevilOrb.EffectType.Wisp)
				{
					effectPrefab = LegacyResourcesAPI.Load<GameObject>("Prefabs/Effects/OrbEffects/WispOrbEffect");
				}
			}
			else
			{
				effectPrefab = LegacyResourcesAPI.Load<GameObject>("Prefabs/Effects/OrbEffects/DevilOrbEffect");
			}
			EffectManager.SpawnEffect(effectPrefab, effectData, true);
		}

		// Token: 0x060040AB RID: 16555 RVA: 0x0010B790 File Offset: 0x00109990
		public override void OnArrival()
		{
			if (this.target)
			{
				HealthComponent healthComponent = this.target.healthComponent;
				if (healthComponent)
				{
					DamageInfo damageInfo = new DamageInfo();
					damageInfo.damage = this.damageValue;
					damageInfo.attacker = this.attacker;
					damageInfo.inflictor = null;
					damageInfo.force = Vector3.zero;
					damageInfo.crit = this.isCrit;
					damageInfo.procChainMask = this.procChainMask;
					damageInfo.procCoefficient = this.procCoefficient;
					damageInfo.position = this.target.transform.position;
					damageInfo.damageColorIndex = this.damageColorIndex;
					healthComponent.TakeDamage(damageInfo);
					GlobalEventManager.instance.OnHitEnemy(damageInfo, healthComponent.gameObject);
					GlobalEventManager.instance.OnHitAll(damageInfo, healthComponent.gameObject);
				}
			}
		}

		// Token: 0x060040AC RID: 16556 RVA: 0x0010B864 File Offset: 0x00109A64
		public HurtBox PickNextTarget(Vector3 position, float range)
		{
			BullseyeSearch bullseyeSearch = new BullseyeSearch();
			bullseyeSearch.searchOrigin = position;
			bullseyeSearch.searchDirection = Vector3.zero;
			bullseyeSearch.teamMaskFilter = TeamMask.allButNeutral;
			bullseyeSearch.teamMaskFilter.RemoveTeam(this.teamIndex);
			bullseyeSearch.filterByLoS = false;
			bullseyeSearch.sortMode = BullseyeSearch.SortMode.Distance;
			bullseyeSearch.maxDistanceFilter = range;
			bullseyeSearch.RefreshCandidates();
			List<HurtBox> list = bullseyeSearch.GetResults().ToList<HurtBox>();
			if (list.Count <= 0)
			{
				return null;
			}
			return list[UnityEngine.Random.Range(0, list.Count)];
		}

		// Token: 0x04003F00 RID: 16128
		private const float speed = 30f;

		// Token: 0x04003F01 RID: 16129
		public float damageValue;

		// Token: 0x04003F02 RID: 16130
		public GameObject attacker;

		// Token: 0x04003F03 RID: 16131
		public TeamIndex teamIndex;

		// Token: 0x04003F04 RID: 16132
		public bool isCrit;

		// Token: 0x04003F05 RID: 16133
		public float scale;

		// Token: 0x04003F06 RID: 16134
		public ProcChainMask procChainMask;

		// Token: 0x04003F07 RID: 16135
		public float procCoefficient = 0.2f;

		// Token: 0x04003F08 RID: 16136
		public DamageColorIndex damageColorIndex;

		// Token: 0x04003F09 RID: 16137
		public DevilOrb.EffectType effectType;

		// Token: 0x02000B0E RID: 2830
		public enum EffectType
		{
			// Token: 0x04003F0B RID: 16139
			Skull,
			// Token: 0x04003F0C RID: 16140
			Wisp
		}
	}
}
