using System;
using UnityEngine;

namespace RoR2.Projectile
{
	// Token: 0x02000B8A RID: 2954
	[RequireComponent(typeof(CharacterController))]
	[RequireComponent(typeof(ProjectileController))]
	[RequireComponent(typeof(ProjectileDamage))]
	public class ProjectileDamageTrail : MonoBehaviour
	{
		// Token: 0x06004344 RID: 17220 RVA: 0x0011702A File Offset: 0x0011522A
		private void Awake()
		{
			this.projectileController = base.GetComponent<ProjectileController>();
			this.projectileDamage = base.GetComponent<ProjectileDamage>();
		}

		// Token: 0x06004345 RID: 17221 RVA: 0x00117044 File Offset: 0x00115244
		private void FixedUpdate()
		{
			if (!this.currentTrailObject)
			{
				this.currentTrailObject = UnityEngine.Object.Instantiate<GameObject>(this.trailPrefab, base.transform.position, base.transform.rotation);
				DamageTrail component = this.currentTrailObject.GetComponent<DamageTrail>();
				component.damagePerSecond = this.projectileDamage.damage * this.damageToTrailDpsFactor;
				component.owner = this.projectileController.owner;
				return;
			}
			this.currentTrailObject.transform.position = base.transform.position;
		}

		// Token: 0x06004346 RID: 17222 RVA: 0x001170D4 File Offset: 0x001152D4
		private void OnDestroy()
		{
			this.DiscontinueTrail();
		}

		// Token: 0x06004347 RID: 17223 RVA: 0x001170DC File Offset: 0x001152DC
		private void DiscontinueTrail()
		{
			if (this.currentTrailObject)
			{
				this.currentTrailObject.AddComponent<DestroyOnTimer>().duration = this.trailLifetimeAfterExpiration;
				this.currentTrailObject.GetComponent<DamageTrail>().active = false;
				this.currentTrailObject = null;
			}
		}

		// Token: 0x04004161 RID: 16737
		public GameObject trailPrefab;

		// Token: 0x04004162 RID: 16738
		public float damageToTrailDpsFactor = 1f;

		// Token: 0x04004163 RID: 16739
		public float trailLifetimeAfterExpiration = 1f;

		// Token: 0x04004164 RID: 16740
		private ProjectileController projectileController;

		// Token: 0x04004165 RID: 16741
		private ProjectileDamage projectileDamage;

		// Token: 0x04004166 RID: 16742
		private GameObject currentTrailObject;
	}
}
