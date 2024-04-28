using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using HG;
using JetBrains.Annotations;

namespace RoR2
{
	// Token: 0x02000990 RID: 2448
	public static class PickupTransmutationManager
	{
		// Token: 0x060037AD RID: 14253 RVA: 0x000EA3DB File Offset: 0x000E85DB
		[SystemInitializer(new Type[]
		{
			typeof(PickupCatalog)
		})]
		private static void Init()
		{
			PickupTransmutationManager.RebuildPickupGroups();
			Run.onRunStartGlobal += PickupTransmutationManager.RebuildAvailablePickupGroups;
		}

		// Token: 0x060037AE RID: 14254 RVA: 0x000EA3F4 File Offset: 0x000E85F4
		private static void RebuildPickupGroups()
		{
			PickupTransmutationManager.pickupGroupMap = new PickupIndex[PickupCatalog.pickupCount][];
			PickupTransmutationManager.<>c__DisplayClass17_0 CS$<>8__locals1;
			CS$<>8__locals1.groups = new List<PickupIndex[]>();
			PickupTransmutationManager.itemTier1Group = PickupTransmutationManager.<RebuildPickupGroups>g__AddItemTierGroup|17_1(ItemTier.Tier1, ref CS$<>8__locals1);
			PickupTransmutationManager.itemTier2Group = PickupTransmutationManager.<RebuildPickupGroups>g__AddItemTierGroup|17_1(ItemTier.Tier2, ref CS$<>8__locals1);
			PickupTransmutationManager.itemTier3Group = PickupTransmutationManager.<RebuildPickupGroups>g__AddItemTierGroup|17_1(ItemTier.Tier3, ref CS$<>8__locals1);
			PickupTransmutationManager.itemTierBossGroup = PickupTransmutationManager.<RebuildPickupGroups>g__AddItemTierGroup|17_1(ItemTier.Boss, ref CS$<>8__locals1);
			PickupTransmutationManager.itemTierLunarGroup = PickupTransmutationManager.<RebuildPickupGroups>g__AddItemTierGroup|17_1(ItemTier.Lunar, ref CS$<>8__locals1);
			PickupTransmutationManager.equipmentNormalGroup = PickupTransmutationManager.<RebuildPickupGroups>g__AddGroup|17_0(PickupTransmutationManager.<RebuildPickupGroups>g__GetEquipmentGroup|17_3(false, false), ref CS$<>8__locals1);
			PickupTransmutationManager.equipmentLunarGroup = PickupTransmutationManager.<RebuildPickupGroups>g__AddGroup|17_0(PickupTransmutationManager.<RebuildPickupGroups>g__GetEquipmentGroup|17_3(false, true), ref CS$<>8__locals1);
			PickupTransmutationManager.equipmentBossGroup = PickupTransmutationManager.<RebuildPickupGroups>g__AddGroup|17_0(PickupTransmutationManager.<RebuildPickupGroups>g__GetEquipmentGroup|17_3(true, false), ref CS$<>8__locals1);
			PickupTransmutationManager.itemVoidTier1Group = PickupTransmutationManager.<RebuildPickupGroups>g__AddItemTierGroup|17_1(ItemTier.VoidTier1, ref CS$<>8__locals1);
			PickupTransmutationManager.itemVoidTier2Group = PickupTransmutationManager.<RebuildPickupGroups>g__AddItemTierGroup|17_1(ItemTier.VoidTier2, ref CS$<>8__locals1);
			PickupTransmutationManager.itemVoidTier3Group = PickupTransmutationManager.<RebuildPickupGroups>g__AddItemTierGroup|17_1(ItemTier.VoidTier3, ref CS$<>8__locals1);
			PickupTransmutationManager.itemVoidBossGroup = PickupTransmutationManager.<RebuildPickupGroups>g__AddItemTierGroup|17_1(ItemTier.VoidBoss, ref CS$<>8__locals1);
			PickupTransmutationManager.pickupGroups = CS$<>8__locals1.groups.ToArray();
		}

		// Token: 0x060037AF RID: 14255 RVA: 0x000EA4DC File Offset: 0x000E86DC
		private static void RebuildAvailablePickupGroups([NotNull] Run source)
		{
			Array.Resize<PickupIndex[]>(ref PickupTransmutationManager.availablePickupGroups, PickupTransmutationManager.pickupGroups.Length);
			Array.Resize<PickupIndex[]>(ref PickupTransmutationManager.availablePickupGroupMap, PickupTransmutationManager.pickupGroupMap.Length);
			Func<PickupIndex, bool> predicate = new Func<PickupIndex, bool>(source.IsPickupAvailable);
			for (int i = 0; i < PickupTransmutationManager.availablePickupGroups.Length; i++)
			{
				PickupIndex[] array = PickupTransmutationManager.pickupGroups[i];
				PickupIndex[] array2 = array.Where(predicate).ToArray<PickupIndex>();
				PickupTransmutationManager.availablePickupGroups[i] = array2;
				for (int j = 0; j < array.Length; j++)
				{
					PickupTransmutationManager.availablePickupGroupMap[array[j].value] = array2;
				}
			}
		}

