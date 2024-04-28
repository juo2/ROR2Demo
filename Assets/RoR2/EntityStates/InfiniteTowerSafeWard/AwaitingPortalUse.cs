using System;
using RoR2;
using UnityEngine;

namespace EntityStates.InfiniteTowerSafeWard
{
	// Token: 0x020002F8 RID: 760
	public class AwaitingPortalUse : BaseSafeWardState
	{
		// Token: 0x06000D8C RID: 3468 RVA: 0x000391D8 File Offset: 0x000373D8
		public override void OnEnter()
		{
			base.OnEnter();
			this.PlayAnimation(this.animationLayerName, this.animationStateName);
			Util.PlaySound(this.enterSoundString, base.gameObject);
			if (this.purchaseInteraction)
			{
				this.purchaseInteraction.SetAvailable(false);
			}
			if (this.zone)
			{
				this.zone.Networkradius = this.radius;
			}
		}

		// Token: 0x04001092 RID: 4242
		[SerializeField]
		public float radius;

		// Token: 0x04001093 RID: 4243
		[SerializeField]
		public string animationLayerName;

		// Token: 0x04001094 RID: 4244
		[SerializeField]
		public string animationStateName;

		// Token: 0x04001095 RID: 4245
		[SerializeField]
		public string enterSoundString;
	}
}
