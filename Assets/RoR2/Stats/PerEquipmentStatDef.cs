using System;
using System.Collections.Generic;

namespace RoR2.Stats
{
	// Token: 0x02000AB8 RID: 2744
	public class PerEquipmentStatDef
	{
		// Token: 0x06003F11 RID: 16145 RVA: 0x00104424 File Offset: 0x00102624
		public static void RegisterStatDefs()
		{
			foreach (PerEquipmentStatDef perEquipmentStatDef in PerEquipmentStatDef.instancesList)
			{
				foreach (EquipmentIndex equipmentIndex in EquipmentCatalog.allEquipment)
				{
					EquipmentDef equipmentDef = EquipmentCatalog.GetEquipmentDef(equipmentIndex);
					StatDef statDef = StatDef.Register(perEquipmentStatDef.prefix + "." + equipmentDef.name, perEquipmentStatDef.recordType, perEquipmentStatDef.dataType, 0.0, perEquipmentStatDef.displayValueFormatter);
					perEquipmentStatDef.keyToStatDef[(int)equipmentIndex] = statDef;
				}
			}
		}

		// Token: 0x06003F12 RID: 16146 RVA: 0x00104500 File Offset: 0x00102700
		private PerEquipmentStatDef(string prefix, StatRecordType recordType, StatDataType dataType, StatDef.DisplayValueFormatterDelegate displayValueFormatter)
		{
			this.prefix = prefix;
			this.recordType = recordType;
			this.dataType = dataType;
			this.displayValueFormatter = (displayValueFormatter ?? new StatDef.DisplayValueFormatterDelegate(StatDef.DefaultDisplayValueFormatter));
		}

		// Token: 0x06003F13 RID: 16147 RVA: 0x00104540 File Offset: 0x00102740
		private static PerEquipmentStatDef Register(string prefix, StatRecordType recordType, StatDataType dataType, StatDef.DisplayValueFormatterDelegate displayValueFormatter = null)
		{
			PerEquipmentStatDef perEquipmentStatDef = new PerEquipmentStatDef(prefix, recordType, dataType, displayValueFormatter);
			PerEquipmentStatDef.instancesList.Add(perEquipmentStatDef);
			return perEquipmentStatDef;
		}

		// Token: 0x06003F14 RID: 16148 RVA: 0x00104563 File Offset: 0x00102763
		public StatDef FindStatDef(EquipmentIndex key)
		{
			return this.keyToStatDef[(int)key];
		}

		// Token: 0x04003D94 RID: 15764
		private readonly string prefix;

		// Token: 0x04003D95 RID: 15765
		private readonly StatRecordType recordType;

		// Token: 0x04003D96 RID: 15766
		private readonly StatDataType dataType;

		// Token: 0x04003D97 RID: 15767
		private readonly StatDef[] keyToStatDef = EquipmentCatalog.GetPerEquipmentBuffer<StatDef>();

		// Token: 0x04003D98 RID: 15768
		private StatDef.DisplayValueFormatterDelegate displayValueFormatter;

		// Token: 0x04003D99 RID: 15769
		private static readonly List<PerEquipmentStatDef> instancesList = new List<PerEquipmentStatDef>();

		// Token: 0x04003D9A RID: 15770
		public static readonly PerEquipmentStatDef totalTimeHeld = PerEquipmentStatDef.Register("totalTimeHeld", StatRecordType.Sum, StatDataType.Double, new StatDef.DisplayValueFormatterDelegate(StatDef.TimeMMSSDisplayValueFormatter));

		// Token: 0x04003D9B RID: 15771
		public static readonly PerEquipmentStatDef totalTimesFired = PerEquipmentStatDef.Register("totalTimesFired", StatRecordType.Sum, StatDataType.ULong, null);
	}
}
