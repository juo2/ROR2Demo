using System;

namespace Epic.OnlineServices.Sanctions
{
	// Token: 0x0200014F RID: 335
	public class CopyPlayerSanctionByIndexOptions
	{
		// Token: 0x17000224 RID: 548
		// (get) Token: 0x0600093D RID: 2365 RVA: 0x0000A4B0 File Offset: 0x000086B0
		// (set) Token: 0x0600093E RID: 2366 RVA: 0x0000A4B8 File Offset: 0x000086B8
		public ProductUserId TargetUserId { get; set; }

		// Token: 0x17000225 RID: 549
		// (get) Token: 0x0600093F RID: 2367 RVA: 0x0000A4C1 File Offset: 0x000086C1
		// (set) Token: 0x06000940 RID: 2368 RVA: 0x0000A4C9 File Offset: 0x000086C9
		public uint SanctionIndex { get; set; }
	}
}
