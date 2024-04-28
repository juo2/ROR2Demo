using System;

namespace Epic.OnlineServices.KWS
{
	// Token: 0x02000407 RID: 1031
	public class UpdateParentEmailCallbackInfo : ICallbackInfo, ISettable
	{
		// Token: 0x17000756 RID: 1878
		// (get) Token: 0x060018DB RID: 6363 RVA: 0x0001A2C3 File Offset: 0x000184C3
		// (set) Token: 0x060018DC RID: 6364 RVA: 0x0001A2CB File Offset: 0x000184CB
		public Result ResultCode { get; private set; }

		// Token: 0x17000757 RID: 1879
		// (get) Token: 0x060018DD RID: 6365 RVA: 0x0001A2D4 File Offset: 0x000184D4
		// (set) Token: 0x060018DE RID: 6366 RVA: 0x0001A2DC File Offset: 0x000184DC
		public object ClientData { get; private set; }

		// Token: 0x17000758 RID: 1880
		// (get) Token: 0x060018DF RID: 6367 RVA: 0x0001A2E5 File Offset: 0x000184E5
		// (set) Token: 0x060018E0 RID: 6368 RVA: 0x0001A2ED File Offset: 0x000184ED
		public ProductUserId LocalUserId { get; private set; }

		// Token: 0x060018E1 RID: 6369 RVA: 0x0001A2F6 File Offset: 0x000184F6
		public Result? GetResultCode()
		{
			return new Result?(this.ResultCode);
		}

		// Token: 0x060018E2 RID: 6370 RVA: 0x0001A304 File Offset: 0x00018504
		internal void Set(UpdateParentEmailCallbackInfoInternal? other)
		{
			if (other != null)
			{
				this.ResultCode = other.Value.ResultCode;
				this.ClientData = other.Value.ClientData;
				this.LocalUserId = other.Value.LocalUserId;
			}
		}

		// Token: 0x060018E3 RID: 6371 RVA: 0x0001A359 File Offset: 0x00018559
		public void Set(object other)
		{
			this.Set(other as UpdateParentEmailCallbackInfoInternal?);
		}
	}
}
