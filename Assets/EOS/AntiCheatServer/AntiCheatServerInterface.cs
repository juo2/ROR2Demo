using System;
using Epic.OnlineServices.AntiCheatCommon;

namespace Epic.OnlineServices.AntiCheatServer
{
	// Token: 0x0200055A RID: 1370
	public sealed class AntiCheatServerInterface : Handle
	{
		// Token: 0x06002130 RID: 8496 RVA: 0x000036D3 File Offset: 0x000018D3
		public AntiCheatServerInterface()
		{
		}

		// Token: 0x06002131 RID: 8497 RVA: 0x000036DB File Offset: 0x000018DB
		public AntiCheatServerInterface(IntPtr innerHandle) : base(innerHandle)
		{
		}

		// Token: 0x06002132 RID: 8498 RVA: 0x00022F08 File Offset: 0x00021108
		public ulong AddNotifyClientActionRequired(AddNotifyClientActionRequiredOptions options, object clientData, OnClientActionRequiredCallback notificationFn)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<AddNotifyClientActionRequiredOptionsInternal, AddNotifyClientActionRequiredOptions>(ref zero, options);
			IntPtr zero2 = IntPtr.Zero;
			OnClientActionRequiredCallbackInternal onClientActionRequiredCallbackInternal = new OnClientActionRequiredCallbackInternal(AntiCheatServerInterface.OnClientActionRequiredCallbackInternalImplementation);
			Helper.AddCallback(ref zero2, clientData, notificationFn, onClientActionRequiredCallbackInternal, Array.Empty<Delegate>());
			ulong num = Bindings.EOS_AntiCheatServer_AddNotifyClientActionRequired(base.InnerHandle, zero, zero2, onClientActionRequiredCallbackInternal);
			Helper.TryMarshalDispose(ref zero);
			Helper.TryAssignNotificationIdToCallback(zero2, num);
			return num;
		}

