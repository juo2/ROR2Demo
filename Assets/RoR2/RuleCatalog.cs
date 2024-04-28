using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RoR2.ConVar;
using RoR2.EntitlementManagement;
using RoR2.ExpansionManagement;
using UnityEngine;

namespace RoR2
{
	// Token: 0x02000A1A RID: 2586
	public static class RuleCatalog
	{
		// Token: 0x170005A6 RID: 1446
		// (get) Token: 0x06003BA6 RID: 15270 RVA: 0x000F67D9 File Offset: 0x000F49D9
		public static IEnumerable<RuleChoiceDef> allChoiceDefsWithUnlocks
		{
			get
			{
				return RuleCatalog._allChoiceDefsWithUnlocks;
			}
		}

		// Token: 0x170005A7 RID: 1447
		// (get) Token: 0x06003BA7 RID: 15271 RVA: 0x000F67E0 File Offset: 0x000F49E0
		public static int ruleCount
		{
			get
			{
				return RuleCatalog.allRuleDefs.Count;
			}
		}

		// Token: 0x170005A8 RID: 1448
		// (get) Token: 0x06003BA8 RID: 15272 RVA: 0x000F67EC File Offset: 0x000F49EC
		public static int choiceCount
		{
			get
			{
				return RuleCatalog.allChoicesDefs.Count;
			}
		}

		// Token: 0x170005A9 RID: 1449
		// (get) Token: 0x06003BA9 RID: 15273 RVA: 0x000F67F8 File Offset: 0x000F49F8
		public static int categoryCount
		{
			get
			{
				return RuleCatalog.allCategoryDefs.Count;
			}
		}

		// Token: 0x170005AA RID: 1450
		// (get) Token: 0x06003BAA RID: 15274 RVA: 0x000F6804 File Offset: 0x000F4A04
		// (set) Token: 0x06003BAB RID: 15275 RVA: 0x000F680B File Offset: 0x000F4A0B
		public static int highestLocalChoiceCount { get; private set; }

		// Token: 0x06003BAC RID: 15276 RVA: 0x000F6813 File Offset: 0x000F4A13
		public static RuleDef GetRuleDef(int ruleDefIndex)
		{
			return RuleCatalog.allRuleDefs[ruleDefIndex];
		}

		// Token: 0x06003BAD RID: 15277 RVA: 0x000F6820 File Offset: 0x000F4A20
		public static RuleDef FindRuleDef(string ruleDefGlobalName)
		{
			RuleDef result;
			RuleCatalog.ruleDefsByGlobalName.TryGetValue(ruleDefGlobalName, out result);
			return result;
		}

		// Token: 0x06003BAE RID: 15278 RVA: 0x000F683C File Offset: 0x000F4A3C
		public static RuleChoiceDef FindChoiceDef(string ruleChoiceDefGlobalName)
		{
			RuleChoiceDef result;
			RuleCatalog.ruleChoiceDefsByGlobalName.TryGetValue(ruleChoiceDefGlobalName, out result);
			return result;
		}

		// Token: 0x06003BAF RID: 15279 RVA: 0x000F6858 File Offset: 0x000F4A58
		public static RuleChoiceDef GetChoiceDef(int ruleChoiceDefIndex)
		{
			return RuleCatalog.allChoicesDefs[ruleChoiceDefIndex];
		}

		// Token: 0x06003BB0 RID: 15280 RVA: 0x000F6865 File Offset: 0x000F4A65
		public static RuleCategoryDef GetCategoryDef(int ruleCategoryDefIndex)
		{
			return RuleCatalog.allCategoryDefs[ruleCategoryDefIndex];
		}

		// Token: 0x06003BB1 RID: 15281 RVA: 0x0000B4B7 File Offset: 0x000096B7
		private static bool HiddenTestTrue()
		{
			return true;
		}

		// Token: 0x06003BB2 RID: 15282 RVA: 0x0000CF8A File Offset: 0x0000B18A
		private static bool HiddenTestFalse()
		{
			return false;
		}

		// Token: 0x06003BB3 RID: 15283 RVA: 0x000F6872 File Offset: 0x000F4A72
		private static bool HiddenTestItemsConvar()
		{
			return !RuleCatalog.ruleShowItems.value;
		}

