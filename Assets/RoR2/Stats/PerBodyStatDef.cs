using System;
using System.Collections.Generic;
using System.Globalization;
using HG;

namespace RoR2.Stats
{
	// Token: 0x02000AB6 RID: 2742
	public class PerBodyStatDef
	{
		// Token: 0x06003F06 RID: 16134 RVA: 0x00104108 File Offset: 0x00102308
		public static void RegisterStatDefs()
		{
			foreach (PerBodyStatDef perBodyStatDef in PerBodyStatDef.instancesList)
			{
				perBodyStatDef.bodyIndexToStatDef = new StatDef[BodyCatalog.bodyCount];
				for (BodyIndex bodyIndex = (BodyIndex)0; bodyIndex < (BodyIndex)BodyCatalog.bodyCount; bodyIndex++)
				{
					string bodyName = BodyCatalog.GetBodyName(bodyIndex);
					StatDef statDef = StatDef.Register(perBodyStatDef.prefix + "." + bodyName, perBodyStatDef.recordType, perBodyStatDef.dataType, 0.0, perBodyStatDef.displayValueFormatter);
					perBodyStatDef.bodyNameToStatDefDictionary.Add(bodyName, statDef);
					perBodyStatDef.bodyNameToStatDefDictionary.Add(bodyName + "(Clone)", statDef);
					perBodyStatDef.bodyIndexToStatDef[(int)bodyIndex] = statDef;
				}
			}
		}

		// Token: 0x06003F07 RID: 16135 RVA: 0x001041E4 File Offset: 0x001023E4
		private PerBodyStatDef(string prefix, StatRecordType recordType, StatDataType dataType, StatDef.DisplayValueFormatterDelegate displayValueFormatter = null)
		{
			this.prefix = prefix;
			this.recordType = recordType;
			this.dataType = dataType;
			this.displayValueFormatter = displayValueFormatter;
			this.nameToken = "PERBODYSTATNAME_" + prefix.ToUpper(CultureInfo.InvariantCulture);
		}

		// Token: 0x06003F08 RID: 16136 RVA: 0x0010423C File Offset: 0x0010243C
		private static PerBodyStatDef Register(string prefix, StatRecordType recordType, StatDataType dataType, StatDef.DisplayValueFormatterDelegate displayValueFormatter = null)
		{
			PerBodyStatDef perBodyStatDef = new PerBodyStatDef(prefix, recordType, dataType, displayValueFormatter);
			PerBodyStatDef.instancesList.Add(perBodyStatDef);
			return perBodyStatDef;
		}

		// Token: 0x06003F09 RID: 16137 RVA: 0x00104260 File Offset: 0x00102460
		public StatDef FindStatDef(string bodyName)
		{
			StatDef result;
			this.bodyNameToStatDefDictionary.TryGetValue(bodyName, out result);
			return result;
		}

		// Token: 0x06003F0A RID: 16138 RVA: 0x0010427D File Offset: 0x0010247D
		public StatDef FindStatDef(BodyIndex bodyIndex)
		{
			return ArrayUtils.GetSafe<StatDef>(this.bodyIndexToStatDef, (int)bodyIndex);
		}

		// Token: 0x04003D73 RID: 15731
		private readonly string prefix;

		// Token: 0x04003D74 RID: 15732
		private readonly StatRecordType recordType;

		// Token: 0x04003D75 RID: 15733
		private readonly StatDataType dataType;

		// Token: 0x04003D76 RID: 15734
		private readonly StatDef.DisplayValueFormatterDelegate displayValueFormatter;

		// Token: 0x04003D77 RID: 15735
		private readonly Dictionary<string, StatDef> bodyNameToStatDefDictionary = new Dictionary<string, StatDef>();

		// Token: 0x04003D78 RID: 15736
		public readonly string nameToken;

		// Token: 0x04003D79 RID: 15737
		private StatDef[] bodyIndexToStatDef;

		// Token: 0x04003D7A RID: 15738
		private static readonly List<PerBodyStatDef> instancesList = new List<PerBodyStatDef>();

