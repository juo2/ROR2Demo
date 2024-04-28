using System;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x0200012B RID: 299
	public sealed class SessionSearch : Handle
	{
		// Token: 0x06000870 RID: 2160 RVA: 0x000036D3 File Offset: 0x000018D3
		public SessionSearch()
		{
		}

		// Token: 0x06000871 RID: 2161 RVA: 0x000036DB File Offset: 0x000018DB
		public SessionSearch(IntPtr innerHandle) : base(innerHandle)
		{
		}

		// Token: 0x06000872 RID: 2162 RVA: 0x00009234 File Offset: 0x00007434
		public Result CopySearchResultByIndex(SessionSearchCopySearchResultByIndexOptions options, out SessionDetails outSessionHandle)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<SessionSearchCopySearchResultByIndexOptionsInternal, SessionSearchCopySearchResultByIndexOptions>(ref zero, options);
			IntPtr zero2 = IntPtr.Zero;
			Result result = Bindings.EOS_SessionSearch_CopySearchResultByIndex(base.InnerHandle, zero, ref zero2);
			Helper.TryMarshalDispose(ref zero);
			Helper.TryMarshalGet<SessionDetails>(zero2, out outSessionHandle);
			return result;
		}

		// Token: 0x06000873 RID: 2163 RVA: 0x00009274 File Offset: 0x00007474
		public void Find(SessionSearchFindOptions options, object clientData, SessionSearchOnFindCallback completionDelegate)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<SessionSearchFindOptionsInternal, SessionSearchFindOptions>(ref zero, options);
			IntPtr zero2 = IntPtr.Zero;
			SessionSearchOnFindCallbackInternal sessionSearchOnFindCallbackInternal = new SessionSearchOnFindCallbackInternal(SessionSearch.OnFindCallbackInternalImplementation);
			Helper.AddCallback(ref zero2, clientData, completionDelegate, sessionSearchOnFindCallbackInternal, Array.Empty<Delegate>());
			Bindings.EOS_SessionSearch_Find(base.InnerHandle, zero, zero2, sessionSearchOnFindCallbackInternal);
			Helper.TryMarshalDispose(ref zero);
		}

		// Token: 0x06000874 RID: 2164 RVA: 0x000092C8 File Offset: 0x000074C8
		public uint GetSearchResultCount(SessionSearchGetSearchResultCountOptions options)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<SessionSearchGetSearchResultCountOptionsInternal, SessionSearchGetSearchResultCountOptions>(ref zero, options);
			uint result = Bindings.EOS_SessionSearch_GetSearchResultCount(base.InnerHandle, zero);
			Helper.TryMarshalDispose(ref zero);
			return result;
		}

		// Token: 0x06000875 RID: 2165 RVA: 0x000092F8 File Offset: 0x000074F8
		public void Release()
		{
			Bindings.EOS_SessionSearch_Release(base.InnerHandle);
		}

		// Token: 0x06000876 RID: 2166 RVA: 0x00009308 File Offset: 0x00007508
		public Result RemoveParameter(SessionSearchRemoveParameterOptions options)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<SessionSearchRemoveParameterOptionsInternal, SessionSearchRemoveParameterOptions>(ref zero, options);
			Result result = Bindings.EOS_SessionSearch_RemoveParameter(base.InnerHandle, zero);
			Helper.TryMarshalDispose(ref zero);
			return result;
		}

		// Token: 0x06000877 RID: 2167 RVA: 0x00009338 File Offset: 0x00007538
		public Result SetMaxResults(SessionSearchSetMaxResultsOptions options)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<SessionSearchSetMaxResultsOptionsInternal, SessionSearchSetMaxResultsOptions>(ref zero, options);
			Result result = Bindings.EOS_SessionSearch_SetMaxResults(base.InnerHandle, zero);
			Helper.TryMarshalDispose(ref zero);
			return result;
		}

		// Token: 0x06000878 RID: 2168 RVA: 0x00009368 File Offset: 0x00007568
		public Result SetParameter(SessionSearchSetParameterOptions options)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<SessionSearchSetParameterOptionsInternal, SessionSearchSetParameterOptions>(ref zero, options);
			Result result = Bindings.EOS_SessionSearch_SetParameter(base.InnerHandle, zero);
			Helper.TryMarshalDispose(ref zero);
			return result;
		}

		// Token: 0x06000879 RID: 2169 RVA: 0x00009398 File Offset: 0x00007598
		public Result SetSessionId(SessionSearchSetSessionIdOptions options)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<SessionSearchSetSessionIdOptionsInternal, SessionSearchSetSessionIdOptions>(ref zero, options);
			Result result = Bindings.EOS_SessionSearch_SetSessionId(base.InnerHandle, zero);
			Helper.TryMarshalDispose(ref zero);
			return result;
		}

		// Token: 0x0600087A RID: 2170 RVA: 0x000093C8 File Offset: 0x000075C8
		public Result SetTargetUserId(SessionSearchSetTargetUserIdOptions options)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<SessionSearchSetTargetUserIdOptionsInternal, SessionSearchSetTargetUserIdOptions>(ref zero, options);
			Result result = Bindings.EOS_SessionSearch_SetTargetUserId(base.InnerHandle, zero);
			Helper.TryMarshalDispose(ref zero);
			return result;
		}

		// Token: 0x0600087B RID: 2171 RVA: 0x000093F8 File Offset: 0x000075F8
		[MonoPInvokeCallback(typeof(SessionSearchOnFindCallbackInternal))]
		internal static void OnFindCallbackInternalImplementation(IntPtr data)
		{
			SessionSearchOnFindCallback sessionSearchOnFindCallback;
			SessionSearchFindCallbackInfo data2;
			if (Helper.TryGetAndRemoveCallback<SessionSearchOnFindCallback, SessionSearchFindCallbackInfoInternal, SessionSearchFindCallbackInfo>(data, out sessionSearchOnFindCallback, out data2))
			{
				sessionSearchOnFindCallback(data2);
			}
		}

		// Token: 0x04000406 RID: 1030
		public const int SessionsearchCopysearchresultbyindexApiLatest = 1;

		// Token: 0x04000407 RID: 1031
		public const int SessionsearchFindApiLatest = 2;

		// Token: 0x04000408 RID: 1032
		public const int SessionsearchGetsearchresultcountApiLatest = 1;

		// Token: 0x04000409 RID: 1033
		public const int SessionsearchRemoveparameterApiLatest = 1;

		// Token: 0x0400040A RID: 1034
		public const int SessionsearchSetmaxsearchresultsApiLatest = 1;

		// Token: 0x0400040B RID: 1035
		public const int SessionsearchSetparameterApiLatest = 1;

		// Token: 0x0400040C RID: 1036
		public const int SessionsearchSetsessionidApiLatest = 1;

		// Token: 0x0400040D RID: 1037
		public const int SessionsearchSettargetuseridApiLatest = 1;
	}
}
