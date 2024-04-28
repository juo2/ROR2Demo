using System;
using System.Collections;
using System.Collections.Generic;

namespace HG
{
	// Token: 0x02000013 RID: 19
	public readonly struct ReadOnlyArray<T> : IReadOnlyCollection<T>, IEnumerable<T>, IEnumerable, ICollection<T>, IList<T>, ICloneable
	{
		// Token: 0x0600008F RID: 143 RVA: 0x00004099 File Offset: 0x00002299
		public ReadOnlyArray(T[] src)
		{
			this.src = src;
		}

		// Token: 0x06000090 RID: 144 RVA: 0x000040A2 File Offset: 0x000022A2
		public static implicit operator ReadOnlyArray<T>(T[] src)
		{
			return new ReadOnlyArray<T>(src);
		}

		// Token: 0x17000008 RID: 8
		public T this[int i]
		{
			get
			{
				return ref this.src[i];
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000092 RID: 146 RVA: 0x000040BA File Offset: 0x000022BA
		public object SyncRoot
		{
			get
			{
				return this.src.SyncRoot;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000093 RID: 147 RVA: 0x000040C7 File Offset: 0x000022C7
		public long LongLength
		{
			get
			{
				return (long)this.src.Length;
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000094 RID: 148 RVA: 0x000040D1 File Offset: 0x000022D1
		public int Length
		{
			get
			{
				return this.src.Length;
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000095 RID: 149 RVA: 0x000040DB File Offset: 0x000022DB
		public bool IsSynchronized
		{
			get
			{
				return this.src.IsSynchronized;
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000096 RID: 150 RVA: 0x000040E8 File Offset: 0x000022E8
		public bool IsReadOnly
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000097 RID: 151 RVA: 0x000040EB File Offset: 0x000022EB
		public bool IsFixedSize
		{
			get
			{
				return this.src.IsFixedSize;
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000098 RID: 152 RVA: 0x000040F8 File Offset: 0x000022F8
		public int Rank
		{
			get
			{
				return this.src.Rank;
			}
		}

		// Token: 0x06000099 RID: 153 RVA: 0x00004105 File Offset: 0x00002305
		public static int BinarySearch(ReadOnlyArray<T> array, int index, int length, T value, IComparer<T> comparer)
		{
			return Array.BinarySearch<T>(array.src, index, length, value, comparer);
		}

		// Token: 0x0600009A RID: 154 RVA: 0x00004117 File Offset: 0x00002317
		public static int BinarySearch(ReadOnlyArray<T> array, int index, int length, T value)
		{
			return Array.BinarySearch<T>(array.src, index, length, value);
		}

		// Token: 0x0600009B RID: 155 RVA: 0x00004127 File Offset: 0x00002327
		public static int BinarySearch(ReadOnlyArray<T> array, T value)
		{
			return Array.BinarySearch<T>(array.src, value);
		}

		// Token: 0x0600009C RID: 156 RVA: 0x00004135 File Offset: 0x00002335
		public static int BinarySearch(ReadOnlyArray<T> array, object value, IComparer comparer)
		{
			return Array.BinarySearch(array.src, value, comparer);
		}

		// Token: 0x0600009D RID: 157 RVA: 0x00004144 File Offset: 0x00002344
		public static int BinarySearch(ReadOnlyArray<T> array, object value)
		{
			return Array.BinarySearch(array.src, value);
		}

		// Token: 0x0600009E RID: 158 RVA: 0x00004152 File Offset: 0x00002352
		public static int BinarySearch(ReadOnlyArray<T> array, int index, int length, object value, IComparer comparer)
		{
			return Array.BinarySearch(array.src, index, length, value, comparer);
		}

		// Token: 0x0600009F RID: 159 RVA: 0x00004164 File Offset: 0x00002364
		public static int BinarySearch(ReadOnlyArray<T> array, T value, IComparer<T> comparer)
		{
			return Array.BinarySearch<T>(array.src, value, comparer);
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x00004173 File Offset: 0x00002373
		public static void ConstrainedCopy(ReadOnlyArray<T> sourceArray, int sourceIndex, Array destinationArray, int destinationIndex, int length)
		{
			Array.ConstrainedCopy(sourceArray.src, sourceIndex, destinationArray, destinationIndex, length);
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x00004185 File Offset: 0x00002385
		public static TOutput[] ConvertAll<TOutput>(ReadOnlyArray<T> array, Converter<T, TOutput> converter)
		{
			return Array.ConvertAll<T, TOutput>(array.src, converter);
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x00004193 File Offset: 0x00002393
		public static void Copy(ReadOnlyArray<T> sourceArray, int sourceIndex, Array destinationArray, int destinationIndex, int length)
		{
			Array.Copy(sourceArray.src, sourceIndex, destinationArray, destinationIndex, length);
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x000041A5 File Offset: 0x000023A5
		public static void Copy(ReadOnlyArray<T> sourceArray, long sourceIndex, Array destinationArray, long destinationIndex, long length)
		{
			Array.Copy(sourceArray.src, sourceIndex, destinationArray, destinationIndex, length);
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x000041B7 File Offset: 0x000023B7
		public static void Copy(ReadOnlyArray<T> sourceArray, Array destinationArray, int length)
		{
			Array.Copy(sourceArray.src, destinationArray, length);
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x000041C6 File Offset: 0x000023C6
		public static void Copy(ReadOnlyArray<T> sourceArray, Array destinationArray, long length)
		{
			Array.Copy(sourceArray.src, destinationArray, length);
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x000041D5 File Offset: 0x000023D5
		public static bool Exists(ReadOnlyArray<T> array, Predicate<T> match)
		{
			return Array.Exists<T>(array.src, match);
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x000041E3 File Offset: 0x000023E3
		public static T Find(ReadOnlyArray<T> array, Predicate<T> match)
		{
			return Array.Find<T>(array.src, match);
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x000041F1 File Offset: 0x000023F1
		public static T[] FindAll(ReadOnlyArray<T> array, Predicate<T> match)
		{
			return Array.FindAll<T>(array.src, match);
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x000041FF File Offset: 0x000023FF
		public static int FindIndex(ReadOnlyArray<T> array, Predicate<T> match)
		{
			return Array.FindIndex<T>(array.src, match);
		}

		// Token: 0x060000AA RID: 170 RVA: 0x0000420D File Offset: 0x0000240D
		public static int FindIndex(ReadOnlyArray<T> array, int startIndex, int count, Predicate<T> match)
		{
			return Array.FindIndex<T>(array.src, startIndex, count, match);
		}

		// Token: 0x060000AB RID: 171 RVA: 0x0000421D File Offset: 0x0000241D
		public static int FindIndex(ReadOnlyArray<T> array, int startIndex, Predicate<T> match)
		{
			return Array.FindIndex<T>(array.src, startIndex, match);
		}

		// Token: 0x060000AC RID: 172 RVA: 0x0000422C File Offset: 0x0000242C
		public static T FindLast(ReadOnlyArray<T> array, Predicate<T> match)
		{
			return Array.FindLast<T>(array.src, match);
		}

		// Token: 0x060000AD RID: 173 RVA: 0x0000423A File Offset: 0x0000243A
		public static int FindLastIndex(ReadOnlyArray<T> array, int startIndex, int count, Predicate<T> match)
		{
			return Array.FindLastIndex<T>(array.src, startIndex, count, match);
		}

		// Token: 0x060000AE RID: 174 RVA: 0x0000424A File Offset: 0x0000244A
		public static int FindLastIndex(ReadOnlyArray<T> array, int startIndex, Predicate<T> match)
		{
			return Array.FindLastIndex<T>(array.src, startIndex, match);
		}

		// Token: 0x060000AF RID: 175 RVA: 0x00004259 File Offset: 0x00002459
		public static int FindLastIndex(ReadOnlyArray<T> array, Predicate<T> match)
		{
			return Array.FindLastIndex<T>(array.src, match);
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x00004267 File Offset: 0x00002467
		public static void ForEach(ReadOnlyArray<T> array, Action<T> action)
		{
			Array.ForEach<T>(array.src, action);
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x00004275 File Offset: 0x00002475
		public static int IndexOf(ReadOnlyArray<T> array, T value, int startIndex, int count)
		{
			return Array.IndexOf<T>(array.src, value, startIndex, count);
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x00004285 File Offset: 0x00002485
		public static int IndexOf(ReadOnlyArray<T> array, T value, int startIndex)
		{
			return Array.IndexOf<T>(array.src, value, startIndex);
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x00004294 File Offset: 0x00002494
		public static int IndexOf(ReadOnlyArray<T> array, object value)
		{
			return Array.IndexOf(array.src, value);
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x000042A2 File Offset: 0x000024A2
		public static int IndexOf(ReadOnlyArray<T> array, object value, int startIndex, int count)
		{
			return Array.IndexOf(array.src, value, startIndex, count);
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x000042B2 File Offset: 0x000024B2
		public static int IndexOf(ReadOnlyArray<T> array, object value, int startIndex)
		{
			return Array.IndexOf(array.src, value, startIndex);
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x000042C1 File Offset: 0x000024C1
		public static int IndexOf(ReadOnlyArray<T> array, T value)
		{
			return Array.IndexOf<T>(array.src, value);
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x000042CF File Offset: 0x000024CF
		public static int LastIndexOf(ReadOnlyArray<T> array, object value)
		{
			return Array.LastIndexOf(array.src, value);
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x000042DD File Offset: 0x000024DD
		public static int LastIndexOf(ReadOnlyArray<T> array, T value, int startIndex, int count)
		{
			return Array.LastIndexOf<T>(array.src, value, startIndex, count);
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x000042ED File Offset: 0x000024ED
		public static int LastIndexOf(ReadOnlyArray<T> array, T value, int startIndex)
		{
			return Array.LastIndexOf<T>(array.src, value, startIndex);
		}

		// Token: 0x060000BA RID: 186 RVA: 0x000042FC File Offset: 0x000024FC
		public static int LastIndexOf(ReadOnlyArray<T> array, T value)
		{
			return Array.LastIndexOf<T>(array.src, value);
		}

		// Token: 0x060000BB RID: 187 RVA: 0x0000430A File Offset: 0x0000250A
		public static int LastIndexOf(ReadOnlyArray<T> array, object value, int startIndex, int count)
		{
			return Array.LastIndexOf(array.src, value, startIndex, count);
		}

		// Token: 0x060000BC RID: 188 RVA: 0x0000431A File Offset: 0x0000251A
		public static int LastIndexOf(ReadOnlyArray<T> array, object value, int startIndex)
		{
			return Array.LastIndexOf(array.src, value, startIndex);
		}

		// Token: 0x060000BD RID: 189 RVA: 0x00004329 File Offset: 0x00002529
		public static bool TrueForAll(ReadOnlyArray<T> array, Predicate<T> match)
		{
			return Array.TrueForAll<T>(array.src, match);
		}

		// Token: 0x060000BE RID: 190 RVA: 0x00004337 File Offset: 0x00002537
		public object Clone()
		{
			return this.src.Clone();
		}

		// Token: 0x060000BF RID: 191 RVA: 0x00004344 File Offset: 0x00002544
		public void CopyTo(Array array, long index)
		{
			this.src.CopyTo(array, index);
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x00004353 File Offset: 0x00002553
		public void CopyTo(Array array, int index)
		{
			this.src.CopyTo(array, index);
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x00004362 File Offset: 0x00002562
		public int GetLength(int dimension)
		{
			return this.src.GetLength(dimension);
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x00004370 File Offset: 0x00002570
		public long GetLongLength(int dimension)
		{
			return this.src.GetLongLength(dimension);
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x0000437E File Offset: 0x0000257E
		public int GetLowerBound(int dimension)
		{
			return this.src.GetLowerBound(dimension);
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x0000438C File Offset: 0x0000258C
		public int GetUpperBound(int dimension)
		{
			return this.src.GetUpperBound(dimension);
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x0000439A File Offset: 0x0000259A
		public ReadOnlyArray<T>.Enumerator GetEnumerator()
		{
			return new ReadOnlyArray<T>.Enumerator(this);
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x000043A7 File Offset: 0x000025A7
		IEnumerator<T> IEnumerable<!0>.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x000043A7 File Offset: 0x000025A7
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x060000C8 RID: 200 RVA: 0x000043B4 File Offset: 0x000025B4
		int IReadOnlyCollection<!0>.Count
		{
			get
			{
				return this.Length;
			}
		}

		// Token: 0x17000011 RID: 17
		T IList<!0>.this[int i]
		{
			get
			{
				return this.src[i];
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x060000CB RID: 203 RVA: 0x000043D1 File Offset: 0x000025D1
		int IList<!0>.IndexOf(T item)
		{
			return this.src.IndexOf(item);
		}

		// Token: 0x060000CC RID: 204 RVA: 0x000043DF File Offset: 0x000025DF
		void IList<!0>.Insert(int index, T item)
		{
			throw new NotSupportedException("Collection is read-only.");
		}

		// Token: 0x060000CD RID: 205 RVA: 0x000043DF File Offset: 0x000025DF
		void IList<!0>.RemoveAt(int index)
		{
			throw new NotSupportedException("Collection is read-only.");
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x060000CE RID: 206 RVA: 0x000043B4 File Offset: 0x000025B4
		int ICollection<!0>.Count
		{
			get
			{
				return this.Length;
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x060000CF RID: 207 RVA: 0x000040E8 File Offset: 0x000022E8
		bool ICollection<!0>.IsReadOnly
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x000043DF File Offset: 0x000025DF
		void ICollection<!0>.Add(T item)
		{
			throw new NotSupportedException("Collection is read-only.");
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x000043DF File Offset: 0x000025DF
		void ICollection<!0>.Clear()
		{
			throw new NotSupportedException("Collection is read-only.");
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x000043EB File Offset: 0x000025EB
		bool ICollection<!0>.Contains(T item)
		{
			return this.src.Contains(item);
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x000043F9 File Offset: 0x000025F9
		void ICollection<!0>.CopyTo(T[] array, int arrayIndex)
		{
			this.src.CopyTo(array, arrayIndex);
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x000043DF File Offset: 0x000025DF
		bool ICollection<!0>.Remove(T item)
		{
			throw new NotSupportedException("Collection is read-only.");
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x00004408 File Offset: 0x00002608
		object ICloneable.Clone()
		{
			return this.src.Clone();
		}

		// Token: 0x04000020 RID: 32
		private readonly T[] src;

		// Token: 0x04000021 RID: 33
		private const string readOnlyMessage = "Collection is read-only.";

		// Token: 0x02000028 RID: 40
		public struct Enumerator : IEnumerator<T>, IEnumerator, IDisposable
		{
			// Token: 0x17000023 RID: 35
			// (get) Token: 0x06000149 RID: 329 RVA: 0x000058E4 File Offset: 0x00003AE4
			public T Current
			{
				get
				{
					return this.src.src[this.index];
				}
			}

			// Token: 0x17000024 RID: 36
			// (get) Token: 0x0600014A RID: 330 RVA: 0x000058FC File Offset: 0x00003AFC
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x0600014B RID: 331 RVA: 0x00005909 File Offset: 0x00003B09
			public Enumerator(ReadOnlyArray<T> src)
			{
				this.src = src;
				this.index = -1;
			}

			// Token: 0x0600014C RID: 332 RVA: 0x00005919 File Offset: 0x00003B19
			public void Dispose()
			{
			}

			// Token: 0x0600014D RID: 333 RVA: 0x0000591C File Offset: 0x00003B1C
			public bool MoveNext()
			{
				int num = this.index + 1;
				this.index = num;
				return num < this.src.Length;
			}

			// Token: 0x0600014E RID: 334 RVA: 0x00005947 File Offset: 0x00003B47
			public void Reset()
			{
				this.index = -1;
			}

			// Token: 0x04000058 RID: 88
			private readonly ReadOnlyArray<T> src;

			// Token: 0x04000059 RID: 89
			private int index;
		}
	}
}
