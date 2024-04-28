using System;

namespace Epic.OnlineServices.AntiCheatCommon
{
	// Token: 0x0200058D RID: 1421
	public class LogPlayerReviveOptions
	{
		// Token: 0x17000A88 RID: 2696
		// (get) Token: 0x06002231 RID: 8753 RVA: 0x0002435E File Offset: 0x0002255E
		// (set) Token: 0x06002232 RID: 8754 RVA: 0x00024366 File Offset: 0x00022566
		public IntPtr RevivedPlayerHandle { get; set; }

		// Token: 0x17000A89 RID: 2697
		// (get) Token: 0x06002233 RID: 8755 RVA: 0x0002436F File Offset: 0x0002256F
		// (set) Token: 0x06002234 RID: 8756 RVA: 0x00024377 File Offset: 0x00022577
		public IntPtr ReviverPlayerHandle { get; set; }
	}
}
