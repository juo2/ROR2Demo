using System;
using Epic.OnlineServices.AntiCheatCommon;

namespace Epic.OnlineServices.AntiCheatClient
{
	// Token: 0x020005B7 RID: 1463
	public sealed class AntiCheatClientInterface : Handle
	{
		// Token: 0x0600236E RID: 9070 RVA: 0x000255E6 File Offset: 0x000237E6
		public AntiCheatClientInterface()
		{
		}

		// Token: 0x0600236F RID: 9071 RVA: 0x000255FA File Offset: 0x000237FA
		public AntiCheatClientInterface(IntPtr innerHandle) : base(innerHandle)
		{
		}

		// Token: 0x06002370 RID: 9072 RVA: 0x00025610 File Offset: 0x00023810
		public Result AddExternalIntegrityCatalog(AddExternalIntegrityCatalogOptions options)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<AddExternalIntegrityCatalogOptionsInternal, AddExternalIntegrityCatalogOptions>(ref zero, options);
			Result result = Bindings.EOS_AntiCheatClient_AddExternalIntegrityCatalog(base.InnerHandle, zero);
			Helper.TryMarshalDispose(ref zero);
			return result;
		}

		// Token: 0x06002371 RID: 9073 RVA: 0x00025640 File Offset: 0x00023840
		public ulong AddNotifyMessageToPeer(AddNotifyMessageToPeerOptions options, object clientData, OnMessageToPeerCallback notificationFn)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<AddNotifyMessageToPeerOptionsInternal, AddNotifyMessageToPeerOptions>(ref zero, options);
			IntPtr zero2 = IntPtr.Zero;
			OnMessageToPeerCallbackInternal onMessageToPeerCallbackInternal = new OnMessageToPeerCallbackInternal(AntiCheatClientInterface.OnMessageToPeerCallbackInternalImplementation);
			Helper.AddCallback(ref zero2, clientData, notificationFn, onMessageToPeerCallbackInternal, Array.Empty<Delegate>());
			ulong num = Bindings.EOS_AntiCheatClient_AddNotifyMessageToPeer(base.InnerHandle, zero, zero2, onMessageToPeerCallbackInternal);
			Helper.TryMarshalDispose(ref zero);
			Helper.TryAssignNotificationIdToCallback(zero2, num);
			return num;
		}

		// Token: 0x06002372 RID: 9074 RVA: 0x000256A0 File Offset: 0x000238A0
		public ulong AddNotifyMessageToServer(AddNotifyMessageToServerOptions options, object clientData, OnMessageToServerCallback notificationFn)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<AddNotifyMessageToServerOptionsInternal, AddNotifyMessageToServerOptions>(ref zero, options);
			IntPtr zero2 = IntPtr.Zero;
			OnMessageToServerCallbackInternal onMessageToServerCallbackInternal = new OnMessageToServerCallbackInternal(AntiCheatClientInterface.OnMessageToServerCallbackInternalImplementation);
			Helper.AddCallback(ref zero2, clientData, notificationFn, onMessageToServerCallbackInternal, Array.Empty<Delegate>());
			ulong num = Bindings.EOS_AntiCheatClient_AddNotifyMessageToServer(base.InnerHandle, zero, zero2, onMessageToServerCallbackInternal);
			Helper.TryMarshalDispose(ref zero);
			Helper.TryAssignNotificationIdToCallback(zero2, num);
			return num;
		}

		// Token: 0x06002373 RID: 9075 RVA: 0x00025700 File Offset: 0x00023900
		public ulong AddNotifyPeerActionRequired(AddNotifyPeerActionRequiredOptions options, object clientData, OnPeerActionRequiredCallback notificationFn)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<AddNotifyPeerActionRequiredOptionsInternal, AddNotifyPeerActionRequiredOptions>(ref zero, options);
			IntPtr zero2 = IntPtr.Zero;
			OnPeerActionRequiredCallbackInternal onPeerActionRequiredCallbackInternal = new OnPeerActionRequiredCallbackInternal(AntiCheatClientInterface.OnPeerActionRequiredCallbackInternalImplementation);
			Helper.AddCallback(ref zero2, clientData, notificationFn, onPeerActionRequiredCallbackInternal, Array.Empty<Delegate>());
			ulong num = Bindings.EOS_AntiCheatClient_AddNotifyPeerActionRequired(base.InnerHandle, zero, zero2, onPeerActionRequiredCallbackInternal);
			Helper.TryMarshalDispose(ref zero);
			Helper.TryAssignNotificationIdToCallback(zero2, num);
			return num;
		}

		// Token: 0x06002374 RID: 9076 RVA: 0x00025760 File Offset: 0x00023960
		public ulong AddNotifyPeerAuthStatusChanged(AddNotifyPeerAuthStatusChangedOptions options, object clientData, OnPeerAuthStatusChangedCallback notificationFn)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<AddNotifyPeerAuthStatusChangedOptionsInternal, AddNotifyPeerAuthStatusChangedOptions>(ref zero, options);
			IntPtr zero2 = IntPtr.Zero;
			OnPeerAuthStatusChangedCallbackInternal onPeerAuthStatusChangedCallbackInternal = new OnPeerAuthStatusChangedCallbackInternal(AntiCheatClientInterface.OnPeerAuthStatusChangedCallbackInternalImplementation);
			Helper.AddCallback(ref zero2, clientData, notificationFn, onPeerAuthStatusChangedCallbackInternal, Array.Empty<Delegate>());
			ulong num = Bindings.EOS_AntiCheatClient_AddNotifyPeerAuthStatusChanged(base.InnerHandle, zero, zero2, onPeerAuthStatusChangedCallbackInternal);
			Helper.TryMarshalDispose(ref zero);
			Helper.TryAssignNotificationIdToCallback(zero2, num);
			return num;
		}

		// Token: 0x06002375 RID: 9077 RVA: 0x000257C0 File Offset: 0x000239C0
		public Result BeginSession(BeginSessionOptions options)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<BeginSessionOptionsInternal, BeginSessionOptions>(ref zero, options);
			Result result = Bindings.EOS_AntiCheatClient_BeginSession(base.InnerHandle, zero);
			Helper.TryMarshalDispose(ref zero);
			return result;
		}

		// Token: 0x06002376 RID: 9078 RVA: 0x000257F0 File Offset: 0x000239F0
		public Result EndSession(EndSessionOptions options)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<EndSessionOptionsInternal, EndSessionOptions>(ref zero, options);
			Result result = Bindings.EOS_AntiCheatClient_EndSession(base.InnerHandle, zero);
			Helper.TryMarshalDispose(ref zero);
			return result;
		}

		// Token: 0x06002377 RID: 9079 RVA: 0x00025820 File Offset: 0x00023A20
		public Result GetProtectMessageOutputLength(GetProtectMessageOutputLengthOptions options, out uint outBufferSizeBytes)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<GetProtectMessageOutputLengthOptionsInternal, GetProtectMessageOutputLengthOptions>(ref zero, options);
			outBufferSizeBytes = Helper.GetDefault<uint>();
			Result result = Bindings.EOS_AntiCheatClient_GetProtectMessageOutputLength(base.InnerHandle, zero, ref outBufferSizeBytes);
			Helper.TryMarshalDispose(ref zero);
			return result;
		}

		// Token: 0x06002378 RID: 9080 RVA: 0x00025858 File Offset: 0x00023A58
		public Result PollStatus(PollStatusOptions options, out AntiCheatClientViolationType outViolationType, out string outMessage)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<PollStatusOptionsInternal, PollStatusOptions>(ref zero, options);
			outViolationType = Helper.GetDefault<AntiCheatClientViolationType>();
			IntPtr zero2 = IntPtr.Zero;
			uint outMessageLength = options.OutMessageLength;
			Helper.TryMarshalAllocate(ref zero2, outMessageLength);
			Result result = Bindings.EOS_AntiCheatClient_PollStatus(base.InnerHandle, zero, ref outViolationType, zero2);
			Helper.TryMarshalDispose(ref zero);
			Helper.TryMarshalGet(zero2, out outMessage);
			Helper.TryMarshalDispose(ref zero2);
			return result;
		}

		// Token: 0x06002379 RID: 9081 RVA: 0x000258B8 File Offset: 0x00023AB8
		public Result ProtectMessage(ProtectMessageOptions options, out byte[] outBuffer)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<ProtectMessageOptionsInternal, ProtectMessageOptions>(ref zero, options);
			IntPtr zero2 = IntPtr.Zero;
			uint outBufferSizeBytes = options.OutBufferSizeBytes;
			Helper.TryMarshalAllocate(ref zero2, outBufferSizeBytes);
			Result result = Bindings.EOS_AntiCheatClient_ProtectMessage(base.InnerHandle, zero, zero2, ref outBufferSizeBytes);
			Helper.TryMarshalDispose(ref zero);
			Helper.TryMarshalGet<byte>(zero2, out outBuffer, outBufferSizeBytes);
			Helper.TryMarshalDispose(ref zero2);
			return result;
		}

		// Token: 0x0600237A RID: 9082 RVA: 0x00025914 File Offset: 0x00023B14
		public Result ReceiveMessageFromPeer(ReceiveMessageFromPeerOptions options)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<ReceiveMessageFromPeerOptionsInternal, ReceiveMessageFromPeerOptions>(ref zero, options);
			Result result = Bindings.EOS_AntiCheatClient_ReceiveMessageFromPeer(base.InnerHandle, zero);
			Helper.TryMarshalDispose(ref zero);
			return result;
		}

		// Token: 0x0600237B RID: 9083 RVA: 0x00025944 File Offset: 0x00023B44
		public Result ReceiveMessageFromServer(ReceiveMessageFromServerOptions options)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<ReceiveMessageFromServerOptionsInternal, ReceiveMessageFromServerOptions>(ref zero, options);
			Result result = Bindings.EOS_AntiCheatClient_ReceiveMessageFromServer(base.InnerHandle, zero);
			Helper.TryMarshalDispose(ref zero);
			return result;
		}

		// Token: 0x0600237C RID: 9084 RVA: 0x00025974 File Offset: 0x00023B74
		public Result RegisterPeer(RegisterPeerOptions options)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<RegisterPeerOptionsInternal, RegisterPeerOptions>(ref zero, options);
			Result result = Bindings.EOS_AntiCheatClient_RegisterPeer(base.InnerHandle, zero);
			Helper.TryMarshalDispose(ref zero);
			return result;
		}

		// Token: 0x0600237D RID: 9085 RVA: 0x000259A4 File Offset: 0x00023BA4
		public void RemoveNotifyMessageToPeer(ulong notificationId)
		{
			Helper.TryRemoveCallbackByNotificationId(notificationId);
			Bindings.EOS_AntiCheatClient_RemoveNotifyMessageToPeer(base.InnerHandle, notificationId);
		}

		// Token: 0x0600237E RID: 9086 RVA: 0x000259B9 File Offset: 0x00023BB9
		public void RemoveNotifyMessageToServer(ulong notificationId)
		{
			Helper.TryRemoveCallbackByNotificationId(notificationId);
			Bindings.EOS_AntiCheatClient_RemoveNotifyMessageToServer(base.InnerHandle, notificationId);
		}

		// Token: 0x0600237F RID: 9087 RVA: 0x000259CE File Offset: 0x00023BCE
		public void RemoveNotifyPeerActionRequired(ulong notificationId)
		{
			Helper.TryRemoveCallbackByNotificationId(notificationId);
			Bindings.EOS_AntiCheatClient_RemoveNotifyPeerActionRequired(base.InnerHandle, notificationId);
		}

		// Token: 0x06002380 RID: 9088 RVA: 0x000259E3 File Offset: 0x00023BE3
		public void RemoveNotifyPeerAuthStatusChanged(ulong notificationId)
		{
			Helper.TryRemoveCallbackByNotificationId(notificationId);
			Bindings.EOS_AntiCheatClient_RemoveNotifyPeerAuthStatusChanged(base.InnerHandle, notificationId);
		}

		// Token: 0x06002381 RID: 9089 RVA: 0x000259F8 File Offset: 0x00023BF8
		public Result UnprotectMessage(UnprotectMessageOptions options, out byte[] outBuffer)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<UnprotectMessageOptionsInternal, UnprotectMessageOptions>(ref zero, options);
			IntPtr zero2 = IntPtr.Zero;
			uint outBufferSizeBytes = options.OutBufferSizeBytes;
			Helper.TryMarshalAllocate(ref zero2, outBufferSizeBytes);
			Result result = Bindings.EOS_AntiCheatClient_UnprotectMessage(base.InnerHandle, zero, zero2, ref outBufferSizeBytes);
			Helper.TryMarshalDispose(ref zero);
			Helper.TryMarshalGet<byte>(zero2, out outBuffer, outBufferSizeBytes);
			Helper.TryMarshalDispose(ref zero2);
			return result;
		}

		// Token: 0x06002382 RID: 9090 RVA: 0x00025A54 File Offset: 0x00023C54
		public Result UnregisterPeer(UnregisterPeerOptions options)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<UnregisterPeerOptionsInternal, UnregisterPeerOptions>(ref zero, options);
			Result result = Bindings.EOS_AntiCheatClient_UnregisterPeer(base.InnerHandle, zero);
			Helper.TryMarshalDispose(ref zero);
			return result;
		}

		// Token: 0x06002383 RID: 9091 RVA: 0x00025A84 File Offset: 0x00023C84
		[MonoPInvokeCallback(typeof(OnMessageToPeerCallbackInternal))]
		internal static void OnMessageToPeerCallbackInternalImplementation(IntPtr data)
		{
			OnMessageToPeerCallback onMessageToPeerCallback;
			OnMessageToClientCallbackInfo data2;
			if (Helper.TryGetAndRemoveCallback<OnMessageToPeerCallback, OnMessageToClientCallbackInfoInternal, OnMessageToClientCallbackInfo>(data, out onMessageToPeerCallback, out data2))
			{
				onMessageToPeerCallback(data2);
			}
		}

		// Token: 0x06002384 RID: 9092 RVA: 0x00025AA4 File Offset: 0x00023CA4
		[MonoPInvokeCallback(typeof(OnMessageToServerCallbackInternal))]
		internal static void OnMessageToServerCallbackInternalImplementation(IntPtr data)
		{
			OnMessageToServerCallback onMessageToServerCallback;
			OnMessageToServerCallbackInfo data2;
			if (Helper.TryGetAndRemoveCallback<OnMessageToServerCallback, OnMessageToServerCallbackInfoInternal, OnMessageToServerCallbackInfo>(data, out onMessageToServerCallback, out data2))
			{
				onMessageToServerCallback(data2);
			}
		}

		// Token: 0x06002385 RID: 9093 RVA: 0x00025AC4 File Offset: 0x00023CC4
		[MonoPInvokeCallback(typeof(OnPeerActionRequiredCallbackInternal))]
		internal static void OnPeerActionRequiredCallbackInternalImplementation(IntPtr data)
		{
			OnPeerActionRequiredCallback onPeerActionRequiredCallback;
			OnClientActionRequiredCallbackInfo data2;
			if (Helper.TryGetAndRemoveCallback<OnPeerActionRequiredCallback, OnClientActionRequiredCallbackInfoInternal, OnClientActionRequiredCallbackInfo>(data, out onPeerActionRequiredCallback, out data2))
			{
				onPeerActionRequiredCallback(data2);
			}
		}

		// Token: 0x06002386 RID: 9094 RVA: 0x00025AE4 File Offset: 0x00023CE4
		[MonoPInvokeCallback(typeof(OnPeerAuthStatusChangedCallbackInternal))]
		internal static void OnPeerAuthStatusChangedCallbackInternalImplementation(IntPtr data)
		{
			OnPeerAuthStatusChangedCallback onPeerAuthStatusChangedCallback;
			OnClientAuthStatusChangedCallbackInfo data2;
			if (Helper.TryGetAndRemoveCallback<OnPeerAuthStatusChangedCallback, OnClientAuthStatusChangedCallbackInfoInternal, OnClientAuthStatusChangedCallbackInfo>(data, out onPeerAuthStatusChangedCallback, out data2))
			{
				onPeerAuthStatusChangedCallback(data2);
			}
		}

		// Token: 0x040010B1 RID: 4273
		public const int AddexternalintegritycatalogApiLatest = 1;

		// Token: 0x040010B2 RID: 4274
		public const int AddnotifymessagetopeerApiLatest = 1;

		// Token: 0x040010B3 RID: 4275
		public const int AddnotifymessagetoserverApiLatest = 1;

		// Token: 0x040010B4 RID: 4276
		public const int AddnotifypeeractionrequiredApiLatest = 1;

		// Token: 0x040010B5 RID: 4277
		public const int AddnotifypeerauthstatuschangedApiLatest = 1;

		// Token: 0x040010B6 RID: 4278
		public const int BeginsessionApiLatest = 3;

		// Token: 0x040010B7 RID: 4279
		public const int EndsessionApiLatest = 1;

		// Token: 0x040010B8 RID: 4280
		public const int GetprotectmessageoutputlengthApiLatest = 1;

		// Token: 0x040010B9 RID: 4281
		public IntPtr PeerSelf = (IntPtr)(-1);

		// Token: 0x040010BA RID: 4282
		public const int PollstatusApiLatest = 1;

		// Token: 0x040010BB RID: 4283
		public const int ProtectmessageApiLatest = 1;

		// Token: 0x040010BC RID: 4284
		public const int ReceivemessagefrompeerApiLatest = 1;

		// Token: 0x040010BD RID: 4285
		public const int ReceivemessagefromserverApiLatest = 1;

		// Token: 0x040010BE RID: 4286
		public const int RegisterpeerApiLatest = 1;

		// Token: 0x040010BF RID: 4287
		public const int UnprotectmessageApiLatest = 1;

		// Token: 0x040010C0 RID: 4288
		public const int UnregisterpeerApiLatest = 1;
	}
}
