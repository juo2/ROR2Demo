using System;
using RoR2;
using UnityEngine;
using UnityEngine.Networking;

namespace EntityStates.InfiniteTowerSafeWard
{
	// Token: 0x020002FB RID: 763
	public class CorrallingPlayers : BaseSafeWardState
	{
		// Token: 0x06000D95 RID: 3477 RVA: 0x0003938C File Offset: 0x0003758C
		public override void OnEnter()
		{
			base.OnEnter();
			this.PlayAnimation(this.animationLayerName, this.animationStateName);
			Util.PlaySound(this.enterSoundString, base.gameObject);
			if (this.purchaseInteraction)
			{
				this.purchaseInteraction.SetAvailable(false);
			}
		}

		// Token: 0x06000D96 RID: 3478 RVA: 0x000393DC File Offset: 0x000375DC
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (this.zone)
			{
				float t = Mathf.Min(1f, base.fixedAge / this.duration);
				this.zone.Networkradius = Mathf.Lerp(this.initialRadius, this.finalRadius, t);
			}
			if (NetworkServer.active && base.fixedAge >= this.duration)
			{
				this.outer.SetNextState(new AwaitingActivation());
			}
		}

		// Token: 0x040010A1 RID: 4257
		[SerializeField]
		public float duration;

		// Token: 0x040010A2 RID: 4258
		[SerializeField]
		public float initialRadius;

		// Token: 0x040010A3 RID: 4259
		[SerializeField]
		public float finalRadius;

		// Token: 0x040010A4 RID: 4260
		[SerializeField]
		public string animationLayerName;

		// Token: 0x040010A5 RID: 4261
		[SerializeField]
		public string animationStateName;

		// Token: 0x040010A6 RID: 4262
		[SerializeField]
		public string enterSoundString;
	}
}
