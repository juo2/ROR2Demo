using System;

namespace Epic.OnlineServices.Friends
{
	// Token: 0x0200042D RID: 1069
	public class SendInviteCallbackInfo : ICallbackInfo, ISettable
	{
		// Token: 0x17000799 RID: 1945
		// (get) Token: 0x060019A9 RID: 6569 RVA: 0x0001AEF4 File Offset: 0x000190F4
		// (set) Token: 0x060019AA RID: 6570 RVA: 0x0001AEFC File Offset: 0x000190FC
		public Result ResultCode { get; private set; }

		// Token: 0x1700079A RID: 1946
		// (get) Token: 0x060019AB RID: 6571 RVA: 0x0001AF05 File Offset: 0x00019105
		// (set) Token: 0x060019AC RID: 6572 RVA: 0x0001AF0D File Offset: 0x0001910D
		public object ClientData { get; private set; }

		// Token: 0x1700079B RID: 1947
		// (get) Token: 0x060019AD RID: 6573 RVA: 0x0001AF16 File Offset: 0x00019116
		// (set) Token: 0x060019AE RID: 6574 RVA: 0x0001AF1E File Offset: 0x0001911E
		public EpicAccountId LocalUserId { get; private set; }

		// Token: 0x1700079C RID: 1948
		// (get) Token: 0x060019AF RID: 6575 RVA: 0x0001AF27 File Offset: 0x00019127
		// (set) Token: 0x060019B0 RID: 6576 RVA: 0x0001AF2F File Offset: 0x0001912F
		public EpicAccountId TargetUserId { get; private set; }

		// Token: 0x060019B1 RID: 6577 RVA: 0x0001AF38 File Offset: 0x00019138
		public Result? GetResultCode()
		{
			return new Result?(this.ResultCode);
		}

		// Token: 0x060019B2 RID: 6578 RVA: 0x0001AF48 File Offset: 0x00019148
		internal void Set(SendInviteCallbackInfoInternal? other)
		{
			if (other != null)
			{
				this.ResultCode = other.Value.ResultCode;
				this.ClientData = other.Value.ClientData;
				this.LocalUserId = other.Value.LocalUserId;
				this.TargetUserId = other.Value.TargetUserId;
			}
		}

		// Token: 0x060019B3 RID: 6579 RVA: 0x0001AFB2 File Offset: 0x000191B2
		public void Set(object other)
		{
			this.Set(other as SendInviteCallbackInfoInternal?);
		}
	}
}
