using System;
using System.Globalization;
using System.Runtime.CompilerServices;

// Token: 0x0200008F RID: 143
public static class TextSerialization
{
	// Token: 0x06000260 RID: 608 RVA: 0x0000A907 File Offset: 0x00008B07
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static int ParseIntInvariant(string s)
	{
		return int.Parse(s, NumberStyles.Integer, TextSerialization.invariantCulture);
	}

	// Token: 0x06000261 RID: 609 RVA: 0x0000A915 File Offset: 0x00008B15
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static uint ParseUintInvariant(string s)
	{
		return uint.Parse(s, NumberStyles.Integer, TextSerialization.invariantCulture);
	}

	// Token: 0x06000262 RID: 610 RVA: 0x0000A923 File Offset: 0x00008B23
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static long ParseLongInvariant(string s)
	{
		return long.Parse(s, NumberStyles.Integer, TextSerialization.invariantCulture);
	}

	// Token: 0x06000263 RID: 611 RVA: 0x0000A931 File Offset: 0x00008B31
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static ulong ParseUlongInvariant(string s)
	{
		return ulong.Parse(s, NumberStyles.Integer, TextSerialization.invariantCulture);
	}

	// Token: 0x06000264 RID: 612 RVA: 0x0000A93F File Offset: 0x00008B3F
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static short ParseShortInvariant(string s)
	{
		return short.Parse(s, NumberStyles.Integer, TextSerialization.invariantCulture);
	}

	// Token: 0x06000265 RID: 613 RVA: 0x0000A94D File Offset: 0x00008B4D
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static ushort ParseUshortInvariant(string s)
	{
		return ushort.Parse(s, NumberStyles.Integer, TextSerialization.invariantCulture);
	}

	// Token: 0x06000266 RID: 614 RVA: 0x0000A95B File Offset: 0x00008B5B
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static float ParseFloatInvariant(string s)
	{
		return float.Parse(s, NumberStyles.Float, TextSerialization.invariantCulture);
	}

	// Token: 0x06000267 RID: 615 RVA: 0x0000A96D File Offset: 0x00008B6D
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static double ParseDoubleInvariant(string s)
	{
		return double.Parse(s, NumberStyles.Float, TextSerialization.invariantCulture);
	}

	// Token: 0x06000268 RID: 616 RVA: 0x0000A97F File Offset: 0x00008B7F
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static decimal ParseDecimalInvariant(string s)
	{
		return decimal.Parse(s, NumberStyles.Float, TextSerialization.invariantCulture);
	}

	// Token: 0x06000269 RID: 617 RVA: 0x0000A991 File Offset: 0x00008B91
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool TryParseInvariant(string s, out int result)
	{
		return int.TryParse(s, NumberStyles.Integer, TextSerialization.invariantCulture, out result);
	}

	// Token: 0x0600026A RID: 618 RVA: 0x0000A9A0 File Offset: 0x00008BA0
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool TryParseInvariant(string s, out uint result)
	{
		return uint.TryParse(s, NumberStyles.Integer, TextSerialization.invariantCulture, out result);
	}

	// Token: 0x0600026B RID: 619 RVA: 0x0000A9AF File Offset: 0x00008BAF
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool TryParseInvariant(string s, out long result)
	{
		return long.TryParse(s, NumberStyles.Integer, TextSerialization.invariantCulture, out result);
	}

	// Token: 0x0600026C RID: 620 RVA: 0x0000A9BE File Offset: 0x00008BBE
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool TryParseInvariant(string s, out ulong result)
	{
		return ulong.TryParse(s, NumberStyles.Integer, TextSerialization.invariantCulture, out result);
	}

	// Token: 0x0600026D RID: 621 RVA: 0x0000A9CD File Offset: 0x00008BCD
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool TryParseInvariant(string s, out short result)
	{
		return short.TryParse(s, NumberStyles.Integer, TextSerialization.invariantCulture, out result);
	}

	// Token: 0x0600026E RID: 622 RVA: 0x0000A9DC File Offset: 0x00008BDC
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool TryParseInvariant(string s, out ushort result)
	{
		return ushort.TryParse(s, NumberStyles.Integer, TextSerialization.invariantCulture, out result);
	}

	// Token: 0x0600026F RID: 623 RVA: 0x0000A9EB File Offset: 0x00008BEB
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool TryParseInvariant(string s, out float result)
	{
		return float.TryParse(s, NumberStyles.Float, TextSerialization.invariantCulture, out result);
	}

