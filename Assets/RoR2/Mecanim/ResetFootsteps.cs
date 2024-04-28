using System;
using UnityEngine;

namespace RoR2.Mecanim
{
	// Token: 0x02000BC7 RID: 3015
	public class ResetFootsteps : StateMachineBehaviour
	{
		// Token: 0x0600449B RID: 17563 RVA: 0x0011DA50 File Offset: 0x0011BC50
		public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
		{
			animator.GetComponent<FootstepHandler>();
		}
	}
}
