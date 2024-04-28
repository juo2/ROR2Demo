using System;

namespace Epic.OnlineServices.Auth
{
	// Token: 0x0200054A RID: 1354
	public class VerifyUserAuthCallbackInfo : ICallbackInfo, ISettable
	{
		// Token: 0x17000A22 RID: 2594
		// (get) Token: 0x060020E4 RID: 8420 RVA: 0x00022A90 File Offset: 0x00020C90
		// (set) Token: 0x060020E5 RID: 8421 RVA: 0x00022A98 File Offset: 0x00020C98
		public Result ResultCode { get; private set; }

		// Token: 0x17000A23 RID: 2595
		// (get) Token: 0x060020E6 RID: 8422 RVA: 0x00022AA1 File Offset: 0x00020CA1
		// (set) Token: 0x060020E7 RID: 8423 RVA: 0x00022AA9 File Offset: 0x00020CA9
		public object ClientData { get; private set; }

		// Token: 0x060020E8 RID: 8424 RVA: 0x00022AB2 File Offset: 0x00020CB2
		public Result? GetResultCode()
		{
			return new Result?(this.ResultCode);
		}

		// Token: 0x060020E9 RID: 8425 RVA: 0x00022AC0 File Offset: 0x00020CC0
		internal void Set(VerifyUserAuthCallbackInfoInternal? other)
		{
			if (other != null)
			{
				this.ResultCode = other.Value.ResultCode;
				this.ClientData = other.Value.ClientData;
			}
		}

		// Token: 0x060020EA RID: 8426 RVA: 0x00022B00 File Offset: 0x00020D00
		public void Set(object other)
		{
			this.Set(other as VerifyUserAuthCallbackInfoInternal?);
		}
	}
}
