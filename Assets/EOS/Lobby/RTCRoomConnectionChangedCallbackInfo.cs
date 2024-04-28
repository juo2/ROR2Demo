using System;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x0200039F RID: 927
	public class RTCRoomConnectionChangedCallbackInfo : ICallbackInfo, ISettable
	{
		// Token: 0x17000690 RID: 1680
		// (get) Token: 0x06001690 RID: 5776 RVA: 0x00017DF0 File Offset: 0x00015FF0
		// (set) Token: 0x06001691 RID: 5777 RVA: 0x00017DF8 File Offset: 0x00015FF8
		public object ClientData { get; private set; }

		// Token: 0x17000691 RID: 1681
		// (get) Token: 0x06001692 RID: 5778 RVA: 0x00017E01 File Offset: 0x00016001
		// (set) Token: 0x06001693 RID: 5779 RVA: 0x00017E09 File Offset: 0x00016009
		public string LobbyId { get; private set; }

		// Token: 0x17000692 RID: 1682
		// (get) Token: 0x06001694 RID: 5780 RVA: 0x00017E12 File Offset: 0x00016012
		// (set) Token: 0x06001695 RID: 5781 RVA: 0x00017E1A File Offset: 0x0001601A
		public ProductUserId LocalUserId { get; private set; }

		// Token: 0x17000693 RID: 1683
		// (get) Token: 0x06001696 RID: 5782 RVA: 0x00017E23 File Offset: 0x00016023
		// (set) Token: 0x06001697 RID: 5783 RVA: 0x00017E2B File Offset: 0x0001602B
		public bool IsConnected { get; private set; }

		// Token: 0x17000694 RID: 1684
		// (get) Token: 0x06001698 RID: 5784 RVA: 0x00017E34 File Offset: 0x00016034
		// (set) Token: 0x06001699 RID: 5785 RVA: 0x00017E3C File Offset: 0x0001603C
		public Result DisconnectReason { get; private set; }

		// Token: 0x0600169A RID: 5786 RVA: 0x00017E48 File Offset: 0x00016048
		public Result? GetResultCode()
		{
			return null;
		}

		// Token: 0x0600169B RID: 5787 RVA: 0x00017E60 File Offset: 0x00016060
		internal void Set(RTCRoomConnectionChangedCallbackInfoInternal? other)
		{
			if (other != null)
			{
				this.ClientData = other.Value.ClientData;
				this.LobbyId = other.Value.LobbyId;
				this.LocalUserId = other.Value.LocalUserId;
				this.IsConnected = other.Value.IsConnected;
				this.DisconnectReason = other.Value.DisconnectReason;
			}
		}

		// Token: 0x0600169C RID: 5788 RVA: 0x00017EDF File Offset: 0x000160DF
		public void Set(object other)
		{
			this.Set(other as RTCRoomConnectionChangedCallbackInfoInternal?);
		}
	}
}
