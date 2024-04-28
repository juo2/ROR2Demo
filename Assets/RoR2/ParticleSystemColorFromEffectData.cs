using System;
using UnityEngine;

namespace RoR2
{
	// Token: 0x020007FA RID: 2042
	[RequireComponent(typeof(EffectComponent))]
	public class ParticleSystemColorFromEffectData : MonoBehaviour
	{
		// Token: 0x06002C08 RID: 11272 RVA: 0x000BC638 File Offset: 0x000BA838
		private void Start()
		{
			Color color = this.effectComponent.effectData.color;
			for (int i = 0; i < this.particleSystems.Length; i++)
			{
				this.particleSystems[i].main.startColor = color;
				this.particleSystems[i].Clear();
				this.particleSystems[i].Play();
			}
		}

		// Token: 0x04002E80 RID: 11904
		public ParticleSystem[] particleSystems;

		// Token: 0x04002E81 RID: 11905
		public EffectComponent effectComponent;
	}
}
