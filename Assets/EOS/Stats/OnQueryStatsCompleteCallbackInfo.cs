using System;

namespace Epic.OnlineServices.Stats
{
	// Token: 0x0200009E RID: 158
	public class OnQueryStatsCompleteCallbackInfo : ICallbackInfo, ISettable
	{
		// Token: 0x1700010A RID: 266
		// (get) Token: 0x06000592 RID: 1426 RVA: 0x000067CA File Offset: 0x000049CA
		// (set) Token: 0x06000593 RID: 1427 RVA: 0x000067D2 File Offset: 0x000049D2
		public Result ResultCode { get; private set; }

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x06000594 RID: 1428 RVA: 0x000067DB File Offset: 0x000049DB
		// (set) Token: 0x06000595 RID: 1429 RVA: 0x000067E3 File Offset: 0x000049E3
		public object ClientData { get; private set; }

		// Token: 0x1700010C RID: 268
		// (get) Token: 0x06000596 RID: 1430 RVA: 0x000067EC File Offset: 0x000049EC
		// (set) Token: 0x06000597 RID: 1431 RVA: 0x000067F4 File Offset: 0x000049F4
		public ProductUserId LocalUserId { get; private set; }

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x06000598 RID: 1432 RVA: 0x000067FD File Offset: 0x000049FD
		// (set) Token: 0x06000599 RID: 1433 RVA: 0x00006805 File Offset: 0x00004A05
		public ProductUserId TargetUserId { get; private set; }

		// Token: 0x0600059A RID: 1434 RVA: 0x0000680E File Offset: 0x00004A0E
		public Result? GetResultCode()
		{
			return new Result?(this.ResultCode);
		}

		// Token: 0x0600059B RID: 1435 RVA: 0x0000681C File Offset: 0x00004A1C
		internal void Set(OnQueryStatsCompleteCallbackInfoInternal? other)
		{
			if (other != null)
			{
				this.ResultCode = other.Value.ResultCode;
				this.ClientData = other.Value.ClientData;
				this.LocalUserId = other.Value.LocalUserId;
				this.TargetUserId = other.Value.TargetUserId;
			}
		}

		// Token: 0x0600059C RID: 1436 RVA: 0x00006886 File Offset: 0x00004A86
		public void Set(object other)
		{
			this.Set(other as OnQueryStatsCompleteCallbackInfoInternal?);
		}
	}
}
