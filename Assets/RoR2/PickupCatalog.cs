using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using HG;
using JetBrains.Annotations;
using UnityEngine;

namespace RoR2
{
	// Token: 0x02000987 RID: 2439
	public static class PickupCatalog
	{
		// Token: 0x17000530 RID: 1328
		// (get) Token: 0x0600375A RID: 14170 RVA: 0x000E999A File Offset: 0x000E7B9A
		// (set) Token: 0x0600375B RID: 14171 RVA: 0x000E99A1 File Offset: 0x000E7BA1
		public static int pickupCount { get; private set; }

		// Token: 0x17000531 RID: 1329
		// (get) Token: 0x0600375C RID: 14172 RVA: 0x000E99AC File Offset: 0x000E7BAC
		public static GenericStaticEnumerable<PickupIndex, PickupCatalog.Enumerator> allPickupIndices
		{
			get
			{
				return default(GenericStaticEnumerable<PickupIndex, PickupCatalog.Enumerator>);
			}
		}

		// Token: 0x17000532 RID: 1330
		// (get) Token: 0x0600375D RID: 14173 RVA: 0x000E99C2 File Offset: 0x000E7BC2
		public static IEnumerable<PickupDef> allPickups
		{
			get
			{
				return PickupCatalog.entries;
			}
		}

		// Token: 0x0600375E RID: 14174 RVA: 0x000E99C9 File Offset: 0x000E7BC9
		[NotNull]
		public static T[] GetPerPickupBuffer<T>()
		{
			return new T[PickupCatalog.pickupCount];
		}

		// Token: 0x0600375F RID: 14175 RVA: 0x000E99D8 File Offset: 0x000E7BD8
		public static void SetEntries([NotNull] PickupDef[] pickupDefs)
		{
			Array.Resize<PickupDef>(ref PickupCatalog.entries, pickupDefs.Length);
			PickupCatalog.pickupCount = pickupDefs.Length;
			Array.Copy(pickupDefs, PickupCatalog.entries, PickupCatalog.entries.Length);
			Array.Resize<PickupIndex>(ref PickupCatalog.itemIndexToPickupIndex, ItemCatalog.itemCount);
			Array.Resize<PickupIndex>(ref PickupCatalog.equipmentIndexToPickupIndex, EquipmentCatalog.equipmentCount);
			Array.Resize<PickupIndex>(ref PickupCatalog.artifactIndexToPickupIndex, ArtifactCatalog.artifactCount);
			Array.Resize<PickupIndex>(ref PickupCatalog.miscPickupIndexToPickupIndex, MiscPickupCatalog.pickupCount);
			PickupCatalog.nameToPickupIndex.Clear();
			PickupCatalog.itemTierToPickupIndex.Clear();
			for (int i = 0; i < PickupCatalog.entries.Length; i++)
			{
				PickupDef pickupDef = PickupCatalog.entries[i];
				PickupIndex pickupIndex = new PickupIndex(i);
				pickupDef.pickupIndex = pickupIndex;
				if (pickupDef.itemIndex != ItemIndex.None)
				{
					PickupCatalog.itemIndexToPickupIndex[(int)pickupDef.itemIndex] = pickupIndex;
				}
				else if (pickupDef.itemTier != ItemTier.NoTier)
				{
					PickupCatalog.itemTierToPickupIndex.Add(pickupDef.itemTier, pickupDef.pickupIndex);
				}
				if (pickupDef.equipmentIndex != EquipmentIndex.None)
				{
					PickupCatalog.equipmentIndexToPickupIndex[(int)pickupDef.equipmentIndex] = pickupIndex;
				}
				if (pickupDef.artifactIndex != ArtifactIndex.None)
				{
					PickupCatalog.artifactIndexToPickupIndex[(int)pickupDef.artifactIndex] = pickupIndex;
				}
				if (pickupDef.miscPickupIndex != MiscPickupIndex.None)
				{
					PickupCatalog.miscPickupIndexToPickupIndex[(int)pickupDef.miscPickupIndex] = pickupIndex;
				}
			}
			for (int j = 0; j < PickupCatalog.entries.Length; j++)
			{
				PickupDef pickupDef2 = PickupCatalog.entries[j];
				PickupCatalog.nameToPickupIndex[pickupDef2.internalName] = pickupDef2.pickupIndex;
			}
		}

