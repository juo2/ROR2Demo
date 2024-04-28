using System;
using RoR2;
using UnityEngine;

namespace EntityStates.Bandit2.Weapon
{
	// Token: 0x0200047B RID: 1147
	public class Reload : BaseState
	{
		// Token: 0x1700012E RID: 302
		// (get) Token: 0x06001480 RID: 5248 RVA: 0x0005B55C File Offset: 0x0005975C
		private float duration
		{
			get
			{
				return Reload.baseDuration / this.attackSpeedStat;
			}
		}

		// Token: 0x06001481 RID: 5249 RVA: 0x0005B56C File Offset: 0x0005976C
		public override void OnEnter()
		{
			base.OnEnter();
			base.PlayAnimation("Gesture, Additive", (base.characterBody.isSprinting && base.characterMotor && base.characterMotor.isGrounded) ? "ReloadSimple" : "Reload", "Reload.playbackRate", this.duration);
			Util.PlayAttackSpeedSound(Reload.enterSoundString, base.gameObject, Reload.enterSoundPitch);
			EffectManager.SimpleMuzzleFlash(Reload.reloadEffectPrefab, base.gameObject, Reload.reloadEffectMuzzleString, false);
		}

		// Token: 0x06001482 RID: 5250 RVA: 0x0005B5F4 File Offset: 0x000597F4
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.fixedAge >= this.duration / 2f)
			{
				this.GiveStock();
			}
			if (!base.isAuthority || base.fixedAge < this.duration)
			{
				return;
			}
			if (base.skillLocator.primary.stock < base.skillLocator.primary.maxStock)
			{
				this.outer.SetNextState(new Reload());
				return;
			}
			Util.PlayAttackSpeedSound(Reload.exitSoundString, base.gameObject, Reload.exitSoundPitch);
			this.outer.SetNextStateToMain();
		}

		// Token: 0x06001483 RID: 5251 RVA: 0x0000EC55 File Offset: 0x0000CE55
		public override void OnExit()
		{
			base.OnExit();
		}

		// Token: 0x06001484 RID: 5252 RVA: 0x0005B68C File Offset: 0x0005988C
		private void GiveStock()
		{
			if (this.hasGivenStock)
			{
				return;
			}
			base.skillLocator.primary.AddOneStock();
			this.hasGivenStock = true;
		}

		// Token: 0x06001485 RID: 5253 RVA: 0x0000B4B7 File Offset: 0x000096B7
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.Skill;
		}

		// Token: 0x04001A46 RID: 6726
		public static float enterSoundPitch;

		// Token: 0x04001A47 RID: 6727
		public static float exitSoundPitch;

		// Token: 0x04001A48 RID: 6728
		public static string enterSoundString;

		// Token: 0x04001A49 RID: 6729
		public static string exitSoundString;

		// Token: 0x04001A4A RID: 6730
		public static GameObject reloadEffectPrefab;

		// Token: 0x04001A4B RID: 6731
		public static string reloadEffectMuzzleString;

		// Token: 0x04001A4C RID: 6732
		public static float baseDuration;

		// Token: 0x04001A4D RID: 6733
		private bool hasGivenStock;
	}
}
