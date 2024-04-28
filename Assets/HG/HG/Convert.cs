using System;
using System.Runtime.CompilerServices;

namespace HG
{
	// Token: 0x0200000A RID: 10
	public static class Convert
	{
		// Token: 0x06000048 RID: 72 RVA: 0x00002AEA File Offset: 0x00000CEA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static ushort ToUShortClamped(short value)
		{
			if (value >= 0)
			{
				return (ushort)value;
			}
			return 0;
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00002AF4 File Offset: 0x00000CF4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static short ToShortClamped(ushort value)
		{
			if (value <= 32767)
			{
				return (short)value;
			}
			return short.MaxValue;
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00002B06 File Offset: 0x00000D06
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint ToUIntClamped(int value)
		{
			if (value >= 0)
			{
				return (uint)value;
			}
			return 0U;
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00002B0F File Offset: 0x00000D0F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int ToIntClamped(uint value)
		{
			if (value <= 2147483647U)
			{
				return (int)value;
			}
			return int.MaxValue;
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00002B20 File Offset: 0x00000D20
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static ulong ToULongClamped(long value)
		{
			if (value >= 0L)
			{
				return (ulong)value;
			}
			return 0UL;
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00002B2B File Offset: 0x00000D2B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static long ToLongClamped(ulong value)
		{
			if (value <= 9223372036854775807UL)
			{
				return (long)value;
			}
			return long.MaxValue;
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00002B44 File Offset: 0x00000D44
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static float Clamp(float value, float min, float max)
		{
			if (value < min)
			{
				return min;
			}
			if (value <= max)
			{
				return value;
			}
			return max;
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00002B53 File Offset: 0x00000D53
		public static uint FloorToUIntClamped(float value)
		{
			return (uint)Convert.Clamp((float)Math.Floor((double)value), 0f, 4.2949673E+09f);
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00002B6D File Offset: 0x00000D6D
		public static uint CeilToUIntClamped(float value)
		{
			return (uint)Convert.Clamp((float)Math.Ceiling((double)value), 0f, 4.2949673E+09f);
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00002B87 File Offset: 0x00000D87
		public static int FloorToIntClamped(float value)
		{
			return (int)Convert.Clamp((float)Math.Floor((double)value), -2.1474836E+09f, 2.1474836E+09f);
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00002BA1 File Offset: 0x00000DA1
		public static int CeilToIntClamped(float value)
		{
			return (int)Convert.Clamp((float)Math.Ceiling((double)value), -2.1474836E+09f, 2.1474836E+09f);
		}
	}
}
