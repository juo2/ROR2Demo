using System;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x0200012E RID: 302
	public class SessionSearchFindCallbackInfo : ICallbackInfo, ISettable
	{
		// Token: 0x170001F0 RID: 496
		// (get) Token: 0x06000883 RID: 2179 RVA: 0x00009458 File Offset: 0x00007658
		// (set) Token: 0x06000884 RID: 2180 RVA: 0x00009460 File Offset: 0x00007660
		public Result ResultCode { get; private set; }

		// Token: 0x170001F1 RID: 497
		// (get) Token: 0x06000885 RID: 2181 RVA: 0x00009469 File Offset: 0x00007669
		// (set) Token: 0x06000886 RID: 2182 RVA: 0x00009471 File Offset: 0x00007671
		public object ClientData { get; private set; }

		// Token: 0x06000887 RID: 2183 RVA: 0x0000947A File Offset: 0x0000767A
		public Result? GetResultCode()
		{
			return new Result?(this.ResultCode);
		}

		// Token: 0x06000888 RID: 2184 RVA: 0x00009488 File Offset: 0x00007688
		internal void Set(SessionSearchFindCallbackInfoInternal? other)
		{
			if (other != null)
			{
				this.ResultCode = other.Value.ResultCode;
				this.ClientData = other.Value.ClientData;
			}
		}

		// Token: 0x06000889 RID: 2185 RVA: 0x000094C8 File Offset: 0x000076C8
		public void Set(object other)
		{
			this.Set(other as SessionSearchFindCallbackInfoInternal?);
		}
	}
}
