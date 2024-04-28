using System;
using UnityEngine;
using UnityEngine.Networking;

namespace RoR2.Projectile
{
	// Token: 0x02000BBA RID: 3002
	[RequireComponent(typeof(ProjectileDamage))]
	[RequireComponent(typeof(ProjectileController))]
	[RequireComponent(typeof(HealthComponent))]
	public class ProjectileGrantOnKillOnDestroy : MonoBehaviour
	{
		// Token: 0x0600446B RID: 17515 RVA: 0x0011CDCC File Offset: 0x0011AFCC
		private void OnDestroy()
		{
			this.healthComponent = base.GetComponent<HealthComponent>();
			this.projectileController = base.GetComponent<ProjectileController>();
			this.projectileDamage = base.GetComponent<ProjectileDamage>();
			if (NetworkServer.active && this.projectileController.owner)
			{
				DamageInfo damageInfo = new DamageInfo
				{
					attacker = this.projectileController.owner,
					crit = this.projectileDamage.crit,
					damage = this.projectileDamage.damage,
					position = base.transform.position,
					procCoefficient = this.projectileController.procCoefficient,
					damageType = this.projectileDamage.damageType,
					damageColorIndex = this.projectileDamage.damageColorIndex
				};
				HealthComponent victim = this.healthComponent;
				DamageReport damageReport = new DamageReport(damageInfo, victim, damageInfo.damage, this.healthComponent.combinedHealth);
				GlobalEventManager.instance.OnCharacterDeath(damageReport);
			}
		}

		// Token: 0x040042E2 RID: 17122
		private ProjectileController projectileController;

		// Token: 0x040042E3 RID: 17123
		private ProjectileDamage projectileDamage;

		// Token: 0x040042E4 RID: 17124
		private HealthComponent healthComponent;
	}
}
