using System;

namespace EntityStates.Treebot.Weapon
{
	// Token: 0x02000181 RID: 385
	public class ChargePlantSonicBoom : ChargeSonicBoom
	{
		// Token: 0x060006B6 RID: 1718 RVA: 0x0001CF26 File Offset: 0x0001B126
		protected override EntityState GetNextState()
		{
			return new FirePlantSonicBoom();
		}
	}
}
