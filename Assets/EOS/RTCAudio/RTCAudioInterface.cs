using System;

namespace Epic.OnlineServices.RTCAudio
{
	// Token: 0x0200019B RID: 411
	public sealed class RTCAudioInterface : Handle
	{
		// Token: 0x06000AE2 RID: 2786 RVA: 0x000036D3 File Offset: 0x000018D3
		public RTCAudioInterface()
		{
		}

		// Token: 0x06000AE3 RID: 2787 RVA: 0x000036DB File Offset: 0x000018DB
		public RTCAudioInterface(IntPtr innerHandle) : base(innerHandle)
		{
		}

		// Token: 0x06000AE4 RID: 2788 RVA: 0x0000BBC4 File Offset: 0x00009DC4
		public ulong AddNotifyAudioBeforeRender(AddNotifyAudioBeforeRenderOptions options, object clientData, OnAudioBeforeRenderCallback completionDelegate)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<AddNotifyAudioBeforeRenderOptionsInternal, AddNotifyAudioBeforeRenderOptions>(ref zero, options);
			IntPtr zero2 = IntPtr.Zero;
			OnAudioBeforeRenderCallbackInternal onAudioBeforeRenderCallbackInternal = new OnAudioBeforeRenderCallbackInternal(RTCAudioInterface.OnAudioBeforeRenderCallbackInternalImplementation);
			Helper.AddCallback(ref zero2, clientData, completionDelegate, onAudioBeforeRenderCallbackInternal, Array.Empty<Delegate>());
			ulong num = Bindings.EOS_RTCAudio_AddNotifyAudioBeforeRender(base.InnerHandle, zero, zero2, onAudioBeforeRenderCallbackInternal);
			Helper.TryMarshalDispose(ref zero);
			Helper.TryAssignNotificationIdToCallback(zero2, num);
			return num;
		}

