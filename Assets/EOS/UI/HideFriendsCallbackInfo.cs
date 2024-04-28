using System;

namespace Epic.OnlineServices.UI
{
	// Token: 0x02000048 RID: 72
	public class HideFriendsCallbackInfo : ICallbackInfo, ISettable
	{
		// Token: 0x1700006E RID: 110
		// (get) Token: 0x060003B2 RID: 946 RVA: 0x00004B4A File Offset: 0x00002D4A
		// (set) Token: 0x060003B3 RID: 947 RVA: 0x00004B52 File Offset: 0x00002D52
		public Result ResultCode { get; private set; }

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x060003B4 RID: 948 RVA: 0x00004B5B File Offset: 0x00002D5B
		// (set) Token: 0x060003B5 RID: 949 RVA: 0x00004B63 File Offset: 0x00002D63
		public object ClientData { get; private set; }

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x060003B6 RID: 950 RVA: 0x00004B6C File Offset: 0x00002D6C
		// (set) Token: 0x060003B7 RID: 951 RVA: 0x00004B74 File Offset: 0x00002D74
		public EpicAccountId LocalUserId { get; private set; }

		// Token: 0x060003B8 RID: 952 RVA: 0x00004B7D File Offset: 0x00002D7D
		public Result? GetResultCode()
		{
			return new Result?(this.ResultCode);
		}

		// Token: 0x060003B9 RID: 953 RVA: 0x00004B8C File Offset: 0x00002D8C
		internal void Set(HideFriendsCallbackInfoInternal? other)
		{
			if (other != null)
			{
				this.ResultCode = other.Value.ResultCode;
				this.ClientData = other.Value.ClientData;
				this.LocalUserId = other.Value.LocalUserId;
			}
		}

		// Token: 0x060003BA RID: 954 RVA: 0x00004BE1 File Offset: 0x00002DE1
		public void Set(object other)
		{
			this.Set(other as HideFriendsCallbackInfoInternal?);
		}
	}
}
