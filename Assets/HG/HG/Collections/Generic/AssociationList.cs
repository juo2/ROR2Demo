using System;
using System.Collections;
using System.Collections.Generic;

namespace HG.Collections.Generic
{
	// Token: 0x0200001F RID: 31
	public class AssociationList<TKey, TValue> : IDictionary<TKey, TValue>, ICollection<KeyValuePair<TKey, TValue>>, IEnumerable<KeyValuePair<TKey, TValue>>, IEnumerable
	{
		// Token: 0x17000018 RID: 24
		// (get) Token: 0x0600011C RID: 284 RVA: 0x000051B2 File Offset: 0x000033B2
		public int count
		{
			get
			{
				return this._count;
			}
		}

		// Token: 0x0600011D RID: 285 RVA: 0x000051BA File Offset: 0x000033BA
		public AssociationList(int capacity = -1, IEqualityComparer<TKey> keyComparer = null, bool allowDuplicateKeys = false)
		{
			this.keyComparer = (keyComparer ?? EqualityComparer<TKey>.Default);
			this.allowDuplicateKeys = allowDuplicateKeys;
			if (capacity > 0)
			{
				this.buffer = new KeyValuePair<TKey, TValue>[capacity];
			}
		}

		// Token: 0x0600011E RID: 286 RVA: 0x000051F4 File Offset: 0x000033F4
		private void AddInternal(KeyValuePair<TKey, TValue> item)
		{
			if (!this.allowDuplicateKeys && this.FindKeyIndex(item.Key) >= 0)
			{
				throw new ArgumentException("Key '" + AssociationList<TKey, TValue>.KeyToStringSafe(item.Key) + "' is already present.", "Key");
			}
			ArrayUtils.ArrayAppend<KeyValuePair<TKey, TValue>>(ref this.buffer, ref this._count, item);
		}

		// Token: 0x0600011F RID: 287 RVA: 0x00005254 File Offset: 0x00003454
		public TValue GetValue(TKey key)
		{
			AssociationList<TKey, TValue>.CheckKeyNotNull(key, "GetValue", "key");
			int num = this.FindKeyIndex(key);
			if (num >= 0)
			{
				return this.buffer[num].Value;
			}
			throw new KeyNotFoundException();
		}

		// Token: 0x06000120 RID: 288 RVA: 0x00005295 File Offset: 0x00003495
		public AssociationList<TKey, TValue>.Enumerator GetEnumerator()
		{
			return new AssociationList<TKey, TValue>.Enumerator(this);
		}

		// Token: 0x06000121 RID: 289 RVA: 0x000052A0 File Offset: 0x000034A0
		public void GetKeys(List<TKey> dest)
		{
			for (int i = 0; i < this._count; i++)
			{
				dest.Add(this.buffer[i].Key);
			}
		}

		// Token: 0x06000122 RID: 290 RVA: 0x000052D8 File Offset: 0x000034D8
		public void GetValues(List<TValue> dest)
		{
			for (int i = 0; i < this._count; i++)
			{
				dest.Add(this.buffer[i].Value);
			}
		}

