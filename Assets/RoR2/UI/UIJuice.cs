using System;
using UnityEngine;

namespace RoR2.UI
{
	// Token: 0x02000DA4 RID: 3492
	public class UIJuice : MonoBehaviour
	{
		// Token: 0x06004FF9 RID: 20473 RVA: 0x0014B2B3 File Offset: 0x001494B3
		private void Awake()
		{
			this.InitializeFirstTimeInfo();
		}

		// Token: 0x06004FFA RID: 20474 RVA: 0x0014B2BB File Offset: 0x001494BB
		private void Update()
		{
			this.transitionStopwatch = Mathf.Min(this.transitionStopwatch + Time.unscaledDeltaTime, this.transitionDuration);
			this.ProcessTransition();
		}

		// Token: 0x06004FFB RID: 20475 RVA: 0x0014B2E0 File Offset: 0x001494E0
		private void ProcessTransition()
		{
			this.InitializeFirstTimeInfo();
			bool flag = this.transitionStopwatch < this.transitionDuration;
			if (flag || flag != this.wasTransition)
			{
				if (flag)
				{
					AnimationCurve animationCurve = AnimationCurve.EaseInOut(0f, this.transitionStartAlpha, 1f, this.transitionEndAlpha);
					if (this.canvasGroup)
					{
						this.canvasGroup.alpha = animationCurve.Evaluate(this.transitionStopwatch / this.transitionDuration);
					}
					AnimationCurve animationCurve2 = new AnimationCurve();
					Keyframe key = new Keyframe(0f, 0f, 3f, 3f);
					Keyframe key2 = new Keyframe(1f, 1f, 0f, 0f);
					animationCurve2.AddKey(key);
					animationCurve2.AddKey(key2);
					Vector2 anchoredPosition = Vector2.Lerp(this.transitionStartPosition, this.transitionEndPosition, animationCurve2.Evaluate(this.transitionStopwatch / this.transitionDuration));
					Vector2 sizeDelta = Vector2.Lerp(this.transitionStartSize, this.transitionEndSize, animationCurve2.Evaluate(this.transitionStopwatch / this.transitionDuration));
					if (this.panningRect)
					{
						this.panningRect.anchoredPosition = anchoredPosition;
						this.panningRect.sizeDelta = sizeDelta;
					}
				}
				else
				{
					if (this.canvasGroup)
					{
						this.canvasGroup.alpha = this.transitionEndAlpha;
					}
					if (this.panningRect)
					{
						this.panningRect.anchoredPosition = this.transitionEndPosition;
						this.panningRect.sizeDelta = this.transitionEndSize;
					}
					if (this.destroyOnEndOfTransition)
					{
						UnityEngine.Object.Destroy(base.gameObject);
					}
				}
			}
			this.wasTransition = flag;
		}

		// Token: 0x06004FFC RID: 20476 RVA: 0x0014B488 File Offset: 0x00149688
		public void TransitionScaleUpWidth()
		{
			this.InitializeFirstTimeInfo();
			if (this.panningRect)
			{
				this.transitionStartSize = new Vector2(0f, this.transitionEndSize.y * 0.8f);
				this.transitionEndSize = this.originalSize;
			}
			this.BeginTransition();
		}

		// Token: 0x06004FFD RID: 20477 RVA: 0x0014B4E0 File Offset: 0x001496E0
		public void TransitionPanFromLeft()
		{
			this.InitializeFirstTimeInfo();
			if (this.panningRect)
			{
				this.transitionStartPosition = new Vector2(-1f, 0f) * this.panningMagnitude;
				this.transitionEndPosition = this.originalPosition;
			}
			this.BeginTransition();
		}

		// Token: 0x06004FFE RID: 20478 RVA: 0x0014B534 File Offset: 0x00149734
		public void TransitionPanToLeft()
		{
			this.InitializeFirstTimeInfo();
			if (this.panningRect)
			{
				this.transitionStartPosition = this.originalPosition;
				this.transitionEndPosition = new Vector2(-1f, 0f) * this.panningMagnitude;
			}
			this.BeginTransition();
		}

		// Token: 0x06004FFF RID: 20479 RVA: 0x0014B588 File Offset: 0x00149788
		public void TransitionPanFromRight()
		{
			this.InitializeFirstTimeInfo();
			if (this.panningRect)
			{
				this.transitionStartPosition = new Vector2(1f, 0f) * this.panningMagnitude;
				this.transitionEndPosition = this.originalPosition;
			}
			this.BeginTransition();
		}

		// Token: 0x06005000 RID: 20480 RVA: 0x0014B5DC File Offset: 0x001497DC
		public void TransitionPanToRight()
		{
			this.InitializeFirstTimeInfo();
			if (this.panningRect)
			{
				this.transitionStartPosition = this.originalPosition;
				this.transitionEndPosition = new Vector2(1f, 0f) * this.panningMagnitude;
			}
			this.BeginTransition();
		}

