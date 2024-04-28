using System;
using UnityEngine;

// Token: 0x0200004D RID: 77
[ExecuteAlways]
public class MaintainRotation : MonoBehaviour
{
	// Token: 0x06000157 RID: 343 RVA: 0x000026ED File Offset: 0x000008ED
	private void Start()
	{
	}

	// Token: 0x06000158 RID: 344 RVA: 0x00007634 File Offset: 0x00005834
	private void LateUpdate()
	{
		base.transform.eulerAngles = this.eulerAngles;
	}

	// Token: 0x04000161 RID: 353
	public Vector3 eulerAngles;
}
