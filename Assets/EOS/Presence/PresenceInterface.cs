using System;

namespace Epic.OnlineServices.Presence
{
	// Token: 0x0200021D RID: 541
	public sealed class PresenceInterface : Handle
	{
		// Token: 0x06000E20 RID: 3616 RVA: 0x000036D3 File Offset: 0x000018D3
		public PresenceInterface()
		{
		}

		// Token: 0x06000E21 RID: 3617 RVA: 0x000036DB File Offset: 0x000018DB
		public PresenceInterface(IntPtr innerHandle) : base(innerHandle)
		{
		}

		// Token: 0x06000E22 RID: 3618 RVA: 0x0000F298 File Offset: 0x0000D498
		public ulong AddNotifyJoinGameAccepted(AddNotifyJoinGameAcceptedOptions options, object clientData, OnJoinGameAcceptedCallback notificationFn)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<AddNotifyJoinGameAcceptedOptionsInternal, AddNotifyJoinGameAcceptedOptions>(ref zero, options);
			IntPtr zero2 = IntPtr.Zero;
			OnJoinGameAcceptedCallbackInternal onJoinGameAcceptedCallbackInternal = new OnJoinGameAcceptedCallbackInternal(PresenceInterface.OnJoinGameAcceptedCallbackInternalImplementation);
			Helper.AddCallback(ref zero2, clientData, notificationFn, onJoinGameAcceptedCallbackInternal, Array.Empty<Delegate>());
			ulong num = Bindings.EOS_Presence_AddNotifyJoinGameAccepted(base.InnerHandle, zero, zero2, onJoinGameAcceptedCallbackInternal);
			Helper.TryMarshalDispose(ref zero);
			Helper.TryAssignNotificationIdToCallback(zero2, num);
			return num;
		}

