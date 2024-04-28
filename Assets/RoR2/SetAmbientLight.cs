using System;
using UnityEngine;
using UnityEngine.Rendering;

namespace RoR2
{
	// Token: 0x02000885 RID: 2181
	[ExecuteAlways]
	public class SetAmbientLight : MonoBehaviour
	{
		// Token: 0x06002FD4 RID: 12244 RVA: 0x000026ED File Offset: 0x000008ED
		private void OnEnable()
		{
		}

		// Token: 0x06002FD5 RID: 12245 RVA: 0x000CBA14 File Offset: 0x000C9C14
		private void ApplyLighting()
		{
			if (this.setAmbientLightColor)
			{
				RenderSettings.ambientMode = this.ambientMode;
				RenderSettings.ambientSkyColor = this.ambientSkyColor * this.ambientIntensity;
				RenderSettings.ambientEquatorColor = this.ambientEquatorColor * this.ambientIntensity;
				RenderSettings.ambientGroundColor = this.ambientGroundColor * this.ambientIntensity;
			}
			if (this.setSkyboxMaterial)
			{
				RenderSettings.skybox = this.skyboxMaterial;
			}
		}

		// Token: 0x04003192 RID: 12690
		public bool setSkyboxMaterial;

		// Token: 0x04003193 RID: 12691
		public bool setAmbientLightColor;

		// Token: 0x04003194 RID: 12692
		public Material skyboxMaterial;

		// Token: 0x04003195 RID: 12693
		public AmbientMode ambientMode;

		// Token: 0x04003196 RID: 12694
		public float ambientIntensity;

		// Token: 0x04003197 RID: 12695
		[ColorUsage(true, true)]
		public Color ambientSkyColor = Color.black;

		// Token: 0x04003198 RID: 12696
		[ColorUsage(true, true)]
		public Color ambientEquatorColor = Color.black;

		// Token: 0x04003199 RID: 12697
		[ColorUsage(true, true)]
		public Color ambientGroundColor = Color.black;
	}
}
