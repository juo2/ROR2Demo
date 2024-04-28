using System;
using Epic.OnlineServices;
using Epic.OnlineServices.Stats;

namespace RoR2
{
	// Token: 0x020009AE RID: 2478
	public class EOSStatManager
	{
		// Token: 0x060038AA RID: 14506 RVA: 0x000EDAF9 File Offset: 0x000EBCF9
		public EOSStatManager()
		{
			EOSStatManager.Interface = EOSPlatformManager.GetPlatformInterface().GetStatsInterface();
		}

		// Token: 0x060038AB RID: 14507 RVA: 0x000EDB10 File Offset: 0x000EBD10
		public static void IngestStat(IngestData[] statsToIngest, object callbackData = null, OnIngestStatCompleteCallback callback = null)
		{
			EOSStatManager.Interface.IngestStat(new IngestStatOptions
			{
				LocalUserId = EOSLoginManager.loggedInProductId,
				Stats = statsToIngest,
				TargetUserId = EOSLoginManager.loggedInProductId
			}, callbackData, callback);
		}

		// Token: 0x060038AC RID: 14508 RVA: 0x000EDB40 File Offset: 0x000EBD40
		public static void QueryStats(QueryStatsOptions queryStatsOptions, object callbackData = null, OnQueryStatsCompleteCallback callback = null)
		{
			EOSStatManager.Interface.QueryStats(queryStatsOptions, callbackData, callback);
		}

		// Token: 0x060038AD RID: 14509 RVA: 0x000EDB50 File Offset: 0x000EBD50
		public static Stat GetStat(string statName, ProductUserId targetUserId)
		{
			Stat result;
			EOSStatManager.Interface.CopyStatByName(new CopyStatByNameOptions
			{
				Name = statName,
				TargetUserId = targetUserId
			}, out result);
			return result;
		}

		// Token: 0x060038AE RID: 14510 RVA: 0x000EDB7E File Offset: 0x000EBD7E
		public static uint GetStatsCount(ProductUserId targetUserId)
		{
			return EOSStatManager.Interface.GetStatsCount(new GetStatCountOptions
			{
				TargetUserId = targetUserId
			});
		}

		// Token: 0x0400386C RID: 14444
		private static StatsInterface Interface;

		// Token: 0x020009AF RID: 2479
		public static class StatNames
		{
			// Token: 0x0400386D RID: 14445
			public const string kFastestWeeklyRun = "FASTESTWEEKLYRUN";
		}
	}
}
