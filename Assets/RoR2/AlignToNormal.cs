using System;
using UnityEngine;

namespace RoR2
{
	// Token: 0x020005C4 RID: 1476
	public class AlignToNormal : MonoBehaviour
	{
		// Token: 0x06001ABA RID: 6842 RVA: 0x00072C04 File Offset: 0x00070E04
		private void Start()
		{
			RaycastHit raycastHit;
			if (Physics.Raycast(base.transform.position + base.transform.up * this.offsetDistance, -base.transform.up, out raycastHit, this.maxDistance, LayerIndex.world.mask))
			{
				base.transform.position = raycastHit.point;
				if (!this.changePositionOnly)
				{
					base.transform.up = raycastHit.normal;
				}
			}
		}

		// Token: 0x040020DD RID: 8413
		[Tooltip("The amount to raycast down from.")]
		public float maxDistance;

		// Token: 0x040020DE RID: 8414
		[Tooltip("The amount to pull the object out of the ground initially to test.")]
		public float offsetDistance;

		// Token: 0x040020DF RID: 8415
		[Tooltip("Send to floor only - don't change normals.")]
		public bool changePositionOnly;
	}
}
