using System;
using UnityEngine.Networking;

namespace EntityStates.GameOver
{
	// Token: 0x0200037E RID: 894
	public class ShowReport : BaseGameOverControllerState
	{
		// Token: 0x06001009 RID: 4105 RVA: 0x00046E76 File Offset: 0x00045076
		public override void OnEnter()
		{
			base.OnEnter();
			if (NetworkClient.active)
			{
				base.gameOverController.shouldDisplayGameEndReportPanels = true;
			}
		}

		// Token: 0x0600100A RID: 4106 RVA: 0x0000EC55 File Offset: 0x0000CE55
		public override void OnExit()
		{
			base.OnExit();
		}
	}
}
