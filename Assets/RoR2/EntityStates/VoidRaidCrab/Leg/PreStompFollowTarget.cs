using System;

namespace EntityStates.VoidRaidCrab.Leg
{
	// Token: 0x0200013C RID: 316
	public class PreStompFollowTarget : BaseStompState
	{
		// Token: 0x1700006B RID: 107
		// (get) Token: 0x060005A1 RID: 1441 RVA: 0x0000B4B7 File Offset: 0x000096B7
		protected override bool shouldUseWarningIndicator
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x060005A2 RID: 1442 RVA: 0x0000B4B7 File Offset: 0x000096B7
		protected override bool shouldUpdateLegStompTargetPosition
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060005A3 RID: 1443 RVA: 0x00018119 File Offset: 0x00016319
		protected override void OnLifetimeExpiredAuthority()
		{
			this.outer.SetNextState(new PreStompPauseBeforeStomp
			{
				target = this.target
			});
		}
	}
}
