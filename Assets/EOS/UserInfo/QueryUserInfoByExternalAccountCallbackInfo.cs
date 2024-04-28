using System;

namespace Epic.OnlineServices.UserInfo
{
	// Token: 0x02000035 RID: 53
	public class QueryUserInfoByExternalAccountCallbackInfo : ICallbackInfo, ISettable
	{
		// Token: 0x1700003E RID: 62
		// (get) Token: 0x06000333 RID: 819 RVA: 0x00004130 File Offset: 0x00002330
		// (set) Token: 0x06000334 RID: 820 RVA: 0x00004138 File Offset: 0x00002338
		public Result ResultCode { get; private set; }

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x06000335 RID: 821 RVA: 0x00004141 File Offset: 0x00002341
		// (set) Token: 0x06000336 RID: 822 RVA: 0x00004149 File Offset: 0x00002349
		public object ClientData { get; private set; }

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x06000337 RID: 823 RVA: 0x00004152 File Offset: 0x00002352
		// (set) Token: 0x06000338 RID: 824 RVA: 0x0000415A File Offset: 0x0000235A
		public EpicAccountId LocalUserId { get; private set; }

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x06000339 RID: 825 RVA: 0x00004163 File Offset: 0x00002363
		// (set) Token: 0x0600033A RID: 826 RVA: 0x0000416B File Offset: 0x0000236B
		public string ExternalAccountId { get; private set; }

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x0600033B RID: 827 RVA: 0x00004174 File Offset: 0x00002374
		// (set) Token: 0x0600033C RID: 828 RVA: 0x0000417C File Offset: 0x0000237C
		public ExternalAccountType AccountType { get; private set; }

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x0600033D RID: 829 RVA: 0x00004185 File Offset: 0x00002385
		// (set) Token: 0x0600033E RID: 830 RVA: 0x0000418D File Offset: 0x0000238D
		public EpicAccountId TargetUserId { get; private set; }

		// Token: 0x0600033F RID: 831 RVA: 0x00004196 File Offset: 0x00002396
		public Result? GetResultCode()
		{
			return new Result?(this.ResultCode);
		}

		// Token: 0x06000340 RID: 832 RVA: 0x000041A4 File Offset: 0x000023A4
		internal void Set(QueryUserInfoByExternalAccountCallbackInfoInternal? other)
		{
			if (other != null)
			{
				this.ResultCode = other.Value.ResultCode;
				this.ClientData = other.Value.ClientData;
				this.LocalUserId = other.Value.LocalUserId;
				this.ExternalAccountId = other.Value.ExternalAccountId;
				this.AccountType = other.Value.AccountType;
				this.TargetUserId = other.Value.TargetUserId;
			}
		}

		// Token: 0x06000341 RID: 833 RVA: 0x00004238 File Offset: 0x00002438
		public void Set(object other)
		{
			this.Set(other as QueryUserInfoByExternalAccountCallbackInfoInternal?);
		}
	}
}
