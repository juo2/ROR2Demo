using System;

namespace UnityEngine.Rendering.PostProcessing
{
	// Token: 0x020004AB RID: 1195
	[PostProcess(typeof(HopooSSRRenderer), PostProcessEvent.BeforeTransparent, "PostProcess/Hopoo SSR", true)]
	[Serializable]
	public sealed class HopooSSR : PostProcessEffectSettings
	{
		// Token: 0x06001589 RID: 5513 RVA: 0x0005FF0C File Offset: 0x0005E10C
		public override bool IsEnabledAndSupported(PostProcessRenderContext context)
		{
			return this.enabled && context.camera.actualRenderingPath == RenderingPath.DeferredShading && SystemInfo.supportsMotionVectors && SystemInfo.supportsComputeShaders && SystemInfo.copyTextureSupport > CopyTextureSupport.None && context.resources.shaders.screenSpaceReflections && context.resources.shaders.screenSpaceReflections.isSupported && context.resources.computeShaders.gaussianDownsample;
		}

		// Token: 0x04001B6C RID: 7020
		[Tooltip("Choose a quality preset, or use \"Custom\" to fine tune it. Don't use a preset higher than \"Medium\" if you care about performances on consoles.")]
		public ScreenSpaceReflectionPresetParameter preset = new ScreenSpaceReflectionPresetParameter
		{
			value = ScreenSpaceReflectionPreset.Medium
		};

		// Token: 0x04001B6D RID: 7021
		[Range(0f, 256f)]
		[Tooltip("Maximum iteration count.")]
		public IntParameter maximumIterationCount = new IntParameter
		{
			value = 16
		};

		// Token: 0x04001B6E RID: 7022
		[Tooltip("Changes the size of the SSR buffer. Downsample it to maximize performances or supersample it to get slow but higher quality results.")]
		public ScreenSpaceReflectionResolutionParameter resolution = new ScreenSpaceReflectionResolutionParameter
		{
			value = ScreenSpaceReflectionResolution.Downsampled
		};

		// Token: 0x04001B6F RID: 7023
		[Range(1f, 64f)]
		[Tooltip("Ray thickness. Lower values are more expensive but allow the effect to detect smaller details.")]
		public FloatParameter thickness = new FloatParameter
		{
			value = 8f
		};

		// Token: 0x04001B70 RID: 7024
		[Tooltip("Maximum distance to traverse after which it will stop drawing reflections.")]
		public FloatParameter maximumMarchDistance = new FloatParameter
		{
			value = 100f
		};

		// Token: 0x04001B71 RID: 7025
		[Range(0f, 1f)]
		[Tooltip("Fades reflections close to the near planes.")]
		public FloatParameter distanceFade = new FloatParameter
		{
			value = 0.5f
		};

		// Token: 0x04001B72 RID: 7026
		[Tooltip("Fades reflections close to the screen edges.")]
		[Range(0f, 1f)]
		public FloatParameter vignette = new FloatParameter
		{
			value = 0.5f
		};
	}
}