	// Token: 0x06000270 RID: 624 RVA: 0x0000A9FE File Offset: 0x00008BFE
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool TryParseInvariant(string s, out double result)
	{
		return double.TryParse(s, NumberStyles.Float, TextSerialization.invariantCulture, out result);
	}

	// Token: 0x06000271 RID: 625 RVA: 0x0000AA11 File Offset: 0x00008C11
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool TryParseInvariant(string s, out decimal result)
	{
		return decimal.TryParse(s, NumberStyles.Float, TextSerialization.invariantCulture, out result);
	}

	// Token: 0x06000272 RID: 626 RVA: 0x0000AA24 File Offset: 0x00008C24
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static string ToStringInvariant(int value)
	{
		return value.ToString(TextSerialization.invariantCulture);
	}

	// Token: 0x06000273 RID: 627 RVA: 0x0000AA32 File Offset: 0x00008C32
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static string ToStringInvariant(uint value)
	{
		return value.ToString(TextSerialization.invariantCulture);
	}

	// Token: 0x06000274 RID: 628 RVA: 0x0000AA40 File Offset: 0x00008C40
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static string ToStringInvariant(long value)
	{
		return value.ToString(TextSerialization.invariantCulture);
	}

	// Token: 0x06000275 RID: 629 RVA: 0x0000AA4E File Offset: 0x00008C4E
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static string ToStringInvariant(ulong value)
	{
		return value.ToString(TextSerialization.invariantCulture);
	}

	// Token: 0x06000276 RID: 630 RVA: 0x0000AA5C File Offset: 0x00008C5C
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static string ToStringInvariant(short value)
	{
		return value.ToString(TextSerialization.invariantCulture);
	}

	// Token: 0x06000277 RID: 631 RVA: 0x0000AA6A File Offset: 0x00008C6A
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static string ToStringInvariant(ushort value)
	{
		return value.ToString(TextSerialization.invariantCulture);
	}

	// Token: 0x06000278 RID: 632 RVA: 0x0000AA78 File Offset: 0x00008C78
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static string ToStringInvariant(float value)
	{
		return value.ToString(TextSerialization.invariantCulture);
	}

	// Token: 0x06000279 RID: 633 RVA: 0x0000AA86 File Offset: 0x00008C86
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static string ToStringInvariant(double value)
	{
		return value.ToString(TextSerialization.invariantCulture);
	}

	// Token: 0x0600027A RID: 634 RVA: 0x0000AA94 File Offset: 0x00008C94
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static string ToStringInvariant(decimal value)
	{
		return value.ToString(TextSerialization.invariantCulture);
	}

	// Token: 0x0600027B RID: 635 RVA: 0x0000AAA2 File Offset: 0x00008CA2
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static string ToStringNumeric(int value)
	{
		return value.ToString("N0");
	}

	// Token: 0x0600027C RID: 636 RVA: 0x0000AAB0 File Offset: 0x00008CB0
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static string ToStringNumeric(uint value)
	{
		return value.ToString("N0");
	}

	// Token: 0x0600027D RID: 637 RVA: 0x0000AABE File Offset: 0x00008CBE
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static string ToStringNumeric(long value)
	{
		return value.ToString("N0");
	}

	// Token: 0x0600027E RID: 638 RVA: 0x0000AACC File Offset: 0x00008CCC
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static string ToStringNumeric(ulong value)
	{
		return value.ToString("N0");
	}

	// Token: 0x0600027F RID: 639 RVA: 0x0000AADA File Offset: 0x00008CDA
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static string ToStringNumeric(short value)
	{
		return value.ToString("N0");
	}

	// Token: 0x06000280 RID: 640 RVA: 0x0000AAE8 File Offset: 0x00008CE8
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static string ToStringNumeric(ushort value)
	{
		return value.ToString("N0");
	}

	// Token: 0x06000281 RID: 641 RVA: 0x0000AAF6 File Offset: 0x00008CF6
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static string ToStringNumeric(float value)
	{
		return value.ToString("N0");
	}

	// Token: 0x06000282 RID: 642 RVA: 0x0000AB04 File Offset: 0x00008D04
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static string ToStringNumeric(double value)
	{
		return value.ToString("N0");
	}

	// Token: 0x06000283 RID: 643 RVA: 0x0000AB12 File Offset: 0x00008D12
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static string ToStringNumeric(decimal value)
	{
		return value.ToString("N0");
	}

	// Token: 0x04000241 RID: 577
	private static readonly CultureInfo invariantCulture = CultureInfo.InvariantCulture;
}
