using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using HG;
using JetBrains.Annotations;
using RoR2.ContentManagement;
using RoR2.ExpansionManagement;
using RoR2.Skills;
using UnityEngine;

namespace RoR2
{
	// Token: 0x02000A94 RID: 2708
	public static class UnlockableCatalog
	{
		// Token: 0x06003E14 RID: 15892 RVA: 0x000FFE80 File Offset: 0x000FE080
		[CanBeNull]
		public static UnlockableDef GetUnlockableDef([NotNull] string name)
		{
			if (name == null)
			{
				return null;
			}
			UnlockableDef result;
			UnlockableCatalog.nameToDefTable.TryGetValue(name, out result);
			return result;
		}

		// Token: 0x06003E15 RID: 15893 RVA: 0x000FFEA1 File Offset: 0x000FE0A1
		[CanBeNull]
		public static UnlockableDef GetUnlockableDef(UnlockableIndex index)
		{
			return ArrayUtils.GetSafe<UnlockableDef>(UnlockableCatalog.indexToDefTable, (int)index);
		}

		// Token: 0x06003E16 RID: 15894 RVA: 0x000FFEB0 File Offset: 0x000FE0B0
		[CanBeNull]
		public static ExpansionDef GetExpansionDefForUnlockable(UnlockableIndex index)
		{
			ExpansionDef result;
			UnlockableCatalog.unlockableToExpansionTable.TryGetValue(index, out result);
			return result;
		}

		// Token: 0x170005D0 RID: 1488
		// (get) Token: 0x06003E17 RID: 15895 RVA: 0x000FFECC File Offset: 0x000FE0CC
		public static int unlockableCount
		{
			get
			{
				return UnlockableCatalog.indexToDefTable.Length;
			}
		}

		// Token: 0x06003E18 RID: 15896 RVA: 0x000FFED5 File Offset: 0x000FE0D5
		[SystemInitializer(new Type[]
		{
			typeof(SurvivorCatalog),
			typeof(ItemCatalog),
			typeof(EquipmentCatalog),
			typeof(ArtifactCatalog),
			typeof(BodyCatalog)
		})]
		private static void Init()
		{
			UnlockableCatalog.SetUnlockableDefs(ContentManager.unlockableDefs);
		}

		// Token: 0x06003E19 RID: 15897 RVA: 0x000FFEE4 File Offset: 0x000FE0E4
		private static void SetUnlockableDefs(UnlockableDef[] newUnlockableDefs)
		{
			UnlockableCatalog.nameToDefTable.Clear();
			UnlockableCatalog.indexToDefTable = ArrayUtils.Clone<UnlockableDef>(newUnlockableDefs);
			for (UnlockableIndex unlockableIndex = (UnlockableIndex)0; unlockableIndex < (UnlockableIndex)UnlockableCatalog.indexToDefTable.Length; unlockableIndex++)
			{
				UnlockableDef unlockableDef = UnlockableCatalog.indexToDefTable[(int)unlockableIndex];
				unlockableDef.index = unlockableIndex;
				UnlockableCatalog.nameToDefTable[unlockableDef.cachedName] = unlockableDef;
			}
			UnlockableCatalog.GenerateUnlockableMetaData(UnlockableCatalog.indexToDefTable);
		}

		// Token: 0x06003E1A RID: 15898 RVA: 0x000FFF44 File Offset: 0x000FE144
		private static void TryAddExpansionMapping(UnlockableDef unlockable, ExpansionDef expansion)
		{
			if (unlockable && (!UnlockableCatalog.unlockableToExpansionTable.ContainsKey(unlockable.index) || !UnlockableCatalog.unlockableToExpansionTable[unlockable.index]))
			{
				UnlockableCatalog.unlockableToExpansionTable[unlockable.index] = expansion;
			}
		}

