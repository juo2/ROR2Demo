using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Xml.Linq;
using HG;
using RoR2.ContentManagement;
using RoR2.Modding;
using UnityEngine;

namespace RoR2
{
	// Token: 0x0200093E RID: 2366
	public static class ItemCatalog
	{
		// Token: 0x170004EF RID: 1263
		// (get) Token: 0x06003576 RID: 13686 RVA: 0x000E1D8F File Offset: 0x000DFF8F
		public static int itemCount
		{
			get
			{
				return ItemCatalog.itemDefs.Length;
			}
		}

		// Token: 0x06003577 RID: 13687 RVA: 0x000E1D98 File Offset: 0x000DFF98
		[SystemInitializer(new Type[]
		{
			typeof(ItemTierCatalog)
		})]
		private static void Init()
		{
			HGXml.Register<ItemIndex[]>(delegate(XElement element, ItemIndex[] obj)
			{
				element.Value = string.Join(" ", obj.Select(delegate(ItemIndex v)
				{
					ItemDef itemDef = ItemCatalog.GetItemDef(v);
					if (itemDef == null)
					{
						return null;
					}
					return itemDef.name;
				}));
			}, delegate(XElement element, ref ItemIndex[] output)
			{
				output = element.Value.Split(new char[]
				{
					' '
				}).Select(delegate(string v)
				{
					ItemDef itemDef = ItemCatalog.GetItemDef(ItemCatalog.FindItemIndex(v));
					if (itemDef == null)
					{
						return ItemIndex.None;
					}
					return itemDef.itemIndex;
				}).ToArray<ItemIndex>();
				return true;
			});
			ItemCatalog.SetItemDefs(ContentManager.itemDefs);
			ItemCatalog.SetItemRelationships(ContentManager.itemRelationshipProviders);
			ItemCatalog.availability.MakeAvailable();
		}

