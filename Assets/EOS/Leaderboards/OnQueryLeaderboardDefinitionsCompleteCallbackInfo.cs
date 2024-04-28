using System;

namespace Epic.OnlineServices.Leaderboards
{
	// Token: 0x020003CB RID: 971
	public class OnQueryLeaderboardDefinitionsCompleteCallbackInfo : ICallbackInfo, ISettable
	{
		// Token: 0x170006E8 RID: 1768
		// (get) Token: 0x0600178C RID: 6028 RVA: 0x00018F84 File Offset: 0x00017184
		// (set) Token: 0x0600178D RID: 6029 RVA: 0x00018F8C File Offset: 0x0001718C
		public Result ResultCode { get; private set; }

		// Token: 0x170006E9 RID: 1769
		// (get) Token: 0x0600178E RID: 6030 RVA: 0x00018F95 File Offset: 0x00017195
		// (set) Token: 0x0600178F RID: 6031 RVA: 0x00018F9D File Offset: 0x0001719D
		public object ClientData { get; private set; }

		// Token: 0x06001790 RID: 6032 RVA: 0x00018FA6 File Offset: 0x000171A6
		public Result? GetResultCode()
		{
			return new Result?(this.ResultCode);
		}

		// Token: 0x06001791 RID: 6033 RVA: 0x00018FB4 File Offset: 0x000171B4
		internal void Set(OnQueryLeaderboardDefinitionsCompleteCallbackInfoInternal? other)
		{
			if (other != null)
			{
				this.ResultCode = other.Value.ResultCode;
				this.ClientData = other.Value.ClientData;
			}
		}

		// Token: 0x06001792 RID: 6034 RVA: 0x00018FF4 File Offset: 0x000171F4
		public void Set(object other)
		{
			this.Set(other as OnQueryLeaderboardDefinitionsCompleteCallbackInfoInternal?);
		}
	}
}
