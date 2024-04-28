using System;
using RoR2;
using UnityEngine;

namespace EntityStates.Treebot
{
	// Token: 0x0200017B RID: 379
	public class TreebotPrepFruitSeed : BaseState
	{
		// Token: 0x0600069D RID: 1693 RVA: 0x0001CAC4 File Offset: 0x0001ACC4
		public override void OnEnter()
		{
			base.OnEnter();
			this.duration = this.baseDuration / this.attackSpeedStat;
			Util.PlaySound(this.enterSoundString, base.gameObject);
			base.PlayAnimation(this.animationLayerName, this.animationStateName, this.playbackRateParam, this.duration);
		}

		// Token: 0x0600069E RID: 1694 RVA: 0x0001CB1A File Offset: 0x0001AD1A
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.fixedAge >= this.duration)
			{
				this.outer.SetNextState(new TreebotFireFruitSeed());
			}
		}

		// Token: 0x0600069F RID: 1695 RVA: 0x0000EE13 File Offset: 0x0000D013
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.PrioritySkill;
		}

		// Token: 0x0400082A RID: 2090
		[SerializeField]
		public float baseDuration;

		// Token: 0x0400082B RID: 2091
		[SerializeField]
		public string enterSoundString;

		// Token: 0x0400082C RID: 2092
		[SerializeField]
		public string animationLayerName = "Gesture, Additive";

		// Token: 0x0400082D RID: 2093
		[SerializeField]
		public string animationStateName = "PrepFlower";

		// Token: 0x0400082E RID: 2094
		[SerializeField]
		public string playbackRateParam = "PrepFlower.playbackRate";

		// Token: 0x0400082F RID: 2095
		private float duration;
	}
}
