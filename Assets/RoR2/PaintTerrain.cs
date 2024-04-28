using System;
using UnityEngine;

namespace RoR2
{
	// Token: 0x020007F8 RID: 2040
	public class PaintTerrain : MonoBehaviour
	{
		// Token: 0x06002BFE RID: 11262 RVA: 0x000BC1EC File Offset: 0x000BA3EC
		private void Start()
		{
			this.snowfallDirection = this.snowfallDirection.normalized;
			this.terrain = base.GetComponent<Terrain>();
			this.data = this.terrain.terrainData;
			this.alphamaps = this.data.GetAlphamaps(0, 0, this.data.alphamapWidth, this.data.alphamapHeight);
			this.detailmapGrass = this.data.GetDetailLayer(0, 0, this.data.detailWidth, this.data.detailHeight, 0);
			this.UpdateAlphaMaps();
			this.UpdateDetailMaps();
		}

		// Token: 0x06002BFF RID: 11263 RVA: 0x000BC288 File Offset: 0x000BA488
		private void UpdateAlphaMaps()
		{
			for (int i = 0; i < this.data.alphamapHeight; i++)
			{
				for (int j = 0; j < this.data.alphamapWidth; j++)
				{
					float z = (float)j / (float)this.data.alphamapWidth * this.data.size.x;
					float x = (float)i / (float)this.data.alphamapHeight * this.data.size.z;
					float num = 0f;
					float num2 = 0f;
					float num3 = Mathf.Pow(Vector3.Dot(Vector3.up, this.data.GetInterpolatedNormal((float)i / (float)this.data.alphamapHeight, (float)j / (float)this.data.alphamapWidth)), this.splatSlopePower);
					float num4 = num3;
					float num5 = 1f - num3;
					float interpolatedHeight = this.data.GetInterpolatedHeight((float)i / (float)this.data.alphamapHeight, (float)j / (float)this.data.alphamapWidth);
					RaycastHit raycastHit;
					if (Physics.Raycast(new Vector3(x, interpolatedHeight, z), this.snowfallDirection, out raycastHit, this.splatRaycastLength, LayerIndex.world.mask))
					{
						float num6 = num4;
						float num7 = Mathf.Clamp01(Mathf.Pow(raycastHit.distance / this.splatHeightReference, this.heightPower));
						num4 = num7 * num6;
						num = (1f - num7) * num6;
					}
					this.alphamaps[j, i, 0] = num4;
					this.alphamaps[j, i, 1] = num;
					this.alphamaps[j, i, 2] = num5;
					this.alphamaps[j, i, 3] = num2;
				}
			}
			this.data.SetAlphamaps(0, 0, this.alphamaps);
		}

		// Token: 0x06002C00 RID: 11264 RVA: 0x000BC45C File Offset: 0x000BA65C
		private void UpdateDetailMaps()
		{
			for (int i = 0; i < this.data.detailHeight; i++)
			{
				for (int j = 0; j < this.data.detailWidth; j++)
				{
					int num = 0;
					this.detailmapGrass[j, i] = num;
				}
			}
			this.data.SetDetailLayer(0, 0, 0, this.detailmapGrass);
		}

		// Token: 0x04002E6D RID: 11885
		public float splatHeightReference = 60f;

		// Token: 0x04002E6E RID: 11886
		public float splatRaycastLength = 200f;

		// Token: 0x04002E6F RID: 11887
		public float splatSlopePower = 1f;

		// Token: 0x04002E70 RID: 11888
		public float heightPower = 1f;

		// Token: 0x04002E71 RID: 11889
		public Vector3 snowfallDirection = Vector3.up;

		// Token: 0x04002E72 RID: 11890
		public Texture2D grassNoiseMap;

		// Token: 0x04002E73 RID: 11891
		private Terrain terrain;

		// Token: 0x04002E74 RID: 11892
		private TerrainData data;

		// Token: 0x04002E75 RID: 11893
		private float[,,] alphamaps;

		// Token: 0x04002E76 RID: 11894
		private int[,] detailmapGrass;
	}
}
