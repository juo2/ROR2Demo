using System;
using UnityEngine;

// Token: 0x02000079 RID: 121
public struct NetworkLerpedAngleNew
{
	// Token: 0x06000217 RID: 535 RVA: 0x00009914 File Offset: 0x00007B14
	public void SetValueImmediate(float value)
	{
		this.newestInterpPoint.time = Time.time;
		this.newestInterpPoint.value = value;
		this.highInterpPoint = this.newestInterpPoint;
		this.lowInterpPoint = this.newestInterpPoint;
		this.inverseLowHighTimespan = 0f;
	}

	// Token: 0x06000218 RID: 536 RVA: 0x00009960 File Offset: 0x00007B60
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
		float t = Mathf.Clamp01((num - this.lowInterpPoint.time) * this.inverseLowHighTimespan);
		return Mathf.LerpAngle(this.lowInterpPoint.value, this.highInterpPoint.value, t);
	}

	// Token: 0x06000219 RID: 537 RVA: 0x00009A16 File Offset: 0x00007C16
	public float GetAuthoritativeValue()
	{
		return this.newestInterpPoint.value;
	}

	// Token: 0x0600021A RID: 538 RVA: 0x00009A23 File Offset: 0x00007C23
	public void PushValue(float value)
	{
		if (this.newestInterpPoint.value != value)
		{
			this.newestInterpPoint.time = Time.time;
			this.newestInterpPoint.value = value;
		}
	}

	// Token: 0x040001EF RID: 495
	private const float interpDelay = 0.1f;

	// Token: 0x040001F0 RID: 496
	private NetworkLerpedAngleNew.InterpPoint lowInterpPoint;

	// Token: 0x040001F1 RID: 497
	private NetworkLerpedAngleNew.InterpPoint highInterpPoint;

	// Token: 0x040001F2 RID: 498
	private NetworkLerpedAngleNew.InterpPoint newestInterpPoint;

	// Token: 0x040001F3 RID: 499
	private float inverseLowHighTimespan;

	// Token: 0x0200007A RID: 122
	private struct InterpPoint
	{
		// Token: 0x040001F4 RID: 500
		public float time;

		// Token: 0x040001F5 RID: 501
		public float value;
	}
}
