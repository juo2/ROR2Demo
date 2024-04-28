using System;

namespace Epic.OnlineServices.Reports
{
	// Token: 0x02000160 RID: 352
	public class SendPlayerBehaviorReportCompleteCallbackInfo : ICallbackInfo, ISettable
	{
		// Token: 0x1700023F RID: 575
		// (get) Token: 0x06000999 RID: 2457 RVA: 0x0000AA40 File Offset: 0x00008C40
		// (set) Token: 0x0600099A RID: 2458 RVA: 0x0000AA48 File Offset: 0x00008C48
		public Result ResultCode { get; private set; }

		// Token: 0x17000240 RID: 576
		// (get) Token: 0x0600099B RID: 2459 RVA: 0x0000AA51 File Offset: 0x00008C51
		// (set) Token: 0x0600099C RID: 2460 RVA: 0x0000AA59 File Offset: 0x00008C59
		public object ClientData { get; private set; }

		// Token: 0x0600099D RID: 2461 RVA: 0x0000AA62 File Offset: 0x00008C62
		public Result? GetResultCode()
		{
			return new Result?(this.ResultCode);
		}

		// Token: 0x0600099E RID: 2462 RVA: 0x0000AA70 File Offset: 0x00008C70
		internal void Set(SendPlayerBehaviorReportCompleteCallbackInfoInternal? other)
		{
			if (other != null)
			{
				this.ResultCode = other.Value.ResultCode;
				this.ClientData = other.Value.ClientData;
			}
		}

		// Token: 0x0600099F RID: 2463 RVA: 0x0000AAB0 File Offset: 0x00008CB0
		public void Set(object other)
		{
			this.Set(other as SendPlayerBehaviorReportCompleteCallbackInfoInternal?);
		}
	}
}
