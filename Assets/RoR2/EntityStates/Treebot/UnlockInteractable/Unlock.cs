using System;
using RoR2;
using RoR2.Mecanim;
using UnityEngine.Networking;

namespace EntityStates.Treebot.UnlockInteractable
{
	// Token: 0x02000189 RID: 393
	public class Unlock : BaseState
	{
		// Token: 0x14000002 RID: 2
		// (add) Token: 0x060006DB RID: 1755 RVA: 0x0001DCD4 File Offset: 0x0001BED4
		// (remove) Token: 0x060006DC RID: 1756 RVA: 0x0001DD08 File Offset: 0x0001BF08
		public static event Action<Interactor> onActivated;

		// Token: 0x060006DD RID: 1757 RVA: 0x0001DD3C File Offset: 0x0001BF3C
		public override void OnEnter()
		{
			base.OnEnter();
			if (NetworkServer.active)
			{
				Action<Interactor> action = Unlock.onActivated;
				if (action != null)
				{
					action(base.GetComponent<PurchaseInteraction>().lastActivator);
				}
			}
			base.GetModelAnimator().SetBool("Revive", true);
			base.GetModelTransform().GetComponent<RandomBlinkController>().enabled = true;
		}
	}
}
