using System;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x020003A9 RID: 937
	public class UpdateLobbyCallbackInfo : ICallbackInfo, ISettable
	{
		// Token: 0x170006B3 RID: 1715
		// (get) Token: 0x060016D7 RID: 5847 RVA: 0x000182A4 File Offset: 0x000164A4
		// (set) Token: 0x060016D8 RID: 5848 RVA: 0x000182AC File Offset: 0x000164AC
		public Result ResultCode { get; private set; }

		// Token: 0x170006B4 RID: 1716
		// (get) Token: 0x060016D9 RID: 5849 RVA: 0x000182B5 File Offset: 0x000164B5
		// (set) Token: 0x060016DA RID: 5850 RVA: 0x000182BD File Offset: 0x000164BD
		public object ClientData { get; private set; }

		// Token: 0x170006B5 RID: 1717
		// (get) Token: 0x060016DB RID: 5851 RVA: 0x000182C6 File Offset: 0x000164C6
		// (set) Token: 0x060016DC RID: 5852 RVA: 0x000182CE File Offset: 0x000164CE
		public string LobbyId { get; private set; }

		// Token: 0x060016DD RID: 5853 RVA: 0x000182D7 File Offset: 0x000164D7
		public Result? GetResultCode()
		{
			return new Result?(this.ResultCode);
		}

		// Token: 0x060016DE RID: 5854 RVA: 0x000182E4 File Offset: 0x000164E4
		internal void Set(UpdateLobbyCallbackInfoInternal? other)
		{
			if (other != null)
			{
				this.ResultCode = other.Value.ResultCode;
				this.ClientData = other.Value.ClientData;
				this.LobbyId = other.Value.LobbyId;
			}
		}

		// Token: 0x060016DF RID: 5855 RVA: 0x00018339 File Offset: 0x00016539
		public void Set(object other)
		{
			this.Set(other as UpdateLobbyCallbackInfoInternal?);
		}
	}
}
