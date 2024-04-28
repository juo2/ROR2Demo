using System;
using System.Collections.Generic;
using RoR2;
using UnityEngine;

// Token: 0x02000008 RID: 8
public class GrandparentEnergyFXController : MonoBehaviour
{
	// Token: 0x06000013 RID: 19 RVA: 0x000026ED File Offset: 0x000008ED
	private void Start()
	{
	}

	// Token: 0x06000014 RID: 20 RVA: 0x000026F0 File Offset: 0x000008F0
	private void FixedUpdate()
	{
		if (this.portalObject != null)
		{
			if (!this.isPortalSoundPlaying)
			{
				if (this.portalObject.transform.localScale == Vector3.zero)
				{
					Util.PlaySound("Play_grandparent_portal_loop", this.portalObject);
					this.isPortalSoundPlaying = false;
					return;
				}
			}
			else if (this.portalObject.transform.localScale != Vector3.zero)
			{
				Util.PlaySound("Stop_grandparent_portal_loop", this.portalObject);
				this.isPortalSoundPlaying = true;
			}
		}
	}

	// Token: 0x06000015 RID: 21 RVA: 0x0000277C File Offset: 0x0000097C
	public void TurnOffFX()
	{
		for (int i = 0; i < this.energyFXParticles.Count; i++)
		{
			this.energyFXParticles[i].emission.enabled = false;
		}
		if (this.portalObject != null)
		{
			ParticleSystem componentInChildren = this.portalObject.GetComponentInChildren<ParticleSystem>();
			if (componentInChildren != null)
			{
				componentInChildren.emission.enabled = false;
			}
		}
	}

	// Token: 0x06000016 RID: 22 RVA: 0x000027EC File Offset: 0x000009EC
	public void TurnOnFX()
	{
		for (int i = 0; i < this.energyFXParticles.Count; i++)
		{
			this.energyFXParticles[i].emission.enabled = true;
		}
		if (this.portalObject != null)
		{
			ParticleSystem componentInChildren = this.portalObject.GetComponentInChildren<ParticleSystem>();
			if (componentInChildren != null)
			{
				componentInChildren.emission.enabled = true;
			}
		}
	}

	// Token: 0x04000020 RID: 32
	public List<ParticleSystem> energyFXParticles = new List<ParticleSystem>();

	// Token: 0x04000021 RID: 33
	[HideInInspector]
	public GameObject portalObject;

	// Token: 0x04000022 RID: 34
	private bool isPortalSoundPlaying;
}
