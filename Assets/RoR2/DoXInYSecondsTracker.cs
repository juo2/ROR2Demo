using System;
using HG;

// Token: 0x0200002B RID: 43
public class DoXInYSecondsTracker
{
	// Token: 0x1700001D RID: 29
	// (get) Token: 0x060000BC RID: 188 RVA: 0x00004A3E File Offset: 0x00002C3E
	private float newestTime
	{
		get
		{
			return this.timestamps[0];
		}
	}

	// Token: 0x1700001E RID: 30
	// (get) Token: 0x060000BD RID: 189 RVA: 0x00004A48 File Offset: 0x00002C48
	private int requirement
	{
		get
		{
			return this.timestamps.Length;
		}
	}

	// Token: 0x060000BE RID: 190 RVA: 0x00004A52 File Offset: 0x00002C52
	public DoXInYSecondsTracker(int requirement, float window)
	{
		if (requirement < 1)
		{
			throw new ArgumentException("Argument must be greater than zero", "requirement");
		}
		this.timestamps = new float[requirement];
		this.Clear();
		this.window = window;
	}

	// Token: 0x060000BF RID: 191 RVA: 0x00004A88 File Offset: 0x00002C88
	public void Clear()
	{
		for (int i = 0; i < this.timestamps.Length; i++)
		{
			this.timestamps[i] = float.NegativeInfinity;
		}
	}

	// Token: 0x060000C0 RID: 192 RVA: 0x00004AB8 File Offset: 0x00002CB8
	private int FindInsertionPosition(float t)
	{
		for (int i = 0; i < this.lastValidCount; i++)
		{
			if (this.timestamps[i] < t)
			{
				return i;
			}
		}
		return this.lastValidCount;
	}

	// Token: 0x060000C1 RID: 193 RVA: 0x00004AEC File Offset: 0x00002CEC
	public bool Push(float t)
	{
		float num = t - this.window;
		if (t < this.newestTime)
		{
			this.lastValidCount = this.timestamps.Length;
		}
		int num2 = this.lastValidCount - 1;
		while (num2 >= 0 && num > this.timestamps[num2])
		{
			this.lastValidCount--;
			num2--;
		}
		int num3 = this.FindInsertionPosition(t);
		if (num3 < this.timestamps.Length)
		{
			this.lastValidCount++;
			ArrayUtils.ArrayInsertNoResize<float>(this.timestamps, this.lastValidCount, num3, t);
		}
		return this.lastValidCount == this.requirement;
	}

	// Token: 0x040000BB RID: 187
	private readonly float[] timestamps;

	// Token: 0x040000BC RID: 188
	private readonly float window;

	// Token: 0x040000BD RID: 189
	private int lastValidCount;
}
