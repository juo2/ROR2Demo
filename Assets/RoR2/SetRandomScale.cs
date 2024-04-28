using System;
using UnityEngine;

// Token: 0x02000014 RID: 20
public class SetRandomScale : MonoBehaviour
{
	// Token: 0x06000052 RID: 82 RVA: 0x000032D8 File Offset: 0x000014D8
	private void Start()
	{
		float d = UnityEngine.Random.Range(this.minimumScale, this.maximumScale);
		base.transform.localScale = Vector3.one * d;
	}

	// Token: 0x0400005E RID: 94
	public float minimumScale;

	// Token: 0x0400005F RID: 95
	public float maximumScale;
}
