using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;

namespace HG
{
	// Token: 0x02000006 RID: 6
	public static class ArrayUtils
	{
		// Token: 0x06000022 RID: 34 RVA: 0x0000246C File Offset: 0x0000066C
		public static void ArrayInsertNoResize<T>(T[] array, int arraySize, int position, in T value)
		{
			for (int i = arraySize - 1; i > position; i--)
			{
				array[i] = array[i - 1];
			}
			array[position] = value;
		}

		// Token: 0x06000023 RID: 35 RVA: 0x000024A4 File Offset: 0x000006A4
		public static void ArrayInsert<T>(ref T[] array, ref int arraySize, int position, in T value)
		{
			arraySize++;
			if (arraySize > array.Length)
			{
				Array.Resize<T>(ref array, arraySize);
			}
			ArrayUtils.ArrayInsertNoResize<T>(array, arraySize, position, value);
		}

		// Token: 0x06000024 RID: 36 RVA: 0x000024C8 File Offset: 0x000006C8
		public static void ArrayInsert<T>(ref T[] array, int position, in T value)
		{
			int num = array.Length;
			ArrayUtils.ArrayInsert<T>(ref array, ref num, position, value);
		}

		// Token: 0x06000025 RID: 37 RVA: 0x000024E4 File Offset: 0x000006E4
		public static void ArrayAppend<T>(ref T[] array, ref int arraySize, in T value)
		{
			ArrayUtils.ArrayInsert<T>(ref array, ref arraySize, arraySize, value);
		}

		// Token: 0x06000026 RID: 38 RVA: 0x000024F0 File Offset: 0x000006F0
		public static void ArrayAppend<T>(ref T[] array, in T value)
		{
			int num = array.Length;
			ArrayUtils.ArrayAppend<T>(ref array, ref num, value);
		}

