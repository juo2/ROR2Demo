using System;

namespace Epic.OnlineServices.Connect
{
	// Token: 0x020004DB RID: 1243
	public class LoginStatusChangedCallbackInfo : ICallbackInfo, ISettable
	{
		// Token: 0x17000937 RID: 2359
		// (get) Token: 0x06001E27 RID: 7719 RVA: 0x00020168 File Offset: 0x0001E368
		// (set) Token: 0x06001E28 RID: 7720 RVA: 0x00020170 File Offset: 0x0001E370
		public object ClientData { get; private set; }

		// Token: 0x17000938 RID: 2360
		// (get) Token: 0x06001E29 RID: 7721 RVA: 0x00020179 File Offset: 0x0001E379
		// (set) Token: 0x06001E2A RID: 7722 RVA: 0x00020181 File Offset: 0x0001E381
		public ProductUserId LocalUserId { get; private set; }

		// Token: 0x17000939 RID: 2361
		// (get) Token: 0x06001E2B RID: 7723 RVA: 0x0002018A File Offset: 0x0001E38A
		// (set) Token: 0x06001E2C RID: 7724 RVA: 0x00020192 File Offset: 0x0001E392
		public LoginStatus PreviousStatus { get; private set; }

		// Token: 0x1700093A RID: 2362
		// (get) Token: 0x06001E2D RID: 7725 RVA: 0x0002019B File Offset: 0x0001E39B
		// (set) Token: 0x06001E2E RID: 7726 RVA: 0x000201A3 File Offset: 0x0001E3A3
		public LoginStatus CurrentStatus { get; private set; }

		// Token: 0x06001E2F RID: 7727 RVA: 0x000201AC File Offset: 0x0001E3AC
		public Result? GetResultCode()
		{
			return null;
		}

		// Token: 0x06001E30 RID: 7728 RVA: 0x000201C4 File Offset: 0x0001E3C4
		internal void Set(LoginStatusChangedCallbackInfoInternal? other)
		{
			if (other != null)
			{
				this.ClientData = other.Value.ClientData;
				this.LocalUserId = other.Value.LocalUserId;
				this.PreviousStatus = other.Value.PreviousStatus;
				this.CurrentStatus = other.Value.CurrentStatus;
			}
		}

		// Token: 0x06001E31 RID: 7729 RVA: 0x0002022E File Offset: 0x0001E42E
		public void Set(object other)
		{
			this.Set(other as LoginStatusChangedCallbackInfoInternal?);
		}
	}
}
