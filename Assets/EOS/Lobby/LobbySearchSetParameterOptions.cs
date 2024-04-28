using System;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x0200036D RID: 877
	public class LobbySearchSetParameterOptions
	{
		// Token: 0x17000667 RID: 1639
		// (get) Token: 0x060015A6 RID: 5542 RVA: 0x000177B8 File Offset: 0x000159B8
		// (set) Token: 0x060015A7 RID: 5543 RVA: 0x000177C0 File Offset: 0x000159C0
		public AttributeData Parameter { get; set; }

		// Token: 0x17000668 RID: 1640
		// (get) Token: 0x060015A8 RID: 5544 RVA: 0x000177C9 File Offset: 0x000159C9
		// (set) Token: 0x060015A9 RID: 5545 RVA: 0x000177D1 File Offset: 0x000159D1
		public ComparisonOp ComparisonOp { get; set; }
	}
}
