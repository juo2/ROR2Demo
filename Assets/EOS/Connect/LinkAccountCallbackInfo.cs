using System;

namespace Epic.OnlineServices.Connect
{
	// Token: 0x020004D3 RID: 1235
	public class LinkAccountCallbackInfo : ICallbackInfo, ISettable
	{
		// Token: 0x1700091F RID: 2335
		// (get) Token: 0x06001DF4 RID: 7668 RVA: 0x0001FE27 File Offset: 0x0001E027
		// (set) Token: 0x06001DF5 RID: 7669 RVA: 0x0001FE2F File Offset: 0x0001E02F
		public Result ResultCode { get; private set; }

		// Token: 0x17000920 RID: 2336
		// (get) Token: 0x06001DF6 RID: 7670 RVA: 0x0001FE38 File Offset: 0x0001E038
		// (set) Token: 0x06001DF7 RID: 7671 RVA: 0x0001FE40 File Offset: 0x0001E040
		public object ClientData { get; private set; }

		// Token: 0x17000921 RID: 2337
		// (get) Token: 0x06001DF8 RID: 7672 RVA: 0x0001FE49 File Offset: 0x0001E049
		// (set) Token: 0x06001DF9 RID: 7673 RVA: 0x0001FE51 File Offset: 0x0001E051
		public ProductUserId LocalUserId { get; private set; }

		// Token: 0x06001DFA RID: 7674 RVA: 0x0001FE5A File Offset: 0x0001E05A
		public Result? GetResultCode()
		{
			return new Result?(this.ResultCode);
		}

		// Token: 0x06001DFB RID: 7675 RVA: 0x0001FE68 File Offset: 0x0001E068
		internal void Set(LinkAccountCallbackInfoInternal? other)
		{
			if (other != null)
			{
				this.ResultCode = other.Value.ResultCode;
				this.ClientData = other.Value.ClientData;
				this.LocalUserId = other.Value.LocalUserId;
			}
		}

		// Token: 0x06001DFC RID: 7676 RVA: 0x0001FEBD File Offset: 0x0001E0BD
		public void Set(object other)
		{
			this.Set(other as LinkAccountCallbackInfoInternal?);
		}
	}
}
