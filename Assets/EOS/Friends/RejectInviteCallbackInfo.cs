using System;

namespace Epic.OnlineServices.Friends
{
	// Token: 0x02000429 RID: 1065
	public class RejectInviteCallbackInfo : ICallbackInfo, ISettable
	{
		// Token: 0x1700078C RID: 1932
		// (get) Token: 0x0600198E RID: 6542 RVA: 0x0001AD30 File Offset: 0x00018F30
		// (set) Token: 0x0600198F RID: 6543 RVA: 0x0001AD38 File Offset: 0x00018F38
		public Result ResultCode { get; private set; }

		// Token: 0x1700078D RID: 1933
		// (get) Token: 0x06001990 RID: 6544 RVA: 0x0001AD41 File Offset: 0x00018F41
		// (set) Token: 0x06001991 RID: 6545 RVA: 0x0001AD49 File Offset: 0x00018F49
		public object ClientData { get; private set; }

		// Token: 0x1700078E RID: 1934
		// (get) Token: 0x06001992 RID: 6546 RVA: 0x0001AD52 File Offset: 0x00018F52
		// (set) Token: 0x06001993 RID: 6547 RVA: 0x0001AD5A File Offset: 0x00018F5A
		public EpicAccountId LocalUserId { get; private set; }

		// Token: 0x1700078F RID: 1935
		// (get) Token: 0x06001994 RID: 6548 RVA: 0x0001AD63 File Offset: 0x00018F63
		// (set) Token: 0x06001995 RID: 6549 RVA: 0x0001AD6B File Offset: 0x00018F6B
		public EpicAccountId TargetUserId { get; private set; }

		// Token: 0x06001996 RID: 6550 RVA: 0x0001AD74 File Offset: 0x00018F74
		public Result? GetResultCode()
		{
			return new Result?(this.ResultCode);
		}

		// Token: 0x06001997 RID: 6551 RVA: 0x0001AD84 File Offset: 0x00018F84
		internal void Set(RejectInviteCallbackInfoInternal? other)
		{
			if (other != null)
			{
				this.ResultCode = other.Value.ResultCode;
				this.ClientData = other.Value.ClientData;
				this.LocalUserId = other.Value.LocalUserId;
				this.TargetUserId = other.Value.TargetUserId;
			}
		}

		// Token: 0x06001998 RID: 6552 RVA: 0x0001ADEE File Offset: 0x00018FEE
		public void Set(object other)
		{
			this.Set(other as RejectInviteCallbackInfoInternal?);
		}
	}
}
