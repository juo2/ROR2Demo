using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;

namespace HG
{
	// Token: 0x02000012 RID: 18
	public static class ListUtils
	{
		// Token: 0x06000077 RID: 119 RVA: 0x00003A94 File Offset: 0x00001C94
		public static int FirstOccurrenceByReference<TList, TElement>([NotNull] TList list, in TElement value) where TList : class, IReadOnlyList<TElement> where TElement : class
		{
			int i = 0;
			int count = list.Count;
			while (i < count)
			{
				if (value == list[i])
				{
					return i;
				}
				i++;
			}
			return -1;
		}

		// Token: 0x06000078 RID: 120 RVA: 0x00003ADC File Offset: 0x00001CDC
		public static int FirstOccurrenceByValue<TList, TElement>([NotNull] TList list, in TElement value) where TList : class, IReadOnlyList<TElement> where TElement : IEquatable<TElement>
		{
			int i = 0;
			int count = list.Count;
			while (i < count)
			{
				TElement telement = value;
				if (telement.Equals(list[i]))
				{
					return i;
				}
				i++;
			}
			return -1;
		}

		// Token: 0x06000079 RID: 121 RVA: 0x00003B26 File Offset: 0x00001D26
		public static void FindExclusiveEntriesByReference<T>([NotNull] List<T> inputListA, [NotNull] List<T> inputListB, [CanBeNull] List<T> outputEntriesInAOnly, [CanBeNull] List<T> outputEntriesInBOnly) where T : class
		{
			ListUtils.FindExclusiveEntriesByReference<List<T>, List<T>, List<T>, List<T>, T>(inputListA, inputListB, outputEntriesInAOnly, outputEntriesInBOnly);
		}

		// Token: 0x0600007A RID: 122 RVA: 0x00003B34 File Offset: 0x00001D34
		public static void FindExclusiveEntriesByReference<TInputListA, TInputListB, TOutputListA, TOutputListB, TElement>([NotNull] TInputListA inputListA, [NotNull] TInputListB inputListB, [CanBeNull] TOutputListA outputEntriesInAOnly, [CanBeNull] TOutputListB outputEntriesInBOnly) where TInputListA : class, IReadOnlyList<TElement> where TInputListB : class, IReadOnlyList<TElement> where TOutputListA : class, IList<TElement> where TOutputListB : class, IList<TElement> where TElement : class
		{
			if (outputEntriesInAOnly == inputListA)
			{
				throw new ArgumentException("'outputEntriesInAOnly' cannot be input list inputListA");
			}
			if (outputEntriesInBOnly == inputListA)
			{
				throw new ArgumentException("'outputEntriesInBOnly' cannot be input list inputListA");
			}
			if (outputEntriesInAOnly == inputListB)
			{
				throw new ArgumentException("'outputEntriesInAOnly' cannot be input list inputListB");
			}
			if (outputEntriesInBOnly == inputListB)
			{
				throw new ArgumentException("'outputEntriesInBOnly' cannot be input list inputListB");
			}
			if (outputEntriesInAOnly != null)
			{
				int i = 0;
				int count = inputListA.Count;
				while (i < count)
				{
					TElement item = inputListA[i];
					if (ListUtils.FirstOccurrenceByReference<TInputListB, TElement>(inputListB, item) == -1)
					{
						outputEntriesInAOnly.Add(item);
					}
					i++;
				}
			}
			if (outputEntriesInBOnly != null)
			{
				int j = 0;
				int count2 = inputListB.Count;
				while (j < count2)
				{
					TElement item2 = inputListB[j];
					if (ListUtils.FirstOccurrenceByReference<TInputListA, TElement>(inputListA, item2) == -1)
					{
						outputEntriesInBOnly.Add(item2);
					}
					j++;
				}
			}
		}

		// Token: 0x0600007B RID: 123 RVA: 0x00003C31 File Offset: 0x00001E31
		public static void FindExclusiveEntriesByValue<T>([NotNull] List<T> inputListA, [NotNull] List<T> inputListB, [CanBeNull] List<T> outputEntriesInAOnly, [CanBeNull] List<T> outputEntriesInBOnly) where T : IEquatable<T>
		{
			ListUtils.FindExclusiveEntriesByValue<List<T>, List<T>, List<T>, List<T>, T>(inputListA, inputListB, outputEntriesInAOnly, outputEntriesInBOnly);
		}

