using System;
using RoR2;

namespace EntityStates.Toolbot
{
	// Token: 0x02000194 RID: 404
	public class CooldownSpear : BaseToolbotPrimarySkillState
	{
		// Token: 0x06000715 RID: 1813 RVA: 0x0001E958 File Offset: 0x0001CB58
		public override void OnEnter()
		{
			base.OnEnter();
			this.duration = CooldownSpear.baseDuration / this.attackSpeedStat;
			if (!base.isInDualWield)
			{
				base.PlayAnimation("Gesture, Additive", "CooldownSpear", "CooldownSpear.playbackRate", this.duration);
			}
			this.soundID = Util.PlaySound(CooldownSpear.enterSoundString, base.gameObject);
		}

		// Token: 0x06000716 RID: 1814 RVA: 0x0001E9B6 File Offset: 0x0001CBB6
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.fixedAge >= this.duration)
			{
				this.outer.SetNextStateToMain();
			}
		}

		// Token: 0x06000717 RID: 1815 RVA: 0x0001E9D7 File Offset: 0x0001CBD7
		public override void OnExit()
		{
			AkSoundEngine.StopPlayingID(this.soundID);
			base.OnExit();
		}

		// Token: 0x06000718 RID: 1816 RVA: 0x0001E9EA File Offset: 0x0001CBEA
		public override void Update()
		{
			base.Update();
			base.characterBody.SetSpreadBloom(1f - base.age / this.duration, false);
		}

		// Token: 0x06000719 RID: 1817 RVA: 0x0000B4B7 File Offset: 0x000096B7
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.Skill;
		}

		// Token: 0x040008AF RID: 2223
		public static float baseDuration;

		// Token: 0x040008B0 RID: 2224
		public static string enterSoundString;

		// Token: 0x040008B1 RID: 2225
		private float duration;

		// Token: 0x040008B2 RID: 2226
		private uint soundID;
	}
}
