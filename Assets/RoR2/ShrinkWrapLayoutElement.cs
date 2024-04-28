using System;
using UnityEngine;
using UnityEngine.UI;

namespace RoR2
{
	// Token: 0x020008D9 RID: 2265
	public class ShrinkWrapLayoutElement : MonoBehaviour, ILayoutElement
	{
		// Token: 0x060032C0 RID: 12992 RVA: 0x000D6470 File Offset: 0x000D4670
		public void CalculateLayoutInputHorizontal()
		{
			this.preferredWidth = 0f;
			if (base.transform.childCount == 0)
			{
				return;
			}
			Rect rect = ((RectTransform)base.transform.GetChild(0)).rect;
			int i = 1;
			int childCount = base.transform.childCount;
			while (i < childCount)
			{
				Rect rect2 = ((RectTransform)base.transform.GetChild(i)).rect;
				rect.xMin = Mathf.Min(rect.xMin, rect2.xMin);
				rect.xMax = Mathf.Max(rect.xMax, rect2.xMax);
				i++;
			}
			this.minWidth = rect.width;
			this.preferredWidth = rect.width;
			this.flexibleWidth = 0f;
		}

		// Token: 0x060032C1 RID: 12993 RVA: 0x000D6538 File Offset: 0x000D4738
		public void CalculateLayoutInputVertical()
		{
			this.preferredHeight = 0f;
			if (base.transform.childCount == 0)
			{
				return;
			}
			Rect rect = ((RectTransform)base.transform.GetChild(0)).rect;
			int i = 1;
			int childCount = base.transform.childCount;
			while (i < childCount)
			{
				Rect rect2 = ((RectTransform)base.transform.GetChild(i)).rect;
				rect.yMin = Mathf.Min(rect.yMin, rect2.yMin);
				rect.yMax = Mathf.Max(rect.yMax, rect2.yMax);
				i++;
			}
			this.minHeight = rect.height;
			this.preferredHeight = rect.height;
			this.flexibleHeight = 0f;
		}

		// Token: 0x170004B0 RID: 1200
		// (get) Token: 0x060032C2 RID: 12994 RVA: 0x000D65FD File Offset: 0x000D47FD
		// (set) Token: 0x060032C3 RID: 12995 RVA: 0x000D6605 File Offset: 0x000D4805
		public float minWidth { get; private set; }

		// Token: 0x170004B1 RID: 1201
		// (get) Token: 0x060032C4 RID: 12996 RVA: 0x000D660E File Offset: 0x000D480E
		// (set) Token: 0x060032C5 RID: 12997 RVA: 0x000D6616 File Offset: 0x000D4816
		public float preferredWidth { get; private set; }

		// Token: 0x170004B2 RID: 1202
		// (get) Token: 0x060032C6 RID: 12998 RVA: 0x000D661F File Offset: 0x000D481F
		// (set) Token: 0x060032C7 RID: 12999 RVA: 0x000D6627 File Offset: 0x000D4827
		public float flexibleWidth { get; private set; }

		// Token: 0x170004B3 RID: 1203
		// (get) Token: 0x060032C8 RID: 13000 RVA: 0x000D6630 File Offset: 0x000D4830
		// (set) Token: 0x060032C9 RID: 13001 RVA: 0x000D6638 File Offset: 0x000D4838
		public float minHeight { get; private set; }

		// Token: 0x170004B4 RID: 1204
		// (get) Token: 0x060032CA RID: 13002 RVA: 0x000D6641 File Offset: 0x000D4841
		// (set) Token: 0x060032CB RID: 13003 RVA: 0x000D6649 File Offset: 0x000D4849
		public float preferredHeight { get; private set; }

		// Token: 0x170004B5 RID: 1205
		// (get) Token: 0x060032CC RID: 13004 RVA: 0x000D6652 File Offset: 0x000D4852
		// (set) Token: 0x060032CD RID: 13005 RVA: 0x000D665A File Offset: 0x000D485A
		public float flexibleHeight { get; private set; }

		// Token: 0x170004B6 RID: 1206
		// (get) Token: 0x060032CE RID: 13006 RVA: 0x000D6663 File Offset: 0x000D4863
		// (set) Token: 0x060032CF RID: 13007 RVA: 0x000D666B File Offset: 0x000D486B
		public int layoutPriority { get; private set; }
	}
}
