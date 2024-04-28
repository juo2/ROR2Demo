using System;
using UnityEngine;
using UnityEngine.Networking;

namespace RoR2.Projectile
{
	// Token: 0x02000B7F RID: 2943
	[RequireComponent(typeof(ProjectileImpactExplosion))]
	[RequireComponent(typeof(ProjectileController))]
	[RequireComponent(typeof(ProjectileOwnerOrbiter))]
	[DisallowMultipleComponent]
	public class LunarSunProjectileController : MonoBehaviour
	{
		// Token: 0x06004306 RID: 17158 RVA: 0x00115FE8 File Offset: 0x001141E8
		public void OnEnable()
		{
			this.explosion = base.GetComponent<ProjectileImpactExplosion>();
			if (NetworkServer.active)
			{
				ProjectileController component = base.GetComponent<ProjectileController>();
				if (component.owner)
				{
					this.AcquireOwner(component);
					return;
				}
				component.onInitialized += this.AcquireOwner;
			}
		}

		// Token: 0x06004307 RID: 17159 RVA: 0x00116038 File Offset: 0x00114238
		private void AcquireOwner(ProjectileController controller)
		{
			controller.onInitialized -= this.AcquireOwner;
			CharacterBody component = controller.owner.GetComponent<CharacterBody>();
			if (component)
			{
				ProjectileOwnerOrbiter component2 = base.GetComponent<ProjectileOwnerOrbiter>();
				component.GetComponent<LunarSunBehavior>().InitializeOrbiter(component2, this);
			}
		}

		// Token: 0x06004308 RID: 17160 RVA: 0x0011607F File Offset: 0x0011427F
		public void Detonate()
		{
			if (this.explosion)
			{
				this.explosion.Detonate();
			}
		}

		// Token: 0x04004108 RID: 16648
		private ProjectileImpactExplosion explosion;
	}
}
