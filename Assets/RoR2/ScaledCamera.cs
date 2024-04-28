using System;
using UnityEngine;

// Token: 0x0200005A RID: 90
public class ScaledCamera : MonoBehaviour
{
	// Token: 0x06000181 RID: 385 RVA: 0x000026ED File Offset: 0x000008ED
	private void Start()
	{
	}

	// Token: 0x06000182 RID: 386 RVA: 0x00007EF0 File Offset: 0x000060F0
	private void LateUpdate()
	{
		Camera main = Camera.main;
		if (main != null)
		{
			if (!this.foundCamera)
			{
				this.foundCamera = true;
				this.offset = main.transform.position - base.transform.position;
			}
			base.transform.eulerAngles = main.transform.eulerAngles;
			base.transform.position = main.transform.position / this.scale - this.offset;
		}
	}

	// Token: 0x0400018C RID: 396
	public float scale = 1f;

	// Token: 0x0400018D RID: 397
	private bool foundCamera;

	// Token: 0x0400018E RID: 398
	private Vector3 offset;
}
