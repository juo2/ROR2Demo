using System;

namespace Epic.OnlineServices.UserInfo
{
	// Token: 0x02000039 RID: 57
	public class QueryUserInfoCallbackInfo : ICallbackInfo, ISettable
	{
		// Token: 0x17000051 RID: 81
		// (get) Token: 0x06000357 RID: 855 RVA: 0x00004386 File Offset: 0x00002586
		// (set) Token: 0x06000358 RID: 856 RVA: 0x0000438E File Offset: 0x0000258E
		public Result ResultCode { get; private set; }

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x06000359 RID: 857 RVA: 0x00004397 File Offset: 0x00002597
		// (set) Token: 0x0600035A RID: 858 RVA: 0x0000439F File Offset: 0x0000259F
		public object ClientData { get; private set; }

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x0600035B RID: 859 RVA: 0x000043A8 File Offset: 0x000025A8
		// (set) Token: 0x0600035C RID: 860 RVA: 0x000043B0 File Offset: 0x000025B0
		public EpicAccountId LocalUserId { get; private set; }

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x0600035D RID: 861 RVA: 0x000043B9 File Offset: 0x000025B9
		// (set) Token: 0x0600035E RID: 862 RVA: 0x000043C1 File Offset: 0x000025C1
		public EpicAccountId TargetUserId { get; private set; }

		// Token: 0x0600035F RID: 863 RVA: 0x000043CA File Offset: 0x000025CA
		public Result? GetResultCode()
		{
			return new Result?(this.ResultCode);
		}

		// Token: 0x06000360 RID: 864 RVA: 0x000043D8 File Offset: 0x000025D8
		internal void Set(QueryUserInfoCallbackInfoInternal? other)
		{
			if (other != null)
			{
				this.ResultCode = other.Value.ResultCode;
				this.ClientData = other.Value.ClientData;
				this.LocalUserId = other.Value.LocalUserId;
				this.TargetUserId = other.Value.TargetUserId;
			}
		}

		// Token: 0x06000361 RID: 865 RVA: 0x00004442 File Offset: 0x00002642
		public void Set(object other)
		{
			this.Set(other as QueryUserInfoCallbackInfoInternal?);
		}
	}
}
