using System;
using RoR2;
using UnityEngine;

namespace EntityStates.Treebot.Weapon
{
	// Token: 0x02000183 RID: 387
	public class FireMortar : BaseState
	{
		// Token: 0x060006BF RID: 1727 RVA: 0x0001D1F8 File Offset: 0x0001B3F8
		public override void OnEnter()
		{
			base.OnEnter();
			this.duration = FireMortar.baseDuration / this.attackSpeedStat;
			base.PlayAnimation("Gesture, Additive", "FireBomb", "FireBomb.playbackRate", this.duration);
			Util.PlaySound(FireMortar.fireSoundString, base.gameObject);
			if (FireMortar.muzzleEffectPrefab)
			{
				EffectManager.SimpleMuzzleFlash(FireMortar.muzzleEffectPrefab, base.gameObject, "MuzzleNailgun", false);
			}
		}

		// Token: 0x060006C0 RID: 1728 RVA: 0x0001D26B File Offset: 0x0001B46B
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.isAuthority && base.fixedAge >= this.duration)
			{
				this.outer.SetNextStateToMain();
			}
		}

		// Token: 0x060006C1 RID: 1729 RVA: 0x0000EE13 File Offset: 0x0000D013
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.PrioritySkill;
		}

		// Token: 0x0400084F RID: 2127
		public static GameObject muzzleEffectPrefab;

		// Token: 0x04000850 RID: 2128
		public static string fireSoundString;

		// Token: 0x04000851 RID: 2129
		public static float baseDuration;

		// Token: 0x04000852 RID: 2130
		private float duration;
	}
}
