using System;
using UnityEngine;

namespace EntityStates
{
	// Token: 0x020000D1 RID: 209
	public class SleepState : EntityState
	{
		// Token: 0x060003CC RID: 972 RVA: 0x0000F958 File Offset: 0x0000DB58
		public override void OnEnter()
		{
			base.OnEnter();
			Animator modelAnimator = base.GetModelAnimator();
			if (modelAnimator)
			{
				int layerIndex = modelAnimator.GetLayerIndex("Body");
				modelAnimator.Play("Sleep", layerIndex, 0f);
			}
		}

		// Token: 0x060003CD RID: 973 RVA: 0x0000F997 File Offset: 0x0000DB97
		public override void FixedUpdate()
		{
			base.FixedUpdate();
		}

		// Token: 0x060003CE RID: 974 RVA: 0x0000CF8A File Offset: 0x0000B18A
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.Any;
		}
	}
}
