using System;
using UnityEngine;

namespace RoR2.Mecanim
{
	// Token: 0x02000BC2 RID: 3010
	public class ClearLayerWeight : StateMachineBehaviour
	{
		// Token: 0x0600448C RID: 17548 RVA: 0x0011D6D4 File Offset: 0x0011B8D4
		public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
		{
			base.OnStateEnter(animator, stateInfo, layerIndex);
			int layerIndex2 = layerIndex;
			if (this.layerName.Length > 0)
			{
				layerIndex2 = animator.GetLayerIndex(this.layerName);
			}
			animator.SetLayerWeight(layerIndex2, 0f);
		}

		// Token: 0x0600448D RID: 17549 RVA: 0x0011D714 File Offset: 0x0011B914
		public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
		{
			base.OnStateExit(animator, stateInfo, layerIndex);
			if (this.resetOnExit)
			{
				int layerIndex2 = layerIndex;
				if (this.layerName.Length > 0)
				{
					layerIndex2 = animator.GetLayerIndex(this.layerName);
				}
				animator.SetLayerWeight(layerIndex2, 1f);
			}
		}

		// Token: 0x0400430C RID: 17164
		public bool resetOnExit = true;

		// Token: 0x0400430D RID: 17165
		public string layerName;
	}
}