		// Token: 0x06003760 RID: 14176 RVA: 0x000E9B48 File Offset: 0x000E7D48
		[SystemInitializer(new Type[]
		{
			typeof(ItemCatalog),
			typeof(EquipmentCatalog),
			typeof(ArtifactCatalog),
			typeof(MiscPickupCatalog)
		})]
		private static void Init()
		{
			List<PickupDef> list = new List<PickupDef>();
			foreach (ItemTierDef itemTierDef in ItemTierCatalog.allItemTierDefs)
			{
				list.Add(new PickupDef
				{
					internalName = "ItemTier." + itemTierDef.tier,
					itemTier = itemTierDef.tier,
					dropletDisplayPrefab = ((itemTierDef != null) ? itemTierDef.dropletDisplayPrefab : null),
					baseColor = ColorCatalog.GetColor(itemTierDef.colorIndex),
					darkColor = ColorCatalog.GetColor(itemTierDef.darkColorIndex),
					interactContextToken = "ITEM_PICKUP_CONTEXT",
					isLunar = (itemTierDef.tier == ItemTier.Lunar),
					isBoss = (itemTierDef.tier == ItemTier.Boss)
				});
			}
			for (int i = 0; i < ItemCatalog.itemCount; i++)
			{
				PickupDef item = ItemCatalog.GetItemDef((ItemIndex)i).CreatePickupDef();
				list.Add(item);
			}
			for (int j = 0; j < EquipmentCatalog.equipmentCount; j++)
			{
				PickupDef item2 = EquipmentCatalog.GetEquipmentDef((EquipmentIndex)j).CreatePickupDef();
				list.Add(item2);
			}
			for (int k = 0; k < MiscPickupCatalog.pickupCount; k++)
			{
				PickupDef item3 = MiscPickupCatalog.miscPickupDefs[k].CreatePickupDef();
				list.Add(item3);
			}
			for (int l = 0; l < ArtifactCatalog.artifactCount; l++)
			{
				PickupDef item4 = ArtifactCatalog.GetArtifactDef((ArtifactIndex)l).CreatePickupDef();
				list.Add(item4);
			}
			Action<List<PickupDef>> action = PickupCatalog.modifyPickups;
			if (action != null)
			{
				action(list);
			}
			PickupCatalog.SetEntries(list.ToArray());
		}

		// Token: 0x06003761 RID: 14177 RVA: 0x000E9D10 File Offset: 0x000E7F10
		public static PickupIndex FindPickupIndex([NotNull] string pickupName)
		{
			PickupIndex result;
			if (PickupCatalog.nameToPickupIndex.TryGetValue(pickupName, out result))
			{
				return result;
			}
			return PickupIndex.none;
		}

		// Token: 0x06003762 RID: 14178 RVA: 0x000E9D33 File Offset: 0x000E7F33
		public static PickupIndex FindPickupIndex(ItemIndex itemIndex)
		{
			return ArrayUtils.GetSafe<PickupIndex>(PickupCatalog.itemIndexToPickupIndex, (int)itemIndex, PickupIndex.none);
		}

		// Token: 0x06003763 RID: 14179 RVA: 0x000E9D48 File Offset: 0x000E7F48
		public static PickupIndex FindPickupIndex(ItemTier tier)
		{
			PickupIndex result;
			if (PickupCatalog.itemTierToPickupIndex.TryGetValue(tier, out result))
			{
				return result;
			}
			return PickupIndex.none;
		}

		// Token: 0x06003764 RID: 14180 RVA: 0x000E9D6B File Offset: 0x000E7F6B
		public static PickupIndex FindPickupIndex(EquipmentIndex equipmentIndex)
		{
			return ArrayUtils.GetSafe<PickupIndex>(PickupCatalog.equipmentIndexToPickupIndex, (int)equipmentIndex, PickupIndex.none);
		}

		// Token: 0x06003765 RID: 14181 RVA: 0x000E9D7D File Offset: 0x000E7F7D
		public static PickupIndex FindPickupIndex(ArtifactIndex artifactIndex)
		{
			return ArrayUtils.GetSafe<PickupIndex>(PickupCatalog.artifactIndexToPickupIndex, (int)artifactIndex, PickupIndex.none);
		}