		// Token: 0x04003D7B RID: 15739
		public static readonly PerBodyStatDef totalTimeAlive = PerBodyStatDef.Register("totalTimeAlive", StatRecordType.Sum, StatDataType.Double, null);

		// Token: 0x04003D7C RID: 15740
		public static readonly PerBodyStatDef totalWins = PerBodyStatDef.Register("totalWins", StatRecordType.Sum, StatDataType.ULong, null);

		// Token: 0x04003D7D RID: 15741
		public static readonly PerBodyStatDef longestRun = PerBodyStatDef.Register("longestRun", StatRecordType.Max, StatDataType.Double, new StatDef.DisplayValueFormatterDelegate(StatDef.TimeMMSSDisplayValueFormatter));

		// Token: 0x04003D7E RID: 15742
		public static readonly PerBodyStatDef damageDealtTo = PerBodyStatDef.Register("damageDealtTo", StatRecordType.Sum, StatDataType.ULong, null);

		// Token: 0x04003D7F RID: 15743
		public static readonly PerBodyStatDef damageDealtAs = PerBodyStatDef.Register("damageDealtAs", StatRecordType.Sum, StatDataType.ULong, null);

		// Token: 0x04003D80 RID: 15744
		public static readonly PerBodyStatDef minionDamageDealtAs = PerBodyStatDef.Register("minionDamageDealtAs", StatRecordType.Sum, StatDataType.ULong, null);

		// Token: 0x04003D81 RID: 15745
		public static readonly PerBodyStatDef damageTakenFrom = PerBodyStatDef.Register("damageTakenFrom", StatRecordType.Sum, StatDataType.ULong, null);

		// Token: 0x04003D82 RID: 15746
		public static readonly PerBodyStatDef damageTakenAs = PerBodyStatDef.Register("damageTakenAs", StatRecordType.Sum, StatDataType.ULong, null);

		// Token: 0x04003D83 RID: 15747
		public static readonly PerBodyStatDef killsAgainst = PerBodyStatDef.Register("killsAgainst", StatRecordType.Sum, StatDataType.ULong, null);

		// Token: 0x04003D84 RID: 15748
		public static readonly PerBodyStatDef killsAgainstElite = PerBodyStatDef.Register("killsAgainstElite", StatRecordType.Sum, StatDataType.ULong, null);

		// Token: 0x04003D85 RID: 15749
		public static readonly PerBodyStatDef deathsFrom = PerBodyStatDef.Register("deathsFrom", StatRecordType.Sum, StatDataType.ULong, null);

		// Token: 0x04003D86 RID: 15750
		public static readonly PerBodyStatDef killsAs = PerBodyStatDef.Register("killsAs", StatRecordType.Sum, StatDataType.ULong, null);

		// Token: 0x04003D87 RID: 15751
		public static readonly PerBodyStatDef minionKillsAs = PerBodyStatDef.Register("minionKillsAs", StatRecordType.Sum, StatDataType.ULong, null);

		// Token: 0x04003D88 RID: 15752
		public static readonly PerBodyStatDef deathsAs = PerBodyStatDef.Register("deathsAs", StatRecordType.Sum, StatDataType.ULong, null);

		// Token: 0x04003D89 RID: 15753
		public static readonly PerBodyStatDef timesPicked = PerBodyStatDef.Register("timesPicked", StatRecordType.Sum, StatDataType.ULong, null);

		// Token: 0x04003D8A RID: 15754
		public static readonly PerBodyStatDef highestInfiniteTowerWaveReachedEasy = PerBodyStatDef.Register("highestInfiniteTowerWaveReachedEasy", StatRecordType.Max, StatDataType.ULong, null);

		// Token: 0x04003D8B RID: 15755
		public static readonly PerBodyStatDef highestInfiniteTowerWaveReachedNormal = PerBodyStatDef.Register("highestInfiniteTowerWaveReachedNormal", StatRecordType.Max, StatDataType.ULong, null);

		// Token: 0x04003D8C RID: 15756
		public static readonly PerBodyStatDef highestInfiniteTowerWaveReachedHard = PerBodyStatDef.Register("highestInfiniteTowerWaveReachedHard", StatRecordType.Max, StatDataType.ULong, null);
	}
}
