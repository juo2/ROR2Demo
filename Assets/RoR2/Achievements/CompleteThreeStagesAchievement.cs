using System;
using RoR2.Stats;

namespace RoR2.Achievements
{
	// Token: 0x02000E88 RID: 3720
	[RegisterAchievement("CompleteThreeStages", "Characters.Bandit2", null, null)]
	public class CompleteThreeStagesAchievement : BaseAchievement
	{
		// Token: 0x06005520 RID: 21792 RVA: 0x0015DCE3 File Offset: 0x0015BEE3
		public override void OnInstall()
		{
			base.OnInstall();
			TeleporterInteraction.onTeleporterChargedGlobal += this.OnTeleporterCharged;
		}

		// Token: 0x06005521 RID: 21793 RVA: 0x0015DCFC File Offset: 0x0015BEFC
		public override void OnUninstall()
		{
			TeleporterInteraction.onTeleporterChargedGlobal -= this.OnTeleporterCharged;
			base.OnUninstall();
		}

		// Token: 0x06005522 RID: 21794 RVA: 0x0015DD15 File Offset: 0x0015BF15
		private void OnTeleporterCharged(TeleporterInteraction teleporterInteraction)
		{
			this.Check();
		}

		// Token: 0x06005523 RID: 21795 RVA: 0x0015DD20 File Offset: 0x0015BF20
		private void Check()
		{
			if (Run.instance && Run.instance.GetType() == typeof(Run))
			{
				SceneDef sceneDefForCurrentScene = SceneCatalog.GetSceneDefForCurrentScene();
				if (sceneDefForCurrentScene == null)
				{
					return;
				}
				if (base.localUser.currentNetworkUser.masterPlayerStatsComponent.currentStats.GetStatValueULong(StatDef.totalDeaths) == 0UL && sceneDefForCurrentScene.stageOrder == 3)
				{
					base.Grant();
				}
			}
		}

		// Token: 0x04005053 RID: 20563
		private const int requirement = 3;
	}
}
