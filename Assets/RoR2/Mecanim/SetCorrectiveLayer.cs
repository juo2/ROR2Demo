using System;
using UnityEngine;

namespace RoR2.Mecanim
{
	// Token: 0x02000BC8 RID: 3016
	public class SetCorrectiveLayer : StateMachineBehaviour
	{
		// Token: 0x0600449D RID: 17565 RVA: 0x0011DA5E File Offset: 0x0011BC5E
		public override void OnStateMachineEnter(Animator animator, int stateMachinePathHash)
		{
			base.OnStateMachineEnter(animator, stateMachinePathHash);
		}

		// Token: 0x0600449E RID: 17566 RVA: 0x0011DA68 File Offset: 0x0011BC68
		public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
		{
			int layerIndex2 = animator.GetLayerIndex(this.referenceOverrideLayerName);
			float target = Mathf.Min(animator.GetLayerWeight(layerIndex2), this.maxWeight);
			float weight = Mathf.SmoothDamp(animator.GetLayerWeight(layerIndex), target, ref this.smoothVelocity, 0.2f);
			animator.SetLayerWeight(layerIndex, weight);
			base.OnStateUpdate(animator, stateInfo, layerIndex);
		}

		// Token: 0x04004321 RID: 17185
		public string referenceOverrideLayerName;

		// Token: 0x04004322 RID: 17186
		public float maxWeight = 1f;

		// Token: 0x04004323 RID: 17187
		private float smoothVelocity;
	}
}
