using System;
using UnityEngine;

namespace RoR2
{
	// Token: 0x020008B3 RID: 2227
	public class TeamAreaIndicator : MonoBehaviour
	{
		// Token: 0x06003179 RID: 12665 RVA: 0x000D2118 File Offset: 0x000D0318
		private void Start()
		{
			if (this.teamFilter || this.teamComponent)
			{
				TeamIndex teamIndex = this.teamFilter ? this.teamFilter.teamIndex : (this.teamComponent ? this.teamComponent.teamIndex : TeamIndex.None);
				for (int i = 0; i < this.teamMaterialPairs.Length; i++)
				{
					if (this.teamMaterialPairs[i].teamIndex == teamIndex)
					{
						Renderer[] array = this.areaIndicatorRenderers;
						for (int j = 0; j < array.Length; j++)
						{
							array[j].sharedMaterial = this.teamMaterialPairs[i].sharedMaterial;
						}
					}
				}
				return;
			}
			Debug.LogWarning("No TeamFilter or TeamComponent assigned to TeamAreaIndicator.");
			base.gameObject.SetActive(false);
		}

		// Token: 0x04003306 RID: 13062
		public TeamComponent teamComponent;

		// Token: 0x04003307 RID: 13063
		public TeamFilter teamFilter;

		// Token: 0x04003308 RID: 13064
		public TeamAreaIndicator.TeamMaterialPair[] teamMaterialPairs;

		// Token: 0x04003309 RID: 13065
		public Renderer[] areaIndicatorRenderers;

		// Token: 0x020008B4 RID: 2228
		[Serializable]
		public struct TeamMaterialPair
		{
			// Token: 0x0400330A RID: 13066
			public TeamIndex teamIndex;

			// Token: 0x0400330B RID: 13067
			public Material sharedMaterial;
		}
	}
}
