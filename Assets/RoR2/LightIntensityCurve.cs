using System;
using UnityEngine;

// Token: 0x0200004A RID: 74
public class LightIntensityCurve : MonoBehaviour
{
	// Token: 0x06000148 RID: 328 RVA: 0x000073BC File Offset: 0x000055BC
	private void Start()
	{
		this.light = base.GetComponent<Light>();
		this.maxIntensity = this.light.intensity;
		this.light.intensity = 0f;
		if (this.randomStart)
		{
			this.time = UnityEngine.Random.Range(0f, this.timeMax);
		}
		if (this.enableNegativeLights)
		{
			this.light.color = new Color(-this.light.color.r, -this.light.color.g, -this.light.color.b);
		}
	}

	// Token: 0x06000149 RID: 329 RVA: 0x00007460 File Offset: 0x00005660
	private void Update()
	{
		this.time += Time.deltaTime;
		this.light.intensity = this.curve.Evaluate(this.time / this.timeMax) * this.maxIntensity;
		if (this.time >= this.timeMax && this.loop)
		{
			this.time = 0f;
		}
	}

	// Token: 0x04000155 RID: 341
	public AnimationCurve curve;

	// Token: 0x04000156 RID: 342
	public float timeMax = 5f;

	// Token: 0x04000157 RID: 343
	private float time;

	// Token: 0x04000158 RID: 344
	private Light light;

	// Token: 0x04000159 RID: 345
	private float maxIntensity;

	// Token: 0x0400015A RID: 346
	[Tooltip("Loops the animation curve.")]
	public bool loop;

	// Token: 0x0400015B RID: 347
	[Tooltip("Starts in a random point in the animation curve.")]
	public bool randomStart;

	// Token: 0x0400015C RID: 348
	[Tooltip("Causes the lights to be negative, casting shadows instead.")]
	public bool enableNegativeLights;
}
