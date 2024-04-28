using System;

namespace Epic.OnlineServices.Friends
{
	// Token: 0x0200041D RID: 1053
	public class OnFriendsUpdateInfo : ICallbackInfo, ISettable
	{
		// Token: 0x17000778 RID: 1912
		// (get) Token: 0x0600194D RID: 6477 RVA: 0x0001AA7E File Offset: 0x00018C7E
		// (set) Token: 0x0600194E RID: 6478 RVA: 0x0001AA86 File Offset: 0x00018C86
		public object ClientData { get; private set; }

		// Token: 0x17000779 RID: 1913
		// (get) Token: 0x0600194F RID: 6479 RVA: 0x0001AA8F File Offset: 0x00018C8F
		// (set) Token: 0x06001950 RID: 6480 RVA: 0x0001AA97 File Offset: 0x00018C97
		public EpicAccountId LocalUserId { get; private set; }

		// Token: 0x1700077A RID: 1914
		// (get) Token: 0x06001951 RID: 6481 RVA: 0x0001AAA0 File Offset: 0x00018CA0
		// (set) Token: 0x06001952 RID: 6482 RVA: 0x0001AAA8 File Offset: 0x00018CA8
		public EpicAccountId TargetUserId { get; private set; }

		// Token: 0x1700077B RID: 1915
		// (get) Token: 0x06001953 RID: 6483 RVA: 0x0001AAB1 File Offset: 0x00018CB1
		// (set) Token: 0x06001954 RID: 6484 RVA: 0x0001AAB9 File Offset: 0x00018CB9
		public FriendsStatus PreviousStatus { get; private set; }

		// Token: 0x1700077C RID: 1916
		// (get) Token: 0x06001955 RID: 6485 RVA: 0x0001AAC2 File Offset: 0x00018CC2
		// (set) Token: 0x06001956 RID: 6486 RVA: 0x0001AACA File Offset: 0x00018CCA
		public FriendsStatus CurrentStatus { get; private set; }

		// Token: 0x06001957 RID: 6487 RVA: 0x0001AAD4 File Offset: 0x00018CD4
		public Result? GetResultCode()
		{
			return null;
		}

		// Token: 0x06001958 RID: 6488 RVA: 0x0001AAEC File Offset: 0x00018CEC
		internal void Set(OnFriendsUpdateInfoInternal? other)
		{
			if (other != null)
			{
				this.ClientData = other.Value.ClientData;
				this.LocalUserId = other.Value.LocalUserId;
				this.TargetUserId = other.Value.TargetUserId;
				this.PreviousStatus = other.Value.PreviousStatus;
				this.CurrentStatus = other.Value.CurrentStatus;
			}
		}

		// Token: 0x06001959 RID: 6489 RVA: 0x0001AB6B File Offset: 0x00018D6B
		public void Set(object other)
		{
			this.Set(other as OnFriendsUpdateInfoInternal?);
		}
	}
}
