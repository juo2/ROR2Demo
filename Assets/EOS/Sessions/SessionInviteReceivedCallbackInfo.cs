using System;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x02000118 RID: 280
	public class SessionInviteReceivedCallbackInfo : ICallbackInfo, ISettable
	{
		// Token: 0x170001D3 RID: 467
		// (get) Token: 0x06000819 RID: 2073 RVA: 0x00008CCC File Offset: 0x00006ECC
		// (set) Token: 0x0600081A RID: 2074 RVA: 0x00008CD4 File Offset: 0x00006ED4
		public object ClientData { get; private set; }

		// Token: 0x170001D4 RID: 468
		// (get) Token: 0x0600081B RID: 2075 RVA: 0x00008CDD File Offset: 0x00006EDD
		// (set) Token: 0x0600081C RID: 2076 RVA: 0x00008CE5 File Offset: 0x00006EE5
		public ProductUserId LocalUserId { get; private set; }

		// Token: 0x170001D5 RID: 469
		// (get) Token: 0x0600081D RID: 2077 RVA: 0x00008CEE File Offset: 0x00006EEE
		// (set) Token: 0x0600081E RID: 2078 RVA: 0x00008CF6 File Offset: 0x00006EF6
		public ProductUserId TargetUserId { get; private set; }

		// Token: 0x170001D6 RID: 470
		// (get) Token: 0x0600081F RID: 2079 RVA: 0x00008CFF File Offset: 0x00006EFF
		// (set) Token: 0x06000820 RID: 2080 RVA: 0x00008D07 File Offset: 0x00006F07
		public string InviteId { get; private set; }

		// Token: 0x06000821 RID: 2081 RVA: 0x00008D10 File Offset: 0x00006F10
		public Result? GetResultCode()
		{
			return null;
		}

		// Token: 0x06000822 RID: 2082 RVA: 0x00008D28 File Offset: 0x00006F28
		internal void Set(SessionInviteReceivedCallbackInfoInternal? other)
		{
			if (other != null)
			{
				this.ClientData = other.Value.ClientData;
				this.LocalUserId = other.Value.LocalUserId;
				this.TargetUserId = other.Value.TargetUserId;
				this.InviteId = other.Value.InviteId;
			}
		}

		// Token: 0x06000823 RID: 2083 RVA: 0x00008D92 File Offset: 0x00006F92
		public void Set(object other)
		{
			this.Set(other as SessionInviteReceivedCallbackInfoInternal?);
		}
	}
}