		// Token: 0x0600007C RID: 124 RVA: 0x00003C3C File Offset: 0x00001E3C
		public static void FindExclusiveEntriesByValue<TInputListA, TInputListB, TOutputListA, TOutputListB, TElement>([NotNull] TInputListA inputListA, [NotNull] TInputListB inputListB, [CanBeNull] TOutputListA outputEntriesInAOnly, [CanBeNull] TOutputListB outputEntriesInBOnly) where TInputListA : class, IReadOnlyList<TElement> where TInputListB : class, IReadOnlyList<TElement> where TOutputListA : class, IList<TElement> where TOutputListB : class, IList<TElement> where TElement : IEquatable<TElement>
		{
			if (outputEntriesInAOnly == inputListA)
			{
				throw new ArgumentException("'outputEntriesInAOnly' cannot be input list inputListA");
			}
			if (outputEntriesInBOnly == inputListA)
			{
				throw new ArgumentException("'outputEntriesInBOnly' cannot be input list inputListA");
			}
			if (outputEntriesInAOnly == inputListB)
			{
				throw new ArgumentException("'outputEntriesInAOnly' cannot be input list inputListB");
			}
			if (outputEntriesInBOnly == inputListB)
			{
				throw new ArgumentException("'outputEntriesInBOnly' cannot be input list inputListB");
			}
			if (outputEntriesInAOnly != null)
			{
				int i = 0;
				int count = inputListA.Count;
				while (i < count)
				{
					TElement item = inputListA[i];
					if (ListUtils.FirstOccurrenceByValue<TInputListB, TElement>(inputListB, item) == -1)
					{
						outputEntriesInAOnly.Add(item);
					}
					i++;
				}
			}
			if (outputEntriesInBOnly != null)
			{
				int j = 0;
				int count2 = inputListB.Count;
				while (j < count2)
				{
					TElement item2 = inputListB[j];
					if (ListUtils.FirstOccurrenceByValue<TInputListA, TElement>(inputListA, item2) == -1)
					{
						outputEntriesInBOnly.Add(item2);
					}
					j++;
				}
			}
		}

