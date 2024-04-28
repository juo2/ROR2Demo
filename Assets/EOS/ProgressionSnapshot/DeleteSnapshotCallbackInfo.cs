using System;

namespace Epic.OnlineServices.ProgressionSnapshot
{
	// Token: 0x020001F4 RID: 500
	public class DeleteSnapshotCallbackInfo : ICallbackInfo, ISettable
	{
		// Token: 0x17000394 RID: 916
		// (get) Token: 0x06000D3B RID: 3387 RVA: 0x0000E502 File Offset: 0x0000C702
		// (set) Token: 0x06000D3C RID: 3388 RVA: 0x0000E50A File Offset: 0x0000C70A
		public Result ResultCode { get; private set; }

		// Token: 0x17000395 RID: 917
		// (get) Token: 0x06000D3D RID: 3389 RVA: 0x0000E513 File Offset: 0x0000C713
		// (set) Token: 0x06000D3E RID: 3390 RVA: 0x0000E51B File Offset: 0x0000C71B
		public ProductUserId LocalUserId { get; private set; }

		// Token: 0x17000396 RID: 918
		// (get) Token: 0x06000D3F RID: 3391 RVA: 0x0000E524 File Offset: 0x0000C724
		// (set) Token: 0x06000D40 RID: 3392 RVA: 0x0000E52C File Offset: 0x0000C72C
		public object ClientData { get; private set; }

		// Token: 0x06000D41 RID: 3393 RVA: 0x0000E535 File Offset: 0x0000C735
		public Result? GetResultCode()
		{
			return new Result?(this.ResultCode);
		}

		// Token: 0x06000D42 RID: 3394 RVA: 0x0000E544 File Offset: 0x0000C744
		internal void Set(DeleteSnapshotCallbackInfoInternal? other)
		{
			if (other != null)
			{
				this.ResultCode = other.Value.ResultCode;
				this.LocalUserId = other.Value.LocalUserId;
				this.ClientData = other.Value.ClientData;
			}
		}

		// Token: 0x06000D43 RID: 3395 RVA: 0x0000E599 File Offset: 0x0000C799
		public void Set(object other)
		{
			this.Set(other as DeleteSnapshotCallbackInfoInternal?);
		}
	}
}
