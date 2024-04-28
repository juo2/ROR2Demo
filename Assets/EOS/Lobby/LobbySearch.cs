using System;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x0200035C RID: 860
	public sealed class LobbySearch : Handle
	{
		// Token: 0x0600155D RID: 5469 RVA: 0x000036D3 File Offset: 0x000018D3
		public LobbySearch()
		{
		}

		// Token: 0x0600155E RID: 5470 RVA: 0x000036DB File Offset: 0x000018DB
		public LobbySearch(IntPtr innerHandle) : base(innerHandle)
		{
		}

		// Token: 0x0600155F RID: 5471 RVA: 0x00017368 File Offset: 0x00015568
		public Result CopySearchResultByIndex(LobbySearchCopySearchResultByIndexOptions options, out LobbyDetails outLobbyDetailsHandle)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<LobbySearchCopySearchResultByIndexOptionsInternal, LobbySearchCopySearchResultByIndexOptions>(ref zero, options);
			IntPtr zero2 = IntPtr.Zero;
			Result result = Bindings.EOS_LobbySearch_CopySearchResultByIndex(base.InnerHandle, zero, ref zero2);
			Helper.TryMarshalDispose(ref zero);
			Helper.TryMarshalGet<LobbyDetails>(zero2, out outLobbyDetailsHandle);
			return result;
		}

		// Token: 0x06001560 RID: 5472 RVA: 0x000173A8 File Offset: 0x000155A8
		public void Find(LobbySearchFindOptions options, object clientData, LobbySearchOnFindCallback completionDelegate)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<LobbySearchFindOptionsInternal, LobbySearchFindOptions>(ref zero, options);
			IntPtr zero2 = IntPtr.Zero;
			LobbySearchOnFindCallbackInternal lobbySearchOnFindCallbackInternal = new LobbySearchOnFindCallbackInternal(LobbySearch.OnFindCallbackInternalImplementation);
			Helper.AddCallback(ref zero2, clientData, completionDelegate, lobbySearchOnFindCallbackInternal, Array.Empty<Delegate>());
			Bindings.EOS_LobbySearch_Find(base.InnerHandle, zero, zero2, lobbySearchOnFindCallbackInternal);
			Helper.TryMarshalDispose(ref zero);
		}

		// Token: 0x06001561 RID: 5473 RVA: 0x000173FC File Offset: 0x000155FC
		public uint GetSearchResultCount(LobbySearchGetSearchResultCountOptions options)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<LobbySearchGetSearchResultCountOptionsInternal, LobbySearchGetSearchResultCountOptions>(ref zero, options);
			uint result = Bindings.EOS_LobbySearch_GetSearchResultCount(base.InnerHandle, zero);
			Helper.TryMarshalDispose(ref zero);
			return result;
		}

		// Token: 0x06001562 RID: 5474 RVA: 0x0001742C File Offset: 0x0001562C
		public void Release()
		{
			Bindings.EOS_LobbySearch_Release(base.InnerHandle);
		}

		// Token: 0x06001563 RID: 5475 RVA: 0x0001743C File Offset: 0x0001563C
		public Result RemoveParameter(LobbySearchRemoveParameterOptions options)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<LobbySearchRemoveParameterOptionsInternal, LobbySearchRemoveParameterOptions>(ref zero, options);
			Result result = Bindings.EOS_LobbySearch_RemoveParameter(base.InnerHandle, zero);
			Helper.TryMarshalDispose(ref zero);
			return result;
		}

		// Token: 0x06001564 RID: 5476 RVA: 0x0001746C File Offset: 0x0001566C
		public Result SetLobbyId(LobbySearchSetLobbyIdOptions options)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<LobbySearchSetLobbyIdOptionsInternal, LobbySearchSetLobbyIdOptions>(ref zero, options);
			Result result = Bindings.EOS_LobbySearch_SetLobbyId(base.InnerHandle, zero);
			Helper.TryMarshalDispose(ref zero);
			return result;
		}

		// Token: 0x06001565 RID: 5477 RVA: 0x0001749C File Offset: 0x0001569C
		public Result SetMaxResults(LobbySearchSetMaxResultsOptions options)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<LobbySearchSetMaxResultsOptionsInternal, LobbySearchSetMaxResultsOptions>(ref zero, options);
			Result result = Bindings.EOS_LobbySearch_SetMaxResults(base.InnerHandle, zero);
			Helper.TryMarshalDispose(ref zero);
			return result;
		}

		// Token: 0x06001566 RID: 5478 RVA: 0x000174CC File Offset: 0x000156CC
		public Result SetParameter(LobbySearchSetParameterOptions options)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<LobbySearchSetParameterOptionsInternal, LobbySearchSetParameterOptions>(ref zero, options);
			Result result = Bindings.EOS_LobbySearch_SetParameter(base.InnerHandle, zero);
			Helper.TryMarshalDispose(ref zero);
			return result;
		}

		// Token: 0x06001567 RID: 5479 RVA: 0x000174FC File Offset: 0x000156FC
		public Result SetTargetUserId(LobbySearchSetTargetUserIdOptions options)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<LobbySearchSetTargetUserIdOptionsInternal, LobbySearchSetTargetUserIdOptions>(ref zero, options);
			Result result = Bindings.EOS_LobbySearch_SetTargetUserId(base.InnerHandle, zero);
			Helper.TryMarshalDispose(ref zero);
			return result;
		}

		// Token: 0x06001568 RID: 5480 RVA: 0x0001752C File Offset: 0x0001572C
		[MonoPInvokeCallback(typeof(LobbySearchOnFindCallbackInternal))]
		internal static void OnFindCallbackInternalImplementation(IntPtr data)
		{
			LobbySearchOnFindCallback lobbySearchOnFindCallback;
			LobbySearchFindCallbackInfo data2;
			if (Helper.TryGetAndRemoveCallback<LobbySearchOnFindCallback, LobbySearchFindCallbackInfoInternal, LobbySearchFindCallbackInfo>(data, out lobbySearchOnFindCallback, out data2))
			{
				lobbySearchOnFindCallback(data2);
			}
		}

		// Token: 0x04000A4B RID: 2635
		public const int LobbysearchCopysearchresultbyindexApiLatest = 1;

		// Token: 0x04000A4C RID: 2636
		public const int LobbysearchFindApiLatest = 1;

		// Token: 0x04000A4D RID: 2637
		public const int LobbysearchGetsearchresultcountApiLatest = 1;

		// Token: 0x04000A4E RID: 2638
		public const int LobbysearchRemoveparameterApiLatest = 1;

		// Token: 0x04000A4F RID: 2639
		public const int LobbysearchSetlobbyidApiLatest = 1;

		// Token: 0x04000A50 RID: 2640
		public const int LobbysearchSetmaxresultsApiLatest = 1;

		// Token: 0x04000A51 RID: 2641
		public const int LobbysearchSetparameterApiLatest = 1;

		// Token: 0x04000A52 RID: 2642
		public const int LobbysearchSettargetuseridApiLatest = 1;
	}
}
