using System;
using UnityEngine;

namespace RoR2
{
	// Token: 0x0200054C RID: 1356
	[CreateAssetMenu(menuName = "RoR2/ItemRelationshipProvider")]
	public class ItemRelationshipProvider : ScriptableObject
	{
		// Token: 0x04001E47 RID: 7751
		public ItemRelationshipType relationshipType;

		// Token: 0x04001E48 RID: 7752
		public ItemDef.Pair[] relationships;
	}
}
