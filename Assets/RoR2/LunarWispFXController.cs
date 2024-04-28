using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200004C RID: 76
public class LunarWispFXController : MonoBehaviour
{
	// Token: 0x06000152 RID: 338 RVA: 0x000026ED File Offset: 0x000008ED
	private void Start()
	{
	}

	// Token: 0x06000153 RID: 339 RVA: 0x000026ED File Offset: 0x000008ED
	private void FixedUpdate()
	{
	}

	// Token: 0x06000154 RID: 340 RVA: 0x000075A4 File Offset: 0x000057A4
	public void TurnOffFX()
	{
		for (int i = 0; i < this.FXParticles.Count; i++)
		{
			this.FXParticles[i].emission.enabled = false;
		}
	}

	// Token: 0x06000155 RID: 341 RVA: 0x000075E4 File Offset: 0x000057E4
	public void TurnOnFX()
	{
		for (int i = 0; i < this.FXParticles.Count; i++)
		{
			this.FXParticles[i].emission.enabled = true;
		}
	}

	// Token: 0x04000160 RID: 352
	public List<ParticleSystem> FXParticles = new List<ParticleSystem>();
}
