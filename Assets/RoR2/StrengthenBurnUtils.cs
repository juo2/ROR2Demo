using System;

namespace RoR2
{
	// Token: 0x02000944 RID: 2372
	public static class StrengthenBurnUtils
	{
		// Token: 0x0600359E RID: 13726 RVA: 0x000E2808 File Offset: 0x000E0A08
		public static void CheckDotForUpgrade(Inventory inventory, ref InflictDotInfo dotInfo)
		{
			if (dotInfo.dotIndex == DotController.DotIndex.Burn || dotInfo.dotIndex == DotController.DotIndex.Helfire)
			{
				int itemCount = inventory.GetItemCount(DLC1Content.Items.StrengthenBurn);
				if (itemCount > 0)
				{
					dotInfo.preUpgradeDotIndex = new DotController.DotIndex?(dotInfo.dotIndex);
					dotInfo.dotIndex = DotController.DotIndex.StrongerBurn;
					float num = (float)(1 + 3 * itemCount);
					dotInfo.damageMultiplier *= num;
					dotInfo.totalDamage *= num;
				}
			}
		}
	}
}
