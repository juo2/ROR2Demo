using System;
using UnityEngine;

// Token: 0x02000050 RID: 80
public class PositionFromParentRaycast : MonoBehaviour
{
	// Token: 0x06000161 RID: 353 RVA: 0x00007934 File Offset: 0x00005B34
	private void Update()
	{
		RaycastHit raycastHit = default(RaycastHit);
		if (Physics.Raycast(base.transform.parent.position, base.transform.parent.forward, out raycastHit, this.maxLength, this.mask))
		{
			base.transform.position = raycastHit.point;
			return;
		}
		base.transform.position = base.transform.parent.position + base.transform.parent.forward * this.maxLength;
	}

	// Token: 0x0400016C RID: 364
	public float maxLength;

	// Token: 0x0400016D RID: 365
	public LayerMask mask;
}
