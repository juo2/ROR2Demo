using System;
using Epic.OnlineServices.RTCAudio;

namespace Epic.OnlineServices.RTC
{
	// Token: 0x020001EA RID: 490
	public sealed class RTCInterface : Handle
	{
		// Token: 0x06000CFC RID: 3324 RVA: 0x000036D3 File Offset: 0x000018D3
		public RTCInterface()
		{
		}

		// Token: 0x06000CFD RID: 3325 RVA: 0x000036DB File Offset: 0x000018DB
		public RTCInterface(IntPtr innerHandle) : base(innerHandle)
		{
		}

		// Token: 0x06000CFE RID: 3326 RVA: 0x0000DF68 File Offset: 0x0000C168
		public ulong AddNotifyDisconnected(AddNotifyDisconnectedOptions options, object clientData, OnDisconnectedCallback completionDelegate)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<AddNotifyDisconnectedOptionsInternal, AddNotifyDisconnectedOptions>(ref zero, options);
			IntPtr zero2 = IntPtr.Zero;
			OnDisconnectedCallbackInternal onDisconnectedCallbackInternal = new OnDisconnectedCallbackInternal(RTCInterface.OnDisconnectedCallbackInternalImplementation);
			Helper.AddCallback(ref zero2, clientData, completionDelegate, onDisconnectedCallbackInternal, Array.Empty<Delegate>());
			ulong num = Bindings.EOS_RTC_AddNotifyDisconnected(base.InnerHandle, zero, zero2, onDisconnectedCallbackInternal);
			Helper.TryMarshalDispose(ref zero);
			Helper.TryAssignNotificationIdToCallback(zero2, num);
			return num;
		}

