using System;
using System.Collections.Generic;
using System.Globalization;
using JetBrains.Annotations;

namespace RoR2.Stats
{
	// Token: 0x02000AB4 RID: 2740
	public class StatDef
	{
		// Token: 0x06003EF9 RID: 16121 RVA: 0x001038E8 File Offset: 0x00101AE8
		[CanBeNull]
		public static StatDef Find(string statName)
		{
			StatDef result;
			StatDef.nameToStatDef.TryGetValue(statName, out result);
			return result;
		}

		// Token: 0x06003EFA RID: 16122 RVA: 0x00103904 File Offset: 0x00101B04
		private StatDef(string name, StatRecordType recordType, StatDataType dataType, double pointValue, StatDef.DisplayValueFormatterDelegate displayValueFormatter)
		{
			this.name = name;
			this.recordType = recordType;
			this.dataType = dataType;
			this.pointValue = pointValue;
			this.displayValueFormatter = displayValueFormatter;
			this.displayToken = "STATNAME_" + name.ToUpper(CultureInfo.InvariantCulture);
		}

		// Token: 0x06003EFC RID: 16124 RVA: 0x00103E69 File Offset: 0x00102069
		[SystemInitializer(new Type[]
		{
			typeof(BodyCatalog),
			typeof(ItemCatalog),
			typeof(EquipmentCatalog),
			typeof(SceneCatalog)
		})]
		private static void Init()
		{
			PerBodyStatDef.RegisterStatDefs();
			PerItemStatDef.RegisterStatDefs();
			PerEquipmentStatDef.RegisterStatDefs();
			PerStageStatDef.RegisterStatDefs();
		}

		// Token: 0x06003EFD RID: 16125 RVA: 0x00103E80 File Offset: 0x00102080
		public static StatDef Register(string name, StatRecordType recordType, StatDataType dataType, double pointValue, StatDef.DisplayValueFormatterDelegate displayValueFormatter = null)
		{
			if (displayValueFormatter == null)
			{
				displayValueFormatter = new StatDef.DisplayValueFormatterDelegate(StatDef.DefaultDisplayValueFormatter);
			}
			StatDef statDef = new StatDef(name, recordType, dataType, pointValue, displayValueFormatter)
			{
				index = StatDef.allStatDefs.Count
			};
			StatDef.allStatDefs.Add(statDef);
			StatDef.nameToStatDef.Add(statDef.name, statDef);
			return statDef;
		}

		// Token: 0x06003EFE RID: 16126 RVA: 0x00103ED8 File Offset: 0x001020D8
		public static string DefaultDisplayValueFormatter(ref StatField statField)
		{
			return statField.ToLocalNumeric();
		}

		// Token: 0x06003EFF RID: 16127 RVA: 0x00103EE0 File Offset: 0x001020E0
		public static string TimeMMSSDisplayValueFormatter(ref StatField statField)
		{
			StatDataType statDataType = statField.dataType;
			ulong num;
			if (statDataType != StatDataType.ULong)
			{
				if (statDataType != StatDataType.Double)
				{
					throw new ArgumentOutOfRangeException();
				}
				num = (ulong)statField.GetDoubleValue();
			}
			else
			{
				num = statField.GetULongValue();
			}
			ulong num2 = num / 60UL;
			ulong num3 = num - num2 * 60UL;
			return string.Format("{0:00}:{1:00}", num2, num3);
		}

		// Token: 0x06003F00 RID: 16128 RVA: 0x00103F40 File Offset: 0x00102140
		public static string DistanceMarathonsDisplayValueFormatter(ref StatField statField)
		{
			StatDataType statDataType = statField.dataType;
			double num;
			if (statDataType != StatDataType.ULong)
			{
				if (statDataType != StatDataType.Double)
				{
					throw new ArgumentOutOfRangeException();
				}
				num = statField.GetDoubleValue();
			}
			else
			{
				num = statField.GetULongValue();
			}
			return string.Format(Language.GetString("STAT_VALUE_MARATHONS_FORMAT"), num * 2.3699E-05);
		}

