using System;

namespace RoR2
{
	// Token: 0x020005BA RID: 1466
	public static class FriendlyFireManager
	{
		// Token: 0x170001C0 RID: 448
		// (get) Token: 0x06001A8C RID: 6796 RVA: 0x00071F65 File Offset: 0x00070165
		// (set) Token: 0x06001A8D RID: 6797 RVA: 0x00071F6C File Offset: 0x0007016C
		public static float friendlyFireDamageScale { get; private set; } = 0.5f;

		// Token: 0x06001A8E RID: 6798 RVA: 0x00071F74 File Offset: 0x00070174
		public static bool ShouldSplashHitProceed(HealthComponent victim, TeamIndex attackerTeamIndex)
		{
			return victim.body.teamComponent.teamIndex != attackerTeamIndex || FriendlyFireManager.friendlyFireMode != FriendlyFireManager.FriendlyFireMode.Off || attackerTeamIndex == TeamIndex.None;
		}

		// Token: 0x06001A8F RID: 6799 RVA: 0x00071F74 File Offset: 0x00070174
		public static bool ShouldDirectHitProceed(HealthComponent victim, TeamIndex attackerTeamIndex)
		{
			return victim.body.teamComponent.teamIndex != attackerTeamIndex || FriendlyFireManager.friendlyFireMode != FriendlyFireManager.FriendlyFireMode.Off || attackerTeamIndex == TeamIndex.None;
		}

		// Token: 0x06001A90 RID: 6800 RVA: 0x00071F96 File Offset: 0x00070196
		public static bool ShouldSeekingProceed(HealthComponent victim, TeamIndex attackerTeamIndex)
		{
			return victim.body.teamComponent.teamIndex != attackerTeamIndex || FriendlyFireManager.friendlyFireMode == FriendlyFireManager.FriendlyFireMode.FreeForAll || attackerTeamIndex == TeamIndex.None;
		}

		// Token: 0x0400209D RID: 8349
		public static FriendlyFireManager.FriendlyFireMode friendlyFireMode = FriendlyFireManager.FriendlyFireMode.Off;

		// Token: 0x020005BB RID: 1467
		public enum FriendlyFireMode
		{
			// Token: 0x0400209F RID: 8351
			Off,
			// Token: 0x040020A0 RID: 8352
			FriendlyFire,
			// Token: 0x040020A1 RID: 8353
			FreeForAll
		}
	}
}
