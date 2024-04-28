using System;
using UnityEngine;

namespace RoR2.Mecanim
{
	// Token: 0x02000BC3 RID: 3011
	public class ClockParamWriter : StateMachineBehaviour
	{
		// Token: 0x0600448F RID: 17551 RVA: 0x0011D76C File Offset: 0x0011B96C
		public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
		{
			base.OnStateUpdate(animator, stateInfo, layerIndex);
			float num;
			if (Run.instance)
			{
				num = Run.instance.GetRunStopwatch();
			}
			else
			{
				num = (float)(DateTime.Now - DateTime.Today).TotalSeconds;
			}
			animator.SetFloat(this.targetParamName, this.cyclesPerDay * num / 86400f);
		}

		// Token: 0x0400430E RID: 17166
		public string targetParamName = "time";

		// Token: 0x0400430F RID: 17167
		public float cyclesPerDay = 2f;

		// Token: 0x04004310 RID: 17168
		private const float secondsPerDay = 86400f;
	}
}
