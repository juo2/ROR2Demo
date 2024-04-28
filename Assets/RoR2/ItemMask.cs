using System;
using System.Collections;
using System.Collections.Generic;
using HG;

namespace RoR2
{
	// Token: 0x0200093A RID: 2362
	[Serializable]
	public class ItemMask : ICollection<ItemIndex>, IEnumerable<ItemIndex>, IEnumerable
	{
		// Token: 0x06003562 RID: 13666 RVA: 0x000E1C02 File Offset: 0x000DFE02
		public ItemMask()
		{
			this.array = new bool[ItemCatalog.itemCount];
		}

		// Token: 0x06003563 RID: 13667 RVA: 0x000E1C1A File Offset: 0x000DFE1A
		public static ItemMask Rent()
		{
			return CollectionPool<ItemIndex, ItemMask>.RentCollection();
		}

		// Token: 0x06003564 RID: 13668 RVA: 0x000E1C21 File Offset: 0x000DFE21
		public static void Return(ItemMask itemMask)
		{
			if (itemMask.array.Length == ItemCatalog.itemCount)
			{
				CollectionPool<ItemIndex, ItemMask>.ReturnCollection(itemMask);
			}
		}

		// Token: 0x06003565 RID: 13669 RVA: 0x000E1C3C File Offset: 0x000DFE3C
		public bool Contains(ItemIndex itemIndex)
		{
			bool[] array = this.array;
			bool flag = false;
			return ArrayUtils.GetSafe<bool>(array, (int)itemIndex, flag);
		}

		// Token: 0x06003566 RID: 13670 RVA: 0x000E1C59 File Offset: 0x000DFE59
		public void Add(ItemIndex itemIndex)
		{
			if (ArrayUtils.IsInBounds<bool>(this.array, (int)itemIndex))
			{
				this.array[(int)itemIndex] = true;
			}
		}

		// Token: 0x06003567 RID: 13671 RVA: 0x000E1C74 File Offset: 0x000DFE74
		public bool Remove(ItemIndex itemIndex)
		{
			if (ArrayUtils.IsInBounds<bool>(this.array, (int)itemIndex))
			{
				bool[] array = this.array;
				bool result = array[(int)itemIndex];
				array[(int)itemIndex] = false;
				return result;
			}
			return false;
		}

		// Token: 0x06003568 RID: 13672 RVA: 0x000E1CA4 File Offset: 0x000DFEA4
		public void Clear()
		{
			bool[] array = this.array;
			bool flag = false;
			ArrayUtils.SetAll<bool>(array, flag);
		}

		// Token: 0x06003569 RID: 13673 RVA: 0x000E1CC0 File Offset: 0x000DFEC0
		public void CopyTo(ItemIndex[] array, int arrayIndex)
		{
			for (int i = 0; i < this.array.Length; i++)
			{
				if (this.array[i])
				{
					array[arrayIndex++] = (ItemIndex)i;
				}
			}
		}

		// Token: 0x170004EB RID: 1259
		// (get) Token: 0x0600356A RID: 13674 RVA: 0x000E1CF3 File Offset: 0x000DFEF3
		public int Count
		{
			get
			{
				return this.array.Length;
			}
		}

		// Token: 0x170004EC RID: 1260
		// (get) Token: 0x0600356B RID: 13675 RVA: 0x0000CF8A File Offset: 0x0000B18A
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600356C RID: 13676 RVA: 0x000E1CFD File Offset: 0x000DFEFD
		public ItemMask.Enumerator GetEnumerator()
		{
			return new ItemMask.Enumerator(this.array);
		}

		// Token: 0x0600356D RID: 13677 RVA: 0x000E1D0A File Offset: 0x000DFF0A
		IEnumerator<ItemIndex> IEnumerable<ItemIndex>.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x0600356E RID: 13678 RVA: 0x000E1D0A File Offset: 0x000DFF0A
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x04003625 RID: 13861
		private readonly bool[] array;

		// Token: 0x0200093B RID: 2363
		public struct Enumerator : IEnumerator<ItemIndex>, IEnumerator, IDisposable
		{
			// Token: 0x0600356F RID: 13679 RVA: 0x000E1D17 File Offset: 0x000DFF17
			public Enumerator(bool[] array)
			{
				this.Current = ItemIndex.None;
				this.target = array;
			}

			// Token: 0x06003570 RID: 13680 RVA: 0x000E1D28 File Offset: 0x000DFF28
			public bool MoveNext()
			{
				ItemIndex itemCount = (ItemIndex)ItemCatalog.itemCount;
				while (this.Current < itemCount)
				{
					if (this.target[(int)this.Current])
					{
						return true;
					}
					ItemIndex value = this.Current + 1;
					this.Current = value;
				}
				return false;
			}

			// Token: 0x06003571 RID: 13681 RVA: 0x000E1D68 File Offset: 0x000DFF68
			public void Reset()
			{
				this.Current = ItemIndex.None;
			}

			// Token: 0x170004ED RID: 1261
			// (get) Token: 0x06003572 RID: 13682 RVA: 0x000E1D71 File Offset: 0x000DFF71
			// (set) Token: 0x06003573 RID: 13683 RVA: 0x000E1D79 File Offset: 0x000DFF79
			public ItemIndex Current { get; private set; }

			// Token: 0x170004EE RID: 1262
			// (get) Token: 0x06003574 RID: 13684 RVA: 0x000E1D82 File Offset: 0x000DFF82
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x06003575 RID: 13685 RVA: 0x000026ED File Offset: 0x000008ED
			public void Dispose()
			{
			}

			// Token: 0x04003626 RID: 13862
			private bool[] target;
		}
	}
}
