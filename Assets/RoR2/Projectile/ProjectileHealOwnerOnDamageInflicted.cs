using System;
using RoR2.Orbs;
using UnityEngine;

namespace RoR2.Projectile
{
	// Token: 0x02000BBB RID: 3003
	[RequireComponent(typeof(ProjectileController))]
	public class ProjectileHealOwnerOnDamageInflicted : MonoBehaviour, IOnDamageInflictedServerReceiver
	{
		// Token: 0x0600446D RID: 17517 RVA: 0x0011CEC4 File Offset: 0x0011B0C4
		private void Awake()
		{
			this.projectileController = base.GetComponent<ProjectileController>();
		}

		// Token: 0x0600446E RID: 17518 RVA: 0x0011CED4 File Offset: 0x0011B0D4
		public void OnDamageInflictedServer(DamageReport damageReport)
		{
			if (this.projectileController.owner)
			{
				HealthComponent component = this.projectileController.owner.GetComponent<HealthComponent>();
				if (component)
				{
					HealOrb healOrb = new HealOrb();
					healOrb.origin = base.transform.position;
					healOrb.target = component.body.mainHurtBox;
					healOrb.healValue = damageReport.damageDealt * this.fractionOfDamage;
					healOrb.overrideDuration = 0.3f;
					OrbManager.instance.AddOrb(healOrb);
				}
			}
		}

		// Token: 0x040042E5 RID: 17125
		public float fractionOfDamage;

		// Token: 0x040042E6 RID: 17126
		private ProjectileController projectileController;
	}
}
