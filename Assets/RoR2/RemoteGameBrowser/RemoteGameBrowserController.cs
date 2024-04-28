using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using HG;
using JetBrains.Annotations;
using RoR2.UI;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace RoR2.RemoteGameBrowser
{
	// Token: 0x02000ADA RID: 2778
	public class RemoteGameBrowserController : MonoBehaviour
	{
		// Token: 0x06003FD8 RID: 16344 RVA: 0x00107C74 File Offset: 0x00105E74
		private void Awake()
		{
			this.cardAllocator = new UIElementAllocator<RemoteGameCardController>(this.cardContainer, this.cardPrefab, true, false);
			this.serverRemoteGameProvider = new SteamworksServerRemoteGameProvider(SteamworksServerRemoteGameProvider.Mode.Internet)
			{
				refreshOnFiltersChanged = true
			};
			if (PlatformSystems.ShouldUseEpicOnlineSystems)
			{
				this.lobbyRemoteGameProvider = new EOSLobbyRemoteGameProvider();
			}
			else
			{
				this.lobbyRemoteGameProvider = new SteamworksLobbyRemoteGameProvider();
			}
			this.aggregateRemoteGameProvider = new AggregateRemoteGameProvider();
			this.advancedFilterRemoteGameProvider = new AdvancedFilterRemoteGameProvider(this.aggregateRemoteGameProvider);
			this.sortRemoteGameProvider = new SortRemoteGameProvider(this.advancedFilterRemoteGameProvider);
			this.pageRemoteGameProvider = new PageRemoteGameProvider(this.sortRemoteGameProvider);
			this.primaryRemoteGameProvider = this.pageRemoteGameProvider;
			this.primaryRemoteGameProvider.onNewInfoAvailable += this.OnNewInfoAvailable;
			this.previousPageButton.onClick.AddListener(new UnityAction(this.OnPreviousPageButtonClick));
			this.nextPageButton.onClick.AddListener(new UnityAction(this.OnNextPageButtonClick));
			this.cardPrefabHeight = this.cardPrefab.GetComponent<LayoutElement>().preferredHeight;
		}

		// Token: 0x06003FD9 RID: 16345 RVA: 0x00107D7B File Offset: 0x00105F7B
		private void Start()
		{
			this.filters = new RemoteGameBrowserController.FilterManager(this);
			this.initialRequestTime = Time.unscaledTime + 0.2f;
		}

		// Token: 0x06003FDA RID: 16346 RVA: 0x00107D9C File Offset: 0x00105F9C
		private void Update()
		{
			if (Input.GetKeyDown(KeyCode.F5) || this.initialRequestTime <= Time.unscaledTime)
			{
				this.initialRequestTime = float.PositiveInfinity;
				this.RequestRefresh();
			}
			this.UpdateSearchFiltersInternal();
			this.UpdateSorting();
			float height = this.cardContainer.rect.height;
			float num = this.cardPrefabHeight;
			int gamesPerPage = Mathf.FloorToInt(height / num);
			this.pageRemoteGameProvider.SetGamesPerPage(gamesPerPage);
			this.previousPageButton.interactable = this.pageRemoteGameProvider.CanGoToPreviousPage();
			this.nextPageButton.interactable = this.pageRemoteGameProvider.CanGoToNextPage();
			if (this.displayDataDirty)
			{
				this.SetDisplayData(this.primaryRemoteGameProvider.GetKnownGames());
			}
			this.UpdateBusyIcon();
		}

		// Token: 0x06003FDB RID: 16347 RVA: 0x00107E58 File Offset: 0x00106058
		private void OnDestroy()
		{
			this.pageRemoteGameProvider.Dispose();
			this.sortRemoteGameProvider.Dispose();
			this.advancedFilterRemoteGameProvider.Dispose();
			this.aggregateRemoteGameProvider.Dispose();
			this.lobbyRemoteGameProvider.Dispose();
			this.serverRemoteGameProvider.Dispose();
		}

		// Token: 0x06003FDC RID: 16348 RVA: 0x00107EA7 File Offset: 0x001060A7
		private void OnEnable()
		{
			this.primaryRemoteGameProvider.RequestRefresh();
			this.RebuildSortTypeDropdown();
		}

		// Token: 0x06003FDD RID: 16349 RVA: 0x00107EBB File Offset: 0x001060BB
		private void OnPreviousPageButtonClick()
		{
			this.pageRemoteGameProvider.GoToPreviousPage();
		}

		// Token: 0x06003FDE RID: 16350 RVA: 0x00107EC9 File Offset: 0x001060C9
		private void OnNextPageButtonClick()
		{
			this.pageRemoteGameProvider.GoToNextPage();
		}

		// Token: 0x06003FDF RID: 16351 RVA: 0x00107ED7 File Offset: 0x001060D7
		public void RequestRefresh()
		{
			this.primaryRemoteGameProvider.RequestRefresh();
		}

		// Token: 0x06003FE0 RID: 16352 RVA: 0x00107EE5 File Offset: 0x001060E5
		private void OnNewInfoAvailable()
		{
			this.displayDataDirty = true;
		}

		// Token: 0x06003FE1 RID: 16353 RVA: 0x00107EF0 File Offset: 0x001060F0
		private void SetDisplayData(IList<RemoteGameInfo> remoteGameInfos)
		{
			this.displayDataDirty = false;
			this.cardAllocator.AllocateElements(remoteGameInfos.Count);
			ReadOnlyCollection<RemoteGameCardController> elements = this.cardAllocator.elements;
			for (int i = 0; i < elements.Count; i++)
			{
				elements[i].SetDisplayData(remoteGameInfos[i]);
			}
			if (this.pageNumberLabel)
			{
				StringBuilder stringBuilder = HG.StringBuilderPool.RentStringBuilder();
				int num;
				int val;
				this.pageRemoteGameProvider.GetCurrentPageInfo(out num, out val);
				stringBuilder.AppendInt(num + 1, 1U, uint.MaxValue).Append("/").AppendInt(Math.Max(val, 1), 1U, uint.MaxValue);
				this.pageNumberLabel.SetText(stringBuilder);
				HG.StringBuilderPool.ReturnStringBuilder(stringBuilder);
			}
		}

		// Token: 0x06003FE2 RID: 16354 RVA: 0x00107FA0 File Offset: 0x001061A0
		private void UpdateSearchFiltersInternal()
		{
			this.filters.CopyUIValuesToInternal();
			if (!PlatformSystems.ShouldUseEpicOnlineSystems)
			{
				this.aggregateRemoteGameProvider.SetProviderAdded(this.serverRemoteGameProvider, this.filters.showDedicatedServers.value.boolValue);
				SteamworksServerRemoteGameProvider.SearchFilters searchFilters = this.serverRemoteGameProvider.GetSearchFilters();
				searchFilters.allowDedicatedServers = this.filters.showDedicatedServers.value.boolValue;
				searchFilters.allowListenServers = true;
				searchFilters.mustNotBeFull = this.filters.mustHaveEnoughSlots.value.boolValue;
				searchFilters.mustHavePlayers = this.filters.mustHavePlayers.value.boolValue;
				searchFilters.requiredTags = this.filters.requiredTags.value.stringValue;
				searchFilters.forbiddenTags = this.filters.forbiddenTags.value.stringValue;
				searchFilters.allowInProgressGames = this.filters.showStartedGames.value.boolValue;
				searchFilters.allowMismatchedMods = !this.filters.hideIncompatibleGames.value.boolValue;
				this.serverRemoteGameProvider.SetSearchFilters(searchFilters);
			}
			this.aggregateRemoteGameProvider.SetProviderAdded(this.lobbyRemoteGameProvider, this.filters.showLobbies.value.boolValue);
			BaseAsyncRemoteGameProvider.SearchFilters searchFilters2 = this.lobbyRemoteGameProvider.GetSearchFilters();
			searchFilters2.allowMismatchedMods = !this.filters.hideIncompatibleGames.value.boolValue;
			this.lobbyRemoteGameProvider.SetSearchFilters(searchFilters2);
			int requiredSlots = 0;
			if (!this.filters.mustHaveEnoughSlots.value.boolValue && PlatformSystems.lobbyManager.calculatedTotalPlayerCount > 0)
			{
				requiredSlots = PlatformSystems.lobbyManager.calculatedTotalPlayerCount;
			}
			AdvancedFilterRemoteGameProvider.SearchFilters searchFilters3 = this.advancedFilterRemoteGameProvider.GetSearchFilters();
			searchFilters3.allowPassword = this.filters.showPasswordedGames.value.boolValue;
			searchFilters3.minMaxPlayers = this.filters.minMaxPlayers.value.intValue;
			searchFilters3.maxMaxPlayers = this.filters.maxMaxPlayers.value.intValue;
			searchFilters3.maxPing = this.filters.maxPing.value.intValue;
			searchFilters3.requiredSlots = requiredSlots;
			searchFilters3.allowDifficultyEasy = this.filters.showDifficultyEasyGames.value.boolValue;
			searchFilters3.allowDifficultyNormal = this.filters.showDifficultyNormalGames.value.boolValue;
			searchFilters3.allowDifficultyHard = this.filters.showDifficultyHardGames.value.boolValue;
			searchFilters3.showGamesWithRuleVoting = this.filters.showGamesWithRuleVoting.value.boolValue;
			searchFilters3.showGamesWithoutRuleVoting = this.filters.showGamesWithoutRuleVoting.value.boolValue;
			searchFilters3.allowInProgressGames = this.filters.showStartedGames.value.boolValue;
			this.advancedFilterRemoteGameProvider.SetSearchFilters(searchFilters3);
		}

		// Token: 0x06003FE3 RID: 16355 RVA: 0x00108298 File Offset: 0x00106498
		private void RebuildSortTypeDropdown()
		{
			List<string> list = CollectionPool<string, List<string>>.RentCollection();
			this.sortTypeDropdown.ClearOptions();
			for (int i = 0; i < SortRemoteGameProvider.sorters.Length; i++)
			{
				list.Add(Language.GetString(SortRemoteGameProvider.sorters[i].nameToken));
			}
			this.sortTypeDropdown.AddOptions(list);
			SortRemoteGameProvider.Parameters parameters = this.sortRemoteGameProvider.GetParameters();
			this.sortTypeDropdown.SetValueWithoutNotify(parameters.sorterIndex);
			CollectionPool<string, List<string>>.ReturnCollection(list);
		}

		// Token: 0x06003FE4 RID: 16356 RVA: 0x00108310 File Offset: 0x00106510
		private void UpdateSorting()
		{
			SortRemoteGameProvider.Parameters parameters = this.sortRemoteGameProvider.GetParameters();
			parameters.ascending = this.sortAscendToggle.isOn;
			parameters.sorterIndex = this.sortTypeDropdown.value;
			this.sortRemoteGameProvider.SetParameters(parameters);
		}

		// Token: 0x06003FE5 RID: 16357 RVA: 0x0010835C File Offset: 0x0010655C
		private void UpdateBusyIcon()
		{
			if (!this.busyIcon)
			{
				return;
			}
			Color color = this.busyIcon.color;
			float num = 1f;
			if (!this.primaryRemoteGameProvider.IsBusy())
			{
				num = color.a - Time.unscaledDeltaTime;
			}
			if (num != color.a)
			{
				color.a = num;
				this.busyIcon.color = color;
			}
		}

		// Token: 0x06003FE6 RID: 16358 RVA: 0x001083C0 File Offset: 0x001065C0
		[ContextMenu("Copy Filter Tokens")]
		private void CopyFilterTokens()
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (RemoteGameBrowserController.FilterInfo filterInfo in this.filters.allFilters)
			{
				stringBuilder.Append('"').Append(filterInfo.token).Append('"').Append(": \"\",").AppendLine();
			}
			GUIUtility.systemCopyBuffer = stringBuilder.ToString();
		}

		// Token: 0x04003E11 RID: 15889
		public GameObject cardPrefab;

		// Token: 0x04003E12 RID: 15890
		public RectTransform cardContainer;

		// Token: 0x04003E13 RID: 15891
		public MPButton previousPageButton;

		// Token: 0x04003E14 RID: 15892
		public MPButton nextPageButton;

		// Token: 0x04003E15 RID: 15893
		public HGTextMeshProUGUI pageNumberLabel;

		// Token: 0x04003E16 RID: 15894
		public GameObject togglePrefab;

		// Token: 0x04003E17 RID: 15895
		public GameObject textFieldPrefab;

		// Token: 0x04003E18 RID: 15896
		public RectTransform filterControlContainer;

		// Token: 0x04003E19 RID: 15897
		public MPDropdown sortTypeDropdown;

		// Token: 0x04003E1A RID: 15898
		public MPToggle sortAscendToggle;

		// Token: 0x04003E1B RID: 15899
		public Graphic busyIcon;

		// Token: 0x04003E1C RID: 15900
		private UIElementAllocator<RemoteGameCardController> cardAllocator;

		// Token: 0x04003E1D RID: 15901
		private bool displayDataDirty;

		// Token: 0x04003E1E RID: 15902
		private float cardPrefabHeight = 1f;

		// Token: 0x04003E1F RID: 15903
		private float initialRequestTime = float.PositiveInfinity;

		// Token: 0x04003E20 RID: 15904
		private IRemoteGameProvider primaryRemoteGameProvider;

		// Token: 0x04003E21 RID: 15905
		private PageRemoteGameProvider pageRemoteGameProvider;

		// Token: 0x04003E22 RID: 15906
		private SortRemoteGameProvider sortRemoteGameProvider;

		// Token: 0x04003E23 RID: 15907
		private AdvancedFilterRemoteGameProvider advancedFilterRemoteGameProvider;

		// Token: 0x04003E24 RID: 15908
		private AggregateRemoteGameProvider aggregateRemoteGameProvider;

		// Token: 0x04003E25 RID: 15909
		private SteamworksServerRemoteGameProvider serverRemoteGameProvider;

		// Token: 0x04003E26 RID: 15910
		private BaseAsyncRemoteGameProvider lobbyRemoteGameProvider;

		// Token: 0x04003E27 RID: 15911
		private RemoteGameBrowserController.FilterManager filters;

		// Token: 0x02000ADB RID: 2779
		private class FilterInfo
		{
			// Token: 0x04003E28 RID: 15912
			public RemoteGameFilterValue value;

			// Token: 0x04003E29 RID: 15913
			public string token;

			// Token: 0x04003E2A RID: 15914
			public Func<Component, RemoteGameFilterValue?> getUIValue;

			// Token: 0x04003E2B RID: 15915
			public Action<Component, RemoteGameFilterValue> setUIValue;

			// Token: 0x04003E2C RID: 15916
			public GameObject controlGameObject;

			// Token: 0x04003E2D RID: 15917
			public Component controlMainComponent;

			// Token: 0x04003E2E RID: 15918
			public LanguageTextMeshController labelController;
		}

		// Token: 0x02000ADC RID: 2780
		private class FilterManager
		{
			// Token: 0x06003FE9 RID: 16361 RVA: 0x0010846A File Offset: 0x0010666A
			public FilterManager(RemoteGameBrowserController owner)
			{
				this.owner = owner;
				this.currentContainer = owner.filterControlContainer;
				this.GenerateFilters();
			}

			// Token: 0x06003FEA RID: 16362 RVA: 0x00108498 File Offset: 0x00106698
			private RemoteGameBrowserController.FilterInfo AddFilter<T>(string token, RemoteGameFilterValue defaultValue, GameObject controlPrefab, Func<Component, RemoteGameFilterValue?> getUIValue, Action<Component, RemoteGameFilterValue> setUIValue) where T : Component
			{
				RemoteGameBrowserController.FilterInfo filterInfo = new RemoteGameBrowserController.FilterInfo
				{
					token = token,
					value = defaultValue,
					controlGameObject = UnityEngine.Object.Instantiate<GameObject>(controlPrefab, this.currentContainer),
					getUIValue = getUIValue,
					setUIValue = setUIValue
				};
				filterInfo.controlMainComponent = filterInfo.controlGameObject.transform.Find("MainControl").GetComponent<T>();
				filterInfo.labelController = filterInfo.controlGameObject.transform.Find("NameLabel").GetComponent<LanguageTextMeshController>();
				filterInfo.controlGameObject.SetActive(true);
				filterInfo.setUIValue(filterInfo.controlMainComponent, defaultValue);
				this.allFilters.Add(filterInfo);
				return filterInfo;
			}

			// Token: 0x06003FEB RID: 16363 RVA: 0x0010854B File Offset: 0x0010674B
			private RemoteGameBrowserController.FilterInfo AddBoolFilter(string token, bool defaultValue)
			{
				return this.AddFilter<MPToggle>(token, defaultValue, this.owner.togglePrefab, new Func<Component, RemoteGameFilterValue?>(RemoteGameBrowserController.FilterManager.<>c.<>9.<AddBoolFilter>g__GetUIValue|23_0), new Action<Component, RemoteGameFilterValue>(RemoteGameBrowserController.FilterManager.<>c.<>9.<AddBoolFilter>g__SetUIValue|23_1));
			}

			// Token: 0x06003FEC RID: 16364 RVA: 0x00108588 File Offset: 0x00106788
			private RemoteGameBrowserController.FilterInfo AddIntFilter(string token, int defaultValue, int minValue = -2147483648, int maxValue = 2147483647, uint minDigits = 1U, uint maxDigits = 4294967295U)
			{
				RemoteGameBrowserController.FilterManager.<>c__DisplayClass24_0 CS$<>8__locals1 = new RemoteGameBrowserController.FilterManager.<>c__DisplayClass24_0();
				CS$<>8__locals1.minValue = minValue;
				CS$<>8__locals1.maxValue = maxValue;
				CS$<>8__locals1.minDigits = minDigits;
				CS$<>8__locals1.maxDigits = maxDigits;
				RemoteGameBrowserController.FilterInfo filterInfo = this.AddFilter<TMP_InputField>(token, defaultValue, this.owner.textFieldPrefab, new Func<Component, RemoteGameFilterValue?>(CS$<>8__locals1.<AddIntFilter>g__GetUIValue|0), new Action<Component, RemoteGameFilterValue>(CS$<>8__locals1.<AddIntFilter>g__SetUIValue|1));
				((TMP_InputField)filterInfo.controlMainComponent).characterValidation = TMP_InputField.CharacterValidation.Integer;
				return filterInfo;
			}

			// Token: 0x06003FED RID: 16365 RVA: 0x001085FB File Offset: 0x001067FB
			private RemoteGameBrowserController.FilterInfo AddStringFilter(string token, string defaultValue)
			{
				return this.AddFilter<TMP_InputField>(token, defaultValue, this.owner.textFieldPrefab, new Func<Component, RemoteGameFilterValue?>(RemoteGameBrowserController.FilterManager.<>c.<>9.<AddStringFilter>g__GetUIValue|25_0), new Action<Component, RemoteGameFilterValue>(RemoteGameBrowserController.FilterManager.<>c.<>9.<AddStringFilter>g__SetUIValue|25_1));
			}

			// Token: 0x06003FEE RID: 16366 RVA: 0x00108638 File Offset: 0x00106838
			private void GenerateFilters()
			{
				Regex regex = new Regex("([A-Z]?[a-z]+)");
				foreach (FieldInfo fieldInfo in typeof(RemoteGameBrowserController.FilterManager).GetFields())
				{
					if (!(fieldInfo.FieldType != typeof(RemoteGameBrowserController.FilterInfo)))
					{
						RemoteGameBrowserController.FilterManager.SetupAttribute customAttribute = fieldInfo.GetCustomAttribute<RemoteGameBrowserController.FilterManager.SetupAttribute>();
						if (customAttribute == null)
						{
							return;
						}
						string name = fieldInfo.Name;
						StringBuilder stringBuilder = HG.StringBuilderPool.RentStringBuilder();
						stringBuilder.Append("GAME_BROWSER_FILTER");
						foreach (object obj in regex.Matches(name))
						{
							Match match = (Match)obj;
							stringBuilder.Append("_");
							for (int j = 0; j < match.Length; j++)
							{
								stringBuilder.Append(char.ToUpperInvariant(name[match.Index + j]));
							}
						}
						string token = stringBuilder.ToString();
						HG.StringBuilderPool.ReturnStringBuilder(stringBuilder);
						RemoteGameBrowserController.FilterInfo filterInfo;
						switch (customAttribute.defaultValue.valueType)
						{
						case RemoteGameFilterValue.ValueType.Bool:
							filterInfo = this.AddBoolFilter(token, customAttribute.defaultValue.boolValue);
							break;
						case RemoteGameFilterValue.ValueType.Int:
							filterInfo = this.AddIntFilter(token, customAttribute.defaultValue.intValue, customAttribute.minValue, customAttribute.maxValue, 1U, uint.MaxValue);
							break;
						case RemoteGameFilterValue.ValueType.String:
							filterInfo = this.AddStringFilter(token, customAttribute.defaultValue.stringValue);
							break;
						default:
							throw new ArgumentOutOfRangeException();
						}
						fieldInfo.SetValue(this, filterInfo);
						filterInfo.labelController.token = filterInfo.token;
					}
				}
				this.CopyInternalValuesToUI();
			}

			// Token: 0x06003FEF RID: 16367 RVA: 0x00108804 File Offset: 0x00106A04
			public void CopyInternalValuesToUI()
			{
				foreach (RemoteGameBrowserController.FilterInfo filterInfo in this.allFilters)
				{
					filterInfo.setUIValue(filterInfo.controlMainComponent, filterInfo.value);
				}
			}

			// Token: 0x06003FF0 RID: 16368 RVA: 0x00108868 File Offset: 0x00106A68
			public void CopyUIValuesToInternal()
			{
				foreach (RemoteGameBrowserController.FilterInfo filterInfo in this.allFilters)
				{
					filterInfo.value = (filterInfo.getUIValue(filterInfo.controlMainComponent) ?? filterInfo.value);
				}
			}

			// Token: 0x04003E2F RID: 15919
			public readonly RemoteGameBrowserController owner;

			// Token: 0x04003E30 RID: 15920
			private RectTransform currentContainer;

			// Token: 0x04003E31 RID: 15921
			public List<RemoteGameBrowserController.FilterInfo> allFilters = new List<RemoteGameBrowserController.FilterInfo>();

			// Token: 0x04003E32 RID: 15922
			[RemoteGameBrowserController.FilterManager.SetupAttribute(true)]
			public RemoteGameBrowserController.FilterInfo showDedicatedServers;

			// Token: 0x04003E33 RID: 15923
			[RemoteGameBrowserController.FilterManager.SetupAttribute(true)]
			public RemoteGameBrowserController.FilterInfo showLobbies;

			// Token: 0x04003E34 RID: 15924
			[RemoteGameBrowserController.FilterManager.SetupAttribute(true)]
			public RemoteGameBrowserController.FilterInfo showDifficultyEasyGames;

			// Token: 0x04003E35 RID: 15925
			[RemoteGameBrowserController.FilterManager.SetupAttribute(true)]
			public RemoteGameBrowserController.FilterInfo showDifficultyNormalGames;

			// Token: 0x04003E36 RID: 15926
			[RemoteGameBrowserController.FilterManager.SetupAttribute(true)]
			public RemoteGameBrowserController.FilterInfo showDifficultyHardGames;

			// Token: 0x04003E37 RID: 15927
			[RemoteGameBrowserController.FilterManager.SetupAttribute(true)]
			public RemoteGameBrowserController.FilterInfo showGamesWithRuleVoting;

			// Token: 0x04003E38 RID: 15928
			[RemoteGameBrowserController.FilterManager.SetupAttribute(true)]
			public RemoteGameBrowserController.FilterInfo showGamesWithoutRuleVoting;

			// Token: 0x04003E39 RID: 15929
			[RemoteGameBrowserController.FilterManager.SetupAttribute(true)]
			public RemoteGameBrowserController.FilterInfo showPasswordedGames;

			// Token: 0x04003E3A RID: 15930
			[RemoteGameBrowserController.FilterManager.SetupAttribute(0, -2147483648, 2147483647, minValue = 0, maxValue = 999)]
			public RemoteGameBrowserController.FilterInfo maxPing;

			// Token: 0x04003E3B RID: 15931
			[RemoteGameBrowserController.FilterManager.SetupAttribute(false)]
			public RemoteGameBrowserController.FilterInfo mustHavePlayers;

			// Token: 0x04003E3C RID: 15932
			[RemoteGameBrowserController.FilterManager.SetupAttribute(true)]
			public RemoteGameBrowserController.FilterInfo mustHaveEnoughSlots;

			// Token: 0x04003E3D RID: 15933
			[RemoteGameBrowserController.FilterManager.SetupAttribute(1, -2147483648, 2147483647, minValue = 1)]
			public RemoteGameBrowserController.FilterInfo minMaxPlayers;

			// Token: 0x04003E3E RID: 15934
			[RemoteGameBrowserController.FilterManager.SetupAttribute(16, -2147483648, 2147483647, minValue = 1)]
			public RemoteGameBrowserController.FilterInfo maxMaxPlayers;

			// Token: 0x04003E3F RID: 15935
			[RemoteGameBrowserController.FilterManager.SetupAttribute("")]
			public RemoteGameBrowserController.FilterInfo requiredTags;

			// Token: 0x04003E40 RID: 15936
			[RemoteGameBrowserController.FilterManager.SetupAttribute("")]
			public RemoteGameBrowserController.FilterInfo forbiddenTags;

			// Token: 0x04003E41 RID: 15937
			[RemoteGameBrowserController.FilterManager.SetupAttribute(false)]
			public RemoteGameBrowserController.FilterInfo showStartedGames;

			// Token: 0x04003E42 RID: 15938
			[RemoteGameBrowserController.FilterManager.SetupAttribute(true)]
			public RemoteGameBrowserController.FilterInfo hideIncompatibleGames;

			// Token: 0x02000ADD RID: 2781
			[MeansImplicitUse(ImplicitUseKindFlags.Assign)]
			private class SetupAttribute : Attribute
			{
				// Token: 0x06003FF1 RID: 16369 RVA: 0x001088E4 File Offset: 0x00106AE4
				public SetupAttribute(bool defaultValue)
				{
					this.defaultValue = defaultValue;
				}

				// Token: 0x06003FF2 RID: 16370 RVA: 0x001088F8 File Offset: 0x00106AF8
				public SetupAttribute(int defaultValue, int minValue = -2147483648, int maxValue = 2147483647)
				{
					this.defaultValue = defaultValue;
					this.minValue = minValue;
					this.maxValue = maxValue;
				}

				// Token: 0x06003FF3 RID: 16371 RVA: 0x0010891A File Offset: 0x00106B1A
				public SetupAttribute(string defaultValue)
				{
					this.defaultValue = defaultValue;
				}

				// Token: 0x04003E43 RID: 15939
				public RemoteGameFilterValue defaultValue;

				// Token: 0x04003E44 RID: 15940
				public int minValue;

				// Token: 0x04003E45 RID: 15941
				public int maxValue;
			}
		}
	}
}
