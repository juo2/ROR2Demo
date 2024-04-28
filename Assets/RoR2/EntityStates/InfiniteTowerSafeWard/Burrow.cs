using System;
using RoR2;
using UnityEngine;
using UnityEngine.Networking;

namespace EntityStates.InfiniteTowerSafeWard
{
	// Token: 0x020002FA RID: 762
	public class Burrow : BaseSafeWardState
	{
		// Token: 0x06000D92 RID: 3474 RVA: 0x00039308 File Offset: 0x00037508
		public override void OnEnter()
		{
			base.OnEnter();
			this.PlayAnimation(this.animationLayerName, this.animationStateName);
			Util.PlaySound(this.enterSoundString, base.gameObject);
			if (this.zone)
			{
				this.zone.Networkradius = this.radius;
			}
		}

		// Token: 0x06000D93 RID: 3475 RVA: 0x0003935D File Offset: 0x0003755D
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (NetworkServer.active && base.fixedAge >= this.duration)
			{
				this.outer.SetNextState(new AwaitingActivation());
			}
		}

		// Token: 0x0400109C RID: 4252
		[SerializeField]
		public float duration;

		// Token: 0x0400109D RID: 4253
		[SerializeField]
		public float radius;

		// Token: 0x0400109E RID: 4254
		[SerializeField]
		public string animationLayerName;

		// Token: 0x0400109F RID: 4255
		[SerializeField]
		public string animationStateName;

		// Token: 0x040010A0 RID: 4256
		[SerializeField]
		public string enterSoundString;
	}
}
