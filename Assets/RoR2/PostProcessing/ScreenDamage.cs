﻿using System;
using UnityEngine;

namespace RoR2.PostProcessing
{
	// Token: 0x02000BBF RID: 3007
	public class ScreenDamage : MonoBehaviour
	{
		// Token: 0x0600447F RID: 17535 RVA: 0x0011D259 File Offset: 0x0011B459
		private void Awake()
		{
			this.cameraRigController = base.GetComponentInParent<CameraRigController>();
			this.mat = UnityEngine.Object.Instantiate<Material>(this.mat);
		}

		// Token: 0x06004480 RID: 17536 RVA: 0x0011D278 File Offset: 0x0011B478
		private void OnRenderImage(RenderTexture source, RenderTexture destination)
		{
			float num = 0f;
			float num2 = 0f;
			if (this.cameraRigController)
			{
				if (this.cameraRigController.target)
				{
					num2 = 0.5f;
					HealthComponent component = this.cameraRigController.target.GetComponent<HealthComponent>();
					if (component)
					{
						this.healthPercentage = Mathf.Clamp(component.health / component.fullHealth, 0f, 1f);
						num = Mathf.Clamp01(1f - component.timeSinceLastHit / 0.6f) * 1.6f;
						if (component.health <= 0f)
						{
							num2 = 0f;
						}
					}
				}
				this.mat.SetFloat("_DistortionStrength", num2 * this.DistortionScale * Mathf.Pow(1f - this.healthPercentage, this.DistortionPower));
				this.mat.SetFloat("_DesaturationStrength", num2 * this.DesaturationScale * Mathf.Pow(1f - this.healthPercentage, this.DesaturationPower));
				this.mat.SetFloat("_TintStrength", num2 * this.TintScale * (Mathf.Pow(1f - this.healthPercentage, this.TintPower) + num));
			}
			Graphics.Blit(source, destination, this.mat);
		}

		// Token: 0x040042F2 RID: 17138
		private CameraRigController cameraRigController;

		// Token: 0x040042F3 RID: 17139
		public Material mat;

		// Token: 0x040042F4 RID: 17140
		public float DistortionScale = 1f;

		// Token: 0x040042F5 RID: 17141
		public float DistortionPower = 1f;

		// Token: 0x040042F6 RID: 17142
		public float DesaturationScale = 1f;

		// Token: 0x040042F7 RID: 17143
		public float DesaturationPower = 1f;

		// Token: 0x040042F8 RID: 17144
		public float TintScale = 1f;

		// Token: 0x040042F9 RID: 17145
		public float TintPower = 1f;

		// Token: 0x040042FA RID: 17146
		private float healthPercentage = 1f;

		// Token: 0x040042FB RID: 17147
		private const float hitTintDecayTime = 0.6f;

		// Token: 0x040042FC RID: 17148
		private const float hitTintScale = 1.6f;

		// Token: 0x040042FD RID: 17149
		private const float deathWeight = 2f;
	}
}
