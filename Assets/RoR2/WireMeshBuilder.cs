using System;
using System.Collections.Generic;
using UnityEngine;

namespace RoR2
{
	// Token: 0x02000AA9 RID: 2729
	public class WireMeshBuilder : IDisposable
	{
		// Token: 0x06003EC6 RID: 16070 RVA: 0x00102F7C File Offset: 0x0010117C
		private int GetVertexIndex(WireMeshBuilder.LineVertex vertex)
		{
			int num;
			if (!this.uniqueVertexToIndex.TryGetValue(vertex, out num))
			{
				int num2 = this.uniqueVertexCount;
				this.uniqueVertexCount = num2 + 1;
				num = num2;
				this.positions.Add(vertex.position);
				this.colors.Add(vertex.color);
				this.uniqueVertexToIndex.Add(vertex, num);
			}
			return num;
		}

		// Token: 0x06003EC7 RID: 16071 RVA: 0x00102FDB File Offset: 0x001011DB
		public void Clear()
		{
			this.uniqueVertexToIndex.Clear();
			this.indices.Clear();
			this.positions.Clear();
			this.colors.Clear();
			this.uniqueVertexCount = 0;
		}

		// Token: 0x06003EC8 RID: 16072 RVA: 0x00103010 File Offset: 0x00101210
		public void AddLine(Vector3 p1, Color c1, Vector3 p2, Color c2)
		{
			WireMeshBuilder.LineVertex vertex = new WireMeshBuilder.LineVertex
			{
				position = p1,
				color = c1
			};
			WireMeshBuilder.LineVertex vertex2 = new WireMeshBuilder.LineVertex
			{
				position = p2,
				color = c2
			};
			int vertexIndex = this.GetVertexIndex(vertex);
			int vertexIndex2 = this.GetVertexIndex(vertex2);
			this.indices.Add(vertexIndex);
			this.indices.Add(vertexIndex2);
		}

		// Token: 0x06003EC9 RID: 16073 RVA: 0x0010307C File Offset: 0x0010127C
		public Mesh GenerateMesh()
		{
			Mesh mesh = new Mesh();
			this.GenerateMesh(mesh);
			return mesh;
		}

		// Token: 0x06003ECA RID: 16074 RVA: 0x00103097 File Offset: 0x00101297
		public void GenerateMesh(Mesh dest)
		{
			dest.SetTriangles(Array.Empty<int>(), 0);
			dest.SetVertices(this.positions);
			dest.SetColors(this.colors);
			dest.SetIndices(this.indices.ToArray(), MeshTopology.Lines, 0);
		}

		// Token: 0x06003ECB RID: 16075 RVA: 0x001030D0 File Offset: 0x001012D0
		public void Dispose()
		{
			this.uniqueVertexToIndex = null;
			this.indices = null;
			this.positions = null;
			this.colors = null;
		}

		// Token: 0x04003D08 RID: 15624
		private int uniqueVertexCount;

		// Token: 0x04003D09 RID: 15625
		private Dictionary<WireMeshBuilder.LineVertex, int> uniqueVertexToIndex = new Dictionary<WireMeshBuilder.LineVertex, int>();

		// Token: 0x04003D0A RID: 15626
		private List<int> indices = new List<int>();

		// Token: 0x04003D0B RID: 15627
		private List<Vector3> positions = new List<Vector3>();

		// Token: 0x04003D0C RID: 15628
		private List<Color> colors = new List<Color>();

		// Token: 0x02000AAA RID: 2730
		private struct LineVertex
		{
			// Token: 0x04003D0D RID: 15629
			public Vector3 position;

			// Token: 0x04003D0E RID: 15630
			public Color color;
		}
	}
}
