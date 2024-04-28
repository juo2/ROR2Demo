using System;
using RoR2;
using UnityEngine.Networking;

namespace EntityStates.GameOver
{
	// Token: 0x02000379 RID: 889
	public class RoR2MainEndingSetSceneAndWaitForPlayers : BaseGameOverControllerState
	{
		// Token: 0x06000FF9 RID: 4089 RVA: 0x00046C7F File Offset: 0x00044E7F
		public override void OnEnter()
		{
			base.OnEnter();
			FadeToBlackManager.ForceFullBlack();
			FadeToBlackManager.fadeCount++;
			this.desiredSceneDef = SceneCatalog.GetSceneDefFromSceneName("outro");
			if (NetworkServer.active)
			{
				Run.instance.AdvanceStage(this.desiredSceneDef);
			}
		}

		// Token: 0x06000FFA RID: 4090 RVA: 0x00046CBF File Offset: 0x00044EBF
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (NetworkServer.active && NetworkUser.AllParticipatingNetworkUsersReady() && SceneCatalog.mostRecentSceneDef == this.desiredSceneDef)
			{
				this.outer.SetNextState(new RoR2MainEndingPlayCutscene());
			}
		}

		// Token: 0x06000FFB RID: 4091 RVA: 0x00046CF7 File Offset: 0x00044EF7
		public override void OnExit()
		{
			FadeToBlackManager.fadeCount--;
			base.OnExit();
		}

		// Token: 0x04001487 RID: 5255
		private SceneDef desiredSceneDef;
	}
}
