using System;

namespace Epic.OnlineServices.KWS
{
	// Token: 0x02000403 RID: 1027
	public class RequestPermissionsCallbackInfo : ICallbackInfo, ISettable
	{
		// Token: 0x1700074B RID: 1867
		// (get) Token: 0x060018C3 RID: 6339 RVA: 0x0001A140 File Offset: 0x00018340
		// (set) Token: 0x060018C4 RID: 6340 RVA: 0x0001A148 File Offset: 0x00018348
		public Result ResultCode { get; private set; }

		// Token: 0x1700074C RID: 1868
		// (get) Token: 0x060018C5 RID: 6341 RVA: 0x0001A151 File Offset: 0x00018351
		// (set) Token: 0x060018C6 RID: 6342 RVA: 0x0001A159 File Offset: 0x00018359
		public object ClientData { get; private set; }

		// Token: 0x1700074D RID: 1869
		// (get) Token: 0x060018C7 RID: 6343 RVA: 0x0001A162 File Offset: 0x00018362
		// (set) Token: 0x060018C8 RID: 6344 RVA: 0x0001A16A File Offset: 0x0001836A
		public ProductUserId LocalUserId { get; private set; }

		// Token: 0x060018C9 RID: 6345 RVA: 0x0001A173 File Offset: 0x00018373
		public Result? GetResultCode()
		{
			return new Result?(this.ResultCode);
		}

		// Token: 0x060018CA RID: 6346 RVA: 0x0001A180 File Offset: 0x00018380
		internal void Set(RequestPermissionsCallbackInfoInternal? other)
		{
			if (other != null)
			{
				this.ResultCode = other.Value.ResultCode;
				this.ClientData = other.Value.ClientData;
				this.LocalUserId = other.Value.LocalUserId;
			}
		}

		// Token: 0x060018CB RID: 6347 RVA: 0x0001A1D5 File Offset: 0x000183D5
		public void Set(object other)
		{
			this.Set(other as RequestPermissionsCallbackInfoInternal?);
		}
	}
}
