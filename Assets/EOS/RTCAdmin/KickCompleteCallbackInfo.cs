using System;

namespace Epic.OnlineServices.RTCAdmin
{
	// Token: 0x020001B4 RID: 436
	public class KickCompleteCallbackInfo : ICallbackInfo, ISettable
	{
		// Token: 0x170002F6 RID: 758
		// (get) Token: 0x06000B97 RID: 2967 RVA: 0x0000CA8E File Offset: 0x0000AC8E
		// (set) Token: 0x06000B98 RID: 2968 RVA: 0x0000CA96 File Offset: 0x0000AC96
		public Result ResultCode { get; private set; }

		// Token: 0x170002F7 RID: 759
		// (get) Token: 0x06000B99 RID: 2969 RVA: 0x0000CA9F File Offset: 0x0000AC9F
		// (set) Token: 0x06000B9A RID: 2970 RVA: 0x0000CAA7 File Offset: 0x0000ACA7
		public object ClientData { get; private set; }

		// Token: 0x06000B9B RID: 2971 RVA: 0x0000CAB0 File Offset: 0x0000ACB0
		public Result? GetResultCode()
		{
			return new Result?(this.ResultCode);
		}

		// Token: 0x06000B9C RID: 2972 RVA: 0x0000CAC0 File Offset: 0x0000ACC0
		internal void Set(KickCompleteCallbackInfoInternal? other)
		{
			if (other != null)
			{
				this.ResultCode = other.Value.ResultCode;
				this.ClientData = other.Value.ClientData;
			}
		}

		// Token: 0x06000B9D RID: 2973 RVA: 0x0000CB00 File Offset: 0x0000AD00
		public void Set(object other)
		{
			this.Set(other as KickCompleteCallbackInfoInternal?);
		}
	}
}