		// Token: 0x06000AE5 RID: 2789 RVA: 0x0000BC24 File Offset: 0x00009E24
		public ulong AddNotifyAudioBeforeSend(AddNotifyAudioBeforeSendOptions options, object clientData, OnAudioBeforeSendCallback completionDelegate)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<AddNotifyAudioBeforeSendOptionsInternal, AddNotifyAudioBeforeSendOptions>(ref zero, options);
			IntPtr zero2 = IntPtr.Zero;
			OnAudioBeforeSendCallbackInternal onAudioBeforeSendCallbackInternal = new OnAudioBeforeSendCallbackInternal(RTCAudioInterface.OnAudioBeforeSendCallbackInternalImplementation);
			Helper.AddCallback(ref zero2, clientData, completionDelegate, onAudioBeforeSendCallbackInternal, Array.Empty<Delegate>());
			ulong num = Bindings.EOS_RTCAudio_AddNotifyAudioBeforeSend(base.InnerHandle, zero, zero2, onAudioBeforeSendCallbackInternal);
			Helper.TryMarshalDispose(ref zero);
			Helper.TryAssignNotificationIdToCallback(zero2, num);
			return num;
		}

		// Token: 0x06000AE6 RID: 2790 RVA: 0x0000BC84 File Offset: 0x00009E84
		public ulong AddNotifyAudioDevicesChanged(AddNotifyAudioDevicesChangedOptions options, object clientData, OnAudioDevicesChangedCallback completionDelegate)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<AddNotifyAudioDevicesChangedOptionsInternal, AddNotifyAudioDevicesChangedOptions>(ref zero, options);
			IntPtr zero2 = IntPtr.Zero;
			OnAudioDevicesChangedCallbackInternal onAudioDevicesChangedCallbackInternal = new OnAudioDevicesChangedCallbackInternal(RTCAudioInterface.OnAudioDevicesChangedCallbackInternalImplementation);
			Helper.AddCallback(ref zero2, clientData, completionDelegate, onAudioDevicesChangedCallbackInternal, Array.Empty<Delegate>());
			ulong num = Bindings.EOS_RTCAudio_AddNotifyAudioDevicesChanged(base.InnerHandle, zero, zero2, onAudioDevicesChangedCallbackInternal);
			Helper.TryMarshalDispose(ref zero);
			Helper.TryAssignNotificationIdToCallback(zero2, num);
			return num;
		}

		// Token: 0x06000AE7 RID: 2791 RVA: 0x0000BCE4 File Offset: 0x00009EE4
		public ulong AddNotifyAudioInputState(AddNotifyAudioInputStateOptions options, object clientData, OnAudioInputStateCallback completionDelegate)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<AddNotifyAudioInputStateOptionsInternal, AddNotifyAudioInputStateOptions>(ref zero, options);
			IntPtr zero2 = IntPtr.Zero;
			OnAudioInputStateCallbackInternal onAudioInputStateCallbackInternal = new OnAudioInputStateCallbackInternal(RTCAudioInterface.OnAudioInputStateCallbackInternalImplementation);
			Helper.AddCallback(ref zero2, clientData, completionDelegate, onAudioInputStateCallbackInternal, Array.Empty<Delegate>());
			ulong num = Bindings.EOS_RTCAudio_AddNotifyAudioInputState(base.InnerHandle, zero, zero2, onAudioInputStateCallbackInternal);
			Helper.TryMarshalDispose(ref zero);
			Helper.TryAssignNotificationIdToCallback(zero2, num);
			return num;
		}

		// Token: 0x06000AE8 RID: 2792 RVA: 0x0000BD44 File Offset: 0x00009F44
		public ulong AddNotifyAudioOutputState(AddNotifyAudioOutputStateOptions options, object clientData, OnAudioOutputStateCallback completionDelegate)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<AddNotifyAudioOutputStateOptionsInternal, AddNotifyAudioOutputStateOptions>(ref zero, options);
			IntPtr zero2 = IntPtr.Zero;
			OnAudioOutputStateCallbackInternal onAudioOutputStateCallbackInternal = new OnAudioOutputStateCallbackInternal(RTCAudioInterface.OnAudioOutputStateCallbackInternalImplementation);
			Helper.AddCallback(ref zero2, clientData, completionDelegate, onAudioOutputStateCallbackInternal, Array.Empty<Delegate>());
			ulong num = Bindings.EOS_RTCAudio_AddNotifyAudioOutputState(base.InnerHandle, zero, zero2, onAudioOutputStateCallbackInternal);
			Helper.TryMarshalDispose(ref zero);
			Helper.TryAssignNotificationIdToCallback(zero2, num);
			return num;
		}

		// Token: 0x06000AE9 RID: 2793 RVA: 0x0000BDA4 File Offset: 0x00009FA4
		public ulong AddNotifyParticipantUpdated(AddNotifyParticipantUpdatedOptions options, object clientData, OnParticipantUpdatedCallback completionDelegate)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<AddNotifyParticipantUpdatedOptionsInternal, AddNotifyParticipantUpdatedOptions>(ref zero, options);
			IntPtr zero2 = IntPtr.Zero;
			OnParticipantUpdatedCallbackInternal onParticipantUpdatedCallbackInternal = new OnParticipantUpdatedCallbackInternal(RTCAudioInterface.OnParticipantUpdatedCallbackInternalImplementation);
			Helper.AddCallback(ref zero2, clientData, completionDelegate, onParticipantUpdatedCallbackInternal, Array.Empty<Delegate>());
			ulong num = Bindings.EOS_RTCAudio_AddNotifyParticipantUpdated(base.InnerHandle, zero, zero2, onParticipantUpdatedCallbackInternal);
			Helper.TryMarshalDispose(ref zero);
			Helper.TryAssignNotificationIdToCallback(zero2, num);
			return num;
		}

		// Token: 0x06000AEA RID: 2794 RVA: 0x0000BE04 File Offset: 0x0000A004
		public AudioInputDeviceInfo GetAudioInputDeviceByIndex(GetAudioInputDeviceByIndexOptions options)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<GetAudioInputDeviceByIndexOptionsInternal, GetAudioInputDeviceByIndexOptions>(ref zero, options);
			IntPtr source = Bindings.EOS_RTCAudio_GetAudioInputDeviceByIndex(base.InnerHandle, zero);
			Helper.TryMarshalDispose(ref zero);
			AudioInputDeviceInfo result;
			Helper.TryMarshalGet<AudioInputDeviceInfoInternal, AudioInputDeviceInfo>(source, out result);
			return result;
		}

		// Token: 0x06000AEB RID: 2795 RVA: 0x0000BE40 File Offset: 0x0000A040
		public uint GetAudioInputDevicesCount(GetAudioInputDevicesCountOptions options)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<GetAudioInputDevicesCountOptionsInternal, GetAudioInputDevicesCountOptions>(ref zero, options);
			uint result = Bindings.EOS_RTCAudio_GetAudioInputDevicesCount(base.InnerHandle, zero);
			Helper.TryMarshalDispose(ref zero);
			return result;
		}

		// Token: 0x06000AEC RID: 2796 RVA: 0x0000BE70 File Offset: 0x0000A070
		public AudioOutputDeviceInfo GetAudioOutputDeviceByIndex(GetAudioOutputDeviceByIndexOptions options)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<GetAudioOutputDeviceByIndexOptionsInternal, GetAudioOutputDeviceByIndexOptions>(ref zero, options);
			IntPtr source = Bindings.EOS_RTCAudio_GetAudioOutputDeviceByIndex(base.InnerHandle, zero);
			Helper.TryMarshalDispose(ref zero);
			AudioOutputDeviceInfo result;
			Helper.TryMarshalGet<AudioOutputDeviceInfoInternal, AudioOutputDeviceInfo>(source, out result);
			return result;
		}

		// Token: 0x06000AED RID: 2797 RVA: 0x0000BEAC File Offset: 0x0000A0AC
		public uint GetAudioOutputDevicesCount(GetAudioOutputDevicesCountOptions options)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<GetAudioOutputDevicesCountOptionsInternal, GetAudioOutputDevicesCountOptions>(ref zero, options);
			uint result = Bindings.EOS_RTCAudio_GetAudioOutputDevicesCount(base.InnerHandle, zero);
			Helper.TryMarshalDispose(ref zero);
			return result;
		}

		// Token: 0x06000AEE RID: 2798 RVA: 0x0000BEDC File Offset: 0x0000A0DC
		public Result RegisterPlatformAudioUser(RegisterPlatformAudioUserOptions options)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<RegisterPlatformAudioUserOptionsInternal, RegisterPlatformAudioUserOptions>(ref zero, options);
			Result result = Bindings.EOS_RTCAudio_RegisterPlatformAudioUser(base.InnerHandle, zero);
			Helper.TryMarshalDispose(ref zero);
			return result;
		}

		// Token: 0x06000AEF RID: 2799 RVA: 0x0000BF0C File Offset: 0x0000A10C
		public void RemoveNotifyAudioBeforeRender(ulong notificationId)
		{
			Helper.TryRemoveCallbackByNotificationId(notificationId);
			Bindings.EOS_RTCAudio_RemoveNotifyAudioBeforeRender(base.InnerHandle, notificationId);
		}

		// Token: 0x06000AF0 RID: 2800 RVA: 0x0000BF21 File Offset: 0x0000A121
		public void RemoveNotifyAudioBeforeSend(ulong notificationId)
		{
			Helper.TryRemoveCallbackByNotificationId(notificationId);
			Bindings.EOS_RTCAudio_RemoveNotifyAudioBeforeSend(base.InnerHandle, notificationId);
		}

		// Token: 0x06000AF1 RID: 2801 RVA: 0x0000BF36 File Offset: 0x0000A136
		public void RemoveNotifyAudioDevicesChanged(ulong notificationId)
		{
			Helper.TryRemoveCallbackByNotificationId(notificationId);
			Bindings.EOS_RTCAudio_RemoveNotifyAudioDevicesChanged(base.InnerHandle, notificationId);
		}

		// Token: 0x06000AF2 RID: 2802 RVA: 0x0000BF4B File Offset: 0x0000A14B
		public void RemoveNotifyAudioInputState(ulong notificationId)
		{
			Helper.TryRemoveCallbackByNotificationId(notificationId);
			Bindings.EOS_RTCAudio_RemoveNotifyAudioInputState(base.InnerHandle, notificationId);
		}

		// Token: 0x06000AF3 RID: 2803 RVA: 0x0000BF60 File Offset: 0x0000A160
		public void RemoveNotifyAudioOutputState(ulong notificationId)
		{
			Helper.TryRemoveCallbackByNotificationId(notificationId);
			Bindings.EOS_RTCAudio_RemoveNotifyAudioOutputState(base.InnerHandle, notificationId);
		}

		// Token: 0x06000AF4 RID: 2804 RVA: 0x0000BF75 File Offset: 0x0000A175
		public void RemoveNotifyParticipantUpdated(ulong notificationId)
		{
			Helper.TryRemoveCallbackByNotificationId(notificationId);
			Bindings.EOS_RTCAudio_RemoveNotifyParticipantUpdated(base.InnerHandle, notificationId);
		}

		// Token: 0x06000AF5 RID: 2805 RVA: 0x0000BF8C File Offset: 0x0000A18C
		public Result SendAudio(SendAudioOptions options)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<SendAudioOptionsInternal, SendAudioOptions>(ref zero, options);
			Result result = Bindings.EOS_RTCAudio_SendAudio(base.InnerHandle, zero);
			Helper.TryMarshalDispose(ref zero);
			return result;
		}

		// Token: 0x06000AF6 RID: 2806 RVA: 0x0000BFBC File Offset: 0x0000A1BC
		public Result SetAudioInputSettings(SetAudioInputSettingsOptions options)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<SetAudioInputSettingsOptionsInternal, SetAudioInputSettingsOptions>(ref zero, options);
			Result result = Bindings.EOS_RTCAudio_SetAudioInputSettings(base.InnerHandle, zero);
			Helper.TryMarshalDispose(ref zero);
			return result;
		}

		// Token: 0x06000AF7 RID: 2807 RVA: 0x0000BFEC File Offset: 0x0000A1EC
		public Result SetAudioOutputSettings(SetAudioOutputSettingsOptions options)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<SetAudioOutputSettingsOptionsInternal, SetAudioOutputSettingsOptions>(ref zero, options);
			Result result = Bindings.EOS_RTCAudio_SetAudioOutputSettings(base.InnerHandle, zero);
			Helper.TryMarshalDispose(ref zero);
			return result;
		}

		// Token: 0x06000AF8 RID: 2808 RVA: 0x0000C01C File Offset: 0x0000A21C
		public Result UnregisterPlatformAudioUser(UnregisterPlatformAudioUserOptions options)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<UnregisterPlatformAudioUserOptionsInternal, UnregisterPlatformAudioUserOptions>(ref zero, options);
			Result result = Bindings.EOS_RTCAudio_UnregisterPlatformAudioUser(base.InnerHandle, zero);
			Helper.TryMarshalDispose(ref zero);
			return result;
		}

		// Token: 0x06000AF9 RID: 2809 RVA: 0x0000C04C File Offset: 0x0000A24C
		public void UpdateReceiving(UpdateReceivingOptions options, object clientData, OnUpdateReceivingCallback completionDelegate)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<UpdateReceivingOptionsInternal, UpdateReceivingOptions>(ref zero, options);
			IntPtr zero2 = IntPtr.Zero;
			OnUpdateReceivingCallbackInternal onUpdateReceivingCallbackInternal = new OnUpdateReceivingCallbackInternal(RTCAudioInterface.OnUpdateReceivingCallbackInternalImplementation);
			Helper.AddCallback(ref zero2, clientData, completionDelegate, onUpdateReceivingCallbackInternal, Array.Empty<Delegate>());
			Bindings.EOS_RTCAudio_UpdateReceiving(base.InnerHandle, zero, zero2, onUpdateReceivingCallbackInternal);
			Helper.TryMarshalDispose(ref zero);
		}

		// Token: 0x06000AFA RID: 2810 RVA: 0x0000C0A0 File Offset: 0x0000A2A0
		public void UpdateSending(UpdateSendingOptions options, object clientData, OnUpdateSendingCallback completionDelegate)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<UpdateSendingOptionsInternal, UpdateSendingOptions>(ref zero, options);
			IntPtr zero2 = IntPtr.Zero;
			OnUpdateSendingCallbackInternal onUpdateSendingCallbackInternal = new OnUpdateSendingCallbackInternal(RTCAudioInterface.OnUpdateSendingCallbackInternalImplementation);
			Helper.AddCallback(ref zero2, clientData, completionDelegate, onUpdateSendingCallbackInternal, Array.Empty<Delegate>());
			Bindings.EOS_RTCAudio_UpdateSending(base.InnerHandle, zero, zero2, onUpdateSendingCallbackInternal);
			Helper.TryMarshalDispose(ref zero);
		}

		// Token: 0x06000AFB RID: 2811 RVA: 0x0000C0F4 File Offset: 0x0000A2F4
		[MonoPInvokeCallback(typeof(OnAudioBeforeRenderCallbackInternal))]
		internal static void OnAudioBeforeRenderCallbackInternalImplementation(IntPtr data)
		{
			OnAudioBeforeRenderCallback onAudioBeforeRenderCallback;
			AudioBeforeRenderCallbackInfo data2;
			if (Helper.TryGetAndRemoveCallback<OnAudioBeforeRenderCallback, AudioBeforeRenderCallbackInfoInternal, AudioBeforeRenderCallbackInfo>(data, out onAudioBeforeRenderCallback, out data2))
			{
				onAudioBeforeRenderCallback(data2);
			}
		}

		// Token: 0x06000AFC RID: 2812 RVA: 0x0000C114 File Offset: 0x0000A314
		[MonoPInvokeCallback(typeof(OnAudioBeforeSendCallbackInternal))]
		internal static void OnAudioBeforeSendCallbackInternalImplementation(IntPtr data)
		{
			OnAudioBeforeSendCallback onAudioBeforeSendCallback;
			AudioBeforeSendCallbackInfo data2;
			if (Helper.TryGetAndRemoveCallback<OnAudioBeforeSendCallback, AudioBeforeSendCallbackInfoInternal, AudioBeforeSendCallbackInfo>(data, out onAudioBeforeSendCallback, out data2))
			{
				onAudioBeforeSendCallback(data2);
			}
		}

		// Token: 0x06000AFD RID: 2813 RVA: 0x0000C134 File Offset: 0x0000A334
		[MonoPInvokeCallback(typeof(OnAudioDevicesChangedCallbackInternal))]
		internal static void OnAudioDevicesChangedCallbackInternalImplementation(IntPtr data)
		{
			OnAudioDevicesChangedCallback onAudioDevicesChangedCallback;
			AudioDevicesChangedCallbackInfo data2;
			if (Helper.TryGetAndRemoveCallback<OnAudioDevicesChangedCallback, AudioDevicesChangedCallbackInfoInternal, AudioDevicesChangedCallbackInfo>(data, out onAudioDevicesChangedCallback, out data2))
			{
				onAudioDevicesChangedCallback(data2);
			}
		}

		// Token: 0x06000AFE RID: 2814 RVA: 0x0000C154 File Offset: 0x0000A354
		[MonoPInvokeCallback(typeof(OnAudioInputStateCallbackInternal))]
		internal static void OnAudioInputStateCallbackInternalImplementation(IntPtr data)
		{
			OnAudioInputStateCallback onAudioInputStateCallback;
			AudioInputStateCallbackInfo data2;
			if (Helper.TryGetAndRemoveCallback<OnAudioInputStateCallback, AudioInputStateCallbackInfoInternal, AudioInputStateCallbackInfo>(data, out onAudioInputStateCallback, out data2))
			{
				onAudioInputStateCallback(data2);
			}
		}

		// Token: 0x06000AFF RID: 2815 RVA: 0x0000C174 File Offset: 0x0000A374
		[MonoPInvokeCallback(typeof(OnAudioOutputStateCallbackInternal))]
		internal static void OnAudioOutputStateCallbackInternalImplementation(IntPtr data)
		{
			OnAudioOutputStateCallback onAudioOutputStateCallback;
			AudioOutputStateCallbackInfo data2;
			if (Helper.TryGetAndRemoveCallback<OnAudioOutputStateCallback, AudioOutputStateCallbackInfoInternal, AudioOutputStateCallbackInfo>(data, out onAudioOutputStateCallback, out data2))
			{
				onAudioOutputStateCallback(data2);
			}
		}

		// Token: 0x06000B00 RID: 2816 RVA: 0x0000C194 File Offset: 0x0000A394
		[MonoPInvokeCallback(typeof(OnParticipantUpdatedCallbackInternal))]
		internal static void OnParticipantUpdatedCallbackInternalImplementation(IntPtr data)
		{
			OnParticipantUpdatedCallback onParticipantUpdatedCallback;
			ParticipantUpdatedCallbackInfo data2;
			if (Helper.TryGetAndRemoveCallback<OnParticipantUpdatedCallback, ParticipantUpdatedCallbackInfoInternal, ParticipantUpdatedCallbackInfo>(data, out onParticipantUpdatedCallback, out data2))
			{
				onParticipantUpdatedCallback(data2);
			}
		}

		// Token: 0x06000B01 RID: 2817 RVA: 0x0000C1B4 File Offset: 0x0000A3B4
		[MonoPInvokeCallback(typeof(OnUpdateReceivingCallbackInternal))]
		internal static void OnUpdateReceivingCallbackInternalImplementation(IntPtr data)
		{
			OnUpdateReceivingCallback onUpdateReceivingCallback;
			UpdateReceivingCallbackInfo data2;
			if (Helper.TryGetAndRemoveCallback<OnUpdateReceivingCallback, UpdateReceivingCallbackInfoInternal, UpdateReceivingCallbackInfo>(data, out onUpdateReceivingCallback, out data2))
			{
				onUpdateReceivingCallback(data2);
			}
		}

		// Token: 0x06000B02 RID: 2818 RVA: 0x0000C1D4 File Offset: 0x0000A3D4
		[MonoPInvokeCallback(typeof(OnUpdateSendingCallbackInternal))]
		internal static void OnUpdateSendingCallbackInternalImplementation(IntPtr data)
		{
			OnUpdateSendingCallback onUpdateSendingCallback;
			UpdateSendingCallbackInfo data2;
			if (Helper.TryGetAndRemoveCallback<OnUpdateSendingCallback, UpdateSendingCallbackInfoInternal, UpdateSendingCallbackInfo>(data, out onUpdateSendingCallback, out data2))
			{
				onUpdateSendingCallback(data2);
			}
		}

		// Token: 0x0400051D RID: 1309
		public const int AddnotifyaudiobeforerenderApiLatest = 1;

		// Token: 0x0400051E RID: 1310
		public const int AddnotifyaudiobeforesendApiLatest = 1;

		// Token: 0x0400051F RID: 1311
		public const int AddnotifyaudiodeviceschangedApiLatest = 1;

		// Token: 0x04000520 RID: 1312
		public const int AddnotifyaudioinputstateApiLatest = 1;

		// Token: 0x04000521 RID: 1313
		public const int AddnotifyaudiooutputstateApiLatest = 1;

		// Token: 0x04000522 RID: 1314
		public const int AddnotifyparticipantupdatedApiLatest = 1;

		// Token: 0x04000523 RID: 1315
		public const int AudiobufferApiLatest = 1;

		// Token: 0x04000524 RID: 1316
		public const int AudioinputdeviceinfoApiLatest = 1;

		// Token: 0x04000525 RID: 1317
		public const int AudiooutputdeviceinfoApiLatest = 1;

		// Token: 0x04000526 RID: 1318
		public const int GetaudioinputdevicebyindexApiLatest = 1;

		// Token: 0x04000527 RID: 1319
		public const int GetaudioinputdevicescountApiLatest = 1;

		// Token: 0x04000528 RID: 1320
		public const int GetaudiooutputdevicebyindexApiLatest = 1;

		// Token: 0x04000529 RID: 1321
		public const int GetaudiooutputdevicescountApiLatest = 1;

		// Token: 0x0400052A RID: 1322
		public const int RegisterplatformaudiouserApiLatest = 1;

		// Token: 0x0400052B RID: 1323
		public const int SendaudioApiLatest = 1;

		// Token: 0x0400052C RID: 1324
		public const int SetaudioinputsettingsApiLatest = 1;

		// Token: 0x0400052D RID: 1325
		public const int SetaudiooutputsettingsApiLatest = 1;

		// Token: 0x0400052E RID: 1326
		public const int UnregisterplatformaudiouserApiLatest = 1;

		// Token: 0x0400052F RID: 1327
		public const int UpdatereceivingApiLatest = 1;

		// Token: 0x04000530 RID: 1328
		public const int UpdatesendingApiLatest = 1;
	}
}
