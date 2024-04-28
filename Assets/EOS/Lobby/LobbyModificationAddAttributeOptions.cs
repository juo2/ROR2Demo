using System;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x0200034B RID: 843
	public class LobbyModificationAddAttributeOptions
	{
		// Token: 0x17000642 RID: 1602
		// (get) Token: 0x0600151F RID: 5407 RVA: 0x000170B0 File Offset: 0x000152B0
		// (set) Token: 0x06001520 RID: 5408 RVA: 0x000170B8 File Offset: 0x000152B8
		public AttributeData Attribute { get; set; }

		// Token: 0x17000643 RID: 1603
		// (get) Token: 0x06001521 RID: 5409 RVA: 0x000170C1 File Offset: 0x000152C1
		// (set) Token: 0x06001522 RID: 5410 RVA: 0x000170C9 File Offset: 0x000152C9
		public LobbyAttributeVisibility Visibility { get; set; }
	}
}
