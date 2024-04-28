using System;
using RoR2;

namespace EntityStates.Toolbot
{
	// Token: 0x020001A8 RID: 424
	public class ToolbotDualWieldEnd : ToolbotDualWieldBase
	{
		// Token: 0x060007A1 RID: 1953 RVA: 0x00020D10 File Offset: 0x0001EF10
		public override void OnEnter()
		{
			base.OnEnter();
			this.duration = ToolbotDualWieldEnd.baseDuration / this.attackSpeedStat;
			base.PlayAnimation(ToolbotDualWieldEnd.animLayer, ToolbotDualWieldEnd.animStateName, ToolbotDualWieldEnd.animPlaybackRateParam, this.duration);
			Util.PlaySound(ToolbotDualWieldEnd.enterSfx, base.gameObject);
		}

		// Token: 0x060007A2 RID: 1954 RVA: 0x00020D61 File Offset: 0x0001EF61
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.isAuthority && base.fixedAge >= this.duration)
			{
				this.outer.SetNextStateToMain();
			}
		}

		// Token: 0x060007A3 RID: 1955 RVA: 0x0000CF8A File Offset: 0x0000B18A
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.Any;
		}

		// Token: 0x0400093B RID: 2363
		public static float baseDuration;

		// Token: 0x0400093C RID: 2364
		public static string animLayer;

		// Token: 0x0400093D RID: 2365
		public static string animStateName;

		// Token: 0x0400093E RID: 2366
		public static string animPlaybackRateParam;

		// Token: 0x0400093F RID: 2367
		public static string enterSfx;

		// Token: 0x04000940 RID: 2368
		private float duration;
	}
}
