using System;
using RoR2;
using UnityEngine;

namespace EntityStates.Toolbot
{
	// Token: 0x0200018E RID: 398
	public class RecoverAimStunDrone : BaseState
	{
		// Token: 0x060006EE RID: 1774 RVA: 0x0001E474 File Offset: 0x0001C674
		public override void OnEnter()
		{
			base.OnEnter();
			this.duration = RecoverAimStunDrone.baseDuration / this.attackSpeedStat;
			base.PlayAnimation("Gesture, Additive", "FireBomb", "FireBomb.playbackRate", this.duration);
			Util.PlaySound(RecoverAimStunDrone.fireSoundString, base.gameObject);
			if (RecoverAimStunDrone.muzzleEffectPrefab)
			{
				EffectManager.SimpleMuzzleFlash(RecoverAimStunDrone.muzzleEffectPrefab, base.gameObject, "MuzzleNailgun", false);
			}
		}

		// Token: 0x060006EF RID: 1775 RVA: 0x0001E4E7 File Offset: 0x0001C6E7
		public override void OnExit()
		{
			base.OnExit();
			base.PlayCrossfade("Stance, Override", "Empty", 0.1f);
		}

		// Token: 0x060006F0 RID: 1776 RVA: 0x0001E504 File Offset: 0x0001C704
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.isAuthority && base.fixedAge >= this.duration)
			{
				this.outer.SetNextStateToMain();
			}
		}

		// Token: 0x060006F1 RID: 1777 RVA: 0x0000B4B7 File Offset: 0x000096B7
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.Skill;
		}

		// Token: 0x0400089B RID: 2203
		public static GameObject muzzleEffectPrefab;

		// Token: 0x0400089C RID: 2204
		public static string fireSoundString;

		// Token: 0x0400089D RID: 2205
		public static float baseDuration;

		// Token: 0x0400089E RID: 2206
		private float duration;
	}
}
