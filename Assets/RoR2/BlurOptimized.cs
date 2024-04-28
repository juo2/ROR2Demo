using System;
using UnityEngine;

namespace RoR2
{
	// Token: 0x0200056C RID: 1388
	[RequireComponent(typeof(Camera))]
	public class BlurOptimized : MonoBehaviour
	{
		// Token: 0x0600191A RID: 6426 RVA: 0x0006C89E File Offset: 0x0006AA9E
		public void Start()
		{
			this.blurMaterial = new Material(LegacyShaderAPI.Find("Hidden/FastBlur"));
			base.enabled = false;
		}

		// Token: 0x0600191B RID: 6427 RVA: 0x0006C8BC File Offset: 0x0006AABC
		public void OnRenderImage(RenderTexture source, RenderTexture destination)
		{
			float num = 1f / (1f * (float)(1 << this.downsample));
			this.blurMaterial.SetVector("_Parameter", new Vector4(this.blurSize * num, -this.blurSize * num, 0f, 0f));
			source.filterMode = FilterMode.Bilinear;
			int width = source.width >> this.downsample;
			int height = source.height >> this.downsample;
			RenderTexture renderTexture = RenderTexture.GetTemporary(width, height, 0, source.format);
			renderTexture.filterMode = FilterMode.Bilinear;
			Graphics.Blit(source, renderTexture, this.blurMaterial, 0);
			int num2 = (this.blurType == BlurOptimized.BlurType.StandardGauss) ? 0 : 2;
			for (int i = 0; i < this.blurIterations; i++)
			{
				float num3 = (float)i * 1f;
				this.blurMaterial.SetVector("_Parameter", new Vector4(this.blurSize * num + num3, -this.blurSize * num - num3, 0f, 0f));
				RenderTexture temporary = RenderTexture.GetTemporary(width, height, 0, source.format);
				temporary.filterMode = FilterMode.Bilinear;
				Graphics.Blit(renderTexture, temporary, this.blurMaterial, 1 + num2);
				RenderTexture.ReleaseTemporary(renderTexture);
				renderTexture = temporary;
				temporary = RenderTexture.GetTemporary(width, height, 0, source.format);
				temporary.filterMode = FilterMode.Bilinear;
				Graphics.Blit(renderTexture, temporary, this.blurMaterial, 2 + num2);
				RenderTexture.ReleaseTemporary(renderTexture);
				renderTexture = temporary;
			}
			Graphics.Blit(renderTexture, destination);
			RenderTexture.ReleaseTemporary(renderTexture);
		}

		// Token: 0x04001EEB RID: 7915
		[Range(0f, 2f)]
		public int downsample = 1;

		// Token: 0x04001EEC RID: 7916
		[Range(0f, 10f)]
		public float blurSize = 3f;

		// Token: 0x04001EED RID: 7917
		[Range(1f, 4f)]
		public int blurIterations = 2;

		// Token: 0x04001EEE RID: 7918
		public BlurOptimized.BlurType blurType;

		// Token: 0x04001EEF RID: 7919
		[HideInInspector]
		public Material blurMaterial;

		// Token: 0x0200056D RID: 1389
		public enum BlurType
		{
			// Token: 0x04001EF1 RID: 7921
			StandardGauss,
			// Token: 0x04001EF2 RID: 7922
			SgxGauss
		}
	}
}
