using System;
using UnityEngine;

namespace RoR2.Projectile
{
	// Token: 0x02000B93 RID: 2963
	public class ProjectileGhostCluster : MonoBehaviour
	{
		// Token: 0x06004377 RID: 17271 RVA: 0x00118300 File Offset: 0x00116500
		private void Start()
		{
			float num = 1f / (Mathf.Log((float)this.clusterCount, 4f) + 1f);
			Vector3 position = base.transform.position;
			for (int i = 0; i < this.clusterCount; i++)
			{
				Vector3 b;
				if (this.distributeEvenly)
				{
					b = Vector3.zero;
				}
				else
				{
					b = UnityEngine.Random.insideUnitSphere * this.clusterDistance;
				}
				GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.ghostClusterPrefab, position + b, Quaternion.identity, base.transform);
				gameObject.transform.localScale = Vector3.one / (Mathf.Log((float)this.clusterCount, 4f) + 1f);
				TrailRenderer component = gameObject.GetComponent<TrailRenderer>();
				if (component)
				{
					component.widthMultiplier *= num;
				}
			}
		}

		// Token: 0x06004378 RID: 17272 RVA: 0x000026ED File Offset: 0x000008ED
		private void Update()
		{
		}

		// Token: 0x040041C7 RID: 16839
		public GameObject ghostClusterPrefab;

		// Token: 0x040041C8 RID: 16840
		public int clusterCount;

		// Token: 0x040041C9 RID: 16841
		public bool distributeEvenly;

		// Token: 0x040041CA RID: 16842
		public float clusterDistance;
	}
}
