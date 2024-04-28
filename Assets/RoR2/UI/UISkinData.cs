using System;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace RoR2.UI
{
	// Token: 0x02000CA3 RID: 3235
	[CreateAssetMenu(menuName = "RoR2/UISkinData")]
	public class UISkinData : ScriptableObject
	{
		// Token: 0x04004686 RID: 18054
		[Header("Main Panel Style")]
		public UISkinData.PanelStyle mainPanelStyle;

		// Token: 0x04004687 RID: 18055
		[Header("Header Style")]
		public UISkinData.PanelStyle headerPanelStyle;

		// Token: 0x04004688 RID: 18056
		public UISkinData.TextStyle headerTextStyle;

		// Token: 0x04004689 RID: 18057
		[Header("Detail Style")]
		public UISkinData.PanelStyle detailPanelStyle;

		// Token: 0x0400468A RID: 18058
		public UISkinData.TextStyle detailTextStyle;

		// Token: 0x0400468B RID: 18059
		[Header("Body Style")]
		public UISkinData.TextStyle bodyTextStyle;

		// Token: 0x0400468C RID: 18060
		[Header("Button Style")]
		public UISkinData.ButtonStyle buttonStyle;

		// Token: 0x0400468D RID: 18061
		[Header("Scroll Rect Style")]
		public UISkinData.ScrollRectStyle scrollRectStyle;

		// Token: 0x02000CA4 RID: 3236
		[Serializable]
		public struct TextStyle
		{
			// Token: 0x060049DC RID: 18908 RVA: 0x0012F40C File Offset: 0x0012D60C
			public void Apply(TextMeshProUGUI label, bool useAlignment = true)
			{
				HGTextMeshProUGUI hgtextMeshProUGUI;
				if (label.font != this.font && ((hgtextMeshProUGUI = (label as HGTextMeshProUGUI)) == null || !hgtextMeshProUGUI.useLanguageDefaultFont))
				{
					label.font = this.font;
				}
				if (label.fontSize != this.fontSize)
				{
					label.fontSize = this.fontSize;
				}
				if (label.color != this.color)
				{
					label.color = this.color;
				}
				if (useAlignment && label.alignment != this.alignment)
				{
					label.alignment = this.alignment;
				}
			}

			// Token: 0x0400468E RID: 18062
			public TMP_FontAsset font;

			// Token: 0x0400468F RID: 18063
			public float fontSize;

			// Token: 0x04004690 RID: 18064
			public TextAlignmentOptions alignment;

			// Token: 0x04004691 RID: 18065
			public Color color;
		}

		// Token: 0x02000CA5 RID: 3237
		[Serializable]
		public struct PanelStyle
		{
			// Token: 0x060049DD RID: 18909 RVA: 0x0012F4A0 File Offset: 0x0012D6A0
			public void Apply(Image image)
			{
				image.material = this.material;
				image.sprite = this.sprite;
				image.color = this.color;
			}

			// Token: 0x04004692 RID: 18066
			public Material material;

			// Token: 0x04004693 RID: 18067
			public Sprite sprite;

			// Token: 0x04004694 RID: 18068
			public Color color;
		}

		// Token: 0x02000CA6 RID: 3238
		[Serializable]
		public struct ButtonStyle
		{
			// Token: 0x04004695 RID: 18069
			public Material material;

			// Token: 0x04004696 RID: 18070
			public Sprite sprite;

			// Token: 0x04004697 RID: 18071
			public ColorBlock colors;

			// Token: 0x04004698 RID: 18072
			public UISkinData.TextStyle interactableTextStyle;

			// Token: 0x04004699 RID: 18073
			public UISkinData.TextStyle disabledTextStyle;

			// Token: 0x0400469A RID: 18074
			public float recommendedWidth;

			// Token: 0x0400469B RID: 18075
			public float recommendedHeight;
		}

		// Token: 0x02000CA7 RID: 3239
		[Serializable]
		public struct ScrollRectStyle
		{
			// Token: 0x0400469C RID: 18076
			[FormerlySerializedAs("viewportPanelStyle")]
			public UISkinData.PanelStyle backgroundPanelStyle;

			// Token: 0x0400469D RID: 18077
			public UISkinData.PanelStyle scrollbarBackgroundStyle;

			// Token: 0x0400469E RID: 18078
			public ColorBlock scrollbarHandleColors;

			// Token: 0x0400469F RID: 18079
			public Sprite scrollbarHandleImage;
		}
	}
}