		// Token: 0x06000E23 RID: 3619 RVA: 0x0000F2F8 File Offset: 0x0000D4F8
		public ulong AddNotifyOnPresenceChanged(AddNotifyOnPresenceChangedOptions options, object clientData, OnPresenceChangedCallback notificationHandler)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<AddNotifyOnPresenceChangedOptionsInternal, AddNotifyOnPresenceChangedOptions>(ref zero, options);
			IntPtr zero2 = IntPtr.Zero;
			OnPresenceChangedCallbackInternal onPresenceChangedCallbackInternal = new OnPresenceChangedCallbackInternal(PresenceInterface.OnPresenceChangedCallbackInternalImplementation);
			Helper.AddCallback(ref zero2, clientData, notificationHandler, onPresenceChangedCallbackInternal, Array.Empty<Delegate>());
			ulong num = Bindings.EOS_Presence_AddNotifyOnPresenceChanged(base.InnerHandle, zero, zero2, onPresenceChangedCallbackInternal);
			Helper.TryMarshalDispose(ref zero);
			Helper.TryAssignNotificationIdToCallback(zero2, num);
			return num;
		}

		// Token: 0x06000E24 RID: 3620 RVA: 0x0000F358 File Offset: 0x0000D558
		public Result CopyPresence(CopyPresenceOptions options, out Info outPresence)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<CopyPresenceOptionsInternal, CopyPresenceOptions>(ref zero, options);
			IntPtr zero2 = IntPtr.Zero;
			Result result = Bindings.EOS_Presence_CopyPresence(base.InnerHandle, zero, ref zero2);
			Helper.TryMarshalDispose(ref zero);
			if (Helper.TryMarshalGet<InfoInternal, Info>(zero2, out outPresence))
			{
				Bindings.EOS_Presence_Info_Release(zero2);
			}
			return result;
		}

		// Token: 0x06000E25 RID: 3621 RVA: 0x0000F3A0 File Offset: 0x0000D5A0
		public Result CreatePresenceModification(CreatePresenceModificationOptions options, out PresenceModification outPresenceModificationHandle)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<CreatePresenceModificationOptionsInternal, CreatePresenceModificationOptions>(ref zero, options);
			IntPtr zero2 = IntPtr.Zero;
			Result result = Bindings.EOS_Presence_CreatePresenceModification(base.InnerHandle, zero, ref zero2);
			Helper.TryMarshalDispose(ref zero);
			Helper.TryMarshalGet<PresenceModification>(zero2, out outPresenceModificationHandle);
			return result;
		}

		// Token: 0x06000E26 RID: 3622 RVA: 0x0000F3E0 File Offset: 0x0000D5E0
		public Result GetJoinInfo(GetJoinInfoOptions options, out string outBuffer)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<GetJoinInfoOptionsInternal, GetJoinInfoOptions>(ref zero, options);
			IntPtr zero2 = IntPtr.Zero;
			int size = 256;
			Helper.TryMarshalAllocate(ref zero2, size);
			Result result = Bindings.EOS_Presence_GetJoinInfo(base.InnerHandle, zero, zero2, ref size);
			Helper.TryMarshalDispose(ref zero);
			Helper.TryMarshalGet(zero2, out outBuffer);
			Helper.TryMarshalDispose(ref zero2);
			return result;
		}

		// Token: 0x06000E27 RID: 3623 RVA: 0x0000F438 File Offset: 0x0000D638
		public bool HasPresence(HasPresenceOptions options)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<HasPresenceOptionsInternal, HasPresenceOptions>(ref zero, options);
			int source = Bindings.EOS_Presence_HasPresence(base.InnerHandle, zero);
			Helper.TryMarshalDispose(ref zero);
			bool result;
			Helper.TryMarshalGet(source, out result);
			return result;
		}

		// Token: 0x06000E28 RID: 3624 RVA: 0x0000F474 File Offset: 0x0000D674
		public void QueryPresence(QueryPresenceOptions options, object clientData, OnQueryPresenceCompleteCallback completionDelegate)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<QueryPresenceOptionsInternal, QueryPresenceOptions>(ref zero, options);
			IntPtr zero2 = IntPtr.Zero;
			OnQueryPresenceCompleteCallbackInternal onQueryPresenceCompleteCallbackInternal = new OnQueryPresenceCompleteCallbackInternal(PresenceInterface.OnQueryPresenceCompleteCallbackInternalImplementation);
			Helper.AddCallback(ref zero2, clientData, completionDelegate, onQueryPresenceCompleteCallbackInternal, Array.Empty<Delegate>());
			Bindings.EOS_Presence_QueryPresence(base.InnerHandle, zero, zero2, onQueryPresenceCompleteCallbackInternal);
			Helper.TryMarshalDispose(ref zero);
		}

		// Token: 0x06000E29 RID: 3625 RVA: 0x0000F4C8 File Offset: 0x0000D6C8
		public void RemoveNotifyJoinGameAccepted(ulong inId)
		{
			Helper.TryRemoveCallbackByNotificationId(inId);
			Bindings.EOS_Presence_RemoveNotifyJoinGameAccepted(base.InnerHandle, inId);
		}

		// Token: 0x06000E2A RID: 3626 RVA: 0x0000F4DD File Offset: 0x0000D6DD
		public void RemoveNotifyOnPresenceChanged(ulong notificationId)
		{
			Helper.TryRemoveCallbackByNotificationId(notificationId);
			Bindings.EOS_Presence_RemoveNotifyOnPresenceChanged(base.InnerHandle, notificationId);
		}

		// Token: 0x06000E2B RID: 3627 RVA: 0x0000F4F4 File Offset: 0x0000D6F4
		public void SetPresence(SetPresenceOptions options, object clientData, SetPresenceCompleteCallback completionDelegate)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<SetPresenceOptionsInternal, SetPresenceOptions>(ref zero, options);
			IntPtr zero2 = IntPtr.Zero;
			SetPresenceCompleteCallbackInternal setPresenceCompleteCallbackInternal = new SetPresenceCompleteCallbackInternal(PresenceInterface.SetPresenceCompleteCallbackInternalImplementation);
			Helper.AddCallback(ref zero2, clientData, completionDelegate, setPresenceCompleteCallbackInternal, Array.Empty<Delegate>());
			Bindings.EOS_Presence_SetPresence(base.InnerHandle, zero, zero2, setPresenceCompleteCallbackInternal);
			Helper.TryMarshalDispose(ref zero);
		}

		// Token: 0x06000E2C RID: 3628 RVA: 0x0000F548 File Offset: 0x0000D748
		[MonoPInvokeCallback(typeof(OnJoinGameAcceptedCallbackInternal))]
		internal static void OnJoinGameAcceptedCallbackInternalImplementation(IntPtr data)
		{
			OnJoinGameAcceptedCallback onJoinGameAcceptedCallback;
			JoinGameAcceptedCallbackInfo data2;
			if (Helper.TryGetAndRemoveCallback<OnJoinGameAcceptedCallback, JoinGameAcceptedCallbackInfoInternal, JoinGameAcceptedCallbackInfo>(data, out onJoinGameAcceptedCallback, out data2))
			{
				onJoinGameAcceptedCallback(data2);
			}
		}

		// Token: 0x06000E2D RID: 3629 RVA: 0x0000F568 File Offset: 0x0000D768
		[MonoPInvokeCallback(typeof(OnPresenceChangedCallbackInternal))]
		internal static void OnPresenceChangedCallbackInternalImplementation(IntPtr data)
		{
			OnPresenceChangedCallback onPresenceChangedCallback;
			PresenceChangedCallbackInfo data2;
			if (Helper.TryGetAndRemoveCallback<OnPresenceChangedCallback, PresenceChangedCallbackInfoInternal, PresenceChangedCallbackInfo>(data, out onPresenceChangedCallback, out data2))
			{
				onPresenceChangedCallback(data2);
			}
		}

		// Token: 0x06000E2E RID: 3630 RVA: 0x0000F588 File Offset: 0x0000D788
		[MonoPInvokeCallback(typeof(OnQueryPresenceCompleteCallbackInternal))]
		internal static void OnQueryPresenceCompleteCallbackInternalImplementation(IntPtr data)
		{
			OnQueryPresenceCompleteCallback onQueryPresenceCompleteCallback;
			QueryPresenceCallbackInfo data2;
			if (Helper.TryGetAndRemoveCallback<OnQueryPresenceCompleteCallback, QueryPresenceCallbackInfoInternal, QueryPresenceCallbackInfo>(data, out onQueryPresenceCompleteCallback, out data2))
			{
				onQueryPresenceCompleteCallback(data2);
			}
		}

		// Token: 0x06000E2F RID: 3631 RVA: 0x0000F5A8 File Offset: 0x0000D7A8
		[MonoPInvokeCallback(typeof(SetPresenceCompleteCallbackInternal))]
		internal static void SetPresenceCompleteCallbackInternalImplementation(IntPtr data)
		{
			SetPresenceCompleteCallback setPresenceCompleteCallback;
			SetPresenceCallbackInfo data2;
			if (Helper.TryGetAndRemoveCallback<SetPresenceCompleteCallback, SetPresenceCallbackInfoInternal, SetPresenceCallbackInfo>(data, out setPresenceCompleteCallback, out data2))
			{
				setPresenceCompleteCallback(data2);
			}
		}

		// Token: 0x0400069A RID: 1690
		public const int AddnotifyjoingameacceptedApiLatest = 2;

		// Token: 0x0400069B RID: 1691
		public const int AddnotifyonpresencechangedApiLatest = 1;

		// Token: 0x0400069C RID: 1692
		public const int CopypresenceApiLatest = 2;

		// Token: 0x0400069D RID: 1693
		public const int CreatepresencemodificationApiLatest = 1;

		// Token: 0x0400069E RID: 1694
		public const int DataMaxKeyLength = 64;

		// Token: 0x0400069F RID: 1695
		public const int DataMaxKeys = 32;

		// Token: 0x040006A0 RID: 1696
		public const int DataMaxValueLength = 255;

		// Token: 0x040006A1 RID: 1697
		public const int DatarecordApiLatest = 1;

		// Token: 0x040006A2 RID: 1698
		public const int DeletedataApiLatest = 1;

		// Token: 0x040006A3 RID: 1699
		public const int GetjoininfoApiLatest = 1;

		// Token: 0x040006A4 RID: 1700
		public const int HaspresenceApiLatest = 1;

		// Token: 0x040006A5 RID: 1701
		public const int InfoApiLatest = 2;

		// Token: 0x040006A6 RID: 1702
		public const int QuerypresenceApiLatest = 1;

		// Token: 0x040006A7 RID: 1703
		public const int RichTextMaxValueLength = 255;

		// Token: 0x040006A8 RID: 1704
		public const int SetdataApiLatest = 1;

		// Token: 0x040006A9 RID: 1705
		public const int SetpresenceApiLatest = 1;

		// Token: 0x040006AA RID: 1706
		public const int SetrawrichtextApiLatest = 1;

		// Token: 0x040006AB RID: 1707
		public const int SetstatusApiLatest = 1;
	}
}
