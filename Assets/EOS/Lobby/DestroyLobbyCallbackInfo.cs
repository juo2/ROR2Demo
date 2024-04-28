using System;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x0200030E RID: 782
	public class DestroyLobbyCallbackInfo : ICallbackInfo, ISettable
	{
		// Token: 0x170005B5 RID: 1461
		// (get) Token: 0x06001389 RID: 5001 RVA: 0x00014CEF File Offset: 0x00012EEF
		// (set) Token: 0x0600138A RID: 5002 RVA: 0x00014CF7 File Offset: 0x00012EF7
		public Result ResultCode { get; private set; }

		// Token: 0x170005B6 RID: 1462
		// (get) Token: 0x0600138B RID: 5003 RVA: 0x00014D00 File Offset: 0x00012F00
		// (set) Token: 0x0600138C RID: 5004 RVA: 0x00014D08 File Offset: 0x00012F08
		public object ClientData { get; private set; }

		// Token: 0x170005B7 RID: 1463
		// (get) Token: 0x0600138D RID: 5005 RVA: 0x00014D11 File Offset: 0x00012F11
		// (set) Token: 0x0600138E RID: 5006 RVA: 0x00014D19 File Offset: 0x00012F19
		public string LobbyId { get; private set; }

		// Token: 0x0600138F RID: 5007 RVA: 0x00014D22 File Offset: 0x00012F22
		public Result? GetResultCode()
		{
			return new Result?(this.ResultCode);
		}

		// Token: 0x06001390 RID: 5008 RVA: 0x00014D30 File Offset: 0x00012F30
		internal void Set(DestroyLobbyCallbackInfoInternal? other)
		{
			if (other != null)
			{
				this.ResultCode = other.Value.ResultCode;
				this.ClientData = other.Value.ClientData;
				this.LobbyId = other.Value.LobbyId;
			}
		}

		// Token: 0x06001391 RID: 5009 RVA: 0x00014D85 File Offset: 0x00012F85
		public void Set(object other)
		{
			this.Set(other as DestroyLobbyCallbackInfoInternal?);
		}
	}
}
