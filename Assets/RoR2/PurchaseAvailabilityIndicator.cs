using System;
using UnityEngine;

namespace RoR2
{
	// Token: 0x02000836 RID: 2102
	[RequireComponent(typeof(PurchaseInteraction))]
	public class PurchaseAvailabilityIndicator : MonoBehaviour
	{
		// Token: 0x06002DC1 RID: 11713 RVA: 0x000C2D7F File Offset: 0x000C0F7F
		private void Awake()
		{
			this.purchaseInteraction = base.GetComponent<PurchaseInteraction>();
		}

		// Token: 0x06002DC2 RID: 11714 RVA: 0x000C2D90 File Offset: 0x000C0F90
		private void FixedUpdate()
		{
			if (this.indicatorObject)
			{
				this.indicatorObject.SetActive(this.purchaseInteraction.available);
			}
			if (this.disabledIndicatorObject)
			{
				this.disabledIndicatorObject.SetActive(!this.purchaseInteraction.available);
			}
			if (this.animator)
			{
				this.animator.SetBool(this.mecanimBool, this.purchaseInteraction.available);
			}
		}

		// Token: 0x04002FBB RID: 12219
		public GameObject indicatorObject;

		// Token: 0x04002FBC RID: 12220
		public GameObject disabledIndicatorObject;

		// Token: 0x04002FBD RID: 12221
		public Animator animator;

		// Token: 0x04002FBE RID: 12222
		public string mecanimBool;

		// Token: 0x04002FBF RID: 12223
		private PurchaseInteraction purchaseInteraction;
	}
}
