using System;
using RoR2;
using UnityEngine;
using UnityEngine.Networking;

namespace EntityStates.GameOver
{
	// Token: 0x02000381 RID: 897
	public class VoidEndingSetSceneAndWaitForPlayers : BaseGameOverControllerState
	{
		// Token: 0x06001016 RID: 4118 RVA: 0x00047046 File Offset: 0x00045246
		public override void OnEnter()
		{
			base.OnEnter();
			FadeToBlackManager.ForceFullBlack();
			FadeToBlackManager.fadeCount++;
			if (NetworkServer.active)
			{
				Run.instance.AdvanceStage(this.desiredSceneDef);
			}
		}

		// Token: 0x06001017 RID: 4119 RVA: 0x00047076 File Offset: 0x00045276
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (NetworkServer.active && NetworkUser.AllParticipatingNetworkUsersReady() && SceneCatalog.mostRecentSceneDef == this.desiredSceneDef)
			{
				this.outer.SetNextState(new VoidEndingPlayCutscene());
			}
		}

		// Token: 0x06001018 RID: 4120 RVA: 0x00046CF7 File Offset: 0x00044EF7
		public override void OnExit()
		{
			FadeToBlackManager.fadeCount--;
			base.OnExit();
		}

		// Token: 0x04001490 RID: 5264
		[SerializeField]
		public SceneDef desiredSceneDef;
	}
}
