using System;
using RoR2;

namespace EntityStates.Toolbot
{
	// Token: 0x020001A7 RID: 423
	public class ToolbotDualWieldStart : ToolbotDualWieldBase
	{
		// Token: 0x0600079E RID: 1950 RVA: 0x00020C54 File Offset: 0x0001EE54
		public override void OnEnter()
		{
			base.OnEnter();
			this.duration = ToolbotDualWieldStart.baseDuration / this.attackSpeedStat;
			base.PlayAnimation(ToolbotDualWieldStart.animLayer, ToolbotDualWieldStart.animStateName, ToolbotDualWieldStart.animPlaybackRateParam, this.duration);
			Util.PlaySound(ToolbotDualWieldStart.enterSfx, base.gameObject);
			if (base.isAuthority && base.characterMotor && !base.characterMotor.isGrounded)
			{
				base.characterMotor.disableAirControlUntilCollision = true;
			}
		}

		// Token: 0x0600079F RID: 1951 RVA: 0x00020CD3 File Offset: 0x0001EED3
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.isAuthority && base.fixedAge >= this.duration)
			{
				this.outer.SetNextState(new ToolbotDualWield
				{
					activatorSkillSlot = base.activatorSkillSlot
				});
			}
		}

		// Token: 0x04000935 RID: 2357
		public static float baseDuration;

		// Token: 0x04000936 RID: 2358
		public static string animLayer;

		// Token: 0x04000937 RID: 2359
		public static string animStateName;

		// Token: 0x04000938 RID: 2360
		public static string animPlaybackRateParam;

		// Token: 0x04000939 RID: 2361
		public static string enterSfx;

		// Token: 0x0400093A RID: 2362
		private float duration;
	}
}
