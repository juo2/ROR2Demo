using System;
using UnityEngine;

namespace EntityStates.Railgunner.Backpack
{
	// Token: 0x0200021C RID: 540
	public class Reboot : BaseBackpack
	{
		// Token: 0x0600097A RID: 2426 RVA: 0x000271F6 File Offset: 0x000253F6
		public override void OnEnter()
		{
			base.OnEnter();
			base.PlayAnimation(this.animationLayerName, this.animationStateName, this.animationPlaybackRateParam, this.duration);
		}

		// Token: 0x0600097B RID: 2427 RVA: 0x0002721C File Offset: 0x0002541C
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.fixedAge >= this.duration)
			{
				this.outer.SetNextState(new OnlineSuper());
			}
		}

		// Token: 0x0600097C RID: 2428 RVA: 0x0000E3AD File Offset: 0x0000C5AD
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.Death;
		}

		// Token: 0x04000B07 RID: 2823
		[SerializeField]
		public float duration;

		// Token: 0x04000B08 RID: 2824
		[SerializeField]
		public string animationLayerName;

		// Token: 0x04000B09 RID: 2825
		[SerializeField]
		public string animationStateName;

		// Token: 0x04000B0A RID: 2826
		[SerializeField]
		public string animationPlaybackRateParam;
	}
}
