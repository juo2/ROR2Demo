using System;
using System.Runtime.CompilerServices;
using HG;
using UnityEngine;
using UnityEngine.UI;

namespace RoR2.UI
{
	// Token: 0x02000D6D RID: 3437
	public class RadialSliceGraphic : MaskableGraphic
	{
		// Token: 0x06004EC7 RID: 20167 RVA: 0x001456D1 File Offset: 0x001438D1
		protected RadialSliceGraphic()
		{
			base.useLegacyMeshGeneration = false;
		}

		// Token: 0x06004EC8 RID: 20168 RVA: 0x001456EB File Offset: 0x001438EB
		public void SetDisplayData(in RadialSliceGraphic.DisplayData newDisplayData)
		{
			if (newDisplayData == this.displayData)
			{
				return;
			}
			this.displayData = newDisplayData;
			this.UpdateDisplayData();
		}

		// Token: 0x06004EC9 RID: 20169 RVA: 0x00145714 File Offset: 0x00143914
		private void UpdateDisplayData()
		{
			if (this.material != this.displayData.material)
			{
				this.material = this.displayData.material;
			}
			if (this.color != this.displayData.color)
			{
				this.color = this.displayData.color;
			}
			base.SetVerticesDirty();
		}

		// Token: 0x06004ECA RID: 20170 RVA: 0x00145779 File Offset: 0x00143979
		private static Vector2 GetDirection(Radians radians)
		{
			return new Vector2(Mathf.Cos((float)radians), Mathf.Sin((float)radians));
		}

		// Token: 0x06004ECB RID: 20171 RVA: 0x0014579C File Offset: 0x0014399C
		protected override void OnPopulateMesh(VertexHelper vh)
		{
			RadialSliceGraphic.<>c__DisplayClass9_0 CS$<>8__locals1;
			CS$<>8__locals1.vh = vh;
			CS$<>8__locals1.vh.Clear();
			Rect rect = base.rectTransform.rect;
			this.currentRadius = Mathf.Min(rect.width, rect.height) * 0.5f;
			CS$<>8__locals1.outerRadius = this.currentRadius;
			CS$<>8__locals1.innerRadius = CS$<>8__locals1.outerRadius * this.displayData.normalizedInnerRadius;
			Radians start = this.displayData.start;
			Radians end = this.displayData.end;
			float startU = this.displayData.startU;
			float endU = this.displayData.endU;
			if (end < start)
			{
				Util.Swap<Radians>(ref start, ref end);
				Util.Swap<float>(ref startU, ref endU);
			}
			Radians radians = end - start;
			if ((float)radians <= 0f)
			{
				return;
			}
			if ((float)this.displayData.maxQuadWidth <= 0f)
			{
				return;
			}
			int num = Mathf.CeilToInt((float)radians / (float)this.displayData.maxQuadWidth);
			Radians radians2 = radians / num;
			float num2 = 1f / (float)num;
			float num3 = endU - startU;
			float quadStartU = startU;
			Vector2 vector = RadialSliceGraphic.GetDirection(start);
			Color color = this.color * this.displayData.color;
			for (int i = 0; i < num; i++)
			{
				int num4 = i + 1;
				Radians radians3 = num4 * radians2;
				Vector2 direction = RadialSliceGraphic.GetDirection(start + radians3);
				float num5 = startU + num3 * ((float)(i + 1) * num2);
				RadialSliceGraphic.<OnPopulateMesh>g__AddQuad|9_0(vector, direction, color, quadStartU, num5, ref CS$<>8__locals1);
				vector = direction;
				quadStartU = num5;
			}
		}

		// Token: 0x17000732 RID: 1842
		// (get) Token: 0x06004ECC RID: 20172 RVA: 0x00145950 File Offset: 0x00143B50
		public override Texture mainTexture
		{
			get
			{
				if (this.displayData.texture)
				{
					return this.displayData.texture;
				}
				if (!this.displayData.material)
				{
					return null;
				}
				return this.displayData.material.mainTexture;
			}
		}

