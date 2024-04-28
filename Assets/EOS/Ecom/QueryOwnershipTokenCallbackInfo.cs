using System;

namespace Epic.OnlineServices.Ecom
{
	// Token: 0x02000486 RID: 1158
	public class QueryOwnershipTokenCallbackInfo : ICallbackInfo, ISettable
	{
		// Token: 0x1700088A RID: 2186
		// (get) Token: 0x06001C31 RID: 7217 RVA: 0x0001DC6C File Offset: 0x0001BE6C
		// (set) Token: 0x06001C32 RID: 7218 RVA: 0x0001DC74 File Offset: 0x0001BE74
		public Result ResultCode { get; private set; }

		// Token: 0x1700088B RID: 2187
		// (get) Token: 0x06001C33 RID: 7219 RVA: 0x0001DC7D File Offset: 0x0001BE7D
		// (set) Token: 0x06001C34 RID: 7220 RVA: 0x0001DC85 File Offset: 0x0001BE85
		public object ClientData { get; private set; }

		// Token: 0x1700088C RID: 2188
		// (get) Token: 0x06001C35 RID: 7221 RVA: 0x0001DC8E File Offset: 0x0001BE8E
		// (set) Token: 0x06001C36 RID: 7222 RVA: 0x0001DC96 File Offset: 0x0001BE96
		public EpicAccountId LocalUserId { get; private set; }

		// Token: 0x1700088D RID: 2189
		// (get) Token: 0x06001C37 RID: 7223 RVA: 0x0001DC9F File Offset: 0x0001BE9F
		// (set) Token: 0x06001C38 RID: 7224 RVA: 0x0001DCA7 File Offset: 0x0001BEA7
		public string OwnershipToken { get; private set; }

		// Token: 0x06001C39 RID: 7225 RVA: 0x0001DCB0 File Offset: 0x0001BEB0
		public Result? GetResultCode()
		{
			return new Result?(this.ResultCode);
		}

		// Token: 0x06001C3A RID: 7226 RVA: 0x0001DCC0 File Offset: 0x0001BEC0
		internal void Set(QueryOwnershipTokenCallbackInfoInternal? other)
		{
			if (other != null)
			{
				this.ResultCode = other.Value.ResultCode;
				this.ClientData = other.Value.ClientData;
				this.LocalUserId = other.Value.LocalUserId;
				this.OwnershipToken = other.Value.OwnershipToken;
			}
		}

		// Token: 0x06001C3B RID: 7227 RVA: 0x0001DD2A File Offset: 0x0001BF2A
		public void Set(object other)
		{
			this.Set(other as QueryOwnershipTokenCallbackInfoInternal?);
		}
	}
}
