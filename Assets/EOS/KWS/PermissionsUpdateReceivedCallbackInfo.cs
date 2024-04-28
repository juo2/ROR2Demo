using System;

namespace Epic.OnlineServices.KWS
{
	// Token: 0x020003F9 RID: 1017
	public class PermissionsUpdateReceivedCallbackInfo : ICallbackInfo, ISettable
	{
		// Token: 0x1700072E RID: 1838
		// (get) Token: 0x06001885 RID: 6277 RVA: 0x00019D24 File Offset: 0x00017F24
		// (set) Token: 0x06001886 RID: 6278 RVA: 0x00019D2C File Offset: 0x00017F2C
		public object ClientData { get; private set; }

		// Token: 0x1700072F RID: 1839
		// (get) Token: 0x06001887 RID: 6279 RVA: 0x00019D35 File Offset: 0x00017F35
		// (set) Token: 0x06001888 RID: 6280 RVA: 0x00019D3D File Offset: 0x00017F3D
		public ProductUserId LocalUserId { get; private set; }

		// Token: 0x06001889 RID: 6281 RVA: 0x00019D48 File Offset: 0x00017F48
		public Result? GetResultCode()
		{
			return null;
		}

		// Token: 0x0600188A RID: 6282 RVA: 0x00019D60 File Offset: 0x00017F60
		internal void Set(PermissionsUpdateReceivedCallbackInfoInternal? other)
		{
			if (other != null)
			{
				this.ClientData = other.Value.ClientData;
				this.LocalUserId = other.Value.LocalUserId;
			}
		}

		// Token: 0x0600188B RID: 6283 RVA: 0x00019DA0 File Offset: 0x00017FA0
		public void Set(object other)
		{
			this.Set(other as PermissionsUpdateReceivedCallbackInfoInternal?);
		}
	}
}
