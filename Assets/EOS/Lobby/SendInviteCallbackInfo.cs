using System;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x020003A5 RID: 933
	public class SendInviteCallbackInfo : ICallbackInfo, ISettable
	{
		// Token: 0x170006A6 RID: 1702
		// (get) Token: 0x060016BC RID: 5820 RVA: 0x000180F0 File Offset: 0x000162F0
		// (set) Token: 0x060016BD RID: 5821 RVA: 0x000180F8 File Offset: 0x000162F8
		public Result ResultCode { get; private set; }

		// Token: 0x170006A7 RID: 1703
		// (get) Token: 0x060016BE RID: 5822 RVA: 0x00018101 File Offset: 0x00016301
		// (set) Token: 0x060016BF RID: 5823 RVA: 0x00018109 File Offset: 0x00016309
		public object ClientData { get; private set; }

		// Token: 0x170006A8 RID: 1704
		// (get) Token: 0x060016C0 RID: 5824 RVA: 0x00018112 File Offset: 0x00016312
		// (set) Token: 0x060016C1 RID: 5825 RVA: 0x0001811A File Offset: 0x0001631A
		public string LobbyId { get; private set; }

		// Token: 0x060016C2 RID: 5826 RVA: 0x00018123 File Offset: 0x00016323
		public Result? GetResultCode()
		{
			return new Result?(this.ResultCode);
		}

		// Token: 0x060016C3 RID: 5827 RVA: 0x00018130 File Offset: 0x00016330
		internal void Set(SendInviteCallbackInfoInternal? other)
		{
			if (other != null)
			{
				this.ResultCode = other.Value.ResultCode;
				this.ClientData = other.Value.ClientData;
				this.LobbyId = other.Value.LobbyId;
			}
		}

		// Token: 0x060016C4 RID: 5828 RVA: 0x00018185 File Offset: 0x00016385
		public void Set(object other)
		{
			this.Set(other as SendInviteCallbackInfoInternal?);
		}
	}
}
