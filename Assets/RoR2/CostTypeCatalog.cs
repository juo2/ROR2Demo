using System;
using System.Linq;
using System.Runtime.CompilerServices;
using HG;
using RoR2.Items;
using UnityEngine;

namespace RoR2
{
	// Token: 0x0200051B RID: 1307
	public static class CostTypeCatalog
	{
		// Token: 0x1700016E RID: 366
		// (get) Token: 0x060017BF RID: 6079 RVA: 0x00067EF8 File Offset: 0x000660F8
		public static int costTypeCount
		{
			get
			{
				return CostTypeCatalog.costTypeDefs.Length;
			}
		}

		// Token: 0x060017C0 RID: 6080 RVA: 0x00067F01 File Offset: 0x00066101
		private static void Register(CostTypeIndex costType, CostTypeDef costTypeDef)
		{
			if (costType < CostTypeIndex.Count)
			{
				costTypeDef.name = costType.ToString();
			}
			CostTypeCatalog.costTypeDefs[(int)costType] = costTypeDef;
		}

		// Token: 0x060017C1 RID: 6081 RVA: 0x00067F24 File Offset: 0x00066124
		[SystemInitializer(new Type[]
		{

		})]
		private static void Init()
		{
			CostTypeCatalog.costTypeDefs = new CostTypeDef[15];
			CostTypeIndex costType = CostTypeIndex.None;
			CostTypeDef costTypeDef11 = new CostTypeDef();
			costTypeDef11.buildCostString = delegate(CostTypeDef costTypeDef, CostTypeDef.BuildCostStringContext context)
			{
				context.stringBuilder.Append("");
			};
			costTypeDef11.isAffordable = ((CostTypeDef costTypeDef, CostTypeDef.IsAffordableContext context) => true);
			costTypeDef11.payCost = delegate(CostTypeDef costTypeDef, CostTypeDef.PayCostContext context)
			{
				MultiShopCardUtils.OnNonMoneyPurchase(context);
			};
			CostTypeCatalog.Register(costType, costTypeDef11);
			CostTypeIndex costType2 = CostTypeIndex.Money;
			CostTypeDef costTypeDef2 = new CostTypeDef();
			costTypeDef2.costStringFormatToken = "COST_MONEY_FORMAT";
			costTypeDef2.isAffordable = delegate(CostTypeDef costTypeDef, CostTypeDef.IsAffordableContext context)
			{
				CharacterBody component = context.activator.GetComponent<CharacterBody>();
				if (component)
				{
					CharacterMaster master = component.master;
					if (master)
					{
						return (ulong)master.money >= (ulong)((long)context.cost);
					}
				}
				return false;
			};
			costTypeDef2.payCost = delegate(CostTypeDef costTypeDef, CostTypeDef.PayCostContext context)
			{
				if (context.activatorMaster)
				{
					context.activatorMaster.money -= (uint)context.cost;
					MultiShopCardUtils.OnMoneyPurchase(context);
				}
			};
			costTypeDef2.colorIndex = ColorCatalog.ColorIndex.Money;
			CostTypeCatalog.Register(costType2, costTypeDef2);
			CostTypeIndex costType3 = CostTypeIndex.PercentHealth;
			CostTypeDef costTypeDef3 = new CostTypeDef();
			costTypeDef3.costStringFormatToken = "COST_PERCENTHEALTH_FORMAT";
			costTypeDef3.saturateWorldStyledCostString = false;
			costTypeDef3.darkenWorldStyledCostString = true;
			costTypeDef3.isAffordable = delegate(CostTypeDef costTypeDef, CostTypeDef.IsAffordableContext context)
			{
				HealthComponent component = context.activator.GetComponent<HealthComponent>();
				return component && component.combinedHealth / component.fullCombinedHealth * 100f >= (float)context.cost;
			};
			costTypeDef3.payCost = delegate(CostTypeDef costTypeDef, CostTypeDef.PayCostContext context)
			{
				HealthComponent component = context.activator.GetComponent<HealthComponent>();
				if (component)
				{
					float combinedHealth = component.combinedHealth;
					float num = component.fullCombinedHealth * (float)context.cost / 100f;
					if (combinedHealth > num)
					{
						component.TakeDamage(new DamageInfo
						{
							damage = num,
							attacker = context.purchasedObject,
							position = context.purchasedObject.transform.position,
							damageType = (DamageType.NonLethal | DamageType.BypassArmor)
						});
						MultiShopCardUtils.OnNonMoneyPurchase(context);
					}
				}
			};
			costTypeDef3.colorIndex = ColorCatalog.ColorIndex.Blood;
			CostTypeCatalog.Register(costType3, costTypeDef3);
			CostTypeIndex costType4 = CostTypeIndex.LunarCoin;
			CostTypeDef costTypeDef4 = new CostTypeDef();
			costTypeDef4.costStringFormatToken = "COST_LUNARCOIN_FORMAT";
			costTypeDef4.saturateWorldStyledCostString = false;
			costTypeDef4.darkenWorldStyledCostString = true;
			costTypeDef4.isAffordable = delegate(CostTypeDef costTypeDef, CostTypeDef.IsAffordableContext context)
			{
				NetworkUser networkUser = Util.LookUpBodyNetworkUser(context.activator.gameObject);
				return networkUser && (ulong)networkUser.lunarCoins >= (ulong)((long)context.cost);
			};
			costTypeDef4.payCost = delegate(CostTypeDef costTypeDef, CostTypeDef.PayCostContext context)
			{
				NetworkUser networkUser = Util.LookUpBodyNetworkUser(context.activator.gameObject);
				if (networkUser)
				{
					networkUser.DeductLunarCoins((uint)context.cost);
					MultiShopCardUtils.OnNonMoneyPurchase(context);
				}
			};
			costTypeDef4.colorIndex = ColorCatalog.ColorIndex.LunarCoin;
			CostTypeCatalog.Register(costType4, costTypeDef4);
			CostTypeIndex costType5 = CostTypeIndex.VoidCoin;
			CostTypeDef costTypeDef5 = new CostTypeDef();
			costTypeDef5.costStringFormatToken = "COST_VOIDCOIN_FORMAT";
			costTypeDef5.isAffordable = delegate(CostTypeDef costTypeDef, CostTypeDef.IsAffordableContext context)
			{
				CharacterBody component = context.activator.GetComponent<CharacterBody>();
				if (component)
				{
					CharacterMaster master = component.master;
					if (master)
					{
						return (ulong)master.voidCoins >= (ulong)((long)context.cost);
					}
				}
				return false;
			};
			costTypeDef5.payCost = delegate(CostTypeDef costTypeDef, CostTypeDef.PayCostContext context)
			{
				if (context.activatorMaster)
				{
					context.activatorMaster.voidCoins -= (uint)context.cost;
					MultiShopCardUtils.OnNonMoneyPurchase(context);
				}
			};
			costTypeDef5.saturateWorldStyledCostString = false;
			costTypeDef5.darkenWorldStyledCostString = true;
			costTypeDef5.colorIndex = ColorCatalog.ColorIndex.VoidCoin;
			CostTypeCatalog.Register(costType5, costTypeDef5);
			CostTypeCatalog.Register(CostTypeIndex.WhiteItem, new CostTypeDef
			{
				costStringFormatToken = "COST_ITEM_FORMAT",
				isAffordable = new CostTypeDef.IsAffordableDelegate(CostTypeCatalog.<>c.<>9.<Init>g__IsAffordableItem|5_0),
				payCost = new CostTypeDef.PayCostDelegate(CostTypeCatalog.<>c.<>9.<Init>g__PayCostItems|5_1),
				colorIndex = ColorCatalog.ColorIndex.Tier1Item,
				itemTier = ItemTier.Tier1
			});
			CostTypeCatalog.Register(CostTypeIndex.GreenItem, new CostTypeDef
			{
				costStringFormatToken = "COST_ITEM_FORMAT",
				saturateWorldStyledCostString = true,
				isAffordable = new CostTypeDef.IsAffordableDelegate(CostTypeCatalog.<>c.<>9.<Init>g__IsAffordableItem|5_0),
				payCost = new CostTypeDef.PayCostDelegate(CostTypeCatalog.<>c.<>9.<Init>g__PayCostItems|5_1),
				colorIndex = ColorCatalog.ColorIndex.Tier2Item,
				itemTier = ItemTier.Tier2
			});
			CostTypeCatalog.Register(CostTypeIndex.RedItem, new CostTypeDef
			{
				costStringFormatToken = "COST_ITEM_FORMAT",
				saturateWorldStyledCostString = false,
				darkenWorldStyledCostString = true,
				isAffordable = new CostTypeDef.IsAffordableDelegate(CostTypeCatalog.<>c.<>9.<Init>g__IsAffordableItem|5_0),
				payCost = new CostTypeDef.PayCostDelegate(CostTypeCatalog.<>c.<>9.<Init>g__PayCostItems|5_1),
				colorIndex = ColorCatalog.ColorIndex.Tier3Item,
				itemTier = ItemTier.Tier3
			});
			CostTypeCatalog.Register(CostTypeIndex.BossItem, new CostTypeDef
			{
				costStringFormatToken = "COST_ITEM_FORMAT",
				darkenWorldStyledCostString = true,
				isAffordable = new CostTypeDef.IsAffordableDelegate(CostTypeCatalog.<>c.<>9.<Init>g__IsAffordableItem|5_0),
				payCost = new CostTypeDef.PayCostDelegate(CostTypeCatalog.<>c.<>9.<Init>g__PayCostItems|5_1),
				colorIndex = ColorCatalog.ColorIndex.BossItem,
				itemTier = ItemTier.Boss
			});
			CostTypeIndex costType6 = CostTypeIndex.Equipment;
			CostTypeDef costTypeDef6 = new CostTypeDef();
			costTypeDef6.costStringFormatToken = "COST_EQUIPMENT_FORMAT";
			costTypeDef6.isAffordable = delegate(CostTypeDef costTypeDef, CostTypeDef.IsAffordableContext context)
			{
				CharacterBody component = context.activator.gameObject.GetComponent<CharacterBody>();
				if (component)
				{
					Inventory inventory = component.inventory;
					if (inventory)
					{
						return inventory.currentEquipmentIndex != EquipmentIndex.None;
					}
				}
				return false;
			};
			costTypeDef6.payCost = new CostTypeDef.PayCostDelegate(CostTypeCatalog.<>c.<>9.<Init>g__PayEquipment|5_2);
			costTypeDef6.colorIndex = ColorCatalog.ColorIndex.Equipment;
			CostTypeCatalog.Register(costType6, costTypeDef6);
			CostTypeIndex costType7 = CostTypeIndex.VolatileBattery;
			CostTypeDef costTypeDef7 = new CostTypeDef();
			costTypeDef7.costStringFormatToken = "COST_VOLATILEBATTERY_FORMAT";
			costTypeDef7.isAffordable = delegate(CostTypeDef costTypeDef, CostTypeDef.IsAffordableContext context)
			{
				CharacterBody component = context.activator.gameObject.GetComponent<CharacterBody>();
				if (component)
				{
					Inventory inventory = component.inventory;
					if (inventory)
					{
						return inventory.currentEquipmentIndex == RoR2Content.Equipment.QuestVolatileBattery.equipmentIndex;
					}
				}
				return false;
			};
			costTypeDef7.payCost = new CostTypeDef.PayCostDelegate(CostTypeCatalog.<>c.<>9.<Init>g__PayEquipment|5_2);
			costTypeDef7.colorIndex = ColorCatalog.ColorIndex.Equipment;
			CostTypeCatalog.Register(costType7, costTypeDef7);
			CostTypeCatalog.Register(CostTypeIndex.LunarItemOrEquipment, new CostTypeDef
			{
				costStringFormatToken = "COST_LUNAR_FORMAT",
				isAffordable = new CostTypeDef.IsAffordableDelegate(CostTypeCatalog.LunarItemOrEquipmentCostTypeHelper.IsAffordable),
				payCost = new CostTypeDef.PayCostDelegate(CostTypeCatalog.LunarItemOrEquipmentCostTypeHelper.PayCost),
				colorIndex = ColorCatalog.ColorIndex.LunarItem
			});
			CostTypeIndex costType8 = CostTypeIndex.ArtifactShellKillerItem;
			CostTypeDef costTypeDef8 = new CostTypeDef();
			costTypeDef8.costStringFormatToken = "COST_ARTIFACTSHELLKILLERITEM_FORMAT";
			costTypeDef8.isAffordable = delegate(CostTypeDef costTypeDef, CostTypeDef.IsAffordableContext context)
			{
				CharacterBody component = context.activator.gameObject.GetComponent<CharacterBody>();
				if (component)
				{
					Inventory inventory = component.inventory;
					if (inventory)
					{
						return inventory.GetItemCount(RoR2Content.Items.ArtifactKey) >= context.cost;
					}
				}
				return false;
			};
			costTypeDef8.payCost = delegate(CostTypeDef costTypeDef, CostTypeDef.PayCostContext context)
			{
				context.activatorBody.inventory.RemoveItem(RoR2Content.Items.ArtifactKey, context.cost);
				MultiShopCardUtils.OnNonMoneyPurchase(context);
			};
			costTypeDef8.colorIndex = ColorCatalog.ColorIndex.Artifact;
			CostTypeCatalog.Register(costType8, costTypeDef8);
			CostTypeIndex costType9 = CostTypeIndex.TreasureCacheItem;
			CostTypeDef costTypeDef9 = new CostTypeDef();
			costTypeDef9.costStringFormatToken = "COST_TREASURECACHEITEM_FORMAT";
			costTypeDef9.isAffordable = delegate(CostTypeDef costTypeDef, CostTypeDef.IsAffordableContext context)
			{
				CharacterBody component = context.activator.gameObject.GetComponent<CharacterBody>();
				if (component)
				{
					Inventory inventory = component.inventory;
					if (inventory)
					{
						return inventory.GetItemCount(RoR2Content.Items.TreasureCache) >= context.cost;
					}
				}
				return false;
			};
			costTypeDef9.payCost = delegate(CostTypeDef costTypeDef, CostTypeDef.PayCostContext context)
			{
				context.activatorBody.inventory.RemoveItem(RoR2Content.Items.TreasureCache, context.cost);
				MultiShopCardUtils.OnNonMoneyPurchase(context);
			};
			costTypeDef9.colorIndex = ColorCatalog.ColorIndex.Tier1Item;
			CostTypeCatalog.Register(costType9, costTypeDef9);
			CostTypeIndex costType10 = CostTypeIndex.TreasureCacheVoidItem;
			CostTypeDef costTypeDef10 = new CostTypeDef();
			costTypeDef10.costStringFormatToken = "COST_TREASURECACHEVOIDITEM_FORMAT";
			costTypeDef10.isAffordable = delegate(CostTypeDef costTypeDef, CostTypeDef.IsAffordableContext context)
			{
				CharacterBody component = context.activator.gameObject.GetComponent<CharacterBody>();
				if (component)
				{
					Inventory inventory = component.inventory;
					if (inventory)
					{
						return inventory.GetItemCount(DLC1Content.Items.TreasureCacheVoid) >= context.cost;
					}
				}
				return false;
			};
			costTypeDef10.payCost = delegate(CostTypeDef costTypeDef, CostTypeDef.PayCostContext context)
			{
				context.activatorBody.inventory.RemoveItem(DLC1Content.Items.TreasureCacheVoid, context.cost);
				MultiShopCardUtils.OnNonMoneyPurchase(context);
			};
			costTypeDef10.colorIndex = ColorCatalog.ColorIndex.VoidItem;
			CostTypeCatalog.Register(costType10, costTypeDef10);
			CostTypeCatalog.modHelper.CollectAndRegisterAdditionalEntries(ref CostTypeCatalog.costTypeDefs);
		}

