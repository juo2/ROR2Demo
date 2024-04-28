using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using HG;
using UnityEngine;

// Token: 0x02000069 RID: 105
public static class HGMath
{
	// Token: 0x060001BD RID: 445 RVA: 0x00008A90 File Offset: 0x00006C90
	[Obsolete("Use HG.Vector3Utils.AverageFast or .AveragePrecise instead.", false)]
	public static Vector3 Average<T>(T entries) where T : ICollection<Vector3>
	{
		int count = entries.Count;
		float d = 1f / (float)count;
		Vector3 vector = Vector3.zero;
		foreach (Vector3 a in entries)
		{
			vector += d * a;
		}
		return vector;
	}

	// Token: 0x060001BE RID: 446 RVA: 0x00008B08 File Offset: 0x00006D08
	[Obsolete("Use HG.Vector3Utils.Average instead.", false)]
	public static Vector3 Average(in Vector3 a, in Vector3 b)
	{
		return Vector3Utils.Average(a, b);
	}

	// Token: 0x060001BF RID: 447 RVA: 0x00008B11 File Offset: 0x00006D11
	[Obsolete("Use HG.Vector3Utils.Average instead.", false)]
	public static Vector3 Average(in Vector3 a, in Vector3 b, in Vector3 c)
	{
		return Vector3Utils.Average(a, b, c);
	}

	// Token: 0x060001C0 RID: 448 RVA: 0x00008B1B File Offset: 0x00006D1B
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static int IntDivCeil(int a, int b)
	{
		return (a - 1) / b + 1;
	}

	// Token: 0x060001C1 RID: 449 RVA: 0x00008B24 File Offset: 0x00006D24
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static uint UintSafeSubtract(uint a, uint b)
	{
		if (b <= a)
		{
			return a - b;
		}
		return 0U;
	}

	// Token: 0x060001C2 RID: 450 RVA: 0x00008B30 File Offset: 0x00006D30
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static uint UintSafeAdd(uint a, uint b)
	{
		uint num = a + b;
		uint num2 = (a > b) ? a : b;
		if (num >= num2)
		{
			return num;
		}
		return uint.MaxValue;
	}

	// Token: 0x060001C3 RID: 451 RVA: 0x00008B51 File Offset: 0x00006D51
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static byte ByteSafeSubtract(byte a, byte b)
	{
		if (b <= a)
		{
			return a - b;
		}
		return 0;
	}

	// Token: 0x060001C4 RID: 452 RVA: 0x00008B60 File Offset: 0x00006D60
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static byte ByteSafeAdd(byte a, byte b)
	{
		byte b2 = a + b;
		byte b3 = (a > b) ? a : b;
		if (b2 >= b3)
		{
			return b2;
		}
		return byte.MaxValue;
	}

	// Token: 0x060001C5 RID: 453 RVA: 0x00008B88 File Offset: 0x00006D88
	public static Vector3 Remap(Vector3 value, Vector3 inMin, Vector3 inMax, Vector3 outMin, Vector3 outMax)
	{
		return new Vector3(outMin.x + (value.x - inMin.x) / (inMax.x - inMin.x) * (outMax.x - outMin.x), outMin.y + (value.y - inMin.y) / (inMax.y - inMin.y) * (outMax.y - outMin.y), outMin.z + (value.z - inMin.z) / (inMax.z - inMin.z) * (outMax.z - outMin.z));
	}

	// Token: 0x060001C6 RID: 454 RVA: 0x00008C2D File Offset: 0x00006E2D
	public static Vector3 Remap(Vector3 value, float inMin, float inMax, float outMin, float outMax)
	{
		return new Vector3(outMin + (value.x - inMin) / (inMax - inMin) * (outMax - outMin), outMin + (value.y - inMin) / (inMax - inMin) * (outMax - outMin), outMin + (value.z - inMin) / (inMax - inMin) * (outMax - outMin));
	}

	// Token: 0x060001C7 RID: 455 RVA: 0x00008C6D File Offset: 0x00006E6D
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private static float Clamp(float value, float min, float max)
	{
		if (value <= min)
		{
			return min;
		}
		if (value >= max)
		{
			return max;
		}
		return value;
	}

	// Token: 0x060001C8 RID: 456 RVA: 0x00008C6D File Offset: 0x00006E6D
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static int Clamp(int value, int min, int max)
	{
		if (value <= min)
		{
			return min;
		}
		if (value >= max)
		{
			return max;
		}
		return value;
	}

	// Token: 0x060001C9 RID: 457 RVA: 0x00008C7C File Offset: 0x00006E7C
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static uint Clamp(uint value, uint min, uint max)
	{
		if (value <= min)
		{
			return min;
		}
		if (value >= max)
		{
			return max;
		}
		return value;
	}

	// Token: 0x060001CA RID: 458 RVA: 0x00008C8C File Offset: 0x00006E8C
	public static Vector3 Clamp(Vector3 value, Vector3 min, Vector3 max)
	{
		return new Vector3(HGMath.Clamp(value.x, min.x, max.x), HGMath.Clamp(value.y, min.y, max.y), HGMath.Clamp(value.z, min.z, max.z));
	}

	// Token: 0x060001CB RID: 459 RVA: 0x00008CE3 File Offset: 0x00006EE3
	public static Vector3 Clamp(Vector3 value, float min, float max)
	{
		return new Vector3(HGMath.Clamp(value.x, min, max), HGMath.Clamp(value.y, min, max), HGMath.Clamp(value.z, min, max));
	}

	// Token: 0x060001CC RID: 460 RVA: 0x00008D11 File Offset: 0x00006F11
	public static bool IsVectorNaN(Vector3 value)
	{
		return float.IsNaN(value.x) || float.IsNaN(value.y) || float.IsNaN(value.z);
	}

	// Token: 0x060001CD RID: 461 RVA: 0x00008D3C File Offset: 0x00006F3C
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool IsVectorValid(ref Vector3 vector3)
	{
		float f = vector3.x + vector3.y + vector3.z;
		return !float.IsInfinity(f) && !float.IsNaN(f);
	}

	// Token: 0x060001CE RID: 462 RVA: 0x00008D74 File Offset: 0x00006F74
	public static bool Overshoots(Vector3 startPosition, Vector3 endPosition, Vector3 targetPosition)
	{
		Vector3 lhs = endPosition - startPosition;
		Vector3 rhs = targetPosition - endPosition;
		return Vector3.Dot(lhs, rhs) <= 0f;
	}

	// Token: 0x060001CF RID: 463 RVA: 0x00008DA0 File Offset: 0x00006FA0
	public static float TriangleArea(in Vector3 a, in Vector3 b, in Vector3 c)
	{
		return 0.5f * Vector3.Cross(b - a, c - a).magnitude;
	}

	// Token: 0x060001D0 RID: 464 RVA: 0x00008DE2 File Offset: 0x00006FE2
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static float CircleRadiusToArea(float radius)
	{
		return 3.1415927f * (radius * radius);
	}

	// Token: 0x060001D1 RID: 465 RVA: 0x00008DED File Offset: 0x00006FED
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static float CircleAreaToRadius(float area)
	{
		return Mathf.Sqrt(area * 0.31830987f);
	}
}
