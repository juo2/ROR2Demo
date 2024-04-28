using System;

namespace Epic.OnlineServices.Mods
{
	// Token: 0x020002BC RID: 700
	public class CopyModInfoOptions
	{
		// Token: 0x17000517 RID: 1303
		// (get) Token: 0x060011BB RID: 4539 RVA: 0x00012E67 File Offset: 0x00011067
		// (set) Token: 0x060011BC RID: 4540 RVA: 0x00012E6F File Offset: 0x0001106F
		public EpicAccountId LocalUserId { get; set; }

		// Token: 0x17000518 RID: 1304
		// (get) Token: 0x060011BD RID: 4541 RVA: 0x00012E78 File Offset: 0x00011078
		// (set) Token: 0x060011BE RID: 4542 RVA: 0x00012E80 File Offset: 0x00011080
		public ModEnumerationType Type { get; set; }
	}
}
