using System;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x02000102 RID: 258
	public class SendInviteCallbackInfo : ICallbackInfo, ISettable
	{
		// Token: 0x170001A1 RID: 417
		// (get) Token: 0x0600078E RID: 1934 RVA: 0x000082D0 File Offset: 0x000064D0
		// (set) Token: 0x0600078F RID: 1935 RVA: 0x000082D8 File Offset: 0x000064D8
		public Result ResultCode { get; private set; }

		// Token: 0x170001A2 RID: 418
		// (get) Token: 0x06000790 RID: 1936 RVA: 0x000082E1 File Offset: 0x000064E1
		// (set) Token: 0x06000791 RID: 1937 RVA: 0x000082E9 File Offset: 0x000064E9
		public object ClientData { get; private set; }

		// Token: 0x06000792 RID: 1938 RVA: 0x000082F2 File Offset: 0x000064F2
		public Result? GetResultCode()
		{
			return new Result?(this.ResultCode);
		}

		// Token: 0x06000793 RID: 1939 RVA: 0x00008300 File Offset: 0x00006500
		internal void Set(SendInviteCallbackInfoInternal? other)
		{
			if (other != null)
			{
				this.ResultCode = other.Value.ResultCode;
				this.ClientData = other.Value.ClientData;
			}
		}

		// Token: 0x06000794 RID: 1940 RVA: 0x00008340 File Offset: 0x00006540
		public void Set(object other)
		{
			this.Set(other as SendInviteCallbackInfoInternal?);
		}
	}
}
