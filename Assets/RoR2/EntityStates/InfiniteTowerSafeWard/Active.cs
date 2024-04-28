using System;
using RoR2;
using UnityEngine;
using UnityEngine.Networking;

namespace EntityStates.InfiniteTowerSafeWard
{
	// Token: 0x020002F6 RID: 758
	public class Active : BaseSafeWardState
	{
		// Token: 0x06000D85 RID: 3461 RVA: 0x0003902C File Offset: 0x0003722C
		public override void OnEnter()
		{
			base.OnEnter();
			if (this.safeWardController)
			{
				this.safeWardController.SetIndicatorEnabled(false);
			}
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
			this.run = (Run.instance as InfiniteTowerRun);
		}

		// Token: 0x06000D86 RID: 3462 RVA: 0x000390C3 File Offset: 0x000372C3
		public void SelfDestruct()
		{
			if (NetworkServer.active)
			{
				this.outer.SetNextState(new SelfDestruct());
			}
		}

		// Token: 0x06000D87 RID: 3463 RVA: 0x000390DC File Offset: 0x000372DC
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (this.zone && this.run)
			{
				float num = 1f;
				if (this.run.waveController)
				{
					num = this.run.waveController.zoneRadiusPercentage;
				}
				this.zone.Networkradius = this.radius * num;
			}
		}

		// Token: 0x04001089 RID: 4233
		[SerializeField]
		public float radius;

		// Token: 0x0400108A RID: 4234
		[SerializeField]
		public string animationLayerName;

		// Token: 0x0400108B RID: 4235
		[SerializeField]
		public string animationStateName;

		// Token: 0x0400108C RID: 4236
		[SerializeField]
		public string enterSoundString;

		// Token: 0x0400108D RID: 4237
		private InfiniteTowerRun run;
	}
}
