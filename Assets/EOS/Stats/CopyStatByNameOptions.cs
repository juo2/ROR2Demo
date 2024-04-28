using System;

namespace Epic.OnlineServices.Stats
{
	// Token: 0x02000090 RID: 144
	public class CopyStatByNameOptions
	{
		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x06000545 RID: 1349 RVA: 0x000063F5 File Offset: 0x000045F5
		// (set) Token: 0x06000546 RID: 1350 RVA: 0x000063FD File Offset: 0x000045FD
		public ProductUserId TargetUserId { get; set; }

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x06000547 RID: 1351 RVA: 0x00006406 File Offset: 0x00004606
		// (set) Token: 0x06000548 RID: 1352 RVA: 0x0000640E File Offset: 0x0000460E
		public string Name { get; set; }
	}
}