		// Token: 0x060017C2 RID: 6082 RVA: 0x0006851A File Offset: 0x0006671A
		public static CostTypeDef GetCostTypeDef(CostTypeIndex costTypeIndex)
		{
			return ArrayUtils.GetSafe<CostTypeDef>(CostTypeCatalog.costTypeDefs, (int)costTypeIndex);
		}

		// Token: 0x060017C4 RID: 6084 RVA: 0x00068554 File Offset: 0x00066754
		[CompilerGenerated]
		internal static void <Init>g__TakeItemFromWeightedSelection|5_14(WeightedSelection<ItemIndex> weightedSelection, int choiceIndex, ref CostTypeCatalog.<>c__DisplayClass5_1 A_2)
		{
			WeightedSelection<ItemIndex>.ChoiceInfo choice = weightedSelection.GetChoice(choiceIndex);
			ItemIndex value = choice.value;
			int num = (int)choice.weight;
			num--;
			if (num <= 0)
			{
				weightedSelection.RemoveChoice(choiceIndex);
			}
			else
			{
				weightedSelection.ModifyChoiceWeight(choiceIndex, (float)num);
			}
			A_2.itemsToTake.Add(value);
		}

		// Token: 0x060017C5 RID: 6085 RVA: 0x0006859C File Offset: 0x0006679C
		[CompilerGenerated]
		internal static void <Init>g__TakeItemsFromWeightedSelection|5_15(WeightedSelection<ItemIndex> weightedSelection, ref CostTypeCatalog.<>c__DisplayClass5_0 A_1, ref CostTypeCatalog.<>c__DisplayClass5_1 A_2)
		{
			while (weightedSelection.Count > 0 && A_2.itemsToTake.Count < A_1.context.cost)
			{
				int choiceIndex = weightedSelection.EvaluateToChoiceIndex(A_1.context.rng.nextNormalizedFloat);
				CostTypeCatalog.<Init>g__TakeItemFromWeightedSelection|5_14(weightedSelection, choiceIndex, ref A_2);
			}
		}

