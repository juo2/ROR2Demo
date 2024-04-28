using System;

namespace EntityStates.VoidRaidCrab
{
	// Token: 0x0200011D RID: 285
	public class LegBreakStunState : BaseState
	{
		// Token: 0x06000503 RID: 1283 RVA: 0x00015A65 File Offset: 0x00013C65
		public override void OnEnter()
		{
			base.OnEnter();
			this.duration = LegBreakStunState.baseDuration;
			base.PlayAnimation(LegBreakStunState.animLayerName, LegBreakStunState.animStateName, LegBreakStunState.animPlaybackRateParamName, this.duration);
		}

		// Token: 0x06000504 RID: 1284 RVA: 0x00015A93 File Offset: 0x00013C93
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.isAuthority && base.fixedAge >= this.duration)
			{
				this.outer.SetNextStateToMain();
			}
		}

		// Token: 0x06000505 RID: 1285 RVA: 0x0000DBF3 File Offset: 0x0000BDF3
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.Frozen;
		}

		// Token: 0x040005DC RID: 1500
		public static string animLayerName;

		// Token: 0x040005DD RID: 1501
		public static string animStateName;

		// Token: 0x040005DE RID: 1502
		public static string animPlaybackRateParamName;

		// Token: 0x040005DF RID: 1503
		public static float baseDuration;

		// Token: 0x040005E0 RID: 1504
		private float duration;
	}
}
