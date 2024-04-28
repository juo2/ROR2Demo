using System;
using UnityEngine;

namespace RoR2
{
	// Token: 0x020006A6 RID: 1702
	public class DirectorPlacementRule
	{
		// Token: 0x170002A7 RID: 679
		// (get) Token: 0x0600212E RID: 8494 RVA: 0x0008E998 File Offset: 0x0008CB98
		public Vector3 targetPosition
		{
			get
			{
				if (!this.spawnOnTarget)
				{
					return this.position;
				}
				return this.spawnOnTarget.position;
			}
		}

		// Token: 0x04002681 RID: 9857
		public Transform spawnOnTarget;

		// Token: 0x04002682 RID: 9858
		public Vector3 position;

		// Token: 0x04002683 RID: 9859
		public DirectorPlacementRule.PlacementMode placementMode;

		// Token: 0x04002684 RID: 9860
		public float minDistance;

		// Token: 0x04002685 RID: 9861
		public float maxDistance;

		// Token: 0x04002686 RID: 9862
		public bool preventOverhead;

		// Token: 0x020006A7 RID: 1703
		public enum PlacementMode
		{
			// Token: 0x04002688 RID: 9864
			Direct,
			// Token: 0x04002689 RID: 9865
			Approximate,
			// Token: 0x0400268A RID: 9866
			ApproximateSimple,
			// Token: 0x0400268B RID: 9867
			NearestNode,
			// Token: 0x0400268C RID: 9868
			Random,
			// Token: 0x0400268D RID: 9869
			RandomNormalized
		}
	}
}
