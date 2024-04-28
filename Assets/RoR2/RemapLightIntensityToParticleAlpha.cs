using System;
using UnityEngine;

namespace RoR2
{
	// Token: 0x02000845 RID: 2117
	public class RemapLightIntensityToParticleAlpha : MonoBehaviour
	{
		// Token: 0x06002E23 RID: 11811 RVA: 0x000C4AB8 File Offset: 0x000C2CB8
		private void LateUpdate()
		{
			ParticleSystem.MainModule main = this.particleSystem.main;
			ParticleSystem.MinMaxGradient startColor = main.startColor;
			Color color = startColor.color;
			color.a = Util.Remap(this.light.intensity, this.lowerLightIntensity, this.upperLightIntensity, this.lowerParticleAlpha, this.upperParticleAlpha);
			startColor.color = color;
			main.startColor = startColor;
		}

		// Token: 0x04003024 RID: 12324
		public Light light;

		// Token: 0x04003025 RID: 12325
		public ParticleSystem particleSystem;

		// Token: 0x04003026 RID: 12326
		public float lowerLightIntensity;

		// Token: 0x04003027 RID: 12327
		public float upperLightIntensity = 1f;

		// Token: 0x04003028 RID: 12328
		public float lowerParticleAlpha;

		// Token: 0x04003029 RID: 12329
		public float upperParticleAlpha = 1f;
	}
}
