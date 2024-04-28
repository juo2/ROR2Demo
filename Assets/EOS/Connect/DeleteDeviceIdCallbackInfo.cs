using System;

namespace Epic.OnlineServices.Connect
{
	// Token: 0x020004C5 RID: 1221
	public class DeleteDeviceIdCallbackInfo : ICallbackInfo, ISettable
	{
		// Token: 0x170008FE RID: 2302
		// (get) Token: 0x06001D9C RID: 7580 RVA: 0x0001F858 File Offset: 0x0001DA58
		// (set) Token: 0x06001D9D RID: 7581 RVA: 0x0001F860 File Offset: 0x0001DA60
		public Result ResultCode { get; private set; }

		// Token: 0x170008FF RID: 2303
		// (get) Token: 0x06001D9E RID: 7582 RVA: 0x0001F869 File Offset: 0x0001DA69
		// (set) Token: 0x06001D9F RID: 7583 RVA: 0x0001F871 File Offset: 0x0001DA71
		public object ClientData { get; private set; }

		// Token: 0x06001DA0 RID: 7584 RVA: 0x0001F87A File Offset: 0x0001DA7A
		public Result? GetResultCode()
		{
			return new Result?(this.ResultCode);
		}

		// Token: 0x06001DA1 RID: 7585 RVA: 0x0001F888 File Offset: 0x0001DA88
		internal void Set(DeleteDeviceIdCallbackInfoInternal? other)
		{
			if (other != null)
			{
				this.ResultCode = other.Value.ResultCode;
				this.ClientData = other.Value.ClientData;
			}
		}

		// Token: 0x06001DA2 RID: 7586 RVA: 0x0001F8C8 File Offset: 0x0001DAC8
		public void Set(object other)
		{
			this.Set(other as DeleteDeviceIdCallbackInfoInternal?);
		}
	}
}
