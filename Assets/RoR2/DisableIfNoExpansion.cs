using System;
using RoR2.EntitlementManagement;
using RoR2.ExpansionManagement;
using UnityEngine;

namespace RoR2
{
	// Token: 0x020006AC RID: 1708
	public class DisableIfNoExpansion : MonoBehaviour
	{
		// Token: 0x0600213B RID: 8507 RVA: 0x0008EC53 File Offset: 0x0008CE53
		private void OnEnable()
		{
			if (this.expansionDef && !EntitlementManager.localUserEntitlementTracker.AnyUserHasEntitlement(this.expansionDef.requiredEntitlement))
			{
				base.gameObject.SetActive(false);
			}
		}

		// Token: 0x0400269B RID: 9883
		[SerializeField]
		private ExpansionDef expansionDef;
	}
}
