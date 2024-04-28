using System;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x020000FE RID: 254
	public class RejectInviteCallbackInfo : ICallbackInfo, ISettable
	{
		// Token: 0x17000198 RID: 408
		// (get) Token: 0x06000779 RID: 1913 RVA: 0x00008194 File Offset: 0x00006394
		// (set) Token: 0x0600077A RID: 1914 RVA: 0x0000819C File Offset: 0x0000639C
		public Result ResultCode { get; private set; }

		// Token: 0x17000199 RID: 409
		// (get) Token: 0x0600077B RID: 1915 RVA: 0x000081A5 File Offset: 0x000063A5
		// (set) Token: 0x0600077C RID: 1916 RVA: 0x000081AD File Offset: 0x000063AD
		public object ClientData { get; private set; }

		// Token: 0x0600077D RID: 1917 RVA: 0x000081B6 File Offset: 0x000063B6
		public Result? GetResultCode()
		{
			return new Result?(this.ResultCode);
		}

		// Token: 0x0600077E RID: 1918 RVA: 0x000081C4 File Offset: 0x000063C4
		internal void Set(RejectInviteCallbackInfoInternal? other)
		{
			if (other != null)
			{
				this.ResultCode = other.Value.ResultCode;
				this.ClientData = other.Value.ClientData;
			}
		}

		// Token: 0x0600077F RID: 1919 RVA: 0x00008204 File Offset: 0x00006404
		public void Set(object other)
		{
			this.Set(other as RejectInviteCallbackInfoInternal?);
		}
	}
}
