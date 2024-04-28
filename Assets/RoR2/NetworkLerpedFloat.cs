using System;
using UnityEngine;

// Token: 0x0200007B RID: 123
public struct NetworkLerpedFloat
{
	// Token: 0x0600021B RID: 539 RVA: 0x00009A50 File Offset: 0x00007C50
	public void SetValueImmediate(float value)
	{
		this.newestInterpPoint.time = Time.time;
		this.newestInterpPoint.value = value;
		this.highInterpPoint = this.newestInterpPoint;
		this.lowInterpPoint = this.newestInterpPoint;
		this.inverseLowHighTimespan = 0f;
	}

	// Token: 0x0600021C RID: 540 RVA: 0x00009A9C File Offset: 0x00007C9C
	public float GetCurrentValue(bool hasAuthority)
	{
		if (hasAuthority)
		{
			return this.newestInterpPoint.value;
		}
		float num = Time.time - 0.1f;
		if (num > this.highInterpPoint.time)
		{
			this.lowInterpPoint = this.highInterpPoint;
			this.highInterpPoint = this.newestInterpPoint;
			float num2 = this.highInterpPoint.time - this.lowInterpPoint.time;
			this.inverseLowHighTimespan = ((num2 == 0f) ? 0f : (1f / num2));
		}
		float t = (num - this.lowInterpPoint.time) * this.inverseLowHighTimespan;
		return Mathf.Lerp(this.lowInterpPoint.value, this.highInterpPoint.value, t);
	}

	// Token: 0x0600021D RID: 541 RVA: 0x00009B4D File Offset: 0x00007D4D
	public float GetAuthoritativeValue()
	{
		return this.newestInterpPoint.value;
	}

	// Token: 0x0600021E RID: 542 RVA: 0x00009B5A File Offset: 0x00007D5A
	public void PushValue(float value)
	{
		if (this.newestInterpPoint.value != value)
		{
			this.newestInterpPoint.time = Time.time;
			this.newestInterpPoint.value = value;
		}
	}

	// Token: 0x040001F6 RID: 502
	private const float interpDelay = 0.1f;

	// Token: 0x040001F7 RID: 503
	private NetworkLerpedFloat.InterpPoint lowInterpPoint;

	// Token: 0x040001F8 RID: 504
	private NetworkLerpedFloat.InterpPoint highInterpPoint;

	// Token: 0x040001F9 RID: 505
	private NetworkLerpedFloat.InterpPoint newestInterpPoint;

	// Token: 0x040001FA RID: 506
	private float inverseLowHighTimespan;

	// Token: 0x0200007C RID: 124
	private struct InterpPoint
	{
		// Token: 0x040001FB RID: 507
		public float time;

		// Token: 0x040001FC RID: 508
		public float value;
	}
}
