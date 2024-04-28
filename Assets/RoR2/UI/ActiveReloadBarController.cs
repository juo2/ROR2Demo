using System;
using UnityEngine;

namespace RoR2.UI
{
	// Token: 0x02000CB0 RID: 3248
	public class ActiveReloadBarController : MonoBehaviour
	{
		// Token: 0x06004A17 RID: 18967 RVA: 0x001304AD File Offset: 0x0012E6AD
		public void OnEnable()
		{
			this.SetTValue(0f);
		}

		// Token: 0x06004A18 RID: 18968 RVA: 0x001304BC File Offset: 0x0012E6BC
		public void SetWindowRange(float tStart, float tEnd)
		{
			tStart = Mathf.Max(0f, tStart);
			tEnd = Mathf.Min(1f, tEnd);
			this.windowIndicatorTransform.anchorMin = new Vector2(tStart, this.windowIndicatorTransform.anchorMin.y);
			this.windowIndicatorTransform.anchorMax = new Vector2(tEnd, this.windowIndicatorTransform.anchorMax.y);
		}

		// Token: 0x06004A19 RID: 18969 RVA: 0x00130525 File Offset: 0x0012E725
		public void SetIsWindowActive(bool isWindowActive)
		{
			if (this.animator)
			{
				this.animator.SetBool(this.isWindowActiveParamName, isWindowActive);
			}
		}

		// Token: 0x06004A1A RID: 18970 RVA: 0x00130546 File Offset: 0x0012E746
		public void SetWasWindowHit(bool wasWindowHit)
		{
			if (this.animator)
			{
				this.animator.SetBool(this.wasWindowHitParamName, wasWindowHit);
			}
		}

		// Token: 0x06004A1B RID: 18971 RVA: 0x00130567 File Offset: 0x0012E767
		public void SetWasFailure(bool wasFailure)
		{
			if (this.animator)
			{
				this.animator.SetBool(this.wasFailureParamName, wasFailure);
			}
		}

		// Token: 0x06004A1C RID: 18972 RVA: 0x00130588 File Offset: 0x0012E788
		public void SetTValue(float t)
		{
			this.timeIndicatorTransform.anchorMin = new Vector2(t, this.timeIndicatorTransform.anchorMin.y);
			this.timeIndicatorTransform.anchorMax = new Vector2(t, this.timeIndicatorTransform.anchorMax.y);
		}

		// Token: 0x040046D5 RID: 18133
		[SerializeField]
		private RectTransform timeIndicatorTransform;

		// Token: 0x040046D6 RID: 18134
		[SerializeField]
		private RectTransform windowIndicatorTransform;

		// Token: 0x040046D7 RID: 18135
		[SerializeField]
		private Animator animator;

		// Token: 0x040046D8 RID: 18136
		[SerializeField]
		private string isWindowActiveParamName;

		// Token: 0x040046D9 RID: 18137
		[SerializeField]
		private string wasWindowHitParamName;

		// Token: 0x040046DA RID: 18138
		[SerializeField]
		private string wasFailureParamName;
	}
}
