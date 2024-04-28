using System;
using UnityEngine;

// Token: 0x0200005C RID: 92
public class ShrinePlaceTotem : MonoBehaviour
{
	// Token: 0x06000187 RID: 391 RVA: 0x00007FD4 File Offset: 0x000061D4
	private void Awake()
	{
		int num = 0;
		for (int i = 0; i < this.totemCount * 2; i++)
		{
			float num2 = (i >= this.totemCount) ? ((float)i * 1f * (360f / (float)this.totemCount)) : (((float)i + 0.5f) * (360f / (float)this.totemCount));
			RaycastHit raycastHit;
			Physics.Raycast(base.transform.position + new Vector3(0f, this.height, 0f), new Vector3(Mathf.Cos(num2 * 0.017453292f) * this.totemRadius, -this.height, Mathf.Sin(num2 * 0.017453292f) * this.totemRadius), out raycastHit, this.raycastDistance);
			if (raycastHit.collider != null && Vector3.Dot(raycastHit.normal, Vector3.up) > this.dotLimit)
			{
				GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.totem, raycastHit.point, Quaternion.identity);
				gameObject.transform.parent = base.transform;
				gameObject.transform.rotation = Quaternion.FromToRotation(gameObject.transform.up, raycastHit.normal);
				gameObject.transform.eulerAngles += new Vector3(UnityEngine.Random.Range(-this.bendAmount, this.bendAmount), UnityEngine.Random.Range(-this.bendAmount, this.bendAmount), UnityEngine.Random.Range(-this.bendAmount, this.bendAmount));
				gameObject.transform.position -= new Vector3(0f, UnityEngine.Random.Range(0.1f, 0.2f), 0f);
				num++;
			}
			if (num == this.totemCount)
			{
				break;
			}
		}
	}

	// Token: 0x06000188 RID: 392 RVA: 0x000026ED File Offset: 0x000008ED
	private void Update()
	{
	}

	// Token: 0x04000191 RID: 401
	public int totemCount = 5;

	// Token: 0x04000192 RID: 402
	public GameObject totem;

	// Token: 0x04000193 RID: 403
	[Tooltip("Distance from which to form totem ring")]
	public float totemRadius = 2f;

	// Token: 0x04000194 RID: 404
	[Tooltip("Height from which to calculate totem placements.")]
	public float height = 10f;

	// Token: 0x04000195 RID: 405
	[Tooltip("Distance to raycast for totems")]
	public float raycastDistance = 20f;

	// Token: 0x04000196 RID: 406
	[Tooltip("Random bending of totems")]
	public float bendAmount = 15f;

	// Token: 0x04000197 RID: 407
	[Tooltip("Allowed difference from straight up (1) to straight down (-1)")]
	public float dotLimit = 0.8f;
}
