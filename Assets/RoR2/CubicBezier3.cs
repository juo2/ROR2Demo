using System;
using UnityEngine;

// Token: 0x02000005 RID: 5
[Serializable]
public struct CubicBezier3
{
	// Token: 0x17000001 RID: 1
	// (get) Token: 0x06000004 RID: 4 RVA: 0x00002060 File Offset: 0x00000260
	public Vector3 p0
	{
		get
		{
			return this.a;
		}
	}

	// Token: 0x17000002 RID: 2
	// (get) Token: 0x06000005 RID: 5 RVA: 0x00002068 File Offset: 0x00000268
	public Vector3 p1
	{
		get
		{
			return this.d;
		}
	}

	// Token: 0x17000003 RID: 3
	// (get) Token: 0x06000006 RID: 6 RVA: 0x00002070 File Offset: 0x00000270
	public Vector3 v0
	{
		get
		{
			return this.b - this.a;
		}
	}

	// Token: 0x17000004 RID: 4
	// (get) Token: 0x06000007 RID: 7 RVA: 0x00002083 File Offset: 0x00000283
	public Vector3 v1
	{
		get
		{
			return this.c - this.d;
		}
	}

	// Token: 0x06000008 RID: 8 RVA: 0x00002098 File Offset: 0x00000298
	public static CubicBezier3 FromVelocities(Vector3 p0, Vector3 v0, Vector3 p1, Vector3 v1)
	{
		return new CubicBezier3
		{
			a = p0,
			b = p0 + v0,
			c = p1 + v1,
			d = p1
		};
	}

	// Token: 0x06000009 RID: 9 RVA: 0x000020DC File Offset: 0x000002DC
	public Vector3 Evaluate(float t)
	{
		float num = t * t;
		float num2 = num * t;
		return this.a + 3f * t * (this.b - this.a) + 3f * num * (this.c - 2f * this.b + this.a) + num2 * (this.d - 3f * this.c + 3f * this.b - this.a);
	}

	// Token: 0x0600000A RID: 10 RVA: 0x00002196 File Offset: 0x00000396
	public void ToVertices(Vector3[] results)
	{
		this.ToVertices(results, 0, results.Length);
	}

	// Token: 0x0600000B RID: 11 RVA: 0x000021A4 File Offset: 0x000003A4
	public void ToVertices(Vector3[] results, int spanStart, int spanLength)
	{
		float num = 1f / (float)(spanLength - 1);
		for (int i = 0; i < spanLength; i++)
		{
			results[spanStart++] = this.Evaluate((float)i * num);
		}
	}

	// Token: 0x0600000C RID: 12 RVA: 0x000021E0 File Offset: 0x000003E0
	public float ApproximateLength(int samples)
	{
		float num = 1f / (float)(samples - 1);
		float num2 = 0f;
		Vector3 vector = this.p0;
		for (int i = 1; i < samples; i++)
		{
			Vector3 vector2 = this.Evaluate((float)i * num);
			num2 += Vector3.Distance(vector, vector2);
			vector = vector2;
		}
		return num2;
	}

	// Token: 0x04000001 RID: 1
	public Vector3 a;

	// Token: 0x04000002 RID: 2
	public Vector3 b;

	// Token: 0x04000003 RID: 3
	public Vector3 c;

	// Token: 0x04000004 RID: 4
	public Vector3 d;
}
