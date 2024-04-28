using System;
using UnityEngine;

namespace RoR2
{
	// Token: 0x02000767 RID: 1895
	[RequireComponent(typeof(EffectComponent))]
	internal class ImpactEffect : MonoBehaviour
	{
		// Token: 0x06002729 RID: 10025 RVA: 0x000AA4C0 File Offset: 0x000A86C0
		private void Start()
		{
			EffectComponent component = base.GetComponent<EffectComponent>();
			Color color = (component.effectData != null) ? component.effectData.color : Color.white;
			for (int i = 0; i < this.particleSystems.Length; i++)
			{
				this.particleSystems[i].main.startColor = color;
				this.particleSystems[i].Play();
			}
		}

		// Token: 0x04002B26 RID: 11046
		public ParticleSystem[] particleSystems = Array.Empty<ParticleSystem>();
	}
}
