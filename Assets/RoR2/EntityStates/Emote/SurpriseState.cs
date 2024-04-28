using System;
using UnityEngine;

namespace EntityStates.Emote
{
	// Token: 0x020001C0 RID: 448
	public class SurpriseState : EntityState
	{
		// Token: 0x06000804 RID: 2052 RVA: 0x00021FD8 File Offset: 0x000201D8
		public override void OnEnter()
		{
			base.OnEnter();
			Animator modelAnimator = base.GetModelAnimator();
			if (modelAnimator)
			{
				int layerIndex = modelAnimator.GetLayerIndex("Body");
				modelAnimator.Play("EmoteSurprise", layerIndex, 0f);
				modelAnimator.Update(0f);
				this.duration = modelAnimator.GetCurrentAnimatorStateInfo(layerIndex).length;
			}
		}

		// Token: 0x06000805 RID: 2053 RVA: 0x00022037 File Offset: 0x00020237
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.fixedAge >= this.duration && base.isAuthority)
			{
				this.outer.SetNextStateToMain();
			}
		}

		// Token: 0x06000806 RID: 2054 RVA: 0x0000E3AD File Offset: 0x0000C5AD
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.Death;
		}

		// Token: 0x04000976 RID: 2422
		private float duration;
	}
}
