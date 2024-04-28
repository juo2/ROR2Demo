using System;

namespace EntityStates.VoidRaidCrab.Leg
{
	// Token: 0x0200013B RID: 315
	public class PreStompLegRaise : BaseStompState
	{
		// Token: 0x17000069 RID: 105
		// (get) Token: 0x0600059D RID: 1437 RVA: 0x0000B4B7 File Offset: 0x000096B7
		protected override bool shouldUseWarningIndicator
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x0600059E RID: 1438 RVA: 0x0000B4B7 File Offset: 0x000096B7
		protected override bool shouldUpdateLegStompTargetPosition
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600059F RID: 1439 RVA: 0x000180F3 File Offset: 0x000162F3
		protected override void OnLifetimeExpiredAuthority()
		{
			this.outer.SetNextState(new PreStompFollowTarget
			{
				target = this.target
			});
		}
	}
}