		// Token: 0x04003D3B RID: 15675
		public static readonly List<StatDef> allStatDefs = new List<StatDef>();

		// Token: 0x04003D3C RID: 15676
		private static readonly Dictionary<string, StatDef> nameToStatDef = new Dictionary<string, StatDef>();

		// Token: 0x04003D3D RID: 15677
		public int index;

		// Token: 0x04003D3E RID: 15678
		public readonly string name;

		// Token: 0x04003D3F RID: 15679
		public readonly string displayToken;

		// Token: 0x04003D40 RID: 15680
		public readonly StatRecordType recordType;

		// Token: 0x04003D41 RID: 15681
		public readonly StatDataType dataType;

		// Token: 0x04003D42 RID: 15682
		public double pointValue;

		// Token: 0x04003D43 RID: 15683
		public readonly StatDef.DisplayValueFormatterDelegate displayValueFormatter;

		// Token: 0x04003D44 RID: 15684
		public static readonly StatDef totalGamesPlayed = StatDef.Register("totalGamesPlayed", StatRecordType.Sum, StatDataType.ULong, 0.0, null);

		// Token: 0x04003D45 RID: 15685
		public static readonly StatDef totalTimeAlive = StatDef.Register("totalTimeAlive", StatRecordType.Sum, StatDataType.Double, 1.0, new StatDef.DisplayValueFormatterDelegate(StatDef.TimeMMSSDisplayValueFormatter));

		// Token: 0x04003D46 RID: 15686
		public static readonly StatDef totalKills = StatDef.Register("totalKills", StatRecordType.Sum, StatDataType.ULong, 10.0, null);

		// Token: 0x04003D47 RID: 15687
		public static readonly StatDef totalMinionKills = StatDef.Register("totalMinionKills", StatRecordType.Sum, StatDataType.ULong, 10.0, null);

		// Token: 0x04003D48 RID: 15688
		public static readonly StatDef totalDeaths = StatDef.Register("totalDeaths", StatRecordType.Sum, StatDataType.ULong, 0.0, null);

		// Token: 0x04003D49 RID: 15689
		public static readonly StatDef totalDamageDealt = StatDef.Register("totalDamageDealt", StatRecordType.Sum, StatDataType.ULong, 0.01, null);

		// Token: 0x04003D4A RID: 15690
		public static readonly StatDef totalMinionDamageDealt = StatDef.Register("totalMinionDamageDealt", StatRecordType.Sum, StatDataType.ULong, 0.01, null);

		// Token: 0x04003D4B RID: 15691
		public static readonly StatDef totalDamageTaken = StatDef.Register("totalDamageTaken", StatRecordType.Sum, StatDataType.ULong, 0.0, null);

		// Token: 0x04003D4C RID: 15692
		public static readonly StatDef totalHealthHealed = StatDef.Register("totalHealthHealed", StatRecordType.Sum, StatDataType.ULong, 0.01, null);

		// Token: 0x04003D4D RID: 15693
		public static readonly StatDef highestDamageDealt = StatDef.Register("highestDamageDealt", StatRecordType.Max, StatDataType.ULong, 1.0, null);

		// Token: 0x04003D4E RID: 15694
		public static readonly StatDef highestLevel = StatDef.Register("highestLevel", StatRecordType.Max, StatDataType.ULong, 100.0, null);

		// Token: 0x04003D4F RID: 15695
		public static readonly StatDef goldCollected = StatDef.Register("totalGoldCollected", StatRecordType.Sum, StatDataType.ULong, 1.0, null);

		// Token: 0x04003D50 RID: 15696
		public static readonly StatDef maxGoldCollected = StatDef.Register("maxGoldCollected", StatRecordType.Max, StatDataType.ULong, 0.0, null);

