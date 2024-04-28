using System;

namespace Epic.OnlineServices
{
	// Token: 0x02000014 RID: 20
	public sealed class ContinuanceToken : Handle
	{
		// Token: 0x0600027F RID: 639 RVA: 0x000036D3 File Offset: 0x000018D3
		public ContinuanceToken()
		{
		}

		// Token: 0x06000280 RID: 640 RVA: 0x000036DB File Offset: 0x000018DB
		public ContinuanceToken(IntPtr innerHandle) : base(innerHandle)
		{
		}

		// Token: 0x06000281 RID: 641 RVA: 0x000036E4 File Offset: 0x000018E4
		public Result ToString(out string outBuffer)
		{
			IntPtr zero = IntPtr.Zero;
			int size = 1024;
			Helper.TryMarshalAllocate(ref zero, size);
			Result result = Bindings.EOS_ContinuanceToken_ToString(base.InnerHandle, zero, ref size);
			Helper.TryMarshalGet(zero, out outBuffer);
			Helper.TryMarshalDispose(ref zero);
			return result;
		}

		// Token: 0x06000282 RID: 642 RVA: 0x00003724 File Offset: 0x00001924
		public override string ToString()
		{
			string result;
			this.ToString(out result);
			return result;
		}
	}
}
