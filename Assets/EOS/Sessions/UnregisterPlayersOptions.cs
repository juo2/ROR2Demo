using System;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x02000147 RID: 327
	public class UnregisterPlayersOptions
	{
		// Token: 0x17000213 RID: 531
		// (get) Token: 0x06000914 RID: 2324 RVA: 0x0000A23E File Offset: 0x0000843E
		// (set) Token: 0x06000915 RID: 2325 RVA: 0x0000A246 File Offset: 0x00008446
		public string SessionName { get; set; }

		// Token: 0x17000214 RID: 532
		// (get) Token: 0x06000916 RID: 2326 RVA: 0x0000A24F File Offset: 0x0000844F
		// (set) Token: 0x06000917 RID: 2327 RVA: 0x0000A257 File Offset: 0x00008457
		public ProductUserId[] PlayersToUnregister { get; set; }
	}
}
