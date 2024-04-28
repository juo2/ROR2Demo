using System;

namespace Epic.OnlineServices.Auth
{
	// Token: 0x0200050F RID: 1295
	public sealed class AuthInterface : Handle
	{
		// Token: 0x06001F46 RID: 8006 RVA: 0x000036D3 File Offset: 0x000018D3
		public AuthInterface()
		{
		}

		// Token: 0x06001F47 RID: 8007 RVA: 0x000036DB File Offset: 0x000018DB
		public AuthInterface(IntPtr innerHandle) : base(innerHandle)
		{
		}

		// Token: 0x06001F48 RID: 8008 RVA: 0x00020DE8 File Offset: 0x0001EFE8
		public ulong AddNotifyLoginStatusChanged(AddNotifyLoginStatusChangedOptions options, object clientData, OnLoginStatusChangedCallback notification)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<AddNotifyLoginStatusChangedOptionsInternal, AddNotifyLoginStatusChangedOptions>(ref zero, options);
			IntPtr zero2 = IntPtr.Zero;
			OnLoginStatusChangedCallbackInternal onLoginStatusChangedCallbackInternal = new OnLoginStatusChangedCallbackInternal(AuthInterface.OnLoginStatusChangedCallbackInternalImplementation);
			Helper.AddCallback(ref zero2, clientData, notification, onLoginStatusChangedCallbackInternal, Array.Empty<Delegate>());
			ulong num = Bindings.EOS_Auth_AddNotifyLoginStatusChanged(base.InnerHandle, zero, zero2, onLoginStatusChangedCallbackInternal);
			Helper.TryMarshalDispose(ref zero);
			Helper.TryAssignNotificationIdToCallback(zero2, num);
			return num;
		}

