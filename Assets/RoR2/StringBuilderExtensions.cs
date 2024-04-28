using System;
using System.Collections.Generic;
using System.Text;
using JetBrains.Annotations;
using UnityEngine;

// Token: 0x0200008D RID: 141
public static class StringBuilderExtensions
{
	// Token: 0x06000252 RID: 594 RVA: 0x0000A68F File Offset: 0x0000888F
	public static StringBuilder AppendInt(this StringBuilder stringBuilder, int value, uint minDigits = 1U, uint maxDigits = 4294967295U)
	{
		return StringBuilderExtensions.AppendSignedInternal(stringBuilder, (long)value, 10U, minDigits, maxDigits);
	}

	// Token: 0x06000253 RID: 595 RVA: 0x0000A69D File Offset: 0x0000889D
	public static StringBuilder AppendUint(this StringBuilder stringBuilder, uint value, uint minDigits = 1U, uint maxDigits = 4294967295U)
	{
		return StringBuilderExtensions.AppendUnsignedInternal(stringBuilder, (ulong)value, 10U, minDigits, maxDigits);
	}

	// Token: 0x06000254 RID: 596 RVA: 0x0000A6AB File Offset: 0x000088AB
	public static StringBuilder AppendLong(this StringBuilder stringBuilder, long value, uint minDigits = 1U, uint maxDigits = 4294967295U)
	{
		return StringBuilderExtensions.AppendSignedInternal(stringBuilder, value, 20U, minDigits, maxDigits);
	}

	// Token: 0x06000255 RID: 597 RVA: 0x0000A6B8 File Offset: 0x000088B8
	public static StringBuilder AppendUlong(this StringBuilder stringBuilder, ulong value, uint minDigits = 1U, uint maxDigits = 4294967295U)
	{
		return StringBuilderExtensions.AppendUnsignedInternal(stringBuilder, value, 20U, minDigits, maxDigits);
	}

	// Token: 0x06000256 RID: 598 RVA: 0x0000A6C8 File Offset: 0x000088C8
	private static StringBuilder AppendUnsignedInternal(StringBuilder stringBuilder, ulong value, uint maxDigitsForType, uint minDigits = 1U, uint maxDigits = 4294967295U)
	{
		if (maxDigits < minDigits)
		{
			throw new ArgumentException("minDigits cannot be greater than maxDigits.");
		}
		uint num = 0U;
		uint num2 = (maxDigitsForType < maxDigits) ? maxDigitsForType : maxDigits;
		ulong num3 = 1UL;
		while (num3 <= value && num < num2)
		{
			num3 *= 10UL;
			num += 1U;
		}
		uint num4 = (minDigits < num) ? num : minDigits;
		if (num4 > 0U)
		{
			uint length = (uint)stringBuilder.Length;
			stringBuilder.Append('0', (int)num4);
			ulong num5 = 0UL;
			ulong num6 = (ulong)(length + num4 - 1U);
			ulong num7 = value;
			while (num5 < (ulong)num)
			{
				ulong num8 = num7 / 10UL;
				byte b = (byte)(num7 - num8 * 10UL);
				char value2 = (char)(48 + b);
				stringBuilder[(int)num6] = value2;
				num7 = num8;
				num5 += 1UL;
				num6 -= 1UL;
			}
		}
		return stringBuilder;
	}

	// Token: 0x06000257 RID: 599 RVA: 0x0000A776 File Offset: 0x00008976
	private static StringBuilder AppendSignedInternal(StringBuilder stringBuilder, long value, uint maxDigitsForType, uint minDigits = 1U, uint maxDigits = 4294967295U)
	{
		if (maxDigits < minDigits)
		{
			throw new ArgumentException("minDigits cannot be greater than maxDigits.");
		}
		if (value < 0L)
		{
			stringBuilder.Append('-');
			value = -value;
		}
		return StringBuilderExtensions.AppendUnsignedInternal(stringBuilder, (ulong)((uint)value), maxDigitsForType, minDigits, maxDigits);
	}

	// Token: 0x06000258 RID: 600 RVA: 0x0000A7A7 File Offset: 0x000089A7
	private static void GetByteHexChars(byte byteValue, out char char1, out char char2)
	{
		char1 = StringBuilderExtensions.hexLookup[byteValue >> 4 & 15];
		char2 = StringBuilderExtensions.hexLookup[(int)(byteValue & 15)];
	}

	// Token: 0x06000259 RID: 601 RVA: 0x0000A7C4 File Offset: 0x000089C4
	public static StringBuilder AppendByteHexValue(this StringBuilder stringBuilder, byte value)
	{
		char value2;
		char value3;
		StringBuilderExtensions.GetByteHexChars(value, out value2, out value3);
		return stringBuilder.Append(value2).Append(value3);
	}

	// Token: 0x0600025A RID: 602 RVA: 0x0000A7E8 File Offset: 0x000089E8
	public static StringBuilder AppendColor32RGBHexValues(this StringBuilder stringBuilder, Color32 color)
	{
		int num = stringBuilder.Length + 6;
		if (stringBuilder.Capacity < num)
		{
			stringBuilder.Capacity = num;
		}
		char value;
		char value2;
		StringBuilderExtensions.GetByteHexChars(color.r, out value, out value2);
		char value3;
		char value4;
		StringBuilderExtensions.GetByteHexChars(color.g, out value3, out value4);
		char value5;
		char value6;
		StringBuilderExtensions.GetByteHexChars(color.b, out value5, out value6);
		return stringBuilder.Append(value).Append(value2).Append(value3).Append(value4).Append(value5).Append(value6);
	}

	// Token: 0x0600025B RID: 603 RVA: 0x0000A868 File Offset: 0x00008A68
	public static StringBuilder JoinEnumerator<TElement, TEnumerator>(this StringBuilder stringBuilder, [NotNull] string separator, TEnumerator enumerator) where TEnumerator : IEnumerator<TElement>
	{
		if (enumerator.MoveNext())
		{
			stringBuilder.Append(enumerator.Current);
			while (enumerator.MoveNext())
			{
				stringBuilder.Append(separator);
				stringBuilder.Append(enumerator.Current);
			}
		}
		return stringBuilder;
	}

	// Token: 0x0600025C RID: 604 RVA: 0x0000A8D0 File Offset: 0x00008AD0
	public static string Take(this StringBuilder stringBuilder)
	{
		string result = stringBuilder.ToString();
		stringBuilder.Clear();
		return result;
	}

	// Token: 0x0400023E RID: 574
	private const uint maxDigitsInUint = 10U;

	// Token: 0x0400023F RID: 575
	private const uint maxDigitsInUlong = 20U;

	// Token: 0x04000240 RID: 576
	private static readonly char[] hexLookup = new char[]
	{
		'0',
		'1',
		'2',
		'3',
		'4',
		'5',
		'6',
		'7',
		'8',
		'9',
		'A',
		'B',
		'C',
		'D',
		'E',
		'F'
	};
}
