using System;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x02000340 RID: 832
	public sealed class LobbyInterface : Handle
	{
		// Token: 0x0600149C RID: 5276 RVA: 0x000036D3 File Offset: 0x000018D3
		public LobbyInterface()
		{
		}

		// Token: 0x0600149D RID: 5277 RVA: 0x000036DB File Offset: 0x000018DB
		public LobbyInterface(IntPtr innerHandle) : base(innerHandle)
		{
		}

		// Token: 0x0600149E RID: 5278 RVA: 0x00015EEC File Offset: 0x000140EC
		public ulong AddNotifyJoinLobbyAccepted(AddNotifyJoinLobbyAcceptedOptions options, object clientData, OnJoinLobbyAcceptedCallback notificationFn)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<AddNotifyJoinLobbyAcceptedOptionsInternal, AddNotifyJoinLobbyAcceptedOptions>(ref zero, options);
			IntPtr zero2 = IntPtr.Zero;
			OnJoinLobbyAcceptedCallbackInternal onJoinLobbyAcceptedCallbackInternal = new OnJoinLobbyAcceptedCallbackInternal(LobbyInterface.OnJoinLobbyAcceptedCallbackInternalImplementation);
			Helper.AddCallback(ref zero2, clientData, notificationFn, onJoinLobbyAcceptedCallbackInternal, Array.Empty<Delegate>());
			ulong num = Bindings.EOS_Lobby_AddNotifyJoinLobbyAccepted(base.InnerHandle, zero, zero2, onJoinLobbyAcceptedCallbackInternal);
			Helper.TryMarshalDispose(ref zero);
			Helper.TryAssignNotificationIdToCallback(zero2, num);
			return num;
		}

		// Token: 0x0600149F RID: 5279 RVA: 0x00015F4C File Offset: 0x0001414C
		public ulong AddNotifyLobbyInviteAccepted(AddNotifyLobbyInviteAcceptedOptions options, object clientData, OnLobbyInviteAcceptedCallback notificationFn)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<AddNotifyLobbyInviteAcceptedOptionsInternal, AddNotifyLobbyInviteAcceptedOptions>(ref zero, options);
			IntPtr zero2 = IntPtr.Zero;
			OnLobbyInviteAcceptedCallbackInternal onLobbyInviteAcceptedCallbackInternal = new OnLobbyInviteAcceptedCallbackInternal(LobbyInterface.OnLobbyInviteAcceptedCallbackInternalImplementation);
			Helper.AddCallback(ref zero2, clientData, notificationFn, onLobbyInviteAcceptedCallbackInternal, Array.Empty<Delegate>());
			ulong num = Bindings.EOS_Lobby_AddNotifyLobbyInviteAccepted(base.InnerHandle, zero, zero2, onLobbyInviteAcceptedCallbackInternal);
			Helper.TryMarshalDispose(ref zero);
			Helper.TryAssignNotificationIdToCallback(zero2, num);
			return num;
		}

		// Token: 0x060014A0 RID: 5280 RVA: 0x00015FAC File Offset: 0x000141AC
		public ulong AddNotifyLobbyInviteReceived(AddNotifyLobbyInviteReceivedOptions options, object clientData, OnLobbyInviteReceivedCallback notificationFn)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<AddNotifyLobbyInviteReceivedOptionsInternal, AddNotifyLobbyInviteReceivedOptions>(ref zero, options);
			IntPtr zero2 = IntPtr.Zero;
			OnLobbyInviteReceivedCallbackInternal onLobbyInviteReceivedCallbackInternal = new OnLobbyInviteReceivedCallbackInternal(LobbyInterface.OnLobbyInviteReceivedCallbackInternalImplementation);
			Helper.AddCallback(ref zero2, clientData, notificationFn, onLobbyInviteReceivedCallbackInternal, Array.Empty<Delegate>());
			ulong num = Bindings.EOS_Lobby_AddNotifyLobbyInviteReceived(base.InnerHandle, zero, zero2, onLobbyInviteReceivedCallbackInternal);
			Helper.TryMarshalDispose(ref zero);
			Helper.TryAssignNotificationIdToCallback(zero2, num);
			return num;
		}

		// Token: 0x060014A1 RID: 5281 RVA: 0x0001600C File Offset: 0x0001420C
		public ulong AddNotifyLobbyMemberStatusReceived(AddNotifyLobbyMemberStatusReceivedOptions options, object clientData, OnLobbyMemberStatusReceivedCallback notificationFn)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<AddNotifyLobbyMemberStatusReceivedOptionsInternal, AddNotifyLobbyMemberStatusReceivedOptions>(ref zero, options);
			IntPtr zero2 = IntPtr.Zero;
			OnLobbyMemberStatusReceivedCallbackInternal onLobbyMemberStatusReceivedCallbackInternal = new OnLobbyMemberStatusReceivedCallbackInternal(LobbyInterface.OnLobbyMemberStatusReceivedCallbackInternalImplementation);
			Helper.AddCallback(ref zero2, clientData, notificationFn, onLobbyMemberStatusReceivedCallbackInternal, Array.Empty<Delegate>());
			ulong num = Bindings.EOS_Lobby_AddNotifyLobbyMemberStatusReceived(base.InnerHandle, zero, zero2, onLobbyMemberStatusReceivedCallbackInternal);
			Helper.TryMarshalDispose(ref zero);
			Helper.TryAssignNotificationIdToCallback(zero2, num);
			return num;
		}

		// Token: 0x060014A2 RID: 5282 RVA: 0x0001606C File Offset: 0x0001426C
		public ulong AddNotifyLobbyMemberUpdateReceived(AddNotifyLobbyMemberUpdateReceivedOptions options, object clientData, OnLobbyMemberUpdateReceivedCallback notificationFn)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<AddNotifyLobbyMemberUpdateReceivedOptionsInternal, AddNotifyLobbyMemberUpdateReceivedOptions>(ref zero, options);
			IntPtr zero2 = IntPtr.Zero;
			OnLobbyMemberUpdateReceivedCallbackInternal onLobbyMemberUpdateReceivedCallbackInternal = new OnLobbyMemberUpdateReceivedCallbackInternal(LobbyInterface.OnLobbyMemberUpdateReceivedCallbackInternalImplementation);
			Helper.AddCallback(ref zero2, clientData, notificationFn, onLobbyMemberUpdateReceivedCallbackInternal, Array.Empty<Delegate>());
			ulong num = Bindings.EOS_Lobby_AddNotifyLobbyMemberUpdateReceived(base.InnerHandle, zero, zero2, onLobbyMemberUpdateReceivedCallbackInternal);
			Helper.TryMarshalDispose(ref zero);
			Helper.TryAssignNotificationIdToCallback(zero2, num);
			return num;
		}

		// Token: 0x060014A3 RID: 5283 RVA: 0x000160CC File Offset: 0x000142CC
		public ulong AddNotifyLobbyUpdateReceived(AddNotifyLobbyUpdateReceivedOptions options, object clientData, OnLobbyUpdateReceivedCallback notificationFn)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<AddNotifyLobbyUpdateReceivedOptionsInternal, AddNotifyLobbyUpdateReceivedOptions>(ref zero, options);
			IntPtr zero2 = IntPtr.Zero;
			OnLobbyUpdateReceivedCallbackInternal onLobbyUpdateReceivedCallbackInternal = new OnLobbyUpdateReceivedCallbackInternal(LobbyInterface.OnLobbyUpdateReceivedCallbackInternalImplementation);
			Helper.AddCallback(ref zero2, clientData, notificationFn, onLobbyUpdateReceivedCallbackInternal, Array.Empty<Delegate>());
			ulong num = Bindings.EOS_Lobby_AddNotifyLobbyUpdateReceived(base.InnerHandle, zero, zero2, onLobbyUpdateReceivedCallbackInternal);
			Helper.TryMarshalDispose(ref zero);
			Helper.TryAssignNotificationIdToCallback(zero2, num);
			return num;
		}

		// Token: 0x060014A4 RID: 5284 RVA: 0x0001612C File Offset: 0x0001432C
		public ulong AddNotifyRTCRoomConnectionChanged(AddNotifyRTCRoomConnectionChangedOptions options, object clientData, OnRTCRoomConnectionChangedCallback notificationFn)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<AddNotifyRTCRoomConnectionChangedOptionsInternal, AddNotifyRTCRoomConnectionChangedOptions>(ref zero, options);
			IntPtr zero2 = IntPtr.Zero;
			OnRTCRoomConnectionChangedCallbackInternal onRTCRoomConnectionChangedCallbackInternal = new OnRTCRoomConnectionChangedCallbackInternal(LobbyInterface.OnRTCRoomConnectionChangedCallbackInternalImplementation);
			Helper.AddCallback(ref zero2, clientData, notificationFn, onRTCRoomConnectionChangedCallbackInternal, Array.Empty<Delegate>());
			ulong num = Bindings.EOS_Lobby_AddNotifyRTCRoomConnectionChanged(base.InnerHandle, zero, zero2, onRTCRoomConnectionChangedCallbackInternal);
			Helper.TryMarshalDispose(ref zero);
			Helper.TryAssignNotificationIdToCallback(zero2, num);
			return num;
		}

		// Token: 0x060014A5 RID: 5285 RVA: 0x0001618C File Offset: 0x0001438C
		public Result CopyLobbyDetailsHandle(CopyLobbyDetailsHandleOptions options, out LobbyDetails outLobbyDetailsHandle)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<CopyLobbyDetailsHandleOptionsInternal, CopyLobbyDetailsHandleOptions>(ref zero, options);
			IntPtr zero2 = IntPtr.Zero;
			Result result = Bindings.EOS_Lobby_CopyLobbyDetailsHandle(base.InnerHandle, zero, ref zero2);
			Helper.TryMarshalDispose(ref zero);
			Helper.TryMarshalGet<LobbyDetails>(zero2, out outLobbyDetailsHandle);
			return result;
		}

		// Token: 0x060014A6 RID: 5286 RVA: 0x000161CC File Offset: 0x000143CC
		public Result CopyLobbyDetailsHandleByInviteId(CopyLobbyDetailsHandleByInviteIdOptions options, out LobbyDetails outLobbyDetailsHandle)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<CopyLobbyDetailsHandleByInviteIdOptionsInternal, CopyLobbyDetailsHandleByInviteIdOptions>(ref zero, options);
			IntPtr zero2 = IntPtr.Zero;
			Result result = Bindings.EOS_Lobby_CopyLobbyDetailsHandleByInviteId(base.InnerHandle, zero, ref zero2);
			Helper.TryMarshalDispose(ref zero);
			Helper.TryMarshalGet<LobbyDetails>(zero2, out outLobbyDetailsHandle);
			return result;
		}

		// Token: 0x060014A7 RID: 5287 RVA: 0x0001620C File Offset: 0x0001440C
		public Result CopyLobbyDetailsHandleByUiEventId(CopyLobbyDetailsHandleByUiEventIdOptions options, out LobbyDetails outLobbyDetailsHandle)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<CopyLobbyDetailsHandleByUiEventIdOptionsInternal, CopyLobbyDetailsHandleByUiEventIdOptions>(ref zero, options);
			IntPtr zero2 = IntPtr.Zero;
			Result result = Bindings.EOS_Lobby_CopyLobbyDetailsHandleByUiEventId(base.InnerHandle, zero, ref zero2);
			Helper.TryMarshalDispose(ref zero);
			Helper.TryMarshalGet<LobbyDetails>(zero2, out outLobbyDetailsHandle);
			return result;
		}

		// Token: 0x060014A8 RID: 5288 RVA: 0x0001624C File Offset: 0x0001444C
		public void CreateLobby(CreateLobbyOptions options, object clientData, OnCreateLobbyCallback completionDelegate)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<CreateLobbyOptionsInternal, CreateLobbyOptions>(ref zero, options);
			IntPtr zero2 = IntPtr.Zero;
			OnCreateLobbyCallbackInternal onCreateLobbyCallbackInternal = new OnCreateLobbyCallbackInternal(LobbyInterface.OnCreateLobbyCallbackInternalImplementation);
			Helper.AddCallback(ref zero2, clientData, completionDelegate, onCreateLobbyCallbackInternal, Array.Empty<Delegate>());
			Bindings.EOS_Lobby_CreateLobby(base.InnerHandle, zero, zero2, onCreateLobbyCallbackInternal);
			Helper.TryMarshalDispose(ref zero);
		}

		// Token: 0x060014A9 RID: 5289 RVA: 0x000162A0 File Offset: 0x000144A0
		public Result CreateLobbySearch(CreateLobbySearchOptions options, out LobbySearch outLobbySearchHandle)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<CreateLobbySearchOptionsInternal, CreateLobbySearchOptions>(ref zero, options);
			IntPtr zero2 = IntPtr.Zero;
			Result result = Bindings.EOS_Lobby_CreateLobbySearch(base.InnerHandle, zero, ref zero2);
			Helper.TryMarshalDispose(ref zero);
			Helper.TryMarshalGet<LobbySearch>(zero2, out outLobbySearchHandle);
			return result;
		}

		// Token: 0x060014AA RID: 5290 RVA: 0x000162E0 File Offset: 0x000144E0
		public void DestroyLobby(DestroyLobbyOptions options, object clientData, OnDestroyLobbyCallback completionDelegate)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<DestroyLobbyOptionsInternal, DestroyLobbyOptions>(ref zero, options);
			IntPtr zero2 = IntPtr.Zero;
			OnDestroyLobbyCallbackInternal onDestroyLobbyCallbackInternal = new OnDestroyLobbyCallbackInternal(LobbyInterface.OnDestroyLobbyCallbackInternalImplementation);
			Helper.AddCallback(ref zero2, clientData, completionDelegate, onDestroyLobbyCallbackInternal, Array.Empty<Delegate>());
			Bindings.EOS_Lobby_DestroyLobby(base.InnerHandle, zero, zero2, onDestroyLobbyCallbackInternal);
			Helper.TryMarshalDispose(ref zero);
		}

		// Token: 0x060014AB RID: 5291 RVA: 0x00016334 File Offset: 0x00014534
		public uint GetInviteCount(GetInviteCountOptions options)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<GetInviteCountOptionsInternal, GetInviteCountOptions>(ref zero, options);
			uint result = Bindings.EOS_Lobby_GetInviteCount(base.InnerHandle, zero);
			Helper.TryMarshalDispose(ref zero);
			return result;
		}

		// Token: 0x060014AC RID: 5292 RVA: 0x00016364 File Offset: 0x00014564
		public Result GetInviteIdByIndex(GetInviteIdByIndexOptions options, out string outBuffer)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<GetInviteIdByIndexOptionsInternal, GetInviteIdByIndexOptions>(ref zero, options);
			IntPtr zero2 = IntPtr.Zero;
			int size = 65;
			Helper.TryMarshalAllocate(ref zero2, size);
			Result result = Bindings.EOS_Lobby_GetInviteIdByIndex(base.InnerHandle, zero, zero2, ref size);
			Helper.TryMarshalDispose(ref zero);
			Helper.TryMarshalGet(zero2, out outBuffer);
			Helper.TryMarshalDispose(ref zero2);
			return result;
		}

		// Token: 0x060014AD RID: 5293 RVA: 0x000163BC File Offset: 0x000145BC
		public Result GetRTCRoomName(GetRTCRoomNameOptions options, out string outBuffer)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<GetRTCRoomNameOptionsInternal, GetRTCRoomNameOptions>(ref zero, options);
			IntPtr zero2 = IntPtr.Zero;
			uint size = 256U;
			Helper.TryMarshalAllocate(ref zero2, size);
			Result result = Bindings.EOS_Lobby_GetRTCRoomName(base.InnerHandle, zero, zero2, ref size);
			Helper.TryMarshalDispose(ref zero);
			Helper.TryMarshalGet(zero2, out outBuffer);
			Helper.TryMarshalDispose(ref zero2);
			return result;
		}

		// Token: 0x060014AE RID: 5294 RVA: 0x00016414 File Offset: 0x00014614
		public Result IsRTCRoomConnected(IsRTCRoomConnectedOptions options, out bool bOutIsConnected)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<IsRTCRoomConnectedOptionsInternal, IsRTCRoomConnectedOptions>(ref zero, options);
			int source = 0;
			Result result = Bindings.EOS_Lobby_IsRTCRoomConnected(base.InnerHandle, zero, ref source);
			Helper.TryMarshalDispose(ref zero);
			Helper.TryMarshalGet(source, out bOutIsConnected);
			return result;
		}

		// Token: 0x060014AF RID: 5295 RVA: 0x00016450 File Offset: 0x00014650
		public void JoinLobby(JoinLobbyOptions options, object clientData, OnJoinLobbyCallback completionDelegate)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<JoinLobbyOptionsInternal, JoinLobbyOptions>(ref zero, options);
			IntPtr zero2 = IntPtr.Zero;
			OnJoinLobbyCallbackInternal onJoinLobbyCallbackInternal = new OnJoinLobbyCallbackInternal(LobbyInterface.OnJoinLobbyCallbackInternalImplementation);
			Helper.AddCallback(ref zero2, clientData, completionDelegate, onJoinLobbyCallbackInternal, Array.Empty<Delegate>());
			Bindings.EOS_Lobby_JoinLobby(base.InnerHandle, zero, zero2, onJoinLobbyCallbackInternal);
			Helper.TryMarshalDispose(ref zero);
		}

		// Token: 0x060014B0 RID: 5296 RVA: 0x000164A4 File Offset: 0x000146A4
		public void KickMember(KickMemberOptions options, object clientData, OnKickMemberCallback completionDelegate)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<KickMemberOptionsInternal, KickMemberOptions>(ref zero, options);
			IntPtr zero2 = IntPtr.Zero;
			OnKickMemberCallbackInternal onKickMemberCallbackInternal = new OnKickMemberCallbackInternal(LobbyInterface.OnKickMemberCallbackInternalImplementation);
			Helper.AddCallback(ref zero2, clientData, completionDelegate, onKickMemberCallbackInternal, Array.Empty<Delegate>());
			Bindings.EOS_Lobby_KickMember(base.InnerHandle, zero, zero2, onKickMemberCallbackInternal);
			Helper.TryMarshalDispose(ref zero);
		}

		// Token: 0x060014B1 RID: 5297 RVA: 0x000164F8 File Offset: 0x000146F8
		public void LeaveLobby(LeaveLobbyOptions options, object clientData, OnLeaveLobbyCallback completionDelegate)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<LeaveLobbyOptionsInternal, LeaveLobbyOptions>(ref zero, options);
			IntPtr zero2 = IntPtr.Zero;
			OnLeaveLobbyCallbackInternal onLeaveLobbyCallbackInternal = new OnLeaveLobbyCallbackInternal(LobbyInterface.OnLeaveLobbyCallbackInternalImplementation);
			Helper.AddCallback(ref zero2, clientData, completionDelegate, onLeaveLobbyCallbackInternal, Array.Empty<Delegate>());
			Bindings.EOS_Lobby_LeaveLobby(base.InnerHandle, zero, zero2, onLeaveLobbyCallbackInternal);
			Helper.TryMarshalDispose(ref zero);
		}

		// Token: 0x060014B2 RID: 5298 RVA: 0x0001654C File Offset: 0x0001474C
		public void PromoteMember(PromoteMemberOptions options, object clientData, OnPromoteMemberCallback completionDelegate)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<PromoteMemberOptionsInternal, PromoteMemberOptions>(ref zero, options);
			IntPtr zero2 = IntPtr.Zero;
			OnPromoteMemberCallbackInternal onPromoteMemberCallbackInternal = new OnPromoteMemberCallbackInternal(LobbyInterface.OnPromoteMemberCallbackInternalImplementation);
			Helper.AddCallback(ref zero2, clientData, completionDelegate, onPromoteMemberCallbackInternal, Array.Empty<Delegate>());
			Bindings.EOS_Lobby_PromoteMember(base.InnerHandle, zero, zero2, onPromoteMemberCallbackInternal);
			Helper.TryMarshalDispose(ref zero);
		}

		// Token: 0x060014B3 RID: 5299 RVA: 0x000165A0 File Offset: 0x000147A0
		public void QueryInvites(QueryInvitesOptions options, object clientData, OnQueryInvitesCallback completionDelegate)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<QueryInvitesOptionsInternal, QueryInvitesOptions>(ref zero, options);
			IntPtr zero2 = IntPtr.Zero;
			OnQueryInvitesCallbackInternal onQueryInvitesCallbackInternal = new OnQueryInvitesCallbackInternal(LobbyInterface.OnQueryInvitesCallbackInternalImplementation);
			Helper.AddCallback(ref zero2, clientData, completionDelegate, onQueryInvitesCallbackInternal, Array.Empty<Delegate>());
			Bindings.EOS_Lobby_QueryInvites(base.InnerHandle, zero, zero2, onQueryInvitesCallbackInternal);
			Helper.TryMarshalDispose(ref zero);
		}

		// Token: 0x060014B4 RID: 5300 RVA: 0x000165F4 File Offset: 0x000147F4
		public void RejectInvite(RejectInviteOptions options, object clientData, OnRejectInviteCallback completionDelegate)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<RejectInviteOptionsInternal, RejectInviteOptions>(ref zero, options);
			IntPtr zero2 = IntPtr.Zero;
			OnRejectInviteCallbackInternal onRejectInviteCallbackInternal = new OnRejectInviteCallbackInternal(LobbyInterface.OnRejectInviteCallbackInternalImplementation);
			Helper.AddCallback(ref zero2, clientData, completionDelegate, onRejectInviteCallbackInternal, Array.Empty<Delegate>());
			Bindings.EOS_Lobby_RejectInvite(base.InnerHandle, zero, zero2, onRejectInviteCallbackInternal);
			Helper.TryMarshalDispose(ref zero);
		}

		// Token: 0x060014B5 RID: 5301 RVA: 0x00016648 File Offset: 0x00014848
		public void RemoveNotifyJoinLobbyAccepted(ulong inId)
		{
			Helper.TryRemoveCallbackByNotificationId(inId);
			Bindings.EOS_Lobby_RemoveNotifyJoinLobbyAccepted(base.InnerHandle, inId);
		}

		// Token: 0x060014B6 RID: 5302 RVA: 0x0001665D File Offset: 0x0001485D
		public void RemoveNotifyLobbyInviteAccepted(ulong inId)
		{
			Helper.TryRemoveCallbackByNotificationId(inId);
			Bindings.EOS_Lobby_RemoveNotifyLobbyInviteAccepted(base.InnerHandle, inId);
		}

		// Token: 0x060014B7 RID: 5303 RVA: 0x00016672 File Offset: 0x00014872
		public void RemoveNotifyLobbyInviteReceived(ulong inId)
		{
			Helper.TryRemoveCallbackByNotificationId(inId);
			Bindings.EOS_Lobby_RemoveNotifyLobbyInviteReceived(base.InnerHandle, inId);
		}

		// Token: 0x060014B8 RID: 5304 RVA: 0x00016687 File Offset: 0x00014887
		public void RemoveNotifyLobbyMemberStatusReceived(ulong inId)
		{
			Helper.TryRemoveCallbackByNotificationId(inId);
			Bindings.EOS_Lobby_RemoveNotifyLobbyMemberStatusReceived(base.InnerHandle, inId);
		}

		// Token: 0x060014B9 RID: 5305 RVA: 0x0001669C File Offset: 0x0001489C
		public void RemoveNotifyLobbyMemberUpdateReceived(ulong inId)
		{
			Helper.TryRemoveCallbackByNotificationId(inId);
			Bindings.EOS_Lobby_RemoveNotifyLobbyMemberUpdateReceived(base.InnerHandle, inId);
		}

		// Token: 0x060014BA RID: 5306 RVA: 0x000166B1 File Offset: 0x000148B1
		public void RemoveNotifyLobbyUpdateReceived(ulong inId)
		{
			Helper.TryRemoveCallbackByNotificationId(inId);
			Bindings.EOS_Lobby_RemoveNotifyLobbyUpdateReceived(base.InnerHandle, inId);
		}

		// Token: 0x060014BB RID: 5307 RVA: 0x000166C6 File Offset: 0x000148C6
		public void RemoveNotifyRTCRoomConnectionChanged(ulong inId)
		{
			Helper.TryRemoveCallbackByNotificationId(inId);
			Bindings.EOS_Lobby_RemoveNotifyRTCRoomConnectionChanged(base.InnerHandle, inId);
		}

		// Token: 0x060014BC RID: 5308 RVA: 0x000166DC File Offset: 0x000148DC
		public void SendInvite(SendInviteOptions options, object clientData, OnSendInviteCallback completionDelegate)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<SendInviteOptionsInternal, SendInviteOptions>(ref zero, options);
			IntPtr zero2 = IntPtr.Zero;
			OnSendInviteCallbackInternal onSendInviteCallbackInternal = new OnSendInviteCallbackInternal(LobbyInterface.OnSendInviteCallbackInternalImplementation);
			Helper.AddCallback(ref zero2, clientData, completionDelegate, onSendInviteCallbackInternal, Array.Empty<Delegate>());
			Bindings.EOS_Lobby_SendInvite(base.InnerHandle, zero, zero2, onSendInviteCallbackInternal);
			Helper.TryMarshalDispose(ref zero);
		}

		// Token: 0x060014BD RID: 5309 RVA: 0x00016730 File Offset: 0x00014930
		public void UpdateLobby(UpdateLobbyOptions options, object clientData, OnUpdateLobbyCallback completionDelegate)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<UpdateLobbyOptionsInternal, UpdateLobbyOptions>(ref zero, options);
			IntPtr zero2 = IntPtr.Zero;
			OnUpdateLobbyCallbackInternal onUpdateLobbyCallbackInternal = new OnUpdateLobbyCallbackInternal(LobbyInterface.OnUpdateLobbyCallbackInternalImplementation);
			Helper.AddCallback(ref zero2, clientData, completionDelegate, onUpdateLobbyCallbackInternal, Array.Empty<Delegate>());
			Bindings.EOS_Lobby_UpdateLobby(base.InnerHandle, zero, zero2, onUpdateLobbyCallbackInternal);
			Helper.TryMarshalDispose(ref zero);
		}

		// Token: 0x060014BE RID: 5310 RVA: 0x00016784 File Offset: 0x00014984
		public Result UpdateLobbyModification(UpdateLobbyModificationOptions options, out LobbyModification outLobbyModificationHandle)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<UpdateLobbyModificationOptionsInternal, UpdateLobbyModificationOptions>(ref zero, options);
			IntPtr zero2 = IntPtr.Zero;
			Result result = Bindings.EOS_Lobby_UpdateLobbyModification(base.InnerHandle, zero, ref zero2);
			Helper.TryMarshalDispose(ref zero);
			Helper.TryMarshalGet<LobbyModification>(zero2, out outLobbyModificationHandle);
			return result;
		}

		// Token: 0x060014BF RID: 5311 RVA: 0x000167C4 File Offset: 0x000149C4
		[MonoPInvokeCallback(typeof(OnCreateLobbyCallbackInternal))]
		internal static void OnCreateLobbyCallbackInternalImplementation(IntPtr data)
		{
			OnCreateLobbyCallback onCreateLobbyCallback;
			CreateLobbyCallbackInfo data2;
			if (Helper.TryGetAndRemoveCallback<OnCreateLobbyCallback, CreateLobbyCallbackInfoInternal, CreateLobbyCallbackInfo>(data, out onCreateLobbyCallback, out data2))
			{
				onCreateLobbyCallback(data2);
			}
		}

		// Token: 0x060014C0 RID: 5312 RVA: 0x000167E4 File Offset: 0x000149E4
		[MonoPInvokeCallback(typeof(OnDestroyLobbyCallbackInternal))]
		internal static void OnDestroyLobbyCallbackInternalImplementation(IntPtr data)
		{
			OnDestroyLobbyCallback onDestroyLobbyCallback;
			DestroyLobbyCallbackInfo data2;
			if (Helper.TryGetAndRemoveCallback<OnDestroyLobbyCallback, DestroyLobbyCallbackInfoInternal, DestroyLobbyCallbackInfo>(data, out onDestroyLobbyCallback, out data2))
			{
				onDestroyLobbyCallback(data2);
			}
		}

		// Token: 0x060014C1 RID: 5313 RVA: 0x00016804 File Offset: 0x00014A04
		[MonoPInvokeCallback(typeof(OnJoinLobbyAcceptedCallbackInternal))]
		internal static void OnJoinLobbyAcceptedCallbackInternalImplementation(IntPtr data)
		{
			OnJoinLobbyAcceptedCallback onJoinLobbyAcceptedCallback;
			JoinLobbyAcceptedCallbackInfo data2;
			if (Helper.TryGetAndRemoveCallback<OnJoinLobbyAcceptedCallback, JoinLobbyAcceptedCallbackInfoInternal, JoinLobbyAcceptedCallbackInfo>(data, out onJoinLobbyAcceptedCallback, out data2))
			{
				onJoinLobbyAcceptedCallback(data2);
			}
		}

		// Token: 0x060014C2 RID: 5314 RVA: 0x00016824 File Offset: 0x00014A24
		[MonoPInvokeCallback(typeof(OnJoinLobbyCallbackInternal))]
		internal static void OnJoinLobbyCallbackInternalImplementation(IntPtr data)
		{
			OnJoinLobbyCallback onJoinLobbyCallback;
			JoinLobbyCallbackInfo data2;
			if (Helper.TryGetAndRemoveCallback<OnJoinLobbyCallback, JoinLobbyCallbackInfoInternal, JoinLobbyCallbackInfo>(data, out onJoinLobbyCallback, out data2))
			{
				onJoinLobbyCallback(data2);
			}
		}

		// Token: 0x060014C3 RID: 5315 RVA: 0x00016844 File Offset: 0x00014A44
		[MonoPInvokeCallback(typeof(OnKickMemberCallbackInternal))]
		internal static void OnKickMemberCallbackInternalImplementation(IntPtr data)
		{
			OnKickMemberCallback onKickMemberCallback;
			KickMemberCallbackInfo data2;
			if (Helper.TryGetAndRemoveCallback<OnKickMemberCallback, KickMemberCallbackInfoInternal, KickMemberCallbackInfo>(data, out onKickMemberCallback, out data2))
			{
				onKickMemberCallback(data2);
			}
		}

		// Token: 0x060014C4 RID: 5316 RVA: 0x00016864 File Offset: 0x00014A64
		[MonoPInvokeCallback(typeof(OnLeaveLobbyCallbackInternal))]
		internal static void OnLeaveLobbyCallbackInternalImplementation(IntPtr data)
		{
			OnLeaveLobbyCallback onLeaveLobbyCallback;
			LeaveLobbyCallbackInfo data2;
			if (Helper.TryGetAndRemoveCallback<OnLeaveLobbyCallback, LeaveLobbyCallbackInfoInternal, LeaveLobbyCallbackInfo>(data, out onLeaveLobbyCallback, out data2))
			{
				onLeaveLobbyCallback(data2);
			}
		}

		// Token: 0x060014C5 RID: 5317 RVA: 0x00016884 File Offset: 0x00014A84
		[MonoPInvokeCallback(typeof(OnLobbyInviteAcceptedCallbackInternal))]
		internal static void OnLobbyInviteAcceptedCallbackInternalImplementation(IntPtr data)
		{
			OnLobbyInviteAcceptedCallback onLobbyInviteAcceptedCallback;
			LobbyInviteAcceptedCallbackInfo data2;
			if (Helper.TryGetAndRemoveCallback<OnLobbyInviteAcceptedCallback, LobbyInviteAcceptedCallbackInfoInternal, LobbyInviteAcceptedCallbackInfo>(data, out onLobbyInviteAcceptedCallback, out data2))
			{
				onLobbyInviteAcceptedCallback(data2);
			}
		}

		// Token: 0x060014C6 RID: 5318 RVA: 0x000168A4 File Offset: 0x00014AA4
		[MonoPInvokeCallback(typeof(OnLobbyInviteReceivedCallbackInternal))]
		internal static void OnLobbyInviteReceivedCallbackInternalImplementation(IntPtr data)
		{
			OnLobbyInviteReceivedCallback onLobbyInviteReceivedCallback;
			LobbyInviteReceivedCallbackInfo data2;
			if (Helper.TryGetAndRemoveCallback<OnLobbyInviteReceivedCallback, LobbyInviteReceivedCallbackInfoInternal, LobbyInviteReceivedCallbackInfo>(data, out onLobbyInviteReceivedCallback, out data2))
			{
				onLobbyInviteReceivedCallback(data2);
			}
		}

		// Token: 0x060014C7 RID: 5319 RVA: 0x000168C4 File Offset: 0x00014AC4
		[MonoPInvokeCallback(typeof(OnLobbyMemberStatusReceivedCallbackInternal))]
		internal static void OnLobbyMemberStatusReceivedCallbackInternalImplementation(IntPtr data)
		{
			OnLobbyMemberStatusReceivedCallback onLobbyMemberStatusReceivedCallback;
			LobbyMemberStatusReceivedCallbackInfo data2;
			if (Helper.TryGetAndRemoveCallback<OnLobbyMemberStatusReceivedCallback, LobbyMemberStatusReceivedCallbackInfoInternal, LobbyMemberStatusReceivedCallbackInfo>(data, out onLobbyMemberStatusReceivedCallback, out data2))
			{
				onLobbyMemberStatusReceivedCallback(data2);
			}
		}

		// Token: 0x060014C8 RID: 5320 RVA: 0x000168E4 File Offset: 0x00014AE4
		[MonoPInvokeCallback(typeof(OnLobbyMemberUpdateReceivedCallbackInternal))]
		internal static void OnLobbyMemberUpdateReceivedCallbackInternalImplementation(IntPtr data)
		{
			OnLobbyMemberUpdateReceivedCallback onLobbyMemberUpdateReceivedCallback;
			LobbyMemberUpdateReceivedCallbackInfo data2;
			if (Helper.TryGetAndRemoveCallback<OnLobbyMemberUpdateReceivedCallback, LobbyMemberUpdateReceivedCallbackInfoInternal, LobbyMemberUpdateReceivedCallbackInfo>(data, out onLobbyMemberUpdateReceivedCallback, out data2))
			{
				onLobbyMemberUpdateReceivedCallback(data2);
			}
		}

		// Token: 0x060014C9 RID: 5321 RVA: 0x00016904 File Offset: 0x00014B04
		[MonoPInvokeCallback(typeof(OnLobbyUpdateReceivedCallbackInternal))]
		internal static void OnLobbyUpdateReceivedCallbackInternalImplementation(IntPtr data)
		{
			OnLobbyUpdateReceivedCallback onLobbyUpdateReceivedCallback;
			LobbyUpdateReceivedCallbackInfo data2;
			if (Helper.TryGetAndRemoveCallback<OnLobbyUpdateReceivedCallback, LobbyUpdateReceivedCallbackInfoInternal, LobbyUpdateReceivedCallbackInfo>(data, out onLobbyUpdateReceivedCallback, out data2))
			{
				onLobbyUpdateReceivedCallback(data2);
			}
		}

		// Token: 0x060014CA RID: 5322 RVA: 0x00016924 File Offset: 0x00014B24
		[MonoPInvokeCallback(typeof(OnPromoteMemberCallbackInternal))]
		internal static void OnPromoteMemberCallbackInternalImplementation(IntPtr data)
		{
			OnPromoteMemberCallback onPromoteMemberCallback;
			PromoteMemberCallbackInfo data2;
			if (Helper.TryGetAndRemoveCallback<OnPromoteMemberCallback, PromoteMemberCallbackInfoInternal, PromoteMemberCallbackInfo>(data, out onPromoteMemberCallback, out data2))
			{
				onPromoteMemberCallback(data2);
			}
		}

		// Token: 0x060014CB RID: 5323 RVA: 0x00016944 File Offset: 0x00014B44
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

		// Token: 0x060014CC RID: 5324 RVA: 0x00016964 File Offset: 0x00014B64
		[MonoPInvokeCallback(typeof(OnRTCRoomConnectionChangedCallbackInternal))]
		internal static void OnRTCRoomConnectionChangedCallbackInternalImplementation(IntPtr data)
		{
			OnRTCRoomConnectionChangedCallback onRTCRoomConnectionChangedCallback;
			RTCRoomConnectionChangedCallbackInfo data2;
			if (Helper.TryGetAndRemoveCallback<OnRTCRoomConnectionChangedCallback, RTCRoomConnectionChangedCallbackInfoInternal, RTCRoomConnectionChangedCallbackInfo>(data, out onRTCRoomConnectionChangedCallback, out data2))
			{
				onRTCRoomConnectionChangedCallback(data2);
			}
		}

		// Token: 0x060014CD RID: 5325 RVA: 0x00016984 File Offset: 0x00014B84
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

		// Token: 0x060014CE RID: 5326 RVA: 0x000169A4 File Offset: 0x00014BA4
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

		// Token: 0x060014CF RID: 5327 RVA: 0x000169C4 File Offset: 0x00014BC4
		[MonoPInvokeCallback(typeof(OnUpdateLobbyCallbackInternal))]
		internal static void OnUpdateLobbyCallbackInternalImplementation(IntPtr data)
		{
			OnUpdateLobbyCallback onUpdateLobbyCallback;
			UpdateLobbyCallbackInfo data2;
			if (Helper.TryGetAndRemoveCallback<OnUpdateLobbyCallback, UpdateLobbyCallbackInfoInternal, UpdateLobbyCallbackInfo>(data, out onUpdateLobbyCallback, out data2))
			{
				onUpdateLobbyCallback(data2);
			}
		}

		// Token: 0x040009D4 RID: 2516
		public const int AddnotifyjoinlobbyacceptedApiLatest = 1;

		// Token: 0x040009D5 RID: 2517
		public const int AddnotifylobbyinviteacceptedApiLatest = 1;

		// Token: 0x040009D6 RID: 2518
		public const int AddnotifylobbyinvitereceivedApiLatest = 1;

		// Token: 0x040009D7 RID: 2519
		public const int AddnotifylobbymemberstatusreceivedApiLatest = 1;

		// Token: 0x040009D8 RID: 2520
		public const int AddnotifylobbymemberupdatereceivedApiLatest = 1;

		// Token: 0x040009D9 RID: 2521
		public const int AddnotifylobbyupdatereceivedApiLatest = 1;

		// Token: 0x040009DA RID: 2522
		public const int AddnotifyrtcroomconnectionchangedApiLatest = 1;

		// Token: 0x040009DB RID: 2523
		public const int AttributeApiLatest = 1;

		// Token: 0x040009DC RID: 2524
		public const int AttributedataApiLatest = 1;

		// Token: 0x040009DD RID: 2525
		public const int CopylobbydetailshandleApiLatest = 1;

		// Token: 0x040009DE RID: 2526
		public const int CopylobbydetailshandlebyinviteidApiLatest = 1;

		// Token: 0x040009DF RID: 2527
		public const int CopylobbydetailshandlebyuieventidApiLatest = 1;

		// Token: 0x040009E0 RID: 2528
		public const int CreatelobbyApiLatest = 7;

		// Token: 0x040009E1 RID: 2529
		public const int CreatelobbysearchApiLatest = 1;

		// Token: 0x040009E2 RID: 2530
		public const int DestroylobbyApiLatest = 1;

		// Token: 0x040009E3 RID: 2531
		public const int GetinvitecountApiLatest = 1;

		// Token: 0x040009E4 RID: 2532
		public const int GetinviteidbyindexApiLatest = 1;

		// Token: 0x040009E5 RID: 2533
		public const int GetrtcroomnameApiLatest = 1;

		// Token: 0x040009E6 RID: 2534
		public const int InviteidMaxLength = 64;

		// Token: 0x040009E7 RID: 2535
		public const int IsrtcroomconnectedApiLatest = 1;

		// Token: 0x040009E8 RID: 2536
		public const int JoinlobbyApiLatest = 3;

		// Token: 0x040009E9 RID: 2537
		public const int KickmemberApiLatest = 1;

		// Token: 0x040009EA RID: 2538
		public const int LeavelobbyApiLatest = 1;

		// Token: 0x040009EB RID: 2539
		public const int LocalrtcoptionsApiLatest = 1;

		// Token: 0x040009EC RID: 2540
		public const int MaxLobbies = 16;

		// Token: 0x040009ED RID: 2541
		public const int MaxLobbyMembers = 64;

		// Token: 0x040009EE RID: 2542
		public const int MaxLobbyidoverrideLength = 60;

		// Token: 0x040009EF RID: 2543
		public const int MaxSearchResults = 200;

		// Token: 0x040009F0 RID: 2544
		public const int MinLobbyidoverrideLength = 4;

		// Token: 0x040009F1 RID: 2545
		public const int PromotememberApiLatest = 1;

		// Token: 0x040009F2 RID: 2546
		public const int QueryinvitesApiLatest = 1;

		// Token: 0x040009F3 RID: 2547
		public const int RejectinviteApiLatest = 1;

		// Token: 0x040009F4 RID: 2548
		public const string SearchBucketId = "bucket";

		// Token: 0x040009F5 RID: 2549
		public const string SearchMincurrentmembers = "mincurrentmembers";

		// Token: 0x040009F6 RID: 2550
		public const string SearchMinslotsavailable = "minslotsavailable";

		// Token: 0x040009F7 RID: 2551
		public const int SendinviteApiLatest = 1;

		// Token: 0x040009F8 RID: 2552
		public const int UpdatelobbyApiLatest = 1;

		// Token: 0x040009F9 RID: 2553
		public const int UpdatelobbymodificationApiLatest = 1;
	}
}
