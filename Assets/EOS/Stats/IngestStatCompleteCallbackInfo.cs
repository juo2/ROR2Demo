using System;

namespace Epic.OnlineServices.Stats
{
	// Token: 0x02000096 RID: 150
	public class IngestStatCompleteCallbackInfo : ICallbackInfo, ISettable
	{
		// Token: 0x170000FB RID: 251
		// (get) Token: 0x06000564 RID: 1380 RVA: 0x000065C8 File Offset: 0x000047C8
		// (set) Token: 0x06000565 RID: 1381 RVA: 0x000065D0 File Offset: 0x000047D0
		public Result ResultCode { get; private set; }

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x06000566 RID: 1382 RVA: 0x000065D9 File Offset: 0x000047D9
		// (set) Token: 0x06000567 RID: 1383 RVA: 0x000065E1 File Offset: 0x000047E1
		public object ClientData { get; private set; }

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x06000568 RID: 1384 RVA: 0x000065EA File Offset: 0x000047EA
		// (set) Token: 0x06000569 RID: 1385 RVA: 0x000065F2 File Offset: 0x000047F2
		public ProductUserId LocalUserId { get; private set; }

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x0600056A RID: 1386 RVA: 0x000065FB File Offset: 0x000047FB
		// (set) Token: 0x0600056B RID: 1387 RVA: 0x00006603 File Offset: 0x00004803
		public ProductUserId TargetUserId { get; private set; }

		// Token: 0x0600056C RID: 1388 RVA: 0x0000660C File Offset: 0x0000480C
		public Result? GetResultCode()
		{
			return new Result?(this.ResultCode);
		}

		// Token: 0x0600056D RID: 1389 RVA: 0x0000661C File Offset: 0x0000481C
		internal void Set(IngestStatCompleteCallbackInfoInternal? other)
		{
			if (other != null)
			{
				this.ResultCode = other.Value.ResultCode;
				this.ClientData = other.Value.ClientData;
				this.LocalUserId = other.Value.LocalUserId;
				this.TargetUserId = other.Value.TargetUserId;
			}
		}

		// Token: 0x0600056E RID: 1390 RVA: 0x00006686 File Offset: 0x00004886
		public void Set(object other)
		{
			this.Set(other as IngestStatCompleteCallbackInfoInternal?);
		}
	}
}
