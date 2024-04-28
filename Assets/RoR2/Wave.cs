using System;
using UnityEngine;
using UnityEngine.Serialization;

// Token: 0x02000094 RID: 148
[Serializable]
public struct Wave
{
	// Token: 0x1700002C RID: 44
	// (get) Token: 0x0600029E RID: 670 RVA: 0x0000AC66 File Offset: 0x00008E66
	// (set) Token: 0x0600029F RID: 671 RVA: 0x0000AC74 File Offset: 0x00008E74
	public float period
	{
		get
		{
			return 1f / this.frequency;
		}
		set
		{
			this.frequency = 1f / value;
		}
	}

	// Token: 0x060002A0 RID: 672 RVA: 0x0000AC83 File Offset: 0x00008E83
	public float Evaluate(float t)
	{
		return Mathf.Sin(6.2831855f * (this.frequency * t + this.cycleOffset)) * this.amplitude;
	}

	// Token: 0x04000245 RID: 581
	public float amplitude;

	// Token: 0x04000246 RID: 582
	public float frequency;

	// Token: 0x04000247 RID: 583
	[FormerlySerializedAs("phase")]
	public float cycleOffset;
}
