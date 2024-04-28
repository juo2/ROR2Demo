using System;

namespace Epic.OnlineServices.Connect
{
	// Token: 0x020004F5 RID: 1269
	public class QueryExternalAccountMappingsCallbackInfo : ICallbackInfo, ISettable
	{
		// Token: 0x17000940 RID: 2368
		// (get) Token: 0x06001E98 RID: 7832 RVA: 0x00020294 File Offset: 0x0001E494
		// (set) Token: 0x06001E99 RID: 7833 RVA: 0x0002029C File Offset: 0x0001E49C
		public Result ResultCode { get; private set; }

		// Token: 0x17000941 RID: 2369
		// (get) Token: 0x06001E9A RID: 7834 RVA: 0x000202A5 File Offset: 0x0001E4A5
		// (set) Token: 0x06001E9B RID: 7835 RVA: 0x000202AD File Offset: 0x0001E4AD
		public object ClientData { get; private set; }

		// Token: 0x17000942 RID: 2370
		// (get) Token: 0x06001E9C RID: 7836 RVA: 0x000202B6 File Offset: 0x0001E4B6
		// (set) Token: 0x06001E9D RID: 7837 RVA: 0x000202BE File Offset: 0x0001E4BE
		public ProductUserId LocalUserId { get; private set; }

		// Token: 0x06001E9E RID: 7838 RVA: 0x000202C7 File Offset: 0x0001E4C7
		public Result? GetResultCode()
		{
			return new Result?(this.ResultCode);
		}

		// Token: 0x06001E9F RID: 7839 RVA: 0x000202D4 File Offset: 0x0001E4D4
		internal void Set(QueryExternalAccountMappingsCallbackInfoInternal? other)
		{
			if (other != null)
			{
				this.ResultCode = other.Value.ResultCode;
				this.ClientData = other.Value.ClientData;
				this.LocalUserId = other.Value.LocalUserId;
			}
		}

		// Token: 0x06001EA0 RID: 7840 RVA: 0x00020329 File Offset: 0x0001E529
		public void Set(object other)
		{
			this.Set(other as QueryExternalAccountMappingsCallbackInfoInternal?);
		}
	}
}
