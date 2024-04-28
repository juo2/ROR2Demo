using System;
using RoR2.ConVar;
using UnityEngine;

namespace RoR2
{
	// Token: 0x02000607 RID: 1543
	[DisallowMultipleComponent]
	[RequireComponent(typeof(Camera))]
	public class CameraResolutionScaler : MonoBehaviour
	{
		// Token: 0x170001E5 RID: 485
		// (get) Token: 0x06001C46 RID: 7238 RVA: 0x0007861D File Offset: 0x0007681D
		// (set) Token: 0x06001C47 RID: 7239 RVA: 0x00078625 File Offset: 0x00076825
		public Camera camera { get; private set; }

		// Token: 0x06001C48 RID: 7240 RVA: 0x0007862E File Offset: 0x0007682E
		private void Awake()
		{
			this.camera = base.GetComponent<Camera>();
		}

		// Token: 0x06001C49 RID: 7241 RVA: 0x0007863C File Offset: 0x0007683C
		private void OnPreRender()
		{
			this.ApplyScalingRenderTexture();
		}

		// Token: 0x06001C4A RID: 7242 RVA: 0x00078644 File Offset: 0x00076844
		private void OnRenderImage(RenderTexture source, RenderTexture destination)
		{
			if (!this.scalingRenderTexture)
			{
				Graphics.Blit(source, destination);
				return;
			}
			this.camera.targetTexture = this.oldRenderTexture;
			Graphics.Blit(source, this.oldRenderTexture);
			this.oldRenderTexture = null;
		}

		// Token: 0x06001C4B RID: 7243 RVA: 0x0007867F File Offset: 0x0007687F
		private static void SetResolutionScale(float newResolutionScale)
		{
			if (CameraResolutionScaler.resolutionScale == newResolutionScale)
			{
				return;
			}
			CameraResolutionScaler.resolutionScale = newResolutionScale;
		}

		// Token: 0x06001C4C RID: 7244 RVA: 0x00078690 File Offset: 0x00076890
		private void ApplyScalingRenderTexture()
		{
			this.oldRenderTexture = this.camera.targetTexture;
			bool flag = CameraResolutionScaler.resolutionScale != 1f;
			this.camera.targetTexture = null;
			Rect pixelRect = this.camera.pixelRect;
			int num = Mathf.FloorToInt(pixelRect.width * CameraResolutionScaler.resolutionScale);
			int num2 = Mathf.FloorToInt(pixelRect.height * CameraResolutionScaler.resolutionScale);
			if (this.scalingRenderTexture && (this.scalingRenderTexture.width != num || this.scalingRenderTexture.height != num2))
			{
				UnityEngine.Object.Destroy(this.scalingRenderTexture);
				this.scalingRenderTexture = null;
			}
			if (flag != this.scalingRenderTexture)
			{
				if (flag)
				{
					this.scalingRenderTexture = new RenderTexture(num, num2, 24);
					this.scalingRenderTexture.autoGenerateMips = false;
					this.scalingRenderTexture.filterMode = (((double)CameraResolutionScaler.resolutionScale > 1.0) ? FilterMode.Bilinear : FilterMode.Point);
				}
				else
				{
					UnityEngine.Object.Destroy(this.scalingRenderTexture);
					this.scalingRenderTexture = null;
				}
			}
			if (flag)
			{
				this.camera.targetTexture = this.scalingRenderTexture;
			}
		}

		// Token: 0x06001C4D RID: 7245 RVA: 0x000787AB File Offset: 0x000769AB
		private void OnDestroy()
		{
			if (this.scalingRenderTexture)
			{
				UnityEngine.Object.Destroy(this.scalingRenderTexture);
				this.scalingRenderTexture = null;
			}
		}

		// Token: 0x04002217 RID: 8727
		private RenderTexture oldRenderTexture;

		// Token: 0x04002218 RID: 8728
		private static float resolutionScale = 1f;

		// Token: 0x04002219 RID: 8729
		private RenderTexture scalingRenderTexture;

		// Token: 0x02000608 RID: 1544
		private class ResolutionScaleConVar : BaseConVar
		{
			// Token: 0x06001C50 RID: 7248 RVA: 0x00009F73 File Offset: 0x00008173
			private ResolutionScaleConVar(string name, ConVarFlags flags, string defaultValue, string helpText) : base(name, flags, defaultValue, helpText)
			{
			}

			// Token: 0x06001C51 RID: 7249 RVA: 0x000787D8 File Offset: 0x000769D8
			public override void SetString(string newValue)
			{
				float num;
				TextSerialization.TryParseInvariant(newValue, out num);
			}

			// Token: 0x06001C52 RID: 7250 RVA: 0x000787EE File Offset: 0x000769EE
			public override string GetString()
			{
				return TextSerialization.ToStringInvariant(CameraResolutionScaler.resolutionScale);
			}

			// Token: 0x0400221A RID: 8730
			private static CameraResolutionScaler.ResolutionScaleConVar instance = new CameraResolutionScaler.ResolutionScaleConVar("resolution_scale", ConVarFlags.Archive, null, "Resolution scale. Currently nonfunctional.");
		}
	}
}