		// Token: 0x04001D6D RID: 7533
		private static CostTypeDef[] costTypeDefs;

		// Token: 0x04001D6E RID: 7534
		public static readonly CatalogModHelper<CostTypeDef> modHelper = new CatalogModHelper<CostTypeDef>(delegate(int i, CostTypeDef def)
		{
			CostTypeCatalog.Register((CostTypeIndex)i, def);
		}, (CostTypeDef v) => v.name);

		// Token: 0x0200051C RID: 1308
		private static class LunarItemOrEquipmentCostTypeHelper
		{
			// Token: 0x060017C6 RID: 6086 RVA: 0x000685EC File Offset: 0x000667EC
			public static bool IsAffordable(CostTypeDef costTypeDef, CostTypeDef.IsAffordableContext context)
			{
				CharacterBody component = context.activator.GetComponent<CharacterBody>();
				if (!component)
				{
					return false;
				}
				Inventory inventory = component.inventory;
				if (!inventory)
				{
					return false;
				}
				int cost = context.cost;
				int num = 0;
				for (int i = 0; i < CostTypeCatalog.LunarItemOrEquipmentCostTypeHelper.lunarItemIndices.Length; i++)
				{
					int itemCount = inventory.GetItemCount(CostTypeCatalog.LunarItemOrEquipmentCostTypeHelper.lunarItemIndices[i]);
					num += itemCount;
					if (num >= cost)
					{
						return true;
					}
				}
				int j = 0;
				int equipmentSlotCount = inventory.GetEquipmentSlotCount();
				while (j < equipmentSlotCount)
				{
					EquipmentState equipment = inventory.GetEquipment((uint)j);
					for (int k = 0; k < CostTypeCatalog.LunarItemOrEquipmentCostTypeHelper.lunarEquipmentIndices.Length; k++)
					{
						if (equipment.equipmentIndex == CostTypeCatalog.LunarItemOrEquipmentCostTypeHelper.lunarEquipmentIndices[k])
						{
							num++;
							if (num >= cost)
							{
								return true;
							}
						}
					}
					j++;
				}
				return false;
			}

