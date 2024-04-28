using System;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x0200000F RID: 15
public class OnParticleEndEvent : MonoBehaviour
{
	// Token: 0x0600003D RID: 61 RVA: 0x00002F88 File Offset: 0x00001188
	public void Update()
	{
		if (this.particleSystemToTrack && !this.particleSystemToTrack.IsAlive() && !this.particleEnded)
		{
			this.particleEnded = true;
			this.onEnd.Invoke();
		}
	}

	// Token: 0x0400004C RID: 76
	public ParticleSystem particleSystemToTrack;

	// Token: 0x0400004D RID: 77
	public UnityEvent onEnd;

	// Token: 0x0400004E RID: 78
	private bool particleEnded;
}
