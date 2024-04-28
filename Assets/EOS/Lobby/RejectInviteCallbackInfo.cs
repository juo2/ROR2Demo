using System;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x020003A1 RID: 929
	public class RejectInviteCallbackInfo : ICallbackInfo, ISettable
	{
		// Token: 0x1700069B RID: 1691
		// (get) Token: 0x060016A4 RID: 5796 RVA: 0x00017F74 File Offset: 0x00016174
		// (set) Token: 0x060016A5 RID: 5797 RVA: 0x00017F7C File Offset: 0x0001617C
		public Result ResultCode { get; private set; }

		// Token: 0x1700069C RID: 1692
		// (get) Token: 0x060016A6 RID: 5798 RVA: 0x00017F85 File Offset: 0x00016185
		// (set) Token: 0x060016A7 RID: 5799 RVA: 0x00017F8D File Offset: 0x0001618D
		public object ClientData { get; private set; }

		// Token: 0x1700069D RID: 1693
		// (get) Token: 0x060016A8 RID: 5800 RVA: 0x00017F96 File Offset: 0x00016196
		// (set) Token: 0x060016A9 RID: 5801 RVA: 0x00017F9E File Offset: 0x0001619E
		public string InviteId { get; private set; }

		// Token: 0x060016AA RID: 5802 RVA: 0x00017FA7 File Offset: 0x000161A7
		public Result? GetResultCode()
		{
			return new Result?(this.ResultCode);
		}

		// Token: 0x060016AB RID: 5803 RVA: 0x00017FB4 File Offset: 0x000161B4
		internal void Set(RejectInviteCallbackInfoInternal? other)
		{
			if (other != null)
			{
				this.ResultCode = other.Value.ResultCode;
				this.ClientData = other.Value.ClientData;
				this.InviteId = other.Value.InviteId;
			}
		}

		// Token: 0x060016AC RID: 5804 RVA: 0x00018009 File Offset: 0x00016209
		public void Set(object other)
		{
			this.Set(other as RejectInviteCallbackInfoInternal?);
		}
	}
}
