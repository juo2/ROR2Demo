using System;
using System.Collections.Generic;

namespace RoR2.Stats
{
	// Token: 0x02000AB7 RID: 2743
	public class PerItemStatDef
	{
		// Token: 0x06003F0C RID: 16140 RVA: 0x001042BC File Offset: 0x001024BC
		public static void RegisterStatDefs()
		{
			foreach (PerItemStatDef perItemStatDef in PerItemStatDef.instancesList)
			{
				foreach (ItemIndex itemIndex in ItemCatalog.allItems)
				{
					ItemDef itemDef = ItemCatalog.GetItemDef(itemIndex);
					StatDef statDef = StatDef.Register(perItemStatDef.prefix + "." + itemDef.name, perItemStatDef.recordType, perItemStatDef.dataType, 0.0, null);
					perItemStatDef.keyToStatDef[(int)itemIndex] = statDef;
				}
			}
		}

		// Token: 0x06003F0D RID: 16141 RVA: 0x00104394 File Offset: 0x00102594
		private PerItemStatDef(string prefix, StatRecordType recordType, StatDataType dataType)
		{
			this.prefix = prefix;
			this.recordType = recordType;
			this.dataType = dataType;
		}

		// Token: 0x06003F0E RID: 16142 RVA: 0x001043BC File Offset: 0x001025BC
		private static PerItemStatDef Register(string prefix, StatRecordType recordType, StatDataType dataType)
		{
			PerItemStatDef perItemStatDef = new PerItemStatDef(prefix, recordType, dataType);
			PerItemStatDef.instancesList.Add(perItemStatDef);
			return perItemStatDef;
		}

		// Token: 0x06003F0F RID: 16143 RVA: 0x001043DE File Offset: 0x001025DE
		public StatDef FindStatDef(ItemIndex key)
		{
			return this.keyToStatDef[(int)key];
		}

		// Token: 0x04003D8D RID: 15757
		private readonly string prefix;

		// Token: 0x04003D8E RID: 15758
		private readonly StatRecordType recordType;

		// Token: 0x04003D8F RID: 15759
		private readonly StatDataType dataType;

		// Token: 0x04003D90 RID: 15760
		private readonly StatDef[] keyToStatDef = ItemCatalog.GetPerItemBuffer<StatDef>();

		// Token: 0x04003D91 RID: 15761
		private static readonly List<PerItemStatDef> instancesList = new List<PerItemStatDef>();

		// Token: 0x04003D92 RID: 15762
		public static readonly PerItemStatDef totalCollected = PerItemStatDef.Register("totalCollected", StatRecordType.Sum, StatDataType.ULong);

		// Token: 0x04003D93 RID: 15763
		public static readonly PerItemStatDef highestCollected = PerItemStatDef.Register("highestCollected", StatRecordType.Max, StatDataType.ULong);
	}
}
