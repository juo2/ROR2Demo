using System;

namespace Epic.OnlineServices.P2P
{
	// Token: 0x020002A9 RID: 681
	public sealed class P2PInterface : Handle
	{
		// Token: 0x06001130 RID: 4400 RVA: 0x000036D3 File Offset: 0x000018D3
		public P2PInterface()
		{
		}

		// Token: 0x06001131 RID: 4401 RVA: 0x000036DB File Offset: 0x000018DB
		public P2PInterface(IntPtr innerHandle) : base(innerHandle)
		{
		}

		// Token: 0x06001132 RID: 4402 RVA: 0x00012290 File Offset: 0x00010490
		public Result AcceptConnection(AcceptConnectionOptions options)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<AcceptConnectionOptionsInternal, AcceptConnectionOptions>(ref zero, options);
			Result result = Bindings.EOS_P2P_AcceptConnection(base.InnerHandle, zero);
			Helper.TryMarshalDispose(ref zero);
			return result;
		}

		// Token: 0x06001133 RID: 4403 RVA: 0x000122C0 File Offset: 0x000104C0
		public ulong AddNotifyIncomingPacketQueueFull(AddNotifyIncomingPacketQueueFullOptions options, object clientData, OnIncomingPacketQueueFullCallback incomingPacketQueueFullHandler)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<AddNotifyIncomingPacketQueueFullOptionsInternal, AddNotifyIncomingPacketQueueFullOptions>(ref zero, options);
			IntPtr zero2 = IntPtr.Zero;
			OnIncomingPacketQueueFullCallbackInternal onIncomingPacketQueueFullCallbackInternal = new OnIncomingPacketQueueFullCallbackInternal(P2PInterface.OnIncomingPacketQueueFullCallbackInternalImplementation);
			Helper.AddCallback(ref zero2, clientData, incomingPacketQueueFullHandler, onIncomingPacketQueueFullCallbackInternal, Array.Empty<Delegate>());
			ulong num = Bindings.EOS_P2P_AddNotifyIncomingPacketQueueFull(base.InnerHandle, zero, zero2, onIncomingPacketQueueFullCallbackInternal);
			Helper.TryMarshalDispose(ref zero);
			Helper.TryAssignNotificationIdToCallback(zero2, num);
			return num;
		}

		// Token: 0x06001134 RID: 4404 RVA: 0x00012320 File Offset: 0x00010520
		public ulong AddNotifyPeerConnectionClosed(AddNotifyPeerConnectionClosedOptions options, object clientData, OnRemoteConnectionClosedCallback connectionClosedHandler)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<AddNotifyPeerConnectionClosedOptionsInternal, AddNotifyPeerConnectionClosedOptions>(ref zero, options);
			IntPtr zero2 = IntPtr.Zero;
			OnRemoteConnectionClosedCallbackInternal onRemoteConnectionClosedCallbackInternal = new OnRemoteConnectionClosedCallbackInternal(P2PInterface.OnRemoteConnectionClosedCallbackInternalImplementation);
			Helper.AddCallback(ref zero2, clientData, connectionClosedHandler, onRemoteConnectionClosedCallbackInternal, Array.Empty<Delegate>());
			ulong num = Bindings.EOS_P2P_AddNotifyPeerConnectionClosed(base.InnerHandle, zero, zero2, onRemoteConnectionClosedCallbackInternal);
			Helper.TryMarshalDispose(ref zero);
			Helper.TryAssignNotificationIdToCallback(zero2, num);
			return num;
		}

		// Token: 0x06001135 RID: 4405 RVA: 0x00012380 File Offset: 0x00010580
		public ulong AddNotifyPeerConnectionEstablished(AddNotifyPeerConnectionEstablishedOptions options, object clientData, OnPeerConnectionEstablishedCallback connectionEstablishedHandler)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<AddNotifyPeerConnectionEstablishedOptionsInternal, AddNotifyPeerConnectionEstablishedOptions>(ref zero, options);
			IntPtr zero2 = IntPtr.Zero;
			OnPeerConnectionEstablishedCallbackInternal onPeerConnectionEstablishedCallbackInternal = new OnPeerConnectionEstablishedCallbackInternal(P2PInterface.OnPeerConnectionEstablishedCallbackInternalImplementation);
			Helper.AddCallback(ref zero2, clientData, connectionEstablishedHandler, onPeerConnectionEstablishedCallbackInternal, Array.Empty<Delegate>());
			ulong num = Bindings.EOS_P2P_AddNotifyPeerConnectionEstablished(base.InnerHandle, zero, zero2, onPeerConnectionEstablishedCallbackInternal);
			Helper.TryMarshalDispose(ref zero);
			Helper.TryAssignNotificationIdToCallback(zero2, num);
			return num;
		}

		// Token: 0x06001136 RID: 4406 RVA: 0x000123E0 File Offset: 0x000105E0
		public ulong AddNotifyPeerConnectionRequest(AddNotifyPeerConnectionRequestOptions options, object clientData, OnIncomingConnectionRequestCallback connectionRequestHandler)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<AddNotifyPeerConnectionRequestOptionsInternal, AddNotifyPeerConnectionRequestOptions>(ref zero, options);
			IntPtr zero2 = IntPtr.Zero;
			OnIncomingConnectionRequestCallbackInternal onIncomingConnectionRequestCallbackInternal = new OnIncomingConnectionRequestCallbackInternal(P2PInterface.OnIncomingConnectionRequestCallbackInternalImplementation);
			Helper.AddCallback(ref zero2, clientData, connectionRequestHandler, onIncomingConnectionRequestCallbackInternal, Array.Empty<Delegate>());
			ulong num = Bindings.EOS_P2P_AddNotifyPeerConnectionRequest(base.InnerHandle, zero, zero2, onIncomingConnectionRequestCallbackInternal);
			Helper.TryMarshalDispose(ref zero);
			Helper.TryAssignNotificationIdToCallback(zero2, num);
			return num;
		}

		// Token: 0x06001137 RID: 4407 RVA: 0x00012440 File Offset: 0x00010640
		public Result ClearPacketQueue(ClearPacketQueueOptions options)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<ClearPacketQueueOptionsInternal, ClearPacketQueueOptions>(ref zero, options);
			Result result = Bindings.EOS_P2P_ClearPacketQueue(base.InnerHandle, zero);
			Helper.TryMarshalDispose(ref zero);
			return result;
		}

		// Token: 0x06001138 RID: 4408 RVA: 0x00012470 File Offset: 0x00010670
		public Result CloseConnection(CloseConnectionOptions options)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<CloseConnectionOptionsInternal, CloseConnectionOptions>(ref zero, options);
			Result result = Bindings.EOS_P2P_CloseConnection(base.InnerHandle, zero);
			Helper.TryMarshalDispose(ref zero);
			return result;
		}

		// Token: 0x06001139 RID: 4409 RVA: 0x000124A0 File Offset: 0x000106A0
		public Result CloseConnections(CloseConnectionsOptions options)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<CloseConnectionsOptionsInternal, CloseConnectionsOptions>(ref zero, options);
			Result result = Bindings.EOS_P2P_CloseConnections(base.InnerHandle, zero);
			Helper.TryMarshalDispose(ref zero);
			return result;
		}

		// Token: 0x0600113A RID: 4410 RVA: 0x000124D0 File Offset: 0x000106D0
		public Result GetNATType(GetNATTypeOptions options, out NATType outNATType)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<GetNATTypeOptionsInternal, GetNATTypeOptions>(ref zero, options);
			outNATType = Helper.GetDefault<NATType>();
			Result result = Bindings.EOS_P2P_GetNATType(base.InnerHandle, zero, ref outNATType);
			Helper.TryMarshalDispose(ref zero);
			return result;
		}

		// Token: 0x0600113B RID: 4411 RVA: 0x00012508 File Offset: 0x00010708
		public Result GetNextReceivedPacketSize(GetNextReceivedPacketSizeOptions options, out uint outPacketSizeBytes)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<GetNextReceivedPacketSizeOptionsInternal, GetNextReceivedPacketSizeOptions>(ref zero, options);
			outPacketSizeBytes = Helper.GetDefault<uint>();
			Result result = Bindings.EOS_P2P_GetNextReceivedPacketSize(base.InnerHandle, zero, ref outPacketSizeBytes);
			Helper.TryMarshalDispose(ref zero);
			return result;
		}

		// Token: 0x0600113C RID: 4412 RVA: 0x00012540 File Offset: 0x00010740
		public Result GetPacketQueueInfo(GetPacketQueueInfoOptions options, out PacketQueueInfo outPacketQueueInfo)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<GetPacketQueueInfoOptionsInternal, GetPacketQueueInfoOptions>(ref zero, options);
			PacketQueueInfoInternal @default = Helper.GetDefault<PacketQueueInfoInternal>();
			Result result = Bindings.EOS_P2P_GetPacketQueueInfo(base.InnerHandle, zero, ref @default);
			Helper.TryMarshalDispose(ref zero);
			Helper.TryMarshalGet<PacketQueueInfoInternal, PacketQueueInfo>(@default, out outPacketQueueInfo);
			return result;
		}

		// Token: 0x0600113D RID: 4413 RVA: 0x00012580 File Offset: 0x00010780
		public Result GetPortRange(GetPortRangeOptions options, out ushort outPort, out ushort outNumAdditionalPortsToTry)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<GetPortRangeOptionsInternal, GetPortRangeOptions>(ref zero, options);
			outPort = Helper.GetDefault<ushort>();
			outNumAdditionalPortsToTry = Helper.GetDefault<ushort>();
			Result result = Bindings.EOS_P2P_GetPortRange(base.InnerHandle, zero, ref outPort, ref outNumAdditionalPortsToTry);
			Helper.TryMarshalDispose(ref zero);
			return result;
		}

		// Token: 0x0600113E RID: 4414 RVA: 0x000125C0 File Offset: 0x000107C0
		public Result GetRelayControl(GetRelayControlOptions options, out RelayControl outRelayControl)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<GetRelayControlOptionsInternal, GetRelayControlOptions>(ref zero, options);
			outRelayControl = Helper.GetDefault<RelayControl>();
			Result result = Bindings.EOS_P2P_GetRelayControl(base.InnerHandle, zero, ref outRelayControl);
			Helper.TryMarshalDispose(ref zero);
			return result;
		}

		// Token: 0x0600113F RID: 4415 RVA: 0x000125F8 File Offset: 0x000107F8
		public void QueryNATType(QueryNATTypeOptions options, object clientData, OnQueryNATTypeCompleteCallback completionDelegate)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<QueryNATTypeOptionsInternal, QueryNATTypeOptions>(ref zero, options);
			IntPtr zero2 = IntPtr.Zero;
			OnQueryNATTypeCompleteCallbackInternal onQueryNATTypeCompleteCallbackInternal = new OnQueryNATTypeCompleteCallbackInternal(P2PInterface.OnQueryNATTypeCompleteCallbackInternalImplementation);
			Helper.AddCallback(ref zero2, clientData, completionDelegate, onQueryNATTypeCompleteCallbackInternal, Array.Empty<Delegate>());
			Bindings.EOS_P2P_QueryNATType(base.InnerHandle, zero, zero2, onQueryNATTypeCompleteCallbackInternal);
			Helper.TryMarshalDispose(ref zero);
		}

		// Token: 0x06001140 RID: 4416 RVA: 0x0001264C File Offset: 0x0001084C
		public Result ReceivePacket(ReceivePacketOptions options, out ProductUserId outPeerId, out SocketId outSocketId, out byte outChannel, out byte[] outData)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<ReceivePacketOptionsInternal, ReceivePacketOptions>(ref zero, options);
			IntPtr zero2 = IntPtr.Zero;
			SocketIdInternal @default = Helper.GetDefault<SocketIdInternal>();
			outChannel = Helper.GetDefault<byte>();
			IntPtr zero3 = IntPtr.Zero;
			uint num = 1170U;
			Helper.TryMarshalAllocate(ref zero3, num);
			Result result = Bindings.EOS_P2P_ReceivePacket(base.InnerHandle, zero, ref zero2, ref @default, ref outChannel, zero3, ref num);
			Helper.TryMarshalDispose(ref zero);
			Helper.TryMarshalGet<ProductUserId>(zero2, out outPeerId);
			Helper.TryMarshalGet<SocketIdInternal, SocketId>(@default, out outSocketId);
			Helper.TryMarshalGet<byte>(zero3, out outData, num);
			Helper.TryMarshalDispose(ref zero3);
			return result;
		}

		// Token: 0x06001141 RID: 4417 RVA: 0x000126D3 File Offset: 0x000108D3
		public void RemoveNotifyIncomingPacketQueueFull(ulong notificationId)
		{
			Helper.TryRemoveCallbackByNotificationId(notificationId);
			Bindings.EOS_P2P_RemoveNotifyIncomingPacketQueueFull(base.InnerHandle, notificationId);
		}

		// Token: 0x06001142 RID: 4418 RVA: 0x000126E8 File Offset: 0x000108E8
		public void RemoveNotifyPeerConnectionClosed(ulong notificationId)
		{
			Helper.TryRemoveCallbackByNotificationId(notificationId);
			Bindings.EOS_P2P_RemoveNotifyPeerConnectionClosed(base.InnerHandle, notificationId);
		}

		// Token: 0x06001143 RID: 4419 RVA: 0x000126FD File Offset: 0x000108FD
		public void RemoveNotifyPeerConnectionEstablished(ulong notificationId)
		{
			Helper.TryRemoveCallbackByNotificationId(notificationId);
			Bindings.EOS_P2P_RemoveNotifyPeerConnectionEstablished(base.InnerHandle, notificationId);
		}

		// Token: 0x06001144 RID: 4420 RVA: 0x00012712 File Offset: 0x00010912
		public void RemoveNotifyPeerConnectionRequest(ulong notificationId)
		{
			Helper.TryRemoveCallbackByNotificationId(notificationId);
			Bindings.EOS_P2P_RemoveNotifyPeerConnectionRequest(base.InnerHandle, notificationId);
		}

		// Token: 0x06001145 RID: 4421 RVA: 0x00012728 File Offset: 0x00010928
		public Result SendPacket(SendPacketOptions options)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<SendPacketOptionsInternal, SendPacketOptions>(ref zero, options);
			Result result = Bindings.EOS_P2P_SendPacket(base.InnerHandle, zero);
			Helper.TryMarshalDispose(ref zero);
			return result;
		}

		// Token: 0x06001146 RID: 4422 RVA: 0x00012758 File Offset: 0x00010958
		public Result SetPacketQueueSize(SetPacketQueueSizeOptions options)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<SetPacketQueueSizeOptionsInternal, SetPacketQueueSizeOptions>(ref zero, options);
			Result result = Bindings.EOS_P2P_SetPacketQueueSize(base.InnerHandle, zero);
			Helper.TryMarshalDispose(ref zero);
			return result;
		}

		// Token: 0x06001147 RID: 4423 RVA: 0x00012788 File Offset: 0x00010988
		public Result SetPortRange(SetPortRangeOptions options)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<SetPortRangeOptionsInternal, SetPortRangeOptions>(ref zero, options);
			Result result = Bindings.EOS_P2P_SetPortRange(base.InnerHandle, zero);
			Helper.TryMarshalDispose(ref zero);
			return result;
		}

		// Token: 0x06001148 RID: 4424 RVA: 0x000127B8 File Offset: 0x000109B8
		public Result SetRelayControl(SetRelayControlOptions options)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<SetRelayControlOptionsInternal, SetRelayControlOptions>(ref zero, options);
			Result result = Bindings.EOS_P2P_SetRelayControl(base.InnerHandle, zero);
			Helper.TryMarshalDispose(ref zero);
			return result;
		}

		// Token: 0x06001149 RID: 4425 RVA: 0x000127E8 File Offset: 0x000109E8
		[MonoPInvokeCallback(typeof(OnIncomingConnectionRequestCallbackInternal))]
		internal static void OnIncomingConnectionRequestCallbackInternalImplementation(IntPtr data)
		{
			OnIncomingConnectionRequestCallback onIncomingConnectionRequestCallback;
			OnIncomingConnectionRequestInfo data2;
			if (Helper.TryGetAndRemoveCallback<OnIncomingConnectionRequestCallback, OnIncomingConnectionRequestInfoInternal, OnIncomingConnectionRequestInfo>(data, out onIncomingConnectionRequestCallback, out data2))
			{
				onIncomingConnectionRequestCallback(data2);
			}
		}

		// Token: 0x0600114A RID: 4426 RVA: 0x00012808 File Offset: 0x00010A08
		[MonoPInvokeCallback(typeof(OnIncomingPacketQueueFullCallbackInternal))]
		internal static void OnIncomingPacketQueueFullCallbackInternalImplementation(IntPtr data)
		{
			OnIncomingPacketQueueFullCallback onIncomingPacketQueueFullCallback;
			OnIncomingPacketQueueFullInfo data2;
			if (Helper.TryGetAndRemoveCallback<OnIncomingPacketQueueFullCallback, OnIncomingPacketQueueFullInfoInternal, OnIncomingPacketQueueFullInfo>(data, out onIncomingPacketQueueFullCallback, out data2))
			{
				onIncomingPacketQueueFullCallback(data2);
			}
		}

		// Token: 0x0600114B RID: 4427 RVA: 0x00012828 File Offset: 0x00010A28
		[MonoPInvokeCallback(typeof(OnPeerConnectionEstablishedCallbackInternal))]
		internal static void OnPeerConnectionEstablishedCallbackInternalImplementation(IntPtr data)
		{
			OnPeerConnectionEstablishedCallback onPeerConnectionEstablishedCallback;
			OnPeerConnectionEstablishedInfo data2;
			if (Helper.TryGetAndRemoveCallback<OnPeerConnectionEstablishedCallback, OnPeerConnectionEstablishedInfoInternal, OnPeerConnectionEstablishedInfo>(data, out onPeerConnectionEstablishedCallback, out data2))
			{
				onPeerConnectionEstablishedCallback(data2);
			}
		}

		// Token: 0x0600114C RID: 4428 RVA: 0x00012848 File Offset: 0x00010A48
		[MonoPInvokeCallback(typeof(OnQueryNATTypeCompleteCallbackInternal))]
		internal static void OnQueryNATTypeCompleteCallbackInternalImplementation(IntPtr data)
		{
			OnQueryNATTypeCompleteCallback onQueryNATTypeCompleteCallback;
			OnQueryNATTypeCompleteInfo data2;
			if (Helper.TryGetAndRemoveCallback<OnQueryNATTypeCompleteCallback, OnQueryNATTypeCompleteInfoInternal, OnQueryNATTypeCompleteInfo>(data, out onQueryNATTypeCompleteCallback, out data2))
			{
				onQueryNATTypeCompleteCallback(data2);
			}
		}

		// Token: 0x0600114D RID: 4429 RVA: 0x00012868 File Offset: 0x00010A68
		[MonoPInvokeCallback(typeof(OnRemoteConnectionClosedCallbackInternal))]
		internal static void OnRemoteConnectionClosedCallbackInternalImplementation(IntPtr data)
		{
			OnRemoteConnectionClosedCallback onRemoteConnectionClosedCallback;
			OnRemoteConnectionClosedInfo data2;
			if (Helper.TryGetAndRemoveCallback<OnRemoteConnectionClosedCallback, OnRemoteConnectionClosedInfoInternal, OnRemoteConnectionClosedInfo>(data, out onRemoteConnectionClosedCallback, out data2))
			{
				onRemoteConnectionClosedCallback(data2);
			}
		}

		// Token: 0x04000808 RID: 2056
		public const int AcceptconnectionApiLatest = 1;

		// Token: 0x04000809 RID: 2057
		public const int AddnotifyincomingpacketqueuefullApiLatest = 1;

		// Token: 0x0400080A RID: 2058
		public const int AddnotifypeerconnectionclosedApiLatest = 1;

		// Token: 0x0400080B RID: 2059
		public const int AddnotifypeerconnectionestablishedApiLatest = 1;

		// Token: 0x0400080C RID: 2060
		public const int AddnotifypeerconnectionrequestApiLatest = 1;

		// Token: 0x0400080D RID: 2061
		public const int ClearpacketqueueApiLatest = 1;

		// Token: 0x0400080E RID: 2062
		public const int CloseconnectionApiLatest = 1;

		// Token: 0x0400080F RID: 2063
		public const int CloseconnectionsApiLatest = 1;

		// Token: 0x04000810 RID: 2064
		public const int GetnattypeApiLatest = 1;

		// Token: 0x04000811 RID: 2065
		public const int GetnextreceivedpacketsizeApiLatest = 2;

		// Token: 0x04000812 RID: 2066
		public const int GetpacketqueueinfoApiLatest = 1;

		// Token: 0x04000813 RID: 2067
		public const int GetportrangeApiLatest = 1;

		// Token: 0x04000814 RID: 2068
		public const int GetrelaycontrolApiLatest = 1;

		// Token: 0x04000815 RID: 2069
		public const int MaxConnections = 32;

		// Token: 0x04000816 RID: 2070
		public const int MaxPacketSize = 1170;

		// Token: 0x04000817 RID: 2071
		public const int MaxQueueSizeUnlimited = 0;

		// Token: 0x04000818 RID: 2072
		public const int QuerynattypeApiLatest = 1;

		// Token: 0x04000819 RID: 2073
		public const int ReceivepacketApiLatest = 2;

		// Token: 0x0400081A RID: 2074
		public const int SendpacketApiLatest = 2;

		// Token: 0x0400081B RID: 2075
		public const int SetpacketqueuesizeApiLatest = 1;

		// Token: 0x0400081C RID: 2076
		public const int SetportrangeApiLatest = 1;

		// Token: 0x0400081D RID: 2077
		public const int SetrelaycontrolApiLatest = 1;

		// Token: 0x0400081E RID: 2078
		public const int SocketidApiLatest = 1;
	}
}
