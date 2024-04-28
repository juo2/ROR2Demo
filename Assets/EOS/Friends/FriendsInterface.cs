using System;

namespace Epic.OnlineServices.Friends
{
	// Token: 0x02000411 RID: 1041
	public sealed class FriendsInterface : Handle
	{
		// Token: 0x06001912 RID: 6418 RVA: 0x000036D3 File Offset: 0x000018D3
		public FriendsInterface()
		{
		}

		// Token: 0x06001913 RID: 6419 RVA: 0x000036DB File Offset: 0x000018DB
		public FriendsInterface(IntPtr innerHandle) : base(innerHandle)
		{
		}

		// Token: 0x06001914 RID: 6420 RVA: 0x0001A620 File Offset: 0x00018820
		public void AcceptInvite(AcceptInviteOptions options, object clientData, OnAcceptInviteCallback completionDelegate)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<AcceptInviteOptionsInternal, AcceptInviteOptions>(ref zero, options);
			IntPtr zero2 = IntPtr.Zero;
			OnAcceptInviteCallbackInternal onAcceptInviteCallbackInternal = new OnAcceptInviteCallbackInternal(FriendsInterface.OnAcceptInviteCallbackInternalImplementation);
			Helper.AddCallback(ref zero2, clientData, completionDelegate, onAcceptInviteCallbackInternal, Array.Empty<Delegate>());
			Bindings.EOS_Friends_AcceptInvite(base.InnerHandle, zero, zero2, onAcceptInviteCallbackInternal);
			Helper.TryMarshalDispose(ref zero);
		}

