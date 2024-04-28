using System;
using UnityEngine;

namespace RoR2.Orbs
{
	// Token: 0x02000B22 RID: 2850
	public class SquidOrb : GenericDamageOrb
	{
		// Token: 0x06004103 RID: 16643 RVA: 0x0010ADE1 File Offset: 0x00108FE1
		public override void Begin()
		{
			this.speed = 120f;
			base.Begin();
		}

		// Token: 0x06004104 RID: 16644 RVA: 0x0010D232 File Offset: 0x0010B432
		protected override GameObject GetOrbEffect()
		{
			return LegacyResourcesAPI.Load<GameObject>("Prefabs/Effects/OrbEffects/SquidOrbEffect");
		}

		// Token: 0x06004105 RID: 16645 RVA: 0x0010D240 File Offset: 0x0010B440
		public override void OnArrival()
		{
			if (this.target)
			{
				HealthComponent healthComponent = this.target.healthComponent;
				if (healthComponent)
				{
					Vector3 vector = this.target.transform.position - this.origin;
					if (vector.sqrMagnitude > 0f)
					{
						vector.Normalize();
						vector *= this.forceScalar;
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
					damageInfo.damageType = this.damageType;
					damageInfo.force = vector;
					healthComponent.TakeDamage(damageInfo);
					GlobalEventManager.instance.OnHitEnemy(damageInfo, healthComponent.gameObject);
					GlobalEventManager.instance.OnHitAll(damageInfo, healthComponent.gameObject);
				}
			}
		}

		// Token: 0x04003F76 RID: 16246
		public float forceScalar;
	}
}
