using System;
using UnityEngine;

namespace RoR2.Achievements
{
	// Token: 0x02000E7B RID: 3707
	[RegisterAchievement("CaptainBuyMegaDrone", "Skills.Captain.CaptainSupplyDropHacking", "CompleteMainEnding", typeof(CaptainBuyMegaDroneAchievement.CaptainBuyMegaDroneServerAchievement))]
	public class CaptainBuyMegaDroneAchievement : BaseAchievement
	{
		// Token: 0x060054E4 RID: 21732 RVA: 0x0015D615 File Offset: 0x0015B815
		protected override BodyIndex LookUpRequiredBodyIndex()
		{
			return BodyCatalog.FindBodyIndex("CaptainBody");
		}

		// Token: 0x060054E5 RID: 21733 RVA: 0x0015D621 File Offset: 0x0015B821
		protected override void OnBodyRequirementMet()
		{
			base.OnBodyRequirementMet();
			base.SetServerTracked(true);
		}

		// Token: 0x060054E6 RID: 21734 RVA: 0x0015D630 File Offset: 0x0015B830
		protected override void OnBodyRequirementBroken()
		{
			base.SetServerTracked(false);
			base.OnBodyRequirementBroken();
		}

		// Token: 0x02000E7C RID: 3708
		private class CaptainBuyMegaDroneServerAchievement : BaseServerAchievement
		{
			// Token: 0x060054E8 RID: 21736 RVA: 0x0015D63F File Offset: 0x0015B83F
			public override void OnInstall()
			{
				base.OnInstall();
				this.megaDroneMasterPrefab = MasterCatalog.FindMasterPrefab("MegaDroneMaster");
				GlobalEventManager.OnInteractionsGlobal += this.OnInteractionsGlobal;
			}

			// Token: 0x060054E9 RID: 21737 RVA: 0x0015D668 File Offset: 0x0015B868
			public override void OnUninstall()
			{
				GlobalEventManager.OnInteractionsGlobal -= this.OnInteractionsGlobal;
				this.megaDroneMasterPrefab = null;
				base.OnUninstall();
			}

			// Token: 0x060054EA RID: 21738 RVA: 0x0015D688 File Offset: 0x0015B888
			private void OnInteractionsGlobal(Interactor interactor, IInteractable interactable, GameObject interactableObject)
			{
				if (base.IsCurrentBody(interactor.gameObject))
				{
					SummonMasterBehavior component = interactableObject.GetComponent<SummonMasterBehavior>();
					if (((component != null) ? component.masterPrefab : null) == this.megaDroneMasterPrefab)
					{
						base.Grant();
					}
				}
			}

			// Token: 0x04005046 RID: 20550
			private GameObject megaDroneMasterPrefab;
		}
	}
}
