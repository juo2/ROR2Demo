using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace RoR2
{
	// Token: 0x020006A0 RID: 1696
	public class DetachTrailOnDestroy : MonoBehaviour
	{
		// Token: 0x0600211B RID: 8475 RVA: 0x0008DF0C File Offset: 0x0008C10C
		private void OnDestroy()
		{
			for (int i = 0; i < this.targetTrailRenderers.Length; i++)
			{
				TrailRenderer trailRenderer = this.targetTrailRenderers[i];
				if (trailRenderer)
				{
					trailRenderer.transform.SetParent(null);
					trailRenderer.autodestruct = true;
				}
			}
			this.targetTrailRenderers = null;
		}

		// Token: 0x0400266F RID: 9839
		[FormerlySerializedAs("trail")]
		public TrailRenderer[] targetTrailRenderers;
	}
}
