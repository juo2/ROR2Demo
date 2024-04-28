using System;
using UnityEngine;

namespace RoR2.Projectile
{
	// Token: 0x02000B90 RID: 2960
	public class ProjectileFireEffects : MonoBehaviour
	{
		// Token: 0x06004367 RID: 17255 RVA: 0x00117F58 File Offset: 0x00116158
		private void Update()
		{
			this.timer += Time.deltaTime;
			this.nextSpawnTimer += Time.deltaTime;
			if (this.timer >= this.duration)
			{
				UnityEngine.Object.Destroy(base.gameObject);
			}
			if (this.nextSpawnTimer >= this.duration / (float)this.count)
			{
				this.nextSpawnTimer -= this.duration / (float)this.count;
				if (this.effectPrefab)
				{
					Vector3 b = new Vector3(UnityEngine.Random.Range(-this.randomOffset.x, this.randomOffset.x), UnityEngine.Random.Range(-this.randomOffset.y, this.randomOffset.y), UnityEngine.Random.Range(-this.randomOffset.z, this.randomOffset.z));
					EffectManager.SimpleImpactEffect(this.effectPrefab, base.transform.position + b, Vector3.forward, true);
				}
			}
		}

		// Token: 0x040041B7 RID: 16823
		public float duration = 5f;

		// Token: 0x040041B8 RID: 16824
		public int count = 5;

		// Token: 0x040041B9 RID: 16825
		public GameObject effectPrefab;

		// Token: 0x040041BA RID: 16826
		public Vector3 randomOffset;

		// Token: 0x040041BB RID: 16827
		private float timer;

		// Token: 0x040041BC RID: 16828
		private float nextSpawnTimer;
	}
}