		// Token: 0x06003766 RID: 14182 RVA: 0x000E9D8F File Offset: 0x000E7F8F
		public static PickupIndex FindPickupIndex(MiscPickupIndex miscIndex)
		{
			return ArrayUtils.GetSafe<PickupIndex>(PickupCatalog.miscPickupIndexToPickupIndex, (int)miscIndex, PickupIndex.none);
		}

		// Token: 0x06003767 RID: 14183 RVA: 0x000E9DA1 File Offset: 0x000E7FA1
		[CanBeNull]
		public static PickupDef GetPickupDef(PickupIndex pickupIndex)
		{
			return ArrayUtils.GetSafe<PickupDef>(PickupCatalog.entries, pickupIndex.value);
		}

		// Token: 0x06003768 RID: 14184 RVA: 0x000E9DB3 File Offset: 0x000E7FB3
		[NotNull]
		public static GameObject GetHiddenPickupDisplayPrefab()
		{
			return LegacyResourcesAPI.Load<GameObject>("Prefabs/PickupModels/PickupMystery");
		}

		// Token: 0x06003769 RID: 14185 RVA: 0x000E9DC0 File Offset: 0x000E7FC0
		[ConCommand(commandName = "pickup_print_all", flags = ConVarFlags.None, helpText = "Prints all pickup definitions.")]
		private static void CCPickupPrintAll(ConCommandArgs args)
		{
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < PickupCatalog.pickupCount; i++)
			{
				PickupDef pickupDef = PickupCatalog.GetPickupDef(new PickupIndex(i));
				stringBuilder.Append("[").Append(i).Append("]={internalName=").Append(pickupDef.internalName).Append("}").AppendLine();
			}
			Debug.Log(stringBuilder.ToString());
		}

		// Token: 0x040037B5 RID: 14261
		private static PickupDef[] entries = Array.Empty<PickupDef>();

		// Token: 0x040037B7 RID: 14263
		private static PickupIndex[] itemIndexToPickupIndex = Array.Empty<PickupIndex>();

		// Token: 0x040037B8 RID: 14264
		private static PickupIndex[] equipmentIndexToPickupIndex = Array.Empty<PickupIndex>();

		// Token: 0x040037B9 RID: 14265
		private static PickupIndex[] artifactIndexToPickupIndex = Array.Empty<PickupIndex>();

		// Token: 0x040037BA RID: 14266
		private static PickupIndex[] miscPickupIndexToPickupIndex = Array.Empty<PickupIndex>();

		// Token: 0x040037BB RID: 14267
		private static readonly Dictionary<string, PickupIndex> nameToPickupIndex = new Dictionary<string, PickupIndex>();

		// Token: 0x040037BC RID: 14268
		private static readonly Dictionary<ItemTier, PickupIndex> itemTierToPickupIndex = new Dictionary<ItemTier, PickupIndex>();

		// Token: 0x040037BD RID: 14269
		public static readonly Color invalidPickupColor = Color.black;

		// Token: 0x040037BE RID: 14270
		public static readonly string invalidPickupToken = "???";

		// Token: 0x040037BF RID: 14271
		public static Action<List<PickupDef>> modifyPickups;

		// Token: 0x02000988 RID: 2440
		public struct Enumerator : IEnumerator<PickupIndex>, IEnumerator, IDisposable
		{
			// Token: 0x0600376B RID: 14187 RVA: 0x000E9E97 File Offset: 0x000E8097
			public bool MoveNext()
			{
				this.position = ++this.position;
				return this.position.value < PickupCatalog.pickupCount;
			}

			// Token: 0x0600376C RID: 14188 RVA: 0x000E9EBC File Offset: 0x000E80BC
			public void Reset()
			{
				this.position = PickupIndex.none;
			}

			// Token: 0x17000533 RID: 1331
			// (get) Token: 0x0600376D RID: 14189 RVA: 0x000E9EC9 File Offset: 0x000E80C9
			public PickupIndex Current
			{
				get
				{
					return this.position;
				}
			}

			// Token: 0x17000534 RID: 1332
			// (get) Token: 0x0600376E RID: 14190 RVA: 0x000E9ED1 File Offset: 0x000E80D1
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x0600376F RID: 14191 RVA: 0x000026ED File Offset: 0x000008ED
			void IDisposable.Dispose()
			{
			}

			// Token: 0x040037C0 RID: 14272
			private PickupIndex position;
		}
	}
}
