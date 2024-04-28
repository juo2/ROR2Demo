using System;
using UnityEngine;

// Token: 0x02000057 RID: 87
public class RotateObject : MonoBehaviour
{
	// Token: 0x06000177 RID: 375 RVA: 0x000026ED File Offset: 0x000008ED
	private void Start()
	{
	}

	// Token: 0x06000178 RID: 376 RVA: 0x00007DF0 File Offset: 0x00005FF0
	private void Update()
	{
		base.transform.Rotate(this.rotationSpeed * Time.deltaTime);
	}

	// Token: 0x04000187 RID: 391
	public Vector3 rotationSpeed;
}
