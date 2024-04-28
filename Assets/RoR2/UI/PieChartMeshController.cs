using System;
using System.Collections.ObjectModel;
using HG;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;

namespace RoR2.UI
{
	// Token: 0x02000D64 RID: 3428
	[ExecuteAlways]
	public class PieChartMeshController : MonoBehaviour
	{
		// Token: 0x1400010D RID: 269
		// (add) Token: 0x06004E8E RID: 20110 RVA: 0x00144500 File Offset: 0x00142700
		// (remove) Token: 0x06004E8F RID: 20111 RVA: 0x00144538 File Offset: 0x00142738
		public event Action onRebuilt;

		// Token: 0x17000729 RID: 1833
		// (get) Token: 0x06004E90 RID: 20112 RVA: 0x0014456D File Offset: 0x0014276D
		// (set) Token: 0x06004E91 RID: 20113 RVA: 0x00144575 File Offset: 0x00142775
		public float totalSliceWeight { get; private set; }

		// Token: 0x06004E92 RID: 20114 RVA: 0x0014457E File Offset: 0x0014277E
		private void Awake()
		{
			this.InitSliceAllocator();
		}

		// Token: 0x06004E93 RID: 20115 RVA: 0x00144586 File Offset: 0x00142786
		private void Update()
		{
			if (this.slicesDirty)
			{
				this.slicesDirty = false;
				this.RebuildSlices();
			}
		}

		// Token: 0x06004E94 RID: 20116 RVA: 0x0014459D File Offset: 0x0014279D
		private void OnValidate()
		{
			this.InitSliceAllocator();
			this.slicesDirty = true;
		}

		// Token: 0x06004E95 RID: 20117 RVA: 0x001445AC File Offset: 0x001427AC
		private void InitSliceAllocator()
		{
			this.sliceAllocator = new UIElementAllocator<RadialSliceGraphic>(this.container, this.slicePrefab, true, true);
		}

		// Token: 0x06004E96 RID: 20118 RVA: 0x001445C7 File Offset: 0x001427C7
		public void SetSlices([NotNull] PieChartMeshController.SliceInfo[] newSliceInfos)
		{
			if (ArrayUtils.SequenceEquals<PieChartMeshController.SliceInfo>(this.sliceInfos, newSliceInfos))
			{
				return;
			}
			Array.Resize<PieChartMeshController.SliceInfo>(ref this.sliceInfos, newSliceInfos.Length);
			Array.Copy(newSliceInfos, this.sliceInfos, this.sliceInfos.Length);
			this.slicesDirty = true;
		}

		// Token: 0x06004E97 RID: 20119 RVA: 0x00144604 File Offset: 0x00142804
		private void RebuildSlices()
		{
			this.totalSliceWeight = 0f;
			this.totalSliceCount = 0;
			for (int i = 0; i < this.sliceInfos.Length; i++)
			{
				float weight = this.sliceInfos[i].weight;
				if (weight > 0f)
				{
					this.totalSliceCount++;
					this.totalSliceWeight += weight;
				}
			}
			this.sliceAllocator.AllocateElements(this.totalSliceCount);
			Radians start = new Radians(1.5707964f);
			float num = 0f;
			ReadOnlyCollection<RadialSliceGraphic> elements = this.sliceAllocator.elements;
			int num2 = 0;
			int j = 0;
			int count = elements.Count;
			while (j < count)
			{
				ref PieChartMeshController.SliceInfo ptr = ref this.sliceInfos[num2++];
				if (ptr.weight > 0f)
				{
					float num3 = ptr.weight / this.totalSliceWeight;
					Radians radians = new Radians(num3 * 6.2831855f);
					Radians radians2 = start - radians;
					Radians radians3 = new Radians(6.2831855f);
					float num4 = num - num3 * this.uTile;
					RadialSliceGraphic radialSliceGraphic = elements[j++];
					RadialSliceGraphic.DisplayData displayData = default(RadialSliceGraphic.DisplayData);
					displayData.start = start;
					displayData.end = radians2;
					displayData.startU = (this.individualSegmentUMapping ? 0f : num);
					displayData.endU = (this.individualSegmentUMapping ? this.uTile : num4);
					displayData.normalizedInnerRadius = this.normalizedInnerRadius;
					float num5 = 720f;
					displayData.maxQuadWidth = radians3 / num5;
					displayData.material = this.material;
					displayData.texture = this.texture;
					displayData.color = ptr.color * this.color;
					radialSliceGraphic.SetDisplayData(displayData);
					TooltipProvider component = radialSliceGraphic.GetComponent<TooltipProvider>();
					if (component)
					{
						component.SetContent(ptr.tooltipContent);
					}
					ChildLocator component2 = radialSliceGraphic.GetComponent<ChildLocator>();
					if (component2)
					{
						Transform transform = component2.FindChild("SliceCenterStickerLabel");
						if (transform)
						{
							transform.GetComponent<TMP_Text>().SetText((num3 >= this.minimumRequiredWeightToDisplay) ? Language.GetStringFormatted("PERCENT_FORMAT", new object[]
							{
								num3
							}) : string.Empty, true);
						}
					}
					start = radians2;
					num = num4;
				}
			}
			Action action = this.onRebuilt;
			if (action == null)
			{
				return;
			}
			action();
		}

