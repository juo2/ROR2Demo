using System;
using UnityEngine;

// Token: 0x0200007D RID: 125
public struct NetworkLerpedVector3
{
	// Token: 0x0600021F RID: 543 RVA: 0x00009B88 File Offset: 0x00007D88
	public void SetValueImmediate(Vector3 value)
	{
		this.newestInterpPoint.time = Time.time;
		this.newestInterpPoint.value = value;
		this.highInterpPoint = this.newestInterpPoint;
		this.lowInterpPoint = this.newestInterpPoint;
		this.inverseLowHighTimespan = 0f;
	}

	// Token: 0x06000220 RID: 544 RVA: 0x00009BD4 File Offset: 0x00007DD4
	public Vector3 GetCurrentValue(bool hasAuthority)
	{
		if (hasAuthority)
		{
			return this.newestInterpPoint.value;
		}
		float num = Time.time - this.interpDelay;
		if (num > this.highInterpPoint.time)
		{
			this.lowInterpPoint = this.highInterpPoint;
			this.highInterpPoint = this.newestInterpPoint;
			float num2 = this.highInterpPoint.time - this.lowInterpPoint.time;
			this.inverseLowHighTimespan = ((num2 == 0f) ? 0f : (1f / num2));
		}
		float t = (num - this.lowInterpPoint.time) * this.inverseLowHighTimespan;
		return Vector3.Lerp(this.lowInterpPoint.value, this.highInterpPoint.value, t);
	}

	// Token: 0x06000221 RID: 545 RVA: 0x00009C86 File Offset: 0x00007E86
	public Vector3 GetAuthoritativeValue()
	{
		return this.newestInterpPoint.value;
	}

	// Token: 0x06000222 RID: 546 RVA: 0x00009C93 File Offset: 0x00007E93
	public void PushValue(Vector3 value)
	{
		if (this.newestInterpPoint.value != value)
		{
			this.newestInterpPoint.time = Time.time;
			this.newestInterpPoint.value = value;
		}
	}

	// Token: 0x040001FD RID: 509
	public float interpDelay;

	// Token: 0x040001FE RID: 510
	private NetworkLerpedVector3.InterpPoint lowInterpPoint;

	// Token: 0x040001FF RID: 511
	private NetworkLerpedVector3.InterpPoint highInterpPoint;

	// Token: 0x04000200 RID: 512
	private NetworkLerpedVector3.InterpPoint newestInterpPoint;

	// Token: 0x04000201 RID: 513
	private float inverseLowHighTimespan;

	// Token: 0x0200007E RID: 126
	private struct InterpPoint
	{
		// Token: 0x04000202 RID: 514
		public float time;

		// Token: 0x04000203 RID: 515
		public Vector3 value;
	}
}
