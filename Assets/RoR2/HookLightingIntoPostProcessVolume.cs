using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace RoR2
{
	// Token: 0x02000736 RID: 1846
	[RequireComponent(typeof(PostProcessVolume))]
	public class HookLightingIntoPostProcessVolume : MonoBehaviour
	{
		// Token: 0x0600266D RID: 9837 RVA: 0x000A7398 File Offset: 0x000A5598
		private void OnEnable()
		{
			this.volumeColliders = base.GetComponents<Collider>();
			if (!this.hasCachedInitialValues)
			{
				this.defaultAmbientColor = RenderSettings.ambientLight;
				if (this.directionalLight)
				{
					this.defaultDirectionalColor = this.directionalLight.color;
				}
				if (this.particleSystem)
				{
					this.defaultParticleSystemMultiplier = this.particleSystem.emission.rateOverTimeMultiplier;
				}
				this.hasCachedInitialValues = true;
			}
			SceneCamera.onSceneCameraPreRender += this.OnPreRenderSceneCam;
		}

		// Token: 0x0600266E RID: 9838 RVA: 0x000A7420 File Offset: 0x000A5620
		private void OnDisable()
		{
			SceneCamera.onSceneCameraPreRender -= this.OnPreRenderSceneCam;
		}

		// Token: 0x0600266F RID: 9839 RVA: 0x000A7434 File Offset: 0x000A5634
		private void OnPreRenderSceneCam(SceneCamera sceneCam)
		{
			float interpFactor = this.GetInterpFactor(sceneCam.camera.transform.position);
			RenderSettings.ambientLight = Color.Lerp(this.defaultAmbientColor, this.overrideAmbientColor, interpFactor);
			if (this.directionalLight)
			{
				this.directionalLight.color = Color.Lerp(this.defaultDirectionalColor, this.overrideDirectionalColor, interpFactor);
			}
			if (this.particleSystem)
			{
				this.particleSystem.emission.rateOverTimeMultiplier = Mathf.Lerp(this.defaultParticleSystemMultiplier, this.overrideParticleSystemMultiplier, interpFactor);
			}
		}

		// Token: 0x06002670 RID: 9840 RVA: 0x000A74CC File Offset: 0x000A56CC
		private float GetInterpFactor(Vector3 triggerPos)
		{
			if (!this.volume.enabled || this.volume.weight <= 0f)
			{
				return 0f;
			}
			if (this.volume.isGlobal)
			{
				return 1f;
			}
			float num = 0f;
			foreach (Collider collider in this.volumeColliders)
			{
				float num2 = float.PositiveInfinity;
				if (collider.enabled)
				{
					float sqrMagnitude = ((collider.ClosestPoint(triggerPos) - triggerPos) / 2f).sqrMagnitude;
					if (sqrMagnitude < num2)
					{
						num2 = sqrMagnitude;
					}
					float num3 = this.volume.blendDistance * this.volume.blendDistance;
					if (num2 <= num3 && num3 > 0f)
					{
						num = Mathf.Max(num, 1f - num2 / num3);
					}
				}
			}
			return num;
		}

		// Token: 0x04002A33 RID: 10803
		[Header("Required Values")]
		public PostProcessVolume volume;

		// Token: 0x04002A34 RID: 10804
		[ColorUsage(true, true)]
		public Color overrideAmbientColor;

		// Token: 0x04002A35 RID: 10805
		[Header("Optional Values")]
		public Light directionalLight;

		// Token: 0x04002A36 RID: 10806
		public Color overrideDirectionalColor;

		// Token: 0x04002A37 RID: 10807
		public ParticleSystem particleSystem;

		// Token: 0x04002A38 RID: 10808
		public float overrideParticleSystemMultiplier;

		// Token: 0x04002A39 RID: 10809
		private Collider[] volumeColliders;

		// Token: 0x04002A3A RID: 10810
		private Color defaultAmbientColor;

		// Token: 0x04002A3B RID: 10811
		private Color defaultDirectionalColor;

		// Token: 0x04002A3C RID: 10812
		private float defaultParticleSystemMultiplier;

		// Token: 0x04002A3D RID: 10813
		private bool hasCachedInitialValues;
	}
}