		// Token: 0x06000CFF RID: 3327 RVA: 0x0000DFC8 File Offset: 0x0000C1C8
		public ulong AddNotifyParticipantStatusChanged(AddNotifyParticipantStatusChangedOptions options, object clientData, OnParticipantStatusChangedCallback completionDelegate)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<AddNotifyParticipantStatusChangedOptionsInternal, AddNotifyParticipantStatusChangedOptions>(ref zero, options);
			IntPtr zero2 = IntPtr.Zero;
			OnParticipantStatusChangedCallbackInternal onParticipantStatusChangedCallbackInternal = new OnParticipantStatusChangedCallbackInternal(RTCInterface.OnParticipantStatusChangedCallbackInternalImplementation);
			Helper.AddCallback(ref zero2, clientData, completionDelegate, onParticipantStatusChangedCallbackInternal, Array.Empty<Delegate>());
			ulong num = Bindings.EOS_RTC_AddNotifyParticipantStatusChanged(base.InnerHandle, zero, zero2, onParticipantStatusChangedCallbackInternal);
			Helper.TryMarshalDispose(ref zero);
			Helper.TryAssignNotificationIdToCallback(zero2, num);
			return num;
		}

		// Token: 0x06000D00 RID: 3328 RVA: 0x0000E028 File Offset: 0x0000C228
		public void BlockParticipant(BlockParticipantOptions options, object clientData, OnBlockParticipantCallback completionDelegate)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<BlockParticipantOptionsInternal, BlockParticipantOptions>(ref zero, options);
			IntPtr zero2 = IntPtr.Zero;
			OnBlockParticipantCallbackInternal onBlockParticipantCallbackInternal = new OnBlockParticipantCallbackInternal(RTCInterface.OnBlockParticipantCallbackInternalImplementation);
			Helper.AddCallback(ref zero2, clientData, completionDelegate, onBlockParticipantCallbackInternal, Array.Empty<Delegate>());
			Bindings.EOS_RTC_BlockParticipant(base.InnerHandle, zero, zero2, onBlockParticipantCallbackInternal);
			Helper.TryMarshalDispose(ref zero);
		}

		// Token: 0x06000D01 RID: 3329 RVA: 0x0000E07C File Offset: 0x0000C27C
		public RTCAudioInterface GetAudioInterface()
		{
			RTCAudioInterface result;
			Helper.TryMarshalGet<RTCAudioInterface>(Bindings.EOS_RTC_GetAudioInterface(base.InnerHandle), out result);
			return result;
		}

		// Token: 0x06000D02 RID: 3330 RVA: 0x0000E0A0 File Offset: 0x0000C2A0
		public void JoinRoom(JoinRoomOptions options, object clientData, OnJoinRoomCallback completionDelegate)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<JoinRoomOptionsInternal, JoinRoomOptions>(ref zero, options);
			IntPtr zero2 = IntPtr.Zero;
			OnJoinRoomCallbackInternal onJoinRoomCallbackInternal = new OnJoinRoomCallbackInternal(RTCInterface.OnJoinRoomCallbackInternalImplementation);
			Helper.AddCallback(ref zero2, clientData, completionDelegate, onJoinRoomCallbackInternal, Array.Empty<Delegate>());
			Bindings.EOS_RTC_JoinRoom(base.InnerHandle, zero, zero2, onJoinRoomCallbackInternal);
			Helper.TryMarshalDispose(ref zero);
		}

		// Token: 0x06000D03 RID: 3331 RVA: 0x0000E0F4 File Offset: 0x0000C2F4
		public void LeaveRoom(LeaveRoomOptions options, object clientData, OnLeaveRoomCallback completionDelegate)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<LeaveRoomOptionsInternal, LeaveRoomOptions>(ref zero, options);
			IntPtr zero2 = IntPtr.Zero;
			OnLeaveRoomCallbackInternal onLeaveRoomCallbackInternal = new OnLeaveRoomCallbackInternal(RTCInterface.OnLeaveRoomCallbackInternalImplementation);
			Helper.AddCallback(ref zero2, clientData, completionDelegate, onLeaveRoomCallbackInternal, Array.Empty<Delegate>());
			Bindings.EOS_RTC_LeaveRoom(base.InnerHandle, zero, zero2, onLeaveRoomCallbackInternal);
			Helper.TryMarshalDispose(ref zero);
		}

		// Token: 0x06000D04 RID: 3332 RVA: 0x0000E148 File Offset: 0x0000C348
		public void RemoveNotifyDisconnected(ulong notificationId)
		{
			Helper.TryRemoveCallbackByNotificationId(notificationId);
			Bindings.EOS_RTC_RemoveNotifyDisconnected(base.InnerHandle, notificationId);
		}

		// Token: 0x06000D05 RID: 3333 RVA: 0x0000E15D File Offset: 0x0000C35D
		public void RemoveNotifyParticipantStatusChanged(ulong notificationId)
		{
			Helper.TryRemoveCallbackByNotificationId(notificationId);
			Bindings.EOS_RTC_RemoveNotifyParticipantStatusChanged(base.InnerHandle, notificationId);
		}

		// Token: 0x06000D06 RID: 3334 RVA: 0x0000E174 File Offset: 0x0000C374
		public Result SetRoomSetting(SetRoomSettingOptions options)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<SetRoomSettingOptionsInternal, SetRoomSettingOptions>(ref zero, options);
			Result result = Bindings.EOS_RTC_SetRoomSetting(base.InnerHandle, zero);
			Helper.TryMarshalDispose(ref zero);
			return result;
		}

		// Token: 0x06000D07 RID: 3335 RVA: 0x0000E1A4 File Offset: 0x0000C3A4
		public Result SetSetting(SetSettingOptions options)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<SetSettingOptionsInternal, SetSettingOptions>(ref zero, options);
			Result result = Bindings.EOS_RTC_SetSetting(base.InnerHandle, zero);
			Helper.TryMarshalDispose(ref zero);
			return result;
		}

		// Token: 0x06000D08 RID: 3336 RVA: 0x0000E1D4 File Offset: 0x0000C3D4
		[MonoPInvokeCallback(typeof(OnBlockParticipantCallbackInternal))]
		internal static void OnBlockParticipantCallbackInternalImplementation(IntPtr data)
		{
			OnBlockParticipantCallback onBlockParticipantCallback;
			BlockParticipantCallbackInfo data2;
			if (Helper.TryGetAndRemoveCallback<OnBlockParticipantCallback, BlockParticipantCallbackInfoInternal, BlockParticipantCallbackInfo>(data, out onBlockParticipantCallback, out data2))
			{
				onBlockParticipantCallback(data2);
			}
		}

		// Token: 0x06000D09 RID: 3337 RVA: 0x0000E1F4 File Offset: 0x0000C3F4
		[MonoPInvokeCallback(typeof(OnDisconnectedCallbackInternal))]
		internal static void OnDisconnectedCallbackInternalImplementation(IntPtr data)
		{
			OnDisconnectedCallback onDisconnectedCallback;
			DisconnectedCallbackInfo data2;
			if (Helper.TryGetAndRemoveCallback<OnDisconnectedCallback, DisconnectedCallbackInfoInternal, DisconnectedCallbackInfo>(data, out onDisconnectedCallback, out data2))
			{
				onDisconnectedCallback(data2);
			}
		}

		// Token: 0x06000D0A RID: 3338 RVA: 0x0000E214 File Offset: 0x0000C414
		[MonoPInvokeCallback(typeof(OnJoinRoomCallbackInternal))]
		internal static void OnJoinRoomCallbackInternalImplementation(IntPtr data)
		{
			OnJoinRoomCallback onJoinRoomCallback;
			JoinRoomCallbackInfo data2;
			if (Helper.TryGetAndRemoveCallback<OnJoinRoomCallback, JoinRoomCallbackInfoInternal, JoinRoomCallbackInfo>(data, out onJoinRoomCallback, out data2))
			{
				onJoinRoomCallback(data2);
			}
		}

		// Token: 0x06000D0B RID: 3339 RVA: 0x0000E234 File Offset: 0x0000C434
		[MonoPInvokeCallback(typeof(OnLeaveRoomCallbackInternal))]
		internal static void OnLeaveRoomCallbackInternalImplementation(IntPtr data)
		{
			OnLeaveRoomCallback onLeaveRoomCallback;
			LeaveRoomCallbackInfo data2;
			if (Helper.TryGetAndRemoveCallback<OnLeaveRoomCallback, LeaveRoomCallbackInfoInternal, LeaveRoomCallbackInfo>(data, out onLeaveRoomCallback, out data2))
			{
				onLeaveRoomCallback(data2);
			}
		}

		// Token: 0x06000D0C RID: 3340 RVA: 0x0000E254 File Offset: 0x0000C454
		[MonoPInvokeCallback(typeof(OnParticipantStatusChangedCallbackInternal))]
		internal static void OnParticipantStatusChangedCallbackInternalImplementation(IntPtr data)
		{
			OnParticipantStatusChangedCallback onParticipantStatusChangedCallback;
			ParticipantStatusChangedCallbackInfo data2;
			if (Helper.TryGetAndRemoveCallback<OnParticipantStatusChangedCallback, ParticipantStatusChangedCallbackInfoInternal, ParticipantStatusChangedCallbackInfo>(data, out onParticipantStatusChangedCallback, out data2))
			{
				onParticipantStatusChangedCallback(data2);
			}
		}

		// Token: 0x0400061F RID: 1567
		public const int AddnotifydisconnectedApiLatest = 1;

		// Token: 0x04000620 RID: 1568
		public const int AddnotifyparticipantstatuschangedApiLatest = 1;

		// Token: 0x04000621 RID: 1569
		public const int BlockparticipantApiLatest = 1;

		// Token: 0x04000622 RID: 1570
		public const int JoinroomApiLatest = 1;

		// Token: 0x04000623 RID: 1571
		public const int LeaveroomApiLatest = 1;

		// Token: 0x04000624 RID: 1572
		public const int ParticipantmetadataApiLatest = 1;

		// Token: 0x04000625 RID: 1573
		public const int ParticipantmetadataKeyMaxcharcount = 256;

		// Token: 0x04000626 RID: 1574
		public const int ParticipantmetadataValueMaxcharcount = 256;

		// Token: 0x04000627 RID: 1575
		public const int SetroomsettingApiLatest = 1;

		// Token: 0x04000628 RID: 1576
		public const int SetsettingApiLatest = 1;
	}
}
