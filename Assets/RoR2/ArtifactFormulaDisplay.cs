using System;
using ThreeEyedGames;
using UnityEngine;

namespace RoR2
{
	// Token: 0x020005D7 RID: 1495
	public class ArtifactFormulaDisplay : MonoBehaviour
	{
		// Token: 0x06001B1C RID: 6940 RVA: 0x00074650 File Offset: 0x00072850
		private void Start()
		{
			foreach (ArtifactFormulaDisplay.ArtifactCompoundDisplayInfo artifactCompoundDisplayInfo in this.artifactCompoundDisplayInfos)
			{
				artifactCompoundDisplayInfo.decal.Material = artifactCompoundDisplayInfo.artifactCompoundDef.decalMaterial;
			}
		}

		// Token: 0x04002140 RID: 8512
		public ArtifactFormulaDisplay.ArtifactCompoundDisplayInfo[] artifactCompoundDisplayInfos;

		// Token: 0x020005D8 RID: 1496
		[Serializable]
		public struct ArtifactCompoundDisplayInfo
		{
			// Token: 0x04002141 RID: 8513
			public ArtifactCompoundDef artifactCompoundDef;

			// Token: 0x04002142 RID: 8514
			public Decal decal;
		}
	}
}
