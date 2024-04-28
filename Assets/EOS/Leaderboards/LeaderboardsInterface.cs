using System;

namespace Epic.OnlineServices.Leaderboards
{
	// Token: 0x020003C8 RID: 968
	public sealed class LeaderboardsInterface : Handle
	{
		// Token: 0x06001773 RID: 6003 RVA: 0x000036D3 File Offset: 0x000018D3
		public LeaderboardsInterface()
		{
		}

		// Token: 0x06001774 RID: 6004 RVA: 0x000036DB File Offset: 0x000018DB
		public LeaderboardsInterface(IntPtr innerHandle) : base(innerHandle)
		{
		}

		// Token: 0x06001775 RID: 6005 RVA: 0x00018BE8 File Offset: 0x00016DE8
		public Result CopyLeaderboardDefinitionByIndex(CopyLeaderboardDefinitionByIndexOptions options, out Definition outLeaderboardDefinition)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<CopyLeaderboardDefinitionByIndexOptionsInternal, CopyLeaderboardDefinitionByIndexOptions>(ref zero, options);
			IntPtr zero2 = IntPtr.Zero;
			Result result = Bindings.EOS_Leaderboards_CopyLeaderboardDefinitionByIndex(base.InnerHandle, zero, ref zero2);
			Helper.TryMarshalDispose(ref zero);
			if (Helper.TryMarshalGet<DefinitionInternal, Definition>(zero2, out outLeaderboardDefinition))
			{
				Bindings.EOS_Leaderboards_Definition_Release(zero2);
			}
			return result;
		}

