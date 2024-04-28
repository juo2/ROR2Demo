using System;
using UnityEngine;

namespace RoR2
{
	// Token: 0x020007C2 RID: 1986
	[ExecuteAlways]
	[RequireComponent(typeof(LineRenderer))]
	public class MultiPointBezierCurveLine : MonoBehaviour
	{
		// Token: 0x06002A1B RID: 10779 RVA: 0x000B5ACD File Offset: 0x000B3CCD
		private void Start()
		{
			this.lineRenderer = base.GetComponent<LineRenderer>();
		}

		// Token: 0x06002A1C RID: 10780 RVA: 0x000B5ADC File Offset: 0x000B3CDC
		private void LateUpdate()
		{
			for (int i = 0; i < this.linePositionList.Length; i++)
			{
				float globalT = (float)i / (float)(this.linePositionList.Length - 1);
				this.linePositionList[i] = this.EvaluateBezier(globalT);
			}
			this.lineRenderer.SetPositions(this.linePositionList);
		}

		// Token: 0x06002A1D RID: 10781 RVA: 0x000B5B30 File Offset: 0x000B3D30
		private Vector3 EvaluateBezier(float globalT)
		{
			int num = this.vertexList.Length - 1;
			int num3;
			int num2 = Mathf.Min((num3 = Mathf.FloorToInt((float)num * globalT)) + 1, num);
			MultiPointBezierCurveLine.Vertex vertex = this.vertexList[num3];
			MultiPointBezierCurveLine.Vertex vertex2 = this.vertexList[num2];
			Vector3 vector = vertex.vertexTransform ? vertex.vertexTransform.position : vertex.position;
			Vector3 a = vertex2.vertexTransform ? vertex2.vertexTransform.position : vertex2.position;
			Vector3 b = vertex.vertexTransform ? vertex.vertexTransform.TransformVector(vertex.localVelocity) : vertex.localVelocity;
			Vector3 b2 = vertex2.vertexTransform ? vertex2.vertexTransform.TransformVector(vertex2.localVelocity) : vertex2.localVelocity;
			if (num3 == num2)
			{
				return vector;
			}
			float inMin = (float)num3 / (float)num;
			float inMax = (float)num2 / (float)num;
			float num4 = Util.Remap(globalT, inMin, inMax, 0f, 1f);
			Vector3 a2 = Vector3.Lerp(vector, vector + b, num4);
			Vector3 b3 = Vector3.Lerp(a, a + b2, 1f - num4);
			return Vector3.Lerp(a2, b3, num4);
		}

		// Token: 0x04002D5E RID: 11614
		public MultiPointBezierCurveLine.Vertex[] vertexList;

		// Token: 0x04002D5F RID: 11615
		public Vector3[] linePositionList;

		// Token: 0x04002D60 RID: 11616
		[HideInInspector]
		public LineRenderer lineRenderer;

		// Token: 0x020007C3 RID: 1987
		[Serializable]
		public struct Vertex
		{
			// Token: 0x04002D61 RID: 11617
			public Transform vertexTransform;

			// Token: 0x04002D62 RID: 11618
			public Vector3 position;

			// Token: 0x04002D63 RID: 11619
			public Vector3 localVelocity;
		}
	}
}
