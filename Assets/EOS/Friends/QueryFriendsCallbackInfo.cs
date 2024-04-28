using System;

namespace Epic.OnlineServices.Friends
{
	// Token: 0x02000425 RID: 1061
	public class QueryFriendsCallbackInfo : ICallbackInfo, ISettable
	{
		// Token: 0x17000783 RID: 1923
		// (get) Token: 0x06001979 RID: 6521 RVA: 0x0001ABEC File Offset: 0x00018DEC
		// (set) Token: 0x0600197A RID: 6522 RVA: 0x0001ABF4 File Offset: 0x00018DF4
		public Result ResultCode { get; private set; }

		// Token: 0x17000784 RID: 1924
		// (get) Token: 0x0600197B RID: 6523 RVA: 0x0001ABFD File Offset: 0x00018DFD
		// (set) Token: 0x0600197C RID: 6524 RVA: 0x0001AC05 File Offset: 0x00018E05
		public object ClientData { get; private set; }

		// Token: 0x17000785 RID: 1925
		// (get) Token: 0x0600197D RID: 6525 RVA: 0x0001AC0E File Offset: 0x00018E0E
		// (set) Token: 0x0600197E RID: 6526 RVA: 0x0001AC16 File Offset: 0x00018E16
		public EpicAccountId LocalUserId { get; private set; }

		// Token: 0x0600197F RID: 6527 RVA: 0x0001AC1F File Offset: 0x00018E1F
		public Result? GetResultCode()
		{
			return new Result?(this.ResultCode);
		}

		// Token: 0x06001980 RID: 6528 RVA: 0x0001AC2C File Offset: 0x00018E2C
		internal void Set(QueryFriendsCallbackInfoInternal? other)
		{
			if (other != null)
			{
				this.ResultCode = other.Value.ResultCode;
				this.ClientData = other.Value.ClientData;
				this.LocalUserId = other.Value.LocalUserId;
			}
		}

		// Token: 0x06001981 RID: 6529 RVA: 0x0001AC81 File Offset: 0x00018E81
		public void Set(object other)
		{
			this.Set(other as QueryFriendsCallbackInfoInternal?);
		}
	}
}
