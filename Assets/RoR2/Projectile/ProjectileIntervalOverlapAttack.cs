using System;
using UnityEngine;
using UnityEngine.Networking;

namespace RoR2.Projectile
{
	// Token: 0x02000BA4 RID: 2980
	[RequireComponent(typeof(ProjectileController))]
	[RequireComponent(typeof(ProjectileDamage))]
	public class ProjectileIntervalOverlapAttack : MonoBehaviour
	{
		// Token: 0x060043B8 RID: 17336 RVA: 0x001197C9 File Offset: 0x001179C9
		private void Awake()
		{
			this.projectileController = base.GetComponent<ProjectileController>();
			this.projectileDamage = base.GetComponent<ProjectileDamage>();
		}

		// Token: 0x060043B9 RID: 17337 RVA: 0x001197E3 File Offset: 0x001179E3
		private void Start()
		{
			this.countdown = 0f;
		}

		// Token: 0x060043BA RID: 17338 RVA: 0x001197F0 File Offset: 0x001179F0
		private void FixedUpdate()
		{
			if (NetworkServer.active)
			{
				this.countdown -= Time.fixedDeltaTime;
				if (this.countdown <= 0f)
				{
					this.countdown += this.interval;
					if (this.hitBoxGroup)
					{
						new OverlapAttack
						{
							attacker = this.projectileController.owner,
							inflictor = base.gameObject,
							teamIndex = this.projectileController.teamFilter.teamIndex,
							damage = this.projectileDamage.damage * this.damageCoefficient,
							hitBoxGroup = this.hitBoxGroup,
							isCrit = this.projectileDamage.crit,
							procCoefficient = 0f,
							damageType = this.projectileDamage.damageType
						}.Fire(null);
					}
				}
			}
		}

		// Token: 0x0400421F RID: 16927
		public HitBoxGroup hitBoxGroup;

		// Token: 0x04004220 RID: 16928
		public float interval;

		// Token: 0x04004221 RID: 16929
		public float damageCoefficient = 1f;

		// Token: 0x04004222 RID: 16930
		private float countdown;

		// Token: 0x04004223 RID: 16931
		private ProjectileController projectileController;

		// Token: 0x04004224 RID: 16932
		private ProjectileDamage projectileDamage;
	}
}
