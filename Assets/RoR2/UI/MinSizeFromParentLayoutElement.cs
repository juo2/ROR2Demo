using System;
using UnityEngine;
using UnityEngine.UI;

namespace RoR2.UI
{
	// Token: 0x02000D4C RID: 3404
	[RequireComponent(typeof(RectTransform))]
	public class MinSizeFromParentLayoutElement : MonoBehaviour, ILayoutElement
	{
		// Token: 0x06004DF8 RID: 19960 RVA: 0x001410EC File Offset: 0x0013F2EC
		public void CalculateLayoutInputHorizontal()
		{
			if (!this.useParentWidthAsMinWidth)
			{
				this.minWidth = -1f;
				return;
			}
			RectTransform rectTransform = base.transform.parent as RectTransform;
			this.minWidth = (rectTransform ? rectTransform.rect.width : -1f);
		}

		// Token: 0x06004DF9 RID: 19961 RVA: 0x00141144 File Offset: 0x0013F344
		public void CalculateLayoutInputVertical()
		{
			if (!this.useParentHeightAsMinHeight)
			{
				this.minHeight = -1f;
				return;
			}
			RectTransform rectTransform = base.transform.parent as RectTransform;
			this.minHeight = (rectTransform ? rectTransform.rect.height : -1f);
		}

		// Token: 0x1700071A RID: 1818
		// (get) Token: 0x06004DFA RID: 19962 RVA: 0x00141199 File Offset: 0x0013F399
		// (set) Token: 0x06004DFB RID: 19963 RVA: 0x001411A1 File Offset: 0x0013F3A1
		public float minWidth { get; protected set; } = -1f;

		// Token: 0x1700071B RID: 1819
		// (get) Token: 0x06004DFC RID: 19964 RVA: 0x001411AA File Offset: 0x0013F3AA
		public float preferredWidth { get; } = -1f;

		// Token: 0x1700071C RID: 1820
		// (get) Token: 0x06004DFD RID: 19965 RVA: 0x001411B2 File Offset: 0x0013F3B2
		public float flexibleWidth { get; } = -1f;

		// Token: 0x1700071D RID: 1821
		// (get) Token: 0x06004DFE RID: 19966 RVA: 0x001411BA File Offset: 0x0013F3BA
		// (set) Token: 0x06004DFF RID: 19967 RVA: 0x001411C2 File Offset: 0x0013F3C2
		public float minHeight { get; protected set; } = -1f;

		// Token: 0x1700071E RID: 1822
		// (get) Token: 0x06004E00 RID: 19968 RVA: 0x001411CB File Offset: 0x0013F3CB
		public float preferredHeight { get; } = -1f;

		// Token: 0x1700071F RID: 1823
		// (get) Token: 0x06004E01 RID: 19969 RVA: 0x001411D3 File Offset: 0x0013F3D3
		public float flexibleHeight { get; } = -1f;

		// Token: 0x17000720 RID: 1824
		// (get) Token: 0x06004E02 RID: 19970 RVA: 0x001411DB File Offset: 0x0013F3DB
		// (set) Token: 0x06004E03 RID: 19971 RVA: 0x001411E3 File Offset: 0x0013F3E3
		public int layoutPriority
		{
			get
			{
				return this._layoutPriority;
			}
			set
			{
				this._layoutPriority = value;
			}
		}

		// Token: 0x04004A99 RID: 19097
		public bool useParentWidthAsMinWidth = true;

		// Token: 0x04004A9A RID: 19098
		public bool useParentHeightAsMinHeight = true;

		// Token: 0x04004A9B RID: 19099
		[SerializeField]
		private int _layoutPriority = 1;
	}
}
