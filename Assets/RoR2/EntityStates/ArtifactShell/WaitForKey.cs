using System;
using RoR2;

namespace EntityStates.ArtifactShell
{
	// Token: 0x02000493 RID: 1171
	public class WaitForKey : ArtifactShellBaseState
	{
		// Token: 0x17000139 RID: 313
		// (get) Token: 0x060014F7 RID: 5367 RVA: 0x0005CFF7 File Offset: 0x0005B1F7
		protected override CostTypeIndex interactionCostType
		{
			get
			{
				return CostTypeIndex.ArtifactShellKillerItem;
			}
		}

		// Token: 0x1700013A RID: 314
		// (get) Token: 0x060014F8 RID: 5368 RVA: 0x0000B4B7 File Offset: 0x000096B7
		protected override int interactionCost
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x1700013B RID: 315
		// (get) Token: 0x060014F9 RID: 5369 RVA: 0x0000B4B7 File Offset: 0x000096B7
		protected override bool interactionAvailable
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060014FA RID: 5370 RVA: 0x0005CFFB File Offset: 0x0005B1FB
		public override void OnEnter()
		{
			base.OnEnter();
		}

		// Token: 0x060014FB RID: 5371 RVA: 0x0005D003 File Offset: 0x0005B203
		protected override void OnPurchase(Interactor activator)
		{
			base.OnPurchase(activator);
			this.outer.SetInterruptState(new StartHurt(), InterruptPriority.Pain);
		}
	}
}
