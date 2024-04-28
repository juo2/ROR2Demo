using System;
using UnityEngine;

namespace RoR2
{
	// Token: 0x020007E4 RID: 2020
	public class OmniEffect : MonoBehaviour
	{
		// Token: 0x06002BAD RID: 11181 RVA: 0x000BB17C File Offset: 0x000B937C
		private void Start()
		{
			this.radius = base.transform.localScale.x;
			foreach (OmniEffect.OmniEffectGroup omniEffectGroup in this.omniEffectGroups)
			{
				float minimumValidRadius = 0f;
				for (int j = 0; j < omniEffectGroup.omniEffectElements.Length; j++)
				{
					OmniEffect.OmniEffectElement omniEffectElement = omniEffectGroup.omniEffectElements[j];
					if (omniEffectElement.maximumValidRadius >= this.radius)
					{
						omniEffectElement.ProcessEffectElement(this.radius, minimumValidRadius);
						break;
					}
					minimumValidRadius = omniEffectElement.maximumValidRadius;
				}
			}
		}

		// Token: 0x04002E18 RID: 11800
		public OmniEffect.OmniEffectGroup[] omniEffectGroups;

		// Token: 0x04002E19 RID: 11801
		private float radius;

		// Token: 0x020007E5 RID: 2021
		[Serializable]
		public class OmniEffectElement
		{
			// Token: 0x06002BAF RID: 11183 RVA: 0x000BB208 File Offset: 0x000B9408
			public void ProcessEffectElement(float radius, float minimumValidRadius)
			{
				if (this.particleSystem)
				{
					if (this.particleSystemOverrideMaterial)
					{
						ParticleSystemRenderer component = this.particleSystem.GetComponent<ParticleSystemRenderer>();
						if (this.particleSystemOverrideMaterial)
						{
							component.material = this.particleSystemOverrideMaterial;
						}
					}
					ParticleSystem.EmissionModule emission = this.particleSystem.emission;
					if (emission.burstCount > 0)
					{
						int num = Mathf.Min((int)emission.GetBurst(0).maxCount + (int)((radius - minimumValidRadius) * this.bonusEmissionPerBonusRadius), this.particleSystem.main.maxParticles);
						emission.SetBurst(0, new ParticleSystem.Burst(0f, (float)num));
					}
					if (this.particleSystemEmitParticles)
					{
						this.particleSystem.gameObject.SetActive(true);
						return;
					}
					this.particleSystem.gameObject.SetActive(false);
				}
			}

			// Token: 0x04002E1A RID: 11802
			public string name;

			// Token: 0x04002E1B RID: 11803
			public ParticleSystem particleSystem;

			// Token: 0x04002E1C RID: 11804
			public bool particleSystemEmitParticles;

			// Token: 0x04002E1D RID: 11805
			public Material particleSystemOverrideMaterial;

			// Token: 0x04002E1E RID: 11806
			public float maximumValidRadius = float.PositiveInfinity;

			// Token: 0x04002E1F RID: 11807
			public float bonusEmissionPerBonusRadius;
		}

		// Token: 0x020007E6 RID: 2022
		[Serializable]
		public class OmniEffectGroup
		{
			// Token: 0x04002E20 RID: 11808
			public string name;

			// Token: 0x04002E21 RID: 11809
			public OmniEffect.OmniEffectElement[] omniEffectElements;
		}
	}
}