		// Token: 0x06001776 RID: 6006 RVA: 0x00018C30 File Offset: 0x00016E30
		public Result CopyLeaderboardDefinitionByLeaderboardId(CopyLeaderboardDefinitionByLeaderboardIdOptions options, out Definition outLeaderboardDefinition)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<CopyLeaderboardDefinitionByLeaderboardIdOptionsInternal, CopyLeaderboardDefinitionByLeaderboardIdOptions>(ref zero, options);
			IntPtr zero2 = IntPtr.Zero;
			Result result = Bindings.EOS_Leaderboards_CopyLeaderboardDefinitionByLeaderboardId(base.InnerHandle, zero, ref zero2);
			Helper.TryMarshalDispose(ref zero);
			if (Helper.TryMarshalGet<DefinitionInternal, Definition>(zero2, out outLeaderboardDefinition))
			{
				Bindings.EOS_Leaderboards_Definition_Release(zero2);
			}
			return result;
		}

		// Token: 0x06001777 RID: 6007 RVA: 0x00018C78 File Offset: 0x00016E78
		public Result CopyLeaderboardRecordByIndex(CopyLeaderboardRecordByIndexOptions options, out LeaderboardRecord outLeaderboardRecord)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<CopyLeaderboardRecordByIndexOptionsInternal, CopyLeaderboardRecordByIndexOptions>(ref zero, options);
			IntPtr zero2 = IntPtr.Zero;
			Result result = Bindings.EOS_Leaderboards_CopyLeaderboardRecordByIndex(base.InnerHandle, zero, ref zero2);
			Helper.TryMarshalDispose(ref zero);
			if (Helper.TryMarshalGet<LeaderboardRecordInternal, LeaderboardRecord>(zero2, out outLeaderboardRecord))
			{
				Bindings.EOS_Leaderboards_LeaderboardRecord_Release(zero2);
			}
			return result;
		}

		// Token: 0x06001778 RID: 6008 RVA: 0x00018CC0 File Offset: 0x00016EC0
		public Result CopyLeaderboardRecordByUserId(CopyLeaderboardRecordByUserIdOptions options, out LeaderboardRecord outLeaderboardRecord)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<CopyLeaderboardRecordByUserIdOptionsInternal, CopyLeaderboardRecordByUserIdOptions>(ref zero, options);
			IntPtr zero2 = IntPtr.Zero;
			Result result = Bindings.EOS_Leaderboards_CopyLeaderboardRecordByUserId(base.InnerHandle, zero, ref zero2);
			Helper.TryMarshalDispose(ref zero);
			if (Helper.TryMarshalGet<LeaderboardRecordInternal, LeaderboardRecord>(zero2, out outLeaderboardRecord))
			{
				Bindings.EOS_Leaderboards_LeaderboardRecord_Release(zero2);
			}
			return result;
		}

		// Token: 0x06001779 RID: 6009 RVA: 0x00018D08 File Offset: 0x00016F08
		public Result CopyLeaderboardUserScoreByIndex(CopyLeaderboardUserScoreByIndexOptions options, out LeaderboardUserScore outLeaderboardUserScore)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<CopyLeaderboardUserScoreByIndexOptionsInternal, CopyLeaderboardUserScoreByIndexOptions>(ref zero, options);
			IntPtr zero2 = IntPtr.Zero;
			Result result = Bindings.EOS_Leaderboards_CopyLeaderboardUserScoreByIndex(base.InnerHandle, zero, ref zero2);
			Helper.TryMarshalDispose(ref zero);
			if (Helper.TryMarshalGet<LeaderboardUserScoreInternal, LeaderboardUserScore>(zero2, out outLeaderboardUserScore))
			{
				Bindings.EOS_Leaderboards_LeaderboardUserScore_Release(zero2);
			}
			return result;
		}

		// Token: 0x0600177A RID: 6010 RVA: 0x00018D50 File Offset: 0x00016F50
		public Result CopyLeaderboardUserScoreByUserId(CopyLeaderboardUserScoreByUserIdOptions options, out LeaderboardUserScore outLeaderboardUserScore)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<CopyLeaderboardUserScoreByUserIdOptionsInternal, CopyLeaderboardUserScoreByUserIdOptions>(ref zero, options);
			IntPtr zero2 = IntPtr.Zero;
			Result result = Bindings.EOS_Leaderboards_CopyLeaderboardUserScoreByUserId(base.InnerHandle, zero, ref zero2);
			Helper.TryMarshalDispose(ref zero);
			if (Helper.TryMarshalGet<LeaderboardUserScoreInternal, LeaderboardUserScore>(zero2, out outLeaderboardUserScore))
			{
				Bindings.EOS_Leaderboards_LeaderboardUserScore_Release(zero2);
			}
			return result;
		}

		// Token: 0x0600177B RID: 6011 RVA: 0x00018D98 File Offset: 0x00016F98
		public uint GetLeaderboardDefinitionCount(GetLeaderboardDefinitionCountOptions options)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<GetLeaderboardDefinitionCountOptionsInternal, GetLeaderboardDefinitionCountOptions>(ref zero, options);
			uint result = Bindings.EOS_Leaderboards_GetLeaderboardDefinitionCount(base.InnerHandle, zero);
			Helper.TryMarshalDispose(ref zero);
			return result;
		}

		// Token: 0x0600177C RID: 6012 RVA: 0x00018DC8 File Offset: 0x00016FC8
		public uint GetLeaderboardRecordCount(GetLeaderboardRecordCountOptions options)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<GetLeaderboardRecordCountOptionsInternal, GetLeaderboardRecordCountOptions>(ref zero, options);
			uint result = Bindings.EOS_Leaderboards_GetLeaderboardRecordCount(base.InnerHandle, zero);
			Helper.TryMarshalDispose(ref zero);
			return result;
		}

		// Token: 0x0600177D RID: 6013 RVA: 0x00018DF8 File Offset: 0x00016FF8
		public uint GetLeaderboardUserScoreCount(GetLeaderboardUserScoreCountOptions options)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<GetLeaderboardUserScoreCountOptionsInternal, GetLeaderboardUserScoreCountOptions>(ref zero, options);
			uint result = Bindings.EOS_Leaderboards_GetLeaderboardUserScoreCount(base.InnerHandle, zero);
			Helper.TryMarshalDispose(ref zero);
			return result;
		}

		// Token: 0x0600177E RID: 6014 RVA: 0x00018E28 File Offset: 0x00017028
		public void QueryLeaderboardDefinitions(QueryLeaderboardDefinitionsOptions options, object clientData, OnQueryLeaderboardDefinitionsCompleteCallback completionDelegate)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<QueryLeaderboardDefinitionsOptionsInternal, QueryLeaderboardDefinitionsOptions>(ref zero, options);
			IntPtr zero2 = IntPtr.Zero;
			OnQueryLeaderboardDefinitionsCompleteCallbackInternal onQueryLeaderboardDefinitionsCompleteCallbackInternal = new OnQueryLeaderboardDefinitionsCompleteCallbackInternal(LeaderboardsInterface.OnQueryLeaderboardDefinitionsCompleteCallbackInternalImplementation);
			Helper.AddCallback(ref zero2, clientData, completionDelegate, onQueryLeaderboardDefinitionsCompleteCallbackInternal, Array.Empty<Delegate>());
			Bindings.EOS_Leaderboards_QueryLeaderboardDefinitions(base.InnerHandle, zero, zero2, onQueryLeaderboardDefinitionsCompleteCallbackInternal);
			Helper.TryMarshalDispose(ref zero);
		}

		// Token: 0x0600177F RID: 6015 RVA: 0x00018E7C File Offset: 0x0001707C
		public void QueryLeaderboardRanks(QueryLeaderboardRanksOptions options, object clientData, OnQueryLeaderboardRanksCompleteCallback completionDelegate)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<QueryLeaderboardRanksOptionsInternal, QueryLeaderboardRanksOptions>(ref zero, options);
			IntPtr zero2 = IntPtr.Zero;
			OnQueryLeaderboardRanksCompleteCallbackInternal onQueryLeaderboardRanksCompleteCallbackInternal = new OnQueryLeaderboardRanksCompleteCallbackInternal(LeaderboardsInterface.OnQueryLeaderboardRanksCompleteCallbackInternalImplementation);
			Helper.AddCallback(ref zero2, clientData, completionDelegate, onQueryLeaderboardRanksCompleteCallbackInternal, Array.Empty<Delegate>());
			Bindings.EOS_Leaderboards_QueryLeaderboardRanks(base.InnerHandle, zero, zero2, onQueryLeaderboardRanksCompleteCallbackInternal);
			Helper.TryMarshalDispose(ref zero);
		}

		// Token: 0x06001780 RID: 6016 RVA: 0x00018ED0 File Offset: 0x000170D0
		public void QueryLeaderboardUserScores(QueryLeaderboardUserScoresOptions options, object clientData, OnQueryLeaderboardUserScoresCompleteCallback completionDelegate)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<QueryLeaderboardUserScoresOptionsInternal, QueryLeaderboardUserScoresOptions>(ref zero, options);
			IntPtr zero2 = IntPtr.Zero;
			OnQueryLeaderboardUserScoresCompleteCallbackInternal onQueryLeaderboardUserScoresCompleteCallbackInternal = new OnQueryLeaderboardUserScoresCompleteCallbackInternal(LeaderboardsInterface.OnQueryLeaderboardUserScoresCompleteCallbackInternalImplementation);
			Helper.AddCallback(ref zero2, clientData, completionDelegate, onQueryLeaderboardUserScoresCompleteCallbackInternal, Array.Empty<Delegate>());
			Bindings.EOS_Leaderboards_QueryLeaderboardUserScores(base.InnerHandle, zero, zero2, onQueryLeaderboardUserScoresCompleteCallbackInternal);
			Helper.TryMarshalDispose(ref zero);
		}

		// Token: 0x06001781 RID: 6017 RVA: 0x00018F24 File Offset: 0x00017124
		[MonoPInvokeCallback(typeof(OnQueryLeaderboardDefinitionsCompleteCallbackInternal))]
		internal static void OnQueryLeaderboardDefinitionsCompleteCallbackInternalImplementation(IntPtr data)
		{
			OnQueryLeaderboardDefinitionsCompleteCallback onQueryLeaderboardDefinitionsCompleteCallback;
			OnQueryLeaderboardDefinitionsCompleteCallbackInfo data2;
			if (Helper.TryGetAndRemoveCallback<OnQueryLeaderboardDefinitionsCompleteCallback, OnQueryLeaderboardDefinitionsCompleteCallbackInfoInternal, OnQueryLeaderboardDefinitionsCompleteCallbackInfo>(data, out onQueryLeaderboardDefinitionsCompleteCallback, out data2))
			{
				onQueryLeaderboardDefinitionsCompleteCallback(data2);
			}
		}

		// Token: 0x06001782 RID: 6018 RVA: 0x00018F44 File Offset: 0x00017144
		[MonoPInvokeCallback(typeof(OnQueryLeaderboardRanksCompleteCallbackInternal))]
		internal static void OnQueryLeaderboardRanksCompleteCallbackInternalImplementation(IntPtr data)
		{
			OnQueryLeaderboardRanksCompleteCallback onQueryLeaderboardRanksCompleteCallback;
			OnQueryLeaderboardRanksCompleteCallbackInfo data2;
			if (Helper.TryGetAndRemoveCallback<OnQueryLeaderboardRanksCompleteCallback, OnQueryLeaderboardRanksCompleteCallbackInfoInternal, OnQueryLeaderboardRanksCompleteCallbackInfo>(data, out onQueryLeaderboardRanksCompleteCallback, out data2))
			{
				onQueryLeaderboardRanksCompleteCallback(data2);
			}
		}

		// Token: 0x06001783 RID: 6019 RVA: 0x00018F64 File Offset: 0x00017164
		[MonoPInvokeCallback(typeof(OnQueryLeaderboardUserScoresCompleteCallbackInternal))]
		internal static void OnQueryLeaderboardUserScoresCompleteCallbackInternalImplementation(IntPtr data)
		{
			OnQueryLeaderboardUserScoresCompleteCallback onQueryLeaderboardUserScoresCompleteCallback;
			OnQueryLeaderboardUserScoresCompleteCallbackInfo data2;
			if (Helper.TryGetAndRemoveCallback<OnQueryLeaderboardUserScoresCompleteCallback, OnQueryLeaderboardUserScoresCompleteCallbackInfoInternal, OnQueryLeaderboardUserScoresCompleteCallbackInfo>(data, out onQueryLeaderboardUserScoresCompleteCallback, out data2))
			{
				onQueryLeaderboardUserScoresCompleteCallback(data2);
			}
		}

		// Token: 0x04000AFD RID: 2813
		public const int CopyleaderboarddefinitionbyindexApiLatest = 1;

		// Token: 0x04000AFE RID: 2814
		public const int CopyleaderboarddefinitionbyleaderboardidApiLatest = 1;

		// Token: 0x04000AFF RID: 2815
		public const int CopyleaderboardrecordbyindexApiLatest = 2;

		// Token: 0x04000B00 RID: 2816
		public const int CopyleaderboardrecordbyuseridApiLatest = 2;

		// Token: 0x04000B01 RID: 2817
		public const int CopyleaderboarduserscorebyindexApiLatest = 1;

		// Token: 0x04000B02 RID: 2818
		public const int CopyleaderboarduserscorebyuseridApiLatest = 1;

		// Token: 0x04000B03 RID: 2819
		public const int DefinitionApiLatest = 1;

		// Token: 0x04000B04 RID: 2820
		public const int GetleaderboarddefinitioncountApiLatest = 1;

		// Token: 0x04000B05 RID: 2821
		public const int GetleaderboardrecordcountApiLatest = 1;

		// Token: 0x04000B06 RID: 2822
		public const int GetleaderboarduserscorecountApiLatest = 1;

		// Token: 0x04000B07 RID: 2823
		public const int LeaderboardrecordApiLatest = 2;

		// Token: 0x04000B08 RID: 2824
		public const int LeaderboarduserscoreApiLatest = 1;

		// Token: 0x04000B09 RID: 2825
		public const int QueryleaderboarddefinitionsApiLatest = 2;

		// Token: 0x04000B0A RID: 2826
		public const int QueryleaderboardranksApiLatest = 2;

		// Token: 0x04000B0B RID: 2827
		public const int QueryleaderboarduserscoresApiLatest = 2;

		// Token: 0x04000B0C RID: 2828
		public const int TimeUndefined = -1;

		// Token: 0x04000B0D RID: 2829
		public const int UserscoresquerystatinfoApiLatest = 1;
	}
}
