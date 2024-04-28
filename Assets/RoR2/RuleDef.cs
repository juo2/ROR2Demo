using System;
using System.Collections.Generic;
using RoR2.ExpansionManagement;
using UnityEngine;

namespace RoR2
{
	// Token: 0x02000A1F RID: 2591
	public class RuleDef
	{
		// Token: 0x06003BCB RID: 15307 RVA: 0x000F738C File Offset: 0x000F558C
		public RuleChoiceDef AddChoice(string choiceName, object extraData = null, bool excludeByDefault = false)
		{
			RuleChoiceDef ruleChoiceDef = new RuleChoiceDef();
			ruleChoiceDef.ruleDef = this;
			ruleChoiceDef.localName = choiceName;
			ruleChoiceDef.globalName = this.globalName + "." + choiceName;
			ruleChoiceDef.localIndex = this.choices.Count;
			ruleChoiceDef.extraData = extraData;
			ruleChoiceDef.excludeByDefault = excludeByDefault;
			this.choices.Add(ruleChoiceDef);
			return ruleChoiceDef;
		}

		// Token: 0x06003BCC RID: 15308 RVA: 0x000F73F0 File Offset: 0x000F55F0
		public int AvailableChoiceCount(RuleChoiceMask availability)
		{
			int num = 0;
			foreach (RuleChoiceDef ruleChoiceDef in this.choices)
			{
				if (availability[ruleChoiceDef.globalIndex])
				{
					num++;
				}
			}
			return num;
		}

		// Token: 0x06003BCD RID: 15309 RVA: 0x000F7454 File Offset: 0x000F5654
		public RuleChoiceDef FindChoice(string choiceLocalName)
		{
			int i = 0;
			int count = this.choices.Count;
			while (i < count)
			{
				if (this.choices[i].localName == choiceLocalName)
				{
					return this.choices[i];
				}
				i++;
			}
			return null;
		}

		// Token: 0x06003BCE RID: 15310 RVA: 0x000F74A0 File Offset: 0x000F56A0
		public void MakeNewestChoiceDefault()
		{
			this.defaultChoiceIndex = this.choices.Count - 1;
		}

		// Token: 0x06003BCF RID: 15311 RVA: 0x000F74B5 File Offset: 0x000F56B5
		public RuleDef(string globalName, string displayToken)
		{
			this.globalName = globalName;
			this.displayToken = displayToken;
		}

		// Token: 0x06003BD0 RID: 15312 RVA: 0x000F74D8 File Offset: 0x000F56D8
		public static RuleDef FromDifficulty()
		{
			RuleDef ruleDef = new RuleDef("Difficulty", "RULE_NAME_DIFFICULTY");
			for (DifficultyIndex difficultyIndex = DifficultyIndex.Easy; difficultyIndex < DifficultyIndex.Count; difficultyIndex++)
			{
				DifficultyDef difficultyDef = DifficultyCatalog.GetDifficultyDef(difficultyIndex);
				RuleChoiceDef ruleChoiceDef = ruleDef.AddChoice(difficultyIndex.ToString(), null, false);
				ruleChoiceDef.spritePath = difficultyDef.iconPath;
				ruleChoiceDef.tooltipNameToken = difficultyDef.nameToken;
				ruleChoiceDef.tooltipNameColor = difficultyDef.color;
				ruleChoiceDef.tooltipBodyToken = difficultyDef.descriptionToken;
				ruleChoiceDef.difficultyIndex = difficultyIndex;
				ruleChoiceDef.serverTag = difficultyDef.serverTag;
				ruleChoiceDef.excludeByDefault = (difficultyIndex >= (DifficultyIndex)DifficultyCatalog.standardDifficultyCount);
			}
			ruleDef.defaultChoiceIndex = 1;
			return ruleDef;
		}

