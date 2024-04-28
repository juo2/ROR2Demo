using System;

namespace Epic.OnlineServices.Reports
{
	// Token: 0x0200015F RID: 351
	public sealed class ReportsInterface : Handle
	{
		// Token: 0x06000995 RID: 2453 RVA: 0x000036D3 File Offset: 0x000018D3
		public ReportsInterface()
		{
		}

		// Token: 0x06000996 RID: 2454 RVA: 0x000036DB File Offset: 0x000018DB
		public ReportsInterface(IntPtr innerHandle) : base(innerHandle)
		{
		}

		// Token: 0x06000997 RID: 2455 RVA: 0x0000A9CC File Offset: 0x00008BCC
		public void SendPlayerBehaviorReport(SendPlayerBehaviorReportOptions options, object clientData, OnSendPlayerBehaviorReportCompleteCallback completionDelegate)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<SendPlayerBehaviorReportOptionsInternal, SendPlayerBehaviorReportOptions>(ref zero, options);
			IntPtr zero2 = IntPtr.Zero;
			OnSendPlayerBehaviorReportCompleteCallbackInternal onSendPlayerBehaviorReportCompleteCallbackInternal = new OnSendPlayerBehaviorReportCompleteCallbackInternal(ReportsInterface.OnSendPlayerBehaviorReportCompleteCallbackInternalImplementation);
			Helper.AddCallback(ref zero2, clientData, completionDelegate, onSendPlayerBehaviorReportCompleteCallbackInternal, Array.Empty<Delegate>());
			Bindings.EOS_Reports_SendPlayerBehaviorReport(base.InnerHandle, zero, zero2, onSendPlayerBehaviorReportCompleteCallbackInternal);
			Helper.TryMarshalDispose(ref zero);
		}

		// Token: 0x06000998 RID: 2456 RVA: 0x0000AA20 File Offset: 0x00008C20
		[MonoPInvokeCallback(typeof(OnSendPlayerBehaviorReportCompleteCallbackInternal))]
		internal static void OnSendPlayerBehaviorReportCompleteCallbackInternalImplementation(IntPtr data)
		{
			OnSendPlayerBehaviorReportCompleteCallback onSendPlayerBehaviorReportCompleteCallback;
			SendPlayerBehaviorReportCompleteCallbackInfo data2;
			if (Helper.TryGetAndRemoveCallback<OnSendPlayerBehaviorReportCompleteCallback, SendPlayerBehaviorReportCompleteCallbackInfoInternal, SendPlayerBehaviorReportCompleteCallbackInfo>(data, out onSendPlayerBehaviorReportCompleteCallback, out data2))
			{
				onSendPlayerBehaviorReportCompleteCallback(data2);
			}
		}

		// Token: 0x0400049B RID: 1179
		public const int ReportcontextMaxLength = 4096;

		// Token: 0x0400049C RID: 1180
		public const int ReportmessageMaxLength = 512;

		// Token: 0x0400049D RID: 1181
		public const int SendplayerbehaviorreportApiLatest = 2;
	}
}
