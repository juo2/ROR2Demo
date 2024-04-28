using System;
using UnityEngine;

namespace RoR2
{
	// Token: 0x02000557 RID: 1367
	[CreateAssetMenu(menuName = "RoR2/RoachParams")]
	public class RoachParams : ScriptableObject
	{
		// Token: 0x04001E6C RID: 7788
		public float reorientTimerMin = 0.2f;

		// Token: 0x04001E6D RID: 7789
		public float reorientTimerMax = 0.5f;

		// Token: 0x04001E6E RID: 7790
		public float turnSpeed = 72f;

		// Token: 0x04001E6F RID: 7791
		public float acceleration = 400f;

		// Token: 0x04001E70 RID: 7792
		public float maxSpeed = 100f;

		// Token: 0x04001E71 RID: 7793
		public float backupDuration = 0.1f;

		// Token: 0x04001E72 RID: 7794
		public float wiggle = 720f;

		// Token: 0x04001E73 RID: 7795
		public float stepSize = 0.1f;

		// Token: 0x04001E74 RID: 7796
		public float minSimulationDuration = 3f;

		// Token: 0x04001E75 RID: 7797
		public float maxSimulationDuration = 7f;

		// Token: 0x04001E76 RID: 7798
		public float chanceToFinishOnBump = 0.5f;

		// Token: 0x04001E77 RID: 7799
		public float keyframeInterval = 0.06666667f;

		// Token: 0x04001E78 RID: 7800
		public float minReactionTime;

		// Token: 0x04001E79 RID: 7801
		public float maxReactionTime = 0.2f;

		// Token: 0x04001E7A RID: 7802
		public GameObject roachPrefab;
	}
}
