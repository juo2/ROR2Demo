using System;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x02000324 RID: 804
	public class LeaveLobbyCallbackInfo : ICallbackInfo, ISettable
	{
		// Token: 0x170005F1 RID: 1521
		// (get) Token: 0x0600140D RID: 5133 RVA: 0x000154E4 File Offset: 0x000136E4
		// (set) Token: 0x0600140E RID: 5134 RVA: 0x000154EC File Offset: 0x000136EC
		public Result ResultCode { get; private set; }

		// Token: 0x170005F2 RID: 1522
		// (get) Token: 0x0600140F RID: 5135 RVA: 0x000154F5 File Offset: 0x000136F5
		// (set) Token: 0x06001410 RID: 5136 RVA: 0x000154FD File Offset: 0x000136FD
		public object ClientData { get; private set; }

		// Token: 0x170005F3 RID: 1523
		// (get) Token: 0x06001411 RID: 5137 RVA: 0x00015506 File Offset: 0x00013706
		// (set) Token: 0x06001412 RID: 5138 RVA: 0x0001550E File Offset: 0x0001370E
		public string LobbyId { get; private set; }

		// Token: 0x06001413 RID: 5139 RVA: 0x00015517 File Offset: 0x00013717
		public Result? GetResultCode()
		{
			return new Result?(this.ResultCode);
		}

		// Token: 0x06001414 RID: 5140 RVA: 0x00015524 File Offset: 0x00013724
		internal void Set(LeaveLobbyCallbackInfoInternal? other)
		{
			if (other != null)
			{
				this.ResultCode = other.Value.ResultCode;
				this.ClientData = other.Value.ClientData;
				this.LobbyId = other.Value.LobbyId;
			}
		}

		// Token: 0x06001415 RID: 5141 RVA: 0x00015579 File Offset: 0x00013779
		public void Set(object other)
		{
			this.Set(other as LeaveLobbyCallbackInfoInternal?);
		}
	}
}
