using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000010 RID: 16
public class ParentEnergyFXController : MonoBehaviour
{
	// Token: 0x0600003F RID: 63 RVA: 0x000026ED File Offset: 0x000008ED
	private void Start()
	{
	}

	// Token: 0x06000040 RID: 64 RVA: 0x000026ED File Offset: 0x000008ED
	private void FixedUpdate()
	{
	}

	// Token: 0x06000041 RID: 65 RVA: 0x00002FC8 File Offset: 0x000011C8
	public void TurnOffFX()
	{
		for (int i = 0; i < this.energyFXParticles.Count; i++)
		{
			this.energyFXParticles[i].emission.enabled = false;
		}
	}

	// Token: 0x06000042 RID: 66 RVA: 0x00003008 File Offset: 0x00001208
	public void TurnOnFX()
	{
		for (int i = 0; i < this.energyFXParticles.Count; i++)
		{
			this.energyFXParticles[i].emission.enabled = true;
		}
	}

	// Token: 0x06000043 RID: 67 RVA: 0x00003048 File Offset: 0x00001248
	public void SetLoomingPresenceParticles(bool setTo)
	{
		this.loomingPresenceParticles.emission.enabled = setTo;
	}

	// Token: 0x0400004F RID: 79
	public List<ParticleSystem> energyFXParticles = new List<ParticleSystem>();

	// Token: 0x04000050 RID: 80
	public ParticleSystem loomingPresenceParticles;
}
