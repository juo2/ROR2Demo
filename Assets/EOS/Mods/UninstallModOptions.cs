using System;

namespace Epic.OnlineServices.Mods
{
	// Token: 0x020002D6 RID: 726
	public class UninstallModOptions
	{
		// Token: 0x1700054E RID: 1358
		// (get) Token: 0x06001262 RID: 4706 RVA: 0x00013924 File Offset: 0x00011B24
		// (set) Token: 0x06001263 RID: 4707 RVA: 0x0001392C File Offset: 0x00011B2C
		public EpicAccountId LocalUserId { get; set; }

		// Token: 0x1700054F RID: 1359
		// (get) Token: 0x06001264 RID: 4708 RVA: 0x00013935 File Offset: 0x00011B35
		// (set) Token: 0x06001265 RID: 4709 RVA: 0x0001393D File Offset: 0x00011B3D
		public ModIdentifier Mod { get; set; }
	}
}
