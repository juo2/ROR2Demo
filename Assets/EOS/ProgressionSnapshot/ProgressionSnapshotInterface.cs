using System;

namespace Epic.OnlineServices.ProgressionSnapshot
{
	// Token: 0x020001FE RID: 510
	public sealed class ProgressionSnapshotInterface : Handle
	{
		// Token: 0x06000D67 RID: 3431 RVA: 0x000036D3 File Offset: 0x000018D3
		public ProgressionSnapshotInterface()
		{
		}

		// Token: 0x06000D68 RID: 3432 RVA: 0x000036DB File Offset: 0x000018DB
		public ProgressionSnapshotInterface(IntPtr innerHandle) : base(innerHandle)
		{
		}

		// Token: 0x06000D69 RID: 3433 RVA: 0x0000E688 File Offset: 0x0000C888
		public Result AddProgression(AddProgressionOptions options)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<AddProgressionOptionsInternal, AddProgressionOptions>(ref zero, options);
			Result result = Bindings.EOS_ProgressionSnapshot_AddProgression(base.InnerHandle, zero);
			Helper.TryMarshalDispose(ref zero);
			return result;
		}

		// Token: 0x06000D6A RID: 3434 RVA: 0x0000E6B8 File Offset: 0x0000C8B8
		public Result BeginSnapshot(BeginSnapshotOptions options, out uint outSnapshotId)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<BeginSnapshotOptionsInternal, BeginSnapshotOptions>(ref zero, options);
			outSnapshotId = Helper.GetDefault<uint>();
			Result result = Bindings.EOS_ProgressionSnapshot_BeginSnapshot(base.InnerHandle, zero, ref outSnapshotId);
			Helper.TryMarshalDispose(ref zero);
			return result;
		}

		// Token: 0x06000D6B RID: 3435 RVA: 0x0000E6F0 File Offset: 0x0000C8F0
		public void DeleteSnapshot(DeleteSnapshotOptions options, object clientData, OnDeleteSnapshotCallback completionDelegate)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<DeleteSnapshotOptionsInternal, DeleteSnapshotOptions>(ref zero, options);
			IntPtr zero2 = IntPtr.Zero;
			OnDeleteSnapshotCallbackInternal onDeleteSnapshotCallbackInternal = new OnDeleteSnapshotCallbackInternal(ProgressionSnapshotInterface.OnDeleteSnapshotCallbackInternalImplementation);
			Helper.AddCallback(ref zero2, clientData, completionDelegate, onDeleteSnapshotCallbackInternal, Array.Empty<Delegate>());
			Bindings.EOS_ProgressionSnapshot_DeleteSnapshot(base.InnerHandle, zero, zero2, onDeleteSnapshotCallbackInternal);
			Helper.TryMarshalDispose(ref zero);
		}

		// Token: 0x06000D6C RID: 3436 RVA: 0x0000E744 File Offset: 0x0000C944
		public Result EndSnapshot(EndSnapshotOptions options)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<EndSnapshotOptionsInternal, EndSnapshotOptions>(ref zero, options);
			Result result = Bindings.EOS_ProgressionSnapshot_EndSnapshot(base.InnerHandle, zero);
			Helper.TryMarshalDispose(ref zero);
			return result;
		}

		// Token: 0x06000D6D RID: 3437 RVA: 0x0000E774 File Offset: 0x0000C974
		public void SubmitSnapshot(SubmitSnapshotOptions options, object clientData, OnSubmitSnapshotCallback completionDelegate)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<SubmitSnapshotOptionsInternal, SubmitSnapshotOptions>(ref zero, options);
			IntPtr zero2 = IntPtr.Zero;
			OnSubmitSnapshotCallbackInternal onSubmitSnapshotCallbackInternal = new OnSubmitSnapshotCallbackInternal(ProgressionSnapshotInterface.OnSubmitSnapshotCallbackInternalImplementation);
			Helper.AddCallback(ref zero2, clientData, completionDelegate, onSubmitSnapshotCallbackInternal, Array.Empty<Delegate>());
			Bindings.EOS_ProgressionSnapshot_SubmitSnapshot(base.InnerHandle, zero, zero2, onSubmitSnapshotCallbackInternal);
			Helper.TryMarshalDispose(ref zero);
		}

		// Token: 0x06000D6E RID: 3438 RVA: 0x0000E7C8 File Offset: 0x0000C9C8
		[MonoPInvokeCallback(typeof(OnDeleteSnapshotCallbackInternal))]
		internal static void OnDeleteSnapshotCallbackInternalImplementation(IntPtr data)
		{
			OnDeleteSnapshotCallback onDeleteSnapshotCallback;
			DeleteSnapshotCallbackInfo data2;
			if (Helper.TryGetAndRemoveCallback<OnDeleteSnapshotCallback, DeleteSnapshotCallbackInfoInternal, DeleteSnapshotCallbackInfo>(data, out onDeleteSnapshotCallback, out data2))
			{
				onDeleteSnapshotCallback(data2);
			}
		}

		// Token: 0x06000D6F RID: 3439 RVA: 0x0000E7E8 File Offset: 0x0000C9E8
		[MonoPInvokeCallback(typeof(OnSubmitSnapshotCallbackInternal))]
		internal static void OnSubmitSnapshotCallbackInternalImplementation(IntPtr data)
		{
			OnSubmitSnapshotCallback onSubmitSnapshotCallback;
			SubmitSnapshotCallbackInfo data2;
			if (Helper.TryGetAndRemoveCallback<OnSubmitSnapshotCallback, SubmitSnapshotCallbackInfoInternal, SubmitSnapshotCallbackInfo>(data, out onSubmitSnapshotCallback, out data2))
			{
				onSubmitSnapshotCallback(data2);
			}
		}

		// Token: 0x04000650 RID: 1616
		public const int AddprogressionApiLatest = 1;

		// Token: 0x04000651 RID: 1617
		public const int BeginsnapshotApiLatest = 1;

		// Token: 0x04000652 RID: 1618
		public const int DeletesnapshotApiLatest = 1;

		// Token: 0x04000653 RID: 1619
		public const int EndsnapshotApiLatest = 1;

		// Token: 0x04000654 RID: 1620
		public const int InvalidProgressionsnapshotid = 0;

		// Token: 0x04000655 RID: 1621
		public const int SubmitsnapshotApiLatest = 1;
	}
}
