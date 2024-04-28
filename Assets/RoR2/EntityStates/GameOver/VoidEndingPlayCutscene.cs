using System;
using RoR2;
using UnityEngine.Networking;

namespace EntityStates.GameOver
{
	// Token: 0x02000380 RID: 896
	public class VoidEndingPlayCutscene : BaseGameOverControllerState
	{
		// Token: 0x06001013 RID: 4115 RVA: 0x00046D0B File Offset: 0x00044F0B
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (NetworkServer.active && (!OutroCutsceneController.instance || OutroCutsceneController.instance.cutsceneIsFinished))
			{
				this.outer.SetNextStateToMain();
			}
		}

		// Token: 0x06001014 RID: 4116 RVA: 0x00046FF8 File Offset: 0x000451F8
		public override void OnExit()
		{
			if (OutroCutsceneController.instance && OutroCutsceneController.instance.playableDirector)
			{
				OutroCutsceneController.instance.playableDirector.time = OutroCutsceneController.instance.playableDirector.duration;
			}
			base.OnExit();
		}
	}
}
