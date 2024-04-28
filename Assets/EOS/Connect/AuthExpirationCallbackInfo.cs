using System;

namespace Epic.OnlineServices.Connect
{
	// Token: 0x020004AE RID: 1198
	public class AuthExpirationCallbackInfo : ICallbackInfo, ISettable
	{
		// Token: 0x170008D5 RID: 2261
		// (get) Token: 0x06001D09 RID: 7433 RVA: 0x0001E9EC File Offset: 0x0001CBEC
		// (set) Token: 0x06001D0A RID: 7434 RVA: 0x0001E9F4 File Offset: 0x0001CBF4
		public object ClientData { get; private set; }

		// Token: 0x170008D6 RID: 2262
		// (get) Token: 0x06001D0B RID: 7435 RVA: 0x0001E9FD File Offset: 0x0001CBFD
		// (set) Token: 0x06001D0C RID: 7436 RVA: 0x0001EA05 File Offset: 0x0001CC05
		public ProductUserId LocalUserId { get; private set; }

		// Token: 0x06001D0D RID: 7437 RVA: 0x0001EA10 File Offset: 0x0001CC10
		public Result? GetResultCode()
		{
			return null;
		}

		// Token: 0x06001D0E RID: 7438 RVA: 0x0001EA28 File Offset: 0x0001CC28
		internal void Set(AuthExpirationCallbackInfoInternal? other)
		{
			if (other != null)
			{
				this.ClientData = other.Value.ClientData;
				this.LocalUserId = other.Value.LocalUserId;
			}
		}

		// Token: 0x06001D0F RID: 7439 RVA: 0x0001EA68 File Offset: 0x0001CC68
		public void Set(object other)
		{
			this.Set(other as AuthExpirationCallbackInfoInternal?);
		}
	}
}
