using System;

namespace Epic.OnlineServices.UserInfo
{
	// Token: 0x02000031 RID: 49
	public class QueryUserInfoByDisplayNameCallbackInfo : ICallbackInfo, ISettable
	{
		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000315 RID: 789 RVA: 0x00003F2B File Offset: 0x0000212B
		// (set) Token: 0x06000316 RID: 790 RVA: 0x00003F33 File Offset: 0x00002133
		public Result ResultCode { get; private set; }

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x06000317 RID: 791 RVA: 0x00003F3C File Offset: 0x0000213C
		// (set) Token: 0x06000318 RID: 792 RVA: 0x00003F44 File Offset: 0x00002144
		public object ClientData { get; private set; }

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x06000319 RID: 793 RVA: 0x00003F4D File Offset: 0x0000214D
		// (set) Token: 0x0600031A RID: 794 RVA: 0x00003F55 File Offset: 0x00002155
		public EpicAccountId LocalUserId { get; private set; }

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x0600031B RID: 795 RVA: 0x00003F5E File Offset: 0x0000215E
		// (set) Token: 0x0600031C RID: 796 RVA: 0x00003F66 File Offset: 0x00002166
		public EpicAccountId TargetUserId { get; private set; }

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x0600031D RID: 797 RVA: 0x00003F6F File Offset: 0x0000216F
		// (set) Token: 0x0600031E RID: 798 RVA: 0x00003F77 File Offset: 0x00002177
		public string DisplayName { get; private set; }

		// Token: 0x0600031F RID: 799 RVA: 0x00003F80 File Offset: 0x00002180
		public Result? GetResultCode()
		{
			return new Result?(this.ResultCode);
		}

		// Token: 0x06000320 RID: 800 RVA: 0x00003F90 File Offset: 0x00002190
		internal void Set(QueryUserInfoByDisplayNameCallbackInfoInternal? other)
		{
			if (other != null)
			{
				this.ResultCode = other.Value.ResultCode;
				this.ClientData = other.Value.ClientData;
				this.LocalUserId = other.Value.LocalUserId;
				this.TargetUserId = other.Value.TargetUserId;
				this.DisplayName = other.Value.DisplayName;
			}
		}

		// Token: 0x06000321 RID: 801 RVA: 0x0000400F File Offset: 0x0000220F
		public void Set(object other)
		{
			this.Set(other as QueryUserInfoByDisplayNameCallbackInfoInternal?);
		}
	}
}
