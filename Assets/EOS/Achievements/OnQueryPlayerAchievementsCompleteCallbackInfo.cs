using System;

namespace Epic.OnlineServices.Achievements
{
	// Token: 0x0200061E RID: 1566
	public class OnQueryPlayerAchievementsCompleteCallbackInfo : ICallbackInfo, ISettable
	{
		// Token: 0x17000C07 RID: 3079
		// (get) Token: 0x0600264C RID: 9804 RVA: 0x00028C44 File Offset: 0x00026E44
		// (set) Token: 0x0600264D RID: 9805 RVA: 0x00028C4C File Offset: 0x00026E4C
		public Result ResultCode { get; private set; }

		// Token: 0x17000C08 RID: 3080
		// (get) Token: 0x0600264E RID: 9806 RVA: 0x00028C55 File Offset: 0x00026E55
		// (set) Token: 0x0600264F RID: 9807 RVA: 0x00028C5D File Offset: 0x00026E5D
		public object ClientData { get; private set; }

		// Token: 0x17000C09 RID: 3081
		// (get) Token: 0x06002650 RID: 9808 RVA: 0x00028C66 File Offset: 0x00026E66
		// (set) Token: 0x06002651 RID: 9809 RVA: 0x00028C6E File Offset: 0x00026E6E
		public ProductUserId UserId { get; private set; }

		// Token: 0x06002652 RID: 9810 RVA: 0x00028C77 File Offset: 0x00026E77
		public Result? GetResultCode()
		{
			return new Result?(this.ResultCode);
		}

		// Token: 0x06002653 RID: 9811 RVA: 0x00028C84 File Offset: 0x00026E84
		internal void Set(OnQueryPlayerAchievementsCompleteCallbackInfoInternal? other)
		{
			if (other != null)
			{
				this.ResultCode = other.Value.ResultCode;
				this.ClientData = other.Value.ClientData;
				this.UserId = other.Value.UserId;
			}
		}

		// Token: 0x06002654 RID: 9812 RVA: 0x00028CD9 File Offset: 0x00026ED9
		public void Set(object other)
		{
			this.Set(other as OnQueryPlayerAchievementsCompleteCallbackInfoInternal?);
		}
	}
}
