using System;

namespace Epic.OnlineServices.PlayerDataStorage
{
	// Token: 0x02000262 RID: 610
	public class QueryFileCallbackInfo : ICallbackInfo, ISettable
	{
		// Token: 0x17000440 RID: 1088
		// (get) Token: 0x06000F9D RID: 3997 RVA: 0x00010AED File Offset: 0x0000ECED
		// (set) Token: 0x06000F9E RID: 3998 RVA: 0x00010AF5 File Offset: 0x0000ECF5
		public Result ResultCode { get; private set; }

		// Token: 0x17000441 RID: 1089
		// (get) Token: 0x06000F9F RID: 3999 RVA: 0x00010AFE File Offset: 0x0000ECFE
		// (set) Token: 0x06000FA0 RID: 4000 RVA: 0x00010B06 File Offset: 0x0000ED06
		public object ClientData { get; private set; }

		// Token: 0x17000442 RID: 1090
		// (get) Token: 0x06000FA1 RID: 4001 RVA: 0x00010B0F File Offset: 0x0000ED0F
		// (set) Token: 0x06000FA2 RID: 4002 RVA: 0x00010B17 File Offset: 0x0000ED17
		public ProductUserId LocalUserId { get; private set; }

		// Token: 0x06000FA3 RID: 4003 RVA: 0x00010B20 File Offset: 0x0000ED20
		public Result? GetResultCode()
		{
			return new Result?(this.ResultCode);
		}

		// Token: 0x06000FA4 RID: 4004 RVA: 0x00010B30 File Offset: 0x0000ED30
		internal void Set(QueryFileCallbackInfoInternal? other)
		{
			if (other != null)
			{
				this.ResultCode = other.Value.ResultCode;
				this.ClientData = other.Value.ClientData;
				this.LocalUserId = other.Value.LocalUserId;
			}
		}

		// Token: 0x06000FA5 RID: 4005 RVA: 0x00010B85 File Offset: 0x0000ED85
		public void Set(object other)
		{
			this.Set(other as QueryFileCallbackInfoInternal?);
		}
	}
}
