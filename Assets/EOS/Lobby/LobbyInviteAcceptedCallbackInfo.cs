using System;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x02000341 RID: 833
	public class LobbyInviteAcceptedCallbackInfo : ICallbackInfo, ISettable
	{
		// Token: 0x1700061E RID: 1566
		// (get) Token: 0x060014D0 RID: 5328 RVA: 0x000169E4 File Offset: 0x00014BE4
		// (set) Token: 0x060014D1 RID: 5329 RVA: 0x000169EC File Offset: 0x00014BEC
		public object ClientData { get; private set; }

		// Token: 0x1700061F RID: 1567
		// (get) Token: 0x060014D2 RID: 5330 RVA: 0x000169F5 File Offset: 0x00014BF5
		// (set) Token: 0x060014D3 RID: 5331 RVA: 0x000169FD File Offset: 0x00014BFD
		public string InviteId { get; private set; }

		// Token: 0x17000620 RID: 1568
		// (get) Token: 0x060014D4 RID: 5332 RVA: 0x00016A06 File Offset: 0x00014C06
		// (set) Token: 0x060014D5 RID: 5333 RVA: 0x00016A0E File Offset: 0x00014C0E
		public ProductUserId LocalUserId { get; private set; }

		// Token: 0x17000621 RID: 1569
		// (get) Token: 0x060014D6 RID: 5334 RVA: 0x00016A17 File Offset: 0x00014C17
		// (set) Token: 0x060014D7 RID: 5335 RVA: 0x00016A1F File Offset: 0x00014C1F
		public ProductUserId TargetUserId { get; private set; }

		// Token: 0x17000622 RID: 1570
		// (get) Token: 0x060014D8 RID: 5336 RVA: 0x00016A28 File Offset: 0x00014C28
		// (set) Token: 0x060014D9 RID: 5337 RVA: 0x00016A30 File Offset: 0x00014C30
		public string LobbyId { get; private set; }

		// Token: 0x060014DA RID: 5338 RVA: 0x00016A3C File Offset: 0x00014C3C
		public Result? GetResultCode()
		{
			return null;
		}

		// Token: 0x060014DB RID: 5339 RVA: 0x00016A54 File Offset: 0x00014C54
		internal void Set(LobbyInviteAcceptedCallbackInfoInternal? other)
		{
			if (other != null)
			{
				this.ClientData = other.Value.ClientData;
				this.InviteId = other.Value.InviteId;
				this.LocalUserId = other.Value.LocalUserId;
				this.TargetUserId = other.Value.TargetUserId;
				this.LobbyId = other.Value.LobbyId;
			}
		}

		// Token: 0x060014DC RID: 5340 RVA: 0x00016AD3 File Offset: 0x00014CD3
		public void Set(object other)
		{
			this.Set(other as LobbyInviteAcceptedCallbackInfoInternal?);
		}
	}
}
