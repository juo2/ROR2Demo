using System;
using RoR2.EntitlementManagement;
using UnityEngine;

namespace RoR2.ExpansionManagement
{
	// Token: 0x02000C7F RID: 3199
	public class ExpansionRequirementComponent : MonoBehaviour
	{
		// Token: 0x06004941 RID: 18753 RVA: 0x0012DCB8 File Offset: 0x0012BEB8
		private void Start()
		{
			CharacterBody component = base.GetComponent<CharacterBody>();
			if (component && component.isPlayerControlled && !this.PlayerCanUseBody(component.master.playerCharacterMasterController))
			{
				UnityEngine.Object.Destroy(base.gameObject);
			}
		}

		// Token: 0x06004942 RID: 18754 RVA: 0x0012DCFC File Offset: 0x0012BEFC
		public bool PlayerCanUseBody(PlayerCharacterMasterController playerCharacterMasterController)
		{
			Run instance = Run.instance;
			if (!instance)
			{
				return false;
			}
			if (this.requiredExpansion)
			{
				if (!instance.IsExpansionEnabled(this.requiredExpansion))
				{
					return false;
				}
				if (this.requireEntitlementIfPlayerControlled)
				{
					EntitlementDef requiredEntitlement = this.requiredExpansion.requiredEntitlement;
					if (requiredEntitlement)
					{
						PlayerCharacterMasterControllerEntitlementTracker component = playerCharacterMasterController.GetComponent<PlayerCharacterMasterControllerEntitlementTracker>();
						if (!component)
						{
							Debug.LogWarning("Rejecting body because the playerCharacterMasterController doesn't have a sibling PlayerCharacterMasterControllerEntitlementTracker");
							return false;
						}
						if (!component.HasEntitlement(requiredEntitlement))
						{
							Debug.LogWarning("Rejecting body because the player doesn't have entitlement " + requiredEntitlement.name);
							return false;
						}
					}
				}
			}
			return true;
		}

		// Token: 0x04004613 RID: 17939
		public ExpansionDef requiredExpansion;

		// Token: 0x04004614 RID: 17940
		public bool requireEntitlementIfPlayerControlled;
	}
}
