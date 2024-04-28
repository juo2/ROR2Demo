using System;
using System.Collections;
using System.Collections.Generic;
using HG;

namespace RoR2
{
	// Token: 0x020005AD RID: 1453
	[Serializable]
	public class EquipmentMask : ICollection<EquipmentIndex>, IEnumerable<EquipmentIndex>, IEnumerable
	{
		// Token: 0x06001A46 RID: 6726 RVA: 0x00071532 File Offset: 0x0006F732
		public EquipmentMask()
		{
			this.array = new bool[EquipmentCatalog.equipmentCount];
		}

		// Token: 0x06001A47 RID: 6727 RVA: 0x0007154A File Offset: 0x0006F74A
		public static EquipmentMask Rent()
		{
			return CollectionPool<EquipmentIndex, EquipmentMask>.RentCollection();
		}

		// Token: 0x06001A48 RID: 6728 RVA: 0x00071551 File Offset: 0x0006F751
		public static void Return(EquipmentMask equipmentMask)
		{
			if (equipmentMask.array.Length == EquipmentCatalog.equipmentCount)
			{
				CollectionPool<EquipmentIndex, EquipmentMask>.ReturnCollection(equipmentMask);
			}
		}

		// Token: 0x06001A49 RID: 6729 RVA: 0x0007156C File Offset: 0x0006F76C
		public bool Contains(EquipmentIndex equipmentIndex)
		{
			bool[] array = this.array;
			bool flag = false;
			return ArrayUtils.GetSafe<bool>(array, (int)equipmentIndex, flag);
		}

		// Token: 0x06001A4A RID: 6730 RVA: 0x00071589 File Offset: 0x0006F789
		public void Add(EquipmentIndex equipmentIndex)
		{
			if (ArrayUtils.IsInBounds<bool>(this.array, (int)equipmentIndex))
			{
				this.array[(int)equipmentIndex] = true;
			}
		}

		// Token: 0x06001A4B RID: 6731 RVA: 0x000715A4 File Offset: 0x0006F7A4
		public bool Remove(EquipmentIndex equipmentIndex)
		{
			if (ArrayUtils.IsInBounds<bool>(this.array, (int)equipmentIndex))
			{
				bool[] array = this.array;
				bool result = array[(int)equipmentIndex];
				array[(int)equipmentIndex] = false;
				return result;
			}
			return false;
		}

		// Token: 0x06001A4C RID: 6732 RVA: 0x000715D4 File Offset: 0x0006F7D4
		public void Clear()
		{
			bool[] array = this.array;
			bool flag = false;
			ArrayUtils.SetAll<bool>(array, flag);
		}

		// Token: 0x06001A4D RID: 6733 RVA: 0x000715F0 File Offset: 0x0006F7F0
		public void CopyTo(EquipmentIndex[] array, int arrayIndex)
		{
			for (int i = 0; i < this.array.Length; i++)
			{
				if (this.array[i])
				{
					array[arrayIndex++] = (EquipmentIndex)i;
				}
			}
		}

		// Token: 0x170001B7 RID: 439
		// (get) Token: 0x06001A4E RID: 6734 RVA: 0x00071623 File Offset: 0x0006F823
		public int Count
		{
			get
			{
				return this.array.Length;
			}
		}

		// Token: 0x170001B8 RID: 440
		// (get) Token: 0x06001A4F RID: 6735 RVA: 0x0000CF8A File Offset: 0x0000B18A
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06001A50 RID: 6736 RVA: 0x0007162D File Offset: 0x0006F82D
		public EquipmentMask.Enumerator GetEnumerator()
		{
			return new EquipmentMask.Enumerator(this.array);
		}

		// Token: 0x06001A51 RID: 6737 RVA: 0x0007163A File Offset: 0x0006F83A
		IEnumerator<EquipmentIndex> IEnumerable<EquipmentIndex>.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06001A52 RID: 6738 RVA: 0x0007163A File Offset: 0x0006F83A
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x04002079 RID: 8313
		private readonly bool[] array;

		// Token: 0x020005AE RID: 1454
		public struct Enumerator : IEnumerator<EquipmentIndex>, IEnumerator, IDisposable
		{
			// Token: 0x06001A53 RID: 6739 RVA: 0x00071647 File Offset: 0x0006F847
			public Enumerator(bool[] array)
			{
				this.Current = EquipmentIndex.None;
				this.target = array;
			}

			// Token: 0x06001A54 RID: 6740 RVA: 0x00071658 File Offset: 0x0006F858
			public bool MoveNext()
			{
				EquipmentIndex equipmentCount = (EquipmentIndex)EquipmentCatalog.equipmentCount;
				while (this.Current < equipmentCount)
				{
					if (this.target[(int)this.Current])
					{
						return true;
					}
					EquipmentIndex value = this.Current + 1;
					this.Current = value;
				}
				return false;
			}

			// Token: 0x06001A55 RID: 6741 RVA: 0x00071698 File Offset: 0x0006F898
			public void Reset()
			{
				this.Current = EquipmentIndex.None;
			}

			// Token: 0x170001B9 RID: 441
			// (get) Token: 0x06001A56 RID: 6742 RVA: 0x000716A1 File Offset: 0x0006F8A1
			// (set) Token: 0x06001A57 RID: 6743 RVA: 0x000716A9 File Offset: 0x0006F8A9
			public EquipmentIndex Current { get; private set; }

			// Token: 0x170001BA RID: 442
			// (get) Token: 0x06001A58 RID: 6744 RVA: 0x000716B2 File Offset: 0x0006F8B2
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x06001A59 RID: 6745 RVA: 0x000026ED File Offset: 0x000008ED
			public void Dispose()
			{
			}

			// Token: 0x0400207A RID: 8314
			private bool[] target;
		}
	}
}
