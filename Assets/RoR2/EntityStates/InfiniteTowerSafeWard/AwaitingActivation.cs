using System;
using RoR2;
using UnityEngine;
using UnityEngine.Networking;

namespace EntityStates.InfiniteTowerSafeWard
{
	// Token: 0x020002F7 RID: 759
	public class AwaitingActivation : BaseSafeWardState
	{
		// Token: 0x06000D89 RID: 3465 RVA: 0x00039150 File Offset: 0x00037350
		public override void OnEnter()
		{
			base.OnEnter();
			this.PlayAnimation(this.animationLayerName, this.animationStateName);
			Util.PlaySound(this.enterSoundString, base.gameObject);
			if (this.purchaseInteraction)
			{
				this.purchaseInteraction.SetAvailable(true);
			}
			if (this.zone)
			{
				this.zone.Networkradius = this.radius;
			}
		}

		// Token: 0x06000D8A RID: 3466 RVA: 0x000391BE File Offset: 0x000373BE
		public void Activate()
		{
			if (NetworkServer.active)
			{
				this.outer.SetNextState(new Active());
			}
		}

		// Token: 0x0400108E RID: 4238
		[SerializeField]
		public float radius;

		// Token: 0x0400108F RID: 4239
		[SerializeField]
		public string animationLayerName;

		// Token: 0x04001090 RID: 4240
		[SerializeField]
		public string animationStateName;

		// Token: 0x04001091 RID: 4241
		[SerializeField]
		public string enterSoundString;
	}
}
