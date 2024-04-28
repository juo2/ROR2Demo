using System;
using UnityEngine;

// Token: 0x02000054 RID: 84
public class RenderInFrontOfParticles : MonoBehaviour
{
	// Token: 0x0600016C RID: 364 RVA: 0x00007C3C File Offset: 0x00005E3C
	private void Start()
	{
		this.rend = base.GetComponent<Renderer>();
		this.rend.material.renderQueue = this.renderOrder;
	}

	// Token: 0x0600016D RID: 365 RVA: 0x000026ED File Offset: 0x000008ED
	private void Update()
	{
	}

	// Token: 0x0400017C RID: 380
	public int renderOrder;

	// Token: 0x0400017D RID: 381
	private Renderer rend;
}
