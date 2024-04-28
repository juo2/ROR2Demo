using System;

namespace RoR2.Achievements
{
	// Token: 0x02000EB3 RID: 3763
	[RegisterAchievement("LoopOnce", "Items.BounceNearby", null, null)]
	public class LoopOnceAchievement : BaseAchievement
	{
		// Token: 0x060055CE RID: 21966 RVA: 0x0015EBCC File Offset: 0x0015CDCC
		public override void OnInstall()
		{
			base.OnInstall();
			UserProfile userProfile = base.userProfile;
			userProfile.onStatsReceived = (Action)Delegate.Combine(userProfile.onStatsReceived, new Action(this.Check));
			this.Check();
		}

		// Token: 0x060055CF RID: 21967 RVA: 0x0015EC01 File Offset: 0x0015CE01
		public override void OnUninstall()
		{
			UserProfile userProfile = base.userProfile;
			userProfile.onStatsReceived = (Action)Delegate.Remove(userProfile.onStatsReceived, new Action(this.Check));
			base.OnUninstall();
		}

		// Token: 0x060055D0 RID: 21968 RVA: 0x0015EC30 File Offset: 0x0015CE30
		private void Check()
		{
			if (Run.instance && Run.instance.GetType() == typeof(Run) && Run.instance.loopClearCount > 0)
			{
				SceneDef sceneDefForCurrentScene = SceneCatalog.GetSceneDefForCurrentScene();
				if (sceneDefForCurrentScene && sceneDefForCurrentScene.sceneType == SceneType.Stage && !sceneDefForCurrentScene.isFinalStage)
				{
					base.Grant();
				}
			}
		}
	}
}
