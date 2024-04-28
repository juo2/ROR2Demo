using System;
using RoR2.EntitlementManagement;
using UnityEngine;

namespace RoR2.ExpansionManagement
{
	// Token: 0x02000C7E RID: 3198
	[CreateAssetMenu(menuName = "RoR2/ExpansionDef")]
	public class ExpansionDef : ScriptableObject
	{
		// Token: 0x0400460B RID: 17931
		[HideInInspector]
		[SerializeField]
		public ExpansionIndex expansionIndex;

		// Token: 0x0400460C RID: 17932
		[Tooltip("The entitlement required to use this expansion.")]
		public EntitlementDef requiredEntitlement;

		// Token: 0x0400460D RID: 17933
		[Tooltip("The token for the user-facing name of this expansion.")]
		public string nameToken;

		// Token: 0x0400460E RID: 17934
		[Tooltip("The token for the user-facing description of this expansion.")]
		public string descriptionToken;

		// Token: 0x0400460F RID: 17935
		[ShowThumbnail]
		[Tooltip("The icon for this expansion.")]
		public Sprite iconSprite;

		// Token: 0x04004610 RID: 17936
		[ShowThumbnail]
		[Tooltip("The icon to display when this expansion is disabled.")]
		public Sprite disabledIconSprite;

		// Token: 0x04004611 RID: 17937
		public RuleChoiceDef enabledChoice;

		// Token: 0x04004612 RID: 17938
		[Tooltip("This prefab is instantiated and childed to the run")]
		public GameObject runBehaviorPrefab;
	}
}
