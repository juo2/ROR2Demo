using System;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x0200039B RID: 923
	public class QueryInvitesCallbackInfo : ICallbackInfo, ISettable
	{
		// Token: 0x17000687 RID: 1671
		// (get) Token: 0x0600167B RID: 5755 RVA: 0x00017CAC File Offset: 0x00015EAC
		// (set) Token: 0x0600167C RID: 5756 RVA: 0x00017CB4 File Offset: 0x00015EB4
		public Result ResultCode { get; private set; }

		// Token: 0x17000688 RID: 1672
		// (get) Token: 0x0600167D RID: 5757 RVA: 0x00017CBD File Offset: 0x00015EBD
		// (set) Token: 0x0600167E RID: 5758 RVA: 0x00017CC5 File Offset: 0x00015EC5
		public object ClientData { get; private set; }

		// Token: 0x17000689 RID: 1673
		// (get) Token: 0x0600167F RID: 5759 RVA: 0x00017CCE File Offset: 0x00015ECE
		// (set) Token: 0x06001680 RID: 5760 RVA: 0x00017CD6 File Offset: 0x00015ED6
		public ProductUserId LocalUserId { get; private set; }

		// Token: 0x06001681 RID: 5761 RVA: 0x00017CDF File Offset: 0x00015EDF
		public Result? GetResultCode()
		{
			return new Result?(this.ResultCode);
		}

		// Token: 0x06001682 RID: 5762 RVA: 0x00017CEC File Offset: 0x00015EEC
		internal void Set(QueryInvitesCallbackInfoInternal? other)
		{
			if (other != null)
			{
				this.ResultCode = other.Value.ResultCode;
				this.ClientData = other.Value.ClientData;
				this.LocalUserId = other.Value.LocalUserId;
			}
		}

		// Token: 0x06001683 RID: 5763 RVA: 0x00017D41 File Offset: 0x00015F41
		public void Set(object other)
		{
			this.Set(other as QueryInvitesCallbackInfoInternal?);
		}
	}
}
