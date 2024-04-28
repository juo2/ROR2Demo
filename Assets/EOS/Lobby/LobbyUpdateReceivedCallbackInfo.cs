using System;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x02000371 RID: 881
	public class LobbyUpdateReceivedCallbackInfo : ICallbackInfo, ISettable
	{
		// Token: 0x1700066D RID: 1645
		// (get) Token: 0x060015B7 RID: 5559 RVA: 0x00017886 File Offset: 0x00015A86
		// (set) Token: 0x060015B8 RID: 5560 RVA: 0x0001788E File Offset: 0x00015A8E
		public object ClientData { get; private set; }

		// Token: 0x1700066E RID: 1646
		// (get) Token: 0x060015B9 RID: 5561 RVA: 0x00017897 File Offset: 0x00015A97
		// (set) Token: 0x060015BA RID: 5562 RVA: 0x0001789F File Offset: 0x00015A9F
		public string LobbyId { get; private set; }

		// Token: 0x060015BB RID: 5563 RVA: 0x000178A8 File Offset: 0x00015AA8
		public Result? GetResultCode()
		{
			return null;
		}

		// Token: 0x060015BC RID: 5564 RVA: 0x000178C0 File Offset: 0x00015AC0
		internal void Set(LobbyUpdateReceivedCallbackInfoInternal? other)
		{
			if (other != null)
			{
				this.ClientData = other.Value.ClientData;
				this.LobbyId = other.Value.LobbyId;
			}
		}

		// Token: 0x060015BD RID: 5565 RVA: 0x00017900 File Offset: 0x00015B00
		public void Set(object other)
		{
			this.Set(other as LobbyUpdateReceivedCallbackInfoInternal?);
		}
	}
}
