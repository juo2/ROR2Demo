using System;
using UnityEngine;

// Token: 0x02000078 RID: 120
public struct NetworkLerpedAngle
{
	// Token: 0x06000215 RID: 533 RVA: 0x000098B0 File Offset: 0x00007AB0
	public float GetCurrentValue(bool hasAuthority)
	{
		if (hasAuthority)
		{
			return this.authoritativeValue;
		}
		float t = Mathf.Clamp01((Time.time - this.interpStartTime) * 20f);
		return Mathf.LerpAngle(this.interpStartValue, this.authoritativeValue, t);
	}

	// Token: 0x06000216 RID: 534 RVA: 0x000098F1 File Offset: 0x00007AF1
	public void SetAuthoritativeValue(float newValue, bool hasAuthority)
	{
		this.interpStartValue = this.GetCurrentValue(hasAuthority);
		this.interpStartTime = Time.time;
		this.authoritativeValue = newValue;
	}

	// Token: 0x040001EA RID: 490
	private const float interpDuration = 0.05f;

	// Token: 0x040001EB RID: 491
	private const float invInterpDuration = 20f;

	// Token: 0x040001EC RID: 492
	public float authoritativeValue;

	// Token: 0x040001ED RID: 493
	private float interpStartValue;

	// Token: 0x040001EE RID: 494
	private float interpStartTime;
}
