using System;

namespace Epic.OnlineServices.Ecom
{
	// Token: 0x0200047E RID: 1150
	public class QueryOffersCallbackInfo : ICallbackInfo, ISettable
	{
		// Token: 0x17000870 RID: 2160
		// (get) Token: 0x06001BFB RID: 7163 RVA: 0x0001D8E6 File Offset: 0x0001BAE6
		// (set) Token: 0x06001BFC RID: 7164 RVA: 0x0001D8EE File Offset: 0x0001BAEE
		public Result ResultCode { get; private set; }

		// Token: 0x17000871 RID: 2161
		// (get) Token: 0x06001BFD RID: 7165 RVA: 0x0001D8F7 File Offset: 0x0001BAF7
		// (set) Token: 0x06001BFE RID: 7166 RVA: 0x0001D8FF File Offset: 0x0001BAFF
		public object ClientData { get; private set; }

		// Token: 0x17000872 RID: 2162
		// (get) Token: 0x06001BFF RID: 7167 RVA: 0x0001D908 File Offset: 0x0001BB08
		// (set) Token: 0x06001C00 RID: 7168 RVA: 0x0001D910 File Offset: 0x0001BB10
		public EpicAccountId LocalUserId { get; private set; }

		// Token: 0x06001C01 RID: 7169 RVA: 0x0001D919 File Offset: 0x0001BB19
		public Result? GetResultCode()
		{
			return new Result?(this.ResultCode);
		}

		// Token: 0x06001C02 RID: 7170 RVA: 0x0001D928 File Offset: 0x0001BB28
		internal void Set(QueryOffersCallbackInfoInternal? other)
		{
			if (other != null)
			{
				this.ResultCode = other.Value.ResultCode;
				this.ClientData = other.Value.ClientData;
				this.LocalUserId = other.Value.LocalUserId;
			}
		}

		// Token: 0x06001C03 RID: 7171 RVA: 0x0001D97D File Offset: 0x0001BB7D
		public void Set(object other)
		{
			this.Set(other as QueryOffersCallbackInfoInternal?);
		}
	}
}