		// Token: 0x06004ECD RID: 20173 RVA: 0x001459A0 File Offset: 0x00143BA0
		public override bool Raycast(Vector2 sp, Camera eventCamera)
		{
			if (!base.Raycast(sp, eventCamera))
			{
				return false;
			}
			Vector2 point;
			if (!RectTransformUtility.ScreenPointToLocalPointInRectangle(base.rectTransform, sp, eventCamera, out point))
			{
				return false;
			}
			Color white = Color.white;
			bool result = RadialSliceGraphic.PointInArc(point, this.displayData.start, this.displayData.end, this.displayData.normalizedInnerRadius * this.currentRadius, this.currentRadius);
			this.color = white;
			return result;
		}

		// Token: 0x06004ECE RID: 20174 RVA: 0x00145A10 File Offset: 0x00143C10
		private static bool PointInArc(Vector2 point, Radians arcStart, Radians arcEnd, float arcInnerRadius, float arcOuterRadius)
		{
			Radians radians = arcStart + arcEnd;
			float num = 0.5f;
			Vector2 direction = RadialSliceGraphic.GetDirection(radians * num);
			radians = arcEnd - arcStart;
			Radians absolute = radians.absolute;
			Vector2 normalized = point.normalized;
			float num2 = Vector2.Dot(direction, normalized);
			float num3 = Mathf.Cos((float)absolute * 0.5f);
			if (num2 < num3)
			{
				return false;
			}
			float sqrMagnitude = point.sqrMagnitude;
			return sqrMagnitude >= arcInnerRadius * arcInnerRadius && sqrMagnitude <= arcOuterRadius * arcOuterRadius;
		}

		// Token: 0x06004ECF RID: 20175 RVA: 0x00145A94 File Offset: 0x00143C94
		public override void GraphicUpdateComplete()
		{
			base.GraphicUpdateComplete();
			if (this.sliceCenterSticker)
			{
				float num = Mathf.LerpAngle((float)this.displayData.start, (float)this.displayData.end, 0.5f);
				Radians radians = (Radians)num;
				float num2 = Mathf.Lerp(this.displayData.normalizedInnerRadius, 1f, 0.5f) * this.currentRadius;
				Vector3 localPosition = new Vector3(radians.cos * num2, radians.sin * num2);
				this.sliceCenterSticker.localPosition = localPosition;
			}
		}

		// Token: 0x06004ED0 RID: 20176 RVA: 0x00145B30 File Offset: 0x00143D30
		// Note: this type is marked as 'beforefieldinit'.
		static RadialSliceGraphic()
		{
			RadialSliceGraphic.DisplayData displayData = default(RadialSliceGraphic.DisplayData);
			displayData.color = Color.white;
			displayData.start = Radians.FromRevolutions(0f);
			displayData.end = Radians.FromRevolutions(-0.4f);
			displayData.startU = 0f;
			displayData.endU = 1f;
			float num = 0.31415927f;
			displayData.maxQuadWidth = (Radians)num;
			displayData.normalizedInnerRadius = 0.2f;
			RadialSliceGraphic.defaultDisplayData = displayData;
		}

		// Token: 0x06004ED1 RID: 20177 RVA: 0x00145BB4 File Offset: 0x00143DB4
		[CompilerGenerated]
		internal static void <OnPopulateMesh>g__AddQuad|9_0(in Vector2 startDirection, in Vector2 endDirection, in Color color, float quadStartU, float quadEndU, ref RadialSliceGraphic.<>c__DisplayClass9_0 A_5)
		{
			int currentVertCount = A_5.vh.currentVertCount;
			A_5.vh.AddVert(startDirection * A_5.innerRadius, color, new Vector2(quadStartU, 0f));
			A_5.vh.AddVert(startDirection * A_5.outerRadius, color, new Vector2(quadStartU, 1f));
			A_5.vh.AddVert(endDirection * A_5.outerRadius, color, new Vector2(quadEndU, 1f));
			A_5.vh.AddVert(endDirection * A_5.innerRadius, color, new Vector2(quadEndU, 0f));
			A_5.vh.AddTriangle(currentVertCount, currentVertCount + 1, currentVertCount + 2);
			A_5.vh.AddTriangle(currentVertCount + 2, currentVertCount + 3, currentVertCount);
		}

