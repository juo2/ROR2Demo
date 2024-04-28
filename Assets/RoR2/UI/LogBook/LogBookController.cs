using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using EntityStates;
using HG;
using RoR2.EntitlementManagement;
using RoR2.ExpansionManagement;
using RoR2.Stats;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace RoR2.UI.LogBook
{
	// Token: 0x02000DDC RID: 3548
	public class LogBookController : MonoBehaviour
	{
		// Token: 0x0600516F RID: 20847 RVA: 0x00150024 File Offset: 0x0014E224
		private void Awake()
		{
			this.navigationCategoryButtonAllocator = new UIElementAllocator<MPButton>(this.categoryContainer, this.categoryButtonPrefab, true, false);
			this.navigationPageIndicatorAllocator = new UIElementAllocator<MPButton>(this.navigationPageIndicatorContainer, this.navigationPageIndicatorPrefab, true, false);
			this.navigationPageIndicatorAllocator.onCreateElement = delegate(int index, MPButton button)
			{
				button.onClick.AddListener(delegate()
				{
					this.desiredPageIndex = index;
				});
			};
			this.previousPageButton.onClick.AddListener(new UnityAction(this.OnLeftButton));
			this.nextPageButton.onClick.AddListener(new UnityAction(this.OnRightButton));
			this.pageViewerBackButton.onClick.AddListener(new UnityAction(this.ReturnToNavigation));
			this.stateMachine = base.gameObject.AddComponent<EntityStateMachine>();
			this.uiLayerKey = base.gameObject.GetComponent<UILayerKey>();
			this.stateMachine.initialStateType = default(SerializableEntityStateType);
			this.categoryHightlightRect = (RectTransform)UnityEngine.Object.Instantiate<GameObject>(this.headerHighlightPrefab, base.transform.parent).transform;
			this.categoryHightlightRect.gameObject.SetActive(false);
		}

		// Token: 0x06005170 RID: 20848 RVA: 0x00150138 File Offset: 0x0014E338
		private void Start()
		{
			LocalUser firstLocalUser = LocalUserManager.GetFirstLocalUser();
			this.GeneratePages((firstLocalUser != null) ? firstLocalUser.userProfile : null);
			this.BuildCategoriesButtons();
			this.stateMachine.SetNextState(new LogBookController.ChangeCategoryState
			{
				newCategoryIndex = 0
			});
		}

		// Token: 0x06005171 RID: 20849 RVA: 0x00150170 File Offset: 0x0014E370
		private void BuildCategoriesButtons()
		{
			Debug.Log("Building category buttons.");
			this.navigationCategoryButtonAllocator.AllocateElements(LogBookController.categories.Length);
			ReadOnlyCollection<MPButton> elements = this.navigationCategoryButtonAllocator.elements;
			for (int i = 0; i < LogBookController.categories.Length; i++)
			{
				int categoryIndex = i;
				MPButton mpbutton = elements[i];
				CategoryDef categoryDef = LogBookController.categories[i];
				mpbutton.GetComponentInChildren<TextMeshProUGUI>().text = Language.GetString(categoryDef.nameToken);
				mpbutton.onClick.RemoveAllListeners();
				mpbutton.onClick.AddListener(delegate()
				{
					this.OnCategoryClicked(categoryIndex);
				});
				mpbutton.requiredTopLayer = this.uiLayerKey;
				ViewableTag viewableTag = mpbutton.gameObject.GetComponent<ViewableTag>();
				if (!viewableTag)
				{
					viewableTag = mpbutton.gameObject.AddComponent<ViewableTag>();
				}
				viewableTag.viewableName = categoryDef.viewableNode.fullName;
			}
			if (this.categorySpaceFiller)
			{
				for (int j = 0; j < this.categorySpaceFillerCount; j++)
				{
					UnityEngine.Object.Instantiate<GameObject>(this.categorySpaceFiller, this.categoryContainer).gameObject.SetActive(true);
				}
			}
		}

		// Token: 0x06005172 RID: 20850 RVA: 0x0015029B File Offset: 0x0014E49B
		[SystemInitializer(new Type[]
		{
			typeof(BodyCatalog),
			typeof(SceneCatalog),
			typeof(AchievementManager),
			typeof(ItemCatalog),
			typeof(EquipmentCatalog),
			typeof(UnlockableCatalog),
			typeof(RunReport),
			typeof(SurvivorCatalog),
			typeof(EntitlementManager),
			typeof(ExpansionCatalog)
		})]
		public static void Init()
		{
			if (LocalUserManager.isAnyUserSignedIn)
			{
				LogBookController.BuildStaticData();
			}
			LocalUserManager.onUserSignIn += LogBookController.OnUserSignIn;
			LogBookController.IsInitialized = true;
		}

		// Token: 0x06005173 RID: 20851 RVA: 0x001502C0 File Offset: 0x0014E4C0
		private static void OnUserSignIn(LocalUser obj)
		{
			LogBookController.BuildStaticData();
		}

		// Token: 0x06005174 RID: 20852 RVA: 0x001502C8 File Offset: 0x0014E4C8
		private static EntryStatus GetPickupStatus(in Entry entry, UserProfile viewerProfile)
		{
			PickupIndex pickupIndex = (PickupIndex)entry.extraData;
			PickupDef pickupDef = PickupCatalog.GetPickupDef(pickupIndex);
			ItemIndex itemIndex = (pickupDef != null) ? pickupDef.itemIndex : ItemIndex.None;
			EquipmentIndex equipmentIndex = (pickupDef != null) ? pickupDef.equipmentIndex : EquipmentIndex.None;
			UnlockableDef unlockableDef;
			if (itemIndex != ItemIndex.None)
			{
				unlockableDef = ItemCatalog.GetItemDef(itemIndex).unlockableDef;
			}
			else
			{
				if (equipmentIndex == EquipmentIndex.None)
				{
					return EntryStatus.Unimplemented;
				}
				unlockableDef = EquipmentCatalog.GetEquipmentDef(equipmentIndex).unlockableDef;
			}
			if (!viewerProfile.HasUnlockable(unlockableDef))
			{
				return EntryStatus.Locked;
			}
			if (!viewerProfile.HasDiscoveredPickup(pickupIndex))
			{
				return EntryStatus.Unencountered;
			}
			return EntryStatus.Available;
		}

		// Token: 0x06005175 RID: 20853 RVA: 0x00150344 File Offset: 0x0014E544
		private static TooltipContent GetPickupTooltipContent(in Entry entry, UserProfile userProfile, EntryStatus status)
		{
			UnlockableDef unlockableDef = PickupCatalog.GetPickupDef((PickupIndex)entry.extraData).unlockableDef;
			TooltipContent result = default(TooltipContent);
			if (status >= EntryStatus.Available)
			{
				result.overrideTitleText = entry.GetDisplayName(userProfile);
				result.titleColor = entry.color;
				if (unlockableDef != null)
				{
					result.overrideBodyText = unlockableDef.getUnlockedString();
				}
				result.bodyToken = "";
				result.bodyColor = ColorCatalog.GetColor(ColorCatalog.ColorIndex.Unlockable);
			}
			else
			{
				result.titleToken = "UNIDENTIFIED";
				result.titleColor = Color.gray;
				result.bodyToken = "";
				if (status == EntryStatus.Unimplemented)
				{
					result.titleToken = "TOOLTIP_WIP_CONTENT_NAME";
					result.bodyToken = "TOOLTIP_WIP_CONTENT_DESCRIPTION";
				}
				else if (status == EntryStatus.Unencountered)
				{
					result.overrideBodyText = Language.GetString("LOGBOOK_UNLOCK_ITEM_LOG");
				}
				else if (status == EntryStatus.Locked)
				{
					result.overrideBodyText = unlockableDef.getHowToUnlockString();
				}
				result.bodyColor = Color.white;
			}
			return result;
		}

		// Token: 0x06005176 RID: 20854 RVA: 0x0015044C File Offset: 0x0014E64C
		private static TooltipContent GetMonsterTooltipContent(in Entry entry, UserProfile userProfile, EntryStatus status)
		{
			TooltipContent result = default(TooltipContent);
			result.titleColor = entry.color;
			if (status >= EntryStatus.Available)
			{
				result.overrideTitleText = entry.GetDisplayName(userProfile);
				result.titleColor = entry.color;
				result.bodyToken = "";
			}
			else
			{
				result.titleToken = "UNIDENTIFIED";
				result.titleColor = Color.gray;
				result.bodyToken = "LOGBOOK_UNLOCK_ITEM_MONSTER";
			}
			return result;
		}

		// Token: 0x06005177 RID: 20855 RVA: 0x001504C4 File Offset: 0x0014E6C4
		private static TooltipContent GetStageTooltipContent(in Entry entry, UserProfile userProfile, EntryStatus status)
		{
			TooltipContent result = default(TooltipContent);
			result.titleColor = entry.color;
			if (status >= EntryStatus.Available)
			{
				result.overrideTitleText = entry.GetDisplayName(userProfile);
				result.titleColor = entry.color;
				result.bodyToken = "";
			}
			else
			{
				result.titleToken = "UNIDENTIFIED";
				result.titleColor = Color.gray;
				result.bodyToken = "LOGBOOK_UNLOCK_ITEM_STAGE";
			}
			return result;
		}

		// Token: 0x06005178 RID: 20856 RVA: 0x0015053C File Offset: 0x0014E73C
		private static TooltipContent GetSurvivorTooltipContent(in Entry entry, UserProfile userProfile, EntryStatus status)
		{
			TooltipContent result = default(TooltipContent);
			UnlockableDef unlockableDef = SurvivorCatalog.FindSurvivorDefFromBody(((CharacterBody)entry.extraData).gameObject).unlockableDef;
			if (status >= EntryStatus.Available)
			{
				result.overrideTitleText = entry.GetDisplayName(userProfile);
				result.titleColor = entry.color;
				result.bodyToken = "";
			}
			else
			{
				result.titleToken = "UNIDENTIFIED";
				result.bodyToken = "";
				result.titleColor = Color.gray;
				if (status == EntryStatus.Unencountered)
				{
					result.overrideBodyText = Language.GetString("LOGBOOK_UNLOCK_ITEM_SURVIVOR");
				}
				else if (status == EntryStatus.Locked)
				{
					result.overrideBodyText = unlockableDef.getHowToUnlockString();
				}
			}
			return result;
		}

		// Token: 0x06005179 RID: 20857 RVA: 0x001505F0 File Offset: 0x0014E7F0
		private static TooltipContent GetAchievementTooltipContent(in Entry entry, UserProfile userProfile, EntryStatus status)
		{
			TooltipContent result = default(TooltipContent);
			UnlockableDef unlockableDef = UnlockableCatalog.GetUnlockableDef(((AchievementDef)entry.extraData).unlockableRewardIdentifier);
			result.titleColor = entry.color;
			result.bodyToken = "";
			if (unlockableDef == null)
			{
				result.overrideTitleText = entry.GetDisplayName(userProfile);
				result.titleColor = Color.gray;
				result.overrideBodyText = "ACHIEVEMENT HAS NO UNLOCKABLE DEFINED";
				result.bodyColor = Color.white;
				return result;
			}
			if (status >= EntryStatus.Available)
			{
				result.titleToken = entry.GetDisplayName(userProfile);
				result.titleColor = entry.color;
				result.overrideBodyText = unlockableDef.getUnlockedString();
			}
			else
			{
				result.titleToken = "UNIDENTIFIED";
				result.titleColor = Color.gray;
				if (status == EntryStatus.Locked)
				{
					result.overrideBodyText = Language.GetString("UNIDENTIFIED_DESCRIPTION");
				}
				else
				{
					result.overrideBodyText = unlockableDef.getHowToUnlockString();
				}
			}
			return result;
		}

		// Token: 0x0600517A RID: 20858 RVA: 0x001506EC File Offset: 0x0014E8EC
		private static TooltipContent GetWIPTooltipContent(in Entry entry, UserProfile userProfile, EntryStatus status)
		{
			return new TooltipContent
			{
				titleColor = ColorCatalog.GetColor(ColorCatalog.ColorIndex.WIP),
				titleToken = "TOOLTIP_WIP_CONTENT_NAME",
				bodyToken = "TOOLTIP_WIP_CONTENT_DESCRIPTION"
			};
		}

		// Token: 0x0600517B RID: 20859 RVA: 0x0000DBF3 File Offset: 0x0000BDF3
		private static EntryStatus GetAlwaysAvailable(UserProfile userProfile, Entry entry)
		{
			return EntryStatus.Available;
		}

		// Token: 0x0600517C RID: 20860 RVA: 0x0000B4B7 File Offset: 0x000096B7
		private static EntryStatus GetUnimplemented(in Entry entry, UserProfile viewerProfile)
		{
			return EntryStatus.Unimplemented;
		}

		// Token: 0x0600517D RID: 20861 RVA: 0x00150730 File Offset: 0x0014E930
		private static EntryStatus GetStageStatus(in Entry entry, UserProfile viewerProfile)
		{
			UnlockableDef unlockableLogFromBaseSceneName = SceneCatalog.GetUnlockableLogFromBaseSceneName((entry.extraData as SceneDef).baseSceneName);
			if (unlockableLogFromBaseSceneName != null && viewerProfile.HasUnlockable(unlockableLogFromBaseSceneName))
			{
				return EntryStatus.Available;
			}
			return EntryStatus.Unencountered;
		}

		// Token: 0x0600517E RID: 20862 RVA: 0x0015076C File Offset: 0x0014E96C
		private static EntryStatus GetMonsterStatus(in Entry entry, UserProfile viewerProfile)
		{
			CharacterBody characterBody = (CharacterBody)entry.extraData;
			DeathRewards component = characterBody.GetComponent<DeathRewards>();
			UnlockableDef unlockableDef = (component != null) ? component.logUnlockableDef : null;
			if (!unlockableDef)
			{
				return EntryStatus.None;
			}
			if (viewerProfile.HasUnlockable(unlockableDef))
			{
				return EntryStatus.Available;
			}
			if (viewerProfile.statSheet.GetStatValueULong(PerBodyStatDef.killsAgainst, characterBody.gameObject.name) > 0UL)
			{
				return EntryStatus.Unencountered;
			}
			return EntryStatus.Locked;
		}

		// Token: 0x0600517F RID: 20863 RVA: 0x001507D0 File Offset: 0x0014E9D0
		private static EntryStatus GetSurvivorStatus(in Entry entry, UserProfile viewerProfile)
		{
			CharacterBody characterBody = (CharacterBody)entry.extraData;
			SurvivorDef survivorDef = SurvivorCatalog.FindSurvivorDefFromBody(characterBody.gameObject);
			if (!viewerProfile.HasUnlockable(survivorDef.unlockableDef))
			{
				return EntryStatus.Locked;
			}
			if (viewerProfile.statSheet.GetStatValueULong(PerBodyStatDef.totalWins, characterBody.gameObject.name) == 0UL)
			{
				return EntryStatus.Unencountered;
			}
			return EntryStatus.Available;
		}

		// Token: 0x06005180 RID: 20864 RVA: 0x00150828 File Offset: 0x0014EA28
		private static EntryStatus GetAchievementStatus(in Entry entry, UserProfile viewerProfile)
		{
			string identifier = ((AchievementDef)entry.extraData).identifier;
			bool flag = viewerProfile.HasAchievement(identifier);
			if (!viewerProfile.CanSeeAchievement(identifier))
			{
				return EntryStatus.Locked;
			}
			if (!flag)
			{
				return EntryStatus.Unencountered;
			}
			return EntryStatus.Available;
		}

		// Token: 0x06005181 RID: 20865 RVA: 0x00150860 File Offset: 0x0014EA60
		private static void BuildStaticData()
		{
			LogBookController.categories = LogBookController.BuildCategories();
			LogBookController.RegisterViewables(LogBookController.categories);
			LogBookController.availability.MakeAvailable();
		}

		// Token: 0x17000765 RID: 1893
		// (get) Token: 0x06005183 RID: 20867 RVA: 0x0015089D File Offset: 0x0014EA9D
		// (set) Token: 0x06005184 RID: 20868 RVA: 0x001508A5 File Offset: 0x0014EAA5
		private LogBookController.NavigationPageInfo[] availableNavigationPages
		{
			get
			{
				return this._availableNavigationPages;
			}
			set
			{
				int num = this._availableNavigationPages.Length;
				this._availableNavigationPages = value;
				if (num != this.availableNavigationPages.Length)
				{
					this.navigationPageIndicatorAllocator.AllocateElements(this.availableNavigationPages.Length);
				}
			}
		}

		// Token: 0x06005185 RID: 20869 RVA: 0x001508D3 File Offset: 0x0014EAD3
		private LogBookController.NavigationPageInfo[] GetCategoryPages(int categoryIndex)
		{
			if (this.navigationPagesByCategory.GetLength(0) <= categoryIndex)
			{
				return new LogBookController.NavigationPageInfo[0];
			}
			return this.navigationPagesByCategory[categoryIndex];
		}

		// Token: 0x06005186 RID: 20870 RVA: 0x001508F3 File Offset: 0x0014EAF3
		public void OnLeftButton()
		{
			this.desiredPageIndex--;
		}

		// Token: 0x06005187 RID: 20871 RVA: 0x00150903 File Offset: 0x0014EB03
		public void OnRightButton()
		{
			this.desiredPageIndex++;
		}

		// Token: 0x06005188 RID: 20872 RVA: 0x00150913 File Offset: 0x0014EB13
		private void OnCategoryClicked(int categoryIndex)
		{
			this.desiredCategoryIndex = categoryIndex;
			this.goToEndOfNextCategory = false;
		}

		// Token: 0x06005189 RID: 20873 RVA: 0x00150924 File Offset: 0x0014EB24
		private void GeneratePages(UserProfile viewerProfile)
		{
			if (!LogBookController.IsInitialized)
			{
				LogBookController.Init();
			}
			this.navigationPagesByCategory = new LogBookController.NavigationPageInfo[LogBookController.categories.Length][];
			IEnumerable<LogBookController.NavigationPageInfo> enumerable = Array.Empty<LogBookController.NavigationPageInfo>();
			int num = 0;
			for (int i = 0; i < LogBookController.categories.Length; i++)
			{
				CategoryDef categoryDef = LogBookController.categories[i];
				Entry[] array = categoryDef.BuildEntries(viewerProfile);
				bool fullWidth = categoryDef.fullWidth;
				Vector2 size = this.entryPageContainer.rect.size;
				if (fullWidth)
				{
					categoryDef.iconSize.x = size.x;
				}
				int num2 = Mathf.FloorToInt(Mathf.Max(size.x / categoryDef.iconSize.x, 1f));
				int num3 = Mathf.FloorToInt(Mathf.Max(size.y / categoryDef.iconSize.y, 1f));
				int num4 = num2 * num3;
				int num5 = Mathf.CeilToInt((float)array.Length / (float)num4);
				if (num5 <= 0)
				{
					num5 = 1;
				}
				LogBookController.NavigationPageInfo[] array2 = new LogBookController.NavigationPageInfo[num5];
				for (int j = 0; j < num5; j++)
				{
					int num6 = num4;
					int num7 = j * num4;
					int num8 = array.Length - num7;
					int num9 = num6;
					if (num9 > num8)
					{
						num9 = num8;
					}
					Entry[] array3 = new Entry[num6];
					Array.Copy(array, num7, array3, 0, num9);
					array2[j] = new LogBookController.NavigationPageInfo
					{
						categoryDef = categoryDef,
						entries = array3,
						index = num++,
						indexInCategory = j
					};
				}
				this.navigationPagesByCategory[i] = array2;
				enumerable = enumerable.Concat(array2);
			}
			this.allNavigationPages = enumerable.ToArray<LogBookController.NavigationPageInfo>();
		}

		// Token: 0x0600518A RID: 20874 RVA: 0x00150AB4 File Offset: 0x0014ECB4
		private void Update()
		{
			if (this.desiredPageIndex > this.availableNavigationPages.Length - 1)
			{
				this.desiredPageIndex = this.availableNavigationPages.Length - 1;
				this.desiredCategoryIndex++;
				this.goToEndOfNextCategory = false;
			}
			if (this.desiredPageIndex < 0)
			{
				this.desiredCategoryIndex--;
				this.desiredPageIndex = 0;
				this.goToEndOfNextCategory = true;
			}
			if (this.desiredCategoryIndex > LogBookController.categories.Length - 1)
			{
				this.desiredCategoryIndex = LogBookController.categories.Length - 1;
			}
			if (this.desiredCategoryIndex < 0)
			{
				this.desiredCategoryIndex = 0;
			}
			foreach (MPButton mpbutton in this.navigationPageIndicatorAllocator.elements)
			{
				ColorBlock colors = mpbutton.colors;
				colors.colorMultiplier = 1f;
				mpbutton.colors = colors;
			}
			if (this.currentPageIndex < this.navigationPageIndicatorAllocator.elements.Count)
			{
				MPButton mpbutton2 = this.navigationPageIndicatorAllocator.elements[this.currentPageIndex];
				ColorBlock colors2 = mpbutton2.colors;
				colors2.colorMultiplier = 2f;
				mpbutton2.colors = colors2;
			}
			if (this.desiredCategoryIndex != this.currentCategoryIndex)
			{
				if (this.stateMachine.state is Idle)
				{
					int num = (this.desiredCategoryIndex > this.currentCategoryIndex) ? 1 : -1;
					this.stateMachine.SetNextState(new LogBookController.ChangeCategoryState
					{
						newCategoryIndex = this.currentCategoryIndex + num,
						goToLastPage = this.goToEndOfNextCategory
					});
					return;
				}
			}
			else if (this.desiredPageIndex != this.currentPageIndex && this.stateMachine.state is Idle)
			{
				int num2 = (this.desiredPageIndex > this.currentPageIndex) ? 1 : -1;
				this.stateMachine.SetNextState(new LogBookController.ChangeEntriesPageState
				{
					newNavigationPageInfo = this.GetCategoryPages(this.currentCategoryIndex)[this.currentPageIndex + num2],
					moveDirection = new Vector2((float)num2, 0f)
				});
			}
		}

		// Token: 0x0600518B RID: 20875 RVA: 0x00150CC0 File Offset: 0x0014EEC0
		private UserProfile LookUpUserProfile()
		{
			LocalUser localUser = LocalUserManager.readOnlyLocalUsersList.FirstOrDefault((LocalUser v) => v != null);
			if (localUser == null)
			{
				return null;
			}
			return localUser.userProfile;
		}

		// Token: 0x0600518C RID: 20876 RVA: 0x00150CF8 File Offset: 0x0014EEF8
		private GameObject BuildEntriesPage(LogBookController.NavigationPageInfo navigationPageInfo)
		{
			Entry[] entries = navigationPageInfo.entries;
			CategoryDef categoryDef = navigationPageInfo.categoryDef;
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.entryPagePrefab, this.entryPageContainer);
			gameObject.GetComponent<GridLayoutGroup>().cellSize = categoryDef.iconSize;
			UIElementAllocator<RectTransform> uielementAllocator = new UIElementAllocator<RectTransform>((RectTransform)gameObject.transform, categoryDef.iconPrefab, true, false);
			uielementAllocator.AllocateElements(entries.Length);
			UserProfile userProfile = this.LookUpUserProfile();
			ReadOnlyCollection<RectTransform> elements = uielementAllocator.elements;
			for (int i = 0; i < elements.Count; i++)
			{
				RectTransform rectTransform = elements[i];
				HGButton component = rectTransform.GetComponent<HGButton>();
				Entry entry = (i < entries.Length) ? entries[i] : null;
				Entry entry2 = entry;
				EntryStatus entryStatus = (entry2 != null) ? entry2.GetStatus(userProfile) : EntryStatus.None;
				if (entryStatus != EntryStatus.None)
				{
					TooltipContent tooltipContent = entry.GetTooltipContent(userProfile, entryStatus);
					Action<GameObject, Entry, EntryStatus, UserProfile> initializeElementGraphics = categoryDef.initializeElementGraphics;
					if (initializeElementGraphics != null)
					{
						initializeElementGraphics(rectTransform.gameObject, entry, entryStatus, userProfile);
					}
					if (component)
					{
						bool flag = entryStatus >= EntryStatus.Available;
						component.interactable = true;
						component.disableGamepadClick = (component.disableGamepadClick || !flag);
						component.disablePointerClick = (component.disablePointerClick || !flag);
						component.imageOnInteractable = (flag ? component.imageOnInteractable : null);
						component.requiredTopLayer = this.uiLayerKey;
						component.updateTextOnHover = true;
						component.hoverLanguageTextMeshController = this.hoverLanguageTextMeshController;
						string titleText = tooltipContent.GetTitleText();
						string bodyText = tooltipContent.GetBodyText();
						Color titleColor = tooltipContent.titleColor;
						titleColor.a = 0.2f;
						component.hoverToken = Language.GetStringFormatted("LOGBOOK_HOVER_DESCRIPTION_FORMAT", new object[]
						{
							titleText,
							bodyText,
							ColorUtility.ToHtmlStringRGBA(titleColor)
						});
					}
					if (entry.viewableNode != null)
					{
						ViewableTag viewableTag = rectTransform.gameObject.GetComponent<ViewableTag>();
						if (!viewableTag)
						{
							viewableTag = rectTransform.gameObject.AddComponent<ViewableTag>();
							viewableTag.viewableVisualStyle = ViewableTag.ViewableVisualStyle.Icon;
						}
						viewableTag.viewableName = entry.viewableNode.fullName;
					}
				}
				if (entryStatus >= EntryStatus.Available && component)
				{
					component.onClick.AddListener(delegate()
					{
						this.ViewEntry(entry);
					});
				}
				if (entryStatus == EntryStatus.None)
				{
					if (component)
					{
						component.enabled = false;
						component.targetGraphic.color = this.spaceFillerColor;
					}
					else
					{
						rectTransform.GetComponent<Image>().color = this.spaceFillerColor;
					}
					for (int j = rectTransform.childCount - 1; j >= 0; j--)
					{
						UnityEngine.Object.Destroy(rectTransform.GetChild(j).gameObject);
					}
				}
				if (component && i == 0)
				{
					component.defaultFallbackButton = true;
				}
			}
			gameObject.gameObject.SetActive(true);
			GridLayoutGroup gridLayoutGroup = gameObject.GetComponent<GridLayoutGroup>();
			Action destroyLayoutGroup = null;
			int frameTimer = 2;
			destroyLayoutGroup = delegate()
			{
				int frameTimer;
				frameTimer--;
				frameTimer = frameTimer;
				if (frameTimer <= 0)
				{
					gridLayoutGroup.enabled = false;
					RoR2Application.onLateUpdate -= destroyLayoutGroup;
				}
			};
			RoR2Application.onLateUpdate += destroyLayoutGroup;
			return gameObject;
		}

		// Token: 0x0600518D RID: 20877 RVA: 0x0015102C File Offset: 0x0014F22C
		private void ViewEntry(Entry entry)
		{
			this.OnViewEntry.Invoke();
			LogBookPage component = this.pageViewerPanel.GetComponent<LogBookPage>();
			component.SetEntry(this.LookUpUserProfile(), entry);
			component.modelPanel.SetAnglesForCharacterThumbnailForSeconds(0.5f, false);
			ViewablesCatalog.Node viewableNode = entry.viewableNode;
			ViewableTrigger.TriggerView((viewableNode != null) ? viewableNode.fullName : null);
		}

		// Token: 0x0600518E RID: 20878 RVA: 0x00151083 File Offset: 0x0014F283
		private void ReturnToNavigation()
		{
			this.navigationPanel.SetActive(true);
			this.pageViewerPanel.SetActive(false);
		}

		// Token: 0x0600518F RID: 20879 RVA: 0x00102196 File Offset: 0x00100396
		private static bool UnlockableExists(UnlockableDef unlockableDef)
		{
			return unlockableDef;
		}

		// Token: 0x06005190 RID: 20880 RVA: 0x001510A0 File Offset: 0x0014F2A0
		private static bool IsEntryBodyWithoutLore(in Entry entry)
		{
			CharacterBody characterBody = (CharacterBody)entry.extraData;
			bool flag = false;
			string baseNameToken = characterBody.baseNameToken;
			if (!string.IsNullOrEmpty(baseNameToken))
			{
				string token = baseNameToken.Replace("_NAME", "_LORE");
				if (Language.english.TokenIsRegistered(token))
				{
					flag = true;
				}
			}
			return !flag;
		}

		// Token: 0x06005191 RID: 20881 RVA: 0x001510F4 File Offset: 0x0014F2F4
		private static bool IsEntryPickupItemWithoutLore(in Entry entry)
		{
			ItemDef itemDef = ItemCatalog.GetItemDef(PickupCatalog.GetPickupDef((PickupIndex)entry.extraData).itemIndex);
			return !Language.english.TokenIsRegistered(itemDef.loreToken);
		}

		// Token: 0x06005192 RID: 20882 RVA: 0x00151130 File Offset: 0x0014F330
		private static bool IsEntryPickupEquipmentWithoutLore(in Entry entry)
		{
			EquipmentDef equipmentDef = EquipmentCatalog.GetEquipmentDef(PickupCatalog.GetPickupDef((PickupIndex)entry.extraData).equipmentIndex);
			return !Language.english.TokenIsRegistered(equipmentDef.loreToken);
		}

		// Token: 0x06005193 RID: 20883 RVA: 0x0015116C File Offset: 0x0014F36C
		private static bool CanSelectItemEntry(ItemDef itemDef, Dictionary<ExpansionDef, bool> expansionAvailability)
		{
			if (itemDef != null)
			{
				ItemTierDef itemTierDef = ItemTierCatalog.GetItemTierDef(itemDef.tier);
				return itemTierDef && itemTierDef.isDroppable && (itemDef.requiredExpansion == null || !expansionAvailability.ContainsKey(itemDef.requiredExpansion) || expansionAvailability[itemDef.requiredExpansion]);
			}
			return false;
		}

		// Token: 0x06005194 RID: 20884 RVA: 0x001511CC File Offset: 0x0014F3CC
		private static bool CanSelectEquipmentEntry(EquipmentDef equipmentDef, Dictionary<ExpansionDef, bool> expansionAvailability)
		{
			return equipmentDef != null && equipmentDef.canDrop && (equipmentDef.requiredExpansion == null || !expansionAvailability.ContainsKey(equipmentDef.requiredExpansion) || expansionAvailability[equipmentDef.requiredExpansion]);
		}

		// Token: 0x06005195 RID: 20885 RVA: 0x00151218 File Offset: 0x0014F418
		private static Entry[] BuildPickupEntries(Dictionary<ExpansionDef, bool> expansionAvailability)
		{
			Entry entry = new Entry();
			entry.nameToken = "TOOLTIP_WIP_CONTENT_NAME";
			entry.color = Color.white;
			entry.iconTexture = LegacyResourcesAPI.Load<Texture>("Textures/MiscIcons/texWIPIcon");
			entry.getStatusImplementation = new Entry.GetStatusDelegate(LogBookController.GetUnimplemented);
			entry.getTooltipContentImplementation = new Entry.GetTooltipContentDelegate(LogBookController.GetWIPTooltipContent);
			IEnumerable<Entry> first = from pickupDef in PickupCatalog.allPickups
			select ItemCatalog.GetItemDef(pickupDef.itemIndex) into itemDef
			where LogBookController.CanSelectItemEntry(itemDef, expansionAvailability)
			orderby (int)(itemDef.tier + ((itemDef.tier == ItemTier.Lunar) ? 100 : 0))
			select new Entry
			{
				nameToken = itemDef.nameToken,
				color = ColorCatalog.GetColor(itemDef.darkColorIndex),
				iconTexture = itemDef.pickupIconTexture,
				bgTexture = itemDef.bgIconTexture,
				extraData = PickupCatalog.FindPickupIndex(itemDef.itemIndex),
				modelPrefab = itemDef.pickupModelPrefab,
				getStatusImplementation = new Entry.GetStatusDelegate(LogBookController.GetPickupStatus),
				getTooltipContentImplementation = new Entry.GetTooltipContentDelegate(LogBookController.GetPickupTooltipContent),
				pageBuilderMethod = new Action<PageBuilder>(PageBuilder.SimplePickup),
				isWIPImplementation = new Entry.IsWIPDelegate(LogBookController.IsEntryPickupItemWithoutLore)
			};
			IEnumerable<Entry> second = from pickupDef in PickupCatalog.allPickups
			select EquipmentCatalog.GetEquipmentDef(pickupDef.equipmentIndex) into equipmentDef
			where LogBookController.CanSelectEquipmentEntry(equipmentDef, expansionAvailability)
			orderby !equipmentDef.isLunar
			select new Entry
			{
				nameToken = equipmentDef.nameToken,
				color = ColorCatalog.GetColor(equipmentDef.colorIndex),
				iconTexture = equipmentDef.pickupIconTexture,
				bgTexture = equipmentDef.bgIconTexture,
				extraData = PickupCatalog.FindPickupIndex(equipmentDef.equipmentIndex),
				modelPrefab = equipmentDef.pickupModelPrefab,
				getStatusImplementation = new Entry.GetStatusDelegate(LogBookController.GetPickupStatus),
				getTooltipContentImplementation = new Entry.GetTooltipContentDelegate(LogBookController.GetPickupTooltipContent),
				pageBuilderMethod = new Action<PageBuilder>(PageBuilder.SimplePickup),
				isWIPImplementation = new Entry.IsWIPDelegate(LogBookController.IsEntryPickupEquipmentWithoutLore)
			};
			return first.Concat(second).ToArray<Entry>();
		}

		// Token: 0x06005196 RID: 20886 RVA: 0x00151394 File Offset: 0x0014F594
		private static bool CanSelectMonsterEntry(CharacterBody characterBody, Dictionary<ExpansionDef, bool> expansionAvailability)
		{
			if (!characterBody)
			{
				return false;
			}
			ExpansionRequirementComponent component = characterBody.GetComponent<ExpansionRequirementComponent>();
			if (!component || !component.requiredExpansion || !expansionAvailability.ContainsKey(component.requiredExpansion) || expansionAvailability[component.requiredExpansion])
			{
				DeathRewards component2 = characterBody.GetComponent<DeathRewards>();
				return LogBookController.UnlockableExists((component2 != null) ? component2.logUnlockableDef : null);
			}
			return false;
		}

		// Token: 0x06005197 RID: 20887 RVA: 0x00151400 File Offset: 0x0014F600
		private static Entry[] BuildMonsterEntries(Dictionary<ExpansionDef, bool> expansionAvailability)
		{
			return (from characterBody in BodyCatalog.allBodyPrefabBodyBodyComponents
			where LogBookController.CanSelectMonsterEntry(characterBody, expansionAvailability)
			orderby characterBody.baseMaxHealth
			select characterBody).Select(delegate(CharacterBody characterBody)
			{
				Entry entry = new Entry();
				entry.nameToken = characterBody.baseNameToken;
				entry.color = ColorCatalog.GetColor(ColorCatalog.ColorIndex.HardDifficulty);
				entry.iconTexture = characterBody.portraitIcon;
				entry.extraData = characterBody;
				ModelLocator component = characterBody.GetComponent<ModelLocator>();
				GameObject modelPrefab;
				if (component == null)
				{
					modelPrefab = null;
				}
				else
				{
					Transform modelTransform = component.modelTransform;
					modelPrefab = ((modelTransform != null) ? modelTransform.gameObject : null);
				}
				entry.modelPrefab = modelPrefab;
				entry.getStatusImplementation = new Entry.GetStatusDelegate(LogBookController.GetMonsterStatus);
				entry.getTooltipContentImplementation = new Entry.GetTooltipContentDelegate(LogBookController.GetMonsterTooltipContent);
				entry.pageBuilderMethod = new Action<PageBuilder>(PageBuilder.MonsterBody);
				entry.bgTexture = (characterBody.isChampion ? LegacyResourcesAPI.Load<Texture>("Textures/ItemIcons/BG/texTier3BGIcon") : LegacyResourcesAPI.Load<Texture>("Textures/ItemIcons/BG/texTier1BGIcon"));
				entry.isWIPImplementation = new Entry.IsWIPDelegate(LogBookController.IsEntryBodyWithoutLore);
				return entry;
			}).ToArray<Entry>();
		}

		// Token: 0x06005198 RID: 20888 RVA: 0x00151480 File Offset: 0x0014F680
		private static bool CanSelectStageEntry(SceneDef sceneDef, Dictionary<ExpansionDef, bool> expansionAvailability)
		{
			if (sceneDef)
			{
				ExpansionDef requiredExpansion = sceneDef.requiredExpansion;
				return (!requiredExpansion || !expansionAvailability.ContainsKey(requiredExpansion) || expansionAvailability[requiredExpansion]) && sceneDef.shouldIncludeInLogbook;
			}
			return false;
		}

		// Token: 0x06005199 RID: 20889 RVA: 0x001514C4 File Offset: 0x0014F6C4
		private static Entry[] BuildStageEntries(Dictionary<ExpansionDef, bool> expansionAvailability)
		{
			return (from sceneDef in SceneCatalog.allSceneDefs
			where LogBookController.CanSelectStageEntry(sceneDef, expansionAvailability)
			orderby sceneDef.stageOrder
			select sceneDef).Select(delegate(SceneDef sceneDef)
			{
				Entry entry2 = new Entry();
				entry2.nameToken = sceneDef.nameToken;
				entry2.iconTexture = sceneDef.previewTexture;
				entry2.color = ColorCatalog.GetColor(ColorCatalog.ColorIndex.Tier2ItemDark);
				entry2.getStatusImplementation = new Entry.GetStatusDelegate(LogBookController.GetStageStatus);
				entry2.modelPrefab = sceneDef.dioramaPrefab;
				entry2.getTooltipContentImplementation = new Entry.GetTooltipContentDelegate(LogBookController.GetStageTooltipContent);
				entry2.pageBuilderMethod = new Action<PageBuilder>(PageBuilder.Stage);
				entry2.extraData = sceneDef;
				entry2.isWIPImplementation = delegate(in Entry entry)
				{
					return !Language.english.TokenIsRegistered(((SceneDef)entry.extraData).loreToken);
				};
				return entry2;
			}).ToArray<Entry>();
		}

		// Token: 0x0600519A RID: 20890 RVA: 0x00151548 File Offset: 0x0014F748
		private static bool CanSelectSurvivorBodyEntry(CharacterBody body, Dictionary<ExpansionDef, bool> expansionAvailability)
		{
			if (body)
			{
				ExpansionRequirementComponent component = body.GetComponent<ExpansionRequirementComponent>();
				return !component || !component.requiredExpansion || !expansionAvailability.ContainsKey(component.requiredExpansion) || expansionAvailability[component.requiredExpansion];
			}
			return false;
		}

		// Token: 0x0600519B RID: 20891 RVA: 0x00151598 File Offset: 0x0014F798
		private static Entry[] BuildSurvivorEntries(Dictionary<ExpansionDef, bool> expansionAvailability)
		{
			return (from survivorDef in SurvivorCatalog.orderedSurvivorDefs
			select BodyCatalog.GetBodyPrefabBodyComponent(SurvivorCatalog.GetBodyIndexFromSurvivorIndex(survivorDef.survivorIndex)) into body
			where LogBookController.CanSelectSurvivorBodyEntry(body, expansionAvailability)
			select body).Select(delegate(CharacterBody characterBody)
			{
				Entry entry = new Entry();
				entry.nameToken = characterBody.baseNameToken;
				entry.color = ColorCatalog.GetColor(ColorCatalog.ColorIndex.NormalDifficulty);
				entry.iconTexture = characterBody.portraitIcon;
				entry.bgTexture = LegacyResourcesAPI.Load<Texture>("Textures/ItemIcons/BG/texSurvivorBGIcon");
				entry.extraData = characterBody;
				ModelLocator component = characterBody.GetComponent<ModelLocator>();
				GameObject modelPrefab;
				if (component == null)
				{
					modelPrefab = null;
				}
				else
				{
					Transform modelTransform = component.modelTransform;
					modelPrefab = ((modelTransform != null) ? modelTransform.gameObject : null);
				}
				entry.modelPrefab = modelPrefab;
				entry.getStatusImplementation = new Entry.GetStatusDelegate(LogBookController.GetSurvivorStatus);
				entry.getTooltipContentImplementation = new Entry.GetTooltipContentDelegate(LogBookController.GetSurvivorTooltipContent);
				entry.pageBuilderMethod = new Action<PageBuilder>(PageBuilder.SurvivorBody);
				entry.isWIPImplementation = new Entry.IsWIPDelegate(LogBookController.IsEntryBodyWithoutLore);
				return entry;
			}).ToArray<Entry>();
		}

		// Token: 0x0600519C RID: 20892 RVA: 0x00151618 File Offset: 0x0014F818
		private static bool CanSelectAchievementEntry(AchievementDef achievementDef, Dictionary<ExpansionDef, bool> expansionAvailability)
		{
			if (achievementDef != null)
			{
				UnlockableDef unlockableDef = UnlockableCatalog.GetUnlockableDef(achievementDef.unlockableRewardIdentifier);
				ExpansionDef expansionDefForUnlockable = UnlockableCatalog.GetExpansionDefForUnlockable((unlockableDef != null) ? unlockableDef.index : UnlockableIndex.None);
				return !expansionDefForUnlockable || !expansionAvailability.ContainsKey(expansionDefForUnlockable) || expansionAvailability[expansionDefForUnlockable];
			}
			return false;
		}

		// Token: 0x0600519D RID: 20893 RVA: 0x00151664 File Offset: 0x0014F864
		private static Entry[] BuildAchievementEntries(Dictionary<ExpansionDef, bool> expansionAvailability)
		{
			return (from achievementDef in AchievementManager.allAchievementDefs
			where LogBookController.CanSelectAchievementEntry(achievementDef, expansionAvailability)
			select achievementDef).Select(delegate(AchievementDef achievementDef)
			{
				Entry entry = new Entry();
				entry.nameToken = achievementDef.nameToken;
				entry.color = ColorCatalog.GetColor(ColorCatalog.ColorIndex.NormalDifficulty);
				Sprite achievedIcon = achievementDef.GetAchievedIcon();
				entry.iconTexture = ((achievedIcon != null) ? achievedIcon.texture : null);
				entry.extraData = achievementDef;
				entry.modelPrefab = null;
				entry.getStatusImplementation = new Entry.GetStatusDelegate(LogBookController.GetAchievementStatus);
				entry.getTooltipContentImplementation = new Entry.GetTooltipContentDelegate(LogBookController.GetAchievementTooltipContent);
				return entry;
			}).ToArray<Entry>();
		}

		// Token: 0x0600519E RID: 20894 RVA: 0x001516C4 File Offset: 0x0014F8C4
		private static Entry[] BuildProfileEntries(UserProfile viewerProfile)
		{
			LogBookController.<>c__DisplayClass86_0 CS$<>8__locals1;
			CS$<>8__locals1.entries = new List<Entry>();
			if (true)
			{
				using (List<string>.Enumerator enumerator = PlatformSystems.saveSystem.GetAvailableProfileNames().GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						string profileName = enumerator.Current;
						LogBookController.<BuildProfileEntries>g__AddProfileStatsEntry|86_0(PlatformSystems.saveSystem.GetProfile(profileName), ref CS$<>8__locals1);
					}
					goto IL_5F;
				}
			}
			if (viewerProfile != null)
			{
				LogBookController.<BuildProfileEntries>g__AddProfileStatsEntry|86_0(viewerProfile, ref CS$<>8__locals1);
			}
			IL_5F:
			return CS$<>8__locals1.entries.ToArray();
		}

		// Token: 0x0600519F RID: 20895 RVA: 0x0015174C File Offset: 0x0014F94C
		private static Entry[] BuildMorgueEntries(UserProfile viewerProfile)
		{
			LogBookController.<>c__DisplayClass87_0 CS$<>8__locals1 = new LogBookController.<>c__DisplayClass87_0();
			CS$<>8__locals1.viewerProfile = viewerProfile;
			CS$<>8__locals1.entries = CollectionPool<Entry, List<Entry>>.RentCollection();
			List<RunReport> list = CollectionPool<RunReport, List<RunReport>>.RentCollection();
			MorgueManager.LoadHistoryRunReports(list);
			foreach (RunReport runReport in list)
			{
				CS$<>8__locals1.<BuildMorgueEntries>g__AddRunReportEntry|0(runReport);
			}
			CollectionPool<RunReport, List<RunReport>>.ReturnCollection(list);
			Entry[] result = CS$<>8__locals1.entries.ToArray();
			CollectionPool<Entry, List<Entry>>.ReturnCollection(CS$<>8__locals1.entries);
			return result;
		}

		// Token: 0x060051A0 RID: 20896 RVA: 0x001517E0 File Offset: 0x0014F9E0
		private static CategoryDef[] BuildCategories()
		{
			Dictionary<ExpansionDef, bool> dictionary = new Dictionary<ExpansionDef, bool>();
			foreach (ExpansionDef expansionDef in ExpansionCatalog.expansionDefs)
			{
				dictionary.Add(expansionDef, !expansionDef.requiredEntitlement || EntitlementManager.localUserEntitlementTracker.AnyUserHasEntitlement(expansionDef.requiredEntitlement));
			}
			Entry[] pickupEntries = LogBookController.BuildPickupEntries(dictionary);
			Entry[] monsterEntries = LogBookController.BuildMonsterEntries(dictionary);
			Entry[] stageEntries = LogBookController.BuildStageEntries(dictionary);
			Entry[] survivorEntries = LogBookController.BuildSurvivorEntries(dictionary);
			Entry[] achievementEntries = LogBookController.BuildAchievementEntries(dictionary);
			return new CategoryDef[]
			{
				new CategoryDef
				{
					nameToken = "LOGBOOK_CATEGORY_ITEMANDEQUIPMENT",
					iconPrefab = LegacyResourcesAPI.Load<GameObject>("Prefabs/UI/Logbook/ItemEntryIcon"),
					buildEntries = ((UserProfile viewerProfile) => pickupEntries)
				},
				new CategoryDef
				{
					nameToken = "LOGBOOK_CATEGORY_MONSTER",
					iconPrefab = LegacyResourcesAPI.Load<GameObject>("Prefabs/UI/Logbook/MonsterEntryIcon"),
					buildEntries = ((UserProfile viewerProfile) => monsterEntries)
				},
				new CategoryDef
				{
					nameToken = "LOGBOOK_CATEGORY_STAGE",
					iconPrefab = LegacyResourcesAPI.Load<GameObject>("Prefabs/UI/Logbook/StageEntryIcon"),
					buildEntries = ((UserProfile viewerProfile) => stageEntries)
				},
				new CategoryDef
				{
					nameToken = "LOGBOOK_CATEGORY_SURVIVOR",
					iconPrefab = LegacyResourcesAPI.Load<GameObject>("Prefabs/UI/Logbook/SurvivorEntryIcon"),
					buildEntries = ((UserProfile viewerProfile) => survivorEntries)
				},
				new CategoryDef
				{
					nameToken = "LOGBOOK_CATEGORY_ACHIEVEMENTS",
					iconPrefab = LegacyResourcesAPI.Load<GameObject>("Prefabs/UI/Logbook/AchievementEntryIcon"),
					buildEntries = ((UserProfile viewerProfile) => achievementEntries),
					initializeElementGraphics = new Action<GameObject, Entry, EntryStatus, UserProfile>(CategoryDef.InitializeChallenge)
				},
				new CategoryDef
				{
					nameToken = "LOGBOOK_CATEGORY_STATS",
					iconPrefab = LegacyResourcesAPI.Load<GameObject>("Prefabs/UI/Logbook/StatsEntryIcon"),
					buildEntries = new CategoryDef.BuildEntriesDelegate(LogBookController.BuildProfileEntries),
					initializeElementGraphics = new Action<GameObject, Entry, EntryStatus, UserProfile>(CategoryDef.InitializeStats)
				},
				new CategoryDef
				{
					nameToken = "LOGBOOK_CATEGORY_MORGUE",
					iconPrefab = LegacyResourcesAPI.Load<GameObject>("Prefabs/UI/Logbook/MorgueEntryIcon"),
					buildEntries = new CategoryDef.BuildEntriesDelegate(LogBookController.BuildMorgueEntries),
					initializeElementGraphics = new Action<GameObject, Entry, EntryStatus, UserProfile>(CategoryDef.InitializeMorgue)
				}
			};
		}

		// Token: 0x060051A1 RID: 20897 RVA: 0x00151A50 File Offset: 0x0014FC50
		[ConCommand(commandName = "logbook_list_unfinished_lore", flags = ConVarFlags.None, helpText = "Prints all logbook entries which still have undefined lore.")]
		private static void CCLogbookListUnfinishedLore(ConCommandArgs args)
		{
			List<string> list = new List<string>();
			foreach (CategoryDef categoryDef in LogBookController.categories)
			{
				LocalUser senderLocalUser = args.GetSenderLocalUser();
				foreach (Entry entry in categoryDef.BuildEntries((senderLocalUser != null) ? senderLocalUser.userProfile : null))
				{
					string text = "";
					UnityEngine.Object @object;
					if ((@object = (entry.extraData as UnityEngine.Object)) != null)
					{
						text = @object.name;
					}
					if (entry.isWip)
					{
						List<string> list2 = list;
						string[] array3 = new string[6];
						int num = 0;
						object extraData = entry.extraData;
						string text2;
						if (extraData == null)
						{
							text2 = null;
						}
						else
						{
							Type type = extraData.GetType();
							text2 = ((type != null) ? type.Name : null);
						}
						array3[num] = text2;
						array3[1] = " \"";
						array3[2] = text;
						array3[3] = "\" \"";
						array3[4] = Language.GetString(entry.nameToken);
						array3[5] = "\"";
						list2.Add(string.Concat(array3));
					}
				}
			}
			args.Log(string.Join("\n", list));
		}

		// Token: 0x060051A2 RID: 20898 RVA: 0x00151B54 File Offset: 0x0014FD54
		private static void RegisterViewables(CategoryDef[] categoriesToGenerateFrom)
		{
			ViewablesCatalog.Node node = new ViewablesCatalog.Node("Logbook", true, null);
			foreach (CategoryDef categoryDef in LogBookController.categories)
			{
				ViewablesCatalog.Node node2 = new ViewablesCatalog.Node(categoryDef.nameToken, true, node);
				categoryDef.viewableNode = node2;
				Entry[] array2 = categoryDef.BuildEntries(null);
				for (int j = 0; j < array2.Length; j++)
				{
					LogBookController.<>c__DisplayClass95_0 CS$<>8__locals1 = new LogBookController.<>c__DisplayClass95_0();
					CS$<>8__locals1.entry = array2[j];
					LogBookController.<>c__DisplayClass95_1 CS$<>8__locals2 = new LogBookController.<>c__DisplayClass95_1();
					CS$<>8__locals2.CS$<>8__locals1 = CS$<>8__locals1;
					string nameToken = CS$<>8__locals2.CS$<>8__locals1.entry.nameToken;
					if (!CS$<>8__locals2.CS$<>8__locals1.entry.isWip && !(nameToken == "TOOLTIP_WIP_CONTENT_NAME") && !string.IsNullOrEmpty(nameToken))
					{
						CS$<>8__locals2.entryNode = new ViewablesCatalog.Node(nameToken, false, node2);
						if ((CS$<>8__locals2.achievementDef = (CS$<>8__locals2.CS$<>8__locals1.entry.extraData as AchievementDef)) != null)
						{
							LogBookController.<>c__DisplayClass95_2 CS$<>8__locals3 = new LogBookController.<>c__DisplayClass95_2();
							CS$<>8__locals3.CS$<>8__locals2 = CS$<>8__locals2;
							CS$<>8__locals3.hasPrereq = !string.IsNullOrEmpty(CS$<>8__locals3.CS$<>8__locals2.achievementDef.prerequisiteAchievementIdentifier);
							CS$<>8__locals3.CS$<>8__locals2.entryNode.shouldShowUnviewed = new Func<UserProfile, bool>(CS$<>8__locals3.<RegisterViewables>g__Check|1);
						}
						else
						{
							CS$<>8__locals2.entryNode.shouldShowUnviewed = new Func<UserProfile, bool>(CS$<>8__locals2.<RegisterViewables>g__Check|0);
						}
						CS$<>8__locals2.CS$<>8__locals1.entry.viewableNode = CS$<>8__locals2.entryNode;
					}
				}
			}
			ViewablesCatalog.AddNodeToRoot(node);
		}

		// Token: 0x060051A5 RID: 20901 RVA: 0x00151D30 File Offset: 0x0014FF30
		[CompilerGenerated]
		internal static void <BuildProfileEntries>g__AddProfileStatsEntry|86_0(UserProfile userProfile, ref LogBookController.<>c__DisplayClass86_0 A_1)
		{
			Entry entry2 = new Entry();
			entry2.pageBuilderMethod = new Action<PageBuilder>(PageBuilder.StatsPanel);
			entry2.getStatusImplementation = delegate(in Entry entry, UserProfile _viewerProfile)
			{
				return EntryStatus.Available;
			};
			entry2.extraData = userProfile;
			entry2.getDisplayNameImplementation = delegate(in Entry entry, UserProfile _viewerProfile)
			{
				return ((UserProfile)entry.extraData).name;
			};
			entry2.iconTexture = userProfile.portraitTexture;
			A_1.entries.Add(entry2);
		}

		// Token: 0x04004DD0 RID: 19920
		[Header("Navigation")]
		public GameObject navigationPanel;

		// Token: 0x04004DD1 RID: 19921
		public RectTransform categoryContainer;

		// Token: 0x04004DD2 RID: 19922
		public GameObject categorySpaceFiller;

		// Token: 0x04004DD3 RID: 19923
		public int categorySpaceFillerCount;

		// Token: 0x04004DD4 RID: 19924
		public Color spaceFillerColor;

		// Token: 0x04004DD5 RID: 19925
		private UIElementAllocator<MPButton> navigationCategoryButtonAllocator;

		// Token: 0x04004DD6 RID: 19926
		public RectTransform entryPageContainer;

		// Token: 0x04004DD7 RID: 19927
		public GameObject entryPagePrefab;

		// Token: 0x04004DD8 RID: 19928
		public RectTransform navigationPageIndicatorContainer;

		// Token: 0x04004DD9 RID: 19929
		public GameObject navigationPageIndicatorPrefab;

		// Token: 0x04004DDA RID: 19930
		public bool moveNavigationPageIndicatorContainerToCategoryButton;

		// Token: 0x04004DDB RID: 19931
		private UIElementAllocator<MPButton> navigationPageIndicatorAllocator;

		// Token: 0x04004DDC RID: 19932
		public MPButton previousPageButton;

		// Token: 0x04004DDD RID: 19933
		public MPButton nextPageButton;

		// Token: 0x04004DDE RID: 19934
		public LanguageTextMeshController currentCategoryLabel;

		// Token: 0x04004DDF RID: 19935
		private RectTransform categoryHightlightRect;

		// Token: 0x04004DE0 RID: 19936
		[Header("PageViewer")]
		public UnityEvent OnViewEntry;

		// Token: 0x04004DE1 RID: 19937
		public GameObject pageViewerPanel;

		// Token: 0x04004DE2 RID: 19938
		public MPButton pageViewerBackButton;

		// Token: 0x04004DE3 RID: 19939
		[Header("Misc")]
		public GameObject categoryButtonPrefab;

		// Token: 0x04004DE4 RID: 19940
		public GameObject headerHighlightPrefab;

		// Token: 0x04004DE5 RID: 19941
		public LanguageTextMeshController hoverLanguageTextMeshController;

		// Token: 0x04004DE6 RID: 19942
		public string hoverDescriptionFormatString;

		// Token: 0x04004DE7 RID: 19943
		private EntityStateMachine stateMachine;

		// Token: 0x04004DE8 RID: 19944
		private UILayerKey uiLayerKey;

		// Token: 0x04004DE9 RID: 19945
		public static CategoryDef[] categories = Array.Empty<CategoryDef>();

		// Token: 0x04004DEA RID: 19946
		public static ResourceAvailability availability = default(ResourceAvailability);

		// Token: 0x04004DEB RID: 19947
		private static bool IsInitialized = false;

		// Token: 0x04004DEC RID: 19948
		private LogBookController.NavigationPageInfo[] _availableNavigationPages = Array.Empty<LogBookController.NavigationPageInfo>();

		// Token: 0x04004DED RID: 19949
		private GameObject currentEntriesPageObject;

		// Token: 0x04004DEE RID: 19950
		private int currentCategoryIndex;

		// Token: 0x04004DEF RID: 19951
		private int desiredCategoryIndex;

		// Token: 0x04004DF0 RID: 19952
		private int currentPageIndex;

		// Token: 0x04004DF1 RID: 19953
		private int desiredPageIndex;

		// Token: 0x04004DF2 RID: 19954
		private bool goToEndOfNextCategory;

		// Token: 0x04004DF3 RID: 19955
		private LogBookController.NavigationPageInfo[] allNavigationPages;

		// Token: 0x04004DF4 RID: 19956
		private LogBookController.NavigationPageInfo[][] navigationPagesByCategory;

		// Token: 0x02000DDD RID: 3549
		private class NavigationPageInfo
		{
			// Token: 0x04004DF5 RID: 19957
			public CategoryDef categoryDef;

			// Token: 0x04004DF6 RID: 19958
			public Entry[] entries;

			// Token: 0x04004DF7 RID: 19959
			public int index;

			// Token: 0x04004DF8 RID: 19960
			public int indexInCategory;
		}

		// Token: 0x02000DDE RID: 3550
		private class LogBookState : EntityState
		{
			// Token: 0x060051A7 RID: 20903 RVA: 0x00151DBE File Offset: 0x0014FFBE
			public override void OnEnter()
			{
				base.OnEnter();
				this.logBookController = base.GetComponent<LogBookController>();
			}

			// Token: 0x060051A8 RID: 20904 RVA: 0x00151DD2 File Offset: 0x0014FFD2
			public override void Update()
			{
				base.Update();
				this.unscaledAge += Time.unscaledDeltaTime;
			}

			// Token: 0x04004DF9 RID: 19961
			protected LogBookController logBookController;

			// Token: 0x04004DFA RID: 19962
			protected float unscaledAge;
		}

		// Token: 0x02000DDF RID: 3551
		private class FadeState : LogBookController.LogBookState
		{
			// Token: 0x060051AA RID: 20906 RVA: 0x00151DEC File Offset: 0x0014FFEC
			public override void OnEnter()
			{
				base.OnEnter();
				this.canvasGroup = base.GetComponent<CanvasGroup>();
				if (this.canvasGroup)
				{
					this.canvasGroup.alpha = 0f;
				}
			}

			// Token: 0x060051AB RID: 20907 RVA: 0x00151E1D File Offset: 0x0015001D
			public override void OnExit()
			{
				if (this.canvasGroup)
				{
					this.canvasGroup.alpha = this.endValue;
				}
				base.OnExit();
			}

			// Token: 0x060051AC RID: 20908 RVA: 0x00151E44 File Offset: 0x00150044
			public override void Update()
			{
				if (this.canvasGroup)
				{
					this.canvasGroup.alpha = this.unscaledAge / this.duration;
					if (this.canvasGroup.alpha >= 1f)
					{
						this.outer.SetNextState(new Idle());
					}
				}
				base.Update();
			}

			// Token: 0x04004DFB RID: 19963
			private CanvasGroup canvasGroup;

			// Token: 0x04004DFC RID: 19964
			public float duration = 0.5f;

			// Token: 0x04004DFD RID: 19965
			public float endValue;
		}

		// Token: 0x02000DE0 RID: 3552
		private class ChangeEntriesPageState : LogBookController.LogBookState
		{
			// Token: 0x060051AE RID: 20910 RVA: 0x00151EB4 File Offset: 0x001500B4
			public override void OnEnter()
			{
				base.OnEnter();
				if (this.logBookController)
				{
					this.oldPageIndex = this.logBookController.currentPageIndex;
					this.oldPage = this.logBookController.currentEntriesPageObject;
					this.newPage = this.logBookController.BuildEntriesPage(this.newNavigationPageInfo);
					this.containerSize = this.logBookController.entryPageContainer.rect.size;
				}
				this.SetPagePositions(0f);
			}

			// Token: 0x060051AF RID: 20911 RVA: 0x00151F38 File Offset: 0x00150138
			public override void OnExit()
			{
				base.OnExit();
				EntityState.Destroy(this.oldPage);
				if (this.logBookController)
				{
					this.logBookController.currentEntriesPageObject = this.newPage;
					this.logBookController.currentPageIndex = this.newNavigationPageInfo.indexInCategory;
				}
			}

			// Token: 0x060051B0 RID: 20912 RVA: 0x00151F8C File Offset: 0x0015018C
			private void SetPagePositions(float t)
			{
				Vector2 vector = new Vector2(this.containerSize.x * -this.moveDirection.x, this.containerSize.y * this.moveDirection.y);
				Vector2 vector2 = vector * t;
				if (this.oldPage)
				{
					this.oldPage.transform.localPosition = vector2;
				}
				if (this.newPage)
				{
					this.newPage.transform.localPosition = vector2 - vector;
				}
			}

			// Token: 0x060051B1 RID: 20913 RVA: 0x00152024 File Offset: 0x00150224
			public override void Update()
			{
				base.Update();
				float num = Mathf.Clamp01(this.unscaledAge / this.duration);
				this.SetPagePositions(num);
				if (num == 1f)
				{
					this.outer.SetNextState(new Idle());
				}
			}

			// Token: 0x04004DFE RID: 19966
			private int oldPageIndex;

			// Token: 0x04004DFF RID: 19967
			public LogBookController.NavigationPageInfo newNavigationPageInfo;

			// Token: 0x04004E00 RID: 19968
			public float duration = 0.1f;

			// Token: 0x04004E01 RID: 19969
			public Vector2 moveDirection;

			// Token: 0x04004E02 RID: 19970
			private GameObject oldPage;

			// Token: 0x04004E03 RID: 19971
			private GameObject newPage;

			// Token: 0x04004E04 RID: 19972
			private Vector2 oldPageTargetPosition;

			// Token: 0x04004E05 RID: 19973
			private Vector2 newPageTargetPosition;

			// Token: 0x04004E06 RID: 19974
			private Vector2 containerSize = Vector2.zero;
		}

		// Token: 0x02000DE1 RID: 3553
		private class ChangeCategoryState : LogBookController.LogBookState
		{
			// Token: 0x060051B3 RID: 20915 RVA: 0x00152088 File Offset: 0x00150288
			public override void OnEnter()
			{
				base.OnEnter();
				if (this.logBookController)
				{
					this.oldCategoryIndex = this.logBookController.currentCategoryIndex;
					this.oldPage = this.logBookController.currentEntriesPageObject;
					this.newNavigationPages = this.logBookController.GetCategoryPages(this.newCategoryIndex);
					this.destinationPageIndex = this.newNavigationPages[0].index;
					if (this.goToLastPage)
					{
						this.destinationPageIndex = this.newNavigationPages[this.newNavigationPages.Length - 1].index;
						Debug.LogFormat("goToLastPage=true destinationPageIndex={0}", new object[]
						{
							this.destinationPageIndex
						});
					}
					this.newNavigationPageInfo = this.logBookController.allNavigationPages[this.destinationPageIndex];
					this.newPage = this.logBookController.BuildEntriesPage(this.newNavigationPageInfo);
					this.containerSize = this.logBookController.entryPageContainer.rect.size;
					this.moveDirection = new Vector2(Mathf.Sign((float)(this.newCategoryIndex - this.oldCategoryIndex)), 0f);
				}
				this.SetPagePositions(0f);
			}

			// Token: 0x060051B4 RID: 20916 RVA: 0x001521B4 File Offset: 0x001503B4
			public override void OnExit()
			{
				EntityState.Destroy(this.oldPage);
				if (this.logBookController)
				{
					this.logBookController.currentEntriesPageObject = this.newPage;
					this.logBookController.currentPageIndex = this.newNavigationPageInfo.indexInCategory;
					this.logBookController.desiredPageIndex = this.newNavigationPageInfo.indexInCategory;
					this.logBookController.currentCategoryIndex = this.newCategoryIndex;
					this.logBookController.availableNavigationPages = this.newNavigationPages;
					this.logBookController.currentCategoryLabel.token = LogBookController.categories[this.newCategoryIndex].nameToken;
					this.logBookController.categoryHightlightRect.SetParent(this.logBookController.navigationCategoryButtonAllocator.elements[this.newCategoryIndex].transform, false);
					this.logBookController.categoryHightlightRect.gameObject.SetActive(false);
					this.logBookController.categoryHightlightRect.gameObject.SetActive(true);
					if (this.logBookController.moveNavigationPageIndicatorContainerToCategoryButton)
					{
						this.logBookController.navigationPageIndicatorContainer.SetParent(this.logBookController.navigationCategoryButtonAllocator.elements[this.newCategoryIndex].transform, false);
					}
				}
				base.OnExit();
			}

			// Token: 0x060051B5 RID: 20917 RVA: 0x00152300 File Offset: 0x00150500
			private void SetPagePositions(float t)
			{
				Vector2 vector = new Vector2(this.containerSize.x * -this.moveDirection.x, this.containerSize.y * this.moveDirection.y);
				Vector2 vector2 = vector * t;
				if (this.oldPage)
				{
					this.oldPage.transform.localPosition = vector2;
				}
				if (this.newPage)
				{
					this.newPage.transform.localPosition = vector2 - vector;
					if (this.frame == 4)
					{
						this.newPage.GetComponent<GridLayoutGroup>().enabled = false;
					}
				}
			}

			// Token: 0x060051B6 RID: 20918 RVA: 0x001523B4 File Offset: 0x001505B4
			public override void Update()
			{
				base.Update();
				this.frame++;
				float num = Mathf.Clamp01(this.unscaledAge / this.duration);
				this.SetPagePositions(num);
				if (num == 1f)
				{
					this.outer.SetNextState(new Idle());
				}
			}

			// Token: 0x04004E07 RID: 19975
			private int oldCategoryIndex;

			// Token: 0x04004E08 RID: 19976
			public int newCategoryIndex;

			// Token: 0x04004E09 RID: 19977
			public bool goToLastPage;

			// Token: 0x04004E0A RID: 19978
			public float duration = 0.1f;

			// Token: 0x04004E0B RID: 19979
			private GameObject oldPage;

			// Token: 0x04004E0C RID: 19980
			private GameObject newPage;

			// Token: 0x04004E0D RID: 19981
			private Vector2 oldPageTargetPosition;

			// Token: 0x04004E0E RID: 19982
			private Vector2 newPageTargetPosition;

			// Token: 0x04004E0F RID: 19983
			private Vector2 moveDirection;

			// Token: 0x04004E10 RID: 19984
			private Vector2 containerSize = Vector2.zero;

			// Token: 0x04004E11 RID: 19985
			private LogBookController.NavigationPageInfo[] newNavigationPages;

			// Token: 0x04004E12 RID: 19986
			private int destinationPageIndex;

			// Token: 0x04004E13 RID: 19987
			private LogBookController.NavigationPageInfo newNavigationPageInfo;

			// Token: 0x04004E14 RID: 19988
			private int frame;
		}

		// Token: 0x02000DE2 RID: 3554
		private class EnterLogViewState : LogBookController.LogBookState
		{
			// Token: 0x060051B8 RID: 20920 RVA: 0x00152428 File Offset: 0x00150628
			public override void OnEnter()
			{
				base.OnEnter();
				this.flyingIcon = new GameObject("FlyingIcon", new Type[]
				{
					typeof(RectTransform),
					typeof(CanvasRenderer),
					typeof(RawImage)
				});
				this.flyingIconTransform = (RectTransform)this.flyingIcon.transform;
				this.flyingIconTransform.SetParent(this.logBookController.transform, false);
				this.flyingIconTransform.localScale = Vector3.one;
				this.flyingIconImage = this.flyingIconTransform.GetComponent<RawImage>();
				this.flyingIconImage.texture = this.iconTexture;
				Vector3[] array = new Vector3[4];
				this.startRectTransform.GetWorldCorners(array);
				this.startRect = this.GetRectRelativeToParent(array);
				this.midRect = new Rect(((RectTransform)this.logBookController.transform).rect.center, this.startRect.size);
				this.endRectTransform.GetWorldCorners(array);
				this.endRect = this.GetRectRelativeToParent(array);
				this.SetIconRect(this.startRect);
			}

			// Token: 0x060051B9 RID: 20921 RVA: 0x0015254F File Offset: 0x0015074F
			private void SetIconRect(Rect rect)
			{
				this.flyingIconTransform.position = rect.position;
				this.flyingIconTransform.offsetMin = rect.min;
				this.flyingIconTransform.offsetMax = rect.max;
			}

			// Token: 0x060051BA RID: 20922 RVA: 0x0015258C File Offset: 0x0015078C
			private Rect GetRectRelativeToParent(Vector3[] corners)
			{
				for (int i = 0; i < 4; i++)
				{
					corners[i] = this.logBookController.transform.InverseTransformPoint(corners[i]);
				}
				return new Rect
				{
					xMin = corners[0].x,
					xMax = corners[2].x,
					yMin = corners[0].y,
					yMax = corners[2].y
				};
			}

			// Token: 0x060051BB RID: 20923 RVA: 0x00152618 File Offset: 0x00150818
			private static Rect RectFromWorldCorners(Vector3[] corners)
			{
				return new Rect
				{
					xMin = corners[0].x,
					xMax = corners[2].x,
					yMin = corners[0].y,
					yMax = corners[2].y
				};
			}

			// Token: 0x060051BC RID: 20924 RVA: 0x0015267C File Offset: 0x0015087C
			private static Rect LerpRect(Rect a, Rect b, float t)
			{
				return new Rect
				{
					min = Vector2.LerpUnclamped(a.min, b.min, t),
					max = Vector2.LerpUnclamped(a.max, b.max, t)
				};
			}

			// Token: 0x060051BD RID: 20925 RVA: 0x001526C8 File Offset: 0x001508C8
			public override void OnExit()
			{
				EntityState.Destroy(this.flyingIcon);
				base.OnExit();
			}

			// Token: 0x060051BE RID: 20926 RVA: 0x001526DC File Offset: 0x001508DC
			public override void Update()
			{
				base.Update();
				float num = Mathf.Min(this.unscaledAge / this.duration, 1f);
				if (num < 0.1f)
				{
					Util.Remap(num, 0f, 0.1f, 0f, 1f);
					this.SetIconRect(this.startRect);
				}
				if (num < 0.2f)
				{
					float t = Util.Remap(num, 0.1f, 0.2f, 0f, 1f);
					this.SetIconRect(LogBookController.EnterLogViewState.LerpRect(this.startRect, this.midRect, t));
					return;
				}
				if (num < 0.4f)
				{
					Util.Remap(num, 0.2f, 0.4f, 0f, 1f);
					this.SetIconRect(this.midRect);
					return;
				}
				if (num < 0.6f)
				{
					float t2 = Util.Remap(num, 0.4f, 0.6f, 0f, 1f);
					this.SetIconRect(LogBookController.EnterLogViewState.LerpRect(this.midRect, this.endRect, t2));
					return;
				}
				if (num < 1f)
				{
					float num2 = Util.Remap(num, 0.6f, 1f, 0f, 1f);
					this.flyingIconImage.color = new Color(1f, 1f, 1f, 1f - num2);
					this.SetIconRect(this.endRect);
					if (!this.submittedViewEntry)
					{
						this.submittedViewEntry = true;
						this.logBookController.ViewEntry(this.entry);
						return;
					}
				}
				else
				{
					this.outer.SetNextState(new Idle());
				}
			}

			// Token: 0x04004E15 RID: 19989
			public Texture iconTexture;

			// Token: 0x04004E16 RID: 19990
			public RectTransform startRectTransform;

			// Token: 0x04004E17 RID: 19991
			public RectTransform endRectTransform;

			// Token: 0x04004E18 RID: 19992
			public Entry entry;

			// Token: 0x04004E19 RID: 19993
			private GameObject flyingIcon;

			// Token: 0x04004E1A RID: 19994
			private RectTransform flyingIconTransform;

			// Token: 0x04004E1B RID: 19995
			private RawImage flyingIconImage;

			// Token: 0x04004E1C RID: 19996
			private float duration = 0.75f;

			// Token: 0x04004E1D RID: 19997
			private Rect startRect;

			// Token: 0x04004E1E RID: 19998
			private Rect midRect;

			// Token: 0x04004E1F RID: 19999
			private Rect endRect;

			// Token: 0x04004E20 RID: 20000
			private bool submittedViewEntry;
		}
	}
}