			// Token: 0x060017C7 RID: 6087 RVA: 0x000686B0 File Offset: 0x000668B0
			public static void PayCost(CostTypeDef costTypeDef, CostTypeDef.PayCostContext context)
			{
				CostTypeCatalog.LunarItemOrEquipmentCostTypeHelper.<>c__DisplayClass3_0 CS$<>8__locals1;
				CS$<>8__locals1.context = context;
				CS$<>8__locals1.inventory = CS$<>8__locals1.context.activator.GetComponent<CharacterBody>().inventory;
				int cost = CS$<>8__locals1.context.cost;
				CS$<>8__locals1.itemWeight = 0;
				CS$<>8__locals1.equipmentWeight = 0;
				for (int i = 0; i < CostTypeCatalog.LunarItemOrEquipmentCostTypeHelper.lunarItemIndices.Length; i++)
				{
					ItemIndex itemIndex = CostTypeCatalog.LunarItemOrEquipmentCostTypeHelper.lunarItemIndices[i];
					int itemCount = CS$<>8__locals1.inventory.GetItemCount(itemIndex);
					CS$<>8__locals1.itemWeight += itemCount;
				}
				int j = 0;
				int equipmentSlotCount = CS$<>8__locals1.inventory.GetEquipmentSlotCount();
				while (j < equipmentSlotCount)
				{
					EquipmentState equipment = CS$<>8__locals1.inventory.GetEquipment((uint)j);
					if (Array.IndexOf<EquipmentIndex>(CostTypeCatalog.LunarItemOrEquipmentCostTypeHelper.lunarEquipmentIndices, equipment.equipmentIndex) != -1)
					{
						int equipmentWeight = CS$<>8__locals1.equipmentWeight + 1;
						CS$<>8__locals1.equipmentWeight = equipmentWeight;
					}
					j++;
				}
				CS$<>8__locals1.totalWeight = CS$<>8__locals1.itemWeight + CS$<>8__locals1.equipmentWeight;
				for (int k = 0; k < cost; k++)
				{
					CostTypeCatalog.LunarItemOrEquipmentCostTypeHelper.<PayCost>g__TakeOne|3_0(ref CS$<>8__locals1);
				}
				MultiShopCardUtils.OnNonMoneyPurchase(CS$<>8__locals1.context);
			}

