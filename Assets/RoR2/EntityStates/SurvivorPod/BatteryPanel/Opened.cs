using System;

namespace EntityStates.SurvivorPod.BatteryPanel
{
	// Token: 0x020001BF RID: 447
	public class Opened : BaseBatteryPanelState
	{
		// Token: 0x06000802 RID: 2050 RVA: 0x00021FB8 File Offset: 0x000201B8
		public override void OnEnter()
		{
			base.OnEnter();
			base.PlayPodAnimation("Additive", "OpenPanelFinished");
			base.EnablePickup();
		}
	}
}
