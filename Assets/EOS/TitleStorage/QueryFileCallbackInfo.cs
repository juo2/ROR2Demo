using System;

namespace Epic.OnlineServices.TitleStorage
{
	// Token: 0x0200007D RID: 125
	public class QueryFileCallbackInfo : ICallbackInfo, ISettable
	{
		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x060004B8 RID: 1208 RVA: 0x00005858 File Offset: 0x00003A58
		// (set) Token: 0x060004B9 RID: 1209 RVA: 0x00005860 File Offset: 0x00003A60
		public Result ResultCode { get; private set; }

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x060004BA RID: 1210 RVA: 0x00005869 File Offset: 0x00003A69
		// (set) Token: 0x060004BB RID: 1211 RVA: 0x00005871 File Offset: 0x00003A71
		public object ClientData { get; private set; }

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x060004BC RID: 1212 RVA: 0x0000587A File Offset: 0x00003A7A
		// (set) Token: 0x060004BD RID: 1213 RVA: 0x00005882 File Offset: 0x00003A82
		public ProductUserId LocalUserId { get; private set; }

		// Token: 0x060004BE RID: 1214 RVA: 0x0000588B File Offset: 0x00003A8B
		public Result? GetResultCode()
		{
			return new Result?(this.ResultCode);
		}

		// Token: 0x060004BF RID: 1215 RVA: 0x00005898 File Offset: 0x00003A98
		internal void Set(QueryFileCallbackInfoInternal? other)
		{
			if (other != null)
			{
				this.ResultCode = other.Value.ResultCode;
				this.ClientData = other.Value.ClientData;
				this.LocalUserId = other.Value.LocalUserId;
			}
		}

		// Token: 0x060004C0 RID: 1216 RVA: 0x000058ED File Offset: 0x00003AED
		public void Set(object other)
		{
			this.Set(other as QueryFileCallbackInfoInternal?);
		}
	}
}
