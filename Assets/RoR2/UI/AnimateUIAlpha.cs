using System;
using UnityEngine;
using UnityEngine.UI;

namespace RoR2.UI
{
	// Token: 0x02000CBA RID: 3258
	public class AnimateUIAlpha : MonoBehaviour
	{
		// Token: 0x06004A4C RID: 19020 RVA: 0x00131044 File Offset: 0x0012F244
		private void Start()
		{
			if (this.image)
			{
				this.originalColor = this.image.color;
			}
			if (this.rawImage)
			{
				this.originalColor = this.rawImage.color;
			}
			else if (this.spriteRenderer)
			{
				this.originalColor = this.spriteRenderer.color;
			}
			this.UpdateAlphas(0f);
		}

		// Token: 0x06004A4D RID: 19021 RVA: 0x001310B8 File Offset: 0x0012F2B8
		private void OnDisable()
		{
			this.time = 0f;
		}

		// Token: 0x06004A4E RID: 19022 RVA: 0x001310C5 File Offset: 0x0012F2C5
		private void Update()
		{
			this.UpdateAlphas(Time.unscaledDeltaTime);
		}

		// Token: 0x06004A4F RID: 19023 RVA: 0x001310D4 File Offset: 0x0012F2D4
		private void UpdateAlphas(float deltaTime)
		{
			this.time = Mathf.Min(this.timeMax, this.time + deltaTime);
			float num = this.alphaCurve.Evaluate(this.time / this.timeMax);
			Color color = new Color(this.originalColor.r, this.originalColor.g, this.originalColor.b, this.originalColor.a * num);
			if (this.image)
			{
				this.image.color = color;
			}
			if (this.rawImage)
			{
				this.rawImage.color = color;
			}
			else if (this.spriteRenderer)
			{
				this.spriteRenderer.color = color;
			}
			if (this.loopOnEnd && this.time >= this.timeMax)
			{
				this.time -= this.timeMax;
			}
			if (this.destroyOnEnd && this.time >= this.timeMax)
			{
				UnityEngine.Object.Destroy(base.gameObject);
			}
			if (this.disableGameObjectOnEnd && this.time >= this.timeMax)
			{
				base.gameObject.SetActive(false);
			}
		}

		// Token: 0x040046FB RID: 18171
		public AnimationCurve alphaCurve;

		// Token: 0x040046FC RID: 18172
		public Image image;

		// Token: 0x040046FD RID: 18173
		public RawImage rawImage;

		// Token: 0x040046FE RID: 18174
		public SpriteRenderer spriteRenderer;

		// Token: 0x040046FF RID: 18175
		public float timeMax = 5f;

		// Token: 0x04004700 RID: 18176
		public bool destroyOnEnd;

		// Token: 0x04004701 RID: 18177
		public bool loopOnEnd;

		// Token: 0x04004702 RID: 18178
		public bool disableGameObjectOnEnd;

		// Token: 0x04004703 RID: 18179
		[HideInInspector]
		public float time;

		// Token: 0x04004704 RID: 18180
		private Color originalColor;
	}
}
