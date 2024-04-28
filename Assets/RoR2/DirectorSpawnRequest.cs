using System;
using UnityEngine;

namespace RoR2
{
	// Token: 0x020006A1 RID: 1697
	public class DirectorSpawnRequest
	{
		// Token: 0x0600211D RID: 8477 RVA: 0x0008DF57 File Offset: 0x0008C157
		public DirectorSpawnRequest(SpawnCard spawnCard, DirectorPlacementRule placementRule, Xoroshiro128Plus rng)
		{
			this.spawnCard = spawnCard;
			this.placementRule = placementRule;
			this.rng = rng;
		}

		// Token: 0x04002670 RID: 9840
		public SpawnCard spawnCard;

		// Token: 0x04002671 RID: 9841
		public DirectorPlacementRule placementRule;

		// Token: 0x04002672 RID: 9842
		public Xoroshiro128Plus rng;

		// Token: 0x04002673 RID: 9843
		public GameObject summonerBodyObject;

		// Token: 0x04002674 RID: 9844
		public TeamIndex? teamIndexOverride;

		// Token: 0x04002675 RID: 9845
		public bool ignoreTeamMemberLimit;

		// Token: 0x04002676 RID: 9846
		public Action<SpawnCard.SpawnResult> onSpawnedServer;
	}
}
