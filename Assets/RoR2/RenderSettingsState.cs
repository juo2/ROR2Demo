using System;
using UnityEngine;
using UnityEngine.Rendering;

// Token: 0x02000086 RID: 134
[Serializable]
public struct RenderSettingsState
{
	// Token: 0x06000234 RID: 564 RVA: 0x0000A008 File Offset: 0x00008208
	public static RenderSettingsState FromCurrent()
	{
		return new RenderSettingsState
		{
			haloStrength = RenderSettings.haloStrength,
			defaultReflectionResolution = RenderSettings.defaultReflectionResolution,
			defaultReflectionMode = RenderSettings.defaultReflectionMode,
			reflectionBounces = RenderSettings.reflectionBounces,
			reflectionIntensity = RenderSettings.reflectionIntensity,
			customReflection = RenderSettings.customReflection,
			ambientProbe = RenderSettings.ambientProbe,
			sun = RenderSettings.sun,
			skybox = RenderSettings.skybox,
			subtractiveShadowColor = RenderSettings.subtractiveShadowColor,
			flareStrength = RenderSettings.flareStrength,
			ambientLight = RenderSettings.ambientLight,
			ambientGroundColor = RenderSettings.ambientGroundColor,
			ambientEquatorColor = RenderSettings.ambientEquatorColor,
			ambientSkyColor = RenderSettings.ambientSkyColor,
			ambientMode = RenderSettings.ambientMode,
			fogDensity = RenderSettings.fogDensity,
			fogColor = RenderSettings.fogColor,
			fogMode = RenderSettings.fogMode,
			fogEndDistance = RenderSettings.fogEndDistance,
			fogStartDistance = RenderSettings.fogStartDistance,
			fog = RenderSettings.fog,
			ambientIntensity = RenderSettings.ambientIntensity,
			flareFadeSpeed = RenderSettings.flareFadeSpeed
		};
	}

	// Token: 0x06000235 RID: 565 RVA: 0x0000A140 File Offset: 0x00008340
	public void Apply()
	{
		RenderSettings.haloStrength = this.haloStrength;
		RenderSettings.defaultReflectionResolution = this.defaultReflectionResolution;
		RenderSettings.defaultReflectionMode = this.defaultReflectionMode;
		RenderSettings.reflectionBounces = this.reflectionBounces;
		RenderSettings.reflectionIntensity = this.reflectionIntensity;
		RenderSettings.customReflection = this.customReflection;
		RenderSettings.ambientProbe = this.ambientProbe;
		RenderSettings.sun = this.sun;
		RenderSettings.skybox = this.skybox;
		RenderSettings.subtractiveShadowColor = this.subtractiveShadowColor;
		RenderSettings.flareStrength = this.flareStrength;
		RenderSettings.ambientLight = this.ambientLight;
		RenderSettings.ambientGroundColor = this.ambientGroundColor;
		RenderSettings.ambientEquatorColor = this.ambientEquatorColor;
		RenderSettings.ambientSkyColor = this.ambientSkyColor;
		RenderSettings.ambientMode = this.ambientMode;
		RenderSettings.fogDensity = this.fogDensity;
		RenderSettings.fogColor = this.fogColor;
		RenderSettings.fogMode = this.fogMode;
		RenderSettings.fogEndDistance = this.fogEndDistance;
		RenderSettings.fogStartDistance = this.fogStartDistance;
		RenderSettings.fog = this.fog;
		RenderSettings.ambientIntensity = this.ambientIntensity;
		RenderSettings.flareFadeSpeed = this.flareFadeSpeed;
	}

	// Token: 0x0400021E RID: 542
	public float haloStrength;

	// Token: 0x0400021F RID: 543
	public int defaultReflectionResolution;

	// Token: 0x04000220 RID: 544
	public DefaultReflectionMode defaultReflectionMode;

	// Token: 0x04000221 RID: 545
	public int reflectionBounces;

	// Token: 0x04000222 RID: 546
	public float reflectionIntensity;

	// Token: 0x04000223 RID: 547
	public Cubemap customReflection;

	// Token: 0x04000224 RID: 548
	public SphericalHarmonicsL2 ambientProbe;

	// Token: 0x04000225 RID: 549
	public Light sun;

	// Token: 0x04000226 RID: 550
	public Material skybox;

	// Token: 0x04000227 RID: 551
	public Color subtractiveShadowColor;

	// Token: 0x04000228 RID: 552
	public float flareStrength;

	// Token: 0x04000229 RID: 553
	[ColorUsage(false, true)]
	public Color ambientLight;

	// Token: 0x0400022A RID: 554
	[ColorUsage(false, true)]
	public Color ambientGroundColor;

	// Token: 0x0400022B RID: 555
	[ColorUsage(false, true)]
	public Color ambientEquatorColor;

	// Token: 0x0400022C RID: 556
	[ColorUsage(false, true)]
	public Color ambientSkyColor;

	// Token: 0x0400022D RID: 557
	public AmbientMode ambientMode;

	// Token: 0x0400022E RID: 558
	public float fogDensity;

	// Token: 0x0400022F RID: 559
	[ColorUsage(true, false)]
	public Color fogColor;

	// Token: 0x04000230 RID: 560
	public FogMode fogMode;

	// Token: 0x04000231 RID: 561
	public float fogEndDistance;

	// Token: 0x04000232 RID: 562
	public float fogStartDistance;

	// Token: 0x04000233 RID: 563
	public bool fog;

	// Token: 0x04000234 RID: 564
	public float ambientIntensity;

	// Token: 0x04000235 RID: 565
	public float flareFadeSpeed;
}
