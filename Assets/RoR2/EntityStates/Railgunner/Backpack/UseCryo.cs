using System;
using UnityEngine;

namespace EntityStates.Railgunner.Backpack
{
	// Token: 0x0200021D RID: 541
	public class UseCryo : BaseBackpack
	{
		// Token: 0x0600097E RID: 2430 RVA: 0x00027242 File Offset: 0x00025442
		public override void OnEnter()
		{
			base.OnEnter();
			base.PlayAnimation(this.animationLayerName, this.animationStateName, this.animationPlaybackRateParam, this.duration);
		}

		// Token: 0x0600097F RID: 2431 RVA: 0x00027268 File Offset: 0x00025468
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.fixedAge >= this.duration)
			{
				this.outer.SetNextState(new OnlineCryo());
			}
		}

		// Token: 0x06000980 RID: 2432 RVA: 0x0000E3AD File Offset: 0x0000C5AD
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.Death;
		}

		// Token: 0x04000B0B RID: 2827
		[SerializeField]
		public float duration;

		// Token: 0x04000B0C RID: 2828
		[SerializeField]
		public string animationLayerName;

		// Token: 0x04000B0D RID: 2829
		[SerializeField]
		public string animationStateName;

		// Token: 0x04000B0E RID: 2830
		[SerializeField]
		public string animationPlaybackRateParam;
	}
}
