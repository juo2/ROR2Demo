using System;
using UnityEngine;

namespace RoR2.Orbs
{
	// Token: 0x02000B0A RID: 2826
	public class DamageOrb : Orb
	{
		// Token: 0x060040A3 RID: 16547 RVA: 0x0010B4D4 File Offset: 0x001096D4
		public override void Begin()
		{
			GameObject effectPrefab = null;
			if (this.damageOrbType == DamageOrb.DamageOrbType.ClayGooOrb)
			{
				this.speed = 5f;
				effectPrefab = LegacyResourcesAPI.Load<GameObject>("Prefabs/Effects/OrbEffects/ClayGooOrbEffect");
				this.orbDamageType = DamageType.ClayGoo;
			}
			base.duration = base.distanceToTarget / this.speed;
			EffectData effectData = new EffectData
			{
				origin = this.origin,
				genericFloat = base.duration
			};
			effectData.SetHurtBoxReference(this.target);
			EffectManager.SpawnEffect(effectPrefab, effectData, true);
		}

		// Token: 0x060040A4 RID: 16548 RVA: 0x0010B554 File Offset: 0x00109754
		public override void OnArrival()
		{
			if (this.target)
			{
				HealthComponent healthComponent = this.target.healthComponent;
				if (healthComponent)
				{
					if (this.damageOrbType == DamageOrb.DamageOrbType.ClayGooOrb)
					{
						CharacterBody component = healthComponent.GetComponent<CharacterBody>();
						if (component && (component.bodyFlags & CharacterBody.BodyFlags.ImmuneToGoo) != CharacterBody.BodyFlags.None)
						{
							healthComponent.Heal(this.damageValue, default(ProcChainMask), true);
							return;
						}
					}
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
					damageInfo.damageType = this.orbDamageType;
					healthComponent.TakeDamage(damageInfo);
					GlobalEventManager.instance.OnHitEnemy(damageInfo, healthComponent.gameObject);
					GlobalEventManager.instance.OnHitAll(damageInfo, healthComponent.gameObject);
				}
			}
		}

		// Token: 0x04003EF1 RID: 16113
		private float speed = 60f;

		// Token: 0x04003EF2 RID: 16114
		public float damageValue;

		// Token: 0x04003EF3 RID: 16115
		public GameObject attacker;

		// Token: 0x04003EF4 RID: 16116
		public TeamIndex teamIndex;

		// Token: 0x04003EF5 RID: 16117
		public bool isCrit;

		// Token: 0x04003EF6 RID: 16118
		public ProcChainMask procChainMask;

		// Token: 0x04003EF7 RID: 16119
		public float procCoefficient = 0.2f;

		// Token: 0x04003EF8 RID: 16120
		public DamageColorIndex damageColorIndex;

		// Token: 0x04003EF9 RID: 16121
		public DamageOrb.DamageOrbType damageOrbType;

		// Token: 0x04003EFA RID: 16122
		private DamageType orbDamageType;

		// Token: 0x02000B0B RID: 2827
		public enum DamageOrbType
		{
			// Token: 0x04003EFC RID: 16124
			ClayGooOrb
		}
	}
}