		// Token: 0x06005001 RID: 20481 RVA: 0x0014B630 File Offset: 0x00149830
		public void TransitionPanFromTop()
		{
			this.InitializeFirstTimeInfo();
			if (this.panningRect)
			{
				this.transitionStartPosition = new Vector2(0f, 1f) * this.panningMagnitude;
				this.transitionEndPosition = this.originalPosition;
			}
			this.BeginTransition();
		}

		// Token: 0x06005002 RID: 20482 RVA: 0x0014B684 File Offset: 0x00149884
		public void TransitionPanToTop()
		{
			this.InitializeFirstTimeInfo();
			if (this.panningRect)
			{
				this.transitionStartPosition = this.originalPosition;
				this.transitionEndPosition = new Vector2(0f, 1f) * this.panningMagnitude;
			}
			this.BeginTransition();
		}

		// Token: 0x06005003 RID: 20483 RVA: 0x0014B6D8 File Offset: 0x001498D8
		public void TransitionPanFromBottom()
		{
			this.InitializeFirstTimeInfo();
			if (this.panningRect)
			{
				this.transitionStartPosition = new Vector2(0f, -1f) * this.panningMagnitude;
				this.transitionEndPosition = this.originalPosition;
			}
			this.BeginTransition();
		}

		// Token: 0x06005004 RID: 20484 RVA: 0x0014B72C File Offset: 0x0014992C
		public void TransitionPanToBottom()
		{
			this.InitializeFirstTimeInfo();
			if (this.panningRect)
			{
				this.transitionStartPosition = this.originalPosition;
				this.transitionEndPosition = new Vector2(0f, -1f) * this.panningMagnitude;
			}
			this.BeginTransition();
		}

		// Token: 0x06005005 RID: 20485 RVA: 0x0014B77E File Offset: 0x0014997E
		public void TransitionAlphaFadeIn()
		{
			this.InitializeFirstTimeInfo();
			this.transitionStartAlpha = 0f;
			this.transitionEndAlpha = this.originalAlpha;
			this.BeginTransition();
		}

		// Token: 0x06005006 RID: 20486 RVA: 0x0014B7A3 File Offset: 0x001499A3
		public void TransitionAlphaFadeOut()
		{
			this.InitializeFirstTimeInfo();
			this.transitionStartAlpha = this.originalAlpha;
			this.transitionEndAlpha = 0f;
			this.BeginTransition();
		}

		// Token: 0x06005007 RID: 20487 RVA: 0x0014B7C8 File Offset: 0x001499C8
		public void DestroyOnEndOfTransition(bool set)
		{
			this.destroyOnEndOfTransition = set;
		}

		// Token: 0x06005008 RID: 20488 RVA: 0x0014B7D1 File Offset: 0x001499D1
		private void BeginTransition()
		{
			this.transitionStopwatch = 0f;
			this.ProcessTransition();
		}

		// Token: 0x06005009 RID: 20489 RVA: 0x0014B7E4 File Offset: 0x001499E4
		private void InitializeFirstTimeInfo()
		{
			if (this.hasInitialized)
			{
				return;
			}
			if (this.panningRect)
			{
				this.originalPosition = this.panningRect.anchoredPosition;
				this.originalSize = this.panningRect.sizeDelta;
			}
			if (this.canvasGroup)
			{
				this.originalAlpha = this.canvasGroup.alpha;
				this.transitionEndAlpha = this.originalAlpha;
				this.transitionStartAlpha = this.originalAlpha;
			}
			this.hasInitialized = true;
		}

		// Token: 0x04004CAA RID: 19626
		[Header("Transition Settings")]
		public CanvasGroup canvasGroup;

		// Token: 0x04004CAB RID: 19627
		public RectTransform panningRect;

		// Token: 0x04004CAC RID: 19628
		public float transitionDuration;

		// Token: 0x04004CAD RID: 19629
		public float panningMagnitude;

		// Token: 0x04004CAE RID: 19630
		public bool destroyOnEndOfTransition;

		// Token: 0x04004CAF RID: 19631
		private float transitionStopwatch;

		// Token: 0x04004CB0 RID: 19632
		private float transitionEndAlpha;

		// Token: 0x04004CB1 RID: 19633
		private float transitionStartAlpha;

		// Token: 0x04004CB2 RID: 19634
		private float originalAlpha;

		// Token: 0x04004CB3 RID: 19635
		private Vector2 transitionStartPosition;

		// Token: 0x04004CB4 RID: 19636
		private Vector2 transitionEndPosition;

		// Token: 0x04004CB5 RID: 19637
		private Vector2 originalPosition;

		// Token: 0x04004CB6 RID: 19638
		private Vector2 transitionStartSize;

		// Token: 0x04004CB7 RID: 19639
		private Vector2 transitionEndSize;

		// Token: 0x04004CB8 RID: 19640
		private Vector3 originalSize;

		// Token: 0x04004CB9 RID: 19641
		private bool wasTransition;

		// Token: 0x04004CBA RID: 19642
		private bool hasInitialized;
	}
}
