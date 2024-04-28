using System;

namespace RoR2
{
	// Token: 0x020009B5 RID: 2485
	[Flags]
	public enum MPFeatures
	{
		// Token: 0x0400387C RID: 14460
		None = 0,
		// Token: 0x0400387D RID: 14461
		HostGame = 1,
		// Token: 0x0400387E RID: 14462
		FindGame = 2,
		// Token: 0x0400387F RID: 14463
		Quickplay = 4,
		// Token: 0x04003880 RID: 14464
		PrivateGame = 8,
		// Token: 0x04003881 RID: 14465
		Invite = 16,
		// Token: 0x04003882 RID: 14466
		JoinViaID = 32,
		// Token: 0x04003883 RID: 14467
		EnterGameButton = 64,
		// Token: 0x04003884 RID: 14468
		AdHoc = 128,
		// Token: 0x04003885 RID: 14469
		All = -1
	}
}
