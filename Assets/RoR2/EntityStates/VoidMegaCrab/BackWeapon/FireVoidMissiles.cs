using System;
using RoR2;
using RoR2.Projectile;
using UnityEngine;

namespace EntityStates.VoidMegaCrab.BackWeapon
{
	// Token: 0x0200014F RID: 335
	public class FireVoidMissiles : BaseSkillState
	{
		// Token: 0x060005DB RID: 1499 RVA: 0x00018DE4 File Offset: 0x00016FE4
		public override void OnEnter()
		{
			base.OnEnter();
			this.duration = FireVoidMissiles.baseDuration / this.attackSpeedStat;
			this.durationBetweenMissiles = FireVoidMissiles.baseDurationBetweenMissiles / this.attackSpeedStat;
			base.PlayAnimation(FireVoidMissiles.animationLayerName, FireVoidMissiles.animationStateName, FireVoidMissiles.animationPlaybackRateParam, this.duration);
			Util.PlaySound(FireVoidMissiles.enterSoundString, base.gameObject);
		}

		// Token: 0x060005DC RID: 1500 RVA: 0x00018E48 File Offset: 0x00017048
		private void FireMissile()
		{
			if (FireVoidMissiles.muzzleEffectPrefab)
			{
				EffectManager.SimpleMuzzleFlash(FireVoidMissiles.muzzleEffectPrefab, base.gameObject, FireVoidMissiles.leftMuzzleString, false);
				EffectManager.SimpleMuzzleFlash(FireVoidMissiles.muzzleEffectPrefab, base.gameObject, FireVoidMissiles.rightMuzzleString, false);
			}
			if (base.isAuthority)
			{
				Transform transform = base.FindModelChild(FireVoidMissiles.leftMuzzleString);
				Transform transform2 = base.FindModelChild(FireVoidMissiles.rightMuzzleString);
				Ray aimRay = base.GetAimRay();
				if (transform != null)
				{
					aimRay = new Ray(transform.position, transform.forward);
				}
				ProjectileManager.instance.FireProjectile(FireVoidMissiles.projectilePrefab, aimRay.origin, Util.QuaternionSafeLookRotation(aimRay.direction), base.gameObject, this.damageStat * FireVoidMissiles.damageCoefficient, FireVoidMissiles.force, Util.CheckRoll(this.critStat, base.characterBody.master), DamageColorIndex.Default, null, -1f);
				if (transform2 != null)
				{
					aimRay = new Ray(transform2.position, transform2.forward);
				}
				ProjectileManager.instance.FireProjectile(FireVoidMissiles.projectilePrefab, aimRay.origin, Util.QuaternionSafeLookRotation(aimRay.direction), base.gameObject, this.damageStat * FireVoidMissiles.damageCoefficient, FireVoidMissiles.force, Util.CheckRoll(this.critStat, base.characterBody.master), DamageColorIndex.Default, null, -1f);
			}
		}

		// Token: 0x060005DD RID: 1501 RVA: 0x00018F9C File Offset: 0x0001719C
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			this.missileTimer -= Time.fixedDeltaTime;
			if (this.missileWaveCount < FireVoidMissiles.totalMissileWaveCount && this.missileTimer <= 0f)
			{
				this.missileWaveCount++;
				this.missileTimer += this.durationBetweenMissiles;
				this.FireMissile();
			}
			if (base.fixedAge >= this.duration)
			{
				this.outer.SetNextStateToMain();
			}
		}

		// Token: 0x040006EC RID: 1772
		public static float baseDuration;

		// Token: 0x040006ED RID: 1773
		public static string leftMuzzleString;

		// Token: 0x040006EE RID: 1774
		public static string rightMuzzleString;

		// Token: 0x040006EF RID: 1775
		public static GameObject muzzleEffectPrefab;

		// Token: 0x040006F0 RID: 1776
		public static GameObject projectilePrefab;

		// Token: 0x040006F1 RID: 1777
		public static int totalMissileWaveCount;

		// Token: 0x040006F2 RID: 1778
		public static float baseDurationBetweenMissiles;

		// Token: 0x040006F3 RID: 1779
		public static float damageCoefficient = 1.2f;

		// Token: 0x040006F4 RID: 1780
		public static float force = 20f;

		// Token: 0x040006F5 RID: 1781
		public static string enterSoundString;

		// Token: 0x040006F6 RID: 1782
		public static string animationLayerName = "Gesture, Additive";

		// Token: 0x040006F7 RID: 1783
		public static string animationStateName = "FireCrabCannon";

		// Token: 0x040006F8 RID: 1784
		public static string animationPlaybackRateParam = "FireCrabCannon.playbackRate";

		// Token: 0x040006F9 RID: 1785
		private float duration;

		// Token: 0x040006FA RID: 1786
		private float durationBetweenMissiles;

		// Token: 0x040006FB RID: 1787
		private float missileTimer;

		// Token: 0x040006FC RID: 1788
		private int missileWaveCount;
	}
}
