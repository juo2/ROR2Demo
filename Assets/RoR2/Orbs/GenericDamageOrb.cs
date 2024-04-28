using System;
using UnityEngine;

namespace RoR2.Orbs
{
	// Token: 0x02000B04 RID: 2820
	public class GenericDamageOrb : Orb
	{
		// Token: 0x0600408D RID: 16525 RVA: 0x0010AC74 File Offset: 0x00108E74
		public override void Begin()
		{
			base.duration = base.distanceToTarget / this.speed;
			if (this.GetOrbEffect())
			{
				EffectData effectData = new EffectData
				{
					scale = this.scale,
					origin = this.origin,
					genericFloat = base.duration
				};
				effectData.SetHurtBoxReference(this.target);
				EffectManager.SpawnEffect(this.GetOrbEffect(), effectData, true);
			}
		}

		// Token: 0x0600408E RID: 16526 RVA: 0x00003BE8 File Offset: 0x00001DE8
		protected virtual GameObject GetOrbEffect()
		{
			return null;
		}

		// Token: 0x0600408F RID: 16527 RVA: 0x0010ACE4 File Offset: 0x00108EE4
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
					damageInfo.damageType = this.damageType;
					healthComponent.TakeDamage(damageInfo);
					GlobalEventManager.instance.OnHitEnemy(damageInfo, healthComponent.gameObject);
					GlobalEventManager.instance.OnHitAll(damageInfo, healthComponent.gameObject);
				}
			}
		}

		// Token: 0x04003ED4 RID: 16084
		public float speed = 60f;

		// Token: 0x04003ED5 RID: 16085
		public float damageValue;

		// Token: 0x04003ED6 RID: 16086
		public GameObject attacker;

		// Token: 0x04003ED7 RID: 16087
		public TeamIndex teamIndex;

		// Token: 0x04003ED8 RID: 16088
		public bool isCrit;

		// Token: 0x04003ED9 RID: 16089
		public float scale;

		// Token: 0x04003EDA RID: 16090
		public ProcChainMask procChainMask;

		// Token: 0x04003EDB RID: 16091
		public float procCoefficient = 0.2f;

		// Token: 0x04003EDC RID: 16092
		public DamageColorIndex damageColorIndex;

		// Token: 0x04003EDD RID: 16093
		public DamageType damageType;
	}
}
