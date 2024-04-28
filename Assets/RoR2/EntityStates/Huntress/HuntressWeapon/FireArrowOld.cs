using System;
using RoR2;
using RoR2.Projectile;
using UnityEngine;

namespace EntityStates.Huntress.HuntressWeapon
{
	// Token: 0x02000320 RID: 800
	public class FireArrowOld : BaseState
	{
		// Token: 0x06000E4D RID: 3661 RVA: 0x0003DB38 File Offset: 0x0003BD38
		public override void OnEnter()
		{
			base.OnEnter();
			this.duration = FireArrowOld.baseDuration / this.attackSpeedStat;
			Ray aimRay = base.GetAimRay();
			base.StartAimMode(aimRay, 2f, false);
			string muzzleName = "Muzzle";
			if (FireArrowOld.effectPrefab)
			{
				EffectManager.SimpleMuzzleFlash(FireArrowOld.effectPrefab, base.gameObject, muzzleName, false);
			}
			Animator modelAnimator = base.GetModelAnimator();
			if (modelAnimator)
			{
				int layerIndex = modelAnimator.GetLayerIndex("Gesture");
				modelAnimator.SetFloat("FireArrow.playbackRate", this.attackSpeedStat);
				modelAnimator.PlayInFixedTime("FireArrow", layerIndex, 0f);
				modelAnimator.Update(0f);
				if (base.isAuthority)
				{
					ProjectileManager.instance.FireProjectile(FireArrowOld.projectilePrefab, aimRay.origin, Util.QuaternionSafeLookRotation(aimRay.direction), base.gameObject, this.damageStat * FireArrowOld.damageCoefficient, FireArrowOld.force, Util.CheckRoll(this.critStat, base.characterBody.master), DamageColorIndex.Default, null, -1f);
				}
			}
		}

		// Token: 0x06000E4E RID: 3662 RVA: 0x0000EC55 File Offset: 0x0000CE55
		public override void OnExit()
		{
			base.OnExit();
		}

		// Token: 0x06000E4F RID: 3663 RVA: 0x0003DC44 File Offset: 0x0003BE44
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.fixedAge >= this.duration && base.isAuthority)
			{
				this.outer.SetNextStateToMain();
				return;
			}
		}

		// Token: 0x06000E50 RID: 3664 RVA: 0x0000B4B7 File Offset: 0x000096B7
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.Skill;
		}

		// Token: 0x040011E6 RID: 4582
		public static GameObject projectilePrefab;

		// Token: 0x040011E7 RID: 4583
		public static GameObject effectPrefab;

		// Token: 0x040011E8 RID: 4584
		public static float baseDuration = 2f;

		// Token: 0x040011E9 RID: 4585
		public static float damageCoefficient = 1.2f;

		// Token: 0x040011EA RID: 4586
		public static float force = 20f;

		// Token: 0x040011EB RID: 4587
		private float duration;
	}
}
