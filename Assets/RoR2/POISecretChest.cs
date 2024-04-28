using System;
using UnityEngine;

// Token: 0x0200004E RID: 78
public class POISecretChest : MonoBehaviour
{
	// Token: 0x0600015A RID: 346 RVA: 0x00007648 File Offset: 0x00005848
	private void OnDrawGizmos()
	{
		Gizmos.matrix = base.transform.localToWorldMatrix;
		Gizmos.color = new Color(0f, 1f, 1f, 0.03f);
		Gizmos.DrawCube(Vector3.zero, base.transform.localScale / 2f);
		Gizmos.color = new Color(0f, 1f, 1f, 0.1f);
		Gizmos.DrawWireCube(Vector3.zero, base.transform.localScale / 2f);
	}

	// Token: 0x04000162 RID: 354
	public float influence = 5f;
}
