using System;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x02000107 RID: 263
	public sealed class SessionDetails : Handle
	{
		// Token: 0x060007A6 RID: 1958 RVA: 0x000036D3 File Offset: 0x000018D3
		public SessionDetails()
		{
		}

		// Token: 0x060007A7 RID: 1959 RVA: 0x000036DB File Offset: 0x000018DB
		public SessionDetails(IntPtr innerHandle) : base(innerHandle)
		{
		}

		// Token: 0x060007A8 RID: 1960 RVA: 0x00008444 File Offset: 0x00006644
		public Result CopyInfo(SessionDetailsCopyInfoOptions options, out SessionDetailsInfo outSessionInfo)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<SessionDetailsCopyInfoOptionsInternal, SessionDetailsCopyInfoOptions>(ref zero, options);
			IntPtr zero2 = IntPtr.Zero;
			Result result = Bindings.EOS_SessionDetails_CopyInfo(base.InnerHandle, zero, ref zero2);
			Helper.TryMarshalDispose(ref zero);
			if (Helper.TryMarshalGet<SessionDetailsInfoInternal, SessionDetailsInfo>(zero2, out outSessionInfo))
			{
				Bindings.EOS_SessionDetails_Info_Release(zero2);
			}
			return result;
		}

		// Token: 0x060007A9 RID: 1961 RVA: 0x0000848C File Offset: 0x0000668C
		public Result CopySessionAttributeByIndex(SessionDetailsCopySessionAttributeByIndexOptions options, out SessionDetailsAttribute outSessionAttribute)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<SessionDetailsCopySessionAttributeByIndexOptionsInternal, SessionDetailsCopySessionAttributeByIndexOptions>(ref zero, options);
			IntPtr zero2 = IntPtr.Zero;
			Result result = Bindings.EOS_SessionDetails_CopySessionAttributeByIndex(base.InnerHandle, zero, ref zero2);
			Helper.TryMarshalDispose(ref zero);
			if (Helper.TryMarshalGet<SessionDetailsAttributeInternal, SessionDetailsAttribute>(zero2, out outSessionAttribute))
			{
				Bindings.EOS_SessionDetails_Attribute_Release(zero2);
			}
			return result;
		}

		// Token: 0x060007AA RID: 1962 RVA: 0x000084D4 File Offset: 0x000066D4
		public Result CopySessionAttributeByKey(SessionDetailsCopySessionAttributeByKeyOptions options, out SessionDetailsAttribute outSessionAttribute)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<SessionDetailsCopySessionAttributeByKeyOptionsInternal, SessionDetailsCopySessionAttributeByKeyOptions>(ref zero, options);
			IntPtr zero2 = IntPtr.Zero;
			Result result = Bindings.EOS_SessionDetails_CopySessionAttributeByKey(base.InnerHandle, zero, ref zero2);
			Helper.TryMarshalDispose(ref zero);
			if (Helper.TryMarshalGet<SessionDetailsAttributeInternal, SessionDetailsAttribute>(zero2, out outSessionAttribute))
			{
				Bindings.EOS_SessionDetails_Attribute_Release(zero2);
			}
			return result;
		}

		// Token: 0x060007AB RID: 1963 RVA: 0x0000851C File Offset: 0x0000671C
		public uint GetSessionAttributeCount(SessionDetailsGetSessionAttributeCountOptions options)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<SessionDetailsGetSessionAttributeCountOptionsInternal, SessionDetailsGetSessionAttributeCountOptions>(ref zero, options);
			uint result = Bindings.EOS_SessionDetails_GetSessionAttributeCount(base.InnerHandle, zero);
			Helper.TryMarshalDispose(ref zero);
			return result;
		}

		// Token: 0x060007AC RID: 1964 RVA: 0x0000854C File Offset: 0x0000674C
		public void Release()
		{
			Bindings.EOS_SessionDetails_Release(base.InnerHandle);
		}

		// Token: 0x040003A4 RID: 932
		public const int SessiondetailsAttributeApiLatest = 1;

		// Token: 0x040003A5 RID: 933
		public const int SessiondetailsCopyinfoApiLatest = 1;

		// Token: 0x040003A6 RID: 934
		public const int SessiondetailsCopysessionattributebyindexApiLatest = 1;

		// Token: 0x040003A7 RID: 935
		public const int SessiondetailsCopysessionattributebykeyApiLatest = 1;

		// Token: 0x040003A8 RID: 936
		public const int SessiondetailsGetsessionattributecountApiLatest = 1;

		// Token: 0x040003A9 RID: 937
		public const int SessiondetailsInfoApiLatest = 1;

		// Token: 0x040003AA RID: 938
		public const int SessiondetailsSettingsApiLatest = 3;
	}
}
