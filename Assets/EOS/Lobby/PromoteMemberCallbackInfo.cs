using System;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x02000397 RID: 919
	public class PromoteMemberCallbackInfo : ICallbackInfo, ISettable
	{
		// Token: 0x1700067A RID: 1658
		// (get) Token: 0x06001660 RID: 5728 RVA: 0x00017AF5 File Offset: 0x00015CF5
		// (set) Token: 0x06001661 RID: 5729 RVA: 0x00017AFD File Offset: 0x00015CFD
		public Result ResultCode { get; private set; }

		// Token: 0x1700067B RID: 1659
		// (get) Token: 0x06001662 RID: 5730 RVA: 0x00017B06 File Offset: 0x00015D06
		// (set) Token: 0x06001663 RID: 5731 RVA: 0x00017B0E File Offset: 0x00015D0E
		public object ClientData { get; private set; }

		// Token: 0x1700067C RID: 1660
		// (get) Token: 0x06001664 RID: 5732 RVA: 0x00017B17 File Offset: 0x00015D17
		// (set) Token: 0x06001665 RID: 5733 RVA: 0x00017B1F File Offset: 0x00015D1F
		public string LobbyId { get; private set; }

		// Token: 0x06001666 RID: 5734 RVA: 0x00017B28 File Offset: 0x00015D28
		public Result? GetResultCode()
		{
			return new Result?(this.ResultCode);
		}

		// Token: 0x06001667 RID: 5735 RVA: 0x00017B38 File Offset: 0x00015D38
		internal void Set(PromoteMemberCallbackInfoInternal? other)
		{
			if (other != null)
			{
				this.ResultCode = other.Value.ResultCode;
				this.ClientData = other.Value.ClientData;
				this.LobbyId = other.Value.LobbyId;
			}
		}

		// Token: 0x06001668 RID: 5736 RVA: 0x00017B8D File Offset: 0x00015D8D
		public void Set(object other)
		{
			this.Set(other as PromoteMemberCallbackInfoInternal?);
		}
	}
}
