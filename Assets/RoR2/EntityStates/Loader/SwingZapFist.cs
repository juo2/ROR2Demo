using System;
using RoR2;
using RoR2.Projectile;
using UnityEngine;

namespace EntityStates.Loader
{
	// Token: 0x020002CB RID: 715
	public class SwingZapFist : BaseSwingChargedFist
	{
		// Token: 0x06000CB0 RID: 3248 RVA: 0x0003570C File Offset: 0x0003390C
		protected override void OnMeleeHitAuthority()
		{
			if (this.hasHit)
			{
				return;
			}
			base.OnMeleeHitAuthority();
			this.hasHit = true;
			if (base.FindModelChild(this.swingEffectMuzzleString))
			{
				FireProjectileInfo fireProjectileInfo = default(FireProjectileInfo);
				fireProjectileInfo.position = base.FindModelChild(this.swingEffectMuzzleString).position;
				fireProjectileInfo.rotation = Quaternion.LookRotation(this.punchVelocity);
				fireProjectileInfo.crit = base.isCritAuthority;
				fireProjectileInfo.damage = 1f * this.damageStat;
				fireProjectileInfo.owner = base.gameObject;
				fireProjectileInfo.projectilePrefab = LegacyResourcesAPI.Load<GameObject>("Prefabs/Projectiles/LoaderZapCone");
				ProjectileManager.instance.FireProjectile(fireProjectileInfo);
			}
		}

		// Token: 0x06000CB1 RID: 3249 RVA: 0x000357BD File Offset: 0x000339BD
		protected override void AuthorityExitHitPause()
		{
			base.AuthorityExitHitPause();
			this.outer.SetNextStateToMain();
		}

		// Token: 0x06000CB2 RID: 3250 RVA: 0x000357D0 File Offset: 0x000339D0
		public override void OnExit()
		{
			base.OnExit();
			if (base.isAuthority && this.hasHit && base.healthComponent)
			{
				Vector3 vector = this.punchVelocity;
				vector.y = Mathf.Min(vector.y, 0f);
				vector = vector.normalized;
				vector *= -SwingZapFist.selfKnockback;
				if (base.characterMotor)
				{
					base.characterMotor.ApplyForce(vector, true, false);
				}
			}
		}

		// Token: 0x04000F80 RID: 3968
		public static float selfKnockback;

		// Token: 0x04000F81 RID: 3969
		private bool hasHit;
	}
}
