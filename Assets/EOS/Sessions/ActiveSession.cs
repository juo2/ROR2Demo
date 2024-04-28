using System;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x020000A5 RID: 165
	public sealed class ActiveSession : Handle
	{
		// Token: 0x060005D5 RID: 1493 RVA: 0x000036D3 File Offset: 0x000018D3
		public ActiveSession()
		{
		}

		// Token: 0x060005D6 RID: 1494 RVA: 0x000036DB File Offset: 0x000018DB
		public ActiveSession(IntPtr innerHandle) : base(innerHandle)
		{
		}

		// Token: 0x060005D7 RID: 1495 RVA: 0x00006D88 File Offset: 0x00004F88
		public Result CopyInfo(ActiveSessionCopyInfoOptions options, out ActiveSessionInfo outActiveSessionInfo)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<ActiveSessionCopyInfoOptionsInternal, ActiveSessionCopyInfoOptions>(ref zero, options);
			IntPtr zero2 = IntPtr.Zero;
			Result result = Bindings.EOS_ActiveSession_CopyInfo(base.InnerHandle, zero, ref zero2);
			Helper.TryMarshalDispose(ref zero);
			if (Helper.TryMarshalGet<ActiveSessionInfoInternal, ActiveSessionInfo>(zero2, out outActiveSessionInfo))
			{
				Bindings.EOS_ActiveSession_Info_Release(zero2);
			}
			return result;
		}

		// Token: 0x060005D8 RID: 1496 RVA: 0x00006DD0 File Offset: 0x00004FD0
		public ProductUserId GetRegisteredPlayerByIndex(ActiveSessionGetRegisteredPlayerByIndexOptions options)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<ActiveSessionGetRegisteredPlayerByIndexOptionsInternal, ActiveSessionGetRegisteredPlayerByIndexOptions>(ref zero, options);
			IntPtr source = Bindings.EOS_ActiveSession_GetRegisteredPlayerByIndex(base.InnerHandle, zero);
			Helper.TryMarshalDispose(ref zero);
			ProductUserId result;
			Helper.TryMarshalGet<ProductUserId>(source, out result);
			return result;
		}

		// Token: 0x060005D9 RID: 1497 RVA: 0x00006E0C File Offset: 0x0000500C
		public uint GetRegisteredPlayerCount(ActiveSessionGetRegisteredPlayerCountOptions options)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<ActiveSessionGetRegisteredPlayerCountOptionsInternal, ActiveSessionGetRegisteredPlayerCountOptions>(ref zero, options);
			uint result = Bindings.EOS_ActiveSession_GetRegisteredPlayerCount(base.InnerHandle, zero);
			Helper.TryMarshalDispose(ref zero);
			return result;
		}

		// Token: 0x060005DA RID: 1498 RVA: 0x00006E3C File Offset: 0x0000503C
		public void Release()
		{
			Bindings.EOS_ActiveSession_Release(base.InnerHandle);
		}

		// Token: 0x040002F4 RID: 756
		public const int ActivesessionCopyinfoApiLatest = 1;

		// Token: 0x040002F5 RID: 757
		public const int ActivesessionGetregisteredplayerbyindexApiLatest = 1;

		// Token: 0x040002F6 RID: 758
		public const int ActivesessionGetregisteredplayercountApiLatest = 1;

		// Token: 0x040002F7 RID: 759
		public const int ActivesessionInfoApiLatest = 1;
	}
}