		// Token: 0x06003BB4 RID: 15284 RVA: 0x000F6881 File Offset: 0x000F4A81
		private static RuleCategoryDef AddCategory(string displayToken, string subtitleToken, Color color)
		{
			return RuleCatalog.AddCategory(displayToken, subtitleToken, color, "", null, new Func<bool>(RuleCatalog.HiddenTestFalse), RuleCatalog.RuleCategoryType.StripVote);
		}

		// Token: 0x06003BB5 RID: 15285 RVA: 0x000F68A0 File Offset: 0x000F4AA0
		private static RuleCategoryDef AddCategory(string displayToken, string subtitleToken, Color color, string emptyTipToken, string editToken, Func<bool> hiddenTest, RuleCatalog.RuleCategoryType ruleCategoryType)
		{
			RuleCategoryDef ruleCategoryDef = new RuleCategoryDef
			{
				position = RuleCatalog.allRuleDefs.Count,
				displayToken = displayToken,
				subtitleToken = subtitleToken,
				color = color,
				emptyTipToken = emptyTipToken,
				editToken = editToken,
				hiddenTest = hiddenTest,
				ruleCategoryType = ruleCategoryType
			};
			RuleCatalog.allCategoryDefs.Add(ruleCategoryDef);
			return ruleCategoryDef;
		}

		// Token: 0x06003BB6 RID: 15286 RVA: 0x000F6904 File Offset: 0x000F4B04
		private static void AddRule(RuleDef ruleDef)
		{
			if (RuleCatalog.allCategoryDefs.Count > 0)
			{
				ruleDef.category = RuleCatalog.allCategoryDefs[RuleCatalog.allCategoryDefs.Count - 1];
				RuleCatalog.allCategoryDefs[RuleCatalog.allCategoryDefs.Count - 1].children.Add(ruleDef);
			}
			RuleCatalog.allRuleDefs.Add(ruleDef);
			if (RuleCatalog.highestLocalChoiceCount < ruleDef.choices.Count)
			{
				RuleCatalog.highestLocalChoiceCount = ruleDef.choices.Count;
			}
			RuleCatalog.ruleDefsByGlobalName[ruleDef.globalName] = ruleDef;
			foreach (RuleChoiceDef ruleChoiceDef in ruleDef.choices)
			{
				RuleCatalog.ruleChoiceDefsByGlobalName[ruleChoiceDef.globalName] = ruleChoiceDef;
			}
		}

