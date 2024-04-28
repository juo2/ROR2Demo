using System;

namespace Epic.OnlineServices.UserInfo
{
	// Token: 0x0200003F RID: 63
	public sealed class UserInfoInterface : Handle
	{
		// Token: 0x0600038C RID: 908 RVA: 0x000036D3 File Offset: 0x000018D3
		public UserInfoInterface()
		{
		}

		// Token: 0x0600038D RID: 909 RVA: 0x000036DB File Offset: 0x000018DB
		public UserInfoInterface(IntPtr innerHandle) : base(innerHandle)
		{
		}

		// Token: 0x0600038E RID: 910 RVA: 0x000047B0 File Offset: 0x000029B0
		public Result CopyExternalUserInfoByAccountId(CopyExternalUserInfoByAccountIdOptions options, out ExternalUserInfo outExternalUserInfo)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<CopyExternalUserInfoByAccountIdOptionsInternal, CopyExternalUserInfoByAccountIdOptions>(ref zero, options);
			IntPtr zero2 = IntPtr.Zero;
			Result result = Bindings.EOS_UserInfo_CopyExternalUserInfoByAccountId(base.InnerHandle, zero, ref zero2);
			Helper.TryMarshalDispose(ref zero);
			if (Helper.TryMarshalGet<ExternalUserInfoInternal, ExternalUserInfo>(zero2, out outExternalUserInfo))
			{
				Bindings.EOS_UserInfo_ExternalUserInfo_Release(zero2);
			}
			return result;
		}

