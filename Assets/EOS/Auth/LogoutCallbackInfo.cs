using System;

namespace Epic.OnlineServices.Auth
{
	// Token: 0x0200052A RID: 1322
	public class LogoutCallbackInfo : ICallbackInfo, ISettable
	{
		// Token: 0x170009D3 RID: 2515
		// (get) Token: 0x06001FFD RID: 8189 RVA: 0x00021DE8 File Offset: 0x0001FFE8
		// (set) Token: 0x06001FFE RID: 8190 RVA: 0x00021DF0 File Offset: 0x0001FFF0
		public Result ResultCode { get; private set; }

		// Token: 0x170009D4 RID: 2516
		// (get) Token: 0x06001FFF RID: 8191 RVA: 0x00021DF9 File Offset: 0x0001FFF9
		// (set) Token: 0x06002000 RID: 8192 RVA: 0x00021E01 File Offset: 0x00020001
		public object ClientData { get; private set; }

		// Token: 0x170009D5 RID: 2517
		// (get) Token: 0x06002001 RID: 8193 RVA: 0x00021E0A File Offset: 0x0002000A
		// (set) Token: 0x06002002 RID: 8194 RVA: 0x00021E12 File Offset: 0x00020012
		public EpicAccountId LocalUserId { get; private set; }

		// Token: 0x06002003 RID: 8195 RVA: 0x00021E1B File Offset: 0x0002001B
		public Result? GetResultCode()
		{
			return new Result?(this.ResultCode);
		}

		// Token: 0x06002004 RID: 8196 RVA: 0x00021E28 File Offset: 0x00020028
		internal void Set(LogoutCallbackInfoInternal? other)
		{
			if (other != null)
			{
				this.ResultCode = other.Value.ResultCode;
				this.ClientData = other.Value.ClientData;
				this.LocalUserId = other.Value.LocalUserId;
			}
		}

		// Token: 0x06002005 RID: 8197 RVA: 0x00021E7D File Offset: 0x0002007D
		public void Set(object other)
		{
			this.Set(other as LogoutCallbackInfoInternal?);
		}
	}
}
