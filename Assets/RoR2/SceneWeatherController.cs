using System;
using UnityEngine;

namespace RoR2
{
	// Token: 0x0200087F RID: 2175
	[ExecuteInEditMode]
	public class SceneWeatherController : MonoBehaviour
	{
		// Token: 0x1700044F RID: 1103
		// (get) Token: 0x06002FA6 RID: 12198 RVA: 0x000CB000 File Offset: 0x000C9200
		public static SceneWeatherController instance
		{
			get
			{
				return SceneWeatherController._instance;
			}
		}

		// Token: 0x06002FA7 RID: 12199 RVA: 0x000CB007 File Offset: 0x000C9207
		private void OnEnable()
		{
			if (!SceneWeatherController._instance)
			{
				SceneWeatherController._instance = this;
			}
		}

		// Token: 0x06002FA8 RID: 12200 RVA: 0x000CB01B File Offset: 0x000C921B
		private void OnDisable()
		{
			if (SceneWeatherController._instance == this)
			{
				SceneWeatherController._instance = null;
			}
		}

		// Token: 0x06002FA9 RID: 12201 RVA: 0x000CB030 File Offset: 0x000C9230
		private SceneWeatherController.WeatherParams GetWeatherParams(float t)
		{
			return new SceneWeatherController.WeatherParams
			{
				sunColor = Color.Lerp(this.initialWeatherParams.sunColor, this.finalWeatherParams.sunColor, t),
				sunIntensity = Mathf.Lerp(this.initialWeatherParams.sunIntensity, this.finalWeatherParams.sunIntensity, t),
				fogStart = Mathf.Lerp(this.initialWeatherParams.fogStart, this.finalWeatherParams.fogStart, t),
				fogScale = Mathf.Lerp(this.initialWeatherParams.fogScale, this.finalWeatherParams.fogScale, t),
				fogIntensity = Mathf.Lerp(this.initialWeatherParams.fogIntensity, this.finalWeatherParams.fogIntensity, t)
			};
		}

		// Token: 0x06002FAA RID: 12202 RVA: 0x000CB0F8 File Offset: 0x000C92F8
		private void Update()
		{
			SceneWeatherController.WeatherParams weatherParams = this.GetWeatherParams(this.weatherLerp);
			if (this.sun)
			{
				this.sun.color = weatherParams.sunColor;
				this.sun.intensity = weatherParams.sunIntensity;
			}
			if (this.fogMaterial)
			{
				this.fogMaterial.SetFloat("_FogPicker", this.weatherLerp);
				this.fogMaterial.SetFloat("_FogStart", weatherParams.fogStart);
				this.fogMaterial.SetFloat("_FogScale", weatherParams.fogScale);
				this.fogMaterial.SetFloat("_FogIntensity", weatherParams.fogIntensity);
			}
			if (true && this.rtpcWeather.Length != 0)
			{
				AkSoundEngine.SetRTPCValue(this.rtpcWeather, Mathf.Lerp(this.rtpcMin, this.rtpcMax, this.weatherLerp), base.gameObject);
			}
		}

		// Token: 0x0400316A RID: 12650
		private static SceneWeatherController _instance;

		// Token: 0x0400316B RID: 12651
		public SceneWeatherController.WeatherParams initialWeatherParams;

		// Token: 0x0400316C RID: 12652
		public SceneWeatherController.WeatherParams finalWeatherParams;

		// Token: 0x0400316D RID: 12653
		public Light sun;

		// Token: 0x0400316E RID: 12654
		public Material fogMaterial;

		// Token: 0x0400316F RID: 12655
		public string rtpcWeather;

		// Token: 0x04003170 RID: 12656
		public float rtpcMin;

		// Token: 0x04003171 RID: 12657
		public float rtpcMax = 100f;

		// Token: 0x04003172 RID: 12658
		public AnimationCurve weatherLerpOverChargeTime;

		// Token: 0x04003173 RID: 12659
		[Range(0f, 1f)]
		public float weatherLerp;

		// Token: 0x02000880 RID: 2176
		[Serializable]
		public struct WeatherParams
		{
			// Token: 0x04003174 RID: 12660
			[ColorUsage(true, true)]
			public Color sunColor;

			// Token: 0x04003175 RID: 12661
			public float sunIntensity;

			// Token: 0x04003176 RID: 12662
			public float fogStart;

			// Token: 0x04003177 RID: 12663
			public float fogScale;

			// Token: 0x04003178 RID: 12664
			public float fogIntensity;
		}
	}
}
