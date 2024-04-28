using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

// Token: 0x02000029 RID: 41
[PostProcess(typeof(SobelRainRenderer), PostProcessEvent.BeforeTransparent, "PostProcess/SobelRain", true)]
[Serializable]
public sealed class SobelRain : PostProcessEffectSettings
{
	// Token: 0x040000B6 RID: 182
	[Range(0f, 100f)]
	[Tooltip("The intensity of the rain.")]
	public FloatParameter rainIntensity = new FloatParameter
	{
		value = 0.5f
	};

	// Token: 0x040000B7 RID: 183
	[Tooltip("The falloff of the outline. Higher values means it relies less on the sobel.")]
	[Range(0f, 10f)]
	public FloatParameter outlineScale = new FloatParameter
	{
		value = 1f
	};

	// Token: 0x040000B8 RID: 184
	[Range(0f, 1f)]
	[Tooltip("The density of rain.")]
	public FloatParameter rainDensity = new FloatParameter
	{
		value = 1f
	};

	// Token: 0x040000B9 RID: 185
	public TextureParameter rainTexture = new TextureParameter
	{
		value = null
	};

	// Token: 0x040000BA RID: 186
	public ColorParameter rainColor = new ColorParameter
	{
		value = Color.white
	};
}
