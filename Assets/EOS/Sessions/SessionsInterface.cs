using System;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x02000140 RID: 320
	public sealed class SessionsInterface : Handle
	{
		// Token: 0x060008CA RID: 2250 RVA: 0x000036D3 File Offset: 0x000018D3
		public SessionsInterface()
		{
		}

		// Token: 0x060008CB RID: 2251 RVA: 0x000036DB File Offset: 0x000018DB
		public SessionsInterface(IntPtr innerHandle) : base(innerHandle)
		{
		}

		// Token: 0x060008CC RID: 2252 RVA: 0x00009754 File Offset: 0x00007954
		public ulong AddNotifyJoinSessionAccepted(AddNotifyJoinSessionAcceptedOptions options, object clientData, OnJoinSessionAcceptedCallback notificationFn)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<AddNotifyJoinSessionAcceptedOptionsInternal, AddNotifyJoinSessionAcceptedOptions>(ref zero, options);
			IntPtr zero2 = IntPtr.Zero;
			OnJoinSessionAcceptedCallbackInternal onJoinSessionAcceptedCallbackInternal = new OnJoinSessionAcceptedCallbackInternal(SessionsInterface.OnJoinSessionAcceptedCallbackInternalImplementation);
			Helper.AddCallback(ref zero2, clientData, notificationFn, onJoinSessionAcceptedCallbackInternal, Array.Empty<Delegate>());
			ulong num = Bindings.EOS_Sessions_AddNotifyJoinSessionAccepted(base.InnerHandle, zero, zero2, onJoinSessionAcceptedCallbackInternal);
			Helper.TryMarshalDispose(ref zero);
			Helper.TryAssignNotificationIdToCallback(zero2, num);
			return num;
		}

		// Token: 0x060008CD RID: 2253 RVA: 0x000097B4 File Offset: 0x000079B4
		public ulong AddNotifySessionInviteAccepted(AddNotifySessionInviteAcceptedOptions options, object clientData, OnSessionInviteAcceptedCallback notificationFn)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<AddNotifySessionInviteAcceptedOptionsInternal, AddNotifySessionInviteAcceptedOptions>(ref zero, options);
			IntPtr zero2 = IntPtr.Zero;
			OnSessionInviteAcceptedCallbackInternal onSessionInviteAcceptedCallbackInternal = new OnSessionInviteAcceptedCallbackInternal(SessionsInterface.OnSessionInviteAcceptedCallbackInternalImplementation);
			Helper.AddCallback(ref zero2, clientData, notificationFn, onSessionInviteAcceptedCallbackInternal, Array.Empty<Delegate>());
			ulong num = Bindings.EOS_Sessions_AddNotifySessionInviteAccepted(base.InnerHandle, zero, zero2, onSessionInviteAcceptedCallbackInternal);
			Helper.TryMarshalDispose(ref zero);
			Helper.TryAssignNotificationIdToCallback(zero2, num);
			return num;
		}

		// Token: 0x060008CE RID: 2254 RVA: 0x00009814 File Offset: 0x00007A14
		public ulong AddNotifySessionInviteReceived(AddNotifySessionInviteReceivedOptions options, object clientData, OnSessionInviteReceivedCallback notificationFn)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<AddNotifySessionInviteReceivedOptionsInternal, AddNotifySessionInviteReceivedOptions>(ref zero, options);
			IntPtr zero2 = IntPtr.Zero;
			OnSessionInviteReceivedCallbackInternal onSessionInviteReceivedCallbackInternal = new OnSessionInviteReceivedCallbackInternal(SessionsInterface.OnSessionInviteReceivedCallbackInternalImplementation);
			Helper.AddCallback(ref zero2, clientData, notificationFn, onSessionInviteReceivedCallbackInternal, Array.Empty<Delegate>());
			ulong num = Bindings.EOS_Sessions_AddNotifySessionInviteReceived(base.InnerHandle, zero, zero2, onSessionInviteReceivedCallbackInternal);
			Helper.TryMarshalDispose(ref zero);
			Helper.TryAssignNotificationIdToCallback(zero2, num);
			return num;
		}

		// Token: 0x060008CF RID: 2255 RVA: 0x00009874 File Offset: 0x00007A74
		public Result CopyActiveSessionHandle(CopyActiveSessionHandleOptions options, out ActiveSession outSessionHandle)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<CopyActiveSessionHandleOptionsInternal, CopyActiveSessionHandleOptions>(ref zero, options);
			IntPtr zero2 = IntPtr.Zero;
			Result result = Bindings.EOS_Sessions_CopyActiveSessionHandle(base.InnerHandle, zero, ref zero2);
			Helper.TryMarshalDispose(ref zero);
			Helper.TryMarshalGet<ActiveSession>(zero2, out outSessionHandle);
			return result;
		}

		// Token: 0x060008D0 RID: 2256 RVA: 0x000098B4 File Offset: 0x00007AB4
		public Result CopySessionHandleByInviteId(CopySessionHandleByInviteIdOptions options, out SessionDetails outSessionHandle)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<CopySessionHandleByInviteIdOptionsInternal, CopySessionHandleByInviteIdOptions>(ref zero, options);
			IntPtr zero2 = IntPtr.Zero;
			Result result = Bindings.EOS_Sessions_CopySessionHandleByInviteId(base.InnerHandle, zero, ref zero2);
			Helper.TryMarshalDispose(ref zero);
			Helper.TryMarshalGet<SessionDetails>(zero2, out outSessionHandle);
			return result;
		}

		// Token: 0x060008D1 RID: 2257 RVA: 0x000098F4 File Offset: 0x00007AF4
		public Result CopySessionHandleByUiEventId(CopySessionHandleByUiEventIdOptions options, out SessionDetails outSessionHandle)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<CopySessionHandleByUiEventIdOptionsInternal, CopySessionHandleByUiEventIdOptions>(ref zero, options);
			IntPtr zero2 = IntPtr.Zero;
			Result result = Bindings.EOS_Sessions_CopySessionHandleByUiEventId(base.InnerHandle, zero, ref zero2);
			Helper.TryMarshalDispose(ref zero);
			Helper.TryMarshalGet<SessionDetails>(zero2, out outSessionHandle);
			return result;
		}

		// Token: 0x060008D2 RID: 2258 RVA: 0x00009934 File Offset: 0x00007B34
		public Result CopySessionHandleForPresence(CopySessionHandleForPresenceOptions options, out SessionDetails outSessionHandle)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<CopySessionHandleForPresenceOptionsInternal, CopySessionHandleForPresenceOptions>(ref zero, options);
			IntPtr zero2 = IntPtr.Zero;
			Result result = Bindings.EOS_Sessions_CopySessionHandleForPresence(base.InnerHandle, zero, ref zero2);
			Helper.TryMarshalDispose(ref zero);
			Helper.TryMarshalGet<SessionDetails>(zero2, out outSessionHandle);
			return result;
		}

		// Token: 0x060008D3 RID: 2259 RVA: 0x00009974 File Offset: 0x00007B74
		public Result CreateSessionModification(CreateSessionModificationOptions options, out SessionModification outSessionModificationHandle)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<CreateSessionModificationOptionsInternal, CreateSessionModificationOptions>(ref zero, options);
			IntPtr zero2 = IntPtr.Zero;
			Result result = Bindings.EOS_Sessions_CreateSessionModification(base.InnerHandle, zero, ref zero2);
			Helper.TryMarshalDispose(ref zero);
			Helper.TryMarshalGet<SessionModification>(zero2, out outSessionModificationHandle);
			return result;
		}

		// Token: 0x060008D4 RID: 2260 RVA: 0x000099B4 File Offset: 0x00007BB4
		public Result CreateSessionSearch(CreateSessionSearchOptions options, out SessionSearch outSessionSearchHandle)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<CreateSessionSearchOptionsInternal, CreateSessionSearchOptions>(ref zero, options);
			IntPtr zero2 = IntPtr.Zero;
			Result result = Bindings.EOS_Sessions_CreateSessionSearch(base.InnerHandle, zero, ref zero2);
			Helper.TryMarshalDispose(ref zero);
			Helper.TryMarshalGet<SessionSearch>(zero2, out outSessionSearchHandle);
			return result;
		}

		// Token: 0x060008D5 RID: 2261 RVA: 0x000099F4 File Offset: 0x00007BF4
		public void DestroySession(DestroySessionOptions options, object clientData, OnDestroySessionCallback completionDelegate)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<DestroySessionOptionsInternal, DestroySessionOptions>(ref zero, options);
			IntPtr zero2 = IntPtr.Zero;
			OnDestroySessionCallbackInternal onDestroySessionCallbackInternal = new OnDestroySessionCallbackInternal(SessionsInterface.OnDestroySessionCallbackInternalImplementation);
			Helper.AddCallback(ref zero2, clientData, completionDelegate, onDestroySessionCallbackInternal, Array.Empty<Delegate>());
			Bindings.EOS_Sessions_DestroySession(base.InnerHandle, zero, zero2, onDestroySessionCallbackInternal);
			Helper.TryMarshalDispose(ref zero);
		}

		// Token: 0x060008D6 RID: 2262 RVA: 0x00009A48 File Offset: 0x00007C48
		public Result DumpSessionState(DumpSessionStateOptions options)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<DumpSessionStateOptionsInternal, DumpSessionStateOptions>(ref zero, options);
			Result result = Bindings.EOS_Sessions_DumpSessionState(base.InnerHandle, zero);
			Helper.TryMarshalDispose(ref zero);
			return result;
		}

		// Token: 0x060008D7 RID: 2263 RVA: 0x00009A78 File Offset: 0x00007C78
		public void EndSession(EndSessionOptions options, object clientData, OnEndSessionCallback completionDelegate)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<EndSessionOptionsInternal, EndSessionOptions>(ref zero, options);
			IntPtr zero2 = IntPtr.Zero;
			OnEndSessionCallbackInternal onEndSessionCallbackInternal = new OnEndSessionCallbackInternal(SessionsInterface.OnEndSessionCallbackInternalImplementation);
			Helper.AddCallback(ref zero2, clientData, completionDelegate, onEndSessionCallbackInternal, Array.Empty<Delegate>());
			Bindings.EOS_Sessions_EndSession(base.InnerHandle, zero, zero2, onEndSessionCallbackInternal);
			Helper.TryMarshalDispose(ref zero);
		}

		// Token: 0x060008D8 RID: 2264 RVA: 0x00009ACC File Offset: 0x00007CCC
		public uint GetInviteCount(GetInviteCountOptions options)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<GetInviteCountOptionsInternal, GetInviteCountOptions>(ref zero, options);
			uint result = Bindings.EOS_Sessions_GetInviteCount(base.InnerHandle, zero);
			Helper.TryMarshalDispose(ref zero);
			return result;
		}

		// Token: 0x060008D9 RID: 2265 RVA: 0x00009AFC File Offset: 0x00007CFC
		public Result GetInviteIdByIndex(GetInviteIdByIndexOptions options, out string outBuffer)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<GetInviteIdByIndexOptionsInternal, GetInviteIdByIndexOptions>(ref zero, options);
			IntPtr zero2 = IntPtr.Zero;
			int size = 65;
			Helper.TryMarshalAllocate(ref zero2, size);
			Result result = Bindings.EOS_Sessions_GetInviteIdByIndex(base.InnerHandle, zero, zero2, ref size);
			Helper.TryMarshalDispose(ref zero);
			Helper.TryMarshalGet(zero2, out outBuffer);
			Helper.TryMarshalDispose(ref zero2);
			return result;
		}

		// Token: 0x060008DA RID: 2266 RVA: 0x00009B54 File Offset: 0x00007D54
		public Result IsUserInSession(IsUserInSessionOptions options)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<IsUserInSessionOptionsInternal, IsUserInSessionOptions>(ref zero, options);
			Result result = Bindings.EOS_Sessions_IsUserInSession(base.InnerHandle, zero);
			Helper.TryMarshalDispose(ref zero);
			return result;
		}

		// Token: 0x060008DB RID: 2267 RVA: 0x00009B84 File Offset: 0x00007D84
		public void JoinSession(JoinSessionOptions options, object clientData, OnJoinSessionCallback completionDelegate)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<JoinSessionOptionsInternal, JoinSessionOptions>(ref zero, options);
			IntPtr zero2 = IntPtr.Zero;
			OnJoinSessionCallbackInternal onJoinSessionCallbackInternal = new OnJoinSessionCallbackInternal(SessionsInterface.OnJoinSessionCallbackInternalImplementation);
			Helper.AddCallback(ref zero2, clientData, completionDelegate, onJoinSessionCallbackInternal, Array.Empty<Delegate>());
			Bindings.EOS_Sessions_JoinSession(base.InnerHandle, zero, zero2, onJoinSessionCallbackInternal);
			Helper.TryMarshalDispose(ref zero);
		}

		// Token: 0x060008DC RID: 2268 RVA: 0x00009BD8 File Offset: 0x00007DD8
		public void QueryInvites(QueryInvitesOptions options, object clientData, OnQueryInvitesCallback completionDelegate)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<QueryInvitesOptionsInternal, QueryInvitesOptions>(ref zero, options);
			IntPtr zero2 = IntPtr.Zero;
			OnQueryInvitesCallbackInternal onQueryInvitesCallbackInternal = new OnQueryInvitesCallbackInternal(SessionsInterface.OnQueryInvitesCallbackInternalImplementation);
			Helper.AddCallback(ref zero2, clientData, completionDelegate, onQueryInvitesCallbackInternal, Array.Empty<Delegate>());
			Bindings.EOS_Sessions_QueryInvites(base.InnerHandle, zero, zero2, onQueryInvitesCallbackInternal);
			Helper.TryMarshalDispose(ref zero);
		}

		// Token: 0x060008DD RID: 2269 RVA: 0x00009C2C File Offset: 0x00007E2C
		public void RegisterPlayers(RegisterPlayersOptions options, object clientData, OnRegisterPlayersCallback completionDelegate)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<RegisterPlayersOptionsInternal, RegisterPlayersOptions>(ref zero, options);
			IntPtr zero2 = IntPtr.Zero;
			OnRegisterPlayersCallbackInternal onRegisterPlayersCallbackInternal = new OnRegisterPlayersCallbackInternal(SessionsInterface.OnRegisterPlayersCallbackInternalImplementation);
			Helper.AddCallback(ref zero2, clientData, completionDelegate, onRegisterPlayersCallbackInternal, Array.Empty<Delegate>());
			Bindings.EOS_Sessions_RegisterPlayers(base.InnerHandle, zero, zero2, onRegisterPlayersCallbackInternal);
			Helper.TryMarshalDispose(ref zero);
		}

		// Token: 0x060008DE RID: 2270 RVA: 0x00009C80 File Offset: 0x00007E80
		public void RejectInvite(RejectInviteOptions options, object clientData, OnRejectInviteCallback completionDelegate)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<RejectInviteOptionsInternal, RejectInviteOptions>(ref zero, options);
			IntPtr zero2 = IntPtr.Zero;
			OnRejectInviteCallbackInternal onRejectInviteCallbackInternal = new OnRejectInviteCallbackInternal(SessionsInterface.OnRejectInviteCallbackInternalImplementation);
			Helper.AddCallback(ref zero2, clientData, completionDelegate, onRejectInviteCallbackInternal, Array.Empty<Delegate>());
			Bindings.EOS_Sessions_RejectInvite(base.InnerHandle, zero, zero2, onRejectInviteCallbackInternal);
			Helper.TryMarshalDispose(ref zero);
		}

		// Token: 0x060008DF RID: 2271 RVA: 0x00009CD4 File Offset: 0x00007ED4
		public void RemoveNotifyJoinSessionAccepted(ulong inId)
		{
			Helper.TryRemoveCallbackByNotificationId(inId);
			Bindings.EOS_Sessions_RemoveNotifyJoinSessionAccepted(base.InnerHandle, inId);
		}

		// Token: 0x060008E0 RID: 2272 RVA: 0x00009CE9 File Offset: 0x00007EE9
		public void RemoveNotifySessionInviteAccepted(ulong inId)
		{
			Helper.TryRemoveCallbackByNotificationId(inId);
			Bindings.EOS_Sessions_RemoveNotifySessionInviteAccepted(base.InnerHandle, inId);
		}

		// Token: 0x060008E1 RID: 2273 RVA: 0x00009CFE File Offset: 0x00007EFE
		public void RemoveNotifySessionInviteReceived(ulong inId)
		{
			Helper.TryRemoveCallbackByNotificationId(inId);
			Bindings.EOS_Sessions_RemoveNotifySessionInviteReceived(base.InnerHandle, inId);
		}

		// Token: 0x060008E2 RID: 2274 RVA: 0x00009D14 File Offset: 0x00007F14
		public void SendInvite(SendInviteOptions options, object clientData, OnSendInviteCallback completionDelegate)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<SendInviteOptionsInternal, SendInviteOptions>(ref zero, options);
			IntPtr zero2 = IntPtr.Zero;
			OnSendInviteCallbackInternal onSendInviteCallbackInternal = new OnSendInviteCallbackInternal(SessionsInterface.OnSendInviteCallbackInternalImplementation);
			Helper.AddCallback(ref zero2, clientData, completionDelegate, onSendInviteCallbackInternal, Array.Empty<Delegate>());
			Bindings.EOS_Sessions_SendInvite(base.InnerHandle, zero, zero2, onSendInviteCallbackInternal);
			Helper.TryMarshalDispose(ref zero);
		}

		// Token: 0x060008E3 RID: 2275 RVA: 0x00009D68 File Offset: 0x00007F68
		public void StartSession(StartSessionOptions options, object clientData, OnStartSessionCallback completionDelegate)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<StartSessionOptionsInternal, StartSessionOptions>(ref zero, options);
			IntPtr zero2 = IntPtr.Zero;
			OnStartSessionCallbackInternal onStartSessionCallbackInternal = new OnStartSessionCallbackInternal(SessionsInterface.OnStartSessionCallbackInternalImplementation);
			Helper.AddCallback(ref zero2, clientData, completionDelegate, onStartSessionCallbackInternal, Array.Empty<Delegate>());
			Bindings.EOS_Sessions_StartSession(base.InnerHandle, zero, zero2, onStartSessionCallbackInternal);
			Helper.TryMarshalDispose(ref zero);
		}

		// Token: 0x060008E4 RID: 2276 RVA: 0x00009DBC File Offset: 0x00007FBC
		public void UnregisterPlayers(UnregisterPlayersOptions options, object clientData, OnUnregisterPlayersCallback completionDelegate)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<UnregisterPlayersOptionsInternal, UnregisterPlayersOptions>(ref zero, options);
			IntPtr zero2 = IntPtr.Zero;
			OnUnregisterPlayersCallbackInternal onUnregisterPlayersCallbackInternal = new OnUnregisterPlayersCallbackInternal(SessionsInterface.OnUnregisterPlayersCallbackInternalImplementation);
			Helper.AddCallback(ref zero2, clientData, completionDelegate, onUnregisterPlayersCallbackInternal, Array.Empty<Delegate>());
			Bindings.EOS_Sessions_UnregisterPlayers(base.InnerHandle, zero, zero2, onUnregisterPlayersCallbackInternal);
			Helper.TryMarshalDispose(ref zero);
		}

		// Token: 0x060008E5 RID: 2277 RVA: 0x00009E10 File Offset: 0x00008010
		public void UpdateSession(UpdateSessionOptions options, object clientData, OnUpdateSessionCallback completionDelegate)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<UpdateSessionOptionsInternal, UpdateSessionOptions>(ref zero, options);
			IntPtr zero2 = IntPtr.Zero;
			OnUpdateSessionCallbackInternal onUpdateSessionCallbackInternal = new OnUpdateSessionCallbackInternal(SessionsInterface.OnUpdateSessionCallbackInternalImplementation);
			Helper.AddCallback(ref zero2, clientData, completionDelegate, onUpdateSessionCallbackInternal, Array.Empty<Delegate>());
			Bindings.EOS_Sessions_UpdateSession(base.InnerHandle, zero, zero2, onUpdateSessionCallbackInternal);
			Helper.TryMarshalDispose(ref zero);
		}

		// Token: 0x060008E6 RID: 2278 RVA: 0x00009E64 File Offset: 0x00008064
		public Result UpdateSessionModification(UpdateSessionModificationOptions options, out SessionModification outSessionModificationHandle)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<UpdateSessionModificationOptionsInternal, UpdateSessionModificationOptions>(ref zero, options);
			IntPtr zero2 = IntPtr.Zero;
			Result result = Bindings.EOS_Sessions_UpdateSessionModification(base.InnerHandle, zero, ref zero2);
			Helper.TryMarshalDispose(ref zero);
			Helper.TryMarshalGet<SessionModification>(zero2, out outSessionModificationHandle);
			return result;
		}

		// Token: 0x060008E7 RID: 2279 RVA: 0x00009EA4 File Offset: 0x000080A4
		[MonoPInvokeCallback(typeof(OnDestroySessionCallbackInternal))]
		internal static void OnDestroySessionCallbackInternalImplementation(IntPtr data)
		{
			OnDestroySessionCallback onDestroySessionCallback;
			DestroySessionCallbackInfo data2;
			if (Helper.TryGetAndRemoveCallback<OnDestroySessionCallback, DestroySessionCallbackInfoInternal, DestroySessionCallbackInfo>(data, out onDestroySessionCallback, out data2))
			{
				onDestroySessionCallback(data2);
			}
		}

		// Token: 0x060008E8 RID: 2280 RVA: 0x00009EC4 File Offset: 0x000080C4
		[MonoPInvokeCallback(typeof(OnEndSessionCallbackInternal))]
		internal static void OnEndSessionCallbackInternalImplementation(IntPtr data)
		{
			OnEndSessionCallback onEndSessionCallback;
			EndSessionCallbackInfo data2;
			if (Helper.TryGetAndRemoveCallback<OnEndSessionCallback, EndSessionCallbackInfoInternal, EndSessionCallbackInfo>(data, out onEndSessionCallback, out data2))
			{
				onEndSessionCallback(data2);
			}
		}

		// Token: 0x060008E9 RID: 2281 RVA: 0x00009EE4 File Offset: 0x000080E4
		[MonoPInvokeCallback(typeof(OnJoinSessionAcceptedCallbackInternal))]
		internal static void OnJoinSessionAcceptedCallbackInternalImplementation(IntPtr data)
		{
			OnJoinSessionAcceptedCallback onJoinSessionAcceptedCallback;
			JoinSessionAcceptedCallbackInfo data2;
			if (Helper.TryGetAndRemoveCallback<OnJoinSessionAcceptedCallback, JoinSessionAcceptedCallbackInfoInternal, JoinSessionAcceptedCallbackInfo>(data, out onJoinSessionAcceptedCallback, out data2))
			{
				onJoinSessionAcceptedCallback(data2);
			}
		}

		// Token: 0x060008EA RID: 2282 RVA: 0x00009F04 File Offset: 0x00008104
		[MonoPInvokeCallback(typeof(OnJoinSessionCallbackInternal))]
		internal static void OnJoinSessionCallbackInternalImplementation(IntPtr data)
		{
			OnJoinSessionCallback onJoinSessionCallback;
			JoinSessionCallbackInfo data2;
			if (Helper.TryGetAndRemoveCallback<OnJoinSessionCallback, JoinSessionCallbackInfoInternal, JoinSessionCallbackInfo>(data, out onJoinSessionCallback, out data2))
			{
				onJoinSessionCallback(data2);
			}
		}

		// Token: 0x060008EB RID: 2283 RVA: 0x00009F24 File Offset: 0x00008124
		[MonoPInvokeCallback(typeof(OnQueryInvitesCallbackInternal))]
		internal static void OnQueryInvitesCallbackInternalImplementation(IntPtr data)
		{
			OnQueryInvitesCallback onQueryInvitesCallback;
			QueryInvitesCallbackInfo data2;
			if (Helper.TryGetAndRemoveCallback<OnQueryInvitesCallback, QueryInvitesCallbackInfoInternal, QueryInvitesCallbackInfo>(data, out onQueryInvitesCallback, out data2))
			{
				onQueryInvitesCallback(data2);
			}
		}

		// Token: 0x060008EC RID: 2284 RVA: 0x00009F44 File Offset: 0x00008144
		[MonoPInvokeCallback(typeof(OnRegisterPlayersCallbackInternal))]
		internal static void OnRegisterPlayersCallbackInternalImplementation(IntPtr data)
		{
			OnRegisterPlayersCallback onRegisterPlayersCallback;
			RegisterPlayersCallbackInfo data2;
			if (Helper.TryGetAndRemoveCallback<OnRegisterPlayersCallback, RegisterPlayersCallbackInfoInternal, RegisterPlayersCallbackInfo>(data, out onRegisterPlayersCallback, out data2))
			{
				onRegisterPlayersCallback(data2);
			}
		}

		// Token: 0x060008ED RID: 2285 RVA: 0x00009F64 File Offset: 0x00008164
		[MonoPInvokeCallback(typeof(OnRejectInviteCallbackInternal))]
		internal static void OnRejectInviteCallbackInternalImplementation(IntPtr data)
		{
			OnRejectInviteCallback onRejectInviteCallback;
			RejectInviteCallbackInfo data2;
			if (Helper.TryGetAndRemoveCallback<OnRejectInviteCallback, RejectInviteCallbackInfoInternal, RejectInviteCallbackInfo>(data, out onRejectInviteCallback, out data2))
			{
				onRejectInviteCallback(data2);
			}
		}

		// Token: 0x060008EE RID: 2286 RVA: 0x00009F84 File Offset: 0x00008184
		[MonoPInvokeCallback(typeof(OnSendInviteCallbackInternal))]
		internal static void OnSendInviteCallbackInternalImplementation(IntPtr data)
		{
			OnSendInviteCallback onSendInviteCallback;
			SendInviteCallbackInfo data2;
			if (Helper.TryGetAndRemoveCallback<OnSendInviteCallback, SendInviteCallbackInfoInternal, SendInviteCallbackInfo>(data, out onSendInviteCallback, out data2))
			{
				onSendInviteCallback(data2);
			}
		}

		// Token: 0x060008EF RID: 2287 RVA: 0x00009FA4 File Offset: 0x000081A4
		[MonoPInvokeCallback(typeof(OnSessionInviteAcceptedCallbackInternal))]
		internal static void OnSessionInviteAcceptedCallbackInternalImplementation(IntPtr data)
		{
			OnSessionInviteAcceptedCallback onSessionInviteAcceptedCallback;
			SessionInviteAcceptedCallbackInfo data2;
			if (Helper.TryGetAndRemoveCallback<OnSessionInviteAcceptedCallback, SessionInviteAcceptedCallbackInfoInternal, SessionInviteAcceptedCallbackInfo>(data, out onSessionInviteAcceptedCallback, out data2))
			{
				onSessionInviteAcceptedCallback(data2);
			}
		}

		// Token: 0x060008F0 RID: 2288 RVA: 0x00009FC4 File Offset: 0x000081C4
		[MonoPInvokeCallback(typeof(OnSessionInviteReceivedCallbackInternal))]
		internal static void OnSessionInviteReceivedCallbackInternalImplementation(IntPtr data)
		{
			OnSessionInviteReceivedCallback onSessionInviteReceivedCallback;
			SessionInviteReceivedCallbackInfo data2;
			if (Helper.TryGetAndRemoveCallback<OnSessionInviteReceivedCallback, SessionInviteReceivedCallbackInfoInternal, SessionInviteReceivedCallbackInfo>(data, out onSessionInviteReceivedCallback, out data2))
			{
				onSessionInviteReceivedCallback(data2);
			}
		}

		// Token: 0x060008F1 RID: 2289 RVA: 0x00009FE4 File Offset: 0x000081E4
		[MonoPInvokeCallback(typeof(OnStartSessionCallbackInternal))]
		internal static void OnStartSessionCallbackInternalImplementation(IntPtr data)
		{
			OnStartSessionCallback onStartSessionCallback;
			StartSessionCallbackInfo data2;
			if (Helper.TryGetAndRemoveCallback<OnStartSessionCallback, StartSessionCallbackInfoInternal, StartSessionCallbackInfo>(data, out onStartSessionCallback, out data2))
			{
				onStartSessionCallback(data2);
			}
		}

		// Token: 0x060008F2 RID: 2290 RVA: 0x0000A004 File Offset: 0x00008204
		[MonoPInvokeCallback(typeof(OnUnregisterPlayersCallbackInternal))]
		internal static void OnUnregisterPlayersCallbackInternalImplementation(IntPtr data)
		{
			OnUnregisterPlayersCallback onUnregisterPlayersCallback;
			UnregisterPlayersCallbackInfo data2;
			if (Helper.TryGetAndRemoveCallback<OnUnregisterPlayersCallback, UnregisterPlayersCallbackInfoInternal, UnregisterPlayersCallbackInfo>(data, out onUnregisterPlayersCallback, out data2))
			{
				onUnregisterPlayersCallback(data2);
			}
		}

		// Token: 0x060008F3 RID: 2291 RVA: 0x0000A024 File Offset: 0x00008224
		[MonoPInvokeCallback(typeof(OnUpdateSessionCallbackInternal))]
		internal static void OnUpdateSessionCallbackInternalImplementation(IntPtr data)
		{
			OnUpdateSessionCallback onUpdateSessionCallback;
			UpdateSessionCallbackInfo data2;
			if (Helper.TryGetAndRemoveCallback<OnUpdateSessionCallback, UpdateSessionCallbackInfoInternal, UpdateSessionCallbackInfo>(data, out onUpdateSessionCallback, out data2))
			{
				onUpdateSessionCallback(data2);
			}
		}

		// Token: 0x0400042C RID: 1068
		public const int AddnotifyjoinsessionacceptedApiLatest = 1;

		// Token: 0x0400042D RID: 1069
		public const int AddnotifysessioninviteacceptedApiLatest = 1;

		// Token: 0x0400042E RID: 1070
		public const int AddnotifysessioninvitereceivedApiLatest = 1;

		// Token: 0x0400042F RID: 1071
		public const int AttributedataApiLatest = 1;

		// Token: 0x04000430 RID: 1072
		public const int CopyactivesessionhandleApiLatest = 1;

		// Token: 0x04000431 RID: 1073
		public const int CopysessionhandlebyinviteidApiLatest = 1;

		// Token: 0x04000432 RID: 1074
		public const int CopysessionhandlebyuieventidApiLatest = 1;

		// Token: 0x04000433 RID: 1075
		public const int CopysessionhandleforpresenceApiLatest = 1;

		// Token: 0x04000434 RID: 1076
		public const int CreatesessionmodificationApiLatest = 4;

		// Token: 0x04000435 RID: 1077
		public const int CreatesessionsearchApiLatest = 1;

		// Token: 0x04000436 RID: 1078
		public const int DestroysessionApiLatest = 1;

		// Token: 0x04000437 RID: 1079
		public const int DumpsessionstateApiLatest = 1;

		// Token: 0x04000438 RID: 1080
		public const int EndsessionApiLatest = 1;

		// Token: 0x04000439 RID: 1081
		public const int GetinvitecountApiLatest = 1;

		// Token: 0x0400043A RID: 1082
		public const int GetinviteidbyindexApiLatest = 1;

		// Token: 0x0400043B RID: 1083
		public const int InviteidMaxLength = 64;

		// Token: 0x0400043C RID: 1084
		public const int IsuserinsessionApiLatest = 1;

		// Token: 0x0400043D RID: 1085
		public const int JoinsessionApiLatest = 2;

		// Token: 0x0400043E RID: 1086
		public const int MaxSearchResults = 200;

		// Token: 0x0400043F RID: 1087
		public const int Maxregisteredplayers = 1000;

		// Token: 0x04000440 RID: 1088
		public const int QueryinvitesApiLatest = 1;

		// Token: 0x04000441 RID: 1089
		public const int RegisterplayersApiLatest = 2;

		// Token: 0x04000442 RID: 1090
		public const int RejectinviteApiLatest = 1;

		// Token: 0x04000443 RID: 1091
		public const string SearchBucketId = "bucket";

		// Token: 0x04000444 RID: 1092
		public const string SearchEmptyServersOnly = "emptyonly";

		// Token: 0x04000445 RID: 1093
		public const string SearchMinslotsavailable = "minslotsavailable";

		// Token: 0x04000446 RID: 1094
		public const string SearchNonemptyServersOnly = "nonemptyonly";

		// Token: 0x04000447 RID: 1095
		public const int SendinviteApiLatest = 1;

		// Token: 0x04000448 RID: 1096
		public const int SessionattributeApiLatest = 1;

		// Token: 0x04000449 RID: 1097
		public const int SessionattributedataApiLatest = 1;

		// Token: 0x0400044A RID: 1098
		public const int StartsessionApiLatest = 1;

		// Token: 0x0400044B RID: 1099
		public const int UnregisterplayersApiLatest = 2;

		// Token: 0x0400044C RID: 1100
		public const int UpdatesessionApiLatest = 1;

		// Token: 0x0400044D RID: 1101
		public const int UpdatesessionmodificationApiLatest = 1;
	}
}
