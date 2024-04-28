using System;
using UnityEngine;

namespace RoR2
{
	// Token: 0x02000792 RID: 1938
	internal class LaserPointerController : MonoBehaviour
	{
		// Token: 0x060028E9 RID: 10473 RVA: 0x000B1870 File Offset: 0x000AFA70
		private void LateUpdate()
		{
			bool enabled = false;
			bool active = false;
			if (this.source)
			{
				Ray ray = new Ray(this.source.aimOrigin, this.source.aimDirection);
				RaycastHit raycastHit;
				if (Physics.Raycast(ray, out raycastHit, float.PositiveInfinity, LayerIndex.world.mask | LayerIndex.entityPrecise.mask, QueryTriggerInteraction.UseGlobal))
				{
					base.transform.position = raycastHit.point;
					base.transform.forward = -ray.direction;
					float num = raycastHit.distance - this.minDistanceFromStart;
					if (num >= 0.1f)
					{
						this.beam.SetPosition(1, new Vector3(0f, 0f, num));
						enabled = true;
					}
					active = true;
				}
			}
			this.dotObject.SetActive(active);
			this.beam.enabled = enabled;
		}

		// Token: 0x04002C67 RID: 11367
		public InputBankTest source;

		// Token: 0x04002C68 RID: 11368
		public GameObject dotObject;

		// Token: 0x04002C69 RID: 11369
		public LineRenderer beam;

		// Token: 0x04002C6A RID: 11370
		public float minDistanceFromStart = 4f;
	}
}