		// Token: 0x060037B0 RID: 14256 RVA: 0x000EA56D File Offset: 0x000E876D
		[CanBeNull]
		public static PickupIndex[] GetGroupFromPickupIndex(PickupIndex pickupIndex)
		{
			return ArrayUtils.GetSafe<PickupIndex[]>(PickupTransmutationManager.pickupGroupMap, pickupIndex.value);
		}

		// Token: 0x060037B1 RID: 14257 RVA: 0x000EA57F File Offset: 0x000E877F
		[CanBeNull]
		public static PickupIndex[] GetAvailableGroupFromPickupIndex(PickupIndex pickupIndex)
		{
			return ArrayUtils.GetSafe<PickupIndex[]>(PickupTransmutationManager.availablePickupGroupMap, pickupIndex.value);
		}

		// Token: 0x060037B3 RID: 14259 RVA: 0x000EA5BC File Offset: 0x000E87BC
		[CompilerGenerated]
		internal static PickupIndex[] <RebuildPickupGroups>g__AddGroup|17_0(PickupIndex[] group, ref PickupTransmutationManager.<>c__DisplayClass17_0 A_1)
		{
			A_1.groups.Add(group);
			foreach (PickupIndex pickupIndex in group)
			{
				PickupTransmutationManager.pickupGroupMap[pickupIndex.value] = group;
			}
			return group;
		}

		// Token: 0x060037B4 RID: 14260 RVA: 0x000EA5FC File Offset: 0x000E87FC
		[CompilerGenerated]
		internal static PickupIndex[] <RebuildPickupGroups>g__AddItemTierGroup|17_1(ItemTier tier, ref PickupTransmutationManager.<>c__DisplayClass17_0 A_1)
		{
			PickupIndex[] array = PickupTransmutationManager.<RebuildPickupGroups>g__GetItemTierGroup|17_2(tier);
			PickupTransmutationManager.<RebuildPickupGroups>g__AddGroup|17_0(array, ref A_1);
			PickupIndex pickupIndex = PickupCatalog.FindPickupIndex(tier);
			if (pickupIndex != PickupIndex.none)
			{
				PickupTransmutationManager.pickupGroupMap[pickupIndex.value] = array;
			}
			return array;
		}

		// Token: 0x060037B5 RID: 14261 RVA: 0x000EA63C File Offset: 0x000E883C
		[CompilerGenerated]
		internal static PickupIndex[] <RebuildPickupGroups>g__GetItemTierGroup|17_2(ItemTier itemTier)
		{
			return (from itemDef in ItemCatalog.allItems.Select(new Func<ItemIndex, ItemDef>(ItemCatalog.GetItemDef))
			where itemDef.tier == itemTier && !itemDef.ContainsTag(ItemTag.WorldUnique)
			select PickupCatalog.FindPickupIndex(itemDef.itemIndex)).ToArray<PickupIndex>();
		}

		// Token: 0x060037B6 RID: 14262 RVA: 0x000EA6AC File Offset: 0x000E88AC
		[CompilerGenerated]
		internal static PickupIndex[] <RebuildPickupGroups>g__GetEquipmentGroup|17_3(bool isBoss, bool isLunar)
		{
			return (from equipmentDef in EquipmentCatalog.allEquipment.Select(new Func<EquipmentIndex, EquipmentDef>(EquipmentCatalog.GetEquipmentDef))
			where equipmentDef.canDrop && equipmentDef.isBoss == isBoss && equipmentDef.isLunar == isLunar
			select PickupCatalog.FindPickupIndex(equipmentDef.equipmentIndex)).ToArray<PickupIndex>();
		}

		// Token: 0x040037DF RID: 14303
		private static PickupIndex[][] pickupGroups = Array.Empty<PickupIndex[]>();

		// Token: 0x040037E0 RID: 14304
		private static PickupIndex[][] pickupGroupMap = Array.Empty<PickupIndex[]>();

		// Token: 0x040037E1 RID: 14305
		private static PickupIndex[] itemTier1Group;

		// Token: 0x040037E2 RID: 14306
		private static PickupIndex[] itemTier2Group;

		// Token: 0x040037E3 RID: 14307
		private static PickupIndex[] itemTier3Group;

		// Token: 0x040037E4 RID: 14308
		private static PickupIndex[] itemTierBossGroup;

		// Token: 0x040037E5 RID: 14309
		private static PickupIndex[] itemTierLunarGroup;

		// Token: 0x040037E6 RID: 14310
		private static PickupIndex[] equipmentNormalGroup;

		// Token: 0x040037E7 RID: 14311
		private static PickupIndex[] equipmentLunarGroup;

		// Token: 0x040037E8 RID: 14312
		private static PickupIndex[] equipmentBossGroup;

		// Token: 0x040037E9 RID: 14313
		private static PickupIndex[] itemVoidTier1Group;

		// Token: 0x040037EA RID: 14314
		private static PickupIndex[] itemVoidTier2Group;

		// Token: 0x040037EB RID: 14315
		private static PickupIndex[] itemVoidTier3Group;

		// Token: 0x040037EC RID: 14316
		private static PickupIndex[] itemVoidBossGroup;

		// Token: 0x040037ED RID: 14317
		private static PickupIndex[][] availablePickupGroups = Array.Empty<PickupIndex[]>();

		// Token: 0x040037EE RID: 14318
		private static PickupIndex[][] availablePickupGroupMap = Array.Empty<PickupIndex[]>();
	}
}
