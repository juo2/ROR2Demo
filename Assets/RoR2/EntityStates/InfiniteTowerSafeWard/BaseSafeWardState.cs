using System;
using RoR2;
using UnityEngine;

namespace EntityStates.InfiniteTowerSafeWard
{
	// Token: 0x020002F9 RID: 761
	public class BaseSafeWardState : EntityState
	{
		// Token: 0x06000D8E RID: 3470 RVA: 0x00039248 File Offset: 0x00037448
		public override void OnEnter()
		{
			base.OnEnter();
			this.purchaseInteraction = base.GetComponent<PurchaseInteraction>();
			this.zone = base.GetComponent<VerticalTubeZone>();
			this.animator = base.gameObject.GetComponentInChildren<Animator>();
			this.safeWardController = base.gameObject.GetComponent<InfiniteTowerSafeWardController>();
			if (!string.IsNullOrEmpty(this.objectiveToken))
			{
				this.genericObjectiveProvider = base.gameObject.AddComponent<GenericObjectiveProvider>();
				this.genericObjectiveProvider.objectiveToken = this.objectiveToken;
			}
		}

		// Token: 0x06000D8F RID: 3471 RVA: 0x000392C4 File Offset: 0x000374C4
		public override void PlayAnimation(string layerName, string animationStateName)
		{
			if (this.animator && !string.IsNullOrEmpty(layerName))
			{
				EntityState.PlayAnimationOnAnimator(this.animator, layerName, animationStateName);
			}
		}

		// Token: 0x06000D90 RID: 3472 RVA: 0x000392E8 File Offset: 0x000374E8
		public override void OnExit()
		{
			if (this.genericObjectiveProvider)
			{
				EntityState.Destroy(this.genericObjectiveProvider);
			}
			base.OnExit();
		}

		// Token: 0x04001096 RID: 4246
		protected PurchaseInteraction purchaseInteraction;

		// Token: 0x04001097 RID: 4247
		protected VerticalTubeZone zone;

		// Token: 0x04001098 RID: 4248
		protected Animator animator;

		// Token: 0x04001099 RID: 4249
		[SerializeField]
		public string objectiveToken;

		// Token: 0x0400109A RID: 4250
		private GenericObjectiveProvider genericObjectiveProvider;

		// Token: 0x0400109B RID: 4251
		protected InfiniteTowerSafeWardController safeWardController;
	}
}