		// Token: 0x06003E1B RID: 15899 RVA: 0x000FFF94 File Offset: 0x000FE194
		private static void GenerateUnlockableMetaData(UnlockableDef[] unlockableDefs)
		{
			UnlockableCatalog.unlockableToExpansionTable.Clear();
			for (int i = 0; i < unlockableDefs.Length; i++)
			{
				unlockableDefs[i].sortScore = int.MaxValue;
			}
			UnlockableCatalog.<>c__DisplayClass12_0 CS$<>8__locals1;
			CS$<>8__locals1.unlockableDefsSet = new HashSet<UnlockableDef>();
			foreach (UnlockableDef item in unlockableDefs)
			{
				CS$<>8__locals1.unlockableDefsSet.Add(item);
			}
			CS$<>8__locals1.topSortValue = 0;
			foreach (ItemDef itemDef in ItemCatalog.allItems.Select(new Func<ItemIndex, ItemDef>(ItemCatalog.GetItemDef)).OrderBy(new Func<ItemDef, int>(UnlockableCatalog.<>c.<>9.<GenerateUnlockableMetaData>g__GetItemSortScore|12_0)))
			{
				UnlockableCatalog.<GenerateUnlockableMetaData>g__AssignUnlockableSortScore|12_2(itemDef.unlockableDef, ref CS$<>8__locals1);
				UnlockableCatalog.TryAddExpansionMapping(itemDef.unlockableDef, itemDef.requiredExpansion);
			}
			foreach (EquipmentDef equipmentDef in EquipmentCatalog.allEquipment.Select(new Func<EquipmentIndex, EquipmentDef>(EquipmentCatalog.GetEquipmentDef)).OrderBy(new Func<EquipmentDef, int>(UnlockableCatalog.<>c.<>9.<GenerateUnlockableMetaData>g__GetEquipmentSortScore|12_1)))
			{
				UnlockableCatalog.<GenerateUnlockableMetaData>g__AssignUnlockableSortScore|12_2(equipmentDef.unlockableDef, ref CS$<>8__locals1);
				UnlockableCatalog.TryAddExpansionMapping(equipmentDef.unlockableDef, equipmentDef.requiredExpansion);
			}
			foreach (SurvivorDef survivorDef in SurvivorCatalog.orderedSurvivorDefs)
			{
				UnlockableCatalog.<GenerateUnlockableMetaData>g__AssignUnlockableSortScore|12_2(survivorDef.unlockableDef, ref CS$<>8__locals1);
				BodyIndex bodyIndex = BodyCatalog.FindBodyIndex(survivorDef.bodyPrefab);
				if (bodyIndex != BodyIndex.None)
				{
					ExpansionDef expansion = null;
					GameObject bodyPrefab = BodyCatalog.GetBodyPrefab(bodyIndex);
					if (bodyPrefab)
					{
						ExpansionRequirementComponent component = bodyPrefab.GetComponent<ExpansionRequirementComponent>();
						if (component)
						{
							expansion = component.requiredExpansion;
						}
					}
					UnlockableCatalog.TryAddExpansionMapping(survivorDef.unlockableDef, expansion);
					GenericSkill[] bodyPrefabSkillSlots = BodyCatalog.GetBodyPrefabSkillSlots(bodyIndex);
					for (int i = 0; i < bodyPrefabSkillSlots.Length; i++)
					{
						SkillFamily skillFamily = bodyPrefabSkillSlots[i].skillFamily;
						if (skillFamily)
						{
							foreach (SkillFamily.Variant variant in skillFamily.variants)
							{
								UnlockableCatalog.<GenerateUnlockableMetaData>g__AssignUnlockableSortScore|12_2(variant.unlockableDef, ref CS$<>8__locals1);
								UnlockableCatalog.TryAddExpansionMapping(variant.unlockableDef, expansion);
							}
						}
					}
					foreach (SkinDef skinDef in BodyCatalog.GetBodySkins(bodyIndex))
					{
						UnlockableCatalog.<GenerateUnlockableMetaData>g__AssignUnlockableSortScore|12_2(skinDef.unlockableDef, ref CS$<>8__locals1);
						UnlockableCatalog.TryAddExpansionMapping(skinDef.unlockableDef, expansion);
					}
				}
			}
			for (ArtifactIndex artifactIndex = (ArtifactIndex)0; artifactIndex < (ArtifactIndex)ArtifactCatalog.artifactCount; artifactIndex++)
			{
				ArtifactDef artifactDef = ArtifactCatalog.GetArtifactDef(artifactIndex);
				UnlockableCatalog.<GenerateUnlockableMetaData>g__AssignUnlockableSortScore|12_2(artifactDef.unlockableDef, ref CS$<>8__locals1);
				UnlockableCatalog.TryAddExpansionMapping(artifactDef.unlockableDef, artifactDef.requiredExpansion);
			}
		}

		// Token: 0x06003E1C RID: 15900 RVA: 0x001002B0 File Offset: 0x000FE4B0
		public static int GetUnlockableSortScore(string unlockableName)
		{
			UnlockableDef unlockableDef = UnlockableCatalog.GetUnlockableDef(unlockableName);
			if (unlockableDef == null)
			{
				return 0;
			}
			return unlockableDef.sortScore;
		}

		// Token: 0x06003E1E RID: 15902 RVA: 0x001002DC File Offset: 0x000FE4DC
		[CompilerGenerated]
		internal static void <GenerateUnlockableMetaData>g__AssignUnlockableSortScore|12_2(UnlockableDef unlockableDef, ref UnlockableCatalog.<>c__DisplayClass12_0 A_1)
		{
			if (A_1.unlockableDefsSet.Contains(unlockableDef) && unlockableDef.sortScore == 2147483647)
			{
				int num = A_1.topSortValue + 1;
				A_1.topSortValue = num;
				unlockableDef.sortScore = num;
			}
		}

		// Token: 0x04003CC2 RID: 15554
		private static readonly Dictionary<UnlockableIndex, ExpansionDef> unlockableToExpansionTable = new Dictionary<UnlockableIndex, ExpansionDef>();

		// Token: 0x04003CC3 RID: 15555
		private static readonly Dictionary<string, UnlockableDef> nameToDefTable = new Dictionary<string, UnlockableDef>();

		// Token: 0x04003CC4 RID: 15556
		private static UnlockableDef[] indexToDefTable;

		// Token: 0x04003CC5 RID: 15557
		public static ResourceAvailability availability;
	}
}