			// Token: 0x060017C8 RID: 6088 RVA: 0x000687C0 File Offset: 0x000669C0
			private static void PayOne(Inventory inventory)
			{
				new WeightedSelection<ItemIndex>(CostTypeCatalog.LunarItemOrEquipmentCostTypeHelper.lunarItemIndices.Length);
				new WeightedSelection<uint>(inventory.GetEquipmentSlotCount());
				int num = 0;
				int num2 = 0;
				for (int i = 0; i < CostTypeCatalog.LunarItemOrEquipmentCostTypeHelper.lunarItemIndices.Length; i++)
				{
					ItemIndex itemIndex = CostTypeCatalog.LunarItemOrEquipmentCostTypeHelper.lunarItemIndices[i];
					int itemCount = inventory.GetItemCount(itemIndex);
					num += itemCount;
				}
				int j = 0;
				int equipmentSlotCount = inventory.GetEquipmentSlotCount();
				while (j < equipmentSlotCount)
				{
					EquipmentState equipment = inventory.GetEquipment((uint)j);
					if (Array.IndexOf<EquipmentIndex>(CostTypeCatalog.LunarItemOrEquipmentCostTypeHelper.lunarEquipmentIndices, equipment.equipmentIndex) != -1)
					{
						num2++;
					}
					j++;
				}
			}

