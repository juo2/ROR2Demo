using System;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x0200031C RID: 796
	public class JoinLobbyCallbackInfo : ICallbackInfo, ISettable
	{
		// Token: 0x170005D5 RID: 1493
		// (get) Token: 0x060013D4 RID: 5076 RVA: 0x00015150 File Offset: 0x00013350
		// (set) Token: 0x060013D5 RID: 5077 RVA: 0x00015158 File Offset: 0x00013358
		public Result ResultCode { get; private set; }

		// Token: 0x170005D6 RID: 1494
		// (get) Token: 0x060013D6 RID: 5078 RVA: 0x00015161 File Offset: 0x00013361
		// (set) Token: 0x060013D7 RID: 5079 RVA: 0x00015169 File Offset: 0x00013369
		public object ClientData { get; private set; }

		// Token: 0x170005D7 RID: 1495
		// (get) Token: 0x060013D8 RID: 5080 RVA: 0x00015172 File Offset: 0x00013372
		// (set) Token: 0x060013D9 RID: 5081 RVA: 0x0001517A File Offset: 0x0001337A
		public string LobbyId { get; private set; }

		// Token: 0x060013DA RID: 5082 RVA: 0x00015183 File Offset: 0x00013383
		public Result? GetResultCode()
		{
			return new Result?(this.ResultCode);
		}

		// Token: 0x060013DB RID: 5083 RVA: 0x00015190 File Offset: 0x00013390
		internal void Set(JoinLobbyCallbackInfoInternal? other)
		{
			if (other != null)
			{
				this.ResultCode = other.Value.ResultCode;
				this.ClientData = other.Value.ClientData;
				this.LobbyId = other.Value.LobbyId;
			}
		}

		// Token: 0x060013DC RID: 5084 RVA: 0x000151E5 File Offset: 0x000133E5
		public void Set(object other)
		{
			this.Set(other as JoinLobbyCallbackInfoInternal?);
		}
	}
}
