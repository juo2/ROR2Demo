using System;

namespace Epic.OnlineServices.Auth
{
	// Token: 0x02000518 RID: 1304
	public class DeletePersistentAuthCallbackInfo : ICallbackInfo, ISettable
	{
		// Token: 0x1700099B RID: 2459
		// (get) Token: 0x06001F87 RID: 8071 RVA: 0x000215F7 File Offset: 0x0001F7F7
		// (set) Token: 0x06001F88 RID: 8072 RVA: 0x000215FF File Offset: 0x0001F7FF
		public Result ResultCode { get; private set; }

		// Token: 0x1700099C RID: 2460
		// (get) Token: 0x06001F89 RID: 8073 RVA: 0x00021608 File Offset: 0x0001F808
		// (set) Token: 0x06001F8A RID: 8074 RVA: 0x00021610 File Offset: 0x0001F810
		public object ClientData { get; private set; }

		// Token: 0x06001F8B RID: 8075 RVA: 0x00021619 File Offset: 0x0001F819
		public Result? GetResultCode()
		{
			return new Result?(this.ResultCode);
		}

		// Token: 0x06001F8C RID: 8076 RVA: 0x00021628 File Offset: 0x0001F828
		internal void Set(DeletePersistentAuthCallbackInfoInternal? other)
		{
			if (other != null)
			{
				this.ResultCode = other.Value.ResultCode;
				this.ClientData = other.Value.ClientData;
			}
		}

		// Token: 0x06001F8D RID: 8077 RVA: 0x00021668 File Offset: 0x0001F868
		public void Set(object other)
		{
			this.Set(other as DeletePersistentAuthCallbackInfoInternal?);
		}
	}
}
