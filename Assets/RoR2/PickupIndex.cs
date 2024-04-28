using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace RoR2
{
	// Token: 0x0200098D RID: 2445
	[Serializable]
	public struct PickupIndex : IEquatable<PickupIndex>
	{
		// Token: 0x17000536 RID: 1334
		// (get) Token: 0x06003784 RID: 14212 RVA: 0x000EA0F6 File Offset: 0x000E82F6
		public bool isValid
		{
			get
			{
				return (ulong)this.value < (ulong)((long)PickupCatalog.pickupCount);
			}
		}

		// Token: 0x06003785 RID: 14213 RVA: 0x000EA107 File Offset: 0x000E8307
		public PickupIndex(int value)
		{
			this.value = ((value < 0) ? -1 : value);
		}

		// Token: 0x06003786 RID: 14214 RVA: 0x000EA117 File Offset: 0x000E8317
		[Obsolete("PickupIndex methods are deprecated. Use PickupCatalog instead.")]
		public PickupIndex(ItemIndex itemIndex)
		{
			this.value = PickupCatalog.FindPickupIndex(itemIndex).value;
		}

		// Token: 0x06003787 RID: 14215 RVA: 0x000EA12A File Offset: 0x000E832A
		[Obsolete("PickupIndex methods are deprecated. Use PickupCatalog instead.")]
		public PickupIndex(EquipmentIndex equipmentIndex)
		{
			this.value = PickupCatalog.FindPickupIndex(equipmentIndex).value;
		}

		// Token: 0x17000537 RID: 1335
		// (get) Token: 0x06003788 RID: 14216 RVA: 0x000EA13D File Offset: 0x000E833D
		private PickupDef pickupDef
		{
			get
			{
				return PickupCatalog.GetPickupDef(this);
			}
		}

		// Token: 0x06003789 RID: 14217 RVA: 0x000EA14A File Offset: 0x000E834A
		[Obsolete("PickupIndex methods are deprecated. Use PickupCatalog instead.")]
		public GameObject GetHiddenPickupDisplayPrefab()
		{
			return PickupCatalog.GetHiddenPickupDisplayPrefab();
		}

		// Token: 0x0600378A RID: 14218 RVA: 0x000EA151 File Offset: 0x000E8351
		[Obsolete("PickupIndex methods are deprecated. Use PickupCatalog instead.")]
		public GameObject GetPickupDisplayPrefab()
		{
			PickupDef pickupDef = this.pickupDef;
			if (pickupDef == null)
			{
				return null;
			}
			return pickupDef.displayPrefab;
		}

		// Token: 0x0600378B RID: 14219 RVA: 0x000EA164 File Offset: 0x000E8364
		[Obsolete("PickupIndex methods are deprecated. Use PickupCatalog instead.")]
		public GameObject GetPickupDropletDisplayPrefab()
		{
			PickupDef pickupDef = this.pickupDef;
			if (pickupDef == null)
			{
				return null;
			}
			return pickupDef.dropletDisplayPrefab;
		}

		// Token: 0x0600378C RID: 14220 RVA: 0x000EA177 File Offset: 0x000E8377
		[Obsolete("PickupIndex methods are deprecated. Use PickupCatalog instead.")]
		public Color GetPickupColor()
		{
			PickupDef pickupDef = this.pickupDef;
			if (pickupDef == null)
			{
				return Color.black;
			}
			return pickupDef.baseColor;
		}

		// Token: 0x0600378D RID: 14221 RVA: 0x000EA18E File Offset: 0x000E838E
		[Obsolete("PickupIndex methods are deprecated. Use PickupCatalog instead.")]
		public string GetPickupNameToken()
		{
			PickupDef pickupDef = this.pickupDef;
			return ((pickupDef != null) ? pickupDef.nameToken : null) ?? "???";
		}

		// Token: 0x0600378E RID: 14222 RVA: 0x000EA1AB File Offset: 0x000E83AB
		[Obsolete("PickupIndex methods are deprecated. Use PickupCatalog instead.")]
		public UnlockableDef GetUnlockable()
		{
			PickupDef pickupDef = this.pickupDef;
			if (pickupDef == null)
			{
				return null;
			}
			return pickupDef.unlockableDef;
		}

		// Token: 0x0600378F RID: 14223 RVA: 0x000EA1BE File Offset: 0x000E83BE
		[Obsolete("PickupIndex methods are deprecated. Use PickupCatalog instead.")]
		public bool IsLunar()
		{
			PickupDef pickupDef = this.pickupDef;
			return pickupDef != null && pickupDef.isLunar;
		}

		// Token: 0x06003790 RID: 14224 RVA: 0x000EA1D1 File Offset: 0x000E83D1
		[Obsolete("PickupIndex methods are deprecated. Use PickupCatalog instead.")]
		public bool IsBoss()
		{
			PickupDef pickupDef = this.pickupDef;
			return pickupDef != null && pickupDef.isBoss;
		}

		// Token: 0x06003791 RID: 14225 RVA: 0x000EA1E4 File Offset: 0x000E83E4
		[Obsolete("PickupIndex methods are deprecated. Use PickupCatalog instead.")]
		public string GetInteractContextToken()
		{
			PickupDef pickupDef = this.pickupDef;
			return ((pickupDef != null) ? pickupDef.interactContextToken : null) ?? "";
		}

		// Token: 0x17000538 RID: 1336
		// (get) Token: 0x06003792 RID: 14226 RVA: 0x000EA201 File Offset: 0x000E8401
		[Obsolete("PickupIndex methods are deprecated. Use PickupCatalog instead.")]
		public ItemIndex itemIndex
		{
			get
			{
				PickupDef pickupDef = this.pickupDef;
				if (pickupDef == null)
				{
					return ItemIndex.None;
				}
				return pickupDef.itemIndex;
			}
		}

		// Token: 0x17000539 RID: 1337
		// (get) Token: 0x06003793 RID: 14227 RVA: 0x000EA214 File Offset: 0x000E8414
		[Obsolete("PickupIndex methods are deprecated. Use PickupCatalog instead.")]
		public EquipmentIndex equipmentIndex
		{
			get
			{
				PickupDef pickupDef = this.pickupDef;
				if (pickupDef == null)
				{
					return EquipmentIndex.None;
				}
				return pickupDef.equipmentIndex;
			}
		}

		// Token: 0x1700053A RID: 1338
		// (get) Token: 0x06003794 RID: 14228 RVA: 0x000EA227 File Offset: 0x000E8427
		[Obsolete("PickupIndex methods are deprecated. Use PickupCatalog instead.")]
		public uint coinValue
		{
			get
			{
				PickupDef pickupDef = this.pickupDef;
				if (pickupDef == null)
				{
					return 0U;
				}
				return pickupDef.coinValue;
			}
		}

		// Token: 0x06003795 RID: 14229 RVA: 0x000EA23A File Offset: 0x000E843A
		public static bool operator ==(PickupIndex a, PickupIndex b)
		{
			return a.value == b.value;
		}

		// Token: 0x06003796 RID: 14230 RVA: 0x000EA24A File Offset: 0x000E844A
		public static bool operator !=(PickupIndex a, PickupIndex b)
		{
			return a.value != b.value;
		}

		// Token: 0x06003797 RID: 14231 RVA: 0x000EA25D File Offset: 0x000E845D
		public static bool operator <(PickupIndex a, PickupIndex b)
		{
			return a.value < b.value;
		}

		// Token: 0x06003798 RID: 14232 RVA: 0x000EA26D File Offset: 0x000E846D
		public static bool operator >(PickupIndex a, PickupIndex b)
		{
			return a.value > b.value;
		}

		// Token: 0x06003799 RID: 14233 RVA: 0x000EA27D File Offset: 0x000E847D
		public static bool operator <=(PickupIndex a, PickupIndex b)
		{
			return a.value >= b.value;
		}

		// Token: 0x0600379A RID: 14234 RVA: 0x000EA290 File Offset: 0x000E8490
		public static bool operator >=(PickupIndex a, PickupIndex b)
		{
			return a.value <= b.value;
		}

		// Token: 0x0600379B RID: 14235 RVA: 0x000EA2A3 File Offset: 0x000E84A3
		public static PickupIndex operator ++(PickupIndex a)
		{
			return new PickupIndex(a.value + 1);
		}

		// Token: 0x0600379C RID: 14236 RVA: 0x000EA2B2 File Offset: 0x000E84B2
		public static PickupIndex operator --(PickupIndex a)
		{
			return new PickupIndex(a.value - 1);
		}

		// Token: 0x0600379D RID: 14237 RVA: 0x000EA2C1 File Offset: 0x000E84C1
		public override bool Equals(object obj)
		{
			return obj is PickupIndex && this == (PickupIndex)obj;
		}

		// Token: 0x0600379E RID: 14238 RVA: 0x000EA23A File Offset: 0x000E843A
		public bool Equals(PickupIndex other)
		{
			return this.value == other.value;
		}

		// Token: 0x0600379F RID: 14239 RVA: 0x000EA2E0 File Offset: 0x000E84E0
		public override int GetHashCode()
		{
			return this.value.GetHashCode();
		}

		// Token: 0x060037A0 RID: 14240 RVA: 0x000EA2FB File Offset: 0x000E84FB
		public static void WriteToNetworkWriter(NetworkWriter writer, PickupIndex value)
		{
			writer.WritePackedIndex32(value.value);
		}

		// Token: 0x060037A1 RID: 14241 RVA: 0x000EA309 File Offset: 0x000E8509
		public static PickupIndex ReadFromNetworkReader(NetworkReader reader)
		{
			return new PickupIndex(reader.ReadPackedIndex32());
		}

		// Token: 0x060037A2 RID: 14242 RVA: 0x000EA316 File Offset: 0x000E8516
		public override string ToString()
		{
			PickupDef pickupDef = this.pickupDef;
			return ((pickupDef != null) ? pickupDef.internalName : null) ?? string.Format("BadPickupIndex{0}", this.value);
		}

		// Token: 0x060037A3 RID: 14243 RVA: 0x000EA343 File Offset: 0x000E8543
		[Obsolete("PickupIndex methods are deprecated. Use PickupCatalog instead.")]
		public static PickupIndex Find(string name)
		{
			return PickupCatalog.FindPickupIndex(name);
		}

		// Token: 0x060037A4 RID: 14244 RVA: 0x000EA34C File Offset: 0x000E854C
		[Obsolete("PickupIndex methods are deprecated. Use PickupCatalog instead.")]
		public static PickupIndex.Enumerator GetEnumerator()
		{
			return default(PickupIndex.Enumerator);
		}

		// Token: 0x1700053B RID: 1339
		// (get) Token: 0x060037A5 RID: 14245 RVA: 0x000EA364 File Offset: 0x000E8564
		[Obsolete("PickupIndex methods are deprecated. Use PickupCatalog instead.")]
		public static GenericStaticEnumerable<PickupIndex, PickupIndex.Enumerator> allPickups
		{
			get
			{
				return default(GenericStaticEnumerable<PickupIndex, PickupIndex.Enumerator>);
			}
		}

		// Token: 0x040037DB RID: 14299
		public static readonly PickupIndex none = new PickupIndex(-1);

		// Token: 0x040037DC RID: 14300
		[SerializeField]
		public readonly int value;

		// Token: 0x0200098E RID: 2446
		public struct Enumerator : IEnumerator<PickupIndex>, IEnumerator, IDisposable
		{
			// Token: 0x060037A7 RID: 14247 RVA: 0x000EA387 File Offset: 0x000E8587
			public bool MoveNext()
			{
				this.position = ++this.position;
				return this.position.value < PickupCatalog.pickupCount;
			}

			// Token: 0x060037A8 RID: 14248 RVA: 0x000EA3AC File Offset: 0x000E85AC
			public void Reset()
			{
				this.position = PickupIndex.none;
			}

			// Token: 0x1700053C RID: 1340
			// (get) Token: 0x060037A9 RID: 14249 RVA: 0x000EA3B9 File Offset: 0x000E85B9
			public PickupIndex Current
			{
				get
				{
					return this.position;
				}
			}

			// Token: 0x1700053D RID: 1341
			// (get) Token: 0x060037AA RID: 14250 RVA: 0x000EA3C1 File Offset: 0x000E85C1
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x060037AB RID: 14251 RVA: 0x000026ED File Offset: 0x000008ED
			void IDisposable.Dispose()
			{
			}

			// Token: 0x040037DD RID: 14301
			private PickupIndex position;
		}
	}
}
