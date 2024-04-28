using System;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x02000149 RID: 329
	public class UpdateSessionCallbackInfo : ICallbackInfo, ISettable
	{
		// Token: 0x17000217 RID: 535
		// (get) Token: 0x0600091E RID: 2334 RVA: 0x0000A2D0 File Offset: 0x000084D0
		// (set) Token: 0x0600091F RID: 2335 RVA: 0x0000A2D8 File Offset: 0x000084D8
		public Result ResultCode { get; private set; }

		// Token: 0x17000218 RID: 536
		// (get) Token: 0x06000920 RID: 2336 RVA: 0x0000A2E1 File Offset: 0x000084E1
		// (set) Token: 0x06000921 RID: 2337 RVA: 0x0000A2E9 File Offset: 0x000084E9
		public object ClientData { get; private set; }

		// Token: 0x17000219 RID: 537
		// (get) Token: 0x06000922 RID: 2338 RVA: 0x0000A2F2 File Offset: 0x000084F2
		// (set) Token: 0x06000923 RID: 2339 RVA: 0x0000A2FA File Offset: 0x000084FA
		public string SessionName { get; private set; }

		// Token: 0x1700021A RID: 538
		// (get) Token: 0x06000924 RID: 2340 RVA: 0x0000A303 File Offset: 0x00008503
		// (set) Token: 0x06000925 RID: 2341 RVA: 0x0000A30B File Offset: 0x0000850B
		public string SessionId { get; private set; }

		// Token: 0x06000926 RID: 2342 RVA: 0x0000A314 File Offset: 0x00008514
		public Result? GetResultCode()
		{
			return new Result?(this.ResultCode);
		}

		// Token: 0x06000927 RID: 2343 RVA: 0x0000A324 File Offset: 0x00008524
		internal void Set(UpdateSessionCallbackInfoInternal? other)
		{
			if (other != null)
			{
				this.ResultCode = other.Value.ResultCode;
				this.ClientData = other.Value.ClientData;
				this.SessionName = other.Value.SessionName;
				this.SessionId = other.Value.SessionId;
			}
		}

		// Token: 0x06000928 RID: 2344 RVA: 0x0000A38E File Offset: 0x0000858E
		public void Set(object other)
		{
			this.Set(other as UpdateSessionCallbackInfoInternal?);
		}
	}
}
