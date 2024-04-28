using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

// Token: 0x02000027 RID: 39
[PostProcess(typeof(SobelOutlineRenderer), PostProcessEvent.BeforeTransparent, "PostProcess/SobelOutline", true)]
[Serializable]
public sealed class SobelOutline : PostProcessEffectSettings
{
	// Token: 0x040000B4 RID: 180
	[Range(0f, 5f)]
	[Tooltip("The intensity of the outline.")]
	public FloatParameter outlineIntensity = new FloatParameter
	{
		value = 0.5f
	};

	// Token: 0x040000B5 RID: 181
	[Tooltip("The falloff of the outline.")]
	[Range(0f, 10f)]
	public FloatParameter outlineScale = new FloatParameter
	{
		value = 1f
	};
}
