using System;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x0200030A RID: 778
	public class CreateLobbyOptions
	{
		// Token: 0x1700059F RID: 1439
		// (get) Token: 0x06001360 RID: 4960 RVA: 0x00014AAC File Offset: 0x00012CAC
		// (set) Token: 0x06001361 RID: 4961 RVA: 0x00014AB4 File Offset: 0x00012CB4
		public ProductUserId LocalUserId { get; set; }

		// Token: 0x170005A0 RID: 1440
		// (get) Token: 0x06001362 RID: 4962 RVA: 0x00014ABD File Offset: 0x00012CBD
		// (set) Token: 0x06001363 RID: 4963 RVA: 0x00014AC5 File Offset: 0x00012CC5
		public uint MaxLobbyMembers { get; set; }

		// Token: 0x170005A1 RID: 1441
		// (get) Token: 0x06001364 RID: 4964 RVA: 0x00014ACE File Offset: 0x00012CCE
		// (set) Token: 0x06001365 RID: 4965 RVA: 0x00014AD6 File Offset: 0x00012CD6
		public LobbyPermissionLevel PermissionLevel { get; set; }

		// Token: 0x170005A2 RID: 1442
		// (get) Token: 0x06001366 RID: 4966 RVA: 0x00014ADF File Offset: 0x00012CDF
		// (set) Token: 0x06001367 RID: 4967 RVA: 0x00014AE7 File Offset: 0x00012CE7
		public bool PresenceEnabled { get; set; }

		// Token: 0x170005A3 RID: 1443
		// (get) Token: 0x06001368 RID: 4968 RVA: 0x00014AF0 File Offset: 0x00012CF0
		// (set) Token: 0x06001369 RID: 4969 RVA: 0x00014AF8 File Offset: 0x00012CF8
		public bool AllowInvites { get; set; }

		// Token: 0x170005A4 RID: 1444
		// (get) Token: 0x0600136A RID: 4970 RVA: 0x00014B01 File Offset: 0x00012D01
		// (set) Token: 0x0600136B RID: 4971 RVA: 0x00014B09 File Offset: 0x00012D09
		public string BucketId { get; set; }

		// Token: 0x170005A5 RID: 1445
		// (get) Token: 0x0600136C RID: 4972 RVA: 0x00014B12 File Offset: 0x00012D12
		// (set) Token: 0x0600136D RID: 4973 RVA: 0x00014B1A File Offset: 0x00012D1A
		public bool DisableHostMigration { get; set; }

		// Token: 0x170005A6 RID: 1446
		// (get) Token: 0x0600136E RID: 4974 RVA: 0x00014B23 File Offset: 0x00012D23
		// (set) Token: 0x0600136F RID: 4975 RVA: 0x00014B2B File Offset: 0x00012D2B
		public bool EnableRTCRoom { get; set; }

		// Token: 0x170005A7 RID: 1447
		// (get) Token: 0x06001370 RID: 4976 RVA: 0x00014B34 File Offset: 0x00012D34
		// (set) Token: 0x06001371 RID: 4977 RVA: 0x00014B3C File Offset: 0x00012D3C
		public LocalRTCOptions LocalRTCOptions { get; set; }

		// Token: 0x170005A8 RID: 1448
		// (get) Token: 0x06001372 RID: 4978 RVA: 0x00014B45 File Offset: 0x00012D45
		// (set) Token: 0x06001373 RID: 4979 RVA: 0x00014B4D File Offset: 0x00012D4D
		public string LobbyId { get; set; }
	}
}
