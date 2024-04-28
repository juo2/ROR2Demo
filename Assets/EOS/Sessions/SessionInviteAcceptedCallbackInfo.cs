using System;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x02000116 RID: 278
	public class SessionInviteAcceptedCallbackInfo : ICallbackInfo, ISettable
	{
		// Token: 0x170001C8 RID: 456
		// (get) Token: 0x06000805 RID: 2053 RVA: 0x00008B37 File Offset: 0x00006D37
		// (set) Token: 0x06000806 RID: 2054 RVA: 0x00008B3F File Offset: 0x00006D3F
		public object ClientData { get; private set; }

		// Token: 0x170001C9 RID: 457
		// (get) Token: 0x06000807 RID: 2055 RVA: 0x00008B48 File Offset: 0x00006D48
		// (set) Token: 0x06000808 RID: 2056 RVA: 0x00008B50 File Offset: 0x00006D50
		public string SessionId { get; private set; }

		// Token: 0x170001CA RID: 458
		// (get) Token: 0x06000809 RID: 2057 RVA: 0x00008B59 File Offset: 0x00006D59
		// (set) Token: 0x0600080A RID: 2058 RVA: 0x00008B61 File Offset: 0x00006D61
		public ProductUserId LocalUserId { get; private set; }

		// Token: 0x170001CB RID: 459
		// (get) Token: 0x0600080B RID: 2059 RVA: 0x00008B6A File Offset: 0x00006D6A
		// (set) Token: 0x0600080C RID: 2060 RVA: 0x00008B72 File Offset: 0x00006D72
		public ProductUserId TargetUserId { get; private set; }

		// Token: 0x170001CC RID: 460
		// (get) Token: 0x0600080D RID: 2061 RVA: 0x00008B7B File Offset: 0x00006D7B
		// (set) Token: 0x0600080E RID: 2062 RVA: 0x00008B83 File Offset: 0x00006D83
		public string InviteId { get; private set; }

		// Token: 0x0600080F RID: 2063 RVA: 0x00008B8C File Offset: 0x00006D8C
		public Result? GetResultCode()
		{
			return null;
		}

		// Token: 0x06000810 RID: 2064 RVA: 0x00008BA4 File Offset: 0x00006DA4
		internal void Set(SessionInviteAcceptedCallbackInfoInternal? other)
		{
			if (other != null)
			{
				this.ClientData = other.Value.ClientData;
				this.SessionId = other.Value.SessionId;
				this.LocalUserId = other.Value.LocalUserId;
				this.TargetUserId = other.Value.TargetUserId;
				this.InviteId = other.Value.InviteId;
			}
		}

		// Token: 0x06000811 RID: 2065 RVA: 0x00008C23 File Offset: 0x00006E23
		public void Set(object other)
		{
			this.Set(other as SessionInviteAcceptedCallbackInfoInternal?);
		}
	}
}