		// Token: 0x06000027 RID: 39 RVA: 0x0000250C File Offset: 0x0000070C
		public static void ArrayRemoveAt<T>(T[] array, ref int arraySize, int position, int count = 1)
		{
			int num = arraySize;
			arraySize -= count;
			int i = position;
			int num2 = arraySize;
			while (i < num2)
			{
				array[i] = array[i + count];
				i++;
			}
			for (int j = arraySize; j < num; j++)
			{
				array[j] = default(T);
			}
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00002560 File Offset: 0x00000760
		public static void ArrayRemoveAtAndResize<T>(ref T[] array, int position, int count = 1)
		{
			int newSize = array.Length;
			ArrayUtils.ArrayRemoveAt<T>(array, ref newSize, position, count);
			Array.Resize<T>(ref array, newSize);
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00002584 File Offset: 0x00000784
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static T GetSafe<T>([NotNull] T[] array, int index)
		{
			if ((ulong)index >= (ulong)((long)array.Length))
			{
				return default(T);
			}
			return array[index];
		}

		// Token: 0x0600002A RID: 42 RVA: 0x000025AA File Offset: 0x000007AA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static T GetSafe<T>([NotNull] T[] array, int index, in T defaultValue)
		{
			if ((ulong)index >= (ulong)((long)array.Length))
			{
				return defaultValue;
			}
			return array[index];
		}

		// Token: 0x0600002B RID: 43 RVA: 0x000025C4 File Offset: 0x000007C4
		public static void SetAll<T>(T[] array, in T value)
		{
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = value;
			}
		}

		// Token: 0x0600002C RID: 44 RVA: 0x000025EC File Offset: 0x000007EC
		public static void SetRange<T>(T[] array, in T value, int startPos, int count)
		{
			if (startPos < 0)
			{
				throw new ArgumentOutOfRangeException("startPos", string.Format("{0} cannot be less than zero. {1}={2}", "startPos", "startPos", startPos));
			}
			int num = startPos + count;
			if (array.Length < num)
			{
				throw new ArgumentOutOfRangeException("count", string.Format("{0} + {1} cannot exceed {2}.{3}. {4}.{5}={6} {7}={8} {9}={10}", new object[]
				{
					"startPos",
					"count",
					"array",
					"Length",
					"array",
					"Length",
					array.Length,
					"startPos",
					startPos,
					"count",
					count
				}));
			}
			for (int i = startPos; i < num; i++)
			{
				array[i] = value;
			}
		}

		// Token: 0x0600002D RID: 45 RVA: 0x000026C0 File Offset: 0x000008C0
		public static void EnsureCapacity<T>(ref T[] array, int capacity)
		{
			if (array.Length < capacity)
			{
				Array.Resize<T>(ref array, capacity);
			}
		}

		// Token: 0x0600002E RID: 46 RVA: 0x000026D0 File Offset: 0x000008D0
		public static void Swap<T>(T[] array, int a, int b)
		{
			ref T ptr = ref array[b];
			T t = array[a];
			array[a] = ptr;
			ptr = t;
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00002705 File Offset: 0x00000905
		public static void Clear<T>(T[] array, ref int count)
		{
			Array.Clear(array, 0, count);
			count = 0;
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00002714 File Offset: 0x00000914
		public static bool SequenceEquals<T>(T[] a, T[] b) where T : IEquatable<T>
		{
			if (a == null || b == null)
			{
				return a == null == (b == null);
			}
			if (a == b)
			{
				return true;
			}
			if (a.Length != b.Length)
			{
				return false;
			}
			for (int i = 0; i < a.Length; i++)
			{
				if (!a[i].Equals(b[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00002770 File Offset: 0x00000970
		public static bool SequenceEquals<T>(T[] a, T[] b, [NotNull] EqualityComparison<T> equalityComparison)
		{
			if (a == null || b == null)
			{
				return a == null == (b == null);
			}
			if (a == b)
			{
				return true;
			}
			if (a.Length != b.Length)
			{
				return false;
			}
			for (int i = 0; i < a.Length; i++)
			{
				if (!equalityComparison(a[i], b[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000032 RID: 50 RVA: 0x000027CC File Offset: 0x000009CC
		public static bool SequenceEquals<T, TComparer>(T[] a, T[] b, [NotNull] TComparer equalityComparison) where TComparer : IEqualityComparer<T>
		{
			if (a == null || b == null)
			{
				return a == null == (b == null);
			}
			if (a == b)
			{
				return true;
			}
			if (a.Length != b.Length)
			{
				return false;
			}
			for (int i = 0; i < a.Length; i++)
			{
				if (!equalityComparison.Equals(a[i], b[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00002828 File Offset: 0x00000A28
		public static T[] Clone<T>(T[] src)
		{
			T[] array = new T[src.Length];
			Array.Copy(src, array, src.Length);
			return array;
		}

		// Token: 0x06000034 RID: 52 RVA: 0x0000284C File Offset: 0x00000A4C
		public static TElement[] Clone<TElement, TSrc>(TSrc src) where TSrc : IReadOnlyList<TElement>
		{
			TElement[] array = new TElement[src.Count];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = src[i];
			}
			return array;
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00002890 File Offset: 0x00000A90
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Clone<TElement, TSrc>(TSrc src, out TElement[] dest) where TSrc : IReadOnlyList<TElement>
		{
			dest = ArrayUtils.Clone<TElement, TSrc>(src);
		}

		// Token: 0x06000036 RID: 54 RVA: 0x0000289A File Offset: 0x00000A9A
		public static void CloneTo<T>(T[] src, ref T[] dest)
		{
			Array.Resize<T>(ref dest, src.Length);
			Array.Copy(src, dest, src.Length);
		}

		// Token: 0x06000037 RID: 55 RVA: 0x000028B0 File Offset: 0x00000AB0
		public static void CloneTo<T>(T[] src, ref T[] dest, ref int destLength)
		{
			ArrayUtils.EnsureCapacity<T>(ref dest, src.Length);
			Array.Copy(src, dest, src.Length);
			destLength = src.Length;
		}

		// Token: 0x06000038 RID: 56 RVA: 0x000028CC File Offset: 0x00000ACC
		public static void CloneTo<TElement, TSrc>(TSrc src, ref TElement[] dest) where TSrc : IReadOnlyList<TElement>
		{
			Array.Resize<TElement>(ref dest, src.Count);
			for (int i = 0; i < dest.Length; i++)
			{
				dest[i] = src[i];
			}
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00002914 File Offset: 0x00000B14
		public static void CloneTo<TElement, TSrc>(TSrc src, ref TElement[] dest, ref int destLength) where TSrc : IReadOnlyList<TElement>
		{
			ArrayUtils.EnsureCapacity<TElement>(ref dest, src.Count);
			destLength = src.Count;
			for (int i = 0; i < destLength; i++)
			{
				dest[i] = src[i];
			}
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00002966 File Offset: 0x00000B66
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool IsInBounds<T>(T[] array, int index)
		{
			return (ulong)index < (ulong)((long)array.Length);
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00002966 File Offset: 0x00000B66
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool IsInBounds<T>(T[] array, uint index)
		{
			return (ulong)index < (ulong)((long)array.Length);
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00002970 File Offset: 0x00000B70
		public static T[] Join<T>(T[] a, T[] b)
		{
			int num = a.Length + b.Length;
			if (num == 0)
			{
				return Array.Empty<T>();
			}
			T[] array = new T[num];
			Array.Copy(a, 0, array, 0, a.Length);
			Array.Copy(b, 0, array, a.Length, b.Length);
			return array;
		}

		// Token: 0x0600003D RID: 61 RVA: 0x000029B0 File Offset: 0x00000BB0
		public static T[] Join<T>(params T[][] arrays)
		{
			int num = 0;
			foreach (T[] array in arrays)
			{
				num += array.Length;
			}
			if (num == 0)
			{
				return Array.Empty<T>();
			}
			T[] array2 = new T[num];
			int num2 = 0;
			foreach (T[] array3 in arrays)
			{
				Array.Copy(array3, 0, array2, num2, array3.Length);
				num2 += array3.Length;
			}
			return array2;
		}
	}
}
