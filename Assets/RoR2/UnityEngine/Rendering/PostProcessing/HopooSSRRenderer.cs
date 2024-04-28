﻿using System;
using RoR2;

namespace UnityEngine.Rendering.PostProcessing
{
	// Token: 0x020004AC RID: 1196
	public sealed class HopooSSRRenderer : PostProcessEffectRenderer<HopooSSR>
	{
		// Token: 0x0600158B RID: 5515 RVA: 0x0000EBE5 File Offset: 0x0000CDE5
		public override DepthTextureMode GetCameraFlags()
		{
			return DepthTextureMode.Depth | DepthTextureMode.MotionVectors;
		}

		// Token: 0x0600158C RID: 5516 RVA: 0x00060034 File Offset: 0x0005E234
		internal void CheckRT(ref RenderTexture rt, int width, int height, RenderTextureFormat format, FilterMode filterMode, bool useMipMap)
		{
			if (rt == null || !rt.IsCreated() || rt.width != width || rt.height != height || rt.format != format)
			{
				if (rt != null)
				{
					rt.Release();
				}
				rt = new RenderTexture(width, height, 0, format)
				{
					filterMode = filterMode,
					useMipMap = useMipMap,
					autoGenerateMips = false,
					hideFlags = HideFlags.HideAndDontSave
				};
				rt.Create();
			}
		}

		// Token: 0x0600158D RID: 5517 RVA: 0x000600B8 File Offset: 0x0005E2B8
		public override void Render(PostProcessRenderContext context)
		{
			CommandBuffer command = context.command;
			command.BeginSample("Screen-space Reflections");
			if (base.settings.preset.value != ScreenSpaceReflectionPreset.Custom)
			{
				int value = (int)base.settings.preset.value;
				base.settings.maximumIterationCount.value = this.m_Presets[value].maximumIterationCount;
				base.settings.thickness.value = this.m_Presets[value].thickness;
				base.settings.resolution.value = this.m_Presets[value].downsampling;
			}
			base.settings.maximumMarchDistance.value = Mathf.Max(0f, base.settings.maximumMarchDistance.value);
			int num = Mathf.ClosestPowerOfTwo(Mathf.Min(context.width, context.height));
			if (base.settings.resolution.value == ScreenSpaceReflectionResolution.Downsampled)
			{
				num >>= 1;
			}
			else if (base.settings.resolution.value == ScreenSpaceReflectionResolution.Supersampled)
			{
				num <<= 1;
			}
			int num2 = Mathf.FloorToInt(Mathf.Log((float)num, 2f) - 3f);
			num2 = Mathf.Min(num2, 12);
			this.CheckRT(ref this.m_Resolve, num, num, context.sourceFormat, FilterMode.Trilinear, true);
			Texture2D texture2D = context.resources.blueNoise256[0];
			PropertySheet propertySheet = context.propertySheets.Get(LegacyShaderAPI.Find("Hidden/PostProcessing/HopooSSR"));
			propertySheet.properties.SetTexture(Shader.PropertyToID("_Noise"), texture2D);
			Matrix4x4 matrix4x = default(Matrix4x4);
			matrix4x.SetRow(0, new Vector4((float)num * 0.5f, 0f, 0f, (float)num * 0.5f));
			matrix4x.SetRow(1, new Vector4(0f, (float)num * 0.5f, 0f, (float)num * 0.5f));
			matrix4x.SetRow(2, new Vector4(0f, 0f, 1f, 0f));
			matrix4x.SetRow(3, new Vector4(0f, 0f, 0f, 1f));
			Matrix4x4 gpuprojectionMatrix = GL.GetGPUProjectionMatrix(context.camera.projectionMatrix, false);
			matrix4x *= gpuprojectionMatrix;
			propertySheet.properties.SetMatrix(Shader.PropertyToID("_ViewMatrix"), context.camera.worldToCameraMatrix);
			propertySheet.properties.SetMatrix(Shader.PropertyToID("_InverseViewMatrix"), context.camera.worldToCameraMatrix.inverse);
			propertySheet.properties.SetMatrix(Shader.PropertyToID("_InverseProjectionMatrix"), gpuprojectionMatrix.inverse);
			propertySheet.properties.SetMatrix(Shader.PropertyToID("_ScreenSpaceProjectionMatrix"), matrix4x);
			propertySheet.properties.SetVector(Shader.PropertyToID("_Params"), new Vector4(base.settings.vignette.value, base.settings.distanceFade.value, base.settings.maximumMarchDistance.value, (float)num2));
			propertySheet.properties.SetVector(Shader.PropertyToID("_Params2"), new Vector4((float)context.width / (float)context.height, (float)num / (float)texture2D.width, base.settings.thickness.value, (float)base.settings.maximumIterationCount.value));
			command.GetTemporaryRT(Shader.PropertyToID("_Test"), num, num, 0, FilterMode.Point, context.sourceFormat);
			command.BlitFullscreenTriangle(context.source, Shader.PropertyToID("_Test"), propertySheet, 0, false, null);
			if (context.isSceneView)
			{
				command.BlitFullscreenTriangle(context.source, this.m_Resolve, propertySheet, 1, false, null);
			}
			else
			{
				this.CheckRT(ref this.m_History, num, num, context.sourceFormat, FilterMode.Bilinear, false);
				if (this.m_ResetHistory)
				{
					context.command.BlitFullscreenTriangle(context.source, this.m_History, false, null);
					this.m_ResetHistory = false;
				}
				command.GetTemporaryRT(Shader.PropertyToID("_SSRResolveTemp"), num, num, 0, FilterMode.Bilinear, context.sourceFormat);
				command.BlitFullscreenTriangle(context.source, Shader.PropertyToID("_SSRResolveTemp"), propertySheet, 1, false, null);
				propertySheet.properties.SetTexture(Shader.PropertyToID("_History"), this.m_History);
				command.BlitFullscreenTriangle(Shader.PropertyToID("_SSRResolveTemp"), this.m_Resolve, propertySheet, 2, false, null);
				command.CopyTexture(this.m_Resolve, 0, 0, this.m_History, 0, 0);
				command.ReleaseTemporaryRT(Shader.PropertyToID("_SSRResolveTemp"));
			}
			command.ReleaseTemporaryRT(Shader.PropertyToID("_Test"));
			if (this.m_MipIDs == null || this.m_MipIDs.Length == 0)
			{
				this.m_MipIDs = new int[12];
				for (int i = 0; i < 12; i++)
				{
					this.m_MipIDs[i] = Shader.PropertyToID("_SSRGaussianMip" + i);
				}
			}
			ComputeShader gaussianDownsample = context.resources.computeShaders.gaussianDownsample;
			int kernelIndex = gaussianDownsample.FindKernel("KMain");
			RenderTargetIdentifier rt = new RenderTargetIdentifier(this.m_Resolve);
			for (int j = 0; j < num2; j++)
			{
				num >>= 1;
				command.GetTemporaryRT(this.m_MipIDs[j], num, num, 0, FilterMode.Bilinear, context.sourceFormat, RenderTextureReadWrite.Default, 1, true);
				command.SetComputeTextureParam(gaussianDownsample, kernelIndex, "_Source", rt);
				command.SetComputeTextureParam(gaussianDownsample, kernelIndex, "_Result", this.m_MipIDs[j]);
				command.SetComputeVectorParam(gaussianDownsample, "_Size", new Vector4((float)num, (float)num, 1f / (float)num, 1f / (float)num));
				command.DispatchCompute(gaussianDownsample, kernelIndex, num / 8, num / 8, 1);
				command.CopyTexture(this.m_MipIDs[j], 0, 0, this.m_Resolve, 0, j + 1);
				rt = this.m_MipIDs[j];
			}
			for (int k = 0; k < num2; k++)
			{
				command.ReleaseTemporaryRT(this.m_MipIDs[k]);
			}
			propertySheet.properties.SetTexture(Shader.PropertyToID("_Resolve"), this.m_Resolve);
			command.BlitFullscreenTriangle(context.source, context.destination, propertySheet, 3, false, null);
			command.EndSample("Screen-space Reflections");
		}