		// Token: 0x06003BD1 RID: 15313 RVA: 0x000F757C File Offset: 0x000F577C
		public unsafe static RuleDef FromArtifact(ArtifactIndex artifactIndex)
		{
			ArtifactDef artifactDef = ArtifactCatalog.GetArtifactDef(artifactIndex);
			RuleDef ruleDef = new RuleDef("Artifacts." + artifactDef.cachedName, artifactDef.nameToken);
			RuleChoiceDef ruleChoiceDef = ruleDef.AddChoice("On", null, false);
			ruleChoiceDef.sprite = artifactDef.smallIconSelectedSprite;
			ruleChoiceDef.tooltipNameToken = artifactDef.nameToken;
			ruleChoiceDef.tooltipBodyToken = artifactDef.descriptionToken;
			*ruleChoiceDef.unlockable = artifactDef.unlockableDef;
			ruleChoiceDef.tooltipNameColor = ColorCatalog.GetColor(ColorCatalog.ColorIndex.Artifact);
			ruleChoiceDef.artifactIndex = artifactIndex;
			ruleChoiceDef.selectionUISound = "Play_UI_artifactSelect";
			ruleChoiceDef.requiredExpansionDef = artifactDef.requiredExpansion;
			ruleChoiceDef.extraData = artifactDef;
			RuleChoiceDef ruleChoiceDef2 = ruleDef.AddChoice("Off", null, false);
			ruleChoiceDef2.sprite = artifactDef.smallIconDeselectedSprite;
			ruleChoiceDef2.tooltipNameToken = artifactDef.nameToken;
			ruleChoiceDef2.getTooltipName = new Func<RuleChoiceDef, string>(RuleChoiceDef.GetOffTooltipNameFromToken);
			ruleChoiceDef2.tooltipBodyToken = artifactDef.descriptionToken;
			ruleChoiceDef2.tooltipNameColor = ColorCatalog.GetColor(ColorCatalog.ColorIndex.Unaffordable);
			ruleChoiceDef2.selectionUISound = "Play_UI_artifactDeselect";
			ruleChoiceDef2.extraData = artifactDef;
			ruleDef.MakeNewestChoiceDefault();
			return ruleDef;
		}

		// Token: 0x06003BD2 RID: 15314 RVA: 0x000F768C File Offset: 0x000F588C
		public unsafe static RuleDef FromItem(ItemIndex itemIndex)
		{
			ItemDef itemDef = ItemCatalog.GetItemDef(itemIndex);
			RuleDef ruleDef = new RuleDef("Items." + itemDef.name, itemDef.nameToken);
			RuleChoiceDef ruleChoiceDef = ruleDef.AddChoice("On", null, false);
			ruleChoiceDef.sprite = itemDef.pickupIconSprite;
			ruleChoiceDef.tooltipNameToken = itemDef.nameToken;
			ruleChoiceDef.tooltipBodyToken = "RULE_ITEM_ON_DESCRIPTION";
			*ruleChoiceDef.unlockable = itemDef.unlockableDef;
			ruleChoiceDef.itemIndex = itemIndex;
			ruleChoiceDef.onlyShowInGameBrowserIfNonDefault = true;
			ruleChoiceDef.requiredExpansionDef = itemDef.requiredExpansion;
			ruleDef.MakeNewestChoiceDefault();
			RuleChoiceDef ruleChoiceDef2 = ruleDef.AddChoice("Off", null, false);
			ruleChoiceDef2.spritePath = "Textures/MiscIcons/texUnlockIcon";
			ruleChoiceDef2.tooltipNameToken = itemDef.nameToken;
			ruleChoiceDef2.getTooltipName = new Func<RuleChoiceDef, string>(RuleChoiceDef.GetOffTooltipNameFromToken);
			ruleChoiceDef2.tooltipBodyToken = "RULE_ITEM_OFF_DESCRIPTION";
			ruleChoiceDef2.onlyShowInGameBrowserIfNonDefault = true;
			return ruleDef;
		}

