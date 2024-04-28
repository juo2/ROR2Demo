using System;
using UnityEngine;

namespace RoR2.Projectile
{
	// Token: 0x02000B9D RID: 2973
	[RequireComponent(typeof(ProjectileController))]
	public class ProjectileImpactEventCaller : MonoBehaviour, IProjectileImpactBehavior
	{
		// Token: 0x060043A6 RID: 17318 RVA: 0x001191D2 File Offset: 0x001173D2
		public void OnProjectileImpact(ProjectileImpactInfo impactInfo)
		{
			ProjectileImpactEvent projectileImpactEvent = this.impactEvent;
			if (projectileImpactEvent == null)
			{
				return;
			}
			projectileImpactEvent.Invoke(impactInfo);
		}

		// Token: 0x040041FE RID: 16894
		public ProjectileImpactEvent impactEvent;
	}
}
