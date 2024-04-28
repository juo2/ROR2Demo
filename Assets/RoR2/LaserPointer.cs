using System;
using UnityEngine;

namespace RoR2
{
	// Token: 0x02000791 RID: 1937
	[RequireComponent(typeof(LineRenderer))]
	public class LaserPointer : MonoBehaviour
	{
		// Token: 0x060028E6 RID: 10470 RVA: 0x000B17F2 File Offset: 0x000AF9F2
		private void Start()
		{
			this.line = base.GetComponent<LineRenderer>();
		}

		// Token: 0x060028E7 RID: 10471 RVA: 0x000B1800 File Offset: 0x000AFA00
		private void Update()
		{
			RaycastHit raycastHit;
			if (Physics.Raycast(base.transform.position, base.transform.forward, out raycastHit, this.laserDistance, LayerIndex.world.mask))
			{
				this.line.SetPosition(0, base.transform.position);
				this.line.SetPosition(1, raycastHit.point);
			}
		}

		// Token: 0x04002C65 RID: 11365
		public float laserDistance;

		// Token: 0x04002C66 RID: 11366
		private LineRenderer line;
	}
}
