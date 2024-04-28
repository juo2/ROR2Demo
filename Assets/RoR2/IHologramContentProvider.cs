using System;
using UnityEngine;

namespace RoR2
{
	// Token: 0x02000756 RID: 1878
	public interface IHologramContentProvider
	{
		// Token: 0x060026EA RID: 9962
		bool ShouldDisplayHologram(GameObject viewer);

		// Token: 0x060026EB RID: 9963
		GameObject GetHologramContentPrefab();

		// Token: 0x060026EC RID: 9964
		void UpdateHologramContent(GameObject hologramContentObject);
	}
}
