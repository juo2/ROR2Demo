using System;

namespace Epic.OnlineServices.Ecom
{
	// Token: 0x0200048A RID: 1162
	public class RedeemEntitlementsCallbackInfo : ICallbackInfo, ISettable
	{
		// Token: 0x17000899 RID: 2201
		// (get) Token: 0x06001C4F RID: 7247 RVA: 0x0001DE6E File Offset: 0x0001C06E
		// (set) Token: 0x06001C50 RID: 7248 RVA: 0x0001DE76 File Offset: 0x0001C076
		public Result ResultCode { get; private set; }

		// Token: 0x1700089A RID: 2202
		// (get) Token: 0x06001C51 RID: 7249 RVA: 0x0001DE7F File Offset: 0x0001C07F
		// (set) Token: 0x06001C52 RID: 7250 RVA: 0x0001DE87 File Offset: 0x0001C087
		public object ClientData { get; private set; }

		// Token: 0x1700089B RID: 2203
		// (get) Token: 0x06001C53 RID: 7251 RVA: 0x0001DE90 File Offset: 0x0001C090
		// (set) Token: 0x06001C54 RID: 7252 RVA: 0x0001DE98 File Offset: 0x0001C098
		public EpicAccountId LocalUserId { get; private set; }

		// Token: 0x06001C55 RID: 7253 RVA: 0x0001DEA1 File Offset: 0x0001C0A1
		public Result? GetResultCode()
		{
			return new Result?(this.ResultCode);
		}

		// Token: 0x06001C56 RID: 7254 RVA: 0x0001DEB0 File Offset: 0x0001C0B0
		internal void Set(RedeemEntitlementsCallbackInfoInternal? other)
		{
			if (other != null)
			{
				this.ResultCode = other.Value.ResultCode;
				this.ClientData = other.Value.ClientData;
				this.LocalUserId = other.Value.LocalUserId;
			}
		}

		// Token: 0x06001C57 RID: 7255 RVA: 0x0001DF05 File Offset: 0x0001C105
		public void Set(object other)
		{
			this.Set(other as RedeemEntitlementsCallbackInfoInternal?);
		}
	}
}