		// Token: 0x06003BB7 RID: 15287 RVA: 0x000F69EC File Offset: 0x000F4BEC
		[SystemInitializer(new Type[]
		{
			typeof(ItemCatalog),
			typeof(EquipmentCatalog),
			typeof(ArtifactCatalog),
			typeof(EntitlementCatalog)
		})]
		private unsafe static void Init()
		{
			RuleCatalog.AddCategory("RULE_HEADER_DIFFICULTY", "", new Color32(43, 124, 181, byte.MaxValue));
			RuleCatalog.AddRule(RuleDef.FromDifficulty());
			RuleCatalog.AddCategory("RULE_HEADER_EXPANSIONS", "RULE_HEADER_EXPANSIONS_SUBTITLE", new Color32(219, 114, 114, byte.MaxValue), null, "RULE_HEADER_EXPANSIONS_EDIT", new Func<bool>(RuleCatalog.HiddenTestFalse), RuleCatalog.RuleCategoryType.VoteResultGrid);
			foreach (ExpansionDef expansionDef in ExpansionCatalog.expansionDefs)
			{
				RuleCatalog.AddRule(RuleDef.FromExpansion(expansionDef));
			}
			RuleCatalog.artifactRuleCategory = RuleCatalog.AddCategory("RULE_HEADER_ARTIFACTS", "RULE_HEADER_ARTIFACTS_SUBTITLE", ColorCatalog.GetColor(ColorCatalog.ColorIndex.Artifact), "RULE_ARTIFACTS_EMPTY_TIP", "RULE_HEADER_ARTIFACTS_EDIT", new Func<bool>(RuleCatalog.HiddenTestFalse), RuleCatalog.RuleCategoryType.VoteResultGrid);
			for (ArtifactIndex artifactIndex = (ArtifactIndex)0; artifactIndex < (ArtifactIndex)ArtifactCatalog.artifactCount; artifactIndex++)
			{
				RuleCatalog.AddRule(RuleDef.FromArtifact(artifactIndex));
			}
			RuleCatalog.AddCategory("RULE_HEADER_ITEMS", "RULE_HEADER_ITEMSANDEQUIPMENT_SUBTITLE", new Color32(147, 225, 128, byte.MaxValue), null, "RULE_HEADER_ITEMSANDEQUIPMENT_EDIT", new Func<bool>(RuleCatalog.HiddenTestItemsConvar), RuleCatalog.RuleCategoryType.VoteResultGrid);
			List<ItemIndex> list = new List<ItemIndex>();
			ItemIndex itemIndex = (ItemIndex)0;
			ItemIndex itemCount = (ItemIndex)ItemCatalog.itemCount;
			while (itemIndex < itemCount)
			{
				list.Add(itemIndex);
				itemIndex++;
			}
			foreach (ItemIndex itemIndex2 in from i in list
			where ItemCatalog.GetItemDef(i).inDroppableTier
			orderby ItemCatalog.GetItemDef(i).tier
			select i)
			{
				RuleCatalog.AddRule(RuleDef.FromItem(itemIndex2));
			}
			from i in list
			where ItemCatalog.GetItemDef(i).inDroppableTier
			orderby ItemCatalog.GetItemDef(i).tier
			select i;
			RuleCatalog.AddCategory("RULE_HEADER_EQUIPMENT", "RULE_HEADER_ITEMSANDEQUIPMENT_SUBTITLE", new Color32(byte.MaxValue, 128, 0, byte.MaxValue), null, "RULE_HEADER_ITEMSANDEQUIPMENT_EDIT", new Func<bool>(RuleCatalog.HiddenTestItemsConvar), RuleCatalog.RuleCategoryType.VoteResultGrid);
			List<EquipmentIndex> list2 = new List<EquipmentIndex>();
			EquipmentIndex equipmentIndex = (EquipmentIndex)0;
			EquipmentIndex equipmentCount = (EquipmentIndex)EquipmentCatalog.equipmentCount;
			while (equipmentIndex < equipmentCount)
			{
				list2.Add(equipmentIndex);
				equipmentIndex++;
			}
			foreach (EquipmentIndex equipmentIndex2 in from i in list2
			where EquipmentCatalog.GetEquipmentDef(i).canDrop
			select i)
			{
				RuleCatalog.AddRule(RuleDef.FromEquipment(equipmentIndex2));
			}
			RuleCatalog.AddCategory("RULE_HEADER_MISC", "", new Color32(192, 192, 192, byte.MaxValue), null, "", new Func<bool>(RuleCatalog.HiddenTestFalse), RuleCatalog.RuleCategoryType.VoteResultGrid);
			RuleDef ruleDef = new RuleDef("Misc.StartingMoney", "RULE_MISC_STARTING_MONEY");
			RuleChoiceDef ruleChoiceDef = ruleDef.AddChoice("0", 0U, true);
			ruleChoiceDef.tooltipNameToken = "RULE_STARTINGMONEY_CHOICE_0_NAME";
			ruleChoiceDef.tooltipBodyToken = "RULE_STARTINGMONEY_CHOICE_0_DESC";
			ruleChoiceDef.tooltipNameColor = ColorCatalog.GetColor(ColorCatalog.ColorIndex.LunarCoin);
			ruleChoiceDef.onlyShowInGameBrowserIfNonDefault = true;
			RuleChoiceDef ruleChoiceDef2 = ruleDef.AddChoice("15", 15U, true);
			ruleChoiceDef2.tooltipNameToken = "RULE_STARTINGMONEY_CHOICE_15_NAME";
			ruleChoiceDef2.tooltipBodyToken = "RULE_STARTINGMONEY_CHOICE_15_DESC";
			ruleChoiceDef2.tooltipNameColor = ColorCatalog.GetColor(ColorCatalog.ColorIndex.LunarCoin);
			ruleChoiceDef2.onlyShowInGameBrowserIfNonDefault = true;
			ruleDef.MakeNewestChoiceDefault();
			RuleChoiceDef ruleChoiceDef3 = ruleDef.AddChoice("50", 50U, true);
			ruleChoiceDef3.tooltipNameToken = "RULE_STARTINGMONEY_CHOICE_50_NAME";
			ruleChoiceDef3.tooltipBodyToken = "RULE_STARTINGMONEY_CHOICE_50_DESC";
			ruleChoiceDef3.spritePath = "Textures/MiscIcons/texRuleBonusStartingMoney";
			ruleChoiceDef3.tooltipNameColor = ColorCatalog.GetColor(ColorCatalog.ColorIndex.LunarCoin);
			ruleChoiceDef3.onlyShowInGameBrowserIfNonDefault = true;
			RuleCatalog.AddRule(ruleDef);
			RuleDef ruleDef2 = new RuleDef("Misc.StageOrder", "RULE_MISC_STAGE_ORDER");
			RuleChoiceDef ruleChoiceDef4 = ruleDef2.AddChoice("Normal", StageOrder.Normal, true);
			ruleChoiceDef4.tooltipNameToken = "RULE_STAGEORDER_CHOICE_NORMAL_NAME";
			ruleChoiceDef4.tooltipBodyToken = "RULE_STAGEORDER_CHOICE_NORMAL_DESC";
			ruleChoiceDef4.tooltipNameColor = ColorCatalog.GetColor(ColorCatalog.ColorIndex.LunarCoin);
			ruleChoiceDef4.onlyShowInGameBrowserIfNonDefault = true;
			ruleDef2.MakeNewestChoiceDefault();
			RuleChoiceDef ruleChoiceDef5 = ruleDef2.AddChoice("Random", StageOrder.Random, true);
			ruleChoiceDef5.tooltipNameToken = "RULE_STAGEORDER_CHOICE_RANDOM_NAME";
			ruleChoiceDef5.tooltipBodyToken = "RULE_STAGEORDER_CHOICE_RANDOM_DESC";
			ruleChoiceDef5.spritePath = "Textures/MiscIcons/texRuleMapIsRandom";
			ruleChoiceDef5.tooltipNameColor = ColorCatalog.GetColor(ColorCatalog.ColorIndex.LunarCoin);
			ruleChoiceDef5.onlyShowInGameBrowserIfNonDefault = true;
			RuleCatalog.AddRule(ruleDef2);
			RuleDef ruleDef3 = new RuleDef("Misc.KeepMoneyBetweenStages", "RULE_MISC_KEEP_MONEY_BETWEEN_STAGES");
			RuleChoiceDef ruleChoiceDef6 = ruleDef3.AddChoice("On", true, true);
			ruleChoiceDef6.tooltipNameToken = "";
			ruleChoiceDef6.tooltipBodyToken = "RULE_KEEPMONEYBETWEENSTAGES_CHOICE_ON_DESC";
			ruleChoiceDef6.onlyShowInGameBrowserIfNonDefault = true;
			RuleChoiceDef ruleChoiceDef7 = ruleDef3.AddChoice("Off", false, true);
			ruleChoiceDef7.tooltipNameToken = "";
			ruleChoiceDef7.tooltipBodyToken = "RULE_KEEPMONEYBETWEENSTAGES_CHOICE_OFF_DESC";
			ruleChoiceDef7.onlyShowInGameBrowserIfNonDefault = true;
			ruleDef3.MakeNewestChoiceDefault();
			RuleCatalog.AddRule(ruleDef3);
			RuleDef ruleDef4 = new RuleDef("Misc.AllowDropIn", "RULE_MISC_ALLOW_DROP_IN");
			RuleChoiceDef ruleChoiceDef8 = ruleDef4.AddChoice("On", true, true);
			ruleChoiceDef8.tooltipNameToken = "";
			ruleChoiceDef8.tooltipBodyToken = "RULE_ALLOWDROPIN_CHOICE_ON_DESC";
			ruleChoiceDef8.onlyShowInGameBrowserIfNonDefault = true;
			RuleChoiceDef ruleChoiceDef9 = ruleDef4.AddChoice("Off", false, true);
			ruleChoiceDef9.tooltipNameToken = "";
			ruleChoiceDef9.tooltipBodyToken = "RULE_ALLOWDROPIN_CHOICE_OFF_DESC";
			ruleChoiceDef9.onlyShowInGameBrowserIfNonDefault = true;
			ruleDef4.MakeNewestChoiceDefault();
			RuleCatalog.AddRule(ruleDef4);
			for (int k = 0; k < RuleCatalog.allRuleDefs.Count; k++)
			{
				RuleDef ruleDef5 = RuleCatalog.allRuleDefs[k];
				ruleDef5.globalIndex = k;
				for (int j = 0; j < ruleDef5.choices.Count; j++)
				{
					RuleChoiceDef ruleChoiceDef10 = ruleDef5.choices[j];
					ruleChoiceDef10.localIndex = j;
					ruleChoiceDef10.globalIndex = RuleCatalog.allChoicesDefs.Count;
					RuleCatalog.allChoicesDefs.Add(ruleChoiceDef10);
				}
				RuleCatalog._allChoiceDefsWithUnlocks = (from choiceDef in RuleCatalog.allChoicesDefs
				where *choiceDef.unlockable
				select choiceDef).ToArray<RuleChoiceDef>();
			}
			RuleCatalog.availability.MakeAvailable();
		}

