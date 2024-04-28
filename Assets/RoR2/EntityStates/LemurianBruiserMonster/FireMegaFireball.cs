using System;
using RoR2;
using RoR2.Projectile;
using UnityEngine;

namespace EntityStates.LemurianBruiserMonster
{
	// Token: 0x020002CF RID: 719
	public class FireMegaFireball : BaseState
	{
		// Token: 0x06000CC6 RID: 3270 RVA: 0x00035B84 File Offset: 0x00033D84
		public override void OnEnter()
		{
			base.OnEnter();
			this.duration = FireMegaFireball.baseDuration / this.attackSpeedStat;
			this.fireDuration = FireMegaFireball.baseFireDuration / this.attackSpeedStat;
			base.PlayAnimation("Gesture, Additive", "FireMegaFireball", "FireMegaFireball.playbackRate", this.duration);
			Util.PlaySound(FireMegaFireball.attackString, base.gameObject);
		}

		// Token: 0x06000CC7 RID: 3271 RVA: 0x0000EC55 File Offset: 0x0000CE55
		public override void OnExit()
		{
			base.OnExit();
		}

		// Token: 0x06000CC8 RID: 3272 RVA: 0x00035BE8 File Offset: 0x00033DE8
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			string muzzleName = "MuzzleMouth";
			if (base.isAuthority)
			{
				int num = Mathf.FloorToInt(base.fixedAge / this.fireDuration * (float)FireMegaFireball.projectileCount);
				if (this.projectilesFired <= num && this.projectilesFired < FireMegaFireball.projectileCount)
				{
					if (FireMegaFireball.muzzleflashEffectPrefab)
					{
						EffectManager.SimpleMuzzleFlash(FireMegaFireball.muzzleflashEffectPrefab, base.gameObject, muzzleName, false);
					}
					Ray aimRay = base.GetAimRay();
					float speedOverride = FireMegaFireball.projectileSpeed;
					float bonusYaw = (float)Mathf.FloorToInt((float)this.projectilesFired - (float)(FireMegaFireball.projectileCount - 1) / 2f) / (float)(FireMegaFireball.projectileCount - 1) * FireMegaFireball.totalYawSpread;
					Vector3 forward = Util.ApplySpread(aimRay.direction, 0f, 0f, 1f, 1f, bonusYaw, 0f);
					ProjectileManager.instance.FireProjectile(FireMegaFireball.projectilePrefab, aimRay.origin, Util.QuaternionSafeLookRotation(forward), base.gameObject, this.damageStat * FireMegaFireball.damageCoefficient, FireMegaFireball.force, Util.CheckRoll(this.critStat, base.characterBody.master), DamageColorIndex.Default, null, speedOverride);
					this.projectilesFired++;
				}
			}
			if (base.fixedAge >= this.duration && base.isAuthority)
			{
				this.outer.SetNextStateToMain();
				return;
			}
		}

		// Token: 0x06000CC9 RID: 3273 RVA: 0x0000B4B7 File Offset: 0x000096B7
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.Skill;
		}

		// Token: 0x04000F8F RID: 3983
		public static GameObject projectilePrefab;

		// Token: 0x04000F90 RID: 3984
		public static GameObject muzzleflashEffectPrefab;

		// Token: 0x04000F91 RID: 3985
		public static int projectileCount = 3;

		// Token: 0x04000F92 RID: 3986
		public static float totalYawSpread = 5f;

		// Token: 0x04000F93 RID: 3987
		public static float baseDuration = 2f;

		// Token: 0x04000F94 RID: 3988
		public static float baseFireDuration = 0.2f;

		// Token: 0x04000F95 RID: 3989
		public static float damageCoefficient = 1.2f;

		// Token: 0x04000F96 RID: 3990
		public static float projectileSpeed;

		// Token: 0x04000F97 RID: 3991
		public static float force = 20f;

		// Token: 0x04000F98 RID: 3992
		public static string attackString;

		// Token: 0x04000F99 RID: 3993
		private float duration;

		// Token: 0x04000F9A RID: 3994
		private float fireDuration;

		// Token: 0x04000F9B RID: 3995
		private int projectilesFired;
	}
}
