using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace RoR2.Stats
{
	// Token: 0x02000AB9 RID: 2745
	public class PerStageStatDef
	{
		// Token: 0x06003F16 RID: 16150 RVA: 0x001045A0 File Offset: 0x001027A0
		public static void RegisterStatDefs()
		{
			foreach (PerStageStatDef perStageStatDef in PerStageStatDef.instancesList)
			{
				foreach (string text in SceneCatalog.allBaseSceneNames)
				{
					StatDef value = StatDef.Register(perStageStatDef.prefix + "." + text, perStageStatDef.recordType, perStageStatDef.dataType, 0.0, perStageStatDef.displayValueFormatter);
					perStageStatDef.keyToStatDef[text] = value;
				}
			}
		}

		// Token: 0x06003F17 RID: 16151 RVA: 0x00104670 File Offset: 0x00102870
		private PerStageStatDef(string prefix, StatRecordType recordType, StatDataType dataType, StatDef.DisplayValueFormatterDelegate displayValueFormatter)
		{
			this.prefix = prefix;
			this.recordType = recordType;
			this.dataType = dataType;
			this.displayValueFormatter = (displayValueFormatter ?? new StatDef.DisplayValueFormatterDelegate(StatDef.DefaultDisplayValueFormatter));
		}

		// Token: 0x06003F18 RID: 16152 RVA: 0x001046B0 File Offset: 0x001028B0
		[NotNull]
		private static PerStageStatDef Register(string prefix, StatRecordType recordType, StatDataType dataType, StatDef.DisplayValueFormatterDelegate displayValueFormatter = null)
		{
			PerStageStatDef perStageStatDef = new PerStageStatDef(prefix, recordType, dataType, displayValueFormatter);
			PerStageStatDef.instancesList.Add(perStageStatDef);
			return perStageStatDef;
		}

		// Token: 0x06003F19 RID: 16153 RVA: 0x001046D4 File Offset: 0x001028D4
		[CanBeNull]
		public StatDef FindStatDef(string key)
		{
			StatDef result;
			if (this.keyToStatDef.TryGetValue(key, out result))
			{
				return result;
			}
			return null;
		}

		// Token: 0x04003D9C RID: 15772
		private readonly string prefix;

		// Token: 0x04003D9D RID: 15773
		private readonly StatRecordType recordType;

		// Token: 0x04003D9E RID: 15774
		private readonly StatDataType dataType;

		// Token: 0x04003D9F RID: 15775
		private readonly Dictionary<string, StatDef> keyToStatDef = new Dictionary<string, StatDef>();

		// Token: 0x04003DA0 RID: 15776
		private StatDef.DisplayValueFormatterDelegate displayValueFormatter;

		// Token: 0x04003DA1 RID: 15777
		private static readonly List<PerStageStatDef> instancesList = new List<PerStageStatDef>();

		// Token: 0x04003DA2 RID: 15778
		public static readonly PerStageStatDef totalTimesVisited = PerStageStatDef.Register("totalTimesVisited", StatRecordType.Sum, StatDataType.ULong, null);

		// Token: 0x04003DA3 RID: 15779
		public static readonly PerStageStatDef totalTimesCleared = PerStageStatDef.Register("totalTimesCleared", StatRecordType.Sum, StatDataType.ULong, null);
	}
}
