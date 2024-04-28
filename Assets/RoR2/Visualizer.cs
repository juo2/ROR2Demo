using System;
using UnityEngine;

// Token: 0x02000066 RID: 102
public class Visualizer : MonoBehaviour
{
	// Token: 0x060001A5 RID: 421 RVA: 0x000088BA File Offset: 0x00006ABA
	private void Start()
	{
		this.initialPos = this.particleObject.transform.localPosition;
	}

	// Token: 0x060001A6 RID: 422 RVA: 0x000088D2 File Offset: 0x00006AD2
	private void Update()
	{
		this.particleObject.transform.localPosition = this.initialPos + new Vector3(0f, this.yvalue / this.yscale, 0f);
	}

	// Token: 0x040001C6 RID: 454
	public float yscale;

	// Token: 0x040001C7 RID: 455
	public GameObject particleObject;

	// Token: 0x040001C8 RID: 456
	public float yvalue;

	// Token: 0x040001C9 RID: 457
	private Vector3 initialPos;
}