		// Token: 0x04003D51 RID: 15697
		public static readonly StatDef totalDistanceTraveled = StatDef.Register("totalDistanceTraveled", StatRecordType.Sum, StatDataType.Double, 0.01, new StatDef.DisplayValueFormatterDelegate(StatDef.DistanceMarathonsDisplayValueFormatter));

		// Token: 0x04003D52 RID: 15698
		public static readonly StatDef totalItemsCollected = StatDef.Register("totalItemsCollected", StatRecordType.Sum, StatDataType.ULong, 110.0, null);

		// Token: 0x04003D53 RID: 15699
		public static readonly StatDef highestItemsCollected = StatDef.Register("highestItemsCollected", StatRecordType.Max, StatDataType.ULong, 10.0, null);

		// Token: 0x04003D54 RID: 15700
		public static readonly StatDef totalStagesCompleted = StatDef.Register("totalStagesCompleted", StatRecordType.Sum, StatDataType.ULong, 100.0, null);

		// Token: 0x04003D55 RID: 15701
		public static readonly StatDef highestStagesCompleted = StatDef.Register("highestStagesCompleted", StatRecordType.Max, StatDataType.ULong, 0.0, null);

		// Token: 0x04003D56 RID: 15702
		public static readonly StatDef highestInfiniteTowerWaveReached = StatDef.Register("highestInfiniteTowerWaveReached", StatRecordType.Max, StatDataType.ULong, 100.0, null);

		// Token: 0x04003D57 RID: 15703
		public static readonly StatDef totalPurchases = StatDef.Register("totalPurchases", StatRecordType.Sum, StatDataType.ULong, 35.0, null);

		// Token: 0x04003D58 RID: 15704
		public static readonly StatDef highestPurchases = StatDef.Register("highestPurchases", StatRecordType.Max, StatDataType.ULong, 0.0, null);

		// Token: 0x04003D59 RID: 15705
		public static readonly StatDef totalGoldPurchases = StatDef.Register("totalGoldPurchases", StatRecordType.Sum, StatDataType.ULong, 0.0, null);

		// Token: 0x04003D5A RID: 15706
		public static readonly StatDef highestGoldPurchases = StatDef.Register("highestGoldPurchases", StatRecordType.Max, StatDataType.ULong, 0.0, null);

		// Token: 0x04003D5B RID: 15707
		public static readonly StatDef totalBloodPurchases = StatDef.Register("totalBloodPurchases", StatRecordType.Sum, StatDataType.ULong, 0.0, null);

		// Token: 0x04003D5C RID: 15708
		public static readonly StatDef highestBloodPurchases = StatDef.Register("highestBloodPurchases", StatRecordType.Max, StatDataType.ULong, 0.0, null);

		// Token: 0x04003D5D RID: 15709
		public static readonly StatDef totalLunarPurchases = StatDef.Register("totalLunarPurchases", StatRecordType.Sum, StatDataType.ULong, 0.0, null);

		// Token: 0x04003D5E RID: 15710
		public static readonly StatDef highestLunarPurchases = StatDef.Register("highestLunarPurchases", StatRecordType.Max, StatDataType.ULong, 0.0, null);

		// Token: 0x04003D5F RID: 15711
		public static readonly StatDef totalTier1Purchases = StatDef.Register("totalTier1Purchases", StatRecordType.Sum, StatDataType.ULong, 0.0, null);

		// Token: 0x04003D60 RID: 15712
		public static readonly StatDef highestTier1Purchases = StatDef.Register("highestTier1Purchases", StatRecordType.Max, StatDataType.ULong, 0.0, null);

		// Token: 0x04003D61 RID: 15713
		public static readonly StatDef totalTier2Purchases = StatDef.Register("totalTier2Purchases", StatRecordType.Sum, StatDataType.ULong, 0.0, null);

