using System;

namespace RoR2.UI
{
	// Token: 0x02000D53 RID: 3411
	public class MoonBatteryMissionObjectiveTracker : ObjectivePanelController.ObjectiveTracker
	{
		// Token: 0x06004E35 RID: 20021 RVA: 0x00142BC0 File Offset: 0x00140DC0
		protected override string GenerateString()
		{
			MoonBatteryMissionController moonBatteryMissionController = (MoonBatteryMissionController)this.sourceDescriptor.source;
			this.numChargedBatteries = moonBatteryMissionController.numChargedBatteries;
			return string.Format(Language.GetString(moonBatteryMissionController.objectiveToken), this.numChargedBatteries, moonBatteryMissionController.numRequiredBatteries);
		}

		// Token: 0x06004E36 RID: 20022 RVA: 0x00142C10 File Offset: 0x00140E10
		protected override bool IsDirty()
		{
			return ((MoonBatteryMissionController)this.sourceDescriptor.source).numChargedBatteries != this.numChargedBatteries;
		}

		// Token: 0x04004AE1 RID: 19169
		private int numChargedBatteries = -1;
	}
}
