using System;
using UnityEngine;

namespace RoR2
{
	// Token: 0x0200069F RID: 1695
	public class DetachParticleOnDestroyAndEndEmission : MonoBehaviour
	{
		// Token: 0x06002119 RID: 8473 RVA: 0x0008DEAC File Offset: 0x0008C0AC
		private void OnDisable()
		{
			if (this.particleSystem)
			{
				this.particleSystem.emission.enabled = false;
				this.particleSystem.main.stopAction = ParticleSystemStopAction.Destroy;
				this.particleSystem.Stop();
				this.particleSystem.transform.SetParent(null);
			}
		}

		// Token: 0x0400266E RID: 9838
		public ParticleSystem particleSystem;
	}
}