		// Token: 0x04003D62 RID: 15714
		public static readonly StatDef highestTier2Purchases = StatDef.Register("highestTier2Purchases", StatRecordType.Max, StatDataType.ULong, 0.0, null);

		// Token: 0x04003D63 RID: 15715
		public static readonly StatDef totalTier3Purchases = StatDef.Register("totalTier3Purchases", StatRecordType.Sum, StatDataType.ULong, 0.0, null);

		// Token: 0x04003D64 RID: 15716
		public static readonly StatDef highestTier3Purchases = StatDef.Register("highestTier3Purchases", StatRecordType.Max, StatDataType.ULong, 0.0, null);

		// Token: 0x04003D65 RID: 15717
		public static readonly StatDef totalDronesPurchased = StatDef.Register("totalDronesPurchased", StatRecordType.Sum, StatDataType.ULong, 0.0, null);

		// Token: 0x04003D66 RID: 15718
		public static readonly StatDef totalTurretsPurchased = StatDef.Register("totalTurretsPurchased", StatRecordType.Sum, StatDataType.ULong, 0.0, null);

		// Token: 0x04003D67 RID: 15719
		public static readonly StatDef totalGreenSoupsPurchased = StatDef.Register("totalGreenSoupsPurchased", StatRecordType.Sum, StatDataType.ULong, 0.0, null);

		// Token: 0x04003D68 RID: 15720
		public static readonly StatDef totalRedSoupsPurchased = StatDef.Register("totalRedSoupsPurchased", StatRecordType.Sum, StatDataType.ULong, 0.0, null);

		// Token: 0x04003D69 RID: 15721
		public static readonly StatDef suicideHermitCrabsAchievementProgress = StatDef.Register("suicideHermitCrabsAchievementProgress", StatRecordType.Sum, StatDataType.ULong, 0.0, null);

		// Token: 0x04003D6A RID: 15722
		public static readonly StatDef firstTeleporterCompleted = StatDef.Register("firstTeleporterCompleted", StatRecordType.Sum, StatDataType.ULong, 0.0, null);

		// Token: 0x04003D6B RID: 15723
		public static readonly StatDef totalEliteKills = StatDef.Register("totalEliteKills", StatRecordType.Sum, StatDataType.ULong, 0.0, null);

		// Token: 0x04003D6C RID: 15724
		public static readonly StatDef totalBurnDeaths = StatDef.Register("totalBurnDeaths", StatRecordType.Sum, StatDataType.ULong, 0.0, null);

		// Token: 0x04003D6D RID: 15725
		public static readonly StatDef totalDeathsWhileBurning = StatDef.Register("totalDeathsWhileBurning", StatRecordType.Sum, StatDataType.ULong, 0.0, null);

		// Token: 0x04003D6E RID: 15726
		public static readonly StatDef totalTeleporterBossKillsWitnessed = StatDef.Register("totalTeleporterBossKillsWitnessed", StatRecordType.Sum, StatDataType.ULong, 0.0, null);

		// Token: 0x04003D6F RID: 15727
		public static readonly StatDef totalCrocoInfectionsInflicted = StatDef.Register("totalCrocoInfectionsInflicted", StatRecordType.Sum, StatDataType.ULong, 0.0, null);

		// Token: 0x04003D70 RID: 15728
		public static readonly StatDef totalCrocoWeakEnemyKills = StatDef.Register("totalCrocoWeakEnemyKills", StatRecordType.Sum, StatDataType.ULong, 0.0, null);

		// Token: 0x04003D71 RID: 15729
		public static readonly StatDef totalMaulingRockKills = StatDef.Register("totalMaulingRockKills", StatRecordType.Sum, StatDataType.ULong, 0.0, null);

		// Token: 0x04003D72 RID: 15730
		private static string[] bodyNames;

		// Token: 0x02000AB5 RID: 2741
		// (Invoke) Token: 0x06003F02 RID: 16130
		public delegate string DisplayValueFormatterDelegate(ref StatField statField);
	}
}
