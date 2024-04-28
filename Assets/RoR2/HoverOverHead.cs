using System;
using UnityEngine;

// Token: 0x02000045 RID: 69
public class HoverOverHead : MonoBehaviour
{
	// Token: 0x06000133 RID: 307 RVA: 0x0000709A File Offset: 0x0000529A
	private void Start()
	{
		this.parentTransform = base.transform.parent;
		this.bodyCollider = base.transform.parent.GetComponent<Collider>();
	}

	// Token: 0x06000134 RID: 308 RVA: 0x000070C4 File Offset: 0x000052C4
	private void Update()
	{
		Vector3 a = this.parentTransform.position;
		if (this.bodyCollider)
		{
			a = this.bodyCollider.bounds.center + new Vector3(0f, this.bodyCollider.bounds.extents.y, 0f);
		}
		base.transform.position = a + this.bonusOffset;
	}

	// Token: 0x04000149 RID: 329
	private Transform parentTransform;

	// Token: 0x0400014A RID: 330
	private Collider bodyCollider;

	// Token: 0x0400014B RID: 331
	public Vector3 bonusOffset;
}
