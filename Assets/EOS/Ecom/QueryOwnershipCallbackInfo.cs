using System;

namespace Epic.OnlineServices.Ecom
{
	// Token: 0x02000482 RID: 1154
	public class QueryOwnershipCallbackInfo : ICallbackInfo, ISettable
	{
		// Token: 0x1700087B RID: 2171
		// (get) Token: 0x06001C13 RID: 7187 RVA: 0x0001DA64 File Offset: 0x0001BC64
		// (set) Token: 0x06001C14 RID: 7188 RVA: 0x0001DA6C File Offset: 0x0001BC6C
		public Result ResultCode { get; private set; }

		// Token: 0x1700087C RID: 2172
		// (get) Token: 0x06001C15 RID: 7189 RVA: 0x0001DA75 File Offset: 0x0001BC75
		// (set) Token: 0x06001C16 RID: 7190 RVA: 0x0001DA7D File Offset: 0x0001BC7D
		public object ClientData { get; private set; }

		// Token: 0x1700087D RID: 2173
		// (get) Token: 0x06001C17 RID: 7191 RVA: 0x0001DA86 File Offset: 0x0001BC86
		// (set) Token: 0x06001C18 RID: 7192 RVA: 0x0001DA8E File Offset: 0x0001BC8E
		public EpicAccountId LocalUserId { get; private set; }

		// Token: 0x1700087E RID: 2174
		// (get) Token: 0x06001C19 RID: 7193 RVA: 0x0001DA97 File Offset: 0x0001BC97
		// (set) Token: 0x06001C1A RID: 7194 RVA: 0x0001DA9F File Offset: 0x0001BC9F
		public ItemOwnership[] ItemOwnership { get; private set; }

		// Token: 0x06001C1B RID: 7195 RVA: 0x0001DAA8 File Offset: 0x0001BCA8
		public Result? GetResultCode()
		{
			return new Result?(this.ResultCode);
		}

		// Token: 0x06001C1C RID: 7196 RVA: 0x0001DAB8 File Offset: 0x0001BCB8
		internal void Set(QueryOwnershipCallbackInfoInternal? other)
		{
			if (other != null)
			{
				this.ResultCode = other.Value.ResultCode;
				this.ClientData = other.Value.ClientData;
				this.LocalUserId = other.Value.LocalUserId;
				this.ItemOwnership = other.Value.ItemOwnership;
			}
		}

		// Token: 0x06001C1D RID: 7197 RVA: 0x0001DB22 File Offset: 0x0001BD22
		public void Set(object other)
		{
			this.Set(other as QueryOwnershipCallbackInfoInternal?);
		}
	}
}
