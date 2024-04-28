using System;

namespace Epic.OnlineServices.Leaderboards
{
	// Token: 0x020003D3 RID: 979
	public class OnQueryLeaderboardUserScoresCompleteCallbackInfo : ICallbackInfo, ISettable
	{
		// Token: 0x170006F2 RID: 1778
		// (get) Token: 0x060017B2 RID: 6066 RVA: 0x000190E4 File Offset: 0x000172E4
		// (set) Token: 0x060017B3 RID: 6067 RVA: 0x000190EC File Offset: 0x000172EC
		public Result ResultCode { get; private set; }

		// Token: 0x170006F3 RID: 1779
		// (get) Token: 0x060017B4 RID: 6068 RVA: 0x000190F5 File Offset: 0x000172F5
		// (set) Token: 0x060017B5 RID: 6069 RVA: 0x000190FD File Offset: 0x000172FD
		public object ClientData { get; private set; }

		// Token: 0x060017B6 RID: 6070 RVA: 0x00019106 File Offset: 0x00017306
		public Result? GetResultCode()
		{
			return new Result?(this.ResultCode);
		}

		// Token: 0x060017B7 RID: 6071 RVA: 0x00019114 File Offset: 0x00017314
		internal void Set(OnQueryLeaderboardUserScoresCompleteCallbackInfoInternal? other)
		{
			if (other != null)
			{
				this.ResultCode = other.Value.ResultCode;
				this.ClientData = other.Value.ClientData;
			}
		}

		// Token: 0x060017B8 RID: 6072 RVA: 0x00019154 File Offset: 0x00017354
		public void Set(object other)
		{
			this.Set(other as OnQueryLeaderboardUserScoresCompleteCallbackInfoInternal?);
		}
	}
}