		// Token: 0x06002133 RID: 8499 RVA: 0x00022F68 File Offset: 0x00021168
		public ulong AddNotifyClientAuthStatusChanged(AddNotifyClientAuthStatusChangedOptions options, object clientData, OnClientAuthStatusChangedCallback notificationFn)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<AddNotifyClientAuthStatusChangedOptionsInternal, AddNotifyClientAuthStatusChangedOptions>(ref zero, options);
			IntPtr zero2 = IntPtr.Zero;
			OnClientAuthStatusChangedCallbackInternal onClientAuthStatusChangedCallbackInternal = new OnClientAuthStatusChangedCallbackInternal(AntiCheatServerInterface.OnClientAuthStatusChangedCallbackInternalImplementation);
			Helper.AddCallback(ref zero2, clientData, notificationFn, onClientAuthStatusChangedCallbackInternal, Array.Empty<Delegate>());
			ulong num = Bindings.EOS_AntiCheatServer_AddNotifyClientAuthStatusChanged(base.InnerHandle, zero, zero2, onClientAuthStatusChangedCallbackInternal);
			Helper.TryMarshalDispose(ref zero);
			Helper.TryAssignNotificationIdToCallback(zero2, num);
			return num;
		}

		// Token: 0x06002134 RID: 8500 RVA: 0x00022FC8 File Offset: 0x000211C8
		public ulong AddNotifyMessageToClient(AddNotifyMessageToClientOptions options, object clientData, OnMessageToClientCallback notificationFn)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<AddNotifyMessageToClientOptionsInternal, AddNotifyMessageToClientOptions>(ref zero, options);
			IntPtr zero2 = IntPtr.Zero;
			OnMessageToClientCallbackInternal onMessageToClientCallbackInternal = new OnMessageToClientCallbackInternal(AntiCheatServerInterface.OnMessageToClientCallbackInternalImplementation);
			Helper.AddCallback(ref zero2, clientData, notificationFn, onMessageToClientCallbackInternal, Array.Empty<Delegate>());
			ulong num = Bindings.EOS_AntiCheatServer_AddNotifyMessageToClient(base.InnerHandle, zero, zero2, onMessageToClientCallbackInternal);
			Helper.TryMarshalDispose(ref zero);
			Helper.TryAssignNotificationIdToCallback(zero2, num);
			return num;
		}

		// Token: 0x06002135 RID: 8501 RVA: 0x00023028 File Offset: 0x00021228
		public Result BeginSession(BeginSessionOptions options)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<BeginSessionOptionsInternal, BeginSessionOptions>(ref zero, options);
			Result result = Bindings.EOS_AntiCheatServer_BeginSession(base.InnerHandle, zero);
			Helper.TryMarshalDispose(ref zero);
			return result;
		}

		// Token: 0x06002136 RID: 8502 RVA: 0x00023058 File Offset: 0x00021258
		public Result EndSession(EndSessionOptions options)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<EndSessionOptionsInternal, EndSessionOptions>(ref zero, options);
			Result result = Bindings.EOS_AntiCheatServer_EndSession(base.InnerHandle, zero);
			Helper.TryMarshalDispose(ref zero);
			return result;
		}

		// Token: 0x06002137 RID: 8503 RVA: 0x00023088 File Offset: 0x00021288
		public Result GetProtectMessageOutputLength(GetProtectMessageOutputLengthOptions options, out uint outBufferSizeBytes)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<GetProtectMessageOutputLengthOptionsInternal, GetProtectMessageOutputLengthOptions>(ref zero, options);
			outBufferSizeBytes = Helper.GetDefault<uint>();
			Result result = Bindings.EOS_AntiCheatServer_GetProtectMessageOutputLength(base.InnerHandle, zero, ref outBufferSizeBytes);
			Helper.TryMarshalDispose(ref zero);
			return result;
		}

		// Token: 0x06002138 RID: 8504 RVA: 0x000230C0 File Offset: 0x000212C0
		public Result LogEvent(LogEventOptions options)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<LogEventOptionsInternal, LogEventOptions>(ref zero, options);
			Result result = Bindings.EOS_AntiCheatServer_LogEvent(base.InnerHandle, zero);
			Helper.TryMarshalDispose(ref zero);
			return result;
		}

		// Token: 0x06002139 RID: 8505 RVA: 0x000230F0 File Offset: 0x000212F0
		public Result LogGameRoundEnd(LogGameRoundEndOptions options)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<LogGameRoundEndOptionsInternal, LogGameRoundEndOptions>(ref zero, options);
			Result result = Bindings.EOS_AntiCheatServer_LogGameRoundEnd(base.InnerHandle, zero);
			Helper.TryMarshalDispose(ref zero);
			return result;
		}

		// Token: 0x0600213A RID: 8506 RVA: 0x00023120 File Offset: 0x00021320
		public Result LogGameRoundStart(LogGameRoundStartOptions options)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<LogGameRoundStartOptionsInternal, LogGameRoundStartOptions>(ref zero, options);
			Result result = Bindings.EOS_AntiCheatServer_LogGameRoundStart(base.InnerHandle, zero);
			Helper.TryMarshalDispose(ref zero);
			return result;
		}

		// Token: 0x0600213B RID: 8507 RVA: 0x00023150 File Offset: 0x00021350
		public Result LogPlayerDespawn(LogPlayerDespawnOptions options)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<LogPlayerDespawnOptionsInternal, LogPlayerDespawnOptions>(ref zero, options);
			Result result = Bindings.EOS_AntiCheatServer_LogPlayerDespawn(base.InnerHandle, zero);
			Helper.TryMarshalDispose(ref zero);
			return result;
		}

		// Token: 0x0600213C RID: 8508 RVA: 0x00023180 File Offset: 0x00021380
		public Result LogPlayerRevive(LogPlayerReviveOptions options)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<LogPlayerReviveOptionsInternal, LogPlayerReviveOptions>(ref zero, options);
			Result result = Bindings.EOS_AntiCheatServer_LogPlayerRevive(base.InnerHandle, zero);
			Helper.TryMarshalDispose(ref zero);
			return result;
		}

		// Token: 0x0600213D RID: 8509 RVA: 0x000231B0 File Offset: 0x000213B0
		public Result LogPlayerSpawn(LogPlayerSpawnOptions options)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<LogPlayerSpawnOptionsInternal, LogPlayerSpawnOptions>(ref zero, options);
			Result result = Bindings.EOS_AntiCheatServer_LogPlayerSpawn(base.InnerHandle, zero);
			Helper.TryMarshalDispose(ref zero);
			return result;
		}

		// Token: 0x0600213E RID: 8510 RVA: 0x000231E0 File Offset: 0x000213E0
		public Result LogPlayerTakeDamage(LogPlayerTakeDamageOptions options)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<LogPlayerTakeDamageOptionsInternal, LogPlayerTakeDamageOptions>(ref zero, options);
			Result result = Bindings.EOS_AntiCheatServer_LogPlayerTakeDamage(base.InnerHandle, zero);
			Helper.TryMarshalDispose(ref zero);
			return result;
		}

		// Token: 0x0600213F RID: 8511 RVA: 0x00023210 File Offset: 0x00021410
		public Result LogPlayerTick(LogPlayerTickOptions options)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<LogPlayerTickOptionsInternal, LogPlayerTickOptions>(ref zero, options);
			Result result = Bindings.EOS_AntiCheatServer_LogPlayerTick(base.InnerHandle, zero);
			Helper.TryMarshalDispose(ref zero);
			return result;
		}

		// Token: 0x06002140 RID: 8512 RVA: 0x00023240 File Offset: 0x00021440
		public Result LogPlayerUseAbility(LogPlayerUseAbilityOptions options)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<LogPlayerUseAbilityOptionsInternal, LogPlayerUseAbilityOptions>(ref zero, options);
			Result result = Bindings.EOS_AntiCheatServer_LogPlayerUseAbility(base.InnerHandle, zero);
			Helper.TryMarshalDispose(ref zero);
			return result;
		}

		// Token: 0x06002141 RID: 8513 RVA: 0x00023270 File Offset: 0x00021470
		public Result LogPlayerUseWeapon(LogPlayerUseWeaponOptions options)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<LogPlayerUseWeaponOptionsInternal, LogPlayerUseWeaponOptions>(ref zero, options);
			Result result = Bindings.EOS_AntiCheatServer_LogPlayerUseWeapon(base.InnerHandle, zero);
			Helper.TryMarshalDispose(ref zero);
			return result;
		}

		// Token: 0x06002142 RID: 8514 RVA: 0x000232A0 File Offset: 0x000214A0
		public Result ProtectMessage(ProtectMessageOptions options, out byte[] outBuffer)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<ProtectMessageOptionsInternal, ProtectMessageOptions>(ref zero, options);
			IntPtr zero2 = IntPtr.Zero;
			uint outBufferSizeBytes = options.OutBufferSizeBytes;
			Helper.TryMarshalAllocate(ref zero2, outBufferSizeBytes);
			Result result = Bindings.EOS_AntiCheatServer_ProtectMessage(base.InnerHandle, zero, zero2, ref outBufferSizeBytes);
			Helper.TryMarshalDispose(ref zero);
			Helper.TryMarshalGet<byte>(zero2, out outBuffer, outBufferSizeBytes);
			Helper.TryMarshalDispose(ref zero2);
			return result;
		}

		// Token: 0x06002143 RID: 8515 RVA: 0x000232FC File Offset: 0x000214FC
		public Result ReceiveMessageFromClient(ReceiveMessageFromClientOptions options)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<ReceiveMessageFromClientOptionsInternal, ReceiveMessageFromClientOptions>(ref zero, options);
			Result result = Bindings.EOS_AntiCheatServer_ReceiveMessageFromClient(base.InnerHandle, zero);
			Helper.TryMarshalDispose(ref zero);
			return result;
		}

		// Token: 0x06002144 RID: 8516 RVA: 0x0002332C File Offset: 0x0002152C
		public Result RegisterClient(RegisterClientOptions options)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<RegisterClientOptionsInternal, RegisterClientOptions>(ref zero, options);
			Result result = Bindings.EOS_AntiCheatServer_RegisterClient(base.InnerHandle, zero);
			Helper.TryMarshalDispose(ref zero);
			return result;
		}

		// Token: 0x06002145 RID: 8517 RVA: 0x0002335C File Offset: 0x0002155C
		public Result RegisterEvent(RegisterEventOptions options)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<RegisterEventOptionsInternal, RegisterEventOptions>(ref zero, options);
			Result result = Bindings.EOS_AntiCheatServer_RegisterEvent(base.InnerHandle, zero);
			Helper.TryMarshalDispose(ref zero);
			return result;
		}

		// Token: 0x06002146 RID: 8518 RVA: 0x0002338C File Offset: 0x0002158C
		public void RemoveNotifyClientActionRequired(ulong notificationId)
		{
			Helper.TryRemoveCallbackByNotificationId(notificationId);
			Bindings.EOS_AntiCheatServer_RemoveNotifyClientActionRequired(base.InnerHandle, notificationId);
		}

		// Token: 0x06002147 RID: 8519 RVA: 0x000233A1 File Offset: 0x000215A1
		public void RemoveNotifyClientAuthStatusChanged(ulong notificationId)
		{
			Helper.TryRemoveCallbackByNotificationId(notificationId);
			Bindings.EOS_AntiCheatServer_RemoveNotifyClientAuthStatusChanged(base.InnerHandle, notificationId);
		}

		// Token: 0x06002148 RID: 8520 RVA: 0x000233B6 File Offset: 0x000215B6
		public void RemoveNotifyMessageToClient(ulong notificationId)
		{
			Helper.TryRemoveCallbackByNotificationId(notificationId);
			Bindings.EOS_AntiCheatServer_RemoveNotifyMessageToClient(base.InnerHandle, notificationId);
		}

		// Token: 0x06002149 RID: 8521 RVA: 0x000233CC File Offset: 0x000215CC
		public Result SetClientDetails(SetClientDetailsOptions options)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<SetClientDetailsOptionsInternal, SetClientDetailsOptions>(ref zero, options);
			Result result = Bindings.EOS_AntiCheatServer_SetClientDetails(base.InnerHandle, zero);
			Helper.TryMarshalDispose(ref zero);
			return result;
		}

		// Token: 0x0600214A RID: 8522 RVA: 0x000233FC File Offset: 0x000215FC
		public Result SetClientNetworkState(SetClientNetworkStateOptions options)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<SetClientNetworkStateOptionsInternal, SetClientNetworkStateOptions>(ref zero, options);
			Result result = Bindings.EOS_AntiCheatServer_SetClientNetworkState(base.InnerHandle, zero);
			Helper.TryMarshalDispose(ref zero);
			return result;
		}

		// Token: 0x0600214B RID: 8523 RVA: 0x0002342C File Offset: 0x0002162C
		public Result SetGameSessionId(SetGameSessionIdOptions options)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<SetGameSessionIdOptionsInternal, SetGameSessionIdOptions>(ref zero, options);
			Result result = Bindings.EOS_AntiCheatServer_SetGameSessionId(base.InnerHandle, zero);
			Helper.TryMarshalDispose(ref zero);
			return result;
		}

		// Token: 0x0600214C RID: 8524 RVA: 0x0002345C File Offset: 0x0002165C
		public Result UnprotectMessage(UnprotectMessageOptions options, out byte[] outBuffer)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<UnprotectMessageOptionsInternal, UnprotectMessageOptions>(ref zero, options);
			IntPtr zero2 = IntPtr.Zero;
			uint outBufferSizeBytes = options.OutBufferSizeBytes;
			Helper.TryMarshalAllocate(ref zero2, outBufferSizeBytes);
			Result result = Bindings.EOS_AntiCheatServer_UnprotectMessage(base.InnerHandle, zero, zero2, ref outBufferSizeBytes);
			Helper.TryMarshalDispose(ref zero);
			Helper.TryMarshalGet<byte>(zero2, out outBuffer, outBufferSizeBytes);
			Helper.TryMarshalDispose(ref zero2);
			return result;
		}

		// Token: 0x0600214D RID: 8525 RVA: 0x000234B8 File Offset: 0x000216B8
		public Result UnregisterClient(UnregisterClientOptions options)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<UnregisterClientOptionsInternal, UnregisterClientOptions>(ref zero, options);
			Result result = Bindings.EOS_AntiCheatServer_UnregisterClient(base.InnerHandle, zero);
			Helper.TryMarshalDispose(ref zero);
			return result;
		}

		// Token: 0x0600214E RID: 8526 RVA: 0x000234E8 File Offset: 0x000216E8
		[MonoPInvokeCallback(typeof(OnClientActionRequiredCallbackInternal))]
		internal static void OnClientActionRequiredCallbackInternalImplementation(IntPtr data)
		{
			OnClientActionRequiredCallback onClientActionRequiredCallback;
			OnClientActionRequiredCallbackInfo data2;
			if (Helper.TryGetAndRemoveCallback<OnClientActionRequiredCallback, OnClientActionRequiredCallbackInfoInternal, OnClientActionRequiredCallbackInfo>(data, out onClientActionRequiredCallback, out data2))
			{
				onClientActionRequiredCallback(data2);
			}
		}

		// Token: 0x0600214F RID: 8527 RVA: 0x00023508 File Offset: 0x00021708
		[MonoPInvokeCallback(typeof(OnClientAuthStatusChangedCallbackInternal))]
		internal static void OnClientAuthStatusChangedCallbackInternalImplementation(IntPtr data)
		{
			OnClientAuthStatusChangedCallback onClientAuthStatusChangedCallback;
			OnClientAuthStatusChangedCallbackInfo data2;
			if (Helper.TryGetAndRemoveCallback<OnClientAuthStatusChangedCallback, OnClientAuthStatusChangedCallbackInfoInternal, OnClientAuthStatusChangedCallbackInfo>(data, out onClientAuthStatusChangedCallback, out data2))
			{
				onClientAuthStatusChangedCallback(data2);
			}
		}

		// Token: 0x06002150 RID: 8528 RVA: 0x00023528 File Offset: 0x00021728
		[MonoPInvokeCallback(typeof(OnMessageToClientCallbackInternal))]
		internal static void OnMessageToClientCallbackInternalImplementation(IntPtr data)
		{
			OnMessageToClientCallback onMessageToClientCallback;
			OnMessageToClientCallbackInfo data2;
			if (Helper.TryGetAndRemoveCallback<OnMessageToClientCallback, OnMessageToClientCallbackInfoInternal, OnMessageToClientCallbackInfo>(data, out onMessageToClientCallback, out data2))
			{
				onMessageToClientCallback(data2);
			}
		}

		// Token: 0x04000F49 RID: 3913
		public const int AddnotifyclientactionrequiredApiLatest = 1;

		// Token: 0x04000F4A RID: 3914
		public const int AddnotifyclientauthstatuschangedApiLatest = 1;

		// Token: 0x04000F4B RID: 3915
		public const int AddnotifymessagetoclientApiLatest = 1;

		// Token: 0x04000F4C RID: 3916
		public const int BeginsessionApiLatest = 3;

		// Token: 0x04000F4D RID: 3917
		public const int BeginsessionMaxRegistertimeout = 120;

		// Token: 0x04000F4E RID: 3918
		public const int BeginsessionMinRegistertimeout = 10;

		// Token: 0x04000F4F RID: 3919
		public const int EndsessionApiLatest = 1;

		// Token: 0x04000F50 RID: 3920
		public const int GetprotectmessageoutputlengthApiLatest = 1;

		// Token: 0x04000F51 RID: 3921
		public const int ProtectmessageApiLatest = 1;

		// Token: 0x04000F52 RID: 3922
		public const int ReceivemessagefromclientApiLatest = 1;

		// Token: 0x04000F53 RID: 3923
		public const int RegisterclientApiLatest = 1;

		// Token: 0x04000F54 RID: 3924
		public const int SetclientnetworkstateApiLatest = 1;

		// Token: 0x04000F55 RID: 3925
		public const int UnprotectmessageApiLatest = 1;

		// Token: 0x04000F56 RID: 3926
		public const int UnregisterclientApiLatest = 1;
	}
}
