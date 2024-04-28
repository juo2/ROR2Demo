using System;

namespace RoR2.UI
{
	// Token: 0x02000DB1 RID: 3505
	public class VoidStageBatteryMissionObjectiveTracker : ObjectivePanelController.ObjectiveTracker
	{
		// Token: 0x06005048 RID: 20552 RVA: 0x0014C2B4 File Offset: 0x0014A4B4
		protected override string GenerateString()
		{
			VoidStageMissionController voidStageMissionController = (VoidStageMissionController)this.sourceDescriptor.source;
			this.numBatteriesActivated = voidStageMissionController.numBatteriesActivated;
			return string.Format(Language.GetString(voidStageMissionController.batteryObjectiveToken), this.numBatteriesActivated, voidStageMissionController.numBatteriesSpawned);
		}

		// Token: 0x06005049 RID: 20553 RVA: 0x0014C304 File Offset: 0x0014A504
		protected override bool IsDirty()
		{
			return ((VoidStageMissionController)this.sourceDescriptor.source).numBatteriesActivated != this.numBatteriesActivated;
		}

		// Token: 0x04004CEF RID: 19695
		private int numBatteriesActivated = -1;
	}
}