		// Token: 0x06001915 RID: 6421 RVA: 0x0001A674 File Offset: 0x00018874
		public ulong AddNotifyFriendsUpdate(AddNotifyFriendsUpdateOptions options, object clientData, OnFriendsUpdateCallback friendsUpdateHandler)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<AddNotifyFriendsUpdateOptionsInternal, AddNotifyFriendsUpdateOptions>(ref zero, options);
			IntPtr zero2 = IntPtr.Zero;
			OnFriendsUpdateCallbackInternal onFriendsUpdateCallbackInternal = new OnFriendsUpdateCallbackInternal(FriendsInterface.OnFriendsUpdateCallbackInternalImplementation);
			Helper.AddCallback(ref zero2, clientData, friendsUpdateHandler, onFriendsUpdateCallbackInternal, Array.Empty<Delegate>());
			ulong num = Bindings.EOS_Friends_AddNotifyFriendsUpdate(base.InnerHandle, zero, zero2, onFriendsUpdateCallbackInternal);
			Helper.TryMarshalDispose(ref zero);
			Helper.TryAssignNotificationIdToCallback(zero2, num);
			return num;
		}

		// Token: 0x06001916 RID: 6422 RVA: 0x0001A6D4 File Offset: 0x000188D4
		public EpicAccountId GetFriendAtIndex(GetFriendAtIndexOptions options)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<GetFriendAtIndexOptionsInternal, GetFriendAtIndexOptions>(ref zero, options);
			IntPtr source = Bindings.EOS_Friends_GetFriendAtIndex(base.InnerHandle, zero);
			Helper.TryMarshalDispose(ref zero);
			EpicAccountId result;
			Helper.TryMarshalGet<EpicAccountId>(source, out result);
			return result;
		}

		// Token: 0x06001917 RID: 6423 RVA: 0x0001A710 File Offset: 0x00018910
		public int GetFriendsCount(GetFriendsCountOptions options)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<GetFriendsCountOptionsInternal, GetFriendsCountOptions>(ref zero, options);
			int result = Bindings.EOS_Friends_GetFriendsCount(base.InnerHandle, zero);
			Helper.TryMarshalDispose(ref zero);
			return result;
		}

		// Token: 0x06001918 RID: 6424 RVA: 0x0001A740 File Offset: 0x00018940
		public FriendsStatus GetStatus(GetStatusOptions options)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<GetStatusOptionsInternal, GetStatusOptions>(ref zero, options);
			FriendsStatus result = Bindings.EOS_Friends_GetStatus(base.InnerHandle, zero);
			Helper.TryMarshalDispose(ref zero);
			return result;
		}

		// Token: 0x06001919 RID: 6425 RVA: 0x0001A770 File Offset: 0x00018970
		public void QueryFriends(QueryFriendsOptions options, object clientData, OnQueryFriendsCallback completionDelegate)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<QueryFriendsOptionsInternal, QueryFriendsOptions>(ref zero, options);
			IntPtr zero2 = IntPtr.Zero;
			OnQueryFriendsCallbackInternal onQueryFriendsCallbackInternal = new OnQueryFriendsCallbackInternal(FriendsInterface.OnQueryFriendsCallbackInternalImplementation);
			Helper.AddCallback(ref zero2, clientData, completionDelegate, onQueryFriendsCallbackInternal, Array.Empty<Delegate>());
			Bindings.EOS_Friends_QueryFriends(base.InnerHandle, zero, zero2, onQueryFriendsCallbackInternal);
			Helper.TryMarshalDispose(ref zero);
		}

		// Token: 0x0600191A RID: 6426 RVA: 0x0001A7C4 File Offset: 0x000189C4
		public void RejectInvite(RejectInviteOptions options, object clientData, OnRejectInviteCallback completionDelegate)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<RejectInviteOptionsInternal, RejectInviteOptions>(ref zero, options);
			IntPtr zero2 = IntPtr.Zero;
			OnRejectInviteCallbackInternal onRejectInviteCallbackInternal = new OnRejectInviteCallbackInternal(FriendsInterface.OnRejectInviteCallbackInternalImplementation);
			Helper.AddCallback(ref zero2, clientData, completionDelegate, onRejectInviteCallbackInternal, Array.Empty<Delegate>());
			Bindings.EOS_Friends_RejectInvite(base.InnerHandle, zero, zero2, onRejectInviteCallbackInternal);
			Helper.TryMarshalDispose(ref zero);
		}

		// Token: 0x0600191B RID: 6427 RVA: 0x0001A818 File Offset: 0x00018A18
		public void RemoveNotifyFriendsUpdate(ulong notificationId)
		{
			Helper.TryRemoveCallbackByNotificationId(notificationId);
			Bindings.EOS_Friends_RemoveNotifyFriendsUpdate(base.InnerHandle, notificationId);
		}

		// Token: 0x0600191C RID: 6428 RVA: 0x0001A830 File Offset: 0x00018A30
		public void SendInvite(SendInviteOptions options, object clientData, OnSendInviteCallback completionDelegate)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<SendInviteOptionsInternal, SendInviteOptions>(ref zero, options);
			IntPtr zero2 = IntPtr.Zero;
			OnSendInviteCallbackInternal onSendInviteCallbackInternal = new OnSendInviteCallbackInternal(FriendsInterface.OnSendInviteCallbackInternalImplementation);
			Helper.AddCallback(ref zero2, clientData, completionDelegate, onSendInviteCallbackInternal, Array.Empty<Delegate>());
			Bindings.EOS_Friends_SendInvite(base.InnerHandle, zero, zero2, onSendInviteCallbackInternal);
			Helper.TryMarshalDispose(ref zero);
		}

		// Token: 0x0600191D RID: 6429 RVA: 0x0001A884 File Offset: 0x00018A84
		[MonoPInvokeCallback(typeof(OnAcceptInviteCallbackInternal))]
		internal static void OnAcceptInviteCallbackInternalImplementation(IntPtr data)
		{
			OnAcceptInviteCallback onAcceptInviteCallback;
			AcceptInviteCallbackInfo data2;
			if (Helper.TryGetAndRemoveCallback<OnAcceptInviteCallback, AcceptInviteCallbackInfoInternal, AcceptInviteCallbackInfo>(data, out onAcceptInviteCallback, out data2))
			{
				onAcceptInviteCallback(data2);
			}
		}

		// Token: 0x0600191E RID: 6430 RVA: 0x0001A8A4 File Offset: 0x00018AA4
		[MonoPInvokeCallback(typeof(OnFriendsUpdateCallbackInternal))]
		internal static void OnFriendsUpdateCallbackInternalImplementation(IntPtr data)
		{
			OnFriendsUpdateCallback onFriendsUpdateCallback;
			OnFriendsUpdateInfo data2;
			if (Helper.TryGetAndRemoveCallback<OnFriendsUpdateCallback, OnFriendsUpdateInfoInternal, OnFriendsUpdateInfo>(data, out onFriendsUpdateCallback, out data2))
			{
				onFriendsUpdateCallback(data2);
			}
		}

		// Token: 0x0600191F RID: 6431 RVA: 0x0001A8C4 File Offset: 0x00018AC4
		[MonoPInvokeCallback(typeof(OnQueryFriendsCallbackInternal))]
		internal static void OnQueryFriendsCallbackInternalImplementation(IntPtr data)
		{
			OnQueryFriendsCallback onQueryFriendsCallback;
			QueryFriendsCallbackInfo data2;
			if (Helper.TryGetAndRemoveCallback<OnQueryFriendsCallback, QueryFriendsCallbackInfoInternal, QueryFriendsCallbackInfo>(data, out onQueryFriendsCallback, out data2))
			{
				onQueryFriendsCallback(data2);
			}
		}

		// Token: 0x06001920 RID: 6432 RVA: 0x0001A8E4 File Offset: 0x00018AE4
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

		// Token: 0x06001921 RID: 6433 RVA: 0x0001A904 File Offset: 0x00018B04
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

		// Token: 0x04000BAD RID: 2989
		public const int AcceptinviteApiLatest = 1;

		// Token: 0x04000BAE RID: 2990
		public const int AddnotifyfriendsupdateApiLatest = 1;

		// Token: 0x04000BAF RID: 2991
		public const int GetfriendatindexApiLatest = 1;

		// Token: 0x04000BB0 RID: 2992
		public const int GetfriendscountApiLatest = 1;

		// Token: 0x04000BB1 RID: 2993
		public const int GetstatusApiLatest = 1;

		// Token: 0x04000BB2 RID: 2994
		public const int QueryfriendsApiLatest = 1;

		// Token: 0x04000BB3 RID: 2995
		public const int RejectinviteApiLatest = 1;

		// Token: 0x04000BB4 RID: 2996
		public const int SendinviteApiLatest = 1;
	}
}
