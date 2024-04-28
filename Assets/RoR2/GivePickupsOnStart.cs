using System;
using HG;
using UnityEngine;
using UnityEngine.Networking;

namespace RoR2
{
	// Token: 0x02000708 RID: 1800
	[RequireComponent(typeof(Inventory))]
	public class GivePickupsOnStart : MonoBehaviour
	{
		// Token: 0x06002523 RID: 9507 RVA: 0x0009DC10 File Offset: 0x0009BE10
		private void Start()
		{
			if (NetworkServer.active)
			{
				this.inventory = base.GetComponent<Inventory>();
				if (this.overwriteEquipment || this.inventory.currentEquipmentIndex == EquipmentIndex.None)
				{
					if (this.equipmentDef)
					{
						this.inventory.SetEquipmentIndex(this.equipmentDef.equipmentIndex);
					}
					else if (!string.IsNullOrEmpty(this.equipmentString))
					{
						this.inventory.GiveEquipmentString(this.equipmentString);
					}
				}
				foreach (GivePickupsOnStart.ItemDefInfo itemDefInfo in this.itemDefInfos)
				{
					if (itemDefInfo.itemDef)
					{
						int num = itemDefInfo.count;
						if (itemDefInfo.dontExceedCount)
						{
							num = Math.Max(itemDefInfo.count - this.inventory.GetItemCount(itemDefInfo.itemDef), 0);
						}
						if (num != 0)
						{
							this.inventory.GiveItem(itemDefInfo.itemDef, num);
						}
					}
				}
				for (int j = 0; j < this.itemInfos.Length; j++)
				{
					GivePickupsOnStart.ItemInfo itemInfo = this.itemInfos[j];
					this.inventory.GiveItemString(itemInfo.itemString, itemInfo.count);
				}
			}
		}

		// Token: 0x040028FB RID: 10491
		public EquipmentDef equipmentDef;

		// Token: 0x040028FC RID: 10492
		[ShowFieldObsolete]
		public string equipmentString;

		// Token: 0x040028FD RID: 10493
		public bool overwriteEquipment = true;

		// Token: 0x040028FE RID: 10494
		public GivePickupsOnStart.ItemDefInfo[] itemDefInfos;

		// Token: 0x040028FF RID: 10495
		[ShowFieldObsolete]
		public GivePickupsOnStart.ItemInfo[] itemInfos;

		// Token: 0x04002900 RID: 10496
		private Inventory inventory;

		// Token: 0x02000709 RID: 1801
		[Serializable]
		public struct ItemInfo
		{
			// Token: 0x04002901 RID: 10497
			public string itemString;

			// Token: 0x04002902 RID: 10498
			public int count;
		}

		// Token: 0x0200070A RID: 1802
		[Serializable]
		public struct ItemDefInfo
		{
			// Token: 0x04002903 RID: 10499
			public ItemDef itemDef;

			// Token: 0x04002904 RID: 10500
			public int count;

			// Token: 0x04002905 RID: 10501
			public bool dontExceedCount;
		}
	}
}
