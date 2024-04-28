using System;
using RoR2.Projectile;
using UnityEngine;
using UnityEngine.Networking;

namespace EntityStates.VoidJailer
{
	// Token: 0x02000150 RID: 336
	public class DeathState : GenericCharacterDeath
	{
		// Token: 0x1700006E RID: 110
		// (get) Token: 0x060005E0 RID: 1504 RVA: 0x0000CF8A File Offset: 0x0000B18A
		protected override bool shouldAutoDestroy
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060005E1 RID: 1505 RVA: 0x0001904F File Offset: 0x0001724F
		protected override void PlayDeathAnimation(float crossfadeDuration = 0.1f)
		{
			base.PlayCrossfade("Body", "Death", "Death.playbackRate", DeathState.duration, 0.1f);
		}

		// Token: 0x060005E2 RID: 1506 RVA: 0x00019070 File Offset: 0x00017270
		public override void OnEnter()
		{
			base.OnEnter();
			this.muzzleTransform = base.FindModelChild(DeathState.muzzleName);
			if (this.muzzleTransform && base.isAuthority)
			{
				FireProjectileInfo fireProjectileInfo = default(FireProjectileInfo);
				fireProjectileInfo.projectilePrefab = DeathState.deathBombProjectile;
				fireProjectileInfo.position = this.muzzleTransform.position;
				fireProjectileInfo.rotation = Quaternion.LookRotation(base.characterDirection.forward, Vector3.up);
				fireProjectileInfo.owner = base.gameObject;
				fireProjectileInfo.damage = this.damageStat;
				fireProjectileInfo.crit = base.characterBody.RollCrit();
				ProjectileManager.instance.FireProjectile(fireProjectileInfo);
			}
		}

		// Token: 0x060005E3 RID: 1507 RVA: 0x00019125 File Offset: 0x00017325
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.fixedAge >= DeathState.duration)
			{
				base.DestroyModel();
				if (NetworkServer.active)
				{
					base.DestroyBodyAsapServer();
				}
			}
		}

		// Token: 0x060005E4 RID: 1508 RVA: 0x0001886B File Offset: 0x00016A6B
		public override void OnExit()
		{
			base.DestroyModel();
			base.OnExit();
		}

		// Token: 0x040006FD RID: 1789
		public static GameObject deathBombProjectile;

		// Token: 0x040006FE RID: 1790
		public static float duration;

		// Token: 0x040006FF RID: 1791
		public static string muzzleName;

		// Token: 0x04000700 RID: 1792
		private Transform muzzleTransform;
	}
}
