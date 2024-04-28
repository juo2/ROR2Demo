using System;
using UnityEngine;

namespace EntityStates.BeetleMonster
{
	// Token: 0x0200045F RID: 1119
	public class MainState : EntityState
	{
		// Token: 0x06001400 RID: 5120 RVA: 0x000593AD File Offset: 0x000575AD
		public override void OnEnter()
		{
			base.OnEnter();
			this.modelAnimator = base.GetModelAnimator();
			if (this.modelAnimator)
			{
				this.modelAnimator.CrossFadeInFixedTime("Walk", 0.1f);
			}
		}

		// Token: 0x06001401 RID: 5121 RVA: 0x000593E3 File Offset: 0x000575E3
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (this.modelAnimator)
			{
				this.modelAnimator.SetFloat("walkToRunBlend", 1f);
			}
		}

		// Token: 0x040019B0 RID: 6576
		private Animator modelAnimator;
	}
}
