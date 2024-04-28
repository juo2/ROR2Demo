using System;

namespace Epic.OnlineServices.Connect
{
	// Token: 0x020004BB RID: 1211
	public class CreateDeviceIdCallbackInfo : ICallbackInfo, ISettable
	{
		// Token: 0x170008EA RID: 2282
		// (get) Token: 0x06001D67 RID: 7527 RVA: 0x0001F51C File Offset: 0x0001D71C
		// (set) Token: 0x06001D68 RID: 7528 RVA: 0x0001F524 File Offset: 0x0001D724
		public Result ResultCode { get; private set; }

		// Token: 0x170008EB RID: 2283
		// (get) Token: 0x06001D69 RID: 7529 RVA: 0x0001F52D File Offset: 0x0001D72D
		// (set) Token: 0x06001D6A RID: 7530 RVA: 0x0001F535 File Offset: 0x0001D735
		public object ClientData { get; private set; }

		// Token: 0x06001D6B RID: 7531 RVA: 0x0001F53E File Offset: 0x0001D73E
		public Result? GetResultCode()
		{
			return new Result?(this.ResultCode);
		}

		// Token: 0x06001D6C RID: 7532 RVA: 0x0001F54C File Offset: 0x0001D74C
		internal void Set(CreateDeviceIdCallbackInfoInternal? other)
		{
			if (other != null)
			{
				this.ResultCode = other.Value.ResultCode;
				this.ClientData = other.Value.ClientData;
			}
		}

		// Token: 0x06001D6D RID: 7533 RVA: 0x0001F58C File Offset: 0x0001D78C
		public void Set(object other)
		{
			this.Set(other as CreateDeviceIdCallbackInfoInternal?);
		}
	}
}
