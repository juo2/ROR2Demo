using System;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x020000F6 RID: 246
	public class QueryInvitesCallbackInfo : ICallbackInfo, ISettable
	{
		// Token: 0x17000182 RID: 386
		// (get) Token: 0x06000749 RID: 1865 RVA: 0x00007E78 File Offset: 0x00006078
		// (set) Token: 0x0600074A RID: 1866 RVA: 0x00007E80 File Offset: 0x00006080
		public Result ResultCode { get; private set; }

		// Token: 0x17000183 RID: 387
		// (get) Token: 0x0600074B RID: 1867 RVA: 0x00007E89 File Offset: 0x00006089
		// (set) Token: 0x0600074C RID: 1868 RVA: 0x00007E91 File Offset: 0x00006091
		public object ClientData { get; private set; }

		// Token: 0x17000184 RID: 388
		// (get) Token: 0x0600074D RID: 1869 RVA: 0x00007E9A File Offset: 0x0000609A
		// (set) Token: 0x0600074E RID: 1870 RVA: 0x00007EA2 File Offset: 0x000060A2
		public ProductUserId LocalUserId { get; private set; }

		// Token: 0x0600074F RID: 1871 RVA: 0x00007EAB File Offset: 0x000060AB
		public Result? GetResultCode()
		{
			return new Result?(this.ResultCode);
		}

		// Token: 0x06000750 RID: 1872 RVA: 0x00007EB8 File Offset: 0x000060B8
		internal void Set(QueryInvitesCallbackInfoInternal? other)
		{
			if (other != null)
			{
				this.ResultCode = other.Value.ResultCode;
				this.ClientData = other.Value.ClientData;
				this.LocalUserId = other.Value.LocalUserId;
			}
		}

		// Token: 0x06000751 RID: 1873 RVA: 0x00007F0D File Offset: 0x0000610D
		public void Set(object other)
		{
			this.Set(other as QueryInvitesCallbackInfoInternal?);
		}
	}
}
