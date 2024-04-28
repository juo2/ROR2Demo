using System;
using UnityEngine;

namespace RoR2
{
	// Token: 0x020007E0 RID: 2016
	public class ObjectScaleCurve : MonoBehaviour
	{
		// Token: 0x170003EE RID: 1006
		// (get) Token: 0x06002B8E RID: 11150 RVA: 0x000BAD09 File Offset: 0x000B8F09
		// (set) Token: 0x06002B8F RID: 11151 RVA: 0x000BAD11 File Offset: 0x000B8F11
		public float time { get; set; }

		// Token: 0x170003EF RID: 1007
		// (get) Token: 0x06002B90 RID: 11152 RVA: 0x000BAD1A File Offset: 0x000B8F1A
		// (set) Token: 0x06002B91 RID: 11153 RVA: 0x000BAD22 File Offset: 0x000B8F22
		public Vector3 baseScale { get; set; }

		// Token: 0x06002B92 RID: 11154 RVA: 0x000BAD2B File Offset: 0x000B8F2B
		private void Awake()
		{
			this.baseScale = base.transform.localScale;
			this.Reset();
		}

		// Token: 0x06002B93 RID: 11155 RVA: 0x000BAD44 File Offset: 0x000B8F44
		private void OnEnable()
		{
			this.Reset();
		}

		// Token: 0x06002B94 RID: 11156 RVA: 0x000BAD4C File Offset: 0x000B8F4C
		public void Reset()
		{
			this.time = 0f;
			this.UpdateScale(0f);
		}

		// Token: 0x06002B95 RID: 11157 RVA: 0x000BAD64 File Offset: 0x000B8F64
		private void Update()
		{
			this.time += Time.deltaTime;
			this.UpdateScale(this.time);
		}

		// Token: 0x06002B96 RID: 11158 RVA: 0x000BAD84 File Offset: 0x000B8F84
		private void UpdateScale(float time)
		{
			float time2 = (this.timeMax > 0f) ? Mathf.Clamp01(time / this.timeMax) : 0f;
			float d = 1f;
			if (this.overallCurve != null)
			{
				d = this.overallCurve.Evaluate(time2);
			}
			Vector3 a;
			if (this.useOverallCurveOnly)
			{
				a = this.baseScale * d;
			}
			else
			{
				if (this.curveX == null || this.curveY == null || this.curveZ == null)
				{
					return;
				}
				a = new Vector3(this.curveX.Evaluate(time2) * this.baseScale.x, this.curveY.Evaluate(time2) * this.baseScale.y, this.curveZ.Evaluate(time2) * this.baseScale.z);
			}
			base.transform.localScale = a * d;
		}

		// Token: 0x04002DFF RID: 11775
		public bool useOverallCurveOnly;

		// Token: 0x04002E00 RID: 11776
		public AnimationCurve curveX;

		// Token: 0x04002E01 RID: 11777
		public AnimationCurve curveY;

		// Token: 0x04002E02 RID: 11778
		public AnimationCurve curveZ;

		// Token: 0x04002E03 RID: 11779
		public AnimationCurve overallCurve;

		// Token: 0x04002E04 RID: 11780
		public float timeMax = 5f;
	}
}