		// Token: 0x1700072A RID: 1834
		// (get) Token: 0x06004E98 RID: 20120 RVA: 0x0014486B File Offset: 0x00142A6B
		public int sliceCount
		{
			get
			{
				return this.sliceInfos.Length;
			}
		}

		// Token: 0x06004E99 RID: 20121 RVA: 0x00144875 File Offset: 0x00142A75
		public PieChartMeshController.SliceInfo GetSliceInfo(int i)
		{
			return this.sliceInfos[i];
		}

		// Token: 0x04004B31 RID: 19249
		public RectTransform container;

		// Token: 0x04004B32 RID: 19250
		public GameObject slicePrefab;

		// Token: 0x04004B33 RID: 19251
		[Range(0f, 1f)]
		public float normalizedInnerRadius;

		// Token: 0x04004B34 RID: 19252
		[Tooltip("The minimum value required to display percentage and show an entry in the legend.")]
		public float minimumRequiredWeightToDisplay = 0.03f;

		// Token: 0x04004B35 RID: 19253
		public Texture texture;

		// Token: 0x04004B36 RID: 19254
		public Material material;

		// Token: 0x04004B37 RID: 19255
		[ColorUsage(true, true)]
		public Color color;

		// Token: 0x04004B38 RID: 19256
		public float uTile = 1f;

		// Token: 0x04004B39 RID: 19257
		public bool individualSegmentUMapping;

		// Token: 0x04004B3A RID: 19258
		[SerializeField]
		private PieChartMeshController.SliceInfo[] sliceInfos = Array.Empty<PieChartMeshController.SliceInfo>();

		// Token: 0x04004B3D RID: 19261
		private int totalSliceCount;

		// Token: 0x04004B3E RID: 19262
		private UIElementAllocator<RadialSliceGraphic> sliceAllocator;

		// Token: 0x04004B3F RID: 19263
		private bool slicesDirty = true;

		// Token: 0x02000D65 RID: 3429
		[Serializable]
		public struct SliceInfo : IEquatable<PieChartMeshController.SliceInfo>
		{
			// Token: 0x06004E9B RID: 20123 RVA: 0x001448B3 File Offset: 0x00142AB3
			public bool Equals(PieChartMeshController.SliceInfo other)
			{
				return this.weight.Equals(other.weight) && this.color.Equals(other.color) && this.tooltipContent.Equals(other.tooltipContent);
			}

			// Token: 0x06004E9C RID: 20124 RVA: 0x001448F0 File Offset: 0x00142AF0
			public override bool Equals(object obj)
			{
				if (obj == null)
				{
					return false;
				}
				if (obj is PieChartMeshController.SliceInfo)
				{
					PieChartMeshController.SliceInfo other = (PieChartMeshController.SliceInfo)obj;
					return this.Equals(other);
				}
				return false;
			}

			// Token: 0x06004E9D RID: 20125 RVA: 0x0014491C File Offset: 0x00142B1C
			public override int GetHashCode()
			{
				return (this.weight.GetHashCode() * 397 ^ this.color.GetHashCode()) * 397 ^ this.tooltipContent.GetHashCode();
			}

			// Token: 0x06004E9E RID: 20126 RVA: 0x00144959 File Offset: 0x00142B59
			public static bool operator ==(PieChartMeshController.SliceInfo left, PieChartMeshController.SliceInfo right)
			{
				return left.Equals(right);
			}

			// Token: 0x06004E9F RID: 20127 RVA: 0x00144963 File Offset: 0x00142B63
			public static bool operator !=(PieChartMeshController.SliceInfo left, PieChartMeshController.SliceInfo right)
			{
				return !left.Equals(right);
			}

			// Token: 0x04004B40 RID: 19264
			[Min(0f)]
			public float weight;

			// Token: 0x04004B41 RID: 19265
			public Color color;

			// Token: 0x04004B42 RID: 19266
			public TooltipContent tooltipContent;
		}
	}
}
