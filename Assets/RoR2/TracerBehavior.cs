using System;
using UnityEngine;

// Token: 0x02000060 RID: 96
public class TracerBehavior : MonoBehaviour
{
	// Token: 0x06000194 RID: 404 RVA: 0x000084B1 File Offset: 0x000066B1
	private void Start()
	{
		this.direction = Vector3.Normalize(this.positions[1] - this.positions[0]);
	}

	// Token: 0x06000195 RID: 405 RVA: 0x000084DC File Offset: 0x000066DC
	private void Update()
	{
		if (Vector3.Distance(base.transform.position, this.positions[1]) > this.speed * Time.deltaTime)
		{
			base.transform.position += this.direction * this.speed * Time.deltaTime;
		}
	}

	// Token: 0x040001AB RID: 427
	public float speed = 1f;

	// Token: 0x040001AC RID: 428
	public Vector3[] positions;

	// Token: 0x040001AD RID: 429
	private Vector3 direction;
}
