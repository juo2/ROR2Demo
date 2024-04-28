using System;
using UnityEngine;

namespace RoR2.Mecanim
{
	// Token: 0x02000BCA RID: 3018
	public class StayInStateForDuration : StateMachineBehaviour
	{
		// Token: 0x060044A2 RID: 17570 RVA: 0x0011DAFC File Offset: 0x0011BCFC
		public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
		{
			base.OnStateEnter(animator, stateInfo, layerIndex);
			animator.SetBool(this.deactivationBoolParameterName, true);
		}

		// Token: 0x060044A3 RID: 17571 RVA: 0x0011DB14 File Offset: 0x0011BD14
		public override void OnStateUpdate(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
		{
			base.OnStateUpdate(animator, animatorStateInfo, layerIndex);
			float num = animator.GetFloat(this.stopwatchFloatParameterName);
			float @float = animator.GetFloat(this.durationFloatParameterName);
			num += Time.deltaTime;
			if (num >= @float)
			{
				animator.SetFloat(this.stopwatchFloatParameterName, 0f);
				animator.SetBool(this.deactivationBoolParameterName, false);
				return;
			}
			animator.SetFloat(this.stopwatchFloatParameterName, num);
		}

		// Token: 0x04004327 RID: 17191
		[Tooltip("The reference float - this is how long we will stay in this state")]
		public string durationFloatParameterName;

		// Token: 0x04004328 RID: 17192
		[Tooltip("The counter float - this is exposed incase we want to reset it")]
		public string stopwatchFloatParameterName;

		// Token: 0x04004329 RID: 17193
		[Tooltip("The bool that will be set to 'false' once the duration is up, and 'true' when entering this state.")]
		public string deactivationBoolParameterName;
	}
}
