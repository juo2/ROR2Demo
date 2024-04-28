using System;
using RoR2;
using UnityEngine.Networking;

namespace EntityStates.Interactables.MSObelisk
{
	// Token: 0x020002F2 RID: 754
	public class TransitionToNextStage : BaseState
	{
		// Token: 0x06000D77 RID: 3447 RVA: 0x00038E84 File Offset: 0x00037084
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (NetworkServer.active && base.fixedAge >= TransitionToNextStage.duration)
			{
				Stage.instance.BeginAdvanceStage(SceneCatalog.GetSceneDefFromSceneName("limbo"));
				this.outer.SetNextState(new Idle());
			}
		}

		// Token: 0x04001085 RID: 4229
		public static float duration;
	}
}