		// Token: 0x06001F49 RID: 8009 RVA: 0x00020E48 File Offset: 0x0001F048
		public Result CopyIdToken(CopyIdTokenOptions options, out IdToken outIdToken)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<CopyIdTokenOptionsInternal, CopyIdTokenOptions>(ref zero, options);
			IntPtr zero2 = IntPtr.Zero;
			Result result = Bindings.EOS_Auth_CopyIdToken(base.InnerHandle, zero, ref zero2);
			Helper.TryMarshalDispose(ref zero);
			if (Helper.TryMarshalGet<IdTokenInternal, IdToken>(zero2, out outIdToken))
			{
				Bindings.EOS_Auth_IdToken_Release(zero2);
			}
			return result;
		}

		// Token: 0x06001F4A RID: 8010 RVA: 0x00020E90 File Offset: 0x0001F090
		public Result CopyUserAuthToken(CopyUserAuthTokenOptions options, EpicAccountId localUserId, out Token outUserAuthToken)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<CopyUserAuthTokenOptionsInternal, CopyUserAuthTokenOptions>(ref zero, options);
			IntPtr zero2 = IntPtr.Zero;
			Helper.TryMarshalSet(ref zero2, localUserId);
			IntPtr zero3 = IntPtr.Zero;
			Result result = Bindings.EOS_Auth_CopyUserAuthToken(base.InnerHandle, zero, zero2, ref zero3);
			Helper.TryMarshalDispose(ref zero);
			if (Helper.TryMarshalGet<TokenInternal, Token>(zero3, out outUserAuthToken))
			{
				Bindings.EOS_Auth_Token_Release(zero3);
			}
			return result;
		}

		// Token: 0x06001F4B RID: 8011 RVA: 0x00020EE8 File Offset: 0x0001F0E8
		public void DeletePersistentAuth(DeletePersistentAuthOptions options, object clientData, OnDeletePersistentAuthCallback completionDelegate)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<DeletePersistentAuthOptionsInternal, DeletePersistentAuthOptions>(ref zero, options);
			IntPtr zero2 = IntPtr.Zero;
			OnDeletePersistentAuthCallbackInternal onDeletePersistentAuthCallbackInternal = new OnDeletePersistentAuthCallbackInternal(AuthInterface.OnDeletePersistentAuthCallbackInternalImplementation);
			Helper.AddCallback(ref zero2, clientData, completionDelegate, onDeletePersistentAuthCallbackInternal, Array.Empty<Delegate>());
			Bindings.EOS_Auth_DeletePersistentAuth(base.InnerHandle, zero, zero2, onDeletePersistentAuthCallbackInternal);
			Helper.TryMarshalDispose(ref zero);
		}

		// Token: 0x06001F4C RID: 8012 RVA: 0x00020F3C File Offset: 0x0001F13C
		public EpicAccountId GetLoggedInAccountByIndex(int index)
		{
			EpicAccountId result;
			Helper.TryMarshalGet<EpicAccountId>(Bindings.EOS_Auth_GetLoggedInAccountByIndex(base.InnerHandle, index), out result);
			return result;
		}

		// Token: 0x06001F4D RID: 8013 RVA: 0x00020F5E File Offset: 0x0001F15E
		public int GetLoggedInAccountsCount()
		{
			return Bindings.EOS_Auth_GetLoggedInAccountsCount(base.InnerHandle);
		}

		// Token: 0x06001F4E RID: 8014 RVA: 0x00020F6C File Offset: 0x0001F16C
		public LoginStatus GetLoginStatus(EpicAccountId localUserId)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet(ref zero, localUserId);
			return Bindings.EOS_Auth_GetLoginStatus(base.InnerHandle, zero);
		}

		// Token: 0x06001F4F RID: 8015 RVA: 0x00020F94 File Offset: 0x0001F194
		public EpicAccountId GetMergedAccountByIndex(EpicAccountId localUserId, uint index)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet(ref zero, localUserId);
			EpicAccountId result;
			Helper.TryMarshalGet<EpicAccountId>(Bindings.EOS_Auth_GetMergedAccountByIndex(base.InnerHandle, zero, index), out result);
			return result;
		}

		// Token: 0x06001F50 RID: 8016 RVA: 0x00020FC8 File Offset: 0x0001F1C8
		public uint GetMergedAccountsCount(EpicAccountId localUserId)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet(ref zero, localUserId);
			return Bindings.EOS_Auth_GetMergedAccountsCount(base.InnerHandle, zero);
		}

		// Token: 0x06001F51 RID: 8017 RVA: 0x00020FF0 File Offset: 0x0001F1F0
		public Result GetSelectedAccountId(EpicAccountId localUserId, out EpicAccountId outSelectedAccountId)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet(ref zero, localUserId);
			IntPtr zero2 = IntPtr.Zero;
			Result result = Bindings.EOS_Auth_GetSelectedAccountId(base.InnerHandle, zero, ref zero2);
			Helper.TryMarshalGet<EpicAccountId>(zero2, out outSelectedAccountId);
			return result;
		}

		// Token: 0x06001F52 RID: 8018 RVA: 0x00021028 File Offset: 0x0001F228
		public void LinkAccount(LinkAccountOptions options, object clientData, OnLinkAccountCallback completionDelegate)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<LinkAccountOptionsInternal, LinkAccountOptions>(ref zero, options);
			IntPtr zero2 = IntPtr.Zero;
			OnLinkAccountCallbackInternal onLinkAccountCallbackInternal = new OnLinkAccountCallbackInternal(AuthInterface.OnLinkAccountCallbackInternalImplementation);
			Helper.AddCallback(ref zero2, clientData, completionDelegate, onLinkAccountCallbackInternal, Array.Empty<Delegate>());
			Bindings.EOS_Auth_LinkAccount(base.InnerHandle, zero, zero2, onLinkAccountCallbackInternal);
			Helper.TryMarshalDispose(ref zero);
		}

		// Token: 0x06001F53 RID: 8019 RVA: 0x0002107C File Offset: 0x0001F27C
		public void Login(LoginOptions options, object clientData, OnLoginCallback completionDelegate)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<LoginOptionsInternal, LoginOptions>(ref zero, options);
			IntPtr zero2 = IntPtr.Zero;
			OnLoginCallbackInternal onLoginCallbackInternal = new OnLoginCallbackInternal(AuthInterface.OnLoginCallbackInternalImplementation);
			Helper.AddCallback(ref zero2, clientData, completionDelegate, onLoginCallbackInternal, Array.Empty<Delegate>());
			Bindings.EOS_Auth_Login(base.InnerHandle, zero, zero2, onLoginCallbackInternal);
			Helper.TryMarshalDispose(ref zero);
		}

		// Token: 0x06001F54 RID: 8020 RVA: 0x000210D0 File Offset: 0x0001F2D0
		public void Logout(LogoutOptions options, object clientData, OnLogoutCallback completionDelegate)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<LogoutOptionsInternal, LogoutOptions>(ref zero, options);
			IntPtr zero2 = IntPtr.Zero;
			OnLogoutCallbackInternal onLogoutCallbackInternal = new OnLogoutCallbackInternal(AuthInterface.OnLogoutCallbackInternalImplementation);
			Helper.AddCallback(ref zero2, clientData, completionDelegate, onLogoutCallbackInternal, Array.Empty<Delegate>());
			Bindings.EOS_Auth_Logout(base.InnerHandle, zero, zero2, onLogoutCallbackInternal);
			Helper.TryMarshalDispose(ref zero);
		}

		// Token: 0x06001F55 RID: 8021 RVA: 0x00021124 File Offset: 0x0001F324
		public void QueryIdToken(QueryIdTokenOptions options, object clientData, OnQueryIdTokenCallback completionDelegate)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<QueryIdTokenOptionsInternal, QueryIdTokenOptions>(ref zero, options);
			IntPtr zero2 = IntPtr.Zero;
			OnQueryIdTokenCallbackInternal onQueryIdTokenCallbackInternal = new OnQueryIdTokenCallbackInternal(AuthInterface.OnQueryIdTokenCallbackInternalImplementation);
			Helper.AddCallback(ref zero2, clientData, completionDelegate, onQueryIdTokenCallbackInternal, Array.Empty<Delegate>());
			Bindings.EOS_Auth_QueryIdToken(base.InnerHandle, zero, zero2, onQueryIdTokenCallbackInternal);
			Helper.TryMarshalDispose(ref zero);
		}

		// Token: 0x06001F56 RID: 8022 RVA: 0x00021178 File Offset: 0x0001F378
		public void RemoveNotifyLoginStatusChanged(ulong inId)
		{
			Helper.TryRemoveCallbackByNotificationId(inId);
			Bindings.EOS_Auth_RemoveNotifyLoginStatusChanged(base.InnerHandle, inId);
		}

		// Token: 0x06001F57 RID: 8023 RVA: 0x00021190 File Offset: 0x0001F390
		public void VerifyIdToken(VerifyIdTokenOptions options, object clientData, OnVerifyIdTokenCallback completionDelegate)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<VerifyIdTokenOptionsInternal, VerifyIdTokenOptions>(ref zero, options);
			IntPtr zero2 = IntPtr.Zero;
			OnVerifyIdTokenCallbackInternal onVerifyIdTokenCallbackInternal = new OnVerifyIdTokenCallbackInternal(AuthInterface.OnVerifyIdTokenCallbackInternalImplementation);
			Helper.AddCallback(ref zero2, clientData, completionDelegate, onVerifyIdTokenCallbackInternal, Array.Empty<Delegate>());
			Bindings.EOS_Auth_VerifyIdToken(base.InnerHandle, zero, zero2, onVerifyIdTokenCallbackInternal);
			Helper.TryMarshalDispose(ref zero);
		}

		// Token: 0x06001F58 RID: 8024 RVA: 0x000211E4 File Offset: 0x0001F3E4
		public void VerifyUserAuth(VerifyUserAuthOptions options, object clientData, OnVerifyUserAuthCallback completionDelegate)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<VerifyUserAuthOptionsInternal, VerifyUserAuthOptions>(ref zero, options);
			IntPtr zero2 = IntPtr.Zero;
			OnVerifyUserAuthCallbackInternal onVerifyUserAuthCallbackInternal = new OnVerifyUserAuthCallbackInternal(AuthInterface.OnVerifyUserAuthCallbackInternalImplementation);
			Helper.AddCallback(ref zero2, clientData, completionDelegate, onVerifyUserAuthCallbackInternal, Array.Empty<Delegate>());
			Bindings.EOS_Auth_VerifyUserAuth(base.InnerHandle, zero, zero2, onVerifyUserAuthCallbackInternal);
			Helper.TryMarshalDispose(ref zero);
		}

		// Token: 0x06001F59 RID: 8025 RVA: 0x00021238 File Offset: 0x0001F438
		[MonoPInvokeCallback(typeof(OnDeletePersistentAuthCallbackInternal))]
		internal static void OnDeletePersistentAuthCallbackInternalImplementation(IntPtr data)
		{
			OnDeletePersistentAuthCallback onDeletePersistentAuthCallback;
			DeletePersistentAuthCallbackInfo data2;
			if (Helper.TryGetAndRemoveCallback<OnDeletePersistentAuthCallback, DeletePersistentAuthCallbackInfoInternal, DeletePersistentAuthCallbackInfo>(data, out onDeletePersistentAuthCallback, out data2))
			{
				onDeletePersistentAuthCallback(data2);
			}
		}

		// Token: 0x06001F5A RID: 8026 RVA: 0x00021258 File Offset: 0x0001F458
		[MonoPInvokeCallback(typeof(OnLinkAccountCallbackInternal))]
		internal static void OnLinkAccountCallbackInternalImplementation(IntPtr data)
		{
			OnLinkAccountCallback onLinkAccountCallback;
			LinkAccountCallbackInfo data2;
			if (Helper.TryGetAndRemoveCallback<OnLinkAccountCallback, LinkAccountCallbackInfoInternal, LinkAccountCallbackInfo>(data, out onLinkAccountCallback, out data2))
			{
				onLinkAccountCallback(data2);
			}
		}

		// Token: 0x06001F5B RID: 8027 RVA: 0x00021278 File Offset: 0x0001F478
		[MonoPInvokeCallback(typeof(OnLoginCallbackInternal))]
		internal static void OnLoginCallbackInternalImplementation(IntPtr data)
		{
			OnLoginCallback onLoginCallback;
			LoginCallbackInfo data2;
			if (Helper.TryGetAndRemoveCallback<OnLoginCallback, LoginCallbackInfoInternal, LoginCallbackInfo>(data, out onLoginCallback, out data2))
			{
				onLoginCallback(data2);
			}
		}

		// Token: 0x06001F5C RID: 8028 RVA: 0x00021298 File Offset: 0x0001F498
		[MonoPInvokeCallback(typeof(OnLoginStatusChangedCallbackInternal))]
		internal static void OnLoginStatusChangedCallbackInternalImplementation(IntPtr data)
		{
			OnLoginStatusChangedCallback onLoginStatusChangedCallback;
			LoginStatusChangedCallbackInfo data2;
			if (Helper.TryGetAndRemoveCallback<OnLoginStatusChangedCallback, LoginStatusChangedCallbackInfoInternal, LoginStatusChangedCallbackInfo>(data, out onLoginStatusChangedCallback, out data2))
			{
				onLoginStatusChangedCallback(data2);
			}
		}

		// Token: 0x06001F5D RID: 8029 RVA: 0x000212B8 File Offset: 0x0001F4B8
		[MonoPInvokeCallback(typeof(OnLogoutCallbackInternal))]
		internal static void OnLogoutCallbackInternalImplementation(IntPtr data)
		{
			OnLogoutCallback onLogoutCallback;
			LogoutCallbackInfo data2;
			if (Helper.TryGetAndRemoveCallback<OnLogoutCallback, LogoutCallbackInfoInternal, LogoutCallbackInfo>(data, out onLogoutCallback, out data2))
			{
				onLogoutCallback(data2);
			}
		}

		// Token: 0x06001F5E RID: 8030 RVA: 0x000212D8 File Offset: 0x0001F4D8
		[MonoPInvokeCallback(typeof(OnQueryIdTokenCallbackInternal))]
		internal static void OnQueryIdTokenCallbackInternalImplementation(IntPtr data)
		{
			OnQueryIdTokenCallback onQueryIdTokenCallback;
			QueryIdTokenCallbackInfo data2;
			if (Helper.TryGetAndRemoveCallback<OnQueryIdTokenCallback, QueryIdTokenCallbackInfoInternal, QueryIdTokenCallbackInfo>(data, out onQueryIdTokenCallback, out data2))
			{
				onQueryIdTokenCallback(data2);
			}
		}

		// Token: 0x06001F5F RID: 8031 RVA: 0x000212F8 File Offset: 0x0001F4F8
		[MonoPInvokeCallback(typeof(OnVerifyIdTokenCallbackInternal))]
		internal static void OnVerifyIdTokenCallbackInternalImplementation(IntPtr data)
		{
			OnVerifyIdTokenCallback onVerifyIdTokenCallback;
			VerifyIdTokenCallbackInfo data2;
			if (Helper.TryGetAndRemoveCallback<OnVerifyIdTokenCallback, VerifyIdTokenCallbackInfoInternal, VerifyIdTokenCallbackInfo>(data, out onVerifyIdTokenCallback, out data2))
			{
				onVerifyIdTokenCallback(data2);
			}
		}

		// Token: 0x06001F60 RID: 8032 RVA: 0x00021318 File Offset: 0x0001F518
		[MonoPInvokeCallback(typeof(OnVerifyUserAuthCallbackInternal))]
		internal static void OnVerifyUserAuthCallbackInternalImplementation(IntPtr data)
		{
			OnVerifyUserAuthCallback onVerifyUserAuthCallback;
			VerifyUserAuthCallbackInfo data2;
			if (Helper.TryGetAndRemoveCallback<OnVerifyUserAuthCallback, VerifyUserAuthCallbackInfoInternal, VerifyUserAuthCallbackInfo>(data, out onVerifyUserAuthCallback, out data2))
			{
				onVerifyUserAuthCallback(data2);
			}
		}

		// Token: 0x06001F61 RID: 8033 RVA: 0x00021338 File Offset: 0x0001F538
		public void Login(IOSLoginOptions options, object clientData, OnLoginCallback completionDelegate)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<IOSLoginOptionsInternal, IOSLoginOptions>(ref zero, options);
			IntPtr zero2 = IntPtr.Zero;
			OnLoginCallbackInternal onLoginCallbackInternal = new OnLoginCallbackInternal(AuthInterface.OnLoginCallbackInternalImplementation);
			Helper.AddCallback(ref zero2, clientData, completionDelegate, onLoginCallbackInternal, Array.Empty<Delegate>());
			Bindings.EOS_Auth_Login(base.InnerHandle, zero, zero2, onLoginCallbackInternal);
			Helper.TryMarshalDispose(ref zero);
		}

		// Token: 0x04000E6E RID: 3694
		public const int AccountfeaturerestrictedinfoApiLatest = 1;

		// Token: 0x04000E6F RID: 3695
		public const int AddnotifyloginstatuschangedApiLatest = 1;

		// Token: 0x04000E70 RID: 3696
		public const int CopyidtokenApiLatest = 1;

		// Token: 0x04000E71 RID: 3697
		public const int CopyuserauthtokenApiLatest = 1;

		// Token: 0x04000E72 RID: 3698
		public const int CredentialsApiLatest = 3;

		// Token: 0x04000E73 RID: 3699
		public const int DeletepersistentauthApiLatest = 2;

		// Token: 0x04000E74 RID: 3700
		public const int IdtokenApiLatest = 1;

		// Token: 0x04000E75 RID: 3701
		public const int LinkaccountApiLatest = 1;

		// Token: 0x04000E76 RID: 3702
		public const int LoginApiLatest = 2;

		// Token: 0x04000E77 RID: 3703
		public const int LogoutApiLatest = 1;

		// Token: 0x04000E78 RID: 3704
		public const int PingrantinfoApiLatest = 2;

		// Token: 0x04000E79 RID: 3705
		public const int QueryidtokenApiLatest = 1;

		// Token: 0x04000E7A RID: 3706
		public const int TokenApiLatest = 2;

		// Token: 0x04000E7B RID: 3707
		public const int VerifyidtokenApiLatest = 1;

		// Token: 0x04000E7C RID: 3708
		public const int VerifyuserauthApiLatest = 1;

		// Token: 0x04000E7D RID: 3709
		public const int AuthIoscredentialssystemauthcredentialsoptionsApiLatest = 1;
	}
}
