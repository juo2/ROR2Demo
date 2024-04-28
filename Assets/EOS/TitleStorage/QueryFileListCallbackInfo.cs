using System;

namespace Epic.OnlineServices.TitleStorage
{
	// Token: 0x0200007F RID: 127
	public class QueryFileListCallbackInfo : ICallbackInfo, ISettable
	{
		// Token: 0x170000BC RID: 188
		// (get) Token: 0x060004C6 RID: 1222 RVA: 0x00005948 File Offset: 0x00003B48
		// (set) Token: 0x060004C7 RID: 1223 RVA: 0x00005950 File Offset: 0x00003B50
		public Result ResultCode { get; private set; }

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x060004C8 RID: 1224 RVA: 0x00005959 File Offset: 0x00003B59
		// (set) Token: 0x060004C9 RID: 1225 RVA: 0x00005961 File Offset: 0x00003B61
		public object ClientData { get; private set; }

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x060004CA RID: 1226 RVA: 0x0000596A File Offset: 0x00003B6A
		// (set) Token: 0x060004CB RID: 1227 RVA: 0x00005972 File Offset: 0x00003B72
		public ProductUserId LocalUserId { get; private set; }

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x060004CC RID: 1228 RVA: 0x0000597B File Offset: 0x00003B7B
		// (set) Token: 0x060004CD RID: 1229 RVA: 0x00005983 File Offset: 0x00003B83
		public uint FileCount { get; private set; }

		// Token: 0x060004CE RID: 1230 RVA: 0x0000598C File Offset: 0x00003B8C
		public Result? GetResultCode()
		{
			return new Result?(this.ResultCode);
		}

		// Token: 0x060004CF RID: 1231 RVA: 0x0000599C File Offset: 0x00003B9C
		internal void Set(QueryFileListCallbackInfoInternal? other)
		{
			if (other != null)
			{
				this.ResultCode = other.Value.ResultCode;
				this.ClientData = other.Value.ClientData;
				this.LocalUserId = other.Value.LocalUserId;
				this.FileCount = other.Value.FileCount;
			}
		}

		// Token: 0x060004D0 RID: 1232 RVA: 0x00005A06 File Offset: 0x00003C06
		public void Set(object other)
		{
			this.Set(other as QueryFileListCallbackInfoInternal?);
		}
	}
}