		// Token: 0x06003BB8 RID: 15288 RVA: 0x000F7080 File Offset: 0x000F5280
		[ConCommand(commandName = "rules_dump", flags = ConVarFlags.None, helpText = "Dump information about the rules system.")]
		private static void CCRulesDump(ConCommandArgs args)
		{
			List<string> list = new List<string>();
			List<string> list2 = new List<string>();
			for (int i = 0; i < RuleCatalog.ruleCount; i++)
			{
				RuleDef ruleDef = RuleCatalog.GetRuleDef(i);
				for (int j = 0; j < ruleDef.choices.Count; j++)
				{
					RuleChoiceDef ruleChoiceDef = ruleDef.choices[j];
					string item = string.Format("  {{localChoiceIndex={0} globalChoiceIndex={1} localName={2}}}", ruleChoiceDef.localIndex, ruleChoiceDef.globalIndex, ruleChoiceDef.localName);
					list2.Add(item);
				}
				string str = string.Join("\n", list2);
				list2.Clear();
				string str2 = string.Format("[{0}] {1} defaultChoiceIndex={2}\n", i, ruleDef.globalName, ruleDef.defaultChoiceIndex);
				list.Add(str2 + str);
			}
			Debug.Log(string.Join("\n", list));
		}

		// Token: 0x06003BB9 RID: 15289 RVA: 0x000F7168 File Offset: 0x000F5368
		[ConCommand(commandName = "rules_list_choices", flags = ConVarFlags.None, helpText = "Lists all rule choices.")]
		private static void CCRulesListChoices(ConCommandArgs args)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendLine("* = default choice.");
			stringBuilder.AppendLine();
			foreach (RuleChoiceDef ruleChoiceDef in RuleCatalog.allChoicesDefs)
			{
				stringBuilder.Append(ruleChoiceDef.globalName);
				if (ruleChoiceDef.isDefaultChoice)
				{
					stringBuilder.Append("*");
				}
				stringBuilder.AppendLine();
			}
			Debug.Log(stringBuilder.ToString());
		}

