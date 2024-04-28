using System;

namespace Epic.OnlineServices.Stats
{
	// Token: 0x020000A4 RID: 164
	public sealed class StatsInterface : Handle
	{
		// Token: 0x060005CC RID: 1484 RVA: 0x000036D3 File Offset: 0x000018D3
		public StatsInterface()
		{
		}

		// Token: 0x060005CD RID: 1485 RVA: 0x000036DB File Offset: 0x000018DB
		public StatsInterface(IntPtr innerHandle) : base(innerHandle)
		{
		}

		// Token: 0x060005CE RID: 1486 RVA: 0x00006BE0 File Offset: 0x00004DE0
		public Result CopyStatByIndex(CopyStatByIndexOptions options, out Stat outStat)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<CopyStatByIndexOptionsInternal, CopyStatByIndexOptions>(ref zero, options);
			IntPtr zero2 = IntPtr.Zero;
			Result result = Bindings.EOS_Stats_CopyStatByIndex(base.InnerHandle, zero, ref zero2);
			Helper.TryMarshalDispose(ref zero);
			if (Helper.TryMarshalGet<StatInternal, Stat>(zero2, out outStat))
			{
				Bindings.EOS_Stats_Stat_Release(zero2);
			}
			return result;
		}

		// Token: 0x060005CF RID: 1487 RVA: 0x00006C28 File Offset: 0x00004E28
		public Result CopyStatByName(CopyStatByNameOptions options, out Stat outStat)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<CopyStatByNameOptionsInternal, CopyStatByNameOptions>(ref zero, options);
			IntPtr zero2 = IntPtr.Zero;
			Result result = Bindings.EOS_Stats_CopyStatByName(base.InnerHandle, zero, ref zero2);
			Helper.TryMarshalDispose(ref zero);
			if (Helper.TryMarshalGet<StatInternal, Stat>(zero2, out outStat))
			{
				Bindings.EOS_Stats_Stat_Release(zero2);
			}
			return result;
		}

		// Token: 0x060005D0 RID: 1488 RVA: 0x00006C70 File Offset: 0x00004E70
		public uint GetStatsCount(GetStatCountOptions options)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<GetStatCountOptionsInternal, GetStatCountOptions>(ref zero, options);
			uint result = Bindings.EOS_Stats_GetStatsCount(base.InnerHandle, zero);
			Helper.TryMarshalDispose(ref zero);
			return result;
		}

		// Token: 0x060005D1 RID: 1489 RVA: 0x00006CA0 File Offset: 0x00004EA0
		public void IngestStat(IngestStatOptions options, object clientData, OnIngestStatCompleteCallback completionDelegate)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<IngestStatOptionsInternal, IngestStatOptions>(ref zero, options);
			IntPtr zero2 = IntPtr.Zero;
			OnIngestStatCompleteCallbackInternal onIngestStatCompleteCallbackInternal = new OnIngestStatCompleteCallbackInternal(StatsInterface.OnIngestStatCompleteCallbackInternalImplementation);
			Helper.AddCallback(ref zero2, clientData, completionDelegate, onIngestStatCompleteCallbackInternal, Array.Empty<Delegate>());
			Bindings.EOS_Stats_IngestStat(base.InnerHandle, zero, zero2, onIngestStatCompleteCallbackInternal);
			Helper.TryMarshalDispose(ref zero);
		}

		// Token: 0x060005D2 RID: 1490 RVA: 0x00006CF4 File Offset: 0x00004EF4
		public void QueryStats(QueryStatsOptions options, object clientData, OnQueryStatsCompleteCallback completionDelegate)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<QueryStatsOptionsInternal, QueryStatsOptions>(ref zero, options);
			IntPtr zero2 = IntPtr.Zero;
			OnQueryStatsCompleteCallbackInternal onQueryStatsCompleteCallbackInternal = new OnQueryStatsCompleteCallbackInternal(StatsInterface.OnQueryStatsCompleteCallbackInternalImplementation);
			Helper.AddCallback(ref zero2, clientData, completionDelegate, onQueryStatsCompleteCallbackInternal, Array.Empty<Delegate>());
			Bindings.EOS_Stats_QueryStats(base.InnerHandle, zero, zero2, onQueryStatsCompleteCallbackInternal);
			Helper.TryMarshalDispose(ref zero);
		}

		// Token: 0x060005D3 RID: 1491 RVA: 0x00006D48 File Offset: 0x00004F48
		[MonoPInvokeCallback(typeof(OnIngestStatCompleteCallbackInternal))]
		internal static void OnIngestStatCompleteCallbackInternalImplementation(IntPtr data)
		{
			OnIngestStatCompleteCallback onIngestStatCompleteCallback;
			IngestStatCompleteCallbackInfo data2;
			if (Helper.TryGetAndRemoveCallback<OnIngestStatCompleteCallback, IngestStatCompleteCallbackInfoInternal, IngestStatCompleteCallbackInfo>(data, out onIngestStatCompleteCallback, out data2))
			{
				onIngestStatCompleteCallback(data2);
			}
		}

		// Token: 0x060005D4 RID: 1492 RVA: 0x00006D68 File Offset: 0x00004F68
		[MonoPInvokeCallback(typeof(OnQueryStatsCompleteCallbackInternal))]
		internal static void OnQueryStatsCompleteCallbackInternalImplementation(IntPtr data)
		{
			OnQueryStatsCompleteCallback onQueryStatsCompleteCallback;
			OnQueryStatsCompleteCallbackInfo data2;
			if (Helper.TryGetAndRemoveCallback<OnQueryStatsCompleteCallback, OnQueryStatsCompleteCallbackInfoInternal, OnQueryStatsCompleteCallbackInfo>(data, out onQueryStatsCompleteCallback, out data2))
			{
				onQueryStatsCompleteCallback(data2);
			}
		}

		// Token: 0x040002E9 RID: 745
		public const int CopystatbyindexApiLatest = 1;

		// Token: 0x040002EA RID: 746
		public const int CopystatbynameApiLatest = 1;

		// Token: 0x040002EB RID: 747
		public const int GetstatcountApiLatest = 1;

		// Token: 0x040002EC RID: 748
		public const int GetstatscountApiLatest = 1;

		// Token: 0x040002ED RID: 749
		public const int IngestdataApiLatest = 1;

		// Token: 0x040002EE RID: 750
		public const int IngeststatApiLatest = 3;

		// Token: 0x040002EF RID: 751
		public const int MaxIngestStats = 3000;

		// Token: 0x040002F0 RID: 752
		public const int MaxQueryStats = 1000;

		// Token: 0x040002F1 RID: 753
		public const int QuerystatsApiLatest = 3;

		// Token: 0x040002F2 RID: 754
		public const int StatApiLatest = 1;

		// Token: 0x040002F3 RID: 755
		public const int TimeUndefined = -1;
	}
}
