using System;
using RoR2;
using UnityEngine;
using UnityEngine.Networking;

namespace EntityStates.GameOver
{
	// Token: 0x02000382 RID: 898
	public class VoidEndingStart : BaseGameOverControllerState
	{
		// Token: 0x0600101A RID: 4122 RVA: 0x000470AE File Offset: 0x000452AE
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (NetworkServer.active && base.fixedAge >= this.duration && this.hasSceneExited)
			{
				this.outer.SetNextState(new VoidEndingFadeToBlack());
			}
		}

		// Token: 0x0600101B RID: 4123 RVA: 0x000470E3 File Offset: 0x000452E3
		public override void OnEnter()
		{
			base.OnEnter();
			SceneExitController.onFinishExit += this.OnFinishSceneExit;
		}

		// Token: 0x0600101C RID: 4124 RVA: 0x000470FC File Offset: 0x000452FC
		public override void OnExit()
		{
			SceneExitController.onFinishExit -= this.OnFinishSceneExit;
			base.OnExit();
		}

		// Token: 0x0600101D RID: 4125 RVA: 0x00047115 File Offset: 0x00045315
		private void OnFinishSceneExit(SceneExitController obj)
		{
			this.hasSceneExited = true;
		}

		// Token: 0x04001491 RID: 5265
		[SerializeField]
		public float duration;

		// Token: 0x04001492 RID: 5266
		private bool hasSceneExited;
	}
}
