using System;
using UnityEngine;

namespace RoR2.Projectile
{
	// Token: 0x02000BA5 RID: 2981
	[RequireComponent(typeof(ProjectileController))]
	[RequireComponent(typeof(ProjectileDamage))]
	public class ProjectileMageFirewallController : MonoBehaviour, IProjectileImpactBehavior
	{
		// Token: 0x060043BC RID: 17340 RVA: 0x001198EF File Offset: 0x00117AEF
		private void Awake()
		{
			this.projectileController = base.GetComponent<ProjectileController>();
			this.projectileDamage = base.GetComponent<ProjectileDamage>();
		}

		// Token: 0x060043BD RID: 17341 RVA: 0x00119909 File Offset: 0x00117B09
		void IProjectileImpactBehavior.OnProjectileImpact(ProjectileImpactInfo impactInfo)
		{
			if (this.consumed)
			{
				return;
			}
			this.consumed = true;
			this.CreateWalkers();
			UnityEngine.Object.Destroy(base.gameObject);
		}

		// Token: 0x060043BE RID: 17342 RVA: 0x0011992C File Offset: 0x00117B2C
		private void CreateWalkers()
		{
			Vector3 forward = base.transform.forward;
			forward.y = 0f;
			forward.Normalize();
			Vector3 vector = Vector3.Cross(Vector3.up, forward);
			ProjectileManager.instance.FireProjectile(this.walkerPrefab, base.transform.position + Vector3.up, Util.QuaternionSafeLookRotation(vector), this.projectileController.owner, this.projectileDamage.damage, this.projectileDamage.force, this.projectileDamage.crit, this.projectileDamage.damageColorIndex, null, -1f);
			ProjectileManager.instance.FireProjectile(this.walkerPrefab, base.transform.position + Vector3.up, Util.QuaternionSafeLookRotation(-vector), this.projectileController.owner, this.projectileDamage.damage, this.projectileDamage.force, this.projectileDamage.crit, this.projectileDamage.damageColorIndex, null, -1f);
		}

		// Token: 0x04004225 RID: 16933
		public GameObject walkerPrefab;

		// Token: 0x04004226 RID: 16934
		private ProjectileController projectileController;

		// Token: 0x04004227 RID: 16935
		private ProjectileDamage projectileDamage;

		// Token: 0x04004228 RID: 16936
		private bool consumed;
	}
}