		// Token: 0x06003BD3 RID: 15315 RVA: 0x000F7760 File Offset: 0x000F5960
		public unsafe static RuleDef FromEquipment(EquipmentIndex equipmentIndex)
		{
			EquipmentDef equipmentDef = EquipmentCatalog.GetEquipmentDef(equipmentIndex);
			RuleDef ruleDef = new RuleDef("Equipment." + equipmentDef.name, equipmentDef.nameToken);
			RuleChoiceDef ruleChoiceDef = ruleDef.AddChoice("On", null, false);
			ruleChoiceDef.sprite = equipmentDef.pickupIconSprite;
			ruleChoiceDef.tooltipNameToken = equipmentDef.nameToken;
			ruleChoiceDef.tooltipBodyToken = "RULE_ITEM_ON_DESCRIPTION";
			*ruleChoiceDef.unlockable = equipmentDef.unlockableDef;
			ruleChoiceDef.equipmentIndex = equipmentIndex;
			ruleChoiceDef.availableInMultiPlayer = equipmentDef.appearsInMultiPlayer;
			ruleChoiceDef.availableInSinglePlayer = equipmentDef.appearsInSinglePlayer;
			ruleChoiceDef.onlyShowInGameBrowserIfNonDefault = true;
			ruleChoiceDef.requiredExpansionDef = equipmentDef.requiredExpansion;
			ruleDef.MakeNewestChoiceDefault();
			RuleChoiceDef ruleChoiceDef2 = ruleDef.AddChoice("Off", null, false);
			ruleChoiceDef2.sprite = equipmentDef.pickupIconSprite;
			ruleChoiceDef2.tooltipNameToken = equipmentDef.nameToken;
			ruleChoiceDef2.getTooltipName = new Func<RuleChoiceDef, string>(RuleChoiceDef.GetOffTooltipNameFromToken);
			ruleChoiceDef2.tooltipBodyToken = "RULE_ITEM_OFF_DESCRIPTION";
			ruleChoiceDef2.onlyShowInGameBrowserIfNonDefault = true;
			return ruleDef;
		}

		// Token: 0x06003BD4 RID: 15316 RVA: 0x000F784C File Offset: 0x000F5A4C
		public static RuleDef FromExpansion(ExpansionDef expansionDef)
		{
			RuleDef ruleDef = new RuleDef("Expansions." + expansionDef.name, expansionDef.nameToken);
			ruleDef.forceLobbyDisplay = true;
			RuleChoiceDef ruleChoiceDef = ruleDef.AddChoice("On", expansionDef, false);
			ruleChoiceDef.sprite = expansionDef.iconSprite;
			ruleChoiceDef.tooltipNameToken = expansionDef.nameToken;
			ruleChoiceDef.tooltipNameColor = new Color32(219, 114, 114, byte.MaxValue);
			ruleChoiceDef.tooltipBodyToken = expansionDef.descriptionToken;
			ruleChoiceDef.requiredEntitlementDef = expansionDef.requiredEntitlement;
			ruleDef.MakeNewestChoiceDefault();
			expansionDef.enabledChoice = ruleChoiceDef;
			RuleChoiceDef ruleChoiceDef2 = ruleDef.AddChoice("Off", null, false);
			ruleChoiceDef2.sprite = expansionDef.disabledIconSprite;
			ruleChoiceDef2.tooltipNameToken = expansionDef.nameToken;
			ruleChoiceDef2.tooltipNameColor = ColorCatalog.GetColor(ColorCatalog.ColorIndex.Unaffordable);
			ruleChoiceDef2.getTooltipName = new Func<RuleChoiceDef, string>(RuleChoiceDef.GetOffTooltipNameFromToken);
			ruleChoiceDef2.tooltipBodyToken = expansionDef.descriptionToken;
			return ruleDef;
		}

		// Token: 0x04003B69 RID: 15209
		public readonly string globalName;

		// Token: 0x04003B6A RID: 15210
		public int globalIndex;

		// Token: 0x04003B6B RID: 15211
		public readonly string displayToken;

		// Token: 0x04003B6C RID: 15212
		public readonly List<RuleChoiceDef> choices = new List<RuleChoiceDef>();

		// Token: 0x04003B6D RID: 15213
		public int defaultChoiceIndex;

		// Token: 0x04003B6E RID: 15214
		public RuleCategoryDef category;

		// Token: 0x04003B6F RID: 15215
		public bool forceLobbyDisplay;

		// Token: 0x04003B70 RID: 15216
		private const string pathToOffChoiceMaterial = "Materials/UI/matRuleChoiceOff";
	}
}
