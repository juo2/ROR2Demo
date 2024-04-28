using System;
using UnityEngine;

// Token: 0x02000056 RID: 86
public class RotateItem : MonoBehaviour
{
	// Token: 0x06000174 RID: 372 RVA: 0x00007CED File Offset: 0x00005EED
	private void Start()
	{
		this.initialPosition = base.transform.position;
	}

	// Token: 0x06000175 RID: 373 RVA: 0x00007D00 File Offset: 0x00005F00
	private void Update()
	{
		this.counter += Time.deltaTime;
		base.transform.Rotate(new Vector3(0f, this.spinSpeed * Time.deltaTime, 0f), Space.World);
		if (base.transform.parent)
		{
			base.transform.localPosition = this.offsetVector + new Vector3(0f, 0f, Mathf.Sin(this.counter) * this.bobHeight);
			return;
		}
		base.transform.position = this.initialPosition + new Vector3(0f, Mathf.Sin(this.counter) * this.bobHeight, 0f);
	}

	// Token: 0x04000182 RID: 386
	public float spinSpeed = 30f;

	// Token: 0x04000183 RID: 387
	public float bobHeight = 0.3f;

	// Token: 0x04000184 RID: 388
	public Vector3 offsetVector = Vector3.zero;

	// Token: 0x04000185 RID: 389
	private float counter;

	// Token: 0x04000186 RID: 390
	private Vector3 initialPosition;
}
