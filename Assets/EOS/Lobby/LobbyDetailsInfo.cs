using System;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x0200033E RID: 830
	public class LobbyDetailsInfo : ISettable
	{
		// Token: 0x1700060C RID: 1548
		// (get) Token: 0x06001472 RID: 5234 RVA: 0x00015B73 File Offset: 0x00013D73
		// (set) Token: 0x06001473 RID: 5235 RVA: 0x00015B7B File Offset: 0x00013D7B
		public string LobbyId { get; set; }

		// Token: 0x1700060D RID: 1549
		// (get) Token: 0x06001474 RID: 5236 RVA: 0x00015B84 File Offset: 0x00013D84
		// (set) Token: 0x06001475 RID: 5237 RVA: 0x00015B8C File Offset: 0x00013D8C
		public ProductUserId LobbyOwnerUserId { get; set; }

		// Token: 0x1700060E RID: 1550
		// (get) Token: 0x06001476 RID: 5238 RVA: 0x00015B95 File Offset: 0x00013D95
		// (set) Token: 0x06001477 RID: 5239 RVA: 0x00015B9D File Offset: 0x00013D9D
		public LobbyPermissionLevel PermissionLevel { get; set; }

		// Token: 0x1700060F RID: 1551
		// (get) Token: 0x06001478 RID: 5240 RVA: 0x00015BA6 File Offset: 0x00013DA6
		// (set) Token: 0x06001479 RID: 5241 RVA: 0x00015BAE File Offset: 0x00013DAE
		public uint AvailableSlots { get; set; }

		// Token: 0x17000610 RID: 1552
		// (get) Token: 0x0600147A RID: 5242 RVA: 0x00015BB7 File Offset: 0x00013DB7
		// (set) Token: 0x0600147B RID: 5243 RVA: 0x00015BBF File Offset: 0x00013DBF
		public uint MaxMembers { get; set; }

		// Token: 0x17000611 RID: 1553
		// (get) Token: 0x0600147C RID: 5244 RVA: 0x00015BC8 File Offset: 0x00013DC8
		// (set) Token: 0x0600147D RID: 5245 RVA: 0x00015BD0 File Offset: 0x00013DD0
		public bool AllowInvites { get; set; }

		// Token: 0x17000612 RID: 1554
		// (get) Token: 0x0600147E RID: 5246 RVA: 0x00015BD9 File Offset: 0x00013DD9
		// (set) Token: 0x0600147F RID: 5247 RVA: 0x00015BE1 File Offset: 0x00013DE1
		public string BucketId { get; set; }

		// Token: 0x17000613 RID: 1555
		// (get) Token: 0x06001480 RID: 5248 RVA: 0x00015BEA File Offset: 0x00013DEA
		// (set) Token: 0x06001481 RID: 5249 RVA: 0x00015BF2 File Offset: 0x00013DF2
		public bool AllowHostMigration { get; set; }

		// Token: 0x17000614 RID: 1556
		// (get) Token: 0x06001482 RID: 5250 RVA: 0x00015BFB File Offset: 0x00013DFB
		// (set) Token: 0x06001483 RID: 5251 RVA: 0x00015C03 File Offset: 0x00013E03
		public bool RTCRoomEnabled { get; set; }

		// Token: 0x06001484 RID: 5252 RVA: 0x00015C0C File Offset: 0x00013E0C
		internal void Set(LobbyDetailsInfoInternal? other)
		{
			if (other != null)
			{
				this.LobbyId = other.Value.LobbyId;
				this.LobbyOwnerUserId = other.Value.LobbyOwnerUserId;
				this.PermissionLevel = other.Value.PermissionLevel;
				this.AvailableSlots = other.Value.AvailableSlots;
				this.MaxMembers = other.Value.MaxMembers;
				this.AllowInvites = other.Value.AllowInvites;
				this.BucketId = other.Value.BucketId;
				this.AllowHostMigration = other.Value.AllowHostMigration;
				this.RTCRoomEnabled = other.Value.RTCRoomEnabled;
			}
		}

		// Token: 0x06001485 RID: 5253 RVA: 0x00015CE2 File Offset: 0x00013EE2
		public void Set(object other)
		{
			this.Set(other as LobbyDetailsInfoInternal?);
		}
	}
}
