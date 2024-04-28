using System;
using RoR2;
using UnityEngine.Networking;

namespace EntityStates.DeepVoidPortalBattery
{
	// Token: 0x020003D7 RID: 983
	public class Charged : BaseDeepVoidPortalBatteryState
	{
		// Token: 0x06001192 RID: 4498 RVA: 0x0004D73B File Offset: 0x0004B93B
		public override void OnEnter()
		{
			base.OnEnter();
			if (VoidStageMissionController.instance && NetworkServer.active)
			{
				VoidStageMissionController.instance.OnBatteryActivated();
			}
		}
	}
}
