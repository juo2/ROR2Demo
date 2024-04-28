using System;
using System.Collections.Generic;
using UnityEngine;

namespace RoR2
{
	// Token: 0x020005FA RID: 1530
	public class BoneParticleController : MonoBehaviour
	{
		// Token: 0x06001BF1 RID: 7153 RVA: 0x00076FB8 File Offset: 0x000751B8
		private void Start()
		{
			this.bonesList = new List<Transform>();
			foreach (Transform transform in this.skinnedMeshRenderer.bones)
			{
				if (transform.name.IndexOf("IK", StringComparison.OrdinalIgnoreCase) == -1 && transform.name.IndexOf("Root", StringComparison.OrdinalIgnoreCase) == -1 && transform.name.IndexOf("Base", StringComparison.OrdinalIgnoreCase) == -1)
				{
					Debug.LogFormat("added bone {0}", new object[]
					{
						transform
					});
					this.bonesList.Add(transform);
				}
			}
		}

		// Token: 0x06001BF2 RID: 7154 RVA: 0x0007704C File Offset: 0x0007524C
		private void Update()
		{
			if (this.skinnedMeshRenderer)
			{
				this.stopwatch += Time.deltaTime;
				if (this.stopwatch > 1f / this.spawnFrequency)
				{
					this.stopwatch -= 1f / this.spawnFrequency;
					int count = this.bonesList.Count;
					Transform transform = this.bonesList[UnityEngine.Random.Range(0, count)];
					if (transform)
					{
						UnityEngine.Object.Instantiate<GameObject>(this.childParticlePrefab, transform.transform.position, Quaternion.identity, transform);
					}
				}
			}
		}

		// Token: 0x040021C1 RID: 8641
		public GameObject childParticlePrefab;

		// Token: 0x040021C2 RID: 8642
		public float spawnFrequency;

		// Token: 0x040021C3 RID: 8643
		public SkinnedMeshRenderer skinnedMeshRenderer;

		// Token: 0x040021C4 RID: 8644
		private float stopwatch;

		// Token: 0x040021C5 RID: 8645
		private List<Transform> bonesList;
	}
}