		// Token: 0x04003B32 RID: 15154
		private static readonly List<RuleDef> allRuleDefs = new List<RuleDef>();

		// Token: 0x04003B33 RID: 15155
		private static readonly List<RuleChoiceDef> allChoicesDefs = new List<RuleChoiceDef>();

		// Token: 0x04003B34 RID: 15156
		public static readonly List<RuleCategoryDef> allCategoryDefs = new List<RuleCategoryDef>();

		// Token: 0x04003B35 RID: 15157
		public static RuleCategoryDef artifactRuleCategory;

		// Token: 0x04003B36 RID: 15158
		private static RuleChoiceDef[] _allChoiceDefsWithUnlocks = Array.Empty<RuleChoiceDef>();

		// Token: 0x04003B37 RID: 15159
		private static readonly Dictionary<string, RuleDef> ruleDefsByGlobalName = new Dictionary<string, RuleDef>();

		// Token: 0x04003B38 RID: 15160
		private static readonly Dictionary<string, RuleChoiceDef> ruleChoiceDefsByGlobalName = new Dictionary<string, RuleChoiceDef>();

		// Token: 0x04003B39 RID: 15161
		public static ResourceAvailability availability;

		// Token: 0x04003B3B RID: 15163
		private static readonly BoolConVar ruleShowItems = new BoolConVar("rule_show_items", ConVarFlags.Cheat, "0", "Whether or not to allow voting on items in the pregame rules.");

		// Token: 0x02000A1B RID: 2587
		public enum RuleCategoryType
		{
			// Token: 0x04003B3D RID: 15165
			StripVote,
			// Token: 0x04003B3E RID: 15166
			VoteResultGrid
		}
	}
}
