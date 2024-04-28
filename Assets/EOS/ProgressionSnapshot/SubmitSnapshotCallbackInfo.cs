using System;

namespace Epic.OnlineServices.ProgressionSnapshot
{
	// Token: 0x020001FF RID: 511
	public class SubmitSnapshotCallbackInfo : ICallbackInfo, ISettable
	{
		// Token: 0x1700039F RID: 927
		// (get) Token: 0x06000D70 RID: 3440 RVA: 0x0000E808 File Offset: 0x0000CA08
		// (set) Token: 0x06000D71 RID: 3441 RVA: 0x0000E810 File Offset: 0x0000CA10
		public Result ResultCode { get; private set; }

		// Token: 0x170003A0 RID: 928
		// (get) Token: 0x06000D72 RID: 3442 RVA: 0x0000E819 File Offset: 0x0000CA19
		// (set) Token: 0x06000D73 RID: 3443 RVA: 0x0000E821 File Offset: 0x0000CA21
		public uint SnapshotId { get; private set; }

		// Token: 0x170003A1 RID: 929
		// (get) Token: 0x06000D74 RID: 3444 RVA: 0x0000E82A File Offset: 0x0000CA2A
		// (set) Token: 0x06000D75 RID: 3445 RVA: 0x0000E832 File Offset: 0x0000CA32
		public object ClientData { get; private set; }

		// Token: 0x06000D76 RID: 3446 RVA: 0x0000E83B File Offset: 0x0000CA3B
		public Result? GetResultCode()
		{
			return new Result?(this.ResultCode);
		}

		// Token: 0x06000D77 RID: 3447 RVA: 0x0000E848 File Offset: 0x0000CA48
		internal void Set(SubmitSnapshotCallbackInfoInternal? other)
		{
			if (other != null)
			{
				this.ResultCode = other.Value.ResultCode;
				this.SnapshotId = other.Value.SnapshotId;
				this.ClientData = other.Value.ClientData;
			}
		}

		// Token: 0x06000D78 RID: 3448 RVA: 0x0000E89D File Offset: 0x0000CA9D
		public void Set(object other)
		{
			this.Set(other as SubmitSnapshotCallbackInfoInternal?);
		}
	}
}
