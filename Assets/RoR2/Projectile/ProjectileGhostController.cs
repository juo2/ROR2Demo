using System;
using UnityEngine;

namespace RoR2.Projectile
{
	// Token: 0x02000B94 RID: 2964
	public class ProjectileGhostController : MonoBehaviour
	{
		// Token: 0x1700062F RID: 1583
		// (get) Token: 0x0600437A RID: 17274 RVA: 0x001183D7 File Offset: 0x001165D7
		// (set) Token: 0x0600437B RID: 17275 RVA: 0x001183DF File Offset: 0x001165DF
		public Transform authorityTransform { get; set; }

		// Token: 0x17000630 RID: 1584
		// (get) Token: 0x0600437C RID: 17276 RVA: 0x001183E8 File Offset: 0x001165E8
		// (set) Token: 0x0600437D RID: 17277 RVA: 0x001183F0 File Offset: 0x001165F0
		public Transform predictionTransform { get; set; }

		// Token: 0x0600437E RID: 17278 RVA: 0x001183F9 File Offset: 0x001165F9
		private void Awake()
		{
			this.transform = base.transform;
		}

		// Token: 0x0600437F RID: 17279 RVA: 0x00118408 File Offset: 0x00116608
		private void Update()
		{
			if (this.authorityTransform ^ this.predictionTransform)
			{
				this.CopyTransform(this.authorityTransform ? this.authorityTransform : this.predictionTransform);
				return;
			}
			if (this.authorityTransform)
			{
				this.LerpTransform(this.predictionTransform, this.authorityTransform, this.migration);
				if (this.migration == 1f)
				{
					this.predictionTransform = null;
					return;
				}
			}
			else
			{
				UnityEngine.Object.Destroy(base.gameObject);
			}
		}

		// Token: 0x06004380 RID: 17280 RVA: 0x00118498 File Offset: 0x00116698
		private void LerpTransform(Transform a, Transform b, float t)
		{
			this.transform.position = Vector3.LerpUnclamped(a.position, b.position, t);
			this.transform.rotation = Quaternion.SlerpUnclamped(a.rotation, b.rotation, t);
			if (this.inheritScaleFromProjectile)
			{
				this.transform.localScale = Vector3.Lerp(a.localScale, b.localScale, t);
			}
		}

		// Token: 0x06004381 RID: 17281 RVA: 0x00118504 File Offset: 0x00116704
		private void CopyTransform(Transform src)
		{
			this.transform.position = src.position;
			this.transform.rotation = src.rotation;
			if (this.inheritScaleFromProjectile)
			{
				this.transform.localScale = src.localScale;
			}
		}

		// Token: 0x040041CB RID: 16843
		private new Transform transform;

		// Token: 0x040041CC RID: 16844
		private float migration;

		// Token: 0x040041CF RID: 16847
		[Tooltip("Sets the ghost's scale to match the base projectile.")]
		public bool inheritScaleFromProjectile;
	}
}
