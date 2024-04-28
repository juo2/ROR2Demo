using System;
using UnityEngine;
using UnityEngine.UI;

namespace RoR2.UI
{
	// Token: 0x02000D7A RID: 3450
	[RequireComponent(typeof(RectTransform))]
	[ExecuteAlways]
	public class ScaleToFitParentRect : MonoBehaviour, ILayoutSelfController, ILayoutController
	{
		// Token: 0x06004F15 RID: 20245 RVA: 0x00147404 File Offset: 0x00145604
		private void CacheComponents()
		{
			this.rectTransform = (RectTransform)base.transform;
		}

		// Token: 0x06004F16 RID: 20246 RVA: 0x00147417 File Offset: 0x00145617
		private void Awake()
		{
			this.CacheComponents();
			this.parentRectTransform = (this.rectTransform.parent as RectTransform);
		}

		// Token: 0x06004F17 RID: 20247 RVA: 0x000026ED File Offset: 0x000008ED
		private void Start()
		{
		}

		// Token: 0x06004F18 RID: 20248 RVA: 0x00147435 File Offset: 0x00145635
		private void OnTransformParentChanged()
		{
			this.parentRectTransform = (this.rectTransform.parent as RectTransform);
		}

		// Token: 0x06004F19 RID: 20249 RVA: 0x00147450 File Offset: 0x00145650
		private void RecalculateScale()
		{
			if (!this.parentRectTransform)
			{
				return;
			}
			this.parentSize = this.parentRectTransform.rect.size;
			float a = this.parentSize.x / this.referenceSize.x;
			float b = this.parentSize.y / this.referenceSize.y;
			if (this.fitToWidth && this.fitToHeight)
			{
				this.desiredScale = Mathf.Min(a, b);
			}
			else if (this.fitToWidth)
			{
				this.desiredScale = a;
			}
			else if (this.fitToHeight)
			{
				this.desiredScale = b;
			}
			else
			{
				this.desiredScale = 1f;
			}
			this.postScaleStretchSize = this.parentSize / this.desiredScale;
			if (float.IsNaN(this.postScaleStretchSize.x))
			{
				this.postScaleStretchSize.x = 1f;
			}
			if (float.IsNaN(this.postScaleStretchSize.y))
			{
				this.postScaleStretchSize.y = 1f;
			}
		}

		// Token: 0x06004F1A RID: 20250 RVA: 0x0014755B File Offset: 0x0014575B
		private void OnRectTransformDimensionsChange()
		{
			if (!this.inApply)
			{
				this.Apply();
			}
		}

		// Token: 0x06004F1B RID: 20251 RVA: 0x0014756C File Offset: 0x0014576C
		public void SetLayoutHorizontal()
		{
			this.RecalculateScale();
			Vector3 localScale = this.rectTransform.localScale;
			if (localScale.x != this.desiredScale)
			{
				localScale.x = this.desiredScale;
				this.rectTransform.localScale = localScale;
			}
			if (this.stretchUnfittedAxis && !this.fitToWidth && this.rectTransform.rect.width != this.postScaleStretchSize.x)
			{
				this.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, this.postScaleStretchSize.x);
			}
		}

		// Token: 0x06004F1C RID: 20252 RVA: 0x001475FC File Offset: 0x001457FC
		public void SetLayoutVertical()
		{
			this.RecalculateScale();
			Vector3 localScale = this.rectTransform.localScale;
			if (localScale.y != this.desiredScale)
			{
				localScale.y = this.desiredScale;
				this.rectTransform.localScale = localScale;
			}
			if (this.stretchUnfittedAxis && !this.fitToHeight && this.rectTransform.rect.height != this.postScaleStretchSize.y)
			{
				this.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, this.postScaleStretchSize.y);
			}
		}

		// Token: 0x06004F1D RID: 20253 RVA: 0x0014768C File Offset: 0x0014588C
		public void Apply()
		{
			if (!base.enabled || this.rectTransform == null)
			{
				return;
			}
			this.RecalculateScale();
			this.inApply = true;
			Vector3 localScale = this.rectTransform.localScale;
			if (localScale.x != this.desiredScale && this.desiredScale != 0f)
			{
				localScale.x = this.desiredScale;
				localScale.y = this.desiredScale;
				this.rectTransform.localScale = localScale;
			}
			if (this.stretchUnfittedAxis)
			{
				Rect rect = this.rectTransform.rect;
				if (!this.fitToWidth && rect.width != this.postScaleStretchSize.x)
				{
					this.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, this.postScaleStretchSize.x);
				}
				if (!this.fitToWidth && rect.height != this.postScaleStretchSize.y)
				{
					this.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, this.postScaleStretchSize.y);
				}
			}
			this.inApply = false;
		}

		// Token: 0x06004F1E RID: 20254 RVA: 0x00147788 File Offset: 0x00145988
		private void OnValidate()
		{
			this.CacheComponents();
			this.parentRectTransform = (this.rectTransform.parent as RectTransform);
			if (this.autoReferenceSize)
			{
				this.referenceSize = this.rectTransform.rect.size;
			}
			this.Apply();
		}

		// Token: 0x04004BCB RID: 19403
		private RectTransform rectTransform;

		// Token: 0x04004BCC RID: 19404
		private RectTransform parentRectTransform;

		// Token: 0x04004BCD RID: 19405
		public bool fitToWidth = true;

		// Token: 0x04004BCE RID: 19406
		public bool fitToHeight = true;

		// Token: 0x04004BCF RID: 19407
		public bool stretchUnfittedAxis;

		// Token: 0x04004BD0 RID: 19408
		public Vector2 referenceSize = Vector2.zero;

		// Token: 0x04004BD1 RID: 19409
		private Vector2 parentSize = Vector2.zero;

		// Token: 0x04004BD2 RID: 19410
		private Vector2 postScaleStretchSize = Vector2.zero;

		// Token: 0x04004BD3 RID: 19411
		private float desiredScale = 1f;

		// Token: 0x04004BD4 RID: 19412
		public bool autoReferenceSize = true;

		// Token: 0x04004BD5 RID: 19413
		private bool inApply;
	}
}
