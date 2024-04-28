using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using RoR2.Networking;
using RoR2.RemoteGameBrowser;
using RoR2.UI;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace RoR2
{
	// Token: 0x02000A02 RID: 2562
	[RequireComponent(typeof(RectTransform))]
	public class RemoteGameDetailsPanelController : MonoBehaviour
	{
		// Token: 0x06003B29 RID: 15145 RVA: 0x000F4F20 File Offset: 0x000F3120
		private void Awake()
		{
			this.rectTransform = (RectTransform)base.transform;
			this.playerStripAllocator = new UIElementAllocator<ChildLocator>(this.playersContainer, this.playerStripPrefab, true, false);
			this.ruleIconAllocator = new UIElementAllocator<RuleChoiceController>(this.rulesContainer, this.ruleIconPrefab, true, false);
			this.ruleBook = new RuleBook();
			this.passwordTextBox.onValueChanged.AddListener(new UnityAction<string>(NetworkManagerSystem.cvClPassword.SetString));
		}

		// Token: 0x06003B2A RID: 15146 RVA: 0x000F4F9C File Offset: 0x000F319C
		private void OnEnable()
		{
			this.passwordTextBox.SetTextWithoutNotify(NetworkManagerSystem.cvClPassword.value);
		}

		// Token: 0x06003B2B RID: 15147 RVA: 0x000F4FB4 File Offset: 0x000F31B4
		private void Update()
		{
			int num = LocalUserManager.readOnlyLocalUsersList.Count;
			if (PlatformSystems.lobbyManager.isInLobby)
			{
				num = PlatformSystems.lobbyManager.calculatedTotalPlayerCount;
			}
			this.joinLobbyButton.interactable = (this.currentGameInfo.availableLobbySlots >= num);
			this.joinServerButton.interactable = (this.currentGameInfo.availableServerSlots >= num);
		}

		// Token: 0x06003B2C RID: 15148 RVA: 0x000F501B File Offset: 0x000F321B
		public void SetGameInfo(RemoteGameInfo newGameInfo)
		{
			this.SetGameInfoInternal(newGameInfo);
			this.RequestRefresh();
		}

		// Token: 0x06003B2D RID: 15149 RVA: 0x000F502C File Offset: 0x000F322C
		private void SetGameInfoInternal(in RemoteGameInfo newGameInfo)
		{
			this.currentGameInfo = newGameInfo;
			SceneDef sceneDef = SceneCatalog.GetSceneDef(newGameInfo.currentSceneIndex ?? SceneIndex.Invalid);
			string value = null;
			if (newGameInfo.didRespond != null)
			{
				value = Language.GetString(newGameInfo.didRespond.Value ? "REMOTE_GAME_STATUS_RESPONSIVE" : "REMOTE_GAME_STATUS_UNRESPONSIVE");
			}
			GameModeIndex gameModeIndex = GameModeIndex.Invalid;
			string text = null;
			if (this.currentGameInfo.gameModeName != null)
			{
				gameModeIndex = GameModeCatalog.FindGameModeIndex(this.currentGameInfo.gameModeName);
			}
			if (gameModeIndex != GameModeIndex.Invalid)
			{
				text = GameModeCatalog.GetGameModePrefabComponent(gameModeIndex).nameToken;
			}
			if (PlatformSystems.ShouldUseEpicOnlineSystems)
			{
				this.SetLabelInfo(this.nameLabel, "REMOTE_GAME_DETAILS_NAME", this.currentGameInfo.serverName, null);
			}
			else
			{
				this.SetLabelInfo(this.nameLabel, "REMOTE_GAME_DETAILS_NAME", this.currentGameInfo.name, null);
			}
			this.SetLabelInfo<int>(this.pingLabel, "REMOTE_GAME_DETAILS_PING", this.currentGameInfo.ping, null);
			this.SetLabelInfo(this.tagsLabel, "REMOTE_GAME_DETAILS_TAGS", (this.currentGameInfo.tags != null) ? string.Join(",", this.currentGameInfo.tags) : null, null);
			this.SetLabelInfo(this.gameModeLabel, "REMOTE_GAME_DETAILS_GAMEMODE", (text != null) ? Language.GetString(text) : null, null);
			this.SetLabelInfo(this.mapLabel, "REMOTE_GAME_DETAILS_MAP", sceneDef ? Language.GetString(sceneDef.nameToken) : null, null);
			this.SetLabelInfo(this.statusLabel, "REMOTE_GAME_DETAILS_STATUS", value, null);
			this.SetLabelInfo(this.lobbyPlayerCountLabel, "REMOTE_GAME_DETAILS_LOBBY_PLAYER_COUNT", this.currentGameInfo.lobbyPlayerCount, this.currentGameInfo.lobbyMaxPlayers, null);
			this.SetLabelInfo(this.serverPlayerCountLabel, "REMOTE_GAME_DETAILS_SERVER_PLAYER_COUNT", this.currentGameInfo.serverPlayerCount, this.currentGameInfo.serverMaxPlayers, null);
			List<RemotePlayerInfo> list = new List<RemotePlayerInfo>();
			RemoteGameInfo remoteGameInfo = newGameInfo;
			remoteGameInfo.GetPlayers(list);
			this.playerStripAllocator.AllocateElements(list.Count);
			ReadOnlyCollection<ChildLocator> elements = this.playerStripAllocator.elements;
			for (int i = 0; i < this.playerStripAllocator.elements.Count; i++)
			{
				ChildLocator childLocator = elements[i];
				RemotePlayerInfo remotePlayerInfo = list[i];
				childLocator.FindChild("NameLabel").GetComponent<TextMeshProUGUI>().SetText(remotePlayerInfo.name, true);
			}
			remoteGameInfo = newGameInfo;
			if (remoteGameInfo.GetRuleBook(this.ruleBook))
			{
				List<RuleChoiceDef> list2 = new List<RuleChoiceDef>();
				foreach (RuleChoiceDef ruleChoiceDef in this.ruleBook.choices)
				{
					if (!ruleChoiceDef.onlyShowInGameBrowserIfNonDefault || !ruleChoiceDef.isDefaultChoice)
					{
						list2.Add(ruleChoiceDef);
					}
				}
				this.ruleIconAllocator.AllocateElements(list2.Count);
				ReadOnlyCollection<RuleChoiceController> elements2 = this.ruleIconAllocator.elements;
				for (int j = 0; j < list2.Count; j++)
				{
					elements2[j].SetChoice(list2[j]);
				}
			}
			else
			{
				this.ruleIconAllocator.AllocateElements(0);
			}
			this.SetWarningEnabled(this.highCapacityWarningPanel, this.currentGameInfo.greaterMaxPlayers > 4);
			this.SetWarningEnabled(this.versionMismatchWarningPanel, newGameInfo.buildId != null && !string.Equals(newGameInfo.buildId, RoR2Application.GetBuildId(), StringComparison.OrdinalIgnoreCase));
			this.SetWarningEnabled(this.modMismatchWarningPanel, newGameInfo.modHash != null && !string.Equals(newGameInfo.modHash, NetworkModCompatibilityHelper.networkModHash, StringComparison.OrdinalIgnoreCase));
			remoteGameInfo = newGameInfo;
			bool flag = remoteGameInfo.IsLobbyIdValid();
			remoteGameInfo = newGameInfo;
			bool flag2 = remoteGameInfo.IsServerIdValid() || newGameInfo.serverAddress != null;
			if (flag && flag2)
			{
				flag2 = false;
			}
			this.joinLobbyButton.gameObject.SetActive(flag);
			this.joinServerButton.gameObject.SetActive(flag2);
			this.passwordPanel.gameObject.SetActive(this.currentGameInfo.hasPassword ?? false);
		}

		// Token: 0x06003B2E RID: 15150 RVA: 0x000F546C File Offset: 0x000F366C
		private void SetLabelInfo<T>(TextMeshProUGUI label, string formatToken, T? value, GameObject explicitContainer = null) where T : struct
		{
			if (explicitContainer == null)
			{
				explicitContainer = label.gameObject;
			}
			bool flag = value != null;
			explicitContainer.SetActive(flag);
			if (flag)
			{
				label.SetText(Language.GetStringFormatted(formatToken, new object[]
				{
					value.Value
				}), true);
			}
		}

		// Token: 0x06003B2F RID: 15151 RVA: 0x000F54BC File Offset: 0x000F36BC
		private void SetLabelInfo(TextMeshProUGUI label, string formatToken, string value, GameObject explicitContainer = null)
		{
			if (explicitContainer == null)
			{
				explicitContainer = label.gameObject;
			}
			bool flag = !string.IsNullOrEmpty(value);
			explicitContainer.SetActive(flag);
			if (flag)
			{
				label.SetText(Language.GetStringFormatted(formatToken, new object[]
				{
					value
				}), true);
			}
		}

		// Token: 0x06003B30 RID: 15152 RVA: 0x000F5504 File Offset: 0x000F3704
		private void SetLabelInfo(TextMeshProUGUI label, string formatToken, int? value1, int? value2, GameObject explicitContainer = null)
		{
			if (explicitContainer == null)
			{
				explicitContainer = label.gameObject;
			}
			bool flag = value1 != null && value2 != null;
			explicitContainer.SetActive(flag);
			if (flag)
			{
				label.SetText(Language.GetStringFormatted(formatToken, new object[]
				{
					value1.Value,
					value2.Value
				}), true);
			}
		}

		// Token: 0x06003B31 RID: 15153 RVA: 0x000F556D File Offset: 0x000F376D
		private void SetWarningEnabled(GameObject labelObject, bool shouldEnable)
		{
			if (labelObject)
			{
				labelObject.SetActive(shouldEnable);
			}
		}

		// Token: 0x06003B32 RID: 15154 RVA: 0x000F5580 File Offset: 0x000F3780
		public void JoinCurrentLobby()
		{
			if (this.currentGameInfo.IsLobbyIdValid())
			{
				string str;
				if (PlatformSystems.ShouldUseEpicOnlineSystems)
				{
					str = this.currentGameInfo.lobbyIdStr;
				}
				else
				{
					str = this.currentGameInfo.lobbyId.ToString();
				}
				Console.instance.SubmitCmd(null, "steam_lobby_join " + str, false);
			}
		}

		// Token: 0x06003B33 RID: 15155 RVA: 0x000F55E0 File Offset: 0x000F37E0
		public void JoinCurrentServer()
		{
			if (this.currentGameInfo.serverAddress != null)
			{
				Console.instance.SubmitCmd(null, "connect " + this.currentGameInfo.serverAddress.Value, false);
				return;
			}
			if (this.currentGameInfo.IsServerIdValid())
			{
				Console.instance.SubmitCmd(null, "connect_steamworks_p2p " + this.currentGameInfo.serverId.ToString(), false);
				return;
			}
		}

		// Token: 0x06003B34 RID: 15156 RVA: 0x000F5668 File Offset: 0x000F3868
		public void RequestRefresh()
		{
			RemoteGameDetailsPanelController.<>c__DisplayClass42_0 CS$<>8__locals1 = new RemoteGameDetailsPanelController.<>c__DisplayClass42_0();
			CS$<>8__locals1.<>4__this = this;
			RemoteGameDetailsPanelController.<>c__DisplayClass42_0 CS$<>8__locals2 = CS$<>8__locals1;
			int capturedCookie = this.cookie + 1;
			this.cookie = capturedCookie;
			CS$<>8__locals2.capturedCookie = capturedCookie;
			this.currentGameInfo.RequestRefresh(delegate(in RemoteGameInfo refreshedGameInfo)
			{
				if (CS$<>8__locals1.<>4__this && CS$<>8__locals1.capturedCookie == CS$<>8__locals1.<>4__this.cookie)
				{
					RemoteGameInfo remoteGameInfo = refreshedGameInfo;
					remoteGameInfo.didRespond = new bool?(true);
					CS$<>8__locals1.<>4__this.SetGameInfoInternal(remoteGameInfo);
				}
			}, delegate
			{
				CS$<>8__locals1.<>4__this.currentGameInfo.didRespond = new bool?(false);
				CS$<>8__locals1.<>4__this.SetGameInfoInternal(CS$<>8__locals1.<>4__this.currentGameInfo);
			}, true);
		}

		// Token: 0x040039E1 RID: 14817
		[Header("Main Info")]
		public TextMeshProUGUI nameLabel;

		// Token: 0x040039E2 RID: 14818
		public TextMeshProUGUI typeLabel;

		// Token: 0x040039E3 RID: 14819
		public TextMeshProUGUI lobbyPlayerCountLabel;

		// Token: 0x040039E4 RID: 14820
		public TextMeshProUGUI serverPlayerCountLabel;

		// Token: 0x040039E5 RID: 14821
		public TextMeshProUGUI pingLabel;

		// Token: 0x040039E6 RID: 14822
		public TextMeshProUGUI tagsLabel;

		// Token: 0x040039E7 RID: 14823
		public TextMeshProUGUI gameModeLabel;

		// Token: 0x040039E8 RID: 14824
		public TextMeshProUGUI mapLabel;

		// Token: 0x040039E9 RID: 14825
		public TextMeshProUGUI statusLabel;

		// Token: 0x040039EA RID: 14826
		[Header("Password")]
		public RectTransform passwordPanel;

		// Token: 0x040039EB RID: 14827
		public TMP_InputField passwordTextBox;

		// Token: 0x040039EC RID: 14828
		[Header("Rules")]
		public RectTransform rulesPanel;

		// Token: 0x040039ED RID: 14829
		public RectTransform rulesContainer;

		// Token: 0x040039EE RID: 14830
		public GameObject ruleIconPrefab;

		// Token: 0x040039EF RID: 14831
		[Header("Players")]
		public RectTransform playersPanel;

		// Token: 0x040039F0 RID: 14832
		public RectTransform playersContainer;

		// Token: 0x040039F1 RID: 14833
		public GameObject playerStripPrefab;

		// Token: 0x040039F2 RID: 14834
		[Header("Favorite/Blacklist")]
		public MPToggle favoriteToggle;

		// Token: 0x040039F3 RID: 14835
		public MPToggle blacklistToggle;

		// Token: 0x040039F4 RID: 14836
		[Header("Buttons")]
		public MPButton refreshButton;

		// Token: 0x040039F5 RID: 14837
		public MPButton joinLobbyButton;

		// Token: 0x040039F6 RID: 14838
		public MPButton joinServerButton;

		// Token: 0x040039F7 RID: 14839
		[Header("Warning Panels")]
		public GameObject highCapacityWarningPanel;

		// Token: 0x040039F8 RID: 14840
		public GameObject versionMismatchWarningPanel;

		// Token: 0x040039F9 RID: 14841
		public GameObject modMismatchWarningPanel;

		// Token: 0x040039FA RID: 14842
		private UIElementAllocator<ChildLocator> playerStripAllocator;

		// Token: 0x040039FB RID: 14843
		private UIElementAllocator<RuleChoiceController> ruleIconAllocator;

		// Token: 0x040039FC RID: 14844
		private RuleBook ruleBook;

		// Token: 0x040039FD RID: 14845
		private RectTransform rectTransform;

		// Token: 0x040039FE RID: 14846
		private RemoteGameInfo currentGameInfo;

		// Token: 0x040039FF RID: 14847
		private int cookie;
	}
}
