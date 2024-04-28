using System;
using System.Runtime.CompilerServices;
using HG;
using JetBrains.Annotations;

// Token: 0x02000068 RID: 104
[Obsolete("HGArrayUtilities is deprecated. Use HG.ArrayUtils instead.")]
public static class HGArrayUtilities
{
	// Token: 0x060001AC RID: 428 RVA: 0x000089E9 File Offset: 0x00006BE9
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static void ArrayInsertNoResize<T>(T[] array, int arraySize, int position, ref T value)
	{
		ArrayUtils.ArrayInsertNoResize<T>(array, arraySize, position, value);
	}

	// Token: 0x060001AD RID: 429 RVA: 0x000089F4 File Offset: 0x00006BF4
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static void ArrayInsert<T>(ref T[] array, ref int arraySize, int position, ref T value)
	{
		ArrayUtils.ArrayInsert<T>(ref array, ref arraySize, position, value);
	}

	// Token: 0x060001AE RID: 430 RVA: 0x000089FF File Offset: 0x00006BFF
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static void ArrayInsert<T>(ref T[] array, int position, ref T value)
	{
		ArrayUtils.ArrayInsert<T>(ref array, position, value);
	}

	// Token: 0x060001AF RID: 431 RVA: 0x00008A09 File Offset: 0x00006C09
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static void ArrayAppend<T>(ref T[] array, ref int arraySize, ref T value)
	{
		ArrayUtils.ArrayAppend<T>(ref array, ref arraySize, value);
	}

	// Token: 0x060001B0 RID: 432 RVA: 0x00008A13 File Offset: 0x00006C13
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static void ArrayAppend<T>(ref T[] array, ref T value)
	{
		ArrayUtils.ArrayAppend<T>(ref array, value);
	}

	// Token: 0x060001B1 RID: 433 RVA: 0x00008A1C File Offset: 0x00006C1C
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static void ArrayRemoveAt<T>(ref T[] array, ref int arraySize, int position, int count = 1)
	{
		ArrayUtils.ArrayRemoveAt<T>(array, ref arraySize, position, count);
	}

	// Token: 0x060001B2 RID: 434 RVA: 0x00008A28 File Offset: 0x00006C28
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static void ArrayRemoveAtAndResize<T>(ref T[] array, int position, int count = 1)
	{
		ArrayUtils.ArrayRemoveAtAndResize<T>(ref array, position, count);
	}

	// Token: 0x060001B3 RID: 435 RVA: 0x00008A32 File Offset: 0x00006C32
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static T GetSafe<T>([NotNull] T[] array, int index)
	{
		return ArrayUtils.GetSafe<T>(array, index);
	}

	// Token: 0x060001B4 RID: 436 RVA: 0x00008A3B File Offset: 0x00006C3B
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static T GetSafe<T>([NotNull] T[] array, int index, T defaultValue)
	{
		return ArrayUtils.GetSafe<T>(array, index, defaultValue);
	}

	// Token: 0x060001B5 RID: 437 RVA: 0x00008A46 File Offset: 0x00006C46
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static void SetAll<T>(T[] array, in T value)
	{
		ArrayUtils.SetAll<T>(array, value);
	}

	// Token: 0x060001B6 RID: 438 RVA: 0x00008A4F File Offset: 0x00006C4F
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static void EnsureCapacity<T>(ref T[] array, int capacity)
	{
		ArrayUtils.EnsureCapacity<T>(ref array, capacity);
	}

	// Token: 0x060001B7 RID: 439 RVA: 0x00008A58 File Offset: 0x00006C58
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static void Swap<T>(T[] array, int a, int b)
	{
		ArrayUtils.Swap<T>(array, a, b);
	}

	// Token: 0x060001B8 RID: 440 RVA: 0x00008A62 File Offset: 0x00006C62
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static void Clear<T>(T[] array, ref int count)
	{
		ArrayUtils.Clear<T>(array, ref count);
	}

	// Token: 0x060001B9 RID: 441 RVA: 0x00008A6B File Offset: 0x00006C6B
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool SequenceEquals<T>(T[] a, T[] b) where T : IEquatable<T>
	{
		return ArrayUtils.SequenceEquals<T>(a, b);
	}

	// Token: 0x060001BA RID: 442 RVA: 0x00008A74 File Offset: 0x00006C74
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static T[] Clone<T>(T[] src)
	{
		return ArrayUtils.Clone<T>(src);
	}

	// Token: 0x060001BB RID: 443 RVA: 0x00008A7C File Offset: 0x00006C7C
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool IsInBounds<T>(T[] array, int index)
	{
		return ArrayUtils.IsInBounds<T>(array, index);
	}

	// Token: 0x060001BC RID: 444 RVA: 0x00008A85 File Offset: 0x00006C85
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool IsInBounds<T>(T[] array, uint index)
	{
		return ArrayUtils.IsInBounds<T>(array, index);
	}
}
