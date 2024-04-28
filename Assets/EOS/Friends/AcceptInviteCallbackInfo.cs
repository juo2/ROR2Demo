using System;

namespace Epic.OnlineServices.Friends
{
	// Token: 0x0200040B RID: 1035
	public class AcceptInviteCallbackInfo : ICallbackInfo, ISettable
	{
		// Token: 0x17000761 RID: 1889
		// (get) Token: 0x060018F3 RID: 6387 RVA: 0x0001A440 File Offset: 0x00018640
		// (set) Token: 0x060018F4 RID: 6388 RVA: 0x0001A448 File Offset: 0x00018648
		public Result ResultCode { get; private set; }

		// Token: 0x17000762 RID: 1890
		// (get) Token: 0x060018F5 RID: 6389 RVA: 0x0001A451 File Offset: 0x00018651
		// (set) Token: 0x060018F6 RID: 6390 RVA: 0x0001A459 File Offset: 0x00018659
		public object ClientData { get; private set; }

		// Token: 0x17000763 RID: 1891
		// (get) Token: 0x060018F7 RID: 6391 RVA: 0x0001A462 File Offset: 0x00018662
		// (set) Token: 0x060018F8 RID: 6392 RVA: 0x0001A46A File Offset: 0x0001866A
		public EpicAccountId LocalUserId { get; private set; }

		// Token: 0x17000764 RID: 1892
		// (get) Token: 0x060018F9 RID: 6393 RVA: 0x0001A473 File Offset: 0x00018673
		// (set) Token: 0x060018FA RID: 6394 RVA: 0x0001A47B File Offset: 0x0001867B
		public EpicAccountId TargetUserId { get; private set; }

		// Token: 0x060018FB RID: 6395 RVA: 0x0001A484 File Offset: 0x00018684
		public Result? GetResultCode()
		{
			return new Result?(this.ResultCode);
		}

		// Token: 0x060018FC RID: 6396 RVA: 0x0001A494 File Offset: 0x00018694
		internal void Set(AcceptInviteCallbackInfoInternal? other)
		{
			if (other != null)
			{
				this.ResultCode = other.Value.ResultCode;
				this.ClientData = other.Value.ClientData;
				this.LocalUserId = other.Value.LocalUserId;
				this.TargetUserId = other.Value.TargetUserId;
			}
		}

		// Token: 0x060018FD RID: 6397 RVA: 0x0001A4FE File Offset: 0x000186FE
		public void Set(object other)
		{
			this.Set(other as AcceptInviteCallbackInfoInternal?);
		}
	}
}
