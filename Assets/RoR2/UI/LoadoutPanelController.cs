using System;
using System.Collections.Generic;
using RoR2.Skills;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace RoR2.UI
{
	// Token: 0x02000D30 RID: 3376
	[RequireComponent(typeof(MPEventSystemLocator))]
	[RequireComponent(typeof(RectTransform))]
	public class LoadoutPanelController : MonoBehaviour
	{
		// Token: 0x06004D00 RID: 19712 RVA: 0x0013DB95 File Offset: 0x0013BD95
		public void SetDisplayData(LoadoutPanelController.DisplayData displayData)
		{
			if (displayData.Equals(this.currentDisplayData))
			{
				return;
			}
			this.currentDisplayData = displayData;
			this.Rebuild();
		}

		// Token: 0x06004D01 RID: 19713 RVA: 0x0013DBB4 File Offset: 0x0013BDB4
		private void OnEnable()
		{
			this.UpdateDisplayData();
		}

		// Token: 0x06004D02 RID: 19714 RVA: 0x0013DBB4 File Offset: 0x0013BDB4
		private void Update()
		{
			this.UpdateDisplayData();
		}

		// Token: 0x06004D03 RID: 19715 RVA: 0x0013DBBC File Offset: 0x0013BDBC
		private void UpdateDisplayData()
		{
			MPEventSystem eventSystem = this.eventSystemLocator.eventSystem;
			UserProfile userProfile;
			if (eventSystem == null)
			{
				userProfile = null;
			}
			else
			{
				LocalUser localUser = eventSystem.localUser;
				userProfile = ((localUser != null) ? localUser.userProfile : null);
			}
			UserProfile userProfile2 = userProfile;
			MPEventSystem eventSystem2 = this.eventSystemLocator.eventSystem;
			NetworkUser networkUser;
			if (eventSystem2 == null)
			{
				networkUser = null;
			}
			else
			{
				LocalUser localUser2 = eventSystem2.localUser;
				networkUser = ((localUser2 != null) ? localUser2.currentNetworkUser : null);
			}
			NetworkUser networkUser2 = networkUser;
			BodyIndex bodyIndex = networkUser2 ? networkUser2.bodyIndexPreference : BodyIndex.None;
			this.SetDisplayData(new LoadoutPanelController.DisplayData
			{
				userProfile = userProfile2,
				bodyIndex = bodyIndex
			});
		}

		// Token: 0x06004D04 RID: 19716 RVA: 0x0013DC44 File Offset: 0x0013BE44
		private void DestroyRows()
		{
			for (int i = this.rows.Count - 1; i >= 0; i--)
			{
				this.rows[i].Dispose();
			}
			this.rows.Clear();
		}

		// Token: 0x06004D05 RID: 19717 RVA: 0x0013DC88 File Offset: 0x0013BE88
		private void Rebuild()
		{
			this.DestroyRows();
			CharacterBody bodyPrefabBodyComponent = BodyCatalog.GetBodyPrefabBodyComponent(this.currentDisplayData.bodyIndex);
			if (bodyPrefabBodyComponent)
			{
				List<GenericSkill> gameObjectComponents = GetComponentsCache<GenericSkill>.GetGameObjectComponents(bodyPrefabBodyComponent.gameObject);
				int i = 0;
				int count = gameObjectComponents.Count;
				while (i < count)
				{
					GenericSkill skillSlot = gameObjectComponents[i];
					this.rows.Add(LoadoutPanelController.Row.FromSkillSlot(this, this.currentDisplayData.bodyIndex, i, skillSlot));
					i++;
				}
				int num = BodyCatalog.GetBodySkins(this.currentDisplayData.bodyIndex).Length;
				if (true)
				{
					this.rows.Add(LoadoutPanelController.Row.FromSkin(this, this.currentDisplayData.bodyIndex));
				}
			}
		}

		// Token: 0x06004D06 RID: 19718 RVA: 0x0013DD2D File Offset: 0x0013BF2D
		private void Awake()
		{
			this.eventSystemLocator = base.GetComponent<MPEventSystemLocator>();
		}

		// Token: 0x06004D07 RID: 19719 RVA: 0x0013DD3B File Offset: 0x0013BF3B
		[RuntimeInitializeOnLoadMethod]
		private static void Init()
		{
			LoadoutPanelController.loadoutButtonPrefab = LegacyResourcesAPI.Load<GameObject>("Prefabs/UI/Loadout/LoadoutButton");
			LoadoutPanelController.rowPrefab = LegacyResourcesAPI.Load<GameObject>("Prefabs/UI/Loadout/Row");
			LoadoutPanelController.lockedIcon = LegacyResourcesAPI.Load<Sprite>("Textures/MiscIcons/texUnlockIcon");
		}

		// Token: 0x170006FE RID: 1790
		// (get) Token: 0x06004D08 RID: 19720 RVA: 0x0013DD6A File Offset: 0x0013BF6A
		private RectTransform rowContainer
		{
			get
			{
				return (RectTransform)base.transform;
			}
		}

		// Token: 0x06004D09 RID: 19721 RVA: 0x0013DD77 File Offset: 0x0013BF77
		private void OnDestroy()
		{
			this.DestroyRows();
		}

		// Token: 0x04004A06 RID: 18950
		public UILayerKey requiredUILayerKey;

		// Token: 0x04004A07 RID: 18951
		public LanguageTextMeshController hoverTextDescription;

		// Token: 0x04004A08 RID: 18952
		private LoadoutPanelController.DisplayData currentDisplayData = new LoadoutPanelController.DisplayData
		{
			userProfile = null,
			bodyIndex = BodyIndex.None
		};

		// Token: 0x04004A09 RID: 18953
		private UIElementAllocator<RectTransform> buttonAllocator;

		// Token: 0x04004A0A RID: 18954
		private readonly List<LoadoutPanelController.Row> rows = new List<LoadoutPanelController.Row>();

		// Token: 0x04004A0B RID: 18955
		public static int minimumEntriesPerRow = 2;

		// Token: 0x04004A0C RID: 18956
		private MPEventSystemLocator eventSystemLocator;

		// Token: 0x04004A0D RID: 18957
		private UIElementAllocator<RectTransform> rowAllocator;

		// Token: 0x04004A0E RID: 18958
		private static GameObject loadoutButtonPrefab;

		// Token: 0x04004A0F RID: 18959
		private static GameObject rowPrefab;

		// Token: 0x04004A10 RID: 18960
		private static Sprite lockedIcon;

		// Token: 0x02000D31 RID: 3377
		public struct DisplayData : IEquatable<LoadoutPanelController.DisplayData>
		{
			// Token: 0x06004D0C RID: 19724 RVA: 0x0013DDC5 File Offset: 0x0013BFC5
			public bool Equals(LoadoutPanelController.DisplayData other)
			{
				return this.userProfile == other.userProfile && this.bodyIndex == other.bodyIndex;
			}

			// Token: 0x06004D0D RID: 19725 RVA: 0x0013DDE8 File Offset: 0x0013BFE8
			public override bool Equals(object obj)
			{
				if (obj is LoadoutPanelController.DisplayData)
				{
					LoadoutPanelController.DisplayData other = (LoadoutPanelController.DisplayData)obj;
					return this.Equals(other);
				}
				return false;
			}

			// Token: 0x06004D0E RID: 19726 RVA: 0x0013DE0F File Offset: 0x0013C00F
			public override int GetHashCode()
			{
				return ((this.userProfile != null) ? this.userProfile.GetHashCode() : 0) * 397 ^ (int)this.bodyIndex;
			}

			// Token: 0x04004A11 RID: 18961
			public UserProfile userProfile;

			// Token: 0x04004A12 RID: 18962
			public BodyIndex bodyIndex;
		}

		// Token: 0x02000D32 RID: 3378
		private class Row : IDisposable
		{
			// Token: 0x06004D0F RID: 19727 RVA: 0x0013DE34 File Offset: 0x0013C034
			private Row(LoadoutPanelController owner, BodyIndex bodyIndex, string titleToken)
			{
				this.owner = owner;
				this.userProfile = owner.currentDisplayData.userProfile;
				this.rowPanelTransform = (RectTransform)UnityEngine.Object.Instantiate<GameObject>(LoadoutPanelController.rowPrefab, owner.rowContainer).transform;
				this.buttonContainerTransform = (RectTransform)this.rowPanelTransform.Find("ButtonContainer");
				this.choiceHighlightRect = (RectTransform)this.rowPanelTransform.Find("ButtonSelectionHighlight, Checkbox");
				UserProfile.onLoadoutChangedGlobal += this.OnLoadoutChangedGlobal;
				SurvivorCatalog.FindSurvivorDefFromBody(BodyCatalog.GetBodyPrefab(bodyIndex));
				CharacterBody bodyPrefabBodyComponent = BodyCatalog.GetBodyPrefabBodyComponent(bodyIndex);
				if (bodyPrefabBodyComponent != null)
				{
					this.primaryColor = bodyPrefabBodyComponent.bodyColor;
				}
				float num;
				float s;
				float v;
				Color.RGBToHSV(this.primaryColor, out num, out s, out v);
				num += 0.5f;
				if (num > 1f)
				{
					num -= 1f;
				}
				this.complementaryColor = Color.HSVToRGB(num, s, v);
				RectTransform rectTransform = (RectTransform)this.rowPanelTransform.Find("SlotLabel");
				rectTransform.GetComponent<LanguageTextMeshController>().token = titleToken;
				rectTransform.GetComponent<HGTextMeshProUGUI>().color = this.primaryColor;
			}

			// Token: 0x06004D10 RID: 19728 RVA: 0x0013DF61 File Offset: 0x0013C161
			private void OnLoadoutChangedGlobal(UserProfile userProfile)
			{
				if (userProfile == this.userProfile)
				{
					this.UpdateHighlightedChoice();
				}
			}

			// Token: 0x06004D11 RID: 19729 RVA: 0x0013DF74 File Offset: 0x0013C174
			public static LoadoutPanelController.Row FromSkillSlot(LoadoutPanelController owner, BodyIndex bodyIndex, int skillSlotIndex, GenericSkill skillSlot)
			{
				SkillFamily skillFamily = skillSlot.skillFamily;
				SkillLocator component = BodyCatalog.GetBodyPrefabBodyComponent(bodyIndex).GetComponent<SkillLocator>();
				bool addWIPIcons = false;
				string titleToken;
				switch (component.FindSkillSlot(skillSlot))
				{
				case SkillSlot.None:
					titleToken = "LOADOUT_SKILL_MISC";
					addWIPIcons = false;
					break;
				case SkillSlot.Primary:
					titleToken = "LOADOUT_SKILL_PRIMARY";
					addWIPIcons = false;
					break;
				case SkillSlot.Secondary:
					titleToken = "LOADOUT_SKILL_SECONDARY";
					break;
				case SkillSlot.Utility:
					titleToken = "LOADOUT_SKILL_UTILITY";
					break;
				case SkillSlot.Special:
					titleToken = "LOADOUT_SKILL_SPECIAL";
					break;
				default:
					throw new ArgumentOutOfRangeException();
				}
				LoadoutPanelController.Row row = new LoadoutPanelController.Row(owner, bodyIndex, titleToken);
				for (int i = 0; i < skillFamily.variants.Length; i++)
				{
					ref SkillFamily.Variant ptr = ref skillFamily.variants[i];
					uint skillVariantIndexToAssign = (uint)i;
					LoadoutPanelController.Row row2 = row;
					Sprite icon = ptr.skillDef.icon;
					string skillNameToken = ptr.skillDef.skillNameToken;
					string skillDescriptionToken = ptr.skillDef.skillDescriptionToken;
					Color tooltipColor = row.primaryColor;
					UnityAction callback = delegate()
					{
						Loadout loadout = new Loadout();
						row.userProfile.CopyLoadout(loadout);
						loadout.bodyLoadoutManager.SetSkillVariant(bodyIndex, skillSlotIndex, skillVariantIndexToAssign);
						row.userProfile.SetLoadout(loadout);
					};
					UnlockableDef unlockableDef = ptr.unlockableDef;
					row2.AddButton(owner, icon, skillNameToken, skillDescriptionToken, tooltipColor, callback, ((unlockableDef != null) ? unlockableDef.cachedName : null) ?? "", ptr.viewableNode, false);
				}
				row.findCurrentChoice = ((Loadout loadout) => (int)loadout.bodyLoadoutManager.GetSkillVariant(bodyIndex, skillSlotIndex));
				row.FinishSetup(addWIPIcons);
				return row;
			}

			// Token: 0x06004D12 RID: 19730 RVA: 0x0013E100 File Offset: 0x0013C300
			public static LoadoutPanelController.Row FromSkin(LoadoutPanelController owner, BodyIndex bodyIndex)
			{
				LoadoutPanelController.Row row = new LoadoutPanelController.Row(owner, bodyIndex, "LOADOUT_SKIN");
				SkinDef[] bodySkins = BodyCatalog.GetBodySkins(bodyIndex);
				for (int i = 0; i < bodySkins.Length; i++)
				{
					SkinDef skinDef = bodySkins[i];
					uint skinToAssign = (uint)i;
					ViewablesCatalog.Node viewableNode = ViewablesCatalog.FindNode(string.Format("/Loadout/Bodies/{0}/Skins/{1}", BodyCatalog.GetBodyName(bodyIndex), skinDef.name));
					LoadoutPanelController.Row row2 = row;
					Sprite icon = skinDef.icon;
					string nameToken = skinDef.nameToken;
					string empty = string.Empty;
					Color tooltipColor = row.primaryColor;
					UnityAction callback = delegate()
					{
						Loadout loadout = new Loadout();
						row.userProfile.CopyLoadout(loadout);
						loadout.bodyLoadoutManager.SetSkinIndex(bodyIndex, skinToAssign);
						row.userProfile.SetLoadout(loadout);
					};
					UnlockableDef unlockableDef = skinDef.unlockableDef;
					row2.AddButton(owner, icon, nameToken, empty, tooltipColor, callback, ((unlockableDef != null) ? unlockableDef.cachedName : null) ?? "", viewableNode, false);
				}
				row.findCurrentChoice = ((Loadout loadout) => (int)loadout.bodyLoadoutManager.GetSkinIndex(bodyIndex));
				row.FinishSetup(false);
				return row;
			}

			// Token: 0x06004D13 RID: 19731 RVA: 0x0013E21C File Offset: 0x0013C41C
			private void FinishSetup(bool addWIPIcons = false)
			{
				if (addWIPIcons)
				{
					Sprite icon = LegacyResourcesAPI.Load<Sprite>("Textures/MiscIcons/texWIPIcon");
					for (int i = this.buttons.Count; i < LoadoutPanelController.minimumEntriesPerRow; i++)
					{
						this.AddButton(this.owner, icon, "TOOLTIP_WIP_CONTENT_NAME", "TOOLTIP_WIP_CONTENT_DESCRIPTION", ColorCatalog.GetColor(ColorCatalog.ColorIndex.WIP), delegate
						{
						}, "", null, true);
					}
				}
				RectTransform rectTransform = (RectTransform)this.rowPanelTransform.Find("ButtonContainer/Spacer");
				if (rectTransform)
				{
					rectTransform.SetAsLastSibling();
				}
				this.UpdateHighlightedChoice();
			}

			// Token: 0x06004D14 RID: 19732 RVA: 0x0013E2C4 File Offset: 0x0013C4C4
			private void SetButtonColorMultiplier(int i, float f)
			{
				MPButton mpbutton = this.buttons[i];
				ColorBlock colors = mpbutton.colors;
				colors.colorMultiplier = f;
				mpbutton.colors = colors;
			}

			// Token: 0x06004D15 RID: 19733 RVA: 0x0013E2F4 File Offset: 0x0013C4F4
			private void UpdateHighlightedChoice()
			{
				foreach (MPButton mpbutton in this.buttons)
				{
					ColorBlock colors = mpbutton.colors;
					colors.colorMultiplier = 0.5f;
					mpbutton.colors = colors;
				}
				for (int i = 0; i < this.buttons.Count; i++)
				{
					this.SetButtonColorMultiplier(i, 0.5f);
				}
				Loadout loadout = new Loadout();
				UserProfile userProfile = this.userProfile;
				if (userProfile != null)
				{
					userProfile.CopyLoadout(loadout);
				}
				int num = this.findCurrentChoice(loadout);
				if (this.buttons.Count > num)
				{
					this.choiceHighlightRect.SetParent((RectTransform)this.buttons[num].transform, false);
					this.SetButtonColorMultiplier(num, 1f);
				}
			}

			// Token: 0x06004D16 RID: 19734 RVA: 0x0013E3E0 File Offset: 0x0013C5E0
			private void AddButton(LoadoutPanelController owner, Sprite icon, string titleToken, string bodyToken, Color tooltipColor, UnityAction callback, string unlockableName, ViewablesCatalog.Node viewableNode, bool isWIP = false)
			{
				HGButton component = UnityEngine.Object.Instantiate<GameObject>(LoadoutPanelController.loadoutButtonPrefab, this.buttonContainerTransform).GetComponent<HGButton>();
				component.updateTextOnHover = true;
				component.hoverLanguageTextMeshController = owner.hoverTextDescription;
				component.requiredTopLayer = owner.requiredUILayerKey;
				TooltipProvider component2 = component.GetComponent<TooltipProvider>();
				UserProfile userProfile = this.userProfile;
				string @string;
				string text;
				Color color;
				if (userProfile != null && userProfile.HasUnlockable(unlockableName))
				{
					component.onClick.AddListener(callback);
					component.interactable = true;
					if (viewableNode != null)
					{
						ViewableTag component3 = component.GetComponent<ViewableTag>();
						component3.viewableName = viewableNode.fullName;
						component3.Refresh();
					}
					@string = Language.GetString(titleToken);
					text = Language.GetString(bodyToken);
					color = tooltipColor;
				}
				else
				{
					UnlockableDef unlockableDef = UnlockableCatalog.GetUnlockableDef(unlockableName);
					icon = LoadoutPanelController.lockedIcon;
					component.interactable = true;
					component.disableGamepadClick = true;
					component.disablePointerClick = true;
					@string = Language.GetString("UNIDENTIFIED");
					text = unlockableDef.getHowToUnlockString();
					color = Color.gray;
				}
				if (isWIP)
				{
					component.interactable = false;
				}
				component2.titleColor = color;
				component2.overrideTitleText = @string;
				component2.overrideBodyText = text;
				color.a = 0.2f;
				string stringFormatted = Language.GetStringFormatted("LOGBOOK_HOVER_DESCRIPTION_FORMAT", new object[]
				{
					@string,
					text,
					ColorUtility.ToHtmlStringRGBA(color)
				});
				component.hoverToken = stringFormatted;
				((Image)component.targetGraphic).sprite = icon;
				this.buttons.Add(component);
			}

			// Token: 0x06004D17 RID: 19735 RVA: 0x0013E53C File Offset: 0x0013C73C
			public void Dispose()
			{
				UserProfile.onLoadoutChangedGlobal -= this.OnLoadoutChangedGlobal;
				for (int i = this.buttons.Count - 1; i >= 0; i--)
				{
					UnityEngine.Object.Destroy(this.buttons[i].gameObject);
				}
				UnityEngine.Object.Destroy(this.rowPanelTransform.gameObject);
			}

			// Token: 0x04004A13 RID: 18963
			private List<MPButton> buttons = new List<MPButton>();

			// Token: 0x04004A14 RID: 18964
			private LoadoutPanelController owner;

			// Token: 0x04004A15 RID: 18965
			private UserProfile userProfile;

			// Token: 0x04004A16 RID: 18966
			private RectTransform rowPanelTransform;

			// Token: 0x04004A17 RID: 18967
			private RectTransform buttonContainerTransform;

			// Token: 0x04004A18 RID: 18968
			private RectTransform choiceHighlightRect;

			// Token: 0x04004A19 RID: 18969
			private Color primaryColor;

			// Token: 0x04004A1A RID: 18970
			private Color complementaryColor;

			// Token: 0x04004A1B RID: 18971
			private Func<Loadout, int> findCurrentChoice;
		}
	}
}
