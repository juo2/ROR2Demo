using System;

namespace Epic.OnlineServices.Auth
{
	// Token: 0x02000540 RID: 1344
	public class QueryIdTokenCallbackInfo : ICallbackInfo, ISettable
	{
		// Token: 0x170009E4 RID: 2532
		// (get) Token: 0x06002068 RID: 8296 RVA: 0x000220F3 File Offset: 0x000202F3
		// (set) Token: 0x06002069 RID: 8297 RVA: 0x000220FB File Offset: 0x000202FB
		public Result ResultCode { get; private set; }

		// Token: 0x170009E5 RID: 2533
		// (get) Token: 0x0600206A RID: 8298 RVA: 0x00022104 File Offset: 0x00020304
		// (set) Token: 0x0600206B RID: 8299 RVA: 0x0002210C File Offset: 0x0002030C
		public object ClientData { get; private set; }

		// Token: 0x170009E6 RID: 2534
		// (get) Token: 0x0600206C RID: 8300 RVA: 0x00022115 File Offset: 0x00020315
		// (set) Token: 0x0600206D RID: 8301 RVA: 0x0002211D File Offset: 0x0002031D
		public EpicAccountId LocalUserId { get; private set; }

		// Token: 0x170009E7 RID: 2535
		// (get) Token: 0x0600206E RID: 8302 RVA: 0x00022126 File Offset: 0x00020326
		// (set) Token: 0x0600206F RID: 8303 RVA: 0x0002212E File Offset: 0x0002032E
		public EpicAccountId TargetAccountId { get; private set; }

		// Token: 0x06002070 RID: 8304 RVA: 0x00022137 File Offset: 0x00020337
		public Result? GetResultCode()
		{
			return new Result?(this.ResultCode);
		}

		// Token: 0x06002071 RID: 8305 RVA: 0x00022144 File Offset: 0x00020344
		internal void Set(QueryIdTokenCallbackInfoInternal? other)
		{
			if (other != null)
			{
				this.ResultCode = other.Value.ResultCode;
				this.ClientData = other.Value.ClientData;
				this.LocalUserId = other.Value.LocalUserId;
				this.TargetAccountId = other.Value.TargetAccountId;
			}
		}

		// Token: 0x06002072 RID: 8306 RVA: 0x000221AE File Offset: 0x000203AE
		public void Set(object other)
		{
			this.Set(other as QueryIdTokenCallbackInfoInternal?);
		}
	}
}
