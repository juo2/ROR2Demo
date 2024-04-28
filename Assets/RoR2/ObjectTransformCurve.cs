using System;
using UnityEngine;

namespace RoR2
{
	// Token: 0x020007E1 RID: 2017
	public class ObjectTransformCurve : MonoBehaviour
	{
		// Token: 0x170003F0 RID: 1008
		// (get) Token: 0x06002B98 RID: 11160 RVA: 0x000BAE72 File Offset: 0x000B9072
		// (set) Token: 0x06002B99 RID: 11161 RVA: 0x000BAE7A File Offset: 0x000B907A
		public float time { get; set; }

		// Token: 0x170003F1 RID: 1009
		// (get) Token: 0x06002B9A RID: 11162 RVA: 0x000BAE83 File Offset: 0x000B9083
		// (set) Token: 0x06002B9B RID: 11163 RVA: 0x000BAE8B File Offset: 0x000B908B
		public Vector3 basePosition { get; private set; }

		// Token: 0x170003F2 RID: 1010
		// (get) Token: 0x06002B9C RID: 11164 RVA: 0x000BAE94 File Offset: 0x000B9094
		// (set) Token: 0x06002B9D RID: 11165 RVA: 0x000BAE9C File Offset: 0x000B909C
		public Quaternion baseRotation { get; private set; }

		// Token: 0x06002B9E RID: 11166 RVA: 0x000BAEA5 File Offset: 0x000B90A5
		private void Awake()
		{
			this.basePosition = base.transform.localPosition;
			this.baseRotation = base.transform.localRotation;
			this.Reset();
		}

		// Token: 0x06002B9F RID: 11167 RVA: 0x000BAECF File Offset: 0x000B90CF
		private void OnEnable()
		{
			this.Reset();
		}

		// Token: 0x06002BA0 RID: 11168 RVA: 0x000BAED7 File Offset: 0x000B90D7
		public void Reset()
		{
			this.time = 0f;
			if (this.randomizeInitialTime)
			{
				this.time = UnityEngine.Random.Range(0f, this.timeMax);
			}
			this.UpdateTransform(this.time);
		}

		// Token: 0x06002BA1 RID: 11169 RVA: 0x000BAF10 File Offset: 0x000B9110
		private void Update()
		{
			this.time += Time.deltaTime;
			if (this.loop && this.time > this.timeMax)
			{
				this.time %= this.timeMax;
			}
			this.UpdateTransform(this.time);
		}

		// Token: 0x06002BA2 RID: 11170 RVA: 0x000BAF64 File Offset: 0x000B9164
		private void UpdateTransform(float time)
		{
			Vector3 basePosition = this.basePosition;
			Quaternion localRotation = this.baseRotation;
			float time2 = (this.timeMax > 0f) ? Mathf.Clamp01(time / this.timeMax) : 0f;
			if (this.useRotationCurves)
			{
				if (this.rotationCurveX == null || this.rotationCurveY == null || this.rotationCurveZ == null)
				{
					return;
				}
				localRotation = Quaternion.Euler(this.rotationCurveX.Evaluate(time2), this.rotationCurveY.Evaluate(time2), this.rotationCurveZ.Evaluate(time2));
				base.transform.localRotation = localRotation;
			}
			if (this.useTranslationCurves)
			{
				if (this.translationCurveX == null || this.translationCurveY == null || this.translationCurveZ == null)
				{
					return;
				}
				basePosition = new Vector3(this.translationCurveX.Evaluate(time2), this.translationCurveY.Evaluate(time2), this.translationCurveZ.Evaluate(time2));
				base.transform.localPosition = basePosition;
			}
		}

		// Token: 0x04002E07 RID: 11783
		public bool useRotationCurves;

		// Token: 0x04002E08 RID: 11784
		public bool useTranslationCurves;

		// Token: 0x04002E09 RID: 11785
		public bool loop;

		// Token: 0x04002E0A RID: 11786
		public bool randomizeInitialTime;

		// Token: 0x04002E0B RID: 11787
		public AnimationCurve rotationCurveX;

		// Token: 0x04002E0C RID: 11788
		public AnimationCurve rotationCurveY;

		// Token: 0x04002E0D RID: 11789
		public AnimationCurve rotationCurveZ;

		// Token: 0x04002E0E RID: 11790
		public AnimationCurve translationCurveX;

		// Token: 0x04002E0F RID: 11791
		public AnimationCurve translationCurveY;

		// Token: 0x04002E10 RID: 11792
		public AnimationCurve translationCurveZ;

		// Token: 0x04002E11 RID: 11793
		public float timeMax = 5f;
	}
}
