using System;
using RoR2;
using RoR2.Projectile;
using UnityEngine;
using UnityEngine.Networking;

namespace EntityStates.VoidMegaCrab
{
	// Token: 0x02000147 RID: 327
	public class DeathState : GenericCharacterDeath
	{
		// Token: 0x1700006D RID: 109
		// (get) Token: 0x060005BF RID: 1471 RVA: 0x0000CF8A File Offset: 0x0000B18A
		protected override bool shouldAutoDestroy
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060005C0 RID: 1472 RVA: 0x00018750 File Offset: 0x00016950
		protected override void PlayDeathAnimation(float crossfadeDuration = 0.1f)
		{
			base.PlayCrossfade("Body", "Death", "Death.playbackRate", DeathState.duration, 0.1f);
			this.PlayAnimation("Left Gun Override (Arm)", "Empty");
			this.PlayAnimation("Right Gun Override (Arm)", "Empty");
		}

		// Token: 0x060005C1 RID: 1473 RVA: 0x0001879C File Offset: 0x0001699C
		public override void OnEnter()
		{
			base.OnEnter();
			this.muzzleTransform = base.FindModelChild(DeathState.muzzleName);
			if (this.muzzleTransform && base.isAuthority)
			{
				FireProjectileInfo fireProjectileInfo = default(FireProjectileInfo);
				fireProjectileInfo.projectilePrefab = DeathState.deathBombProjectile;
				fireProjectileInfo.position = this.muzzleTransform.position;
				fireProjectileInfo.rotation = Util.QuaternionSafeLookRotation(Vector3.up);
				fireProjectileInfo.owner = base.gameObject;
				fireProjectileInfo.damage = this.damageStat;
				fireProjectileInfo.crit = base.characterBody.RollCrit();
				ProjectileManager.instance.FireProjectile(fireProjectileInfo);
			}
		}

		// Token: 0x060005C2 RID: 1474 RVA: 0x00018843 File Offset: 0x00016A43
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

		// Token: 0x060005C3 RID: 1475 RVA: 0x0001886B File Offset: 0x00016A6B
		public override void OnExit()
		{
			base.DestroyModel();
			base.OnExit();
		}

		// Token: 0x040006C3 RID: 1731
		public static GameObject deathBombProjectile;

		// Token: 0x040006C4 RID: 1732
		public static float duration;

		// Token: 0x040006C5 RID: 1733
		public static string muzzleName;

		// Token: 0x040006C6 RID: 1734
		private Transform muzzleTransform;
	}
}
