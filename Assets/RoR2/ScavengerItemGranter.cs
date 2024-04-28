using System;
using UnityEngine;

namespace RoR2
{
	// Token: 0x0200086C RID: 2156
	[RequireComponent(typeof(Inventory))]
	public class ScavengerItemGranter : MonoBehaviour
	{
		// Token: 0x06002F34 RID: 12084 RVA: 0x000C93C0 File Offset: 0x000C75C0
		private void Start()
		{
			Inventory component = base.GetComponent<Inventory>();
			foreach (ScavengerItemGranter.StackRollData stackRollData in this.stackRollDataList)
			{
				if (stackRollData.dropTable)
				{
					for (int j = 0; j < stackRollData.numRolls; j++)
					{
						PickupDef pickupDef = PickupCatalog.GetPickupDef(stackRollData.dropTable.GenerateDrop(ScavengerItemGranter.rng));
						component.GiveItem(pickupDef.itemIndex, stackRollData.stacks);
					}
				}
			}
			if (this.overwriteEquipment || component.currentEquipmentIndex == EquipmentIndex.None)
			{
				component.GiveRandomEquipment(ScavengerItemGranter.rng);
			}
		}

		// Token: 0x06002F35 RID: 12085 RVA: 0x000C9459 File Offset: 0x000C7659
		static ScavengerItemGranter()
		{
			Run.onRunStartGlobal += ScavengerItemGranter.OnRunStart;
		}

		// Token: 0x06002F36 RID: 12086 RVA: 0x000C9478 File Offset: 0x000C7678
		private static void OnRunStart(Run run)
		{
			ScavengerItemGranter.rng.ResetSeed(run.seed);
		}

		// Token: 0x0400311C RID: 12572
		public bool overwriteEquipment;

		// Token: 0x0400311D RID: 12573
		public ScavengerItemGranter.StackRollData[] stackRollDataList;

		// Token: 0x0400311E RID: 12574
		private static readonly Xoroshiro128Plus rng = new Xoroshiro128Plus(0UL);

		// Token: 0x0200086D RID: 2157
		[Serializable]
		public struct StackRollData
		{
			// Token: 0x0400311F RID: 12575
			public PickupDropTable dropTable;

			// Token: 0x04003120 RID: 12576
			public int stacks;

			// Token: 0x04003121 RID: 12577
			public int numRolls;
		}
	}
}
