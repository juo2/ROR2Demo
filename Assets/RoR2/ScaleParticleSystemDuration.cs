using System;
using UnityEngine;

namespace RoR2
{
	// Token: 0x02000868 RID: 2152
	public class ScaleParticleSystemDuration : MonoBehaviour
	{
		// Token: 0x17000443 RID: 1091
		// (get) Token: 0x06002F1D RID: 12061 RVA: 0x000C8F17 File Offset: 0x000C7117
		// (set) Token: 0x06002F1C RID: 12060 RVA: 0x000C8EFF File Offset: 0x000C70FF
		public float newDuration
		{
			get
			{
				return this._newDuration;
			}
			set
			{
				if (this._newDuration != value)
				{
					this._newDuration = value;
					this.UpdateParticleDurations();
				}
			}
		}

		// Token: 0x06002F1E RID: 12062 RVA: 0x000C8F1F File Offset: 0x000C711F
		private void Start()
		{
			this.UpdateParticleDurations();
		}

		// Token: 0x06002F1F RID: 12063 RVA: 0x000C8F28 File Offset: 0x000C7128
		private void UpdateParticleDurations()
		{
			float simulationSpeed = this.initialDuration / this._newDuration;
			for (int i = 0; i < this.particleSystems.Length; i++)
			{
				ParticleSystem particleSystem = this.particleSystems[i];
				if (particleSystem)
				{
					particleSystem.main.simulationSpeed = simulationSpeed;
				}
			}
		}

		// Token: 0x04003105 RID: 12549
		public float initialDuration = 1f;

		// Token: 0x04003106 RID: 12550
		private float _newDuration = 1f;

		// Token: 0x04003107 RID: 12551
		public ParticleSystem[] particleSystems;
	}
}
