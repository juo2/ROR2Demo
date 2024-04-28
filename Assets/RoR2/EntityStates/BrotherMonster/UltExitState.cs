using System;
using RoR2;
using UnityEngine;

namespace EntityStates.BrotherMonster
{
	// Token: 0x0200044E RID: 1102
	public class UltExitState : BaseState
	{
		// Token: 0x060013B6 RID: 5046 RVA: 0x000578B0 File Offset: 0x00055AB0
		public override void OnEnter()
		{
			base.OnEnter();
			base.PlayAnimation("Body", "UltExit", "Ult.playbackRate", UltExitState.duration);
			Util.PlaySound(UltExitState.soundString, base.gameObject);
			if (UltExitState.channelFinishEffectPrefab)
			{
				EffectManager.SimpleMuzzleFlash(UltExitState.channelFinishEffectPrefab, base.gameObject, "MuzzleUlt", false);
			}
		}

		// Token: 0x060013B7 RID: 5047 RVA: 0x00057910 File Offset: 0x00055B10
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.fixedAge > UltExitState.duration)
			{
				this.outer.SetNextStateToMain();
			}
		}

		// Token: 0x060013B8 RID: 5048 RVA: 0x00057930 File Offset: 0x00055B30
		public override void OnExit()
		{
			GenericSkill genericSkill = base.skillLocator ? base.skillLocator.special : null;
			if (genericSkill)
			{
				genericSkill.UnsetSkillOverride(this.outer, UltChannelState.replacementSkillDef, GenericSkill.SkillOverridePriority.Contextual);
			}
			base.OnExit();
		}

		// Token: 0x0400191D RID: 6429
		public static float lendInterval;

		// Token: 0x0400191E RID: 6430
		public static float duration;

		// Token: 0x0400191F RID: 6431
		public static string soundString;

		// Token: 0x04001920 RID: 6432
		public static GameObject channelFinishEffectPrefab;
	}
}
