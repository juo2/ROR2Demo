using System;

namespace RoR2
{
	// Token: 0x020009B6 RID: 2486
	[Flags]
	public enum MPLobbyFeatures
	{
		// Token: 0x04003887 RID: 14471
		None = 0,
		// Token: 0x04003888 RID: 14472
		CreateLobby = 1,
		// Token: 0x04003889 RID: 14473
		SocialIcon = 2,
		// Token: 0x0400388A RID: 14474
		HostPromotion = 4,
		// Token: 0x0400388B RID: 14475
		Clipboard = 8,
		// Token: 0x0400388C RID: 14476
		Invite = 16,
		// Token: 0x0400388D RID: 14477
		UserIcon = 32,
		// Token: 0x0400388E RID: 14478
		LeaveLobby = 64,
		// Token: 0x0400388F RID: 14479
		LobbyDropdownOptions = 128,
		// Token: 0x04003890 RID: 14480
		All = -1
	}
}
