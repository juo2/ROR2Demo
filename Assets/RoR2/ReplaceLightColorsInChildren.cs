using System;
using UnityEngine;

namespace RoR2
{
	// Token: 0x02000847 RID: 2119
	public class ReplaceLightColorsInChildren : MonoBehaviour
	{
		// Token: 0x06002E29 RID: 11817 RVA: 0x000C4B60 File Offset: 0x000C2D60
		private void Awake()
		{
			foreach (Light light in base.GetComponentsInChildren<Light>())
			{
				light.color = this.newLightColor;
				light.intensity *= this.intensityMultiplier;
			}
			if (this.newParticleSystemMaterial)
			{
				ParticleSystem[] componentsInChildren2 = base.GetComponentsInChildren<ParticleSystem>();
				for (int i = 0; i < componentsInChildren2.Length; i++)
				{
					ParticleSystemRenderer component = componentsInChildren2[i].GetComponent<ParticleSystemRenderer>();
					if (component)
					{
						component.material = this.newParticleSystemMaterial;
					}
				}
			}
		}

		// Token: 0x06002E2A RID: 11818 RVA: 0x000026ED File Offset: 0x000008ED
		private void Update()
		{
		}

		// Token: 0x0400302B RID: 12331
		public Color newLightColor;

		// Token: 0x0400302C RID: 12332
		public float intensityMultiplier;

		// Token: 0x0400302D RID: 12333
		public Material newParticleSystemMaterial;
	}
}
