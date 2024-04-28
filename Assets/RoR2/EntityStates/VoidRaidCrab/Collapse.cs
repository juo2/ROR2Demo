using System;
using UnityEngine;

namespace EntityStates.VoidRaidCrab
{
	// Token: 0x02000118 RID: 280
	public class Collapse : BaseState
	{
		// Token: 0x060004EA RID: 1258 RVA: 0x000153A8 File Offset: 0x000135A8
		public override void OnEnter()
		{
			base.OnEnter();
			base.PlayAnimation(this.animationLayerName, this.animationStateName, this.animationPlaybackRateParam, this.duration);
		}

		// Token: 0x060004EB RID: 1259 RVA: 0x000153CE File Offset: 0x000135CE
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.isAuthority && base.fixedAge >= this.duration)
			{
				this.outer.SetNextState(new ReEmerge());
			}
		}

		// Token: 0x060004EC RID: 1260 RVA: 0x0000E3AD File Offset: 0x0000C5AD
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.Death;
		}

		// Token: 0x040005AA RID: 1450
		[SerializeField]
		public float duration;

		// Token: 0x040005AB RID: 1451
		[SerializeField]
		public string animationLayerName;

		// Token: 0x040005AC RID: 1452
		[SerializeField]
		public string animationStateName;

		// Token: 0x040005AD RID: 1453
		[SerializeField]
		public string animationPlaybackRateParam;
	}
}