			// Token: 0x060017C9 RID: 6089 RVA: 0x00068850 File Offset: 0x00066A50
			[SystemInitializer(new Type[]
			{
				typeof(ItemCatalog),
				typeof(EquipmentCatalog)
			})]
			private static void Init()
			{
				CostTypeCatalog.LunarItemOrEquipmentCostTypeHelper.lunarItemIndices = ItemCatalog.lunarItemList.ToArray();
				CostTypeCatalog.LunarItemOrEquipmentCostTypeHelper.lunarEquipmentIndices = (from v in EquipmentCatalog.equipmentList
				where EquipmentCatalog.GetEquipmentDef(v).isLunar
				select v).ToArray<EquipmentIndex>();
			}

			// Token: 0x060017CB RID: 6091 RVA: 0x000688B8 File Offset: 0x00066AB8
			[CompilerGenerated]
			internal static void <PayCost>g__TakeOne|3_0(ref CostTypeCatalog.LunarItemOrEquipmentCostTypeHelper.<>c__DisplayClass3_0 A_0)
			{
				float nextNormalizedFloat = A_0.context.rng.nextNormalizedFloat;
				float num = (float)A_0.itemWeight / (float)A_0.totalWeight;
				if (nextNormalizedFloat < num)
				{
					int num2 = Mathf.FloorToInt(Util.Remap(Util.Remap(nextNormalizedFloat, 0f, num, 0f, 1f), 0f, 1f, 0f, (float)A_0.itemWeight));
					int num3 = 0;
					for (int i = 0; i < CostTypeCatalog.LunarItemOrEquipmentCostTypeHelper.lunarItemIndices.Length; i++)
					{
						ItemIndex itemIndex = CostTypeCatalog.LunarItemOrEquipmentCostTypeHelper.lunarItemIndices[i];
						int itemCount = A_0.inventory.GetItemCount(itemIndex);
						num3 += itemCount;
						if (num2 < num3)
						{
							A_0.inventory.RemoveItem(itemIndex, 1);
							A_0.context.results.itemsTaken.Add(itemIndex);
							return;
						}
					}
					return;
				}
				int num4 = Mathf.FloorToInt(Util.Remap(Util.Remap(nextNormalizedFloat, num, 1f, 0f, 1f), 0f, 1f, 0f, (float)A_0.equipmentWeight));
				int num5 = 0;
				for (int j = 0; j < A_0.inventory.GetEquipmentSlotCount(); j++)
				{
					EquipmentIndex equipmentIndex = A_0.inventory.GetEquipment((uint)j).equipmentIndex;
					if (Array.IndexOf<EquipmentIndex>(CostTypeCatalog.LunarItemOrEquipmentCostTypeHelper.lunarEquipmentIndices, equipmentIndex) != -1)
					{
						num5++;
						if (num4 < num5)
						{
							A_0.inventory.SetEquipment(EquipmentState.empty, (uint)j);
							A_0.context.results.equipmentTaken.Add(equipmentIndex);
						}
					}
				}
			}

			// Token: 0x04001D6F RID: 7535
			private static ItemIndex[] lunarItemIndices = Array.Empty<ItemIndex>();

			// Token: 0x04001D70 RID: 7536
			private static EquipmentIndex[] lunarEquipmentIndices = Array.Empty<EquipmentIndex>();
		}
	}
}