		// Token: 0x0600007D RID: 125 RVA: 0x00003D3C File Offset: 0x00001F3C
		public static bool SequenceEquals<T>(List<T> a, List<T> b) where T : IEquatable<T>
		{
			if (a == null || b == null)
			{
				return a == null == (b == null);
			}
			if (a == b)
			{
				return true;
			}
			if (a.Count != b.Count)
			{
				return false;
			}
			for (int i = 0; i < a.Count; i++)
			{
				T t = a[i];
				if (!t.Equals(b[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600007E RID: 126 RVA: 0x00003DA4 File Offset: 0x00001FA4
		public static bool SequenceEquals<T>(List<T> a, List<T> b, [NotNull] EqualityComparison<T> equalityComparison)
		{
			if (a == null || b == null)
			{
				return a == null == (b == null);
			}
			if (a == b)
			{
				return true;
			}
			if (a.Count != b.Count)
			{
				return false;
			}
			for (int i = 0; i < a.Count; i++)
			{
				T t = a[i];
				T t2 = b[i];
				if (!equalityComparison(t, t2))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600007F RID: 127 RVA: 0x00003E08 File Offset: 0x00002008
		public static void Sort<TValue, TKey>([NotNull] List<TValue> values, [NotNull] List<TKey> keys) where TKey : IComparable<TKey>
		{
			for (int i = keys.Count; i > 1; i--)
			{
				bool flag = true;
				for (int j = 1; j < i; j++)
				{
					TKey tkey = keys[j - 1];
					if (tkey.CompareTo(keys[j]) > 0)
					{
						TKey value = keys[j - 1];
						keys[j - 1] = keys[j];
						keys[j] = value;
						TValue value2 = values[j - 1];
						values[j - 1] = values[j];
						values[j] = value2;
						flag = false;
					}
				}
				if (flag)
				{
					return;
				}
				i--;
			}
		}

		// Token: 0x06000080 RID: 128 RVA: 0x00003EB0 File Offset: 0x000020B0
		public static void AddRange<T>([NotNull] List<T> dest, [NotNull] T[] src)
		{
			foreach (T item in src)
			{
				dest.Add(item);
			}
		}

		// Token: 0x06000081 RID: 129 RVA: 0x00003EDC File Offset: 0x000020DC
		public static void AddRange<TElement, TSrc>([NotNull] List<TElement> dest, [NotNull] TSrc src) where TSrc : IReadOnlyList<TElement>
		{
			int i = 0;
			int count = src.Count;
			while (i < count)
			{
				dest.Add(src[i]);
				i++;
			}
		}

		// Token: 0x06000082 RID: 130 RVA: 0x00003F17 File Offset: 0x00002117
		public static void AddIfUnique<TElement>([NotNull] List<TElement> dest, [CanBeNull] TElement value)
		{
			if (!dest.Contains(value))
			{
				dest.Add(value);
			}
		}

		// Token: 0x06000083 RID: 131 RVA: 0x00003F29 File Offset: 0x00002129
		public static void EnsureCapacity<T>([NotNull] List<T> list, int minCapacity)
		{
			if (list.Capacity < minCapacity)
			{
				list.Capacity = minCapacity;
			}
		}

		// Token: 0x06000084 RID: 132 RVA: 0x00003F3C File Offset: 0x0000213C
		public static void CloneTo<T>([NotNull] List<T> src, [NotNull] List<T> dest)
		{
			ListUtils.EnsureCapacity<T>(dest, src.Count);
			int i = 0;
			int num = Math.Min(dest.Count, src.Count);
			while (i < num)
			{
				dest[i] = src[i];
				i++;
			}
			int j = dest.Count;
			int count = src.Count;
			while (j < count)
			{
				dest.Add(src[j]);
				j++;
			}
			if (dest.Count > src.Count)
			{
				dest.RemoveRange(src.Count, dest.Count - src.Count);
			}
		}

		// Token: 0x06000085 RID: 133 RVA: 0x00003FCD File Offset: 0x000021CD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool IsInBounds<T>([NotNull] List<T> list, int index)
		{
			return (ulong)index < (ulong)((long)list.Count);
		}

		// Token: 0x06000086 RID: 134 RVA: 0x00003FDA File Offset: 0x000021DA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool IsInBounds<T>([NotNull] List<T> list, uint index)
		{
			return (ulong)index < (ulong)((long)list.Count);
		}

		// Token: 0x06000087 RID: 135 RVA: 0x00003FE8 File Offset: 0x000021E8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static T GetSafe<T>([NotNull] List<T> list, int index)
		{
			if ((ulong)index >= (ulong)((long)list.Count))
			{
				return default(T);
			}
			return list[index];
		}

		// Token: 0x06000088 RID: 136 RVA: 0x00004011 File Offset: 0x00002211
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static T GetSafe<T>([NotNull] List<T> list, int index, in T defaultValue)
		{
			if ((ulong)index >= (ulong)((long)list.Count))
			{
				return defaultValue;
			}
			return list[index];
		}

		// Token: 0x06000089 RID: 137 RVA: 0x0000402C File Offset: 0x0000222C
		public static T Take<T>([NotNull] List<T> list, int index)
		{
			T result = list[index];
			list.RemoveAt(index);
			return result;
		}

		// Token: 0x0600008A RID: 138 RVA: 0x0000403C File Offset: 0x0000223C
		public static T TakeFirst<T>([NotNull] List<T> list)
		{
			return ListUtils.Take<T>(list, 0);
		}

		// Token: 0x0600008B RID: 139 RVA: 0x00004045 File Offset: 0x00002245
		public static T TakeLast<T>([NotNull] List<T> list)
		{
			return ListUtils.Take<T>(list, list.Count - 1);
		}

		// Token: 0x0600008C RID: 140 RVA: 0x00004055 File Offset: 0x00002255
		public static bool TryTake<T>([NotNull] List<T> list, int index, out T result)
		{
			if (!ListUtils.IsInBounds<T>(list, index))
			{
				result = default(T);
				return false;
			}
			result = list[index];
			list.RemoveAt(index);
			return true;
		}

		// Token: 0x0600008D RID: 141 RVA: 0x0000407E File Offset: 0x0000227E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryTakeFirst<T>([NotNull] List<T> list, out T result)
		{
			return ListUtils.TryTake<T>(list, 0, out result);
		}

		// Token: 0x0600008E RID: 142 RVA: 0x00004088 File Offset: 0x00002288
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryTakeLast<T>([NotNull] List<T> list, out T result)
		{
			return ListUtils.TryTake<T>(list, list.Count - 1, out result);
		}
	}
}
