using System;
using UnityEngine;

namespace EntityStates
{
	// Token: 0x020000B9 RID: 185
	public class EmotePoint : BaseState
	{
		// Token: 0x0600031A RID: 794 RVA: 0x0000CE90 File Offset: 0x0000B090
		public override void OnEnter()
		{
			base.OnEnter();
			Animator modelAnimator = base.GetModelAnimator();
			if (modelAnimator)
			{
				int layerIndex = modelAnimator.GetLayerIndex("Gesture");
				modelAnimator.SetFloat("EmotePoint.playbackRate", 1f);
				modelAnimator.PlayInFixedTime("EmotePoint", layerIndex, 0f);
				modelAnimator.Update(0f);
				modelAnimator.SetFloat("EmotePoint.playbackRate", this.attackSpeedStat);
			}
		}

		// Token: 0x0600031B RID: 795 RVA: 0x0000CEFD File Offset: 0x0000B0FD
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.fixedAge > EmotePoint.duration && base.isAuthority)
			{
				this.outer.SetNextStateToMain();
				return;
			}
		}

		// Token: 0x0400034D RID: 845
		public static float duration = 0.5f;
	}
}
