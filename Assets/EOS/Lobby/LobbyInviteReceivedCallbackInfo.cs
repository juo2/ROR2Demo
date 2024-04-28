using System;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x02000343 RID: 835
	public class LobbyInviteReceivedCallbackInfo : ICallbackInfo, ISettable
	{
		// Token: 0x17000629 RID: 1577
		// (get) Token: 0x060014E4 RID: 5348 RVA: 0x00016B7C File Offset: 0x00014D7C
		// (set) Token: 0x060014E5 RID: 5349 RVA: 0x00016B84 File Offset: 0x00014D84
		public object ClientData { get; private set; }

		// Token: 0x1700062A RID: 1578
		// (get) Token: 0x060014E6 RID: 5350 RVA: 0x00016B8D File Offset: 0x00014D8D
		// (set) Token: 0x060014E7 RID: 5351 RVA: 0x00016B95 File Offset: 0x00014D95
		public string InviteId { get; private set; }

		// Token: 0x1700062B RID: 1579
		// (get) Token: 0x060014E8 RID: 5352 RVA: 0x00016B9E File Offset: 0x00014D9E
		// (set) Token: 0x060014E9 RID: 5353 RVA: 0x00016BA6 File Offset: 0x00014DA6
		public ProductUserId LocalUserId { get; private set; }

		// Token: 0x1700062C RID: 1580
		// (get) Token: 0x060014EA RID: 5354 RVA: 0x00016BAF File Offset: 0x00014DAF
		// (set) Token: 0x060014EB RID: 5355 RVA: 0x00016BB7 File Offset: 0x00014DB7
		public ProductUserId TargetUserId { get; private set; }

		// Token: 0x060014EC RID: 5356 RVA: 0x00016BC0 File Offset: 0x00014DC0
		public Result? GetResultCode()
		{
			return null;
		}

		// Token: 0x060014ED RID: 5357 RVA: 0x00016BD8 File Offset: 0x00014DD8
		internal void Set(LobbyInviteReceivedCallbackInfoInternal? other)
		{
			if (other != null)
			{
				this.ClientData = other.Value.ClientData;
				this.InviteId = other.Value.InviteId;
				this.LocalUserId = other.Value.LocalUserId;
				this.TargetUserId = other.Value.TargetUserId;
			}
		}

		// Token: 0x060014EE RID: 5358 RVA: 0x00016C42 File Offset: 0x00014E42
		public void Set(object other)
		{
			this.Set(other as LobbyInviteReceivedCallbackInfoInternal?);
		}
	}
}
