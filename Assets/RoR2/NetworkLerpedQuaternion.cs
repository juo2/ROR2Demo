using System;
using UnityEngine;

// Token: 0x0200007F RID: 127
public struct NetworkLerpedQuaternion
{
	// Token: 0x06000223 RID: 547 RVA: 0x00009CC4 File Offset: 0x00007EC4
	public void SetValueImmediate(Quaternion value)
	{
		this.newestInterpPoint.time = Time.time;
		this.newestInterpPoint.value = value;
		this.highInterpPoint = this.newestInterpPoint;
		this.lowInterpPoint = this.newestInterpPoint;
		this.inverseLowHighTimespan = 0f;
	}

	// Token: 0x06000224 RID: 548 RVA: 0x00009D10 File Offset: 0x00007F10
	public Quaternion GetCurrentValue(bool hasAuthority)
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
		return Quaternion.Slerp(this.lowInterpPoint.value, this.highInterpPoint.value, t);
	}

	// Token: 0x06000225 RID: 549 RVA: 0x00009DC1 File Offset: 0x00007FC1
	public Quaternion GetAuthoritativeValue()
	{
		return this.newestInterpPoint.value;
	}

	// Token: 0x06000226 RID: 550 RVA: 0x00009DCE File Offset: 0x00007FCE
	public void PushValue(Quaternion value)
	{
		if (this.newestInterpPoint.value != value)
		{
			this.newestInterpPoint.time = Time.time;
			this.newestInterpPoint.value = value;
		}
	}

	// Token: 0x04000204 RID: 516
	private const float interpDelay = 0.1f;

	// Token: 0x04000205 RID: 517
	private NetworkLerpedQuaternion.InterpPoint lowInterpPoint;

	// Token: 0x04000206 RID: 518
	private NetworkLerpedQuaternion.InterpPoint highInterpPoint;

	// Token: 0x04000207 RID: 519
	private NetworkLerpedQuaternion.InterpPoint newestInterpPoint;

	// Token: 0x04000208 RID: 520
	private float inverseLowHighTimespan;

	// Token: 0x02000080 RID: 128
	private struct InterpPoint
	{
		// Token: 0x04000209 RID: 521
		public float time;

		// Token: 0x0400020A RID: 522
		public Quaternion value;
	}
}
