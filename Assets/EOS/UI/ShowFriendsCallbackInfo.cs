using System;

namespace Epic.OnlineServices.UI
{
	// Token: 0x0200005E RID: 94
	public class ShowFriendsCallbackInfo : ICallbackInfo, ISettable
	{
		// Token: 0x17000086 RID: 134
		// (get) Token: 0x06000409 RID: 1033 RVA: 0x00004EBC File Offset: 0x000030BC
		// (set) Token: 0x0600040A RID: 1034 RVA: 0x00004EC4 File Offset: 0x000030C4
		public Result ResultCode { get; private set; }

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x0600040B RID: 1035 RVA: 0x00004ECD File Offset: 0x000030CD
		// (set) Token: 0x0600040C RID: 1036 RVA: 0x00004ED5 File Offset: 0x000030D5
		public object ClientData { get; private set; }

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x0600040D RID: 1037 RVA: 0x00004EDE File Offset: 0x000030DE
		// (set) Token: 0x0600040E RID: 1038 RVA: 0x00004EE6 File Offset: 0x000030E6
		public EpicAccountId LocalUserId { get; private set; }

		// Token: 0x0600040F RID: 1039 RVA: 0x00004EEF File Offset: 0x000030EF
		public Result? GetResultCode()
		{
			return new Result?(this.ResultCode);
		}

		// Token: 0x06000410 RID: 1040 RVA: 0x00004EFC File Offset: 0x000030FC
		internal void Set(ShowFriendsCallbackInfoInternal? other)
		{
			if (other != null)
			{
				this.ResultCode = other.Value.ResultCode;
				this.ClientData = other.Value.ClientData;
				this.LocalUserId = other.Value.LocalUserId;
			}
		}

		// Token: 0x06000411 RID: 1041 RVA: 0x00004F51 File Offset: 0x00003151
		public void Set(object other)
		{
			this.Set(other as ShowFriendsCallbackInfoInternal?);
		}
	}
}
