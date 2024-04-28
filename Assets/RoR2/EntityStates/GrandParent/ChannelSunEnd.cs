using System;

namespace EntityStates.GrandParent
{
	// Token: 0x0200035A RID: 858
	public class ChannelSunEnd : ChannelSunBase
	{
		// Token: 0x06000F71 RID: 3953 RVA: 0x00043953 File Offset: 0x00041B53
		public override void OnEnter()
		{
			base.OnEnter();
			this.duration = ChannelSunEnd.baseDuration / this.attackSpeedStat;
			base.PlayAnimation(ChannelSunEnd.animLayerName, ChannelSunEnd.animStateName, ChannelSunEnd.animPlaybackRateParam, this.duration);
		}

		// Token: 0x06000F72 RID: 3954 RVA: 0x00043988 File Offset: 0x00041B88
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.isAuthority && base.fixedAge >= this.duration)
			{
				this.outer.SetNextStateToMain();
			}
		}

		// Token: 0x0400138B RID: 5003
		public static string animLayerName;

		// Token: 0x0400138C RID: 5004
		public static string animStateName;

		// Token: 0x0400138D RID: 5005
		public static string animPlaybackRateParam;

		// Token: 0x0400138E RID: 5006
		public static float baseDuration;

		// Token: 0x0400138F RID: 5007
		private float duration;
	}
}
