using System;

namespace Epic.OnlineServices.CustomInvites
{
	// Token: 0x02000497 RID: 1175
	public sealed class CustomInvitesInterface : Handle
	{
		// Token: 0x06001C80 RID: 7296 RVA: 0x000036D3 File Offset: 0x000018D3
		public CustomInvitesInterface()
		{
		}

		// Token: 0x06001C81 RID: 7297 RVA: 0x000036DB File Offset: 0x000018DB
		public CustomInvitesInterface(IntPtr innerHandle) : base(innerHandle)
		{
		}

		// Token: 0x06001C82 RID: 7298 RVA: 0x0001E144 File Offset: 0x0001C344
		public ulong AddNotifyCustomInviteAccepted(AddNotifyCustomInviteAcceptedOptions options, object clientData, OnCustomInviteAcceptedCallback notificationFn)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<AddNotifyCustomInviteAcceptedOptionsInternal, AddNotifyCustomInviteAcceptedOptions>(ref zero, options);
			IntPtr zero2 = IntPtr.Zero;
			OnCustomInviteAcceptedCallbackInternal onCustomInviteAcceptedCallbackInternal = new OnCustomInviteAcceptedCallbackInternal(CustomInvitesInterface.OnCustomInviteAcceptedCallbackInternalImplementation);
			Helper.AddCallback(ref zero2, clientData, notificationFn, onCustomInviteAcceptedCallbackInternal, Array.Empty<Delegate>());
			ulong num = Bindings.EOS_CustomInvites_AddNotifyCustomInviteAccepted(base.InnerHandle, zero, zero2, onCustomInviteAcceptedCallbackInternal);
			Helper.TryMarshalDispose(ref zero);
			Helper.TryAssignNotificationIdToCallback(zero2, num);
			return num;
		}

