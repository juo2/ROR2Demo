using System;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x020000C0 RID: 192
	public class CreateSessionModificationOptions
	{
		// Token: 0x17000144 RID: 324
		// (get) Token: 0x06000652 RID: 1618 RVA: 0x0000765C File Offset: 0x0000585C
		// (set) Token: 0x06000653 RID: 1619 RVA: 0x00007664 File Offset: 0x00005864
		public string SessionName { get; set; }

		// Token: 0x17000145 RID: 325
		// (get) Token: 0x06000654 RID: 1620 RVA: 0x0000766D File Offset: 0x0000586D
		// (set) Token: 0x06000655 RID: 1621 RVA: 0x00007675 File Offset: 0x00005875
		public string BucketId { get; set; }

		// Token: 0x17000146 RID: 326
		// (get) Token: 0x06000656 RID: 1622 RVA: 0x0000767E File Offset: 0x0000587E
		// (set) Token: 0x06000657 RID: 1623 RVA: 0x00007686 File Offset: 0x00005886
		public uint MaxPlayers { get; set; }

		// Token: 0x17000147 RID: 327
		// (get) Token: 0x06000658 RID: 1624 RVA: 0x0000768F File Offset: 0x0000588F
		// (set) Token: 0x06000659 RID: 1625 RVA: 0x00007697 File Offset: 0x00005897
		public ProductUserId LocalUserId { get; set; }

		// Token: 0x17000148 RID: 328
		// (get) Token: 0x0600065A RID: 1626 RVA: 0x000076A0 File Offset: 0x000058A0
		// (set) Token: 0x0600065B RID: 1627 RVA: 0x000076A8 File Offset: 0x000058A8
		public bool PresenceEnabled { get; set; }

		// Token: 0x17000149 RID: 329
		// (get) Token: 0x0600065C RID: 1628 RVA: 0x000076B1 File Offset: 0x000058B1
		// (set) Token: 0x0600065D RID: 1629 RVA: 0x000076B9 File Offset: 0x000058B9
		public string SessionId { get; set; }

		// Token: 0x1700014A RID: 330
		// (get) Token: 0x0600065E RID: 1630 RVA: 0x000076C2 File Offset: 0x000058C2
		// (set) Token: 0x0600065F RID: 1631 RVA: 0x000076CA File Offset: 0x000058CA
		public bool SanctionsEnabled { get; set; }
	}
}
