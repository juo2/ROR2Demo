using System;

namespace Epic.OnlineServices.Connect
{
	// Token: 0x020004B0 RID: 1200
	public sealed class ConnectInterface : Handle
	{
		// Token: 0x06001D14 RID: 7444 RVA: 0x000036D3 File Offset: 0x000018D3
		public ConnectInterface()
		{
		}

		// Token: 0x06001D15 RID: 7445 RVA: 0x000036DB File Offset: 0x000018DB
		public ConnectInterface(IntPtr innerHandle) : base(innerHandle)
		{
		}

		// Token: 0x06001D16 RID: 7446 RVA: 0x0001EABC File Offset: 0x0001CCBC
		public ulong AddNotifyAuthExpiration(AddNotifyAuthExpirationOptions options, object clientData, OnAuthExpirationCallback notification)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<AddNotifyAuthExpirationOptionsInternal, AddNotifyAuthExpirationOptions>(ref zero, options);
			IntPtr zero2 = IntPtr.Zero;
			OnAuthExpirationCallbackInternal onAuthExpirationCallbackInternal = new OnAuthExpirationCallbackInternal(ConnectInterface.OnAuthExpirationCallbackInternalImplementation);
			Helper.AddCallback(ref zero2, clientData, notification, onAuthExpirationCallbackInternal, Array.Empty<Delegate>());
			ulong num = Bindings.EOS_Connect_AddNotifyAuthExpiration(base.InnerHandle, zero, zero2, onAuthExpirationCallbackInternal);
			Helper.TryMarshalDispose(ref zero);
			Helper.TryAssignNotificationIdToCallback(zero2, num);
			return num;
		}

