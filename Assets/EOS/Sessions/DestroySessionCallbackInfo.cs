using System;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x020000C4 RID: 196
	public class DestroySessionCallbackInfo : ICallbackInfo, ISettable
	{
		// Token: 0x17000154 RID: 340
		// (get) Token: 0x06000672 RID: 1650 RVA: 0x00007823 File Offset: 0x00005A23
		// (set) Token: 0x06000673 RID: 1651 RVA: 0x0000782B File Offset: 0x00005A2B
		public Result ResultCode { get; private set; }

		// Token: 0x17000155 RID: 341
		// (get) Token: 0x06000674 RID: 1652 RVA: 0x00007834 File Offset: 0x00005A34
		// (set) Token: 0x06000675 RID: 1653 RVA: 0x0000783C File Offset: 0x00005A3C
		public object ClientData { get; private set; }

		// Token: 0x06000676 RID: 1654 RVA: 0x00007845 File Offset: 0x00005A45
		public Result? GetResultCode()
		{
			return new Result?(this.ResultCode);
		}

		// Token: 0x06000677 RID: 1655 RVA: 0x00007854 File Offset: 0x00005A54
		internal void Set(DestroySessionCallbackInfoInternal? other)
		{
			if (other != null)
			{
				this.ResultCode = other.Value.ResultCode;
				this.ClientData = other.Value.ClientData;
			}
		}

		// Token: 0x06000678 RID: 1656 RVA: 0x00007894 File Offset: 0x00005A94
		public void Set(object other)
		{
			this.Set(other as DestroySessionCallbackInfoInternal?);
		}
	}
}
