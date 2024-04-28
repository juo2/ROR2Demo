using System;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x02000367 RID: 871
	public class LobbySearchRemoveParameterOptions
	{
		// Token: 0x1700065F RID: 1631
		// (get) Token: 0x0600158E RID: 5518 RVA: 0x000176AA File Offset: 0x000158AA
		// (set) Token: 0x0600158F RID: 5519 RVA: 0x000176B2 File Offset: 0x000158B2
		public string Key { get; set; }

		// Token: 0x17000660 RID: 1632
		// (get) Token: 0x06001590 RID: 5520 RVA: 0x000176BB File Offset: 0x000158BB
		// (set) Token: 0x06001591 RID: 5521 RVA: 0x000176C3 File Offset: 0x000158C3
		public ComparisonOp ComparisonOp { get; set; }
	}
}
