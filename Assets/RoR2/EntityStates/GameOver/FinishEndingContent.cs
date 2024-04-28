using System;
using UnityEngine.Networking;

namespace EntityStates.GameOver
{
	// Token: 0x0200037C RID: 892
	public class FinishEndingContent : BaseGameOverControllerState
	{
		// Token: 0x06001003 RID: 4099 RVA: 0x00046DC1 File Offset: 0x00044FC1
		public override void OnEnter()
		{
			base.OnEnter();
			if (NetworkServer.active)
			{
				this.outer.SetNextState(base.gameEnding.showCredits ? new ShowCredits() : new ShowReport());
			}
		}
	}
}
