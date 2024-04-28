using System;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x020000CA RID: 202
	public class EndSessionCallbackInfo : ICallbackInfo, ISettable
	{
		// Token: 0x1700015D RID: 349
		// (get) Token: 0x0600068B RID: 1675 RVA: 0x0000797C File Offset: 0x00005B7C
		// (set) Token: 0x0600068C RID: 1676 RVA: 0x00007984 File Offset: 0x00005B84
		public Result ResultCode { get; private set; }

		// Token: 0x1700015E RID: 350
		// (get) Token: 0x0600068D RID: 1677 RVA: 0x0000798D File Offset: 0x00005B8D
		// (set) Token: 0x0600068E RID: 1678 RVA: 0x00007995 File Offset: 0x00005B95
		public object ClientData { get; private set; }

		// Token: 0x0600068F RID: 1679 RVA: 0x0000799E File Offset: 0x00005B9E
		public Result? GetResultCode()
		{
			return new Result?(this.ResultCode);
		}

		// Token: 0x06000690 RID: 1680 RVA: 0x000079AC File Offset: 0x00005BAC
		internal void Set(EndSessionCallbackInfoInternal? other)
		{
			if (other != null)
			{
				this.ResultCode = other.Value.ResultCode;
				this.ClientData = other.Value.ClientData;
			}
		}

		// Token: 0x06000691 RID: 1681 RVA: 0x000079EC File Offset: 0x00005BEC
		public void Set(object other)
		{
			this.Set(other as EndSessionCallbackInfoInternal?);
		}
	}
}
