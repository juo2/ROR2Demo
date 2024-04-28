using System;

namespace Epic.OnlineServices.UI
{
	// Token: 0x02000062 RID: 98
	public sealed class UIInterface : Handle
	{
		// Token: 0x0600041E RID: 1054 RVA: 0x000036D3 File Offset: 0x000018D3
		public UIInterface()
		{
		}

		// Token: 0x0600041F RID: 1055 RVA: 0x000036DB File Offset: 0x000018DB
		public UIInterface(IntPtr innerHandle) : base(innerHandle)
		{
		}

		// Token: 0x06000420 RID: 1056 RVA: 0x00005000 File Offset: 0x00003200
		public Result AcknowledgeEventId(AcknowledgeEventIdOptions options)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<AcknowledgeEventIdOptionsInternal, AcknowledgeEventIdOptions>(ref zero, options);
			Result result = Bindings.EOS_UI_AcknowledgeEventId(base.InnerHandle, zero);
			Helper.TryMarshalDispose(ref zero);
			return result;
		}

		// Token: 0x06000421 RID: 1057 RVA: 0x00005030 File Offset: 0x00003230
		public ulong AddNotifyDisplaySettingsUpdated(AddNotifyDisplaySettingsUpdatedOptions options, object clientData, OnDisplaySettingsUpdatedCallback notificationFn)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<AddNotifyDisplaySettingsUpdatedOptionsInternal, AddNotifyDisplaySettingsUpdatedOptions>(ref zero, options);
			IntPtr zero2 = IntPtr.Zero;
			OnDisplaySettingsUpdatedCallbackInternal onDisplaySettingsUpdatedCallbackInternal = new OnDisplaySettingsUpdatedCallbackInternal(UIInterface.OnDisplaySettingsUpdatedCallbackInternalImplementation);
			Helper.AddCallback(ref zero2, clientData, notificationFn, onDisplaySettingsUpdatedCallbackInternal, Array.Empty<Delegate>());
			ulong num = Bindings.EOS_UI_AddNotifyDisplaySettingsUpdated(base.InnerHandle, zero, zero2, onDisplaySettingsUpdatedCallbackInternal);
			Helper.TryMarshalDispose(ref zero);
			Helper.TryAssignNotificationIdToCallback(zero2, num);
			return num;
		}

		// Token: 0x06000422 RID: 1058 RVA: 0x00005090 File Offset: 0x00003290
		public bool GetFriendsVisible(GetFriendsVisibleOptions options)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<GetFriendsVisibleOptionsInternal, GetFriendsVisibleOptions>(ref zero, options);
			int source = Bindings.EOS_UI_GetFriendsVisible(base.InnerHandle, zero);
			Helper.TryMarshalDispose(ref zero);
			bool result;
			Helper.TryMarshalGet(source, out result);
			return result;
		}

		// Token: 0x06000423 RID: 1059 RVA: 0x000050C9 File Offset: 0x000032C9
		public NotificationLocation GetNotificationLocationPreference()
		{
			return Bindings.EOS_UI_GetNotificationLocationPreference(base.InnerHandle);
		}

		// Token: 0x06000424 RID: 1060 RVA: 0x000050D8 File Offset: 0x000032D8
		public KeyCombination GetToggleFriendsKey(GetToggleFriendsKeyOptions options)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<GetToggleFriendsKeyOptionsInternal, GetToggleFriendsKeyOptions>(ref zero, options);
			KeyCombination result = Bindings.EOS_UI_GetToggleFriendsKey(base.InnerHandle, zero);
			Helper.TryMarshalDispose(ref zero);
			return result;
		}

		// Token: 0x06000425 RID: 1061 RVA: 0x00005108 File Offset: 0x00003308
		public void HideFriends(HideFriendsOptions options, object clientData, OnHideFriendsCallback completionDelegate)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<HideFriendsOptionsInternal, HideFriendsOptions>(ref zero, options);
			IntPtr zero2 = IntPtr.Zero;
			OnHideFriendsCallbackInternal onHideFriendsCallbackInternal = new OnHideFriendsCallbackInternal(UIInterface.OnHideFriendsCallbackInternalImplementation);
			Helper.AddCallback(ref zero2, clientData, completionDelegate, onHideFriendsCallbackInternal, Array.Empty<Delegate>());
			Bindings.EOS_UI_HideFriends(base.InnerHandle, zero, zero2, onHideFriendsCallbackInternal);
			Helper.TryMarshalDispose(ref zero);
		}

		// Token: 0x06000426 RID: 1062 RVA: 0x0000515C File Offset: 0x0000335C
		public bool IsValidKeyCombination(KeyCombination keyCombination)
		{
			bool result;
			Helper.TryMarshalGet(Bindings.EOS_UI_IsValidKeyCombination(base.InnerHandle, keyCombination), out result);
			return result;
		}

		// Token: 0x06000427 RID: 1063 RVA: 0x0000517E File Offset: 0x0000337E
		public void RemoveNotifyDisplaySettingsUpdated(ulong id)
		{
			Helper.TryRemoveCallbackByNotificationId(id);
			Bindings.EOS_UI_RemoveNotifyDisplaySettingsUpdated(base.InnerHandle, id);
		}

		// Token: 0x06000428 RID: 1064 RVA: 0x00005194 File Offset: 0x00003394
		public Result SetDisplayPreference(SetDisplayPreferenceOptions options)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<SetDisplayPreferenceOptionsInternal, SetDisplayPreferenceOptions>(ref zero, options);
			Result result = Bindings.EOS_UI_SetDisplayPreference(base.InnerHandle, zero);
			Helper.TryMarshalDispose(ref zero);
			return result;
		}

		// Token: 0x06000429 RID: 1065 RVA: 0x000051C4 File Offset: 0x000033C4
		public Result SetToggleFriendsKey(SetToggleFriendsKeyOptions options)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<SetToggleFriendsKeyOptionsInternal, SetToggleFriendsKeyOptions>(ref zero, options);
			Result result = Bindings.EOS_UI_SetToggleFriendsKey(base.InnerHandle, zero);
			Helper.TryMarshalDispose(ref zero);
			return result;
		}

		// Token: 0x0600042A RID: 1066 RVA: 0x000051F4 File Offset: 0x000033F4
		public void ShowFriends(ShowFriendsOptions options, object clientData, OnShowFriendsCallback completionDelegate)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<ShowFriendsOptionsInternal, ShowFriendsOptions>(ref zero, options);
			IntPtr zero2 = IntPtr.Zero;
			OnShowFriendsCallbackInternal onShowFriendsCallbackInternal = new OnShowFriendsCallbackInternal(UIInterface.OnShowFriendsCallbackInternalImplementation);
			Helper.AddCallback(ref zero2, clientData, completionDelegate, onShowFriendsCallbackInternal, Array.Empty<Delegate>());
			Bindings.EOS_UI_ShowFriends(base.InnerHandle, zero, zero2, onShowFriendsCallbackInternal);
			Helper.TryMarshalDispose(ref zero);
		}

		// Token: 0x0600042B RID: 1067 RVA: 0x00005248 File Offset: 0x00003448
		[MonoPInvokeCallback(typeof(OnDisplaySettingsUpdatedCallbackInternal))]
		internal static void OnDisplaySettingsUpdatedCallbackInternalImplementation(IntPtr data)
		{
			OnDisplaySettingsUpdatedCallback onDisplaySettingsUpdatedCallback;
			OnDisplaySettingsUpdatedCallbackInfo data2;
			if (Helper.TryGetAndRemoveCallback<OnDisplaySettingsUpdatedCallback, OnDisplaySettingsUpdatedCallbackInfoInternal, OnDisplaySettingsUpdatedCallbackInfo>(data, out onDisplaySettingsUpdatedCallback, out data2))
			{
				onDisplaySettingsUpdatedCallback(data2);
			}
		}

		// Token: 0x0600042C RID: 1068 RVA: 0x00005268 File Offset: 0x00003468
		[MonoPInvokeCallback(typeof(OnHideFriendsCallbackInternal))]
		internal static void OnHideFriendsCallbackInternalImplementation(IntPtr data)
		{
			OnHideFriendsCallback onHideFriendsCallback;
			HideFriendsCallbackInfo data2;
			if (Helper.TryGetAndRemoveCallback<OnHideFriendsCallback, HideFriendsCallbackInfoInternal, HideFriendsCallbackInfo>(data, out onHideFriendsCallback, out data2))
			{
				onHideFriendsCallback(data2);
			}
		}

		// Token: 0x0600042D RID: 1069 RVA: 0x00005288 File Offset: 0x00003488
		[MonoPInvokeCallback(typeof(OnShowFriendsCallbackInternal))]
		internal static void OnShowFriendsCallbackInternalImplementation(IntPtr data)
		{
			OnShowFriendsCallback onShowFriendsCallback;
			ShowFriendsCallbackInfo data2;
			if (Helper.TryGetAndRemoveCallback<OnShowFriendsCallback, ShowFriendsCallbackInfoInternal, ShowFriendsCallbackInfo>(data, out onShowFriendsCallback, out data2))
			{
				onShowFriendsCallback(data2);
			}
		}

		// Token: 0x0400022D RID: 557
		public const int AcknowledgecorrelationidApiLatest = 1;

		// Token: 0x0400022E RID: 558
		public const int AcknowledgeeventidApiLatest = 1;

		// Token: 0x0400022F RID: 559
		public const int AddnotifydisplaysettingsupdatedApiLatest = 1;

		// Token: 0x04000230 RID: 560
		public const int EventidInvalid = 0;

		// Token: 0x04000231 RID: 561
		public const int GetfriendsvisibleApiLatest = 1;

		// Token: 0x04000232 RID: 562
		public const int GettogglefriendskeyApiLatest = 1;

		// Token: 0x04000233 RID: 563
		public const int HidefriendsApiLatest = 1;

		// Token: 0x04000234 RID: 564
		public const int PrepresentApiLatest = 1;

		// Token: 0x04000235 RID: 565
		public const int ReportkeyeventApiLatest = 1;

		// Token: 0x04000236 RID: 566
		public const int SetdisplaypreferenceApiLatest = 1;

		// Token: 0x04000237 RID: 567
		public const int SettogglefriendskeyApiLatest = 1;

		// Token: 0x04000238 RID: 568
		public const int ShowfriendsApiLatest = 1;
	}
}
