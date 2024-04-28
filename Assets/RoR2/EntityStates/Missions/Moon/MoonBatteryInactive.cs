using System;

namespace EntityStates.Missions.Moon
{
	// Token: 0x02000241 RID: 577
	public class MoonBatteryInactive : MoonBatteryBaseState
	{
		// Token: 0x06000A3E RID: 2622 RVA: 0x0002A81E File Offset: 0x00028A1E
		public override void OnEnter()
		{
			base.OnEnter();
			this.purchaseInteraction.SetAvailable(true);
			base.FindModelChild("InactiveFX").gameObject.SetActive(true);
		}

		// Token: 0x06000A3F RID: 2623 RVA: 0x0002A848 File Offset: 0x00028A48
		public override void OnExit()
		{
			base.FindModelChild("InactiveFX").gameObject.SetActive(false);
			this.purchaseInteraction.SetAvailable(false);
			base.OnExit();
		}
	}
}
