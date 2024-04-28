using System;

namespace RoR2.Achievements
{
	// Token: 0x02000E7F RID: 3711
	[RegisterAchievement("ChargeTeleporterWhileNearDeath", "Items.WarCryOnMultiKill", null, null)]
	public class ChargeTeleporterWhileNearDeathAchievement : BaseAchievement
	{
		// Token: 0x060054F8 RID: 21752 RVA: 0x0015D8B0 File Offset: 0x0015BAB0
		public override void OnInstall()
		{
			base.OnInstall();
			TeleporterInteraction.onTeleporterChargedGlobal += this.OnTeleporterCharged;
		}

		// Token: 0x060054F9 RID: 21753 RVA: 0x0015D8C9 File Offset: 0x0015BAC9
		public override void OnUninstall()
		{
			TeleporterInteraction.onTeleporterChargedGlobal -= this.OnTeleporterCharged;
			base.OnUninstall();
		}

		// Token: 0x060054FA RID: 21754 RVA: 0x0015D8E2 File Offset: 0x0015BAE2
		private void OnTeleporterCharged(TeleporterInteraction teleporterInteraction)
		{
			this.Check();
		}

		// Token: 0x060054FB RID: 21755 RVA: 0x0015D8EC File Offset: 0x0015BAEC
		private void Check()
		{
			if (base.localUser.cachedBody && base.localUser.cachedBody.healthComponent && base.localUser.cachedBody.healthComponent.alive && base.localUser.cachedBody.healthComponent.combinedHealthFraction <= 0.1f)
			{
				base.Grant();
			}
		}

		// Token: 0x0400504C RID: 20556
		private const float requirement = 0.1f;
	}
}
