using System;

namespace Epic.OnlineServices.Leaderboards
{
	// Token: 0x020003CF RID: 975
	public class OnQueryLeaderboardRanksCompleteCallbackInfo : ICallbackInfo, ISettable
	{
		// Token: 0x170006ED RID: 1773
		// (get) Token: 0x0600179F RID: 6047 RVA: 0x00019034 File Offset: 0x00017234
		// (set) Token: 0x060017A0 RID: 6048 RVA: 0x0001903C File Offset: 0x0001723C
		public Result ResultCode { get; private set; }

		// Token: 0x170006EE RID: 1774
		// (get) Token: 0x060017A1 RID: 6049 RVA: 0x00019045 File Offset: 0x00017245
		// (set) Token: 0x060017A2 RID: 6050 RVA: 0x0001904D File Offset: 0x0001724D
		public object ClientData { get; private set; }

		// Token: 0x060017A3 RID: 6051 RVA: 0x00019056 File Offset: 0x00017256
		public Result? GetResultCode()
		{
			return new Result?(this.ResultCode);
		}

		// Token: 0x060017A4 RID: 6052 RVA: 0x00019064 File Offset: 0x00017264
		internal void Set(OnQueryLeaderboardRanksCompleteCallbackInfoInternal? other)
		{
			if (other != null)
			{
				this.ResultCode = other.Value.ResultCode;
				this.ClientData = other.Value.ClientData;
			}
		}

		// Token: 0x060017A5 RID: 6053 RVA: 0x000190A4 File Offset: 0x000172A4
		public void Set(object other)
		{
			this.Set(other as OnQueryLeaderboardRanksCompleteCallbackInfoInternal?);
		}
	}
}
