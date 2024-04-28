using System;
using UnityEngine;

namespace RoR2.EntitlementManagement
{
	// Token: 0x02000C90 RID: 3216
	[CreateAssetMenu(menuName = "RoR2/EntitlementDef")]
	public class EntitlementDef : ScriptableObject
	{
		// Token: 0x06004992 RID: 18834 RVA: 0x0012E94A File Offset: 0x0012CB4A
		private void OnDisable()
		{
			this.entitlementIndex = EntitlementIndex.None;
		}

		// Token: 0x04004645 RID: 17989
		[HideInInspector]
		[SerializeField]
		public EntitlementIndex entitlementIndex = EntitlementIndex.None;

		// Token: 0x04004646 RID: 17990
		[Tooltip("The user-facing display name of this entitlement.")]
		public string nameToken;

		// Token: 0x04004647 RID: 17991
		public uint steamAppId;

		// Token: 0x04004648 RID: 17992
		[Tooltip("This is an EOS Item Id, not Offer Id.")]
		public string eosItemId;
	}
}
