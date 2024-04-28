using System;
using UnityEngine;

namespace RoR2
{
	// Token: 0x020004CD RID: 1229
	[CreateAssetMenu(menuName = "RoR2/ArtifactCompoundDef")]
	public class ArtifactCompoundDef : ScriptableObject
	{
		// Token: 0x04001C05 RID: 7173
		public int value;

		// Token: 0x04001C06 RID: 7174
		public Material decalMaterial;

		// Token: 0x04001C07 RID: 7175
		public GameObject modelPrefab;
	}
}
