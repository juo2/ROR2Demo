using System;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x02000141 RID: 321
	public class StartSessionCallbackInfo : ICallbackInfo, ISettable
	{
		// Token: 0x17000205 RID: 517
		// (get) Token: 0x060008F4 RID: 2292 RVA: 0x0000A044 File Offset: 0x00008244
		// (set) Token: 0x060008F5 RID: 2293 RVA: 0x0000A04C File Offset: 0x0000824C
		public Result ResultCode { get; private set; }

		// Token: 0x17000206 RID: 518
		// (get) Token: 0x060008F6 RID: 2294 RVA: 0x0000A055 File Offset: 0x00008255
		// (set) Token: 0x060008F7 RID: 2295 RVA: 0x0000A05D File Offset: 0x0000825D
		public object ClientData { get; private set; }

		// Token: 0x060008F8 RID: 2296 RVA: 0x0000A066 File Offset: 0x00008266
		public Result? GetResultCode()
		{
			return new Result?(this.ResultCode);
		}

		// Token: 0x060008F9 RID: 2297 RVA: 0x0000A074 File Offset: 0x00008274
		internal void Set(StartSessionCallbackInfoInternal? other)
		{
			if (other != null)
			{
				this.ResultCode = other.Value.ResultCode;
				this.ClientData = other.Value.ClientData;
			}
		}

		// Token: 0x060008FA RID: 2298 RVA: 0x0000A0B4 File Offset: 0x000082B4
		public void Set(object other)
		{
			this.Set(other as StartSessionCallbackInfoInternal?);
		}
	}
}
