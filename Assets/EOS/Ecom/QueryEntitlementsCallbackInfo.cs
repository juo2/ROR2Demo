using System;

namespace Epic.OnlineServices.Ecom
{
	// Token: 0x0200047A RID: 1146
	public class QueryEntitlementsCallbackInfo : ICallbackInfo, ISettable
	{
		// Token: 0x17000863 RID: 2147
		// (get) Token: 0x06001BE0 RID: 7136 RVA: 0x0001D735 File Offset: 0x0001B935
		// (set) Token: 0x06001BE1 RID: 7137 RVA: 0x0001D73D File Offset: 0x0001B93D
		public Result ResultCode { get; private set; }

		// Token: 0x17000864 RID: 2148
		// (get) Token: 0x06001BE2 RID: 7138 RVA: 0x0001D746 File Offset: 0x0001B946
		// (set) Token: 0x06001BE3 RID: 7139 RVA: 0x0001D74E File Offset: 0x0001B94E
		public object ClientData { get; private set; }

		// Token: 0x17000865 RID: 2149
		// (get) Token: 0x06001BE4 RID: 7140 RVA: 0x0001D757 File Offset: 0x0001B957
		// (set) Token: 0x06001BE5 RID: 7141 RVA: 0x0001D75F File Offset: 0x0001B95F
		public EpicAccountId LocalUserId { get; private set; }

		// Token: 0x06001BE6 RID: 7142 RVA: 0x0001D768 File Offset: 0x0001B968
		public Result? GetResultCode()
		{
			return new Result?(this.ResultCode);
		}

		// Token: 0x06001BE7 RID: 7143 RVA: 0x0001D778 File Offset: 0x0001B978
		internal void Set(QueryEntitlementsCallbackInfoInternal? other)
		{
			if (other != null)
			{
				this.ResultCode = other.Value.ResultCode;
				this.ClientData = other.Value.ClientData;
				this.LocalUserId = other.Value.LocalUserId;
			}
		}

		// Token: 0x06001BE8 RID: 7144 RVA: 0x0001D7CD File Offset: 0x0001B9CD
		public void Set(object other)
		{
			this.Set(other as QueryEntitlementsCallbackInfoInternal?);
		}
	}
}
