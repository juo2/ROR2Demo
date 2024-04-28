using System;

namespace Epic.OnlineServices.Metrics
{
	// Token: 0x020002E5 RID: 741
	public sealed class MetricsInterface : Handle
	{
		// Token: 0x060012C5 RID: 4805 RVA: 0x000036D3 File Offset: 0x000018D3
		public MetricsInterface()
		{
		}

		// Token: 0x060012C6 RID: 4806 RVA: 0x000036DB File Offset: 0x000018DB
		public MetricsInterface(IntPtr innerHandle) : base(innerHandle)
		{
		}

		// Token: 0x060012C7 RID: 4807 RVA: 0x00014094 File Offset: 0x00012294
		public Result BeginPlayerSession(BeginPlayerSessionOptions options)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<BeginPlayerSessionOptionsInternal, BeginPlayerSessionOptions>(ref zero, options);
			Result result = Bindings.EOS_Metrics_BeginPlayerSession(base.InnerHandle, zero);
			Helper.TryMarshalDispose(ref zero);
			return result;
		}

		// Token: 0x060012C8 RID: 4808 RVA: 0x000140C4 File Offset: 0x000122C4
		public Result EndPlayerSession(EndPlayerSessionOptions options)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<EndPlayerSessionOptionsInternal, EndPlayerSessionOptions>(ref zero, options);
			Result result = Bindings.EOS_Metrics_EndPlayerSession(base.InnerHandle, zero);
			Helper.TryMarshalDispose(ref zero);
			return result;
		}

		// Token: 0x040008CE RID: 2254
		public const int BeginplayersessionApiLatest = 1;

		// Token: 0x040008CF RID: 2255
		public const int EndplayersessionApiLatest = 1;
	}
}
