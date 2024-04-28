using System;

namespace Epic.OnlineServices.Mods
{
	// Token: 0x020002C4 RID: 708
	public class InstallModOptions
	{
		// Token: 0x17000531 RID: 1329
		// (get) Token: 0x060011F1 RID: 4593 RVA: 0x000131B4 File Offset: 0x000113B4
		// (set) Token: 0x060011F2 RID: 4594 RVA: 0x000131BC File Offset: 0x000113BC
		public EpicAccountId LocalUserId { get; set; }

		// Token: 0x17000532 RID: 1330
		// (get) Token: 0x060011F3 RID: 4595 RVA: 0x000131C5 File Offset: 0x000113C5
		// (set) Token: 0x060011F4 RID: 4596 RVA: 0x000131CD File Offset: 0x000113CD
		public ModIdentifier Mod { get; set; }

		// Token: 0x17000533 RID: 1331
		// (get) Token: 0x060011F5 RID: 4597 RVA: 0x000131D6 File Offset: 0x000113D6
		// (set) Token: 0x060011F6 RID: 4598 RVA: 0x000131DE File Offset: 0x000113DE
		public bool RemoveAfterExit { get; set; }
	}
}
