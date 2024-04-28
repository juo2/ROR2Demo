using System;
using UnityEngine;

namespace RoR2
{
	// Token: 0x020005F8 RID: 1528
	public class BodyAnimatorSmoothingParameters : MonoBehaviour
	{
		// Token: 0x040021B9 RID: 8633
		public BodyAnimatorSmoothingParameters.SmoothingParameters smoothingParameters;

		// Token: 0x040021BA RID: 8634
		public static BodyAnimatorSmoothingParameters.SmoothingParameters defaultParameters = new BodyAnimatorSmoothingParameters.SmoothingParameters
		{
			walkMagnitudeSmoothDamp = 0.2f,
			walkAngleSmoothDamp = 0.2f,
			forwardSpeedSmoothDamp = 0.1f,
			rightSpeedSmoothDamp = 0.1f,
			intoJumpTransitionTime = 0.05f,
			turnAngleSmoothDamp = 1f
		};

		// Token: 0x020005F9 RID: 1529
		[Serializable]
		public struct SmoothingParameters
		{
			// Token: 0x040021BB RID: 8635
			public float walkMagnitudeSmoothDamp;

			// Token: 0x040021BC RID: 8636
			public float walkAngleSmoothDamp;

			// Token: 0x040021BD RID: 8637
			public float forwardSpeedSmoothDamp;

			// Token: 0x040021BE RID: 8638
			public float rightSpeedSmoothDamp;

			// Token: 0x040021BF RID: 8639
			public float intoJumpTransitionTime;

			// Token: 0x040021C0 RID: 8640
			public float turnAngleSmoothDamp;
		}
	}
}
