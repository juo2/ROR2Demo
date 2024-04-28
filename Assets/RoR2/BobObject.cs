using System;
using RoR2;
using UnityEngine;

// Token: 0x02000034 RID: 52
public class BobObject : MonoBehaviour
{
	// Token: 0x060000F1 RID: 241 RVA: 0x00005540 File Offset: 0x00003740
	private void Start()
	{
		if (base.transform.parent)
		{
			this.initialPosition = base.transform.localPosition;
			return;
		}
		this.initialPosition = base.transform.position;
	}

	// Token: 0x060000F2 RID: 242 RVA: 0x00005578 File Offset: 0x00003778
	private void FixedUpdate()
	{
		if (Run.instance)
		{
			Vector3 vector = this.initialPosition + this.bobDistance * Mathf.Sin(Run.instance.fixedTime - this.bobDelay);
			if (base.transform.parent)
			{
				base.transform.localPosition = vector;
				return;
			}
			base.transform.position = vector;
		}
	}

	// Token: 0x040000EA RID: 234
	public float bobDelay;

	// Token: 0x040000EB RID: 235
	public Vector3 bobDistance = Vector3.zero;

	// Token: 0x040000EC RID: 236
	private Vector3 initialPosition;
}
