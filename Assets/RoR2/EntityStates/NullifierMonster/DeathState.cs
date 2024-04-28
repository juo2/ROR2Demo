using System;
using RoR2.Projectile;
using UnityEngine;
using UnityEngine.Networking;

namespace EntityStates.NullifierMonster
{
	// Token: 0x02000232 RID: 562
	public class DeathState : GenericCharacterDeath
	{
		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x060009EF RID: 2543 RVA: 0x0000CF8A File Offset: 0x0000B18A
		protected override bool shouldAutoDestroy
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060009F0 RID: 2544 RVA: 0x00029299 File Offset: 0x00027499
		protected override void PlayDeathAnimation(float crossfadeDuration = 0.1f)
		{
			base.PlayCrossfade("Body", "Death", "Death.playbackRate", DeathState.duration, 0.1f);
		}

		// Token: 0x060009F1 RID: 2545 RVA: 0x000292BC File Offset: 0x000274BC
		public override void OnEnter()
		{
			base.OnEnter();
			this.muzzleTransform = base.FindModelChild(DeathState.muzzleName);
			if (this.muzzleTransform && base.isAuthority)
			{
				FireProjectileInfo fireProjectileInfo = default(FireProjectileInfo);
				fireProjectileInfo.projectilePrefab = DeathState.deathBombProjectile;
				fireProjectileInfo.position = this.muzzleTransform.position;
				fireProjectileInfo.rotation = Quaternion.identity;
				fireProjectileInfo.owner = base.gameObject;
				fireProjectileInfo.damage = this.damageStat;
				fireProjectileInfo.crit = base.characterBody.RollCrit();
				ProjectileManager.instance.FireProjectile(fireProjectileInfo);
			}
		}

		// Token: 0x060009F2 RID: 2546 RVA: 0x0002935E File Offset: 0x0002755E
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

		// Token: 0x060009F3 RID: 2547 RVA: 0x0001886B File Offset: 0x00016A6B
		public override void OnExit()
		{
			base.DestroyModel();
			base.OnExit();
		}

		// Token: 0x04000B90 RID: 2960
		public static GameObject deathBombProjectile;

		// Token: 0x04000B91 RID: 2961
		public static float duration;

		// Token: 0x04000B92 RID: 2962
		public static string muzzleName;

		// Token: 0x04000B93 RID: 2963
		private Transform muzzleTransform;
	}
}
