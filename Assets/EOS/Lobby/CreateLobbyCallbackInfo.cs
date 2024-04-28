using System;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x02000308 RID: 776
	public class CreateLobbyCallbackInfo : ICallbackInfo, ISettable
	{
		// Token: 0x17000598 RID: 1432
		// (get) Token: 0x06001352 RID: 4946 RVA: 0x000149BC File Offset: 0x00012BBC
		// (set) Token: 0x06001353 RID: 4947 RVA: 0x000149C4 File Offset: 0x00012BC4
		public Result ResultCode { get; private set; }

		// Token: 0x17000599 RID: 1433
		// (get) Token: 0x06001354 RID: 4948 RVA: 0x000149CD File Offset: 0x00012BCD
		// (set) Token: 0x06001355 RID: 4949 RVA: 0x000149D5 File Offset: 0x00012BD5
		public object ClientData { get; private set; }

		// Token: 0x1700059A RID: 1434
		// (get) Token: 0x06001356 RID: 4950 RVA: 0x000149DE File Offset: 0x00012BDE
		// (set) Token: 0x06001357 RID: 4951 RVA: 0x000149E6 File Offset: 0x00012BE6
		public string LobbyId { get; private set; }

		// Token: 0x06001358 RID: 4952 RVA: 0x000149EF File Offset: 0x00012BEF
		public Result? GetResultCode()
		{
			return new Result?(this.ResultCode);
		}

		// Token: 0x06001359 RID: 4953 RVA: 0x000149FC File Offset: 0x00012BFC
		internal void Set(CreateLobbyCallbackInfoInternal? other)
		{
			if (other != null)
			{
				this.ResultCode = other.Value.ResultCode;
				this.ClientData = other.Value.ClientData;
				this.LobbyId = other.Value.LobbyId;
			}
		}

		// Token: 0x0600135A RID: 4954 RVA: 0x00014A51 File Offset: 0x00012C51
		public void Set(object other)
		{
			this.Set(other as CreateLobbyCallbackInfoInternal?);
		}
	}
}
