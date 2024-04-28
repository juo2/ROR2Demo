using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace RoR2
{
	// Token: 0x02000820 RID: 2080
	public class PostProcessDuration : MonoBehaviour
	{
		// Token: 0x06002D08 RID: 11528 RVA: 0x000C01CE File Offset: 0x000BE3CE
		private void Update()
		{
			this.stopwatch += Time.deltaTime;
			this.UpdatePostProccess();
		}

		// Token: 0x06002D09 RID: 11529 RVA: 0x000C01E8 File Offset: 0x000BE3E8
		private void Awake()
		{
			this.UpdatePostProccess();
		}

		// Token: 0x06002D0A RID: 11530 RVA: 0x000C01F0 File Offset: 0x000BE3F0
		private void OnEnable()
		{
			this.stopwatch = 0f;
		}

		// Token: 0x06002D0B RID: 11531 RVA: 0x000C0200 File Offset: 0x000BE400
		private void UpdatePostProccess()
		{
			float num = Mathf.Clamp01(this.stopwatch / this.maxDuration);
			this.ppVolume.weight = this.ppWeightCurve.Evaluate(num);
			if (num == 1f && this.destroyOnEnd)
			{
				UnityEngine.Object.Destroy(this.ppVolume.gameObject);
			}
		}

		// Token: 0x06002D0C RID: 11532 RVA: 0x000C0257 File Offset: 0x000BE457
		private void OnValidate()
		{
			if (this.maxDuration <= Mathf.Epsilon)
			{
				Debug.LogErrorFormat("{0} has PP of time zero!", new object[]
				{
					base.gameObject
				});
			}
		}

		// Token: 0x04002F39 RID: 12089
		public PostProcessVolume ppVolume;

		// Token: 0x04002F3A RID: 12090
		public AnimationCurve ppWeightCurve;

		// Token: 0x04002F3B RID: 12091
		public float maxDuration;

		// Token: 0x04002F3C RID: 12092
		public bool destroyOnEnd;

		// Token: 0x04002F3D RID: 12093
		private float stopwatch;
	}
}
