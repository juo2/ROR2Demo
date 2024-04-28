using System;
using RoR2;
using RoR2.Projectile;
using UnityEngine;

namespace EntityStates.Treebot.Weapon
{
	// Token: 0x02000188 RID: 392
	public class FireSyringe : BaseState
	{
		// Token: 0x060006D5 RID: 1749 RVA: 0x0001DAC6 File Offset: 0x0001BCC6
		public override void OnEnter()
		{
			base.OnEnter();
			this.duration = FireSyringe.baseDuration / this.attackSpeedStat;
			this.fireDuration = FireSyringe.baseFireDuration / this.attackSpeedStat;
		}

		// Token: 0x060006D6 RID: 1750 RVA: 0x0000EC55 File Offset: 0x0000CE55
		public override void OnExit()
		{
			base.OnExit();
		}

		// Token: 0x060006D7 RID: 1751 RVA: 0x0001DAF4 File Offset: 0x0001BCF4
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			int num = Mathf.FloorToInt(base.fixedAge / this.fireDuration * (float)FireSyringe.projectileCount);
			if (this.projectilesFired <= num && this.projectilesFired < FireSyringe.projectileCount)
			{
				GameObject prefab = FireSyringe.projectilePrefab;
				string soundString = FireSyringe.attackSound;
				if (this.projectilesFired == FireSyringe.projectileCount - 1)
				{
					prefab = FireSyringe.finalProjectilePrefab;
					soundString = FireSyringe.finalAttackSound;
				}
				this.PlayAnimation("Gesture, Additive", "FireSyringe");
				Util.PlaySound(soundString, base.gameObject);
				base.characterBody.SetAimTimer(3f);
				if (FireSyringe.muzzleflashEffectPrefab)
				{
					EffectManager.SimpleMuzzleFlash(FireSyringe.muzzleflashEffectPrefab, base.gameObject, FireSyringe.muzzleName, false);
				}
				if (base.isAuthority)
				{
					Ray aimRay = base.GetAimRay();
					float bonusYaw = (float)Mathf.FloorToInt((float)this.projectilesFired - (float)(FireSyringe.projectileCount - 1) / 2f) / (float)(FireSyringe.projectileCount - 1) * FireSyringe.totalYawSpread;
					Vector3 forward = Util.ApplySpread(aimRay.direction, 0f, 0f, 1f, 1f, bonusYaw, 0f);
					ProjectileManager.instance.FireProjectile(prefab, aimRay.origin, Util.QuaternionSafeLookRotation(forward), base.gameObject, this.damageStat * FireSyringe.damageCoefficient, FireSyringe.force, Util.CheckRoll(this.critStat, base.characterBody.master), DamageColorIndex.Default, null, -1f);
				}
				this.projectilesFired++;
			}
			if (base.fixedAge >= this.duration && base.isAuthority)
			{
				this.outer.SetNextStateToMain();
				return;
			}
		}

		// Token: 0x060006D8 RID: 1752 RVA: 0x0000B4B7 File Offset: 0x000096B7
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.Skill;
		}

		// Token: 0x04000871 RID: 2161
		public static GameObject projectilePrefab;

		// Token: 0x04000872 RID: 2162
		public static GameObject finalProjectilePrefab;

		// Token: 0x04000873 RID: 2163
		public static GameObject muzzleflashEffectPrefab;

		// Token: 0x04000874 RID: 2164
		public static int projectileCount = 3;

		// Token: 0x04000875 RID: 2165
		public static float totalYawSpread = 5f;

		// Token: 0x04000876 RID: 2166
		public static float baseDuration = 2f;

		// Token: 0x04000877 RID: 2167
		public static float baseFireDuration = 0.2f;

		// Token: 0x04000878 RID: 2168
		public static float damageCoefficient = 1.2f;

		// Token: 0x04000879 RID: 2169
		public static float force = 20f;

		// Token: 0x0400087A RID: 2170
		public static string attackSound;

		// Token: 0x0400087B RID: 2171
		public static string finalAttackSound;

		// Token: 0x0400087C RID: 2172
		public static string muzzleName;

		// Token: 0x0400087D RID: 2173
		private float duration;

		// Token: 0x0400087E RID: 2174
		private float fireDuration;

		// Token: 0x0400087F RID: 2175
		private int projectilesFired;
	}
}
