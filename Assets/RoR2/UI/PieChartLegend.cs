using System;
using System.Collections.ObjectModel;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace RoR2.UI
{
	// Token: 0x02000D63 RID: 3427
	public class PieChartLegend : MonoBehaviour
	{
		// Token: 0x17000727 RID: 1831
		// (get) Token: 0x06004E83 RID: 20099 RVA: 0x00144217 File Offset: 0x00142417
		// (set) Token: 0x06004E84 RID: 20100 RVA: 0x0014421F File Offset: 0x0014241F
		public PieChartMeshController source
		{
			get
			{
				return this._source;
			}
			set
			{
				if (this._source == value)
				{
					return;
				}
				this._source = value;
				this.subscribedSource = this.source;
			}
		}

		// Token: 0x17000728 RID: 1832
		// (get) Token: 0x06004E85 RID: 20101 RVA: 0x0014423E File Offset: 0x0014243E
		// (set) Token: 0x06004E86 RID: 20102 RVA: 0x00144248 File Offset: 0x00142448
		private PieChartMeshController subscribedSource
		{
			get
			{
				return this._subscribedSource;
			}
			set
			{
				if (this._subscribedSource == value)
				{
					return;
				}
				if (this._subscribedSource)
				{
					this._subscribedSource.onRebuilt -= this.OnSourceUpdated;
				}
				this._subscribedSource = value;
				if (this._subscribedSource)
				{
					this._subscribedSource.onRebuilt += this.OnSourceUpdated;
				}
			}
		}

		// Token: 0x06004E87 RID: 20103 RVA: 0x001442AE File Offset: 0x001424AE
		private void Awake()
		{
			this.InitStripAllocator();
			this.subscribedSource = this.source;
		}

		// Token: 0x06004E88 RID: 20104 RVA: 0x001442C2 File Offset: 0x001424C2
		private void OnDestroy()
		{
			this.subscribedSource = null;
		}

		// Token: 0x06004E89 RID: 20105 RVA: 0x001442CB File Offset: 0x001424CB
		private void OnValidate()
		{
			this.InitStripAllocator();
			this.subscribedSource = this.source;
			base.Invoke("Rebuild", 0f);
		}

		// Token: 0x06004E8A RID: 20106 RVA: 0x001442F0 File Offset: 0x001424F0
		private void InitStripAllocator()
		{
			if (this.stripAllocator != null && (this.stripAllocator.containerTransform != this.container || this.stripAllocator.elementPrefab != this.stripPrefab))
			{
				this.stripAllocator.AllocateElements(0);
				this.stripAllocator = null;
			}
			if (this.stripAllocator == null)
			{
				this.stripAllocator = new UIElementAllocator<ChildLocator>(this.container, this.stripPrefab, true, true);
			}
		}

		// Token: 0x06004E8B RID: 20107 RVA: 0x0014436C File Offset: 0x0014256C
		private void Rebuild()
		{
			if (!this.source || !this.stripAllocator.containerTransform || !this.stripAllocator.elementPrefab)
			{
				this.stripAllocator.AllocateElements(0);
				return;
			}
			int num = 0;
			for (int i = 0; i < this.source.sliceCount; i++)
			{
				if (this.source.GetSliceInfo(i).weight / this.source.totalSliceWeight > this.source.minimumRequiredWeightToDisplay || num < 10)
				{
					num++;
				}
			}
			this.stripAllocator.AllocateElements(num);
			ReadOnlyCollection<ChildLocator> elements = this.stripAllocator.elements;
			int j = 0;
			int num2 = Math.Min(num, elements.Count);
			int num3 = 0;
			while (j < num2)
			{
				PieChartMeshController.SliceInfo sliceInfo = this.source.GetSliceInfo(j);
				if (this.source.GetSliceInfo(j).weight / this.source.totalSliceWeight > this.source.minimumRequiredWeightToDisplay || num3 < 10)
				{
					num3++;
					ChildLocator childLocator = elements[j];
					Transform transform = childLocator.FindChild("ColorBox");
					Graphic graphic = (transform != null) ? transform.GetComponent<Graphic>() : null;
					Transform transform2 = childLocator.FindChild("Label");
					TMP_Text tmp_Text = (transform2 != null) ? transform2.GetComponent<TMP_Text>() : null;
					if (graphic)
					{
						graphic.color = sliceInfo.color;
					}
					if (tmp_Text)
					{
						tmp_Text.SetText(sliceInfo.tooltipContent.GetTitleText(), true);
					}
				}
				j++;
			}
		}

		// Token: 0x06004E8C RID: 20108 RVA: 0x001444F8 File Offset: 0x001426F8
		private void OnSourceUpdated()
		{
			this.Rebuild();
		}

		// Token: 0x04004B2C RID: 19244
		[SerializeField]
		private PieChartMeshController _source;

		// Token: 0x04004B2D RID: 19245
		public RectTransform container;

		// Token: 0x04004B2E RID: 19246
		public GameObject stripPrefab;

		// Token: 0x04004B2F RID: 19247
		private UIElementAllocator<ChildLocator> stripAllocator;

		// Token: 0x04004B30 RID: 19248
		private PieChartMeshController _subscribedSource;
	}
}
