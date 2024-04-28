using System;
using JetBrains.Annotations;
using RoR2.EntitlementManagement;
using RoR2.ExpansionManagement;
using UnityEngine;

namespace RoR2
{
	// Token: 0x02000A1E RID: 2590
	public class RuleChoiceDef
	{
		// Token: 0x170005AC RID: 1452
		// (set) Token: 0x06003BC5 RID: 15301 RVA: 0x000F72CA File Offset: 0x000F54CA
		public string spritePath
		{
			set
			{
				this.sprite = LegacyResourcesAPI.Load<Sprite>(value);
			}
		}

		// Token: 0x170005AD RID: 1453
		// (get) Token: 0x06003BC6 RID: 15302 RVA: 0x000F72D8 File Offset: 0x000F54D8
		[Obsolete("Use 'requiredUnlockable' instead.", false)]
		public ref UnlockableDef unlockable
		{
			get
			{
				return ref this.requiredUnlockable;
			}
		}

		// Token: 0x170005AE RID: 1454
		// (get) Token: 0x06003BC7 RID: 15303 RVA: 0x000F72E0 File Offset: 0x000F54E0
		public bool isDefaultChoice
		{
			get
			{
				return this.ruleDef.defaultChoiceIndex == this.localIndex;
			}
		}

		// Token: 0x06003BC8 RID: 15304 RVA: 0x000F72F5 File Offset: 0x000F54F5
		public static string GetNormalTooltipNameFromToken(RuleChoiceDef ruleChoiceDef)
		{
			return Language.GetString(ruleChoiceDef.tooltipNameToken);
		}

		// Token: 0x06003BC9 RID: 15305 RVA: 0x000F7302 File Offset: 0x000F5502
		public static string GetOffTooltipNameFromToken(RuleChoiceDef ruleChoiceDef)
		{
			return Language.GetStringFormatted("RULE_OFF_FORMAT", new object[]
			{
				Language.GetString(ruleChoiceDef.tooltipNameToken)
			});
		}

		// Token: 0x04003B4F RID: 15183
		public RuleDef ruleDef;

		// Token: 0x04003B50 RID: 15184
		public Sprite sprite;

		// Token: 0x04003B51 RID: 15185
		[NotNull]
		public string tooltipNameToken;

		// Token: 0x04003B52 RID: 15186
		public Func<RuleChoiceDef, string> getTooltipName = new Func<RuleChoiceDef, string>(RuleChoiceDef.GetNormalTooltipNameFromToken);

		// Token: 0x04003B53 RID: 15187
		public Color tooltipNameColor = Color.white;

		// Token: 0x04003B54 RID: 15188
		[NotNull]
		public string tooltipBodyToken;

		// Token: 0x04003B55 RID: 15189
		public Color tooltipBodyColor = Color.white;

		// Token: 0x04003B56 RID: 15190
		public string localName;

		// Token: 0x04003B57 RID: 15191
		public string globalName;

		// Token: 0x04003B58 RID: 15192
		public int localIndex;

		// Token: 0x04003B59 RID: 15193
		public int globalIndex;

		// Token: 0x04003B5A RID: 15194
		public UnlockableDef requiredUnlockable;

		// Token: 0x04003B5B RID: 15195
		public RuleChoiceDef requiredChoiceDef;

		// Token: 0x04003B5C RID: 15196
		public EntitlementDef requiredEntitlementDef;

		// Token: 0x04003B5D RID: 15197
		public ExpansionDef requiredExpansionDef;

		// Token: 0x04003B5E RID: 15198
		public bool availableInSinglePlayer = true;

		// Token: 0x04003B5F RID: 15199
		public bool availableInMultiPlayer = true;

		// Token: 0x04003B60 RID: 15200
		public DifficultyIndex difficultyIndex = DifficultyIndex.Invalid;

		// Token: 0x04003B61 RID: 15201
		public ArtifactIndex artifactIndex = ArtifactIndex.None;

		// Token: 0x04003B62 RID: 15202
		public ItemIndex itemIndex = ItemIndex.None;

		// Token: 0x04003B63 RID: 15203
		public EquipmentIndex equipmentIndex = EquipmentIndex.None;

		// Token: 0x04003B64 RID: 15204
		public object extraData;

		// Token: 0x04003B65 RID: 15205
		public bool excludeByDefault;

		// Token: 0x04003B66 RID: 15206
		public string selectionUISound;

		// Token: 0x04003B67 RID: 15207
		[CanBeNull]
		public string serverTag;

		// Token: 0x04003B68 RID: 15208
		public bool onlyShowInGameBrowserIfNonDefault;
	}
}