		// Token: 0x04004B7B RID: 19323
		public RectTransform sliceCenterSticker;

		// Token: 0x04004B7C RID: 19324
		private static readonly RadialSliceGraphic.DisplayData defaultDisplayData;

		// Token: 0x04004B7D RID: 19325
		private RadialSliceGraphic.DisplayData displayData = RadialSliceGraphic.defaultDisplayData;

		// Token: 0x04004B7E RID: 19326
		private float currentRadius;

		// Token: 0x02000D6E RID: 3438
		public struct DisplayData : IEquatable<RadialSliceGraphic.DisplayData>
		{
			// Token: 0x06004ED2 RID: 20178 RVA: 0x00145CDC File Offset: 0x00143EDC
			public override bool Equals(object obj)
			{
				if (obj == null)
				{
					return false;
				}
				if (obj is RadialSliceGraphic.DisplayData)
				{
					RadialSliceGraphic.DisplayData other = (RadialSliceGraphic.DisplayData)obj;
					return this.Equals(other);
				}
				return false;
			}

			// Token: 0x06004ED3 RID: 20179 RVA: 0x00145D08 File Offset: 0x00143F08
			public static bool operator ==(RadialSliceGraphic.DisplayData left, RadialSliceGraphic.DisplayData right)
			{
				return left.Equals(right);
			}

			// Token: 0x06004ED4 RID: 20180 RVA: 0x00145D12 File Offset: 0x00143F12
			public static bool operator !=(RadialSliceGraphic.DisplayData left, RadialSliceGraphic.DisplayData right)
			{
				return !left.Equals(right);
			}

			// Token: 0x06004ED5 RID: 20181 RVA: 0x00145D20 File Offset: 0x00143F20
			public bool Equals(RadialSliceGraphic.DisplayData other)
			{
				return this.start.Equals(other.start) && this.end.Equals(other.end) && this.startU.Equals(other.startU) && this.endU.Equals(other.endU) && object.Equals(this.material, other.material) && object.Equals(this.texture, other.texture) && this.color.Equals(other.color) && this.normalizedInnerRadius.Equals(other.normalizedInnerRadius) && this.maxQuadWidth.Equals(other.maxQuadWidth);
			}

			// Token: 0x06004ED6 RID: 20182 RVA: 0x00145DE0 File Offset: 0x00143FE0
			public override int GetHashCode()
			{
				return (((((((this.start.GetHashCode() * 397 ^ this.end.GetHashCode()) * 397 ^ this.startU.GetHashCode()) * 397 ^ this.endU.GetHashCode()) * 397 ^ ((this.material != null) ? this.material.GetHashCode() : 0)) * 397 ^ ((this.texture != null) ? this.texture.GetHashCode() : 0)) * 397 ^ this.color.GetHashCode()) * 397 ^ this.normalizedInnerRadius.GetHashCode()) * 397 ^ this.maxQuadWidth.GetHashCode();
			}

			// Token: 0x04004B7F RID: 19327
			public Radians start;

			// Token: 0x04004B80 RID: 19328
			public Radians end;

			// Token: 0x04004B81 RID: 19329
			public float startU;

			// Token: 0x04004B82 RID: 19330
			public float endU;

			// Token: 0x04004B83 RID: 19331
			public Material material;

			// Token: 0x04004B84 RID: 19332
			public Texture texture;

			// Token: 0x04004B85 RID: 19333
			public Color color;

			// Token: 0x04004B86 RID: 19334
			public float normalizedInnerRadius;

			// Token: 0x04004B87 RID: 19335
			public Radians maxQuadWidth;
		}
	}
}