		// Token: 0x06000123 RID: 291 RVA: 0x00005310 File Offset: 0x00003510
		public int FindKeyIndex(TKey key)
		{
			for (int i = 0; i < this.buffer.Length; i++)
			{
				if (this.keyComparer.Equals(key, this.buffer[i].Key))
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x06000124 RID: 292 RVA: 0x00005354 File Offset: 0x00003554
		public int FindItemIndex(KeyValuePair<TKey, TValue> item)
		{
			for (int i = 0; i < this._count; i++)
			{
				ref KeyValuePair<TKey, TValue> ptr = ref this.buffer[i];
				if (this.keyComparer.Equals(item.Key, ptr.Key))
				{
					if (EqualityComparer<TValue>.Default.Equals(item.Value, ptr.Value))
					{
						return i;
					}
					if (!this.allowDuplicateKeys)
					{
						return -1;
					}
				}
			}
			return -1;
		}

		// Token: 0x17000019 RID: 25
		public KeyValuePair<TKey, TValue> this[int i]
		{
			get
			{
				if (i < 0 || i >= this._count)
				{
					throw new ArgumentOutOfRangeException("i", string.Format("Index {0} is outside range [0, {1}).", i, this._count));
				}
				return this.buffer[i];
			}
		}

		// Token: 0x06000126 RID: 294 RVA: 0x0000540C File Offset: 0x0000360C
		public void RemoveAt(int i)
		{
			if (i < 0 || i >= this._count)
			{
				throw new ArgumentOutOfRangeException("i", string.Format("Index {0} is outside range [0, {1}).", i, this._count));
			}
			ArrayUtils.ArrayRemoveAt<KeyValuePair<TKey, TValue>>(this.buffer, ref this._count, i, 1);
		}

		// Token: 0x1700001A RID: 26
		public TValue this[TKey key]
		{
			get
			{
				AssociationList<TKey, TValue>.CheckKeyNotNull(key, string.Empty, "key");
				int num = this.FindKeyIndex(key);
				if (num < 0)
				{
					throw new KeyNotFoundException();
				}
				return this.buffer[num].Value;
			}
			set
			{
				AssociationList<TKey, TValue>.CheckKeyNotNull(key, string.Empty, "key");
				int num = this.FindKeyIndex(key);
				if (num >= 0)
				{
					this.buffer[num] = new KeyValuePair<TKey, TValue>(key, value);
					return;
				}
				KeyValuePair<TKey, TValue> keyValuePair = new KeyValuePair<TKey, TValue>(key, value);
				ArrayUtils.ArrayAppend<KeyValuePair<TKey, TValue>>(ref this.buffer, ref this._count, keyValuePair);
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000129 RID: 297 RVA: 0x00005500 File Offset: 0x00003700
		public ICollection<TKey> Keys
		{
			get
			{
				List<TKey> list = new List<TKey>(this._count);
				this.GetKeys(list);
				return list;
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x0600012A RID: 298 RVA: 0x00005524 File Offset: 0x00003724
		public ICollection<TValue> Values
		{
			get
			{
				List<TValue> list = new List<TValue>(this._count);
				this.GetValues(list);
				return list;
			}
		}

		// Token: 0x0600012B RID: 299 RVA: 0x00005545 File Offset: 0x00003745
		public void Add(TKey key, TValue value)
		{
			AssociationList<TKey, TValue>.CheckKeyNotNull(key, "Add", "key");
			this.AddInternal(new KeyValuePair<TKey, TValue>(key, value));
		}

		// Token: 0x0600012C RID: 300 RVA: 0x00005565 File Offset: 0x00003765
		public bool ContainsKey(TKey key)
		{
			AssociationList<TKey, TValue>.CheckKeyNotNull(key, "ContainsKey", "key");
			return this.FindKeyIndex(key) >= 0;
		}

		// Token: 0x0600012D RID: 301 RVA: 0x00005588 File Offset: 0x00003788
		public bool Remove(TKey key)
		{
			AssociationList<TKey, TValue>.CheckKeyNotNull(key, "Remove", "key");
			int num = this.FindKeyIndex(key);
			if (num >= 0)
			{
				ArrayUtils.ArrayRemoveAt<KeyValuePair<TKey, TValue>>(this.buffer, ref this._count, num, 1);
				return true;
			}
			return false;
		}

		// Token: 0x0600012E RID: 302 RVA: 0x000055C8 File Offset: 0x000037C8
		public bool TryGetValue(TKey key, out TValue value)
		{
			AssociationList<TKey, TValue>.CheckKeyNotNull(key, "TryGetValue", "key");
			int num = this.FindKeyIndex(key);
			if (num >= 0)
			{
				value = this.buffer[num].Value;
				return true;
			}
			value = default(TValue);
			return false;
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x0600012F RID: 303 RVA: 0x000051B2 File Offset: 0x000033B2
		public int Count
		{
			get
			{
				return this._count;
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000130 RID: 304 RVA: 0x00005613 File Offset: 0x00003813
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000131 RID: 305 RVA: 0x00005618 File Offset: 0x00003818
		public void Add(KeyValuePair<TKey, TValue> item)
		{
			TKey key = item.Key;
			AssociationList<TKey, TValue>.CheckKeyNotNull(key, "Add", "Key");
			this.AddInternal(item);
		}

		// Token: 0x06000132 RID: 306 RVA: 0x00005645 File Offset: 0x00003845
		public void Clear()
		{
			Array.Clear(this.buffer, 0, this._count);
			this._count = 0;
		}

		// Token: 0x06000133 RID: 307 RVA: 0x00005660 File Offset: 0x00003860
		public bool Contains(KeyValuePair<TKey, TValue> item)
		{
			TKey key = item.Key;
			AssociationList<TKey, TValue>.CheckKeyNotNull(key, "ContainsKey", "Key");
			return this.FindItemIndex(item) >= 0;
		}

		// Token: 0x06000134 RID: 308 RVA: 0x00005693 File Offset: 0x00003893
		public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
		{
			Array.Copy(this.buffer, 0, array, arrayIndex, this._count);
		}

		// Token: 0x06000135 RID: 309 RVA: 0x000056AC File Offset: 0x000038AC
		public bool Remove(KeyValuePair<TKey, TValue> item)
		{
			TKey key = item.Key;
			AssociationList<TKey, TValue>.CheckKeyNotNull(key, "Remove", "Key");
			int num = this.FindItemIndex(item);
			if (num >= 0)
			{
				ArrayUtils.ArrayRemoveAt<KeyValuePair<TKey, TValue>>(this.buffer, ref this._count, num, 1);
				return true;
			}
			return false;
		}

		// Token: 0x06000136 RID: 310 RVA: 0x000056F4 File Offset: 0x000038F4
		IEnumerator<KeyValuePair<TKey, TValue>> IEnumerable<KeyValuePair<!0, !1>>.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06000137 RID: 311 RVA: 0x000056F4 File Offset: 0x000038F4
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06000138 RID: 312 RVA: 0x00005704 File Offset: 0x00003904
		private static string KeyToStringSafe(TKey key)
		{
			string result;
			try
			{
				result = key.ToString();
			}
			catch (Exception ex)
			{
				try
				{
					result = string.Format("EXCEPTION: {0}", ex);
				}
				catch (Exception)
				{
					result = "EXCEPTION: " + ex.GetType().FullName;
				}
			}
			return result;
		}

		// Token: 0x06000139 RID: 313 RVA: 0x00005768 File Offset: 0x00003968
		private static void CheckKeyNotNull(in TKey key, string methodName, string paramName)
		{
			if (!AssociationList<TKey, TValue>.keyIsValueType && key == null)
			{
				throw new ArgumentNullException(string.Concat(new string[]
				{
					"Argument '",
					paramName,
					"' to method '",
					methodName,
					"' cannot be null."
				}));
			}
		}

		// Token: 0x04000040 RID: 64
		private static readonly Type keyType = typeof(TKey);

		// Token: 0x04000041 RID: 65
		private static readonly bool keyIsValueType = AssociationList<TKey, TValue>.keyType.IsValueType;

		// Token: 0x04000042 RID: 66
		private KeyValuePair<TKey, TValue>[] buffer = Array.Empty<KeyValuePair<TKey, TValue>>();

		// Token: 0x04000043 RID: 67
		private int _count;

		// Token: 0x04000044 RID: 68
		private IEqualityComparer<TKey> keyComparer;

		// Token: 0x04000045 RID: 69
		private bool allowDuplicateKeys;

		// Token: 0x02000032 RID: 50
		public struct Enumerator : IEnumerator<KeyValuePair<TKey, TValue>>, IEnumerator, IDisposable
		{
			// Token: 0x0600015E RID: 350 RVA: 0x00005AEA File Offset: 0x00003CEA
			public Enumerator(AssociationList<TKey, TValue> target)
			{
				this.target = target;
				this.i = -1;
			}

			// Token: 0x17000027 RID: 39
			// (get) Token: 0x0600015F RID: 351 RVA: 0x00005AFA File Offset: 0x00003CFA
			// (set) Token: 0x06000160 RID: 352 RVA: 0x00005B02 File Offset: 0x00003D02
			public KeyValuePair<TKey, TValue> Current { get; private set; }

			// Token: 0x17000028 RID: 40
			// (get) Token: 0x06000161 RID: 353 RVA: 0x00005B0B File Offset: 0x00003D0B
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x06000162 RID: 354 RVA: 0x00005B18 File Offset: 0x00003D18
			public void Dispose()
			{
				this.Current = default(KeyValuePair<TKey, TValue>);
			}

			// Token: 0x06000163 RID: 355 RVA: 0x00005B34 File Offset: 0x00003D34
			public bool MoveNext()
			{
				this.i++;
				if (this.i >= this.target._count)
				{
					return false;
				}
				this.Current = this.target.buffer[this.i];
				return true;
			}

			// Token: 0x06000164 RID: 356 RVA: 0x00005B84 File Offset: 0x00003D84
			public void Reset()
			{
				this.i = 0;
				this.Current = default(KeyValuePair<TKey, TValue>);
			}

			// Token: 0x0400006E RID: 110
			private AssociationList<TKey, TValue> target;

			// Token: 0x0400006F RID: 111
			private int i;
		}
	}
}