		// Token: 0x06001C83 RID: 7299 RVA: 0x0001E1A4 File Offset: 0x0001C3A4
		public ulong AddNotifyCustomInviteReceived(AddNotifyCustomInviteReceivedOptions options, object clientData, OnCustomInviteReceivedCallback notificationFn)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<AddNotifyCustomInviteReceivedOptionsInternal, AddNotifyCustomInviteReceivedOptions>(ref zero, options);
			IntPtr zero2 = IntPtr.Zero;
			OnCustomInviteReceivedCallbackInternal onCustomInviteReceivedCallbackInternal = new OnCustomInviteReceivedCallbackInternal(CustomInvitesInterface.OnCustomInviteReceivedCallbackInternalImplementation);
			Helper.AddCallback(ref zero2, clientData, notificationFn, onCustomInviteReceivedCallbackInternal, Array.Empty<Delegate>());
			ulong num = Bindings.EOS_CustomInvites_AddNotifyCustomInviteReceived(base.InnerHandle, zero, zero2, onCustomInviteReceivedCallbackInternal);
			Helper.TryMarshalDispose(ref zero);
			Helper.TryAssignNotificationIdToCallback(zero2, num);
			return num;
		}

		// Token: 0x06001C84 RID: 7300 RVA: 0x0001E204 File Offset: 0x0001C404
		public Result FinalizeInvite(FinalizeInviteOptions options)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<FinalizeInviteOptionsInternal, FinalizeInviteOptions>(ref zero, options);
			Result result = Bindings.EOS_CustomInvites_FinalizeInvite(base.InnerHandle, zero);
			Helper.TryMarshalDispose(ref zero);
			return result;
		}

		// Token: 0x06001C85 RID: 7301 RVA: 0x0001E234 File Offset: 0x0001C434
		public void RemoveNotifyCustomInviteAccepted(ulong inId)
		{
			Helper.TryRemoveCallbackByNotificationId(inId);
			Bindings.EOS_CustomInvites_RemoveNotifyCustomInviteAccepted(base.InnerHandle, inId);
		}

		// Token: 0x06001C86 RID: 7302 RVA: 0x0001E249 File Offset: 0x0001C449
		public void RemoveNotifyCustomInviteReceived(ulong inId)
		{
			Helper.TryRemoveCallbackByNotificationId(inId);
			Bindings.EOS_CustomInvites_RemoveNotifyCustomInviteReceived(base.InnerHandle, inId);
		}

		// Token: 0x06001C87 RID: 7303 RVA: 0x0001E260 File Offset: 0x0001C460
		public void SendCustomInvite(SendCustomInviteOptions options, object clientData, OnSendCustomInviteCallback completionDelegate)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<SendCustomInviteOptionsInternal, SendCustomInviteOptions>(ref zero, options);
			IntPtr zero2 = IntPtr.Zero;
			OnSendCustomInviteCallbackInternal onSendCustomInviteCallbackInternal = new OnSendCustomInviteCallbackInternal(CustomInvitesInterface.OnSendCustomInviteCallbackInternalImplementation);
			Helper.AddCallback(ref zero2, clientData, completionDelegate, onSendCustomInviteCallbackInternal, Array.Empty<Delegate>());
			Bindings.EOS_CustomInvites_SendCustomInvite(base.InnerHandle, zero, zero2, onSendCustomInviteCallbackInternal);
			Helper.TryMarshalDispose(ref zero);
		}

		// Token: 0x06001C88 RID: 7304 RVA: 0x0001E2B4 File Offset: 0x0001C4B4
		public Result SetCustomInvite(SetCustomInviteOptions options)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<SetCustomInviteOptionsInternal, SetCustomInviteOptions>(ref zero, options);
			Result result = Bindings.EOS_CustomInvites_SetCustomInvite(base.InnerHandle, zero);
			Helper.TryMarshalDispose(ref zero);
			return result;
		}

		// Token: 0x06001C89 RID: 7305 RVA: 0x0001E2E4 File Offset: 0x0001C4E4
		[MonoPInvokeCallback(typeof(OnCustomInviteAcceptedCallbackInternal))]
		internal static void OnCustomInviteAcceptedCallbackInternalImplementation(IntPtr data)
		{
			OnCustomInviteAcceptedCallback onCustomInviteAcceptedCallback;
			OnCustomInviteAcceptedCallbackInfo data2;
			if (Helper.TryGetAndRemoveCallback<OnCustomInviteAcceptedCallback, OnCustomInviteAcceptedCallbackInfoInternal, OnCustomInviteAcceptedCallbackInfo>(data, out onCustomInviteAcceptedCallback, out data2))
			{
				onCustomInviteAcceptedCallback(data2);
			}
		}

		// Token: 0x06001C8A RID: 7306 RVA: 0x0001E304 File Offset: 0x0001C504
		[MonoPInvokeCallback(typeof(OnCustomInviteReceivedCallbackInternal))]
		internal static void OnCustomInviteReceivedCallbackInternalImplementation(IntPtr data)
		{
			OnCustomInviteReceivedCallback onCustomInviteReceivedCallback;
			OnCustomInviteReceivedCallbackInfo data2;
			if (Helper.TryGetAndRemoveCallback<OnCustomInviteReceivedCallback, OnCustomInviteReceivedCallbackInfoInternal, OnCustomInviteReceivedCallbackInfo>(data, out onCustomInviteReceivedCallback, out data2))
			{
				onCustomInviteReceivedCallback(data2);
			}
		}

		// Token: 0x06001C8B RID: 7307 RVA: 0x0001E324 File Offset: 0x0001C524
		[MonoPInvokeCallback(typeof(OnSendCustomInviteCallbackInternal))]
		internal static void OnSendCustomInviteCallbackInternalImplementation(IntPtr data)
		{
			OnSendCustomInviteCallback onSendCustomInviteCallback;
			SendCustomInviteCallbackInfo data2;
			if (Helper.TryGetAndRemoveCallback<OnSendCustomInviteCallback, SendCustomInviteCallbackInfoInternal, SendCustomInviteCallbackInfo>(data, out onSendCustomInviteCallback, out data2))
			{
				onSendCustomInviteCallback(data2);
			}
		}

		// Token: 0x04000D4E RID: 3406
		public const int AddnotifycustominviteacceptedApiLatest = 1;

		// Token: 0x04000D4F RID: 3407
		public const int AddnotifycustominvitereceivedApiLatest = 1;

		// Token: 0x04000D50 RID: 3408
		public const int FinalizeinviteApiLatest = 1;

		// Token: 0x04000D51 RID: 3409
		public const int MaxPayloadLength = 500;

		// Token: 0x04000D52 RID: 3410
		public const int SendcustominviteApiLatest = 1;

		// Token: 0x04000D53 RID: 3411
		public const int SetcustominviteApiLatest = 1;
	}
}
