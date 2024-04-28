using System;

namespace Epic.OnlineServices.Connect
{
	// Token: 0x020004FD RID: 1277
	public class TransferDeviceIdAccountCallbackInfo : ICallbackInfo, ISettable
	{
		// Token: 0x1700095A RID: 2394
		// (get) Token: 0x06001ECE RID: 7886 RVA: 0x000205E8 File Offset: 0x0001E7E8
		// (set) Token: 0x06001ECF RID: 7887 RVA: 0x000205F0 File Offset: 0x0001E7F0
		public Result ResultCode { get; private set; }

		// Token: 0x1700095B RID: 2395
		// (get) Token: 0x06001ED0 RID: 7888 RVA: 0x000205F9 File Offset: 0x0001E7F9
		// (set) Token: 0x06001ED1 RID: 7889 RVA: 0x00020601 File Offset: 0x0001E801
		public object ClientData { get; private set; }

		// Token: 0x1700095C RID: 2396
		// (get) Token: 0x06001ED2 RID: 7890 RVA: 0x0002060A File Offset: 0x0001E80A
		// (set) Token: 0x06001ED3 RID: 7891 RVA: 0x00020612 File Offset: 0x0001E812
		public ProductUserId LocalUserId { get; private set; }

		// Token: 0x06001ED4 RID: 7892 RVA: 0x0002061B File Offset: 0x0001E81B
		public Result? GetResultCode()
		{
			return new Result?(this.ResultCode);
		}

		// Token: 0x06001ED5 RID: 7893 RVA: 0x00020628 File Offset: 0x0001E828
		internal void Set(TransferDeviceIdAccountCallbackInfoInternal? other)
		{
			if (other != null)
			{
				this.ResultCode = other.Value.ResultCode;
				this.ClientData = other.Value.ClientData;
				this.LocalUserId = other.Value.LocalUserId;
			}
		}

		// Token: 0x06001ED6 RID: 7894 RVA: 0x0002067D File Offset: 0x0001E87D
		public void Set(object other)
		{
			this.Set(other as TransferDeviceIdAccountCallbackInfoInternal?);
		}
	}
}