		// Token: 0x0600038F RID: 911 RVA: 0x000047F8 File Offset: 0x000029F8
		public Result CopyExternalUserInfoByAccountType(CopyExternalUserInfoByAccountTypeOptions options, out ExternalUserInfo outExternalUserInfo)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<CopyExternalUserInfoByAccountTypeOptionsInternal, CopyExternalUserInfoByAccountTypeOptions>(ref zero, options);
			IntPtr zero2 = IntPtr.Zero;
			Result result = Bindings.EOS_UserInfo_CopyExternalUserInfoByAccountType(base.InnerHandle, zero, ref zero2);
			Helper.TryMarshalDispose(ref zero);
			if (Helper.TryMarshalGet<ExternalUserInfoInternal, ExternalUserInfo>(zero2, out outExternalUserInfo))
			{
				Bindings.EOS_UserInfo_ExternalUserInfo_Release(zero2);
			}
			return result;
		}

		// Token: 0x06000390 RID: 912 RVA: 0x00004840 File Offset: 0x00002A40
		public Result CopyExternalUserInfoByIndex(CopyExternalUserInfoByIndexOptions options, out ExternalUserInfo outExternalUserInfo)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<CopyExternalUserInfoByIndexOptionsInternal, CopyExternalUserInfoByIndexOptions>(ref zero, options);
			IntPtr zero2 = IntPtr.Zero;
			Result result = Bindings.EOS_UserInfo_CopyExternalUserInfoByIndex(base.InnerHandle, zero, ref zero2);
			Helper.TryMarshalDispose(ref zero);
			if (Helper.TryMarshalGet<ExternalUserInfoInternal, ExternalUserInfo>(zero2, out outExternalUserInfo))
			{
				Bindings.EOS_UserInfo_ExternalUserInfo_Release(zero2);
			}
			return result;
		}

		// Token: 0x06000391 RID: 913 RVA: 0x00004888 File Offset: 0x00002A88
		public Result CopyUserInfo(CopyUserInfoOptions options, out UserInfoData outUserInfo)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<CopyUserInfoOptionsInternal, CopyUserInfoOptions>(ref zero, options);
			IntPtr zero2 = IntPtr.Zero;
			Result result = Bindings.EOS_UserInfo_CopyUserInfo(base.InnerHandle, zero, ref zero2);
			Helper.TryMarshalDispose(ref zero);
			if (Helper.TryMarshalGet<UserInfoDataInternal, UserInfoData>(zero2, out outUserInfo))
			{
				Bindings.EOS_UserInfo_Release(zero2);
			}
			return result;
		}

		// Token: 0x06000392 RID: 914 RVA: 0x000048D0 File Offset: 0x00002AD0
		public uint GetExternalUserInfoCount(GetExternalUserInfoCountOptions options)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<GetExternalUserInfoCountOptionsInternal, GetExternalUserInfoCountOptions>(ref zero, options);
			uint result = Bindings.EOS_UserInfo_GetExternalUserInfoCount(base.InnerHandle, zero);
			Helper.TryMarshalDispose(ref zero);
			return result;
		}

		// Token: 0x06000393 RID: 915 RVA: 0x00004900 File Offset: 0x00002B00
		public void QueryUserInfo(QueryUserInfoOptions options, object clientData, OnQueryUserInfoCallback completionDelegate)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<QueryUserInfoOptionsInternal, QueryUserInfoOptions>(ref zero, options);
			IntPtr zero2 = IntPtr.Zero;
			OnQueryUserInfoCallbackInternal onQueryUserInfoCallbackInternal = new OnQueryUserInfoCallbackInternal(UserInfoInterface.OnQueryUserInfoCallbackInternalImplementation);
			Helper.AddCallback(ref zero2, clientData, completionDelegate, onQueryUserInfoCallbackInternal, Array.Empty<Delegate>());
			Bindings.EOS_UserInfo_QueryUserInfo(base.InnerHandle, zero, zero2, onQueryUserInfoCallbackInternal);
			Helper.TryMarshalDispose(ref zero);
		}

		// Token: 0x06000394 RID: 916 RVA: 0x00004954 File Offset: 0x00002B54
		public void QueryUserInfoByDisplayName(QueryUserInfoByDisplayNameOptions options, object clientData, OnQueryUserInfoByDisplayNameCallback completionDelegate)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<QueryUserInfoByDisplayNameOptionsInternal, QueryUserInfoByDisplayNameOptions>(ref zero, options);
			IntPtr zero2 = IntPtr.Zero;
			OnQueryUserInfoByDisplayNameCallbackInternal onQueryUserInfoByDisplayNameCallbackInternal = new OnQueryUserInfoByDisplayNameCallbackInternal(UserInfoInterface.OnQueryUserInfoByDisplayNameCallbackInternalImplementation);
			Helper.AddCallback(ref zero2, clientData, completionDelegate, onQueryUserInfoByDisplayNameCallbackInternal, Array.Empty<Delegate>());
			Bindings.EOS_UserInfo_QueryUserInfoByDisplayName(base.InnerHandle, zero, zero2, onQueryUserInfoByDisplayNameCallbackInternal);
			Helper.TryMarshalDispose(ref zero);
		}

		// Token: 0x06000395 RID: 917 RVA: 0x000049A8 File Offset: 0x00002BA8
		public void QueryUserInfoByExternalAccount(QueryUserInfoByExternalAccountOptions options, object clientData, OnQueryUserInfoByExternalAccountCallback completionDelegate)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<QueryUserInfoByExternalAccountOptionsInternal, QueryUserInfoByExternalAccountOptions>(ref zero, options);
			IntPtr zero2 = IntPtr.Zero;
			OnQueryUserInfoByExternalAccountCallbackInternal onQueryUserInfoByExternalAccountCallbackInternal = new OnQueryUserInfoByExternalAccountCallbackInternal(UserInfoInterface.OnQueryUserInfoByExternalAccountCallbackInternalImplementation);
			Helper.AddCallback(ref zero2, clientData, completionDelegate, onQueryUserInfoByExternalAccountCallbackInternal, Array.Empty<Delegate>());
			Bindings.EOS_UserInfo_QueryUserInfoByExternalAccount(base.InnerHandle, zero, zero2, onQueryUserInfoByExternalAccountCallbackInternal);
			Helper.TryMarshalDispose(ref zero);
		}

		// Token: 0x06000396 RID: 918 RVA: 0x000049FC File Offset: 0x00002BFC
		[MonoPInvokeCallback(typeof(OnQueryUserInfoByDisplayNameCallbackInternal))]
		internal static void OnQueryUserInfoByDisplayNameCallbackInternalImplementation(IntPtr data)
		{
			OnQueryUserInfoByDisplayNameCallback onQueryUserInfoByDisplayNameCallback;
			QueryUserInfoByDisplayNameCallbackInfo data2;
			if (Helper.TryGetAndRemoveCallback<OnQueryUserInfoByDisplayNameCallback, QueryUserInfoByDisplayNameCallbackInfoInternal, QueryUserInfoByDisplayNameCallbackInfo>(data, out onQueryUserInfoByDisplayNameCallback, out data2))
			{
				onQueryUserInfoByDisplayNameCallback(data2);
			}
		}

		// Token: 0x06000397 RID: 919 RVA: 0x00004A1C File Offset: 0x00002C1C
		[MonoPInvokeCallback(typeof(OnQueryUserInfoByExternalAccountCallbackInternal))]
		internal static void OnQueryUserInfoByExternalAccountCallbackInternalImplementation(IntPtr data)
		{
			OnQueryUserInfoByExternalAccountCallback onQueryUserInfoByExternalAccountCallback;
			QueryUserInfoByExternalAccountCallbackInfo data2;
			if (Helper.TryGetAndRemoveCallback<OnQueryUserInfoByExternalAccountCallback, QueryUserInfoByExternalAccountCallbackInfoInternal, QueryUserInfoByExternalAccountCallbackInfo>(data, out onQueryUserInfoByExternalAccountCallback, out data2))
			{
				onQueryUserInfoByExternalAccountCallback(data2);
			}
		}

		// Token: 0x06000398 RID: 920 RVA: 0x00004A3C File Offset: 0x00002C3C
		[MonoPInvokeCallback(typeof(OnQueryUserInfoCallbackInternal))]
		internal static void OnQueryUserInfoCallbackInternalImplementation(IntPtr data)
		{
			OnQueryUserInfoCallback onQueryUserInfoCallback;
			QueryUserInfoCallbackInfo data2;
			if (Helper.TryGetAndRemoveCallback<OnQueryUserInfoCallback, QueryUserInfoCallbackInfoInternal, QueryUserInfoCallbackInfo>(data, out onQueryUserInfoCallback, out data2))
			{
				onQueryUserInfoCallback(data2);
			}
		}

		// Token: 0x0400017F RID: 383
		public const int CopyexternaluserinfobyaccountidApiLatest = 1;

		// Token: 0x04000180 RID: 384
		public const int CopyexternaluserinfobyaccounttypeApiLatest = 1;

		// Token: 0x04000181 RID: 385
		public const int CopyexternaluserinfobyindexApiLatest = 1;

		// Token: 0x04000182 RID: 386
		public const int CopyuserinfoApiLatest = 2;

		// Token: 0x04000183 RID: 387
		public const int ExternaluserinfoApiLatest = 1;

		// Token: 0x04000184 RID: 388
		public const int GetexternaluserinfocountApiLatest = 1;

		// Token: 0x04000185 RID: 389
		public const int MaxDisplaynameCharacters = 16;

		// Token: 0x04000186 RID: 390
		public const int MaxDisplaynameUtf8Length = 64;

		// Token: 0x04000187 RID: 391
		public const int QueryuserinfoApiLatest = 1;

		// Token: 0x04000188 RID: 392
		public const int QueryuserinfobydisplaynameApiLatest = 1;

		// Token: 0x04000189 RID: 393
		public const int QueryuserinfobyexternalaccountApiLatest = 1;
	}
}
