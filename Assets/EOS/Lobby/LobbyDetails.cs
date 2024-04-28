using System;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x02000329 RID: 809
	public sealed class LobbyDetails : Handle
	{
		// Token: 0x06001425 RID: 5157 RVA: 0x000036D3 File Offset: 0x000018D3
		public LobbyDetails()
		{
		}

		// Token: 0x06001426 RID: 5158 RVA: 0x000036DB File Offset: 0x000018DB
		public LobbyDetails(IntPtr innerHandle) : base(innerHandle)
		{
		}

		// Token: 0x06001427 RID: 5159 RVA: 0x00015660 File Offset: 0x00013860
		public Result CopyAttributeByIndex(LobbyDetailsCopyAttributeByIndexOptions options, out Attribute outAttribute)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<LobbyDetailsCopyAttributeByIndexOptionsInternal, LobbyDetailsCopyAttributeByIndexOptions>(ref zero, options);
			IntPtr zero2 = IntPtr.Zero;
			Result result = Bindings.EOS_LobbyDetails_CopyAttributeByIndex(base.InnerHandle, zero, ref zero2);
			Helper.TryMarshalDispose(ref zero);
			if (Helper.TryMarshalGet<AttributeInternal, Attribute>(zero2, out outAttribute))
			{
				Bindings.EOS_Lobby_Attribute_Release(zero2);
			}
			return result;
		}

		// Token: 0x06001428 RID: 5160 RVA: 0x000156A8 File Offset: 0x000138A8
		public Result CopyAttributeByKey(LobbyDetailsCopyAttributeByKeyOptions options, out Attribute outAttribute)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<LobbyDetailsCopyAttributeByKeyOptionsInternal, LobbyDetailsCopyAttributeByKeyOptions>(ref zero, options);
			IntPtr zero2 = IntPtr.Zero;
			Result result = Bindings.EOS_LobbyDetails_CopyAttributeByKey(base.InnerHandle, zero, ref zero2);
			Helper.TryMarshalDispose(ref zero);
			if (Helper.TryMarshalGet<AttributeInternal, Attribute>(zero2, out outAttribute))
			{
				Bindings.EOS_Lobby_Attribute_Release(zero2);
			}
			return result;
		}

		// Token: 0x06001429 RID: 5161 RVA: 0x000156F0 File Offset: 0x000138F0
		public Result CopyInfo(LobbyDetailsCopyInfoOptions options, out LobbyDetailsInfo outLobbyDetailsInfo)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<LobbyDetailsCopyInfoOptionsInternal, LobbyDetailsCopyInfoOptions>(ref zero, options);
			IntPtr zero2 = IntPtr.Zero;
			Result result = Bindings.EOS_LobbyDetails_CopyInfo(base.InnerHandle, zero, ref zero2);
			Helper.TryMarshalDispose(ref zero);
			if (Helper.TryMarshalGet<LobbyDetailsInfoInternal, LobbyDetailsInfo>(zero2, out outLobbyDetailsInfo))
			{
				Bindings.EOS_LobbyDetails_Info_Release(zero2);
			}
			return result;
		}

		// Token: 0x0600142A RID: 5162 RVA: 0x00015738 File Offset: 0x00013938
		public Result CopyMemberAttributeByIndex(LobbyDetailsCopyMemberAttributeByIndexOptions options, out Attribute outAttribute)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<LobbyDetailsCopyMemberAttributeByIndexOptionsInternal, LobbyDetailsCopyMemberAttributeByIndexOptions>(ref zero, options);
			IntPtr zero2 = IntPtr.Zero;
			Result result = Bindings.EOS_LobbyDetails_CopyMemberAttributeByIndex(base.InnerHandle, zero, ref zero2);
			Helper.TryMarshalDispose(ref zero);
			if (Helper.TryMarshalGet<AttributeInternal, Attribute>(zero2, out outAttribute))
			{
				Bindings.EOS_Lobby_Attribute_Release(zero2);
			}
			return result;
		}

		// Token: 0x0600142B RID: 5163 RVA: 0x00015780 File Offset: 0x00013980
		public Result CopyMemberAttributeByKey(LobbyDetailsCopyMemberAttributeByKeyOptions options, out Attribute outAttribute)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<LobbyDetailsCopyMemberAttributeByKeyOptionsInternal, LobbyDetailsCopyMemberAttributeByKeyOptions>(ref zero, options);
			IntPtr zero2 = IntPtr.Zero;
			Result result = Bindings.EOS_LobbyDetails_CopyMemberAttributeByKey(base.InnerHandle, zero, ref zero2);
			Helper.TryMarshalDispose(ref zero);
			if (Helper.TryMarshalGet<AttributeInternal, Attribute>(zero2, out outAttribute))
			{
				Bindings.EOS_Lobby_Attribute_Release(zero2);
			}
			return result;
		}

		// Token: 0x0600142C RID: 5164 RVA: 0x000157C8 File Offset: 0x000139C8
		public uint GetAttributeCount(LobbyDetailsGetAttributeCountOptions options)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<LobbyDetailsGetAttributeCountOptionsInternal, LobbyDetailsGetAttributeCountOptions>(ref zero, options);
			uint result = Bindings.EOS_LobbyDetails_GetAttributeCount(base.InnerHandle, zero);
			Helper.TryMarshalDispose(ref zero);
			return result;
		}

		// Token: 0x0600142D RID: 5165 RVA: 0x000157F8 File Offset: 0x000139F8
		public ProductUserId GetLobbyOwner(LobbyDetailsGetLobbyOwnerOptions options)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<LobbyDetailsGetLobbyOwnerOptionsInternal, LobbyDetailsGetLobbyOwnerOptions>(ref zero, options);
			IntPtr source = Bindings.EOS_LobbyDetails_GetLobbyOwner(base.InnerHandle, zero);
			Helper.TryMarshalDispose(ref zero);
			ProductUserId result;
			Helper.TryMarshalGet<ProductUserId>(source, out result);
			return result;
		}

		// Token: 0x0600142E RID: 5166 RVA: 0x00015834 File Offset: 0x00013A34
		public uint GetMemberAttributeCount(LobbyDetailsGetMemberAttributeCountOptions options)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<LobbyDetailsGetMemberAttributeCountOptionsInternal, LobbyDetailsGetMemberAttributeCountOptions>(ref zero, options);
			uint result = Bindings.EOS_LobbyDetails_GetMemberAttributeCount(base.InnerHandle, zero);
			Helper.TryMarshalDispose(ref zero);
			return result;
		}

		// Token: 0x0600142F RID: 5167 RVA: 0x00015864 File Offset: 0x00013A64
		public ProductUserId GetMemberByIndex(LobbyDetailsGetMemberByIndexOptions options)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<LobbyDetailsGetMemberByIndexOptionsInternal, LobbyDetailsGetMemberByIndexOptions>(ref zero, options);
			IntPtr source = Bindings.EOS_LobbyDetails_GetMemberByIndex(base.InnerHandle, zero);
			Helper.TryMarshalDispose(ref zero);
			ProductUserId result;
			Helper.TryMarshalGet<ProductUserId>(source, out result);
			return result;
		}

		// Token: 0x06001430 RID: 5168 RVA: 0x000158A0 File Offset: 0x00013AA0
		public uint GetMemberCount(LobbyDetailsGetMemberCountOptions options)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<LobbyDetailsGetMemberCountOptionsInternal, LobbyDetailsGetMemberCountOptions>(ref zero, options);
			uint result = Bindings.EOS_LobbyDetails_GetMemberCount(base.InnerHandle, zero);
			Helper.TryMarshalDispose(ref zero);
			return result;
		}

		// Token: 0x06001431 RID: 5169 RVA: 0x000158D0 File Offset: 0x00013AD0
		public void Release()
		{
			Bindings.EOS_LobbyDetails_Release(base.InnerHandle);
		}

		// Token: 0x0400099C RID: 2460
		public const int LobbydetailsCopyattributebyindexApiLatest = 1;

		// Token: 0x0400099D RID: 2461
		public const int LobbydetailsCopyattributebykeyApiLatest = 1;

		// Token: 0x0400099E RID: 2462
		public const int LobbydetailsCopyinfoApiLatest = 1;

		// Token: 0x0400099F RID: 2463
		public const int LobbydetailsCopymemberattributebyindexApiLatest = 1;

		// Token: 0x040009A0 RID: 2464
		public const int LobbydetailsCopymemberattributebykeyApiLatest = 1;

		// Token: 0x040009A1 RID: 2465
		public const int LobbydetailsGetattributecountApiLatest = 1;

		// Token: 0x040009A2 RID: 2466
		public const int LobbydetailsGetlobbyownerApiLatest = 1;

		// Token: 0x040009A3 RID: 2467
		public const int LobbydetailsGetmemberattributecountApiLatest = 1;

		// Token: 0x040009A4 RID: 2468
		public const int LobbydetailsGetmemberbyindexApiLatest = 1;

		// Token: 0x040009A5 RID: 2469
		public const int LobbydetailsGetmembercountApiLatest = 1;

		// Token: 0x040009A6 RID: 2470
		public const int LobbydetailsInfoApiLatest = 1;
	}
}
