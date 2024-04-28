using System;

namespace Epic.OnlineServices.Achievements
{
	// Token: 0x02000622 RID: 1570
	public class OnUnlockAchievementsCompleteCallbackInfo : ICallbackInfo, ISettable
	{
		// Token: 0x17000C0E RID: 3086
		// (get) Token: 0x06002662 RID: 9826 RVA: 0x00028D34 File Offset: 0x00026F34
		// (set) Token: 0x06002663 RID: 9827 RVA: 0x00028D3C File Offset: 0x00026F3C
		public Result ResultCode { get; private set; }

		// Token: 0x17000C0F RID: 3087
		// (get) Token: 0x06002664 RID: 9828 RVA: 0x00028D45 File Offset: 0x00026F45
		// (set) Token: 0x06002665 RID: 9829 RVA: 0x00028D4D File Offset: 0x00026F4D
		public object ClientData { get; private set; }

		// Token: 0x17000C10 RID: 3088
		// (get) Token: 0x06002666 RID: 9830 RVA: 0x00028D56 File Offset: 0x00026F56
		// (set) Token: 0x06002667 RID: 9831 RVA: 0x00028D5E File Offset: 0x00026F5E
		public ProductUserId UserId { get; private set; }

		// Token: 0x17000C11 RID: 3089
		// (get) Token: 0x06002668 RID: 9832 RVA: 0x00028D67 File Offset: 0x00026F67
		// (set) Token: 0x06002669 RID: 9833 RVA: 0x00028D6F File Offset: 0x00026F6F
		public uint AchievementsCount { get; private set; }

		// Token: 0x0600266A RID: 9834 RVA: 0x00028D78 File Offset: 0x00026F78
		public Result? GetResultCode()
		{
			return new Result?(this.ResultCode);
		}

		// Token: 0x0600266B RID: 9835 RVA: 0x00028D88 File Offset: 0x00026F88
		internal void Set(OnUnlockAchievementsCompleteCallbackInfoInternal? other)
		{
			if (other != null)
			{
				this.ResultCode = other.Value.ResultCode;
				this.ClientData = other.Value.ClientData;
				this.UserId = other.Value.UserId;
				this.AchievementsCount = other.Value.AchievementsCount;
			}
		}

		// Token: 0x0600266C RID: 9836 RVA: 0x00028DF2 File Offset: 0x00026FF2
		public void Set(object other)
		{
			this.Set(other as OnUnlockAchievementsCompleteCallbackInfoInternal?);
		}
	}
}
