using System;
using UnityEngine;

namespace RoR2
{
	// Token: 0x0200081B RID: 2075
	[ExecuteAlways]
	public class PositionAlongBasicBezierSpline : MonoBehaviour
	{
		// Token: 0x06002CFD RID: 11517 RVA: 0x000BFF17 File Offset: 0x000BE117
		private void Update()
		{
			if (this.curve)
			{
				base.transform.position = this.curve.Evaluate(this.normalizedPositionAlongCurve);
			}
		}

		// Token: 0x04002F26 RID: 12070
		public BasicBezierSpline curve;

		// Token: 0x04002F27 RID: 12071
		[Range(0f, 1f)]
		public float normalizedPositionAlongCurve;
	}
}
