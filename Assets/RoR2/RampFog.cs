using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

// Token: 0x02000025 RID: 37
[PostProcess(typeof(RampFogRenderer), PostProcessEvent.BeforeTransparent, "PostProcess/RampFog", true)]
[Serializable]
public sealed class RampFog : PostProcessEffectSettings
{
	// Token: 0x040000A9 RID: 169
	[Range(0f, 1f)]
	[Tooltip("Fog intensity.")]
	public FloatParameter fogIntensity = new FloatParameter
	{
		value = 0.5f
	};

	// Token: 0x040000AA RID: 170
	[Range(0f, 20f)]
	[Tooltip("Fog Power")]
	public FloatParameter fogPower = new FloatParameter
	{
		value = 1f
	};

	// Token: 0x040000AB RID: 171
	[Range(-1f, 1f)]
	[Tooltip("The zero value for the fog depth remap.")]
	public FloatParameter fogZero = new FloatParameter
	{
		value = 0f
	};

	// Token: 0x040000AC RID: 172
	[Range(-1f, 1f)]
	[Tooltip("The one value for the fog depth remap.")]
	public FloatParameter fogOne = new FloatParameter
	{
		value = 1f
	};

	// Token: 0x040000AD RID: 173
	[Range(-100f, 100f)]
	[Tooltip("The world position value where the height fog begins.")]
	public FloatParameter fogHeightStart = new FloatParameter
	{
		value = 0f
	};

	// Token: 0x040000AE RID: 174
	[Tooltip("The world position value where the height fog ends.")]
	[Range(-100f, 600f)]
	public FloatParameter fogHeightEnd = new FloatParameter
	{
		value = 100f
	};

	// Token: 0x040000AF RID: 175
	[Tooltip("The overall strength of the height fog.")]
	[Range(0f, 5f)]
	public FloatParameter fogHeightIntensity = new FloatParameter
	{
		value = 0f
	};

	// Token: 0x040000B0 RID: 176
	[Tooltip("Color of the fog at the beginning.")]
	public ColorParameter fogColorStart = new ColorParameter
	{
		value = Color.white
	};

	// Token: 0x040000B1 RID: 177
	[Tooltip("Color of the fog at the middle.")]
	public ColorParameter fogColorMid = new ColorParameter
	{
		value = Color.white
	};

	// Token: 0x040000B2 RID: 178
	[Tooltip("Color of the fog at the end.")]
	public ColorParameter fogColorEnd = new ColorParameter
	{
		value = Color.white
	};

	// Token: 0x040000B3 RID: 179
	[Range(0f, 1f)]
	[Tooltip("How much of the skybox will leak through?")]
	public FloatParameter skyboxStrength = new FloatParameter
	{
		value = 0f
	};
}