		// Token: 0x0600158E RID: 5518 RVA: 0x00060752 File Offset: 0x0005E952
		public override void Release()
		{
			RuntimeUtilities.Destroy(this.m_Resolve);
			RuntimeUtilities.Destroy(this.m_History);
			this.m_Resolve = null;
			this.m_History = null;
		}

		// Token: 0x04001B73 RID: 7027
		private RenderTexture m_Resolve;

		// Token: 0x04001B74 RID: 7028
		private RenderTexture m_History;

		// Token: 0x04001B75 RID: 7029
		private int[] m_MipIDs;

		// Token: 0x04001B76 RID: 7030
		private readonly HopooSSRRenderer.QualityPreset[] m_Presets = new HopooSSRRenderer.QualityPreset[]
		{
			new HopooSSRRenderer.QualityPreset
			{
				maximumIterationCount = 10,
				thickness = 32f,
				downsampling = ScreenSpaceReflectionResolution.Downsampled
			},
			new HopooSSRRenderer.QualityPreset
			{
				maximumIterationCount = 16,
				thickness = 32f,
				downsampling = ScreenSpaceReflectionResolution.Downsampled
			},
			new HopooSSRRenderer.QualityPreset
			{
				maximumIterationCount = 32,
				thickness = 16f,
				downsampling = ScreenSpaceReflectionResolution.Downsampled
			},
			new HopooSSRRenderer.QualityPreset
			{
				maximumIterationCount = 48,
				thickness = 8f,
				downsampling = ScreenSpaceReflectionResolution.Downsampled
			},
			new HopooSSRRenderer.QualityPreset
			{
				maximumIterationCount = 16,
				thickness = 32f,
				downsampling = ScreenSpaceReflectionResolution.FullSize
			},
			new HopooSSRRenderer.QualityPreset
			{
				maximumIterationCount = 48,
				thickness = 16f,
				downsampling = ScreenSpaceReflectionResolution.FullSize
			},
			new HopooSSRRenderer.QualityPreset
			{
				maximumIterationCount = 128,
				thickness = 12f,
				downsampling = ScreenSpaceReflectionResolution.Supersampled
			}
		};

		// Token: 0x020004AD RID: 1197
		private class QualityPreset
		{
			// Token: 0x04001B77 RID: 7031
			public int maximumIterationCount;

			// Token: 0x04001B78 RID: 7032
			public float thickness;

			// Token: 0x04001B79 RID: 7033
			public ScreenSpaceReflectionResolution downsampling;
		}

		// Token: 0x020004AE RID: 1198
		private enum Pass
		{
			// Token: 0x04001B7B RID: 7035
			Test,
			// Token: 0x04001B7C RID: 7036
			Resolve,
			// Token: 0x04001B7D RID: 7037
			Reproject,
			// Token: 0x04001B7E RID: 7038
			Composite
		}
	}
}
