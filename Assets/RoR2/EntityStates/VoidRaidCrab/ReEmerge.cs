using System;
using RoR2.VoidRaidCrab;
using UnityEngine;
using UnityEngine.Networking;

namespace EntityStates.VoidRaidCrab
{
	// Token: 0x0200011E RID: 286
	public class ReEmerge : BaseState
	{
		// Token: 0x06000507 RID: 1287 RVA: 0x00015ABC File Offset: 0x00013CBC
		public override void OnEnter()
		{
			base.OnEnter();
			base.PlayAnimation(this.animationLayerName, this.animationStateName, this.animationPlaybackRateParam, this.duration);
			if (NetworkServer.active)
			{
				CentralLegController component = base.GetComponent<CentralLegController>();
				if (component)
				{
					component.RegenerateAllBrokenServer();
				}
			}
		}

		// Token: 0x06000508 RID: 1288 RVA: 0x00015B09 File Offset: 0x00013D09
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.isAuthority && base.fixedAge >= this.duration)
			{
				this.outer.SetNextStateToMain();
			}
		}

		// Token: 0x06000509 RID: 1289 RVA: 0x0000E3AD File Offset: 0x0000C5AD
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.Death;
		}

		// Token: 0x040005E1 RID: 1505
		[SerializeField]
		public float duration;

		// Token: 0x040005E2 RID: 1506
		[SerializeField]
		public string animationLayerName;

		// Token: 0x040005E3 RID: 1507
		[SerializeField]
		public string animationStateName;

		// Token: 0x040005E4 RID: 1508
		[SerializeField]
		public string animationPlaybackRateParam;
	}
}
