using System;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x02000346 RID: 838
	public class LobbyMemberStatusReceivedCallbackInfo : ICallbackInfo, ISettable
	{
		// Token: 0x17000632 RID: 1586
		// (get) Token: 0x060014F5 RID: 5365 RVA: 0x00016CD0 File Offset: 0x00014ED0
		// (set) Token: 0x060014F6 RID: 5366 RVA: 0x00016CD8 File Offset: 0x00014ED8
		public object ClientData { get; private set; }

		// Token: 0x17000633 RID: 1587
		// (get) Token: 0x060014F7 RID: 5367 RVA: 0x00016CE1 File Offset: 0x00014EE1
		// (set) Token: 0x060014F8 RID: 5368 RVA: 0x00016CE9 File Offset: 0x00014EE9
		public string LobbyId { get; private set; }

		// Token: 0x17000634 RID: 1588
		// (get) Token: 0x060014F9 RID: 5369 RVA: 0x00016CF2 File Offset: 0x00014EF2
		// (set) Token: 0x060014FA RID: 5370 RVA: 0x00016CFA File Offset: 0x00014EFA
		public ProductUserId TargetUserId { get; private set; }

		// Token: 0x17000635 RID: 1589
		// (get) Token: 0x060014FB RID: 5371 RVA: 0x00016D03 File Offset: 0x00014F03
		// (set) Token: 0x060014FC RID: 5372 RVA: 0x00016D0B File Offset: 0x00014F0B
		public LobbyMemberStatus CurrentStatus { get; private set; }

		// Token: 0x060014FD RID: 5373 RVA: 0x00016D14 File Offset: 0x00014F14
		public Result? GetResultCode()
		{
			return null;
		}

		// Token: 0x060014FE RID: 5374 RVA: 0x00016D2C File Offset: 0x00014F2C
		internal void Set(LobbyMemberStatusReceivedCallbackInfoInternal? other)
		{
			if (other != null)
			{
				this.ClientData = other.Value.ClientData;
				this.LobbyId = other.Value.LobbyId;
				this.TargetUserId = other.Value.TargetUserId;
				this.CurrentStatus = other.Value.CurrentStatus;
			}
		}

		// Token: 0x060014FF RID: 5375 RVA: 0x00016D96 File Offset: 0x00014F96
		public void Set(object other)
		{
			this.Set(other as LobbyMemberStatusReceivedCallbackInfoInternal?);
		}
	}
}
