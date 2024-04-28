using System;
using UnityEngine;

namespace RoR2.Projectile
{
	// Token: 0x02000BA1 RID: 2977
	[RequireComponent(typeof(ProjectileController))]
	public class ProjectileImpaleOnEnemy : MonoBehaviour, IProjectileImpactBehavior
	{
		// Token: 0x060043AF RID: 17327 RVA: 0x001195CE File Offset: 0x001177CE
		private void Awake()
		{
			this.rigidbody = base.GetComponent<Rigidbody>();
		}

		// Token: 0x060043B0 RID: 17328 RVA: 0x001195DC File Offset: 0x001177DC
		public void OnProjectileImpact(ProjectileImpactInfo impactInfo)
		{
			if (!this.alive)
			{
				return;
			}
			Collider collider = impactInfo.collider;
			if (collider)
			{
				HurtBox component = collider.GetComponent<HurtBox>();
				if (component)
				{
					Vector3 position = base.transform.position;
					Vector3 estimatedPointOfImpact = impactInfo.estimatedPointOfImpact;
					Quaternion identity = Quaternion.identity;
					if (this.rigidbody)
					{
						Util.QuaternionSafeLookRotation(this.rigidbody.velocity);
					}
					GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.impalePrefab, component.transform);
					gameObject.transform.position = estimatedPointOfImpact;
					gameObject.transform.rotation = base.transform.rotation;
				}
			}
		}

		// Token: 0x04004214 RID: 16916
		private bool alive = true;

		// Token: 0x04004215 RID: 16917
		public GameObject impalePrefab;

		// Token: 0x04004216 RID: 16918
		private Rigidbody rigidbody;
	}
}
