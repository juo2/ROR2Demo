using System;
using RoR2.Stats;

namespace RoR2.Achievements
{
	// Token: 0x02000EBC RID: 3772
	[RegisterAchievement("RepeatFirstTeleporter", "Characters.Toolbot", null, typeof(RepeatFirstTeleporterAchievement.RepeatFirstTeleporterServerAchievement))]
	public class RepeatFirstTeleporterAchievement : BaseAchievement
	{
		// Token: 0x060055F8 RID: 22008 RVA: 0x0015F087 File Offset: 0x0015D287
		public override void OnInstall()
		{
			base.OnInstall();
			UserProfile userProfile = base.userProfile;
			userProfile.onStatsReceived = (Action)Delegate.Combine(userProfile.onStatsReceived, new Action(this.Check));
			base.SetServerTracked(true);
		}

		// Token: 0x060055F9 RID: 22009 RVA: 0x0015F0BD File Offset: 0x0015D2BD
		public override void OnUninstall()
		{
			UserProfile userProfile = base.userProfile;
			userProfile.onStatsReceived = (Action)Delegate.Remove(userProfile.onStatsReceived, new Action(this.Check));
			base.OnUninstall();
		}

		// Token: 0x060055FA RID: 22010 RVA: 0x0015F0EC File Offset: 0x0015D2EC
		private void Check()
		{
			if (base.userProfile.statSheet.GetStatValueULong(StatDef.firstTeleporterCompleted) >= 5UL)
			{
				base.Grant();
			}
		}

		// Token: 0x060055FB RID: 22011 RVA: 0x0015F10D File Offset: 0x0015D30D
		public override float ProgressForAchievement()
		{
			return base.userProfile.statSheet.GetStatValueULong(StatDef.firstTeleporterCompleted) / 5f;
		}

		// Token: 0x04005073 RID: 20595
		private const int requirement = 5;

		// Token: 0x02000EBD RID: 3773
		private class RepeatFirstTeleporterServerAchievement : BaseServerAchievement
		{
			// Token: 0x060055FD RID: 22013 RVA: 0x0015F12D File Offset: 0x0015D32D
			public override void OnInstall()
			{
				base.OnInstall();
				TeleporterInteraction.onTeleporterChargedGlobal += this.OnTeleporterCharged;
			}

			// Token: 0x060055FE RID: 22014 RVA: 0x0015F146 File Offset: 0x0015D346
			public override void OnUninstall()
			{
				TeleporterInteraction.onTeleporterChargedGlobal -= this.OnTeleporterCharged;
				base.OnUninstall();
			}

			// Token: 0x060055FF RID: 22015 RVA: 0x0015F160 File Offset: 0x0015D360
			private void OnTeleporterCharged(TeleporterInteraction teleporterInteraction)
			{
				SceneCatalog.GetSceneDefForCurrentScene();
				StatSheet currentStats = base.networkUser.masterPlayerStatsComponent.currentStats;
				if (Run.instance && Run.instance.stageClearCount == 0)
				{
					PlayerStatsComponent masterPlayerStatsComponent = base.networkUser.masterPlayerStatsComponent;
					if (masterPlayerStatsComponent)
					{
						masterPlayerStatsComponent.currentStats.PushStatValue(StatDef.firstTeleporterCompleted, 1UL);
					}
				}
			}
		}
	}
}
