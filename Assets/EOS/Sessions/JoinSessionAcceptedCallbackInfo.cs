using System;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x020000D4 RID: 212
	public class JoinSessionAcceptedCallbackInfo : ICallbackInfo, ISettable
	{
		// Token: 0x1700016E RID: 366
		// (get) Token: 0x060006B8 RID: 1720 RVA: 0x00007BDA File Offset: 0x00005DDA
		// (set) Token: 0x060006B9 RID: 1721 RVA: 0x00007BE2 File Offset: 0x00005DE2
		public object ClientData { get; private set; }

		// Token: 0x1700016F RID: 367
		// (get) Token: 0x060006BA RID: 1722 RVA: 0x00007BEB File Offset: 0x00005DEB
		// (set) Token: 0x060006BB RID: 1723 RVA: 0x00007BF3 File Offset: 0x00005DF3
		public ProductUserId LocalUserId { get; private set; }

		// Token: 0x17000170 RID: 368
		// (get) Token: 0x060006BC RID: 1724 RVA: 0x00007BFC File Offset: 0x00005DFC
		// (set) Token: 0x060006BD RID: 1725 RVA: 0x00007C04 File Offset: 0x00005E04
		public ulong UiEventId { get; private set; }

		// Token: 0x060006BE RID: 1726 RVA: 0x00007C10 File Offset: 0x00005E10
		public Result? GetResultCode()
		{
			return null;
		}

		// Token: 0x060006BF RID: 1727 RVA: 0x00007C28 File Offset: 0x00005E28
		internal void Set(JoinSessionAcceptedCallbackInfoInternal? other)
		{
			if (other != null)
			{
				this.ClientData = other.Value.ClientData;
				this.LocalUserId = other.Value.LocalUserId;
				this.UiEventId = other.Value.UiEventId;
			}
		}

		// Token: 0x060006C0 RID: 1728 RVA: 0x00007C7D File Offset: 0x00005E7D
		public void Set(object other)
		{
			this.Set(other as JoinSessionAcceptedCallbackInfoInternal?);
		}
	}
}
