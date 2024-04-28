using System;
using System.Collections.Generic;
using UnityEngine;

namespace RoR2
{
	// Token: 0x0200089E RID: 2206
	public class SmoothTrailMesh : MonoBehaviour
	{
		// Token: 0x060030C9 RID: 12489 RVA: 0x000CF47C File Offset: 0x000CD67C
		private void Awake()
		{
			this.mesh = new Mesh();
			this.mesh.MarkDynamic();
			GameObject gameObject = new GameObject("SmoothTrailMeshRenderer");
			this.meshFilter = gameObject.AddComponent<MeshFilter>();
			this.meshFilter.mesh = this.mesh;
			this.meshRenderer = gameObject.AddComponent<MeshRenderer>();
			this.meshRenderer.sharedMaterials = this.sharedMaterials;
		}

		// Token: 0x060030CA RID: 12490 RVA: 0x000CF4E4 File Offset: 0x000CD6E4
		private void AddCurrentPoint()
		{
			float time = Time.time;
			Vector3 position = base.transform.position;
			Vector3 b = base.transform.up * this.width * 0.5f;
			this.pointsQueue.Enqueue(new SmoothTrailMesh.Point
			{
				vertex1 = position + b,
				vertex2 = position - b,
				time = time
			});
		}

		// Token: 0x060030CB RID: 12491 RVA: 0x000CF55C File Offset: 0x000CD75C
		private void OnEnable()
		{
			this.AddCurrentPoint();
		}

		// Token: 0x060030CC RID: 12492 RVA: 0x000CF564 File Offset: 0x000CD764
		private void OnDisable()
		{
			this.pointsQueue.Clear();
			this.mesh.Clear();
		}

		// Token: 0x060030CD RID: 12493 RVA: 0x000CF57C File Offset: 0x000CD77C
		private void OnDestroy()
		{
			if (this.meshFilter)
			{
				this.meshFilter.mesh = null;
				UnityEngine.Object.Destroy(this.meshFilter.gameObject);
			}
			UnityEngine.Object.Destroy(this.mesh);
		}

		// Token: 0x060030CE RID: 12494 RVA: 0x000CF5B4 File Offset: 0x000CD7B4
		private void Simulate()
		{
			float time = Time.time;
			Vector3 position = base.transform.position;
			Vector3 b = base.transform.up * this.width * 0.5f;
			float num = time - this.previousTime;
			if (num > 0f)
			{
				float num2 = 1f / num;
				for (float num3 = this.previousTime; num3 <= time; num3 += this.timeStep)
				{
					float t = (num3 - this.previousTime) * num2;
					Vector3 a = Vector3.LerpUnclamped(this.previousPosition, position, t);
					Vector3 b2 = Vector3.SlerpUnclamped(this.previousUp, b, t);
					this.pointsQueue.Enqueue(new SmoothTrailMesh.Point
					{
						vertex1 = a + b2,
						vertex2 = a - b2,
						time = num3
					});
				}
			}
			float num4 = time - this.trailLifetime;
			while (this.pointsQueue.Count > 0 && this.pointsQueue.Peek().time < num4)
			{
				this.pointsQueue.Dequeue();
			}
			this.previousTime = time;
			this.previousPosition = position;
			this.previousUp = b;
		}

		// Token: 0x060030CF RID: 12495 RVA: 0x000CF6E6 File Offset: 0x000CD8E6
		private void LateUpdate()
		{
			this.Simulate();
			this.GenerateMesh();
		}

		// Token: 0x060030D0 RID: 12496 RVA: 0x000CF6F4 File Offset: 0x000CD8F4
		private void GenerateMesh()
		{
			Vector3[] array = new Vector3[this.pointsQueue.Count * 2];
			Vector2[] array2 = new Vector2[this.pointsQueue.Count * 2];
			Color[] array3 = new Color[this.pointsQueue.Count * 2];
			float num = 1f / (float)this.pointsQueue.Count;
			int num2 = 0;
			if (this.pointsQueue.Count > 0)
			{
				float time = this.pointsQueue.Peek().time;
				float time2 = Time.time;
				float num3 = time2 - time;
				float num4 = 1f / num3;
				foreach (SmoothTrailMesh.Point point in this.pointsQueue)
				{
					float num5 = (time2 - point.time) * num4;
					array[num2] = point.vertex1;
					array2[num2] = new Vector2(1f, num5);
					array3[num2] = new Color(1f, 1f, 1f, this.fadeVertexAlpha ? (1f - num5) : 1f);
					num2++;
					array[num2] = point.vertex2;
					array2[num2] = new Vector2(0f, num5);
					num2++;
				}
			}
			int num6 = this.pointsQueue.Count - 1;
			int[] array4 = new int[num6 * 2 * 3];
			int num7 = 0;
			int num8 = 0;
			for (int i = 0; i < num6; i++)
			{
				array4[num7] = num8;
				array4[num7 + 1] = num8 + 1;
				array4[num7 + 2] = num8 + 2;
				array4[num7 + 3] = num8 + 3;
				array4[num7 + 4] = num8 + 1;
				array4[num7 + 5] = num8 + 2;
				num7 += 6;
				num8 += 2;
			}
			this.mesh.Clear();
			this.mesh.vertices = array;
			this.mesh.uv = array2;
			this.mesh.triangles = array4;
			this.mesh.colors = array3;
			this.mesh.RecalculateBounds();
			this.mesh.UploadMeshData(false);
		}

		// Token: 0x04003274 RID: 12916
		private MeshFilter meshFilter;

		// Token: 0x04003275 RID: 12917
		private MeshRenderer meshRenderer;

		// Token: 0x04003276 RID: 12918
		private Mesh mesh;

		// Token: 0x04003277 RID: 12919
		public float timeStep = 0.0055555557f;

		// Token: 0x04003278 RID: 12920
		public float width = 1f;

		// Token: 0x04003279 RID: 12921
		public Material[] sharedMaterials;

		// Token: 0x0400327A RID: 12922
		public float trailLifetime = 1f;

		// Token: 0x0400327B RID: 12923
		public bool fadeVertexAlpha = true;

		// Token: 0x0400327C RID: 12924
		private Vector3 previousPosition;

		// Token: 0x0400327D RID: 12925
		private Vector3 previousUp;

		// Token: 0x0400327E RID: 12926
		private float previousTime;

		// Token: 0x0400327F RID: 12927
		private Queue<SmoothTrailMesh.Point> pointsQueue = new Queue<SmoothTrailMesh.Point>();

		// Token: 0x0200089F RID: 2207
		[Serializable]
		private struct Point
		{
			// Token: 0x04003280 RID: 12928
			public Vector3 vertex1;

			// Token: 0x04003281 RID: 12929
			public Vector3 vertex2;

			// Token: 0x04003282 RID: 12930
			public float time;
		}
	}
}
