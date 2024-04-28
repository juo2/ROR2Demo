using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace RoR2.UI
{
	// Token: 0x02000CF1 RID: 3313
	[ExecuteAlways]
	public class DifficultyBarController : MonoBehaviour
	{
		// Token: 0x06004B76 RID: 19318 RVA: 0x00135D24 File Offset: 0x00133F24
		private static Color ColorMultiplySaturationAndValue(ref Color col, float saturationMultiplier, float valueMultiplier)
		{
			float h;
			float num;
			float num2;
			Color.RGBToHSV(col, out h, out num, out num2);
			return Color.HSVToRGB(h, num * saturationMultiplier, num2 * valueMultiplier);
		}

		// Token: 0x06004B77 RID: 19319 RVA: 0x00135D50 File Offset: 0x00133F50
		private void OnCurrentSegmentIndexChanged(int newSegmentIndex)
		{
			if (!Application.isPlaying)
			{
				return;
			}
			int num = newSegmentIndex - 1;
			float width = this.viewPort.rect.width;
			int i = 0;
			int num2 = this.images.Length - 1;
			while (i < num2)
			{
				Image image = this.images[i];
				RectTransform rectTransform = image.rectTransform;
				bool enabled = rectTransform.offsetMax.x + this.scrollX >= 0f && rectTransform.offsetMin.x + this.scrollX <= width;
				image.enabled = enabled;
				i++;
			}
			int num3 = this.images.Length - 1;
			Image image2 = this.images[num3];
			bool enabled2 = image2.rectTransform.offsetMax.x + this.scrollX >= 0f;
			image2.enabled = enabled2;
			for (int j = 0; j <= num; j++)
			{
				this.images[j].color = DifficultyBarController.ColorMultiplySaturationAndValue(ref this.segmentDefs[j].color, this.pastSaturationMultiplier, this.pastValueMultiplier);
				this.labels[j].color = this.pastLabelColor;
			}
			for (int k = newSegmentIndex + 1; k < this.images.Length; k++)
			{
				this.images[k].color = DifficultyBarController.ColorMultiplySaturationAndValue(ref this.segmentDefs[k].color, this.upcomingSaturationMultiplier, this.upcomingValueMultiplier);
				this.labels[k].color = this.upcomingLabelColor;
			}
			Image image3 = (num != -1) ? this.images[num] : null;
			Image image4 = (newSegmentIndex != -1) ? this.images[newSegmentIndex] : null;
			TextMeshProUGUI textMeshProUGUI = (newSegmentIndex != -1) ? this.labels[newSegmentIndex] : null;
			if (image3)
			{
				this.playingAnimations.Add(new DifficultyBarController.SegmentImageAnimation
				{
					age = 0f,
					duration = this.fadeAnimationDuration,
					segmentImage = image3,
					colorCurve = this.fadeAnimationCurve,
					color0 = this.segmentDefs[num].color,
					color1 = DifficultyBarController.ColorMultiplySaturationAndValue(ref this.segmentDefs[num].color, this.pastSaturationMultiplier, this.pastValueMultiplier)
				});
			}
			if (image4)
			{
				this.playingAnimations.Add(new DifficultyBarController.SegmentImageAnimation
				{
					age = 0f,
					duration = this.flashAnimationDuration,
					segmentImage = image4,
					colorCurve = this.flashAnimationCurve,
					color0 = DifficultyBarController.ColorMultiplySaturationAndValue(ref this.segmentDefs[newSegmentIndex].color, this.currentSaturationMultiplier, this.currentValueMultiplier),
					color1 = Color.white
				});
			}
			if (textMeshProUGUI)
			{
				textMeshProUGUI.color = this.currentLabelColor;
			}
		}

		// Token: 0x06004B78 RID: 19320 RVA: 0x0013601C File Offset: 0x0013421C
		private void SetSegmentScroll(float segmentScroll)
		{
			float num = (float)(this.segmentDefs.Length + 2);
			if (segmentScroll > num)
			{
				segmentScroll = num - 1f + (segmentScroll - Mathf.Floor(segmentScroll));
			}
			this.scrollXRaw = (segmentScroll - 1f) * -this.elementWidth;
			this.scrollX = Mathf.Floor(this.scrollXRaw);
			int num2 = this.currentSegmentIndex;
			this.currentSegmentIndex = Mathf.Clamp(Mathf.FloorToInt(segmentScroll), 0, this.segmentContainer.childCount - 1);
			if (num2 != this.currentSegmentIndex)
			{
				this.OnCurrentSegmentIndexChanged(this.currentSegmentIndex);
			}
			Vector2 offsetMin = this.segmentContainer.offsetMin;
			offsetMin.x = this.scrollX;
			this.segmentContainer.offsetMin = offsetMin;
			if (this.segmentContainer && this.segmentContainer.childCount > 0)
			{
				int num3 = this.segmentContainer.childCount - 1;
				RectTransform rectTransform = (RectTransform)this.segmentContainer.GetChild(num3);
				RectTransform rectTransform2 = (RectTransform)rectTransform.Find("Label");
				TextMeshProUGUI textMeshProUGUI = this.labels[num3];
				if (segmentScroll >= (float)(num3 - 1))
				{
					float num4 = this.elementWidth;
					Vector2 offsetMin2 = rectTransform.offsetMin;
					offsetMin2.x = this.CalcSegmentStartX(num3);
					rectTransform.offsetMin = offsetMin2;
					Vector2 offsetMax = rectTransform.offsetMax;
					offsetMax.x = offsetMin2.x + num4;
					rectTransform.offsetMax = offsetMax;
					rectTransform2.anchorMin = new Vector2(0f, 0f);
					rectTransform2.anchorMax = new Vector2(0f, 1f);
					rectTransform2.offsetMin = new Vector2(0f, 0f);
					rectTransform2.offsetMax = new Vector2(this.elementWidth, 0f);
					return;
				}
				rectTransform.offsetMax = rectTransform.offsetMin + new Vector2(this.elementWidth, 0f);
				this.SetLabelDefaultDimensions(rectTransform2);
			}
		}

		// Token: 0x06004B79 RID: 19321 RVA: 0x001361FF File Offset: 0x001343FF
		private float CalcSegmentStartX(int i)
		{
			return (float)i * this.elementWidth;
		}

		// Token: 0x06004B7A RID: 19322 RVA: 0x0013620A File Offset: 0x0013440A
		private float CalcSegmentEndX(int i)
		{
			return (float)(i + 1) * this.elementWidth;
		}

		// Token: 0x06004B7B RID: 19323 RVA: 0x00136218 File Offset: 0x00134418
		private void SetLabelDefaultDimensions(RectTransform labelRectTransform)
		{
			labelRectTransform.anchorMin = new Vector2(0f, 0f);
			labelRectTransform.anchorMax = new Vector2(1f, 1f);
			labelRectTransform.pivot = new Vector2(0.5f, 0.5f);
			labelRectTransform.offsetMin = new Vector2(0f, 0f);
			labelRectTransform.offsetMax = new Vector2(0f, 0f);
		}

		// Token: 0x06004B7C RID: 19324 RVA: 0x00136290 File Offset: 0x00134490
		private void SetSegmentCount(uint desiredCount)
		{
			if (!this.segmentContainer || !this.segmentPrefab)
			{
				return;
			}
			uint num = (uint)this.segmentContainer.childCount;
			if (this.images == null || (long)this.images.Length != (long)((ulong)desiredCount))
			{
				this.images = new Image[desiredCount];
				this.labels = new TextMeshProUGUI[desiredCount];
			}
			int i = 0;
			int num2 = Mathf.Min(this.images.Length, this.segmentContainer.childCount);
			while (i < num2)
			{
				Transform child = this.segmentContainer.GetChild(i);
				this.images[i] = child.GetComponent<Image>();
				this.labels[i] = child.Find("Label").GetComponent<TextMeshProUGUI>();
				i++;
			}
			while (num > desiredCount)
			{
				UnityEngine.Object.DestroyImmediate(this.segmentContainer.GetChild((int)(num - 1U)).gameObject);
				num -= 1U;
			}
			while (num < desiredCount)
			{
				GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.segmentPrefab, this.segmentContainer);
				gameObject.SetActive(true);
				this.images[i] = gameObject.GetComponent<Image>();
				this.labels[i] = gameObject.transform.Find("Label").GetComponent<TextMeshProUGUI>();
				i++;
				num += 1U;
			}
		}

		// Token: 0x06004B7D RID: 19325 RVA: 0x001363C4 File Offset: 0x001345C4
		private void SetupSegments()
		{
			if (!this.segmentContainer || !this.segmentPrefab)
			{
				return;
			}
			this.SetSegmentCount((uint)this.segmentDefs.Length);
			for (int i = 0; i < this.segmentContainer.childCount; i++)
			{
				this.SetupSegment((RectTransform)this.segmentContainer.GetChild(i), ref this.segmentDefs[i], i);
			}
			this.SetupFinalSegment((RectTransform)this.segmentContainer.GetChild(this.segmentContainer.childCount - 1));
		}

		// Token: 0x06004B7E RID: 19326 RVA: 0x00136458 File Offset: 0x00134658
		private static void ScaleLabelToWidth(TextMeshProUGUI label, float width)
		{
			RectTransform rectTransform = (RectTransform)label.transform;
			float x = label.textBounds.size.x;
			Vector3 localScale = rectTransform.localScale;
			localScale.x = width / x;
			rectTransform.localScale = localScale;
		}

		// Token: 0x06004B7F RID: 19327 RVA: 0x0013649C File Offset: 0x0013469C
		private void SetupFinalSegment(RectTransform segmentTransform)
		{
			TextMeshProUGUI[] array = segmentTransform.GetComponentsInChildren<TextMeshProUGUI>();
			int num = 4;
			if (array.Length < num)
			{
				TextMeshProUGUI[] array2 = new TextMeshProUGUI[num];
				for (int i = 0; i < array.Length; i++)
				{
					array2[i] = array[i];
				}
				for (int j = array.Length; j < num; j++)
				{
					array2[j] = UnityEngine.Object.Instantiate<GameObject>(array[0].gameObject, segmentTransform).GetComponent<TextMeshProUGUI>();
				}
				array = array2;
			}
			int k = 0;
			int num2 = array.Length;
			while (k < num2)
			{
				TextMeshProUGUI textMeshProUGUI = array[k];
				textMeshProUGUI.enableWordWrapping = false;
				textMeshProUGUI.overflowMode = TextOverflowModes.Overflow;
				textMeshProUGUI.alignment = TextAlignmentOptions.MidlineLeft;
				textMeshProUGUI.text = Language.GetString(this.segmentDefs[this.segmentDefs.Length - 1].token);
				textMeshProUGUI.enableAutoSizing = true;
				Vector3 localPosition = textMeshProUGUI.transform.localPosition;
				localPosition.x = (float)k * this.elementWidth;
				textMeshProUGUI.transform.localPosition = localPosition;
				k++;
			}
			segmentTransform.GetComponent<Image>().sprite = this.finalSegmentSprite;
		}

		// Token: 0x06004B80 RID: 19328 RVA: 0x00136598 File Offset: 0x00134798
		private void SetupSegment(RectTransform segmentTransform, ref DifficultyBarController.SegmentDef segmentDef, int i)
		{
			Vector2 offsetMin = segmentTransform.offsetMin;
			Vector2 offsetMax = segmentTransform.offsetMax;
			offsetMin.x = this.CalcSegmentStartX(i);
			offsetMax.x = this.CalcSegmentEndX(i);
			segmentTransform.offsetMin = offsetMin;
			segmentTransform.offsetMax = offsetMax;
			segmentTransform.GetComponent<Image>().color = segmentDef.color;
			((RectTransform)segmentTransform.Find("Label")).GetComponent<LanguageTextMeshController>().token = segmentDef.token;
		}

		// Token: 0x06004B81 RID: 19329 RVA: 0x0013660E File Offset: 0x0013480E
		private void Awake()
		{
			this.SetupSegments();
		}

		// Token: 0x06004B82 RID: 19330 RVA: 0x00136618 File Offset: 0x00134818
		private void Update()
		{
			if (Run.instance)
			{
				this.SetSegmentScroll((Run.instance.ambientLevel - 1f) / this.levelsPerSegment);
			}
			if (Application.isPlaying)
			{
				this.RunAnimations(Time.deltaTime);
			}
			this.UpdateGears();
		}

		// Token: 0x06004B83 RID: 19331 RVA: 0x00136668 File Offset: 0x00134868
		private void UpdateGears()
		{
			foreach (RawImage rawImage in this.wormGearImages)
			{
				Rect uvRect = rawImage.uvRect;
				float num = Mathf.Sign(uvRect.width);
				uvRect.x = this.scrollXRaw * this.UVScaleToScrollX * num + ((num < 0f) ? this.gearUVOffset : 0f);
				rawImage.uvRect = uvRect;
			}
		}

		// Token: 0x06004B84 RID: 19332 RVA: 0x001366D4 File Offset: 0x001348D4
		private void RunAnimations(float deltaTime)
		{
			for (int i = this.playingAnimations.Count - 1; i >= 0; i--)
			{
				DifficultyBarController.SegmentImageAnimation segmentImageAnimation = this.playingAnimations[i];
				segmentImageAnimation.age += deltaTime;
				float num = Mathf.Clamp01(segmentImageAnimation.age / segmentImageAnimation.duration);
				segmentImageAnimation.Update(num);
				if (num >= 1f)
				{
					this.playingAnimations.RemoveAt(i);
				}
			}
		}

		// Token: 0x0400481B RID: 18459
		[Header("Component References")]
		public RectTransform viewPort;

		// Token: 0x0400481C RID: 18460
		public RectTransform segmentContainer;

		// Token: 0x0400481D RID: 18461
		[Tooltip("How wide each segment should be.")]
		[Header("Layout")]
		public float elementWidth;

		// Token: 0x0400481E RID: 18462
		public float levelsPerSegment;

		// Token: 0x0400481F RID: 18463
		public float debugTime;

		// Token: 0x04004820 RID: 18464
		[Header("Segment Parameters")]
		public DifficultyBarController.SegmentDef[] segmentDefs;

		// Token: 0x04004821 RID: 18465
		[Tooltip("The prefab to instantiate for each segment.")]
		public GameObject segmentPrefab;

		// Token: 0x04004822 RID: 18466
		[Header("Colors")]
		public float pastSaturationMultiplier;

		// Token: 0x04004823 RID: 18467
		public float pastValueMultiplier;

		// Token: 0x04004824 RID: 18468
		public Color pastLabelColor;

		// Token: 0x04004825 RID: 18469
		public float currentSaturationMultiplier;

		// Token: 0x04004826 RID: 18470
		public float currentValueMultiplier;

		// Token: 0x04004827 RID: 18471
		public Color currentLabelColor;

		// Token: 0x04004828 RID: 18472
		public float upcomingSaturationMultiplier;

		// Token: 0x04004829 RID: 18473
		public float upcomingValueMultiplier;

		// Token: 0x0400482A RID: 18474
		public Color upcomingLabelColor;

		// Token: 0x0400482B RID: 18475
		[Header("Animations")]
		public AnimationCurve fadeAnimationCurve;

		// Token: 0x0400482C RID: 18476
		public float fadeAnimationDuration = 1f;

		// Token: 0x0400482D RID: 18477
		public AnimationCurve flashAnimationCurve;

		// Token: 0x0400482E RID: 18478
		public float flashAnimationDuration = 0.5f;

		// Token: 0x0400482F RID: 18479
		private int currentSegmentIndex = -1;

		// Token: 0x04004830 RID: 18480
		private static readonly Color labelFadedColor = Color.Lerp(Color.gray, Color.white, 0.5f);

		// Token: 0x04004831 RID: 18481
		[Header("Final Segment")]
		public Sprite finalSegmentSprite;

		// Token: 0x04004832 RID: 18482
		private float scrollX;

		// Token: 0x04004833 RID: 18483
		private float scrollXRaw;

		// Token: 0x04004834 RID: 18484
		[Tooltip("Do not set this manually. Regenerate the children instead.")]
		public Image[] images;

		// Token: 0x04004835 RID: 18485
		[Tooltip("Do not set this manually. Regenerate the children instead.")]
		public TextMeshProUGUI[] labels;

		// Token: 0x04004836 RID: 18486
		public RawImage[] wormGearImages;

		// Token: 0x04004837 RID: 18487
		public float UVScaleToScrollX;

		// Token: 0x04004838 RID: 18488
		public float gearUVOffset;

		// Token: 0x04004839 RID: 18489
		private readonly List<DifficultyBarController.SegmentImageAnimation> playingAnimations = new List<DifficultyBarController.SegmentImageAnimation>();

		// Token: 0x02000CF2 RID: 3314
		[Serializable]
		public struct SegmentDef
		{
			// Token: 0x0400483A RID: 18490
			[Tooltip("The default English string to use for the element at design time.")]
			public string debugString;

			// Token: 0x0400483B RID: 18491
			[Tooltip("The final language token to use for this element at runtime.")]
			public string token;

			// Token: 0x0400483C RID: 18492
			[Tooltip("The color to use for the panel.")]
			public Color color;
		}

		// Token: 0x02000CF3 RID: 3315
		private class SegmentImageAnimation
		{
			// Token: 0x06004B87 RID: 19335 RVA: 0x0013678D File Offset: 0x0013498D
			public void Update(float t)
			{
				this.segmentImage.color = Color.Lerp(this.color0, this.color1, this.colorCurve.Evaluate(t));
			}

			// Token: 0x0400483D RID: 18493
			public Image segmentImage;

			// Token: 0x0400483E RID: 18494
			public float age;

			// Token: 0x0400483F RID: 18495
			public float duration;

			// Token: 0x04004840 RID: 18496
			public AnimationCurve colorCurve;

			// Token: 0x04004841 RID: 18497
			public Color color0;

			// Token: 0x04004842 RID: 18498
			public Color color1;
		}
	}
}
