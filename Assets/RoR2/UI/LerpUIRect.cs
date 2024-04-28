using System;
using UnityEngine;

namespace RoR2.UI
{
	// Token: 0x02000D2C RID: 3372
	[RequireComponent(typeof(RectTransform))]
	public class LerpUIRect : MonoBehaviour
	{
		// Token: 0x06004CF1 RID: 19697 RVA: 0x0013D902 File Offset: 0x0013BB02
		private void Start()
		{
			this.rectTransform = base.GetComponent<RectTransform>();
		}

		// Token: 0x06004CF2 RID: 19698 RVA: 0x0013D910 File Offset: 0x0013BB10
		private void OnDisable()
		{
			this.lerpState = LerpUIRect.LerpState.Entering;
			this.stopwatch = 0f;
			this.UpdateLerp();
		}

		// Token: 0x06004CF3 RID: 19699 RVA: 0x0013D92A File Offset: 0x0013BB2A
		private void Update()
		{
			this.stopwatch += Time.deltaTime;
			this.UpdateLerp();
		}

		// Token: 0x06004CF4 RID: 19700 RVA: 0x0013D944 File Offset: 0x0013BB44
		private void UpdateLerp()
		{
			LerpUIRect.LerpState lerpState = this.lerpState;
			if (lerpState != LerpUIRect.LerpState.Entering)
			{
				if (lerpState != LerpUIRect.LerpState.Leaving)
				{
					return;
				}
				float num = this.stopwatch / this.enterDuration;
				float t = this.leavingCurve.Evaluate(num);
				this.rectTransform.anchoredPosition = Vector3.LerpUnclamped(this.finalLocalPosition, this.startLocalPosition, t);
				if (num >= 1f)
				{
					this.lerpState = LerpUIRect.LerpState.Holding;
					this.stopwatch = 0f;
				}
			}
			else
			{
				float num = this.stopwatch / this.enterDuration;
				float t = this.enterCurve.Evaluate(num);
				this.rectTransform.anchoredPosition = Vector3.LerpUnclamped(this.startLocalPosition, this.finalLocalPosition, t);
				if (num >= 1f)
				{
					this.lerpState = LerpUIRect.LerpState.Holding;
					this.stopwatch = 0f;
					return;
				}
			}
		}

		// Token: 0x040049ED RID: 18925
		public Vector3 startLocalPosition;

		// Token: 0x040049EE RID: 18926
		public Vector3 finalLocalPosition;

		// Token: 0x040049EF RID: 18927
		public LerpUIRect.LerpState lerpState;

		// Token: 0x040049F0 RID: 18928
		public AnimationCurve enterCurve;

		// Token: 0x040049F1 RID: 18929
		public float enterDuration;

		// Token: 0x040049F2 RID: 18930
		public AnimationCurve leavingCurve;

		// Token: 0x040049F3 RID: 18931
		public float leaveDuration;

		// Token: 0x040049F4 RID: 18932
		private float stopwatch;

		// Token: 0x040049F5 RID: 18933
		private RectTransform rectTransform;

		// Token: 0x02000D2D RID: 3373
		public enum LerpState
		{
			// Token: 0x040049F7 RID: 18935
			Entering,
			// Token: 0x040049F8 RID: 18936
			Holding,
			// Token: 0x040049F9 RID: 18937
			Leaving
		}
	}
}