		// Token: 0x06003578 RID: 13688 RVA: 0x000E1E08 File Offset: 0x000E0008
		private static void SetItemDefs(ItemDef[] newItemDefs)
		{
			ItemDef[] array = ItemCatalog.itemDefs;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].itemIndex = ItemIndex.None;
			}
			ArrayUtils.CloneTo<ItemDef>(newItemDefs, ref ItemCatalog.itemDefs);
			ItemCatalog.itemNameToIndex.Clear();
			ItemCatalog.itemOrderBuffers.Clear();
			ItemCatalog.itemStackArrays.Clear();
			Array.Resize<string>(ref ItemCatalog.itemNames, newItemDefs.Length);
			for (int j = 0; j < newItemDefs.Length; j++)
			{
				ItemCatalog.itemNames[j] = newItemDefs[j].name;
			}
			Array.Sort<string, ItemDef>(ItemCatalog.itemNames, ItemCatalog.itemDefs, StringComparer.Ordinal);
			for (ItemIndex itemIndex = (ItemIndex)0; itemIndex < (ItemIndex)ItemCatalog.itemDefs.Length; itemIndex++)
			{
				ItemDef itemDef = ItemCatalog.itemDefs[(int)itemIndex];
				string key = ItemCatalog.itemNames[(int)itemIndex];
				itemDef.itemIndex = itemIndex;
				switch (itemDef.tier)
				{
				case ItemTier.Tier1:
					ItemCatalog.tier1ItemList.Add(itemIndex);
					break;
				case ItemTier.Tier2:
					ItemCatalog.tier2ItemList.Add(itemIndex);
					break;
				case ItemTier.Tier3:
					ItemCatalog.tier3ItemList.Add(itemIndex);
					break;
				case ItemTier.Lunar:
					ItemCatalog.lunarItemList.Add(itemIndex);
					break;
				}
				ItemCatalog.itemNameToIndex[key] = itemIndex;
			}
			int num = 21;
			Array.Resize<ItemIndex[]>(ref ItemCatalog.itemIndicesByTag, num);
			ItemIndex[][] array2 = ItemCatalog.itemIndicesByTag;
			ItemIndex[] array3 = Array.Empty<ItemIndex>();
			ArrayUtils.SetAll<ItemIndex[]>(array2, array3);
			List<ItemIndex>[] array4 = new List<ItemIndex>[num];
			for (ItemTag itemTag = ItemTag.Any; itemTag < (ItemTag)num; itemTag++)
			{
				array4[(int)itemTag] = CollectionPool<ItemIndex, List<ItemIndex>>.RentCollection();
			}
			for (ItemIndex itemIndex2 = (ItemIndex)0; itemIndex2 < (ItemIndex)ItemCatalog.itemDefs.Length; itemIndex2++)
			{
				foreach (ItemTag itemTag2 in ItemCatalog.itemDefs[(int)itemIndex2].tags)
				{
					array4[(int)itemTag2].Add(itemIndex2);
				}
			}
			for (ItemTag itemTag3 = ItemTag.Any; itemTag3 < (ItemTag)num; itemTag3++)
			{
				ref List<ItemIndex> ptr = ref array4[(int)itemTag3];
				ItemCatalog.itemIndicesByTag[(int)itemTag3] = ptr.ToArray();
				ptr = CollectionPool<ItemIndex, List<ItemIndex>>.ReturnCollection(ptr);
			}
		}

		// Token: 0x06003579 RID: 13689 RVA: 0x000E1FE8 File Offset: 0x000E01E8
		private static void SetItemRelationships(ItemRelationshipProvider[] newProviders)
		{
			Dictionary<ItemRelationshipType, HashSet<ItemDef.Pair>> dictionary = new Dictionary<ItemRelationshipType, HashSet<ItemDef.Pair>>();
			foreach (ItemRelationshipProvider itemRelationshipProvider in newProviders)
			{
				if (!dictionary.ContainsKey(itemRelationshipProvider.relationshipType))
				{
					dictionary.Add(itemRelationshipProvider.relationshipType, new HashSet<ItemDef.Pair>());
				}
				dictionary[itemRelationshipProvider.relationshipType].UnionWith(itemRelationshipProvider.relationships);
			}
			ItemCatalog.itemRelationships.Clear();
			foreach (KeyValuePair<ItemRelationshipType, HashSet<ItemDef.Pair>> keyValuePair in dictionary)
			{
				IEnumerable<ItemDef.Pair> enumerable = from pair in keyValuePair.Value
				where !pair.itemDef1 || !pair.itemDef2
				select pair;
				foreach (ItemDef.Pair pair2 in enumerable)
				{
					string[] array = new string[7];
					array[0] = "Trying to define a ";
					array[1] = keyValuePair.Key.name;
					array[2] = " relationship between ";
					int num = 3;
					ItemDef itemDef = pair2.itemDef1;
					array[num] = ((itemDef != null) ? itemDef.name : null);
					array[4] = " and ";
					int num2 = 5;
					ItemDef itemDef2 = pair2.itemDef2;
					array[num2] = ((itemDef2 != null) ? itemDef2.name : null);
					array[6] = ".";
					Debug.LogError(string.Concat(array));
				}
				keyValuePair.Value.ExceptWith(enumerable);
				ItemCatalog.itemRelationships.Add(keyValuePair.Key, keyValuePair.Value.ToArray<ItemDef.Pair>());
			}
		}

		// Token: 0x0600357A RID: 13690 RVA: 0x000E21A4 File Offset: 0x000E03A4
		public static ItemIndex[] RequestItemOrderBuffer()
		{
			if (ItemCatalog.itemOrderBuffers.Count > 0)
			{
				return ItemCatalog.itemOrderBuffers.Pop();
			}
			return new ItemIndex[ItemCatalog.itemCount];
		}

		// Token: 0x0600357B RID: 13691 RVA: 0x000E21C8 File Offset: 0x000E03C8
		public static void ReturnItemOrderBuffer(ItemIndex[] buffer)
		{
			ItemCatalog.itemOrderBuffers.Push(buffer);
		}

		// Token: 0x0600357C RID: 13692 RVA: 0x000E21D5 File Offset: 0x000E03D5
		public static int[] RequestItemStackArray()
		{
			if (ItemCatalog.itemStackArrays.Count > 0)
			{
				return ItemCatalog.itemStackArrays.Pop();
			}
			return new int[ItemCatalog.itemCount];
		}

		// Token: 0x0600357D RID: 13693 RVA: 0x000E21F9 File Offset: 0x000E03F9
		public static void ReturnItemStackArray(int[] itemStackArray)
		{
			if (itemStackArray.Length != ItemCatalog.itemCount)
			{
				return;
			}
			Array.Clear(itemStackArray, 0, itemStackArray.Length);
			ItemCatalog.itemStackArrays.Push(itemStackArray);
		}

		// Token: 0x0600357E RID: 13694 RVA: 0x000E221B File Offset: 0x000E041B
		public static ItemDef GetItemDef(ItemIndex itemIndex)
		{
			return ArrayUtils.GetSafe<ItemDef>(ItemCatalog.itemDefs, (int)itemIndex);
		}

		// Token: 0x0600357F RID: 13695 RVA: 0x000E2228 File Offset: 0x000E0428
		public static ItemIndex FindItemIndex(string itemName)
		{
			ItemIndex result;
			if (ItemCatalog.itemNameToIndex.TryGetValue(itemName, out result))
			{
				return result;
			}
			return ItemIndex.None;
		}

		// Token: 0x06003580 RID: 13696 RVA: 0x000E2247 File Offset: 0x000E0447
		public static T[] GetPerItemBuffer<T>()
		{
			return new T[ItemCatalog.itemCount];
		}

		// Token: 0x06003581 RID: 13697 RVA: 0x000E2253 File Offset: 0x000E0453
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool IsIndexValid(in ItemIndex itemIndex)
		{
			return itemIndex < (ItemIndex)ItemCatalog.itemCount;
		}

		// Token: 0x06003582 RID: 13698 RVA: 0x000E2260 File Offset: 0x000E0460
		public static ReadOnlyArray<ItemIndex> GetItemsWithTag(ItemTag itemTag)
		{
			ItemIndex[][] array = ItemCatalog.itemIndicesByTag;
			ItemIndex[] array2 = Array.Empty<ItemIndex>();
			return ArrayUtils.GetSafe<ItemIndex[]>(array, (int)itemTag, array2);
		}

		// Token: 0x06003583 RID: 13699 RVA: 0x000E2285 File Offset: 0x000E0485
		public static ReadOnlyArray<ItemDef.Pair> GetItemPairsForRelationship(ItemRelationshipType relationshipType)
		{
			if (ItemCatalog.itemRelationships.ContainsKey(relationshipType))
			{
				return ItemCatalog.itemRelationships[relationshipType];
			}
			return Array.Empty<ItemDef.Pair>();
		}

		// Token: 0x170004F0 RID: 1264
		// (get) Token: 0x06003584 RID: 13700 RVA: 0x000E22AF File Offset: 0x000E04AF
		public static ReadOnlyArray<ItemDef> allItemDefs
		{
			get
			{
				return ItemCatalog.itemDefs;
			}
		}

		// Token: 0x06003585 RID: 13701 RVA: 0x000E22BC File Offset: 0x000E04BC
		[ConCommand(commandName = "item_list", flags = ConVarFlags.None, helpText = "Lists internal names of all items registered to the item catalog.")]
		private static void CCEquipmentList(ConCommandArgs args)
		{
			StringBuilder stringBuilder = HG.StringBuilderPool.RentStringBuilder();
			foreach (ItemDef itemDef in ItemCatalog.itemDefs)
			{
				string colorHexString = ColorCatalog.GetColorHexString(itemDef.colorIndex);
				stringBuilder.AppendLine(string.Concat(new string[]
				{
					"<color=#",
					colorHexString,
					">",
					itemDef.name,
					"  (",
					Language.GetString(itemDef.nameToken),
					")</color>"
				}));
			}
			args.Log(stringBuilder.ToString());
			HG.StringBuilderPool.ReturnStringBuilder(stringBuilder);
		}

		// Token: 0x140000B5 RID: 181
		// (add) Token: 0x06003586 RID: 13702 RVA: 0x000E2356 File Offset: 0x000E0556
		// (remove) Token: 0x06003587 RID: 13703 RVA: 0x000026ED File Offset: 0x000008ED
		[Obsolete("Use IContentPackProvider instead.")]
		public static event Action<List<ItemDef>> getAdditionalEntries
		{
			add
			{
				LegacyModContentPackProvider.instance.HandleLegacyGetAdditionalEntries<ItemDef>("RoR2.ItemCatalog.getAdditionalEntries", value, LegacyModContentPackProvider.instance.registrationContentPack.itemDefs);
			}
			remove
			{
			}
		}

		// Token: 0x06003588 RID: 13704 RVA: 0x000026ED File Offset: 0x000008ED
		private static void DefineItems()
		{
		}

		// Token: 0x0400364B RID: 13899
		public static List<ItemIndex> tier1ItemList = new List<ItemIndex>();

		// Token: 0x0400364C RID: 13900
		public static List<ItemIndex> tier2ItemList = new List<ItemIndex>();

		// Token: 0x0400364D RID: 13901
		public static List<ItemIndex> tier3ItemList = new List<ItemIndex>();

		// Token: 0x0400364E RID: 13902
		public static List<ItemIndex> lunarItemList = new List<ItemIndex>();

		// Token: 0x0400364F RID: 13903
		private static ItemDef[] itemDefs = Array.Empty<ItemDef>();

		// Token: 0x04003650 RID: 13904
		public static ResourceAvailability availability = default(ResourceAvailability);

		// Token: 0x04003651 RID: 13905
		public static string[] itemNames = Array.Empty<string>();

		// Token: 0x04003652 RID: 13906
		private static readonly Dictionary<string, ItemIndex> itemNameToIndex = new Dictionary<string, ItemIndex>();

		// Token: 0x04003653 RID: 13907
		private static ItemIndex[][] itemIndicesByTag = Array.Empty<ItemIndex[]>();

		// Token: 0x04003654 RID: 13908
		private static Dictionary<ItemRelationshipType, ItemDef.Pair[]> itemRelationships = new Dictionary<ItemRelationshipType, ItemDef.Pair[]>();

		// Token: 0x04003655 RID: 13909
		private static readonly Stack<ItemIndex[]> itemOrderBuffers = new Stack<ItemIndex[]>();

		// Token: 0x04003656 RID: 13910
		private static readonly Stack<int[]> itemStackArrays = new Stack<int[]>();

		// Token: 0x04003657 RID: 13911
		public static readonly GenericStaticEnumerable<ItemIndex, ItemCatalog.AllItemsEnumerator> allItems;

		// Token: 0x0200093F RID: 2367
		public struct AllItemsEnumerator : IEnumerator<ItemIndex>, IEnumerator, IDisposable
		{
			// Token: 0x0600358A RID: 13706 RVA: 0x000E23FE File Offset: 0x000E05FE
			public bool MoveNext()
			{
				this.position++;
				return this.position < (ItemIndex)ItemCatalog.itemCount;
			}

			// Token: 0x0600358B RID: 13707 RVA: 0x000E241B File Offset: 0x000E061B
			public void Reset()
			{
				this.position = ItemIndex.None;
			}

			// Token: 0x170004F1 RID: 1265
			// (get) Token: 0x0600358C RID: 13708 RVA: 0x000E2424 File Offset: 0x000E0624
			public ItemIndex Current
			{
				get
				{
					return this.position;
				}
			}

			// Token: 0x170004F2 RID: 1266
			// (get) Token: 0x0600358D RID: 13709 RVA: 0x000E242C File Offset: 0x000E062C
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x0600358E RID: 13710 RVA: 0x000026ED File Offset: 0x000008ED
			void IDisposable.Dispose()
			{
			}

			// Token: 0x04003658 RID: 13912
			private ItemIndex position;
		}
	}
}
