using System;
using UnityEngine;

namespace RoR2.Mecanim
{
	// Token: 0x02000BC9 RID: 3017
	public class SetRandomIntOnEnter : StateMachineBehaviour
	{
		// Token: 0x060044A0 RID: 17568 RVA: 0x0011DAD2 File Offset: 0x0011BCD2
		public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
		{
			base.OnStateEnter(animator, stateInfo, layerIndex);
			animator.SetInteger(this.intParameterName, UnityEngine.Random.Range(this.rangeMin, this.rangeMax + 1));
		}

		// Token: 0x04004324 RID: 17188
		public string intParameterName;

		// Token: 0x04004325 RID: 17189
		public int rangeMin;

		// Token: 0x04004326 RID: 17190
		public int rangeMax;
	}
}
