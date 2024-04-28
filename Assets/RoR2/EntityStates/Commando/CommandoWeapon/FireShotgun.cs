using System;
using RoR2;
using UnityEngine;

namespace EntityStates.Commando.CommandoWeapon
{
	// Token: 0x020003EE RID: 1006
	public class FireShotgun : BaseState
	{
		// Token: 0x06001215 RID: 4629 RVA: 0x000504C4 File Offset: 0x0004E6C4
		public override void OnEnter()
		{
			base.OnEnter();
			base.AddRecoil(-1f * FireShotgun.recoilAmplitude, -2f * FireShotgun.recoilAmplitude, -0.5f * FireShotgun.recoilAmplitude, 0.5f * FireShotgun.recoilAmplitude);
			this.maxDuration = FireShotgun.baseMaxDuration / this.attackSpeedStat;
			this.minDuration = FireShotgun.baseMinDuration / this.attackSpeedStat;
			Ray aimRay = base.GetAimRay();
			base.StartAimMode(aimRay, 2f, false);
			Util.PlaySound(FireShotgun.attackSoundString, base.gameObject);
			base.PlayAnimation("Gesture, Additive", "FireShotgun", "FireShotgun.playbackRate", this.maxDuration * 1.1f);
			base.PlayAnimation("Gesture, Override", "FireShotgun", "FireShotgun.playbackRate", this.maxDuration * 1.1f);
			string muzzleName = "MuzzleShotgun";
			if (FireShotgun.effectPrefab)
			{
				EffectManager.SimpleMuzzleFlash(FireShotgun.effectPrefab, base.gameObject, muzzleName, false);
			}
			if (base.isAuthority)
			{
				new BulletAttack
				{
					owner = base.gameObject,
					weapon = base.gameObject,
					origin = aimRay.origin,
					aimVector = aimRay.direction,
					minSpread = 0f,
					maxSpread = base.characterBody.spreadBloomAngle,
					bulletCount = (uint)((FireShotgun.bulletCount > 0) ? FireShotgun.bulletCount : 0),
					procCoefficient = 1f / (float)FireShotgun.bulletCount,
					damage = FireShotgun.damageCoefficient * this.damageStat / (float)FireShotgun.bulletCount,
					force = FireShotgun.force,
					falloffModel = BulletAttack.FalloffModel.DefaultBullet,
					tracerEffectPrefab = FireShotgun.tracerEffectPrefab,
					muzzleName = muzzleName,
					hitEffectPrefab = FireShotgun.hitEffectPrefab,
					isCrit = Util.CheckRoll(this.critStat, base.characterBody.master),
					HitEffectNormal = false,
					radius = 0f
				}.Fire();
			}
			base.characterBody.AddSpreadBloom(FireShotgun.spreadBloomValue);
		}

		// Token: 0x06001216 RID: 4630 RVA: 0x0000EC55 File Offset: 0x0000CE55
		public override void OnExit()
		{
			base.OnExit();
		}

		// Token: 0x06001217 RID: 4631 RVA: 0x000506CC File Offset: 0x0004E8CC
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			this.buttonReleased |= !base.inputBank.skill1.down;
			if (base.fixedAge >= this.maxDuration && base.isAuthority)
			{
				this.outer.SetNextStateToMain();
				return;
			}
		}

		// Token: 0x06001218 RID: 4632 RVA: 0x00050721 File Offset: 0x0004E921
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			if (this.buttonReleased && base.fixedAge >= this.minDuration)
			{
				return InterruptPriority.Any;
			}
			return InterruptPriority.Skill;
		}

		// Token: 0x04001714 RID: 5908
		public static GameObject effectPrefab;

		// Token: 0x04001715 RID: 5909
		public static GameObject hitEffectPrefab;

		// Token: 0x04001716 RID: 5910
		public static GameObject tracerEffectPrefab;

		// Token: 0x04001717 RID: 5911
		public static float damageCoefficient;

		// Token: 0x04001718 RID: 5912
		public static float force;

		// Token: 0x04001719 RID: 5913
		public static int bulletCount;

		// Token: 0x0400171A RID: 5914
		public static float baseMaxDuration = 2f;

		// Token: 0x0400171B RID: 5915
		public static float baseMinDuration = 0.5f;

		// Token: 0x0400171C RID: 5916
		public static string attackSoundString;

		// Token: 0x0400171D RID: 5917
		public static float recoilAmplitude;

		// Token: 0x0400171E RID: 5918
		public static float spreadBloomValue = 0.3f;

		// Token: 0x0400171F RID: 5919
		private float maxDuration;

		// Token: 0x04001720 RID: 5920
		private float minDuration;

		// Token: 0x04001721 RID: 5921
		private bool buttonReleased;
	}
}
