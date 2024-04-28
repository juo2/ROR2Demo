using System;
using RoR2;
using UnityEngine.Networking;

namespace EntityStates.GameOver
{
	// Token: 0x0200037A RID: 890
	public class RoR2MainEndingPlayCutscene : BaseGameOverControllerState
	{
		// Token: 0x06000FFD RID: 4093 RVA: 0x00046D0B File Offset: 0x00044F0B
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (NetworkServer.active && (!OutroCutsceneController.instance || OutroCutsceneController.instance.cutsceneIsFinished))
			{
				this.outer.SetNextStateToMain();
			}
		}

		// Token: 0x06000FFE RID: 4094 RVA: 0x00046D40 File Offset: 0x00044F40
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
