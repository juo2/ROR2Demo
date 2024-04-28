using System;

namespace Epic.OnlineServices.Mods
{
	// Token: 0x020002C0 RID: 704
	public class EnumerateModsOptions
	{
		// Token: 0x17000524 RID: 1316
		// (get) Token: 0x060011D6 RID: 4566 RVA: 0x00013004 File Offset: 0x00011204
		// (set) Token: 0x060011D7 RID: 4567 RVA: 0x0001300C File Offset: 0x0001120C
		public EpicAccountId LocalUserId { get; set; }

		// Token: 0x17000525 RID: 1317
		// (get) Token: 0x060011D8 RID: 4568 RVA: 0x00013015 File Offset: 0x00011215
		// (set) Token: 0x060011D9 RID: 4569 RVA: 0x0001301D File Offset: 0x0001121D
		public ModEnumerationType Type { get; set; }
	}
}