		// Token: 0x06001D17 RID: 7447 RVA: 0x0001EB1C File Offset: 0x0001CD1C
		public ulong AddNotifyLoginStatusChanged(AddNotifyLoginStatusChangedOptions options, object clientData, OnLoginStatusChangedCallback notification)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<AddNotifyLoginStatusChangedOptionsInternal, AddNotifyLoginStatusChangedOptions>(ref zero, options);
			IntPtr zero2 = IntPtr.Zero;
			OnLoginStatusChangedCallbackInternal onLoginStatusChangedCallbackInternal = new OnLoginStatusChangedCallbackInternal(ConnectInterface.OnLoginStatusChangedCallbackInternalImplementation);
			Helper.AddCallback(ref zero2, clientData, notification, onLoginStatusChangedCallbackInternal, Array.Empty<Delegate>());
			ulong num = Bindings.EOS_Connect_AddNotifyLoginStatusChanged(base.InnerHandle, zero, zero2, onLoginStatusChangedCallbackInternal);
			Helper.TryMarshalDispose(ref zero);
			Helper.TryAssignNotificationIdToCallback(zero2, num);
			return num;
		}

		// Token: 0x06001D18 RID: 7448 RVA: 0x0001EB7C File Offset: 0x0001CD7C
		public Result CopyIdToken(CopyIdTokenOptions options, out IdToken outIdToken)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<CopyIdTokenOptionsInternal, CopyIdTokenOptions>(ref zero, options);
			IntPtr zero2 = IntPtr.Zero;
			Result result = Bindings.EOS_Connect_CopyIdToken(base.InnerHandle, zero, ref zero2);
			Helper.TryMarshalDispose(ref zero);
			if (Helper.TryMarshalGet<IdTokenInternal, IdToken>(zero2, out outIdToken))
			{
				Bindings.EOS_Connect_IdToken_Release(zero2);
			}
			return result;
		}

		// Token: 0x06001D19 RID: 7449 RVA: 0x0001EBC4 File Offset: 0x0001CDC4
		public Result CopyProductUserExternalAccountByAccountId(CopyProductUserExternalAccountByAccountIdOptions options, out ExternalAccountInfo outExternalAccountInfo)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<CopyProductUserExternalAccountByAccountIdOptionsInternal, CopyProductUserExternalAccountByAccountIdOptions>(ref zero, options);
			IntPtr zero2 = IntPtr.Zero;
			Result result = Bindings.EOS_Connect_CopyProductUserExternalAccountByAccountId(base.InnerHandle, zero, ref zero2);
			Helper.TryMarshalDispose(ref zero);
			if (Helper.TryMarshalGet<ExternalAccountInfoInternal, ExternalAccountInfo>(zero2, out outExternalAccountInfo))
			{
				Bindings.EOS_Connect_ExternalAccountInfo_Release(zero2);
			}
			return result;
		}

		// Token: 0x06001D1A RID: 7450 RVA: 0x0001EC0C File Offset: 0x0001CE0C
		public Result CopyProductUserExternalAccountByAccountType(CopyProductUserExternalAccountByAccountTypeOptions options, out ExternalAccountInfo outExternalAccountInfo)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<CopyProductUserExternalAccountByAccountTypeOptionsInternal, CopyProductUserExternalAccountByAccountTypeOptions>(ref zero, options);
			IntPtr zero2 = IntPtr.Zero;
			Result result = Bindings.EOS_Connect_CopyProductUserExternalAccountByAccountType(base.InnerHandle, zero, ref zero2);
			Helper.TryMarshalDispose(ref zero);
			if (Helper.TryMarshalGet<ExternalAccountInfoInternal, ExternalAccountInfo>(zero2, out outExternalAccountInfo))
			{
				Bindings.EOS_Connect_ExternalAccountInfo_Release(zero2);
			}
			return result;
		}

		// Token: 0x06001D1B RID: 7451 RVA: 0x0001EC54 File Offset: 0x0001CE54
		public Result CopyProductUserExternalAccountByIndex(CopyProductUserExternalAccountByIndexOptions options, out ExternalAccountInfo outExternalAccountInfo)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<CopyProductUserExternalAccountByIndexOptionsInternal, CopyProductUserExternalAccountByIndexOptions>(ref zero, options);
			IntPtr zero2 = IntPtr.Zero;
			Result result = Bindings.EOS_Connect_CopyProductUserExternalAccountByIndex(base.InnerHandle, zero, ref zero2);
			Helper.TryMarshalDispose(ref zero);
			if (Helper.TryMarshalGet<ExternalAccountInfoInternal, ExternalAccountInfo>(zero2, out outExternalAccountInfo))
			{
				Bindings.EOS_Connect_ExternalAccountInfo_Release(zero2);
			}
			return result;
		}

		// Token: 0x06001D1C RID: 7452 RVA: 0x0001EC9C File Offset: 0x0001CE9C
		public Result CopyProductUserInfo(CopyProductUserInfoOptions options, out ExternalAccountInfo outExternalAccountInfo)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<CopyProductUserInfoOptionsInternal, CopyProductUserInfoOptions>(ref zero, options);
			IntPtr zero2 = IntPtr.Zero;
			Result result = Bindings.EOS_Connect_CopyProductUserInfo(base.InnerHandle, zero, ref zero2);
			Helper.TryMarshalDispose(ref zero);
			if (Helper.TryMarshalGet<ExternalAccountInfoInternal, ExternalAccountInfo>(zero2, out outExternalAccountInfo))
			{
				Bindings.EOS_Connect_ExternalAccountInfo_Release(zero2);
			}
			return result;
		}

		// Token: 0x06001D1D RID: 7453 RVA: 0x0001ECE4 File Offset: 0x0001CEE4
		public void CreateDeviceId(CreateDeviceIdOptions options, object clientData, OnCreateDeviceIdCallback completionDelegate)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<CreateDeviceIdOptionsInternal, CreateDeviceIdOptions>(ref zero, options);
			IntPtr zero2 = IntPtr.Zero;
			OnCreateDeviceIdCallbackInternal onCreateDeviceIdCallbackInternal = new OnCreateDeviceIdCallbackInternal(ConnectInterface.OnCreateDeviceIdCallbackInternalImplementation);
			Helper.AddCallback(ref zero2, clientData, completionDelegate, onCreateDeviceIdCallbackInternal, Array.Empty<Delegate>());
			Bindings.EOS_Connect_CreateDeviceId(base.InnerHandle, zero, zero2, onCreateDeviceIdCallbackInternal);
			Helper.TryMarshalDispose(ref zero);
		}

		// Token: 0x06001D1E RID: 7454 RVA: 0x0001ED38 File Offset: 0x0001CF38
		public void CreateUser(CreateUserOptions options, object clientData, OnCreateUserCallback completionDelegate)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<CreateUserOptionsInternal, CreateUserOptions>(ref zero, options);
			IntPtr zero2 = IntPtr.Zero;
			OnCreateUserCallbackInternal onCreateUserCallbackInternal = new OnCreateUserCallbackInternal(ConnectInterface.OnCreateUserCallbackInternalImplementation);
			Helper.AddCallback(ref zero2, clientData, completionDelegate, onCreateUserCallbackInternal, Array.Empty<Delegate>());
			Bindings.EOS_Connect_CreateUser(base.InnerHandle, zero, zero2, onCreateUserCallbackInternal);
			Helper.TryMarshalDispose(ref zero);
		}

		// Token: 0x06001D1F RID: 7455 RVA: 0x0001ED8C File Offset: 0x0001CF8C
		public void DeleteDeviceId(DeleteDeviceIdOptions options, object clientData, OnDeleteDeviceIdCallback completionDelegate)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<DeleteDeviceIdOptionsInternal, DeleteDeviceIdOptions>(ref zero, options);
			IntPtr zero2 = IntPtr.Zero;
			OnDeleteDeviceIdCallbackInternal onDeleteDeviceIdCallbackInternal = new OnDeleteDeviceIdCallbackInternal(ConnectInterface.OnDeleteDeviceIdCallbackInternalImplementation);
			Helper.AddCallback(ref zero2, clientData, completionDelegate, onDeleteDeviceIdCallbackInternal, Array.Empty<Delegate>());
			Bindings.EOS_Connect_DeleteDeviceId(base.InnerHandle, zero, zero2, onDeleteDeviceIdCallbackInternal);
			Helper.TryMarshalDispose(ref zero);
		}

		// Token: 0x06001D20 RID: 7456 RVA: 0x0001EDE0 File Offset: 0x0001CFE0
		public ProductUserId GetExternalAccountMapping(GetExternalAccountMappingsOptions options)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<GetExternalAccountMappingsOptionsInternal, GetExternalAccountMappingsOptions>(ref zero, options);
			IntPtr source = Bindings.EOS_Connect_GetExternalAccountMapping(base.InnerHandle, zero);
			Helper.TryMarshalDispose(ref zero);
			ProductUserId result;
			Helper.TryMarshalGet<ProductUserId>(source, out result);
			return result;
		}

		// Token: 0x06001D21 RID: 7457 RVA: 0x0001EE1C File Offset: 0x0001D01C
		public ProductUserId GetLoggedInUserByIndex(int index)
		{
			ProductUserId result;
			Helper.TryMarshalGet<ProductUserId>(Bindings.EOS_Connect_GetLoggedInUserByIndex(base.InnerHandle, index), out result);
			return result;
		}

		// Token: 0x06001D22 RID: 7458 RVA: 0x0001EE3E File Offset: 0x0001D03E
		public int GetLoggedInUsersCount()
		{
			return Bindings.EOS_Connect_GetLoggedInUsersCount(base.InnerHandle);
		}

		// Token: 0x06001D23 RID: 7459 RVA: 0x0001EE4C File Offset: 0x0001D04C
		public LoginStatus GetLoginStatus(ProductUserId localUserId)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet(ref zero, localUserId);
			return Bindings.EOS_Connect_GetLoginStatus(base.InnerHandle, zero);
		}

		// Token: 0x06001D24 RID: 7460 RVA: 0x0001EE74 File Offset: 0x0001D074
		public uint GetProductUserExternalAccountCount(GetProductUserExternalAccountCountOptions options)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<GetProductUserExternalAccountCountOptionsInternal, GetProductUserExternalAccountCountOptions>(ref zero, options);
			uint result = Bindings.EOS_Connect_GetProductUserExternalAccountCount(base.InnerHandle, zero);
			Helper.TryMarshalDispose(ref zero);
			return result;
		}

		// Token: 0x06001D25 RID: 7461 RVA: 0x0001EEA4 File Offset: 0x0001D0A4
		public Result GetProductUserIdMapping(GetProductUserIdMappingOptions options, out string outBuffer)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<GetProductUserIdMappingOptionsInternal, GetProductUserIdMappingOptions>(ref zero, options);
			IntPtr zero2 = IntPtr.Zero;
			int size = 257;
			Helper.TryMarshalAllocate(ref zero2, size);
			Result result = Bindings.EOS_Connect_GetProductUserIdMapping(base.InnerHandle, zero, zero2, ref size);
			Helper.TryMarshalDispose(ref zero);
			Helper.TryMarshalGet(zero2, out outBuffer);
			Helper.TryMarshalDispose(ref zero2);
			return result;
		}

		// Token: 0x06001D26 RID: 7462 RVA: 0x0001EEFC File Offset: 0x0001D0FC
		public void LinkAccount(LinkAccountOptions options, object clientData, OnLinkAccountCallback completionDelegate)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<LinkAccountOptionsInternal, LinkAccountOptions>(ref zero, options);
			IntPtr zero2 = IntPtr.Zero;
			OnLinkAccountCallbackInternal onLinkAccountCallbackInternal = new OnLinkAccountCallbackInternal(ConnectInterface.OnLinkAccountCallbackInternalImplementation);
			Helper.AddCallback(ref zero2, clientData, completionDelegate, onLinkAccountCallbackInternal, Array.Empty<Delegate>());
			Bindings.EOS_Connect_LinkAccount(base.InnerHandle, zero, zero2, onLinkAccountCallbackInternal);
			Helper.TryMarshalDispose(ref zero);
		}

		// Token: 0x06001D27 RID: 7463 RVA: 0x0001EF50 File Offset: 0x0001D150
		public void Login(LoginOptions options, object clientData, OnLoginCallback completionDelegate)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<LoginOptionsInternal, LoginOptions>(ref zero, options);
			IntPtr zero2 = IntPtr.Zero;
			OnLoginCallbackInternal onLoginCallbackInternal = new OnLoginCallbackInternal(ConnectInterface.OnLoginCallbackInternalImplementation);
			Helper.AddCallback(ref zero2, clientData, completionDelegate, onLoginCallbackInternal, Array.Empty<Delegate>());
			Bindings.EOS_Connect_Login(base.InnerHandle, zero, zero2, onLoginCallbackInternal);
			Helper.TryMarshalDispose(ref zero);
		}

		// Token: 0x06001D28 RID: 7464 RVA: 0x0001EFA4 File Offset: 0x0001D1A4
		public void QueryExternalAccountMappings(QueryExternalAccountMappingsOptions options, object clientData, OnQueryExternalAccountMappingsCallback completionDelegate)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<QueryExternalAccountMappingsOptionsInternal, QueryExternalAccountMappingsOptions>(ref zero, options);
			IntPtr zero2 = IntPtr.Zero;
			OnQueryExternalAccountMappingsCallbackInternal onQueryExternalAccountMappingsCallbackInternal = new OnQueryExternalAccountMappingsCallbackInternal(ConnectInterface.OnQueryExternalAccountMappingsCallbackInternalImplementation);
			Helper.AddCallback(ref zero2, clientData, completionDelegate, onQueryExternalAccountMappingsCallbackInternal, Array.Empty<Delegate>());
			Bindings.EOS_Connect_QueryExternalAccountMappings(base.InnerHandle, zero, zero2, onQueryExternalAccountMappingsCallbackInternal);
			Helper.TryMarshalDispose(ref zero);
		}

		// Token: 0x06001D29 RID: 7465 RVA: 0x0001EFF8 File Offset: 0x0001D1F8
		public void QueryProductUserIdMappings(QueryProductUserIdMappingsOptions options, object clientData, OnQueryProductUserIdMappingsCallback completionDelegate)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<QueryProductUserIdMappingsOptionsInternal, QueryProductUserIdMappingsOptions>(ref zero, options);
			IntPtr zero2 = IntPtr.Zero;
			OnQueryProductUserIdMappingsCallbackInternal onQueryProductUserIdMappingsCallbackInternal = new OnQueryProductUserIdMappingsCallbackInternal(ConnectInterface.OnQueryProductUserIdMappingsCallbackInternalImplementation);
			Helper.AddCallback(ref zero2, clientData, completionDelegate, onQueryProductUserIdMappingsCallbackInternal, Array.Empty<Delegate>());
			Bindings.EOS_Connect_QueryProductUserIdMappings(base.InnerHandle, zero, zero2, onQueryProductUserIdMappingsCallbackInternal);
			Helper.TryMarshalDispose(ref zero);
		}

		// Token: 0x06001D2A RID: 7466 RVA: 0x0001F04C File Offset: 0x0001D24C
		public void RemoveNotifyAuthExpiration(ulong inId)
		{
			Helper.TryRemoveCallbackByNotificationId(inId);
			Bindings.EOS_Connect_RemoveNotifyAuthExpiration(base.InnerHandle, inId);
		}

		// Token: 0x06001D2B RID: 7467 RVA: 0x0001F061 File Offset: 0x0001D261
		public void RemoveNotifyLoginStatusChanged(ulong inId)
		{
			Helper.TryRemoveCallbackByNotificationId(inId);
			Bindings.EOS_Connect_RemoveNotifyLoginStatusChanged(base.InnerHandle, inId);
		}

		// Token: 0x06001D2C RID: 7468 RVA: 0x0001F078 File Offset: 0x0001D278
		public void TransferDeviceIdAccount(TransferDeviceIdAccountOptions options, object clientData, OnTransferDeviceIdAccountCallback completionDelegate)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<TransferDeviceIdAccountOptionsInternal, TransferDeviceIdAccountOptions>(ref zero, options);
			IntPtr zero2 = IntPtr.Zero;
			OnTransferDeviceIdAccountCallbackInternal onTransferDeviceIdAccountCallbackInternal = new OnTransferDeviceIdAccountCallbackInternal(ConnectInterface.OnTransferDeviceIdAccountCallbackInternalImplementation);
			Helper.AddCallback(ref zero2, clientData, completionDelegate, onTransferDeviceIdAccountCallbackInternal, Array.Empty<Delegate>());
			Bindings.EOS_Connect_TransferDeviceIdAccount(base.InnerHandle, zero, zero2, onTransferDeviceIdAccountCallbackInternal);
			Helper.TryMarshalDispose(ref zero);
		}

		// Token: 0x06001D2D RID: 7469 RVA: 0x0001F0CC File Offset: 0x0001D2CC
		public void UnlinkAccount(UnlinkAccountOptions options, object clientData, OnUnlinkAccountCallback completionDelegate)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<UnlinkAccountOptionsInternal, UnlinkAccountOptions>(ref zero, options);
			IntPtr zero2 = IntPtr.Zero;
			OnUnlinkAccountCallbackInternal onUnlinkAccountCallbackInternal = new OnUnlinkAccountCallbackInternal(ConnectInterface.OnUnlinkAccountCallbackInternalImplementation);
			Helper.AddCallback(ref zero2, clientData, completionDelegate, onUnlinkAccountCallbackInternal, Array.Empty<Delegate>());
			Bindings.EOS_Connect_UnlinkAccount(base.InnerHandle, zero, zero2, onUnlinkAccountCallbackInternal);
			Helper.TryMarshalDispose(ref zero);
		}

		// Token: 0x06001D2E RID: 7470 RVA: 0x0001F120 File Offset: 0x0001D320
		public void VerifyIdToken(VerifyIdTokenOptions options, object clientData, OnVerifyIdTokenCallback completionDelegate)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<VerifyIdTokenOptionsInternal, VerifyIdTokenOptions>(ref zero, options);
			IntPtr zero2 = IntPtr.Zero;
			OnVerifyIdTokenCallbackInternal onVerifyIdTokenCallbackInternal = new OnVerifyIdTokenCallbackInternal(ConnectInterface.OnVerifyIdTokenCallbackInternalImplementation);
			Helper.AddCallback(ref zero2, clientData, completionDelegate, onVerifyIdTokenCallbackInternal, Array.Empty<Delegate>());
			Bindings.EOS_Connect_VerifyIdToken(base.InnerHandle, zero, zero2, onVerifyIdTokenCallbackInternal);
			Helper.TryMarshalDispose(ref zero);
		}

		// Token: 0x06001D2F RID: 7471 RVA: 0x0001F174 File Offset: 0x0001D374
		[MonoPInvokeCallback(typeof(OnAuthExpirationCallbackInternal))]
		internal static void OnAuthExpirationCallbackInternalImplementation(IntPtr data)
		{
			OnAuthExpirationCallback onAuthExpirationCallback;
			AuthExpirationCallbackInfo data2;
			if (Helper.TryGetAndRemoveCallback<OnAuthExpirationCallback, AuthExpirationCallbackInfoInternal, AuthExpirationCallbackInfo>(data, out onAuthExpirationCallback, out data2))
			{
				onAuthExpirationCallback(data2);
			}
		}

		// Token: 0x06001D30 RID: 7472 RVA: 0x0001F194 File Offset: 0x0001D394
		[MonoPInvokeCallback(typeof(OnCreateDeviceIdCallbackInternal))]
		internal static void OnCreateDeviceIdCallbackInternalImplementation(IntPtr data)
		{
			OnCreateDeviceIdCallback onCreateDeviceIdCallback;
			CreateDeviceIdCallbackInfo data2;
			if (Helper.TryGetAndRemoveCallback<OnCreateDeviceIdCallback, CreateDeviceIdCallbackInfoInternal, CreateDeviceIdCallbackInfo>(data, out onCreateDeviceIdCallback, out data2))
			{
				onCreateDeviceIdCallback(data2);
			}
		}

		// Token: 0x06001D31 RID: 7473 RVA: 0x0001F1B4 File Offset: 0x0001D3B4
		[MonoPInvokeCallback(typeof(OnCreateUserCallbackInternal))]
		internal static void OnCreateUserCallbackInternalImplementation(IntPtr data)
		{
			OnCreateUserCallback onCreateUserCallback;
			CreateUserCallbackInfo data2;
			if (Helper.TryGetAndRemoveCallback<OnCreateUserCallback, CreateUserCallbackInfoInternal, CreateUserCallbackInfo>(data, out onCreateUserCallback, out data2))
			{
				onCreateUserCallback(data2);
			}
		}

		// Token: 0x06001D32 RID: 7474 RVA: 0x0001F1D4 File Offset: 0x0001D3D4
		[MonoPInvokeCallback(typeof(OnDeleteDeviceIdCallbackInternal))]
		internal static void OnDeleteDeviceIdCallbackInternalImplementation(IntPtr data)
		{
			OnDeleteDeviceIdCallback onDeleteDeviceIdCallback;
			DeleteDeviceIdCallbackInfo data2;
			if (Helper.TryGetAndRemoveCallback<OnDeleteDeviceIdCallback, DeleteDeviceIdCallbackInfoInternal, DeleteDeviceIdCallbackInfo>(data, out onDeleteDeviceIdCallback, out data2))
			{
				onDeleteDeviceIdCallback(data2);
			}
		}

		// Token: 0x06001D33 RID: 7475 RVA: 0x0001F1F4 File Offset: 0x0001D3F4
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

		// Token: 0x06001D34 RID: 7476 RVA: 0x0001F214 File Offset: 0x0001D414
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

		// Token: 0x06001D35 RID: 7477 RVA: 0x0001F234 File Offset: 0x0001D434
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

		// Token: 0x06001D36 RID: 7478 RVA: 0x0001F254 File Offset: 0x0001D454
		[MonoPInvokeCallback(typeof(OnQueryExternalAccountMappingsCallbackInternal))]
		internal static void OnQueryExternalAccountMappingsCallbackInternalImplementation(IntPtr data)
		{
			OnQueryExternalAccountMappingsCallback onQueryExternalAccountMappingsCallback;
			QueryExternalAccountMappingsCallbackInfo data2;
			if (Helper.TryGetAndRemoveCallback<OnQueryExternalAccountMappingsCallback, QueryExternalAccountMappingsCallbackInfoInternal, QueryExternalAccountMappingsCallbackInfo>(data, out onQueryExternalAccountMappingsCallback, out data2))
			{
				onQueryExternalAccountMappingsCallback(data2);
			}
		}

		// Token: 0x06001D37 RID: 7479 RVA: 0x0001F274 File Offset: 0x0001D474
		[MonoPInvokeCallback(typeof(OnQueryProductUserIdMappingsCallbackInternal))]
		internal static void OnQueryProductUserIdMappingsCallbackInternalImplementation(IntPtr data)
		{
			OnQueryProductUserIdMappingsCallback onQueryProductUserIdMappingsCallback;
			QueryProductUserIdMappingsCallbackInfo data2;
			if (Helper.TryGetAndRemoveCallback<OnQueryProductUserIdMappingsCallback, QueryProductUserIdMappingsCallbackInfoInternal, QueryProductUserIdMappingsCallbackInfo>(data, out onQueryProductUserIdMappingsCallback, out data2))
			{
				onQueryProductUserIdMappingsCallback(data2);
			}
		}

		// Token: 0x06001D38 RID: 7480 RVA: 0x0001F294 File Offset: 0x0001D494
		[MonoPInvokeCallback(typeof(OnTransferDeviceIdAccountCallbackInternal))]
		internal static void OnTransferDeviceIdAccountCallbackInternalImplementation(IntPtr data)
		{
			OnTransferDeviceIdAccountCallback onTransferDeviceIdAccountCallback;
			TransferDeviceIdAccountCallbackInfo data2;
			if (Helper.TryGetAndRemoveCallback<OnTransferDeviceIdAccountCallback, TransferDeviceIdAccountCallbackInfoInternal, TransferDeviceIdAccountCallbackInfo>(data, out onTransferDeviceIdAccountCallback, out data2))
			{
				onTransferDeviceIdAccountCallback(data2);
			}
		}

		// Token: 0x06001D39 RID: 7481 RVA: 0x0001F2B4 File Offset: 0x0001D4B4
		[MonoPInvokeCallback(typeof(OnUnlinkAccountCallbackInternal))]
		internal static void OnUnlinkAccountCallbackInternalImplementation(IntPtr data)
		{
			OnUnlinkAccountCallback onUnlinkAccountCallback;
			UnlinkAccountCallbackInfo data2;
			if (Helper.TryGetAndRemoveCallback<OnUnlinkAccountCallback, UnlinkAccountCallbackInfoInternal, UnlinkAccountCallbackInfo>(data, out onUnlinkAccountCallback, out data2))
			{
				onUnlinkAccountCallback(data2);
			}
		}

		// Token: 0x06001D3A RID: 7482 RVA: 0x0001F2D4 File Offset: 0x0001D4D4
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

		// Token: 0x04000D8B RID: 3467
		public const int AddnotifyauthexpirationApiLatest = 1;

		// Token: 0x04000D8C RID: 3468
		public const int AddnotifyloginstatuschangedApiLatest = 1;

		// Token: 0x04000D8D RID: 3469
		public const int CopyidtokenApiLatest = 1;

		// Token: 0x04000D8E RID: 3470
		public const int CopyproductuserexternalaccountbyaccountidApiLatest = 1;

		// Token: 0x04000D8F RID: 3471
		public const int CopyproductuserexternalaccountbyaccounttypeApiLatest = 1;

		// Token: 0x04000D90 RID: 3472
		public const int CopyproductuserexternalaccountbyindexApiLatest = 1;

		// Token: 0x04000D91 RID: 3473
		public const int CopyproductuserinfoApiLatest = 1;

		// Token: 0x04000D92 RID: 3474
		public const int CreatedeviceidApiLatest = 1;

		// Token: 0x04000D93 RID: 3475
		public const int CreatedeviceidDevicemodelMaxLength = 64;

		// Token: 0x04000D94 RID: 3476
		public const int CreateuserApiLatest = 1;

		// Token: 0x04000D95 RID: 3477
		public const int CredentialsApiLatest = 1;

		// Token: 0x04000D96 RID: 3478
		public const int DeletedeviceidApiLatest = 1;

		// Token: 0x04000D97 RID: 3479
		public const int ExternalAccountIdMaxLength = 256;

		// Token: 0x04000D98 RID: 3480
		public const int ExternalaccountinfoApiLatest = 1;

		// Token: 0x04000D99 RID: 3481
		public const int GetexternalaccountmappingApiLatest = 1;

		// Token: 0x04000D9A RID: 3482
		public const int GetexternalaccountmappingsApiLatest = 1;

		// Token: 0x04000D9B RID: 3483
		public const int GetproductuserexternalaccountcountApiLatest = 1;

		// Token: 0x04000D9C RID: 3484
		public const int GetproductuseridmappingApiLatest = 1;

		// Token: 0x04000D9D RID: 3485
		public const int IdtokenApiLatest = 1;

		// Token: 0x04000D9E RID: 3486
		public const int LinkaccountApiLatest = 1;

		// Token: 0x04000D9F RID: 3487
		public const int LoginApiLatest = 2;

		// Token: 0x04000DA0 RID: 3488
		public const int OnauthexpirationcallbackApiLatest = 1;

		// Token: 0x04000DA1 RID: 3489
		public const int QueryexternalaccountmappingsApiLatest = 1;

		// Token: 0x04000DA2 RID: 3490
		public const int QueryexternalaccountmappingsMaxAccountIds = 128;

		// Token: 0x04000DA3 RID: 3491
		public const int QueryproductuseridmappingsApiLatest = 2;

		// Token: 0x04000DA4 RID: 3492
		public const int TimeUndefined = -1;

		// Token: 0x04000DA5 RID: 3493
		public const int TransferdeviceidaccountApiLatest = 1;

		// Token: 0x04000DA6 RID: 3494
		public const int UnlinkaccountApiLatest = 1;

		// Token: 0x04000DA7 RID: 3495
		public const int UserlogininfoApiLatest = 1;

		// Token: 0x04000DA8 RID: 3496
		public const int UserlogininfoDisplaynameMaxLength = 32;

		// Token: 0x04000DA9 RID: 3497
		public const int VerifyidtokenApiLatest = 1;
	}
}
