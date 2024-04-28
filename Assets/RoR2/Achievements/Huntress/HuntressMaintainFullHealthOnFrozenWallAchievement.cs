using System;

namespace RoR2.Achievements.Huntress
{
	// Token: 0x02000EF0 RID: 3824
	[RegisterAchievement("HuntressMaintainFullHealthOnFrozenWall", "Skills.Huntress.Snipe", null, null)]
	public class HuntressMaintainFullHealthOnFrozenWallAchievement : BaseAchievement
	{
		// Token: 0x06005710 RID: 22288 RVA: 0x00160BDC File Offset: 0x0015EDDC
		protected override BodyIndex LookUpRequiredBodyIndex()
		{
			return BodyCatalog.FindBodyIndex("HuntressBody");
		}

		// Token: 0x06005711 RID: 22289 RVA: 0x00160D85 File Offset: 0x0015EF85
		private void SubscribeHealthCheck()
		{
			RoR2Application.onFixedUpdate += this.CheckHealth;
		}

		// Token: 0x06005712 RID: 22290 RVA: 0x00160D98 File Offset: 0x0015EF98
		private void UnsubscribeHealthCheck()
		{
			RoR2Application.onFixedUpdate -= this.CheckHealth;
		}

		// Token: 0x06005713 RID: 22291 RVA: 0x00160DAB File Offset: 0x0015EFAB
		private void SubscribeTeleporterCheck()
		{
			TeleporterInteraction.onTeleporterChargedGlobal += this.CheckTeleporter;
		}

		// Token: 0x06005714 RID: 22292 RVA: 0x00160DBE File Offset: 0x0015EFBE
		private void UnsubscribeTeleporterCheck()
		{
			TeleporterInteraction.onTeleporterChargedGlobal -= this.CheckTeleporter;
		}

		// Token: 0x06005715 RID: 22293 RVA: 0x00160DD1 File Offset: 0x0015EFD1
		private void CheckTeleporter(TeleporterInteraction teleporterInteraction)
		{
			if (this.sceneOk && this.characterOk && !this.failed)
			{
				base.Grant();
			}
		}

		// Token: 0x06005716 RID: 22294 RVA: 0x00160DF4 File Offset: 0x0015EFF4
		public override void OnInstall()
		{
			base.OnInstall();
			this.healthCheck = new ToggleAction(new Action(this.SubscribeHealthCheck), new Action(this.UnsubscribeHealthCheck));
			this.teleporterCheck = new ToggleAction(new Action(this.SubscribeTeleporterCheck), new Action(this.UnsubscribeTeleporterCheck));
			SceneCatalog.onMostRecentSceneDefChanged += this.OnMostRecentSceneDefChanged;
			base.localUser.onBodyChanged += this.OnBodyChanged;
		}

		// Token: 0x06005717 RID: 22295 RVA: 0x00160E78 File Offset: 0x0015F078
		public override void OnUninstall()
		{
			base.localUser.onBodyChanged -= this.OnBodyChanged;
			SceneCatalog.onMostRecentSceneDefChanged -= this.OnMostRecentSceneDefChanged;
			this.healthCheck.Dispose();
			this.teleporterCheck.Dispose();
			base.OnUninstall();
		}

		// Token: 0x06005718 RID: 22296 RVA: 0x00160ECC File Offset: 0x0015F0CC
		private void OnBodyChanged()
		{
			if (this.sceneOk && this.characterOk && !this.failed && base.localUser.cachedBody)
			{
				this.healthComponent = base.localUser.cachedBody.healthComponent;
				this.healthCheck.SetActive(true);
				this.teleporterCheck.SetActive(true);
			}
		}

		// Token: 0x06005719 RID: 22297 RVA: 0x00160F31 File Offset: 0x0015F131
		private void OnMostRecentSceneDefChanged(SceneDef sceneDef)
		{
			this.sceneOk = (Array.IndexOf<string>(HuntressMaintainFullHealthOnFrozenWallAchievement.requiredScenes, sceneDef.baseSceneName) != -1);
			if (this.sceneOk)
			{
				this.failed = false;
			}
		}

		// Token: 0x0600571A RID: 22298 RVA: 0x00160F5E File Offset: 0x0015F15E
		protected override void OnBodyRequirementMet()
		{
			base.OnBodyRequirementMet();
			this.characterOk = true;
		}

		// Token: 0x0600571B RID: 22299 RVA: 0x00160F6D File Offset: 0x0015F16D
		protected override void OnBodyRequirementBroken()
		{
			this.characterOk = false;
			this.Fail();
			base.OnBodyRequirementBroken();
		}

		// Token: 0x0600571C RID: 22300 RVA: 0x00160F82 File Offset: 0x0015F182
		private void Fail()
		{
			this.failed = true;
			this.healthCheck.SetActive(false);
			this.teleporterCheck.SetActive(false);
		}

		// Token: 0x0600571D RID: 22301 RVA: 0x00160FA3 File Offset: 0x0015F1A3
		private void CheckHealth()
		{
			if (this.healthComponent && this.healthComponent.combinedHealth < this.healthComponent.fullCombinedHealth)
			{
				this.Fail();
			}
		}

		// Token: 0x040050B4 RID: 20660
		private static readonly string[] requiredScenes = new string[]
		{
			"frozenwall",
			"wispgraveyard"
		};

		// Token: 0x040050B5 RID: 20661
		private HealthComponent healthComponent;

		// Token: 0x040050B6 RID: 20662
		private bool failed;

		// Token: 0x040050B7 RID: 20663
		private bool sceneOk;

		// Token: 0x040050B8 RID: 20664
		private bool characterOk;

		// Token: 0x040050B9 RID: 20665
		private ToggleAction healthCheck;

		// Token: 0x040050BA RID: 20666
		private ToggleAction teleporterCheck;
	}
}
