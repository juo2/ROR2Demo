using System;
using UnityEngine;

// Token: 0x02000058 RID: 88
public class ScaleLineRenderer : MonoBehaviour
{
	// Token: 0x0600017A RID: 378 RVA: 0x00007E0D File Offset: 0x0000600D
	private void Start()
	{
		this.line = base.GetComponent<LineRenderer>();
		this.SetScale();
	}

	// Token: 0x0600017B RID: 379 RVA: 0x000026ED File Offset: 0x000008ED
	private void Update()
	{
	}

	// Token: 0x0600017C RID: 380 RVA: 0x00007E24 File Offset: 0x00006024
	private void SetScale()
	{
		this.line.SetPosition(0, this.positions[0]);
		this.line.SetPosition(1, this.positions[1]);
		this.line.material.SetTextureScale("_MainTex", new Vector2(Vector3.Distance(this.positions[0], this.positions[1]) * this.scaleSize, 1f));
	}

	// Token: 0x04000188 RID: 392
	private LineRenderer line;

	// Token: 0x04000189 RID: 393
	public float scaleSize = 1f;

	// Token: 0x0400018A RID: 394
	public Vector3[] positions;
}
