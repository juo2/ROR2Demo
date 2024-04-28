using System;
using RoR2;
using RoR2.Projectile;
using UnityEngine;

namespace EntityStates.GlobalSkills.LunarNeedle
{
	// Token: 0x02000373 RID: 883
	public class FireLunarNeedle : BaseSkillState
	{
		// Token: 0x06000FDF RID: 4063 RVA: 0x000465FC File Offset: 0x000447FC
		public override void OnEnter()
		{
			base.OnEnter();
			this.duration = FireLunarNeedle.baseDuration / this.attackSpeedStat;
			if (base.isAuthority)
			{
				Ray aimRay = base.GetAimRay();
				aimRay.direction = Util.ApplySpread(aimRay.direction, 0f, FireLunarNeedle.maxSpread, 1f, 1f, 0f, 0f);
				FireProjectileInfo fireProjectileInfo = default(FireProjectileInfo);
				fireProjectileInfo.position = aimRay.origin;
				fireProjectileInfo.rotation = Quaternion.LookRotation(aimRay.direction);
				fireProjectileInfo.crit = base.characterBody.RollCrit();
				fireProjectileInfo.damage = base.characterBody.damage * FireLunarNeedle.damageCoefficient;
				fireProjectileInfo.damageColorIndex = DamageColorIndex.Default;
				fireProjectileInfo.owner = base.gameObject;
				fireProjectileInfo.procChainMask = default(ProcChainMask);
				fireProjectileInfo.force = 0f;
				fireProjectileInfo.useFuseOverride = false;
				fireProjectileInfo.useSpeedOverride = false;
				fireProjectileInfo.target = null;
				fireProjectileInfo.projectilePrefab = FireLunarNeedle.projectilePrefab;
				ProjectileManager.instance.FireProjectile(fireProjectileInfo);
			}
			base.AddRecoil(-0.4f * FireLunarNeedle.recoilAmplitude, -0.8f * FireLunarNeedle.recoilAmplitude, -0.3f * FireLunarNeedle.recoilAmplitude, 0.3f * FireLunarNeedle.recoilAmplitude);
			base.characterBody.AddSpreadBloom(FireLunarNeedle.spreadBloomValue);
			base.StartAimMode(2f, false);
			EffectManager.SimpleMuzzleFlash(FireLunarNeedle.muzzleFlashEffectPrefab, base.gameObject, "Head", false);
			Util.PlaySound(FireLunarNeedle.fireSound, base.gameObject);
			base.PlayAnimation(this.animationLayerName, this.animationStateName, this.playbackRateParam, this.duration);
		}

		// Token: 0x06000FE0 RID: 4064 RVA: 0x000467A7 File Offset: 0x000449A7
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.isAuthority && base.fixedAge >= this.duration)
			{
				this.outer.SetNextStateToMain();
			}
		}

		// Token: 0x06000FE1 RID: 4065 RVA: 0x0000B4B7 File Offset: 0x000096B7
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.Skill;
		}

		// Token: 0x0400145D RID: 5213
		public static float baseDuration;

		// Token: 0x0400145E RID: 5214
		public static float damageCoefficient;

		// Token: 0x0400145F RID: 5215
		public static GameObject projectilePrefab;

		// Token: 0x04001460 RID: 5216
		public static float recoilAmplitude;

		// Token: 0x04001461 RID: 5217
		public static float spreadBloomValue;

		// Token: 0x04001462 RID: 5218
		public static GameObject muzzleFlashEffectPrefab;

		// Token: 0x04001463 RID: 5219
		public static string fireSound;

		// Token: 0x04001464 RID: 5220
		public static float maxSpread;

		// Token: 0x04001465 RID: 5221
		private float duration;

		// Token: 0x04001466 RID: 5222
		[SerializeField]
		public string animationLayerName = "Gesture, Override";

		// Token: 0x04001467 RID: 5223
		[SerializeField]
		public string animationStateName = "FireLunarNeedle";

		// Token: 0x04001468 RID: 5224
		[SerializeField]
		public string playbackRateParam = "FireLunarNeedle.playbackRate";
	}
}
