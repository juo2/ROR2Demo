using System;
using RoR2;
using UnityEngine;

namespace EntityStates.VoidRaidCrab
{
	// Token: 0x02000117 RID: 279
	public class CloseGauntlet : BaseState
	{
		// Token: 0x060004E6 RID: 1254 RVA: 0x00015347 File Offset: 0x00013547
		public override void OnEnter()
		{
			base.OnEnter();
			base.PlayAnimation(this.animationLayerName, this.animationStateName, this.animationPlaybackRateParam, this.duration);
			Util.PlaySound(this.enterSoundString, base.gameObject);
		}

		// Token: 0x060004E7 RID: 1255 RVA: 0x0001537F File Offset: 0x0001357F
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.isAuthority && base.fixedAge >= this.duration)
			{
				this.outer.SetNextStateToMain();
			}
		}

		// Token: 0x060004E8 RID: 1256 RVA: 0x0000E3AD File Offset: 0x0000C5AD
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.Death;
		}

		// Token: 0x040005A5 RID: 1445
		[SerializeField]
		public float duration;

		// Token: 0x040005A6 RID: 1446
		[SerializeField]
		public string animationLayerName;

		// Token: 0x040005A7 RID: 1447
		[SerializeField]
		public string animationStateName;

		// Token: 0x040005A8 RID: 1448
		[SerializeField]
		public string animationPlaybackRateParam;

		// Token: 0x040005A9 RID: 1449
		[SerializeField]
		public string enterSoundString;
	}
}
