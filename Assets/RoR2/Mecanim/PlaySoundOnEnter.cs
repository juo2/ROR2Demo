using System;
using UnityEngine;

namespace RoR2.Mecanim
{
	// Token: 0x02000BC5 RID: 3013
	public class PlaySoundOnEnter : StateMachineBehaviour
	{
		// Token: 0x06004496 RID: 17558 RVA: 0x0011D99B File Offset: 0x0011BB9B
		public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
		{
			base.OnStateEnter(animator, stateInfo, layerIndex);
			Util.PlaySound(this.soundString, animator.gameObject);
		}

		// Token: 0x06004497 RID: 17559 RVA: 0x0011D9B8 File Offset: 0x0011BBB8
		public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
		{
			base.OnStateEnter(animator, stateInfo, layerIndex);
			Util.PlaySound(this.stopSoundString, animator.gameObject);
		}

		// Token: 0x0400431A RID: 17178
		public string soundString;

		// Token: 0x0400431B RID: 17179
		public string stopSoundString;
	}
}
