using System;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace RoR2.UI
{
	// Token: 0x02000D38 RID: 3384
	public class LobbyUserList : MonoBehaviour
	{
		// Token: 0x170006FF RID: 1791
		// (get) Token: 0x06004D23 RID: 19747 RVA: 0x0013E697 File Offset: 0x0013C897
		// (set) Token: 0x06004D24 RID: 19748 RVA: 0x0013E69F File Offset: 0x0013C89F
		public int NumberOfParticipants { get; private set; }

		// Token: 0x17000700 RID: 1792
		// (get) Token: 0x06004D25 RID: 19749 RVA: 0x0013E6A8 File Offset: 0x0013C8A8
		private bool isInLobby
		{
			get
			{
				return PlatformSystems.lobbyManager.isInLobby;
			}
		}

		// Token: 0x06004D26 RID: 19750 RVA: 0x0013E6B4 File Offset: 0x0013C8B4
		private void Awake()
		{
			this.lobbyTypeDropdown.onValueChanged.AddListener(new UnityAction<int>(this.OnLobbyStateDropdownValueChanged));
		}

		// Token: 0x06004D27 RID: 19751 RVA: 0x0013E6D4 File Offset: 0x0013C8D4
		private void OnEnable()
		{
			LobbyManager lobbyManager = PlatformSystems.lobbyManager;
			lobbyManager.onLobbyChanged = (Action)Delegate.Combine(lobbyManager.onLobbyChanged, new Action(this.OnLobbyChanged));
			LobbyManager lobbyManager2 = PlatformSystems.lobbyManager;
			lobbyManager2.onLobbyDataUpdated = (Action)Delegate.Combine(lobbyManager2.onLobbyDataUpdated, new Action(this.RebuildPlayers));
			LobbyManager lobbyManager3 = PlatformSystems.lobbyManager;
			lobbyManager3.onLobbyStateChanged = (Action)Delegate.Combine(lobbyManager3.onLobbyStateChanged, new Action(this.OnLobbyStateChanged));
			LobbyManager lobbyManager4 = PlatformSystems.lobbyManager;
			lobbyManager4.onLobbyMemberDataUpdated = (Action<UserID>)Delegate.Combine(lobbyManager4.onLobbyMemberDataUpdated, new Action<UserID>(this.OnLobbyMemberDataUpdated));
			LobbyManager lobbyManager5 = PlatformSystems.lobbyManager;
			lobbyManager5.onPlayerCountUpdated = (Action)Delegate.Combine(lobbyManager5.onPlayerCountUpdated, new Action(this.RebuildPlayers));
			this.RebuildLobbyStateDropdownOptions();
			this.Refresh();
		}

		// Token: 0x06004D28 RID: 19752 RVA: 0x0013E7AC File Offset: 0x0013C9AC
		private void OnDisable()
		{
			LobbyManager lobbyManager = PlatformSystems.lobbyManager;
			lobbyManager.onLobbyChanged = (Action)Delegate.Remove(lobbyManager.onLobbyChanged, new Action(this.OnLobbyChanged));
			LobbyManager lobbyManager2 = PlatformSystems.lobbyManager;
			lobbyManager2.onLobbyDataUpdated = (Action)Delegate.Remove(lobbyManager2.onLobbyDataUpdated, new Action(this.RebuildPlayers));
			LobbyManager lobbyManager3 = PlatformSystems.lobbyManager;
			lobbyManager3.onLobbyStateChanged = (Action)Delegate.Remove(lobbyManager3.onLobbyStateChanged, new Action(this.OnLobbyStateChanged));
			LobbyManager lobbyManager4 = PlatformSystems.lobbyManager;
			lobbyManager4.onLobbyMemberDataUpdated = (Action<UserID>)Delegate.Remove(lobbyManager4.onLobbyMemberDataUpdated, new Action<UserID>(this.OnLobbyMemberDataUpdated));
			LobbyManager lobbyManager5 = PlatformSystems.lobbyManager;
			lobbyManager5.onPlayerCountUpdated = (Action)Delegate.Remove(lobbyManager5.onPlayerCountUpdated, new Action(this.RebuildPlayers));
		}

		// Token: 0x06004D29 RID: 19753 RVA: 0x0013E877 File Offset: 0x0013CA77
		private void Update()
		{
			this.Refresh();
		}

		// Token: 0x06004D2A RID: 19754 RVA: 0x0013E87F File Offset: 0x0013CA7F
		internal void ToggleActions(bool val)
		{
			this.createLobbyButton.gameObject.SetActive(val);
			this.leaveLobbyButton.gameObject.SetActive(val);
			this.copyLobbyButton.gameObject.SetActive(val);
		}

		// Token: 0x06004D2B RID: 19755 RVA: 0x0013E8B4 File Offset: 0x0013CAB4
		public void Refresh()
		{
			if (PlatformSystems.lobbyManager.HasMPLobbyUI())
			{
				if (this.lobbyControlPanel.activeSelf != this.isInLobby)
				{
					this.lobbyControlPanel.SetActive(this.isInLobby);
				}
				if (this.createLobbyButton && PlatformSystems.lobbyManager.HasMPLobbyFeature(MPLobbyFeatures.CreateLobby) && this.createLobbyButton.gameObject.activeSelf == this.isInLobby)
				{
					this.createLobbyButton.gameObject.SetActive(!this.isInLobby);
				}
				if (this.leaveLobbyButton && PlatformSystems.lobbyManager.HasMPLobbyFeature(MPLobbyFeatures.LeaveLobby) && this.leaveLobbyButton.gameObject.activeSelf != this.isInLobby)
				{
					this.leaveLobbyButton.gameObject.SetActive(this.isInLobby);
				}
				if (this.copyLobbyButton && PlatformSystems.lobbyManager.HasMPLobbyFeature(MPLobbyFeatures.Clipboard))
				{
					this.copyLobbyButton.interactable = this.isInLobby;
					this.copyLobbyButton.gameObject.SetActive(this.isInLobby);
				}
				if (this.lobbyControlPanel.activeInHierarchy && PlatformSystems.lobbyManager.HasMPLobbyFeature(MPLobbyFeatures.LobbyDropdownOptions))
				{
					this.lobbyTypeDropdown.interactable = PlatformSystems.lobbyManager.ownsLobby;
					LobbyType currentLobbyType = PlatformSystems.lobbyManager.currentLobbyType;
					for (int i = 0; i < LobbyUserList.lobbyStateChoices.Length; i++)
					{
						if (currentLobbyType == LobbyUserList.lobbyStateChoices[i].lobbyType)
						{
							this.lobbyTypeDropdown.SetValueWithoutNotify(i);
							break;
						}
					}
				}
			}
			this.RebuildPlayers();
		}

		// Token: 0x06004D2C RID: 19756 RVA: 0x0013EA40 File Offset: 0x0013CC40
		private void RebuildLobbyStateDropdownOptions()
		{
			if (!PlatformSystems.lobbyManager.HasMPLobbyUI())
			{
				return;
			}
			for (int i = 0; i < LobbyUserList.lobbyStateChoices.Length; i++)
			{
				LobbyUserList.optionsBuffer.Add(Language.GetString(LobbyUserList.lobbyStateChoices[i].token));
			}
			this.lobbyTypeDropdown.ClearOptions();
			this.lobbyTypeDropdown.AddOptions(LobbyUserList.optionsBuffer);
			LobbyUserList.optionsBuffer.Clear();
		}

		// Token: 0x06004D2D RID: 19757 RVA: 0x0013EAB0 File Offset: 0x0013CCB0
		private void OnLobbyStateDropdownValueChanged(int newValue)
		{
			if (!this.isInLobby)
			{
				return;
			}
			PlatformSystems.lobbyManager.currentLobbyType = LobbyUserList.lobbyStateChoices[newValue].lobbyType;
			this.Refresh();
		}

		// Token: 0x06004D2E RID: 19758 RVA: 0x0013EADC File Offset: 0x0013CCDC
		public void ClearUserList()
		{
			while (this.userList.Count > 0)
			{
				int index = this.userList.Count - 1;
				UnityEngine.Object.Destroy(this.userList[index].gameObject);
				this.userList.RemoveAt(index);
			}
		}

		// Token: 0x06004D2F RID: 19759 RVA: 0x0013EB2C File Offset: 0x0013CD2C
		public void RebuildPlayers()
		{
			if (!this.playerListContainer || !this.playerListContainer.gameObject || !this.playerListContainer.gameObject.activeInHierarchy)
			{
				return;
			}
			bool isInLobby = this.isInLobby;
			UserID[] lobbyMembers = PlatformSystems.lobbyManager.GetLobbyMembers();
			int num = Math.Min(isInLobby ? (lobbyMembers.Length + 1) : 0, RoR2Application.maxPlayers);
			while (this.userList.Count > num)
			{
				int index = this.userList.Count - 1;
				UnityEngine.Object.Destroy(this.userList[index].gameObject);
				this.userList.RemoveAt(index);
			}
			while (this.userList.Count < num)
			{
				GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(LegacyResourcesAPI.Load<GameObject>("Prefabs/UI/SteamLobbyUserListElement"), this.playerListContainer);
				gameObject.SetActive(true);
				SocialUserIcon componentInChildren = gameObject.GetComponentInChildren<SocialUserIcon>();
				SteamUsernameLabel componentInChildren2 = gameObject.GetComponentInChildren<SteamUsernameLabel>();
				ChildLocator component = gameObject.GetComponent<ChildLocator>();
				this.userList.Add(new LobbyUserList.UserElement
				{
					gameObject = gameObject,
					userIcon = componentInChildren,
					usernameLabel = componentInChildren2,
					elementChildLocator = component
				});
			}
			if (lobbyMembers != null)
			{
				int i;
				for (i = 0; i < lobbyMembers.Length; i++)
				{
					if (i >= this.userList.Count)
					{
						break;
					}
					this.userList[i].SetUser(lobbyMembers[i], i);
				}
				while (i < num)
				{
					this.userList[i].SetUser(default(UserID), 0);
					i++;
				}
			}
		}

		// Token: 0x06004D30 RID: 19760 RVA: 0x0013ECB0 File Offset: 0x0013CEB0
		private void UpdateUser(UserID userId)
		{
			for (int i = 0; i < this.userList.Count; i++)
			{
				if (this.userList[i].id == userId)
				{
					this.userList[i].Refresh();
				}
			}
		}

		// Token: 0x06004D31 RID: 19761 RVA: 0x0013ECFD File Offset: 0x0013CEFD
		private void OnLobbyStateChanged()
		{
			this.lobbyTypeDropdown.interactable = PlatformSystems.lobbyManager.ownsLobby;
			this.Refresh();
		}

		// Token: 0x06004D32 RID: 19762 RVA: 0x0013ED1A File Offset: 0x0013CF1A
		private void OnLobbyMemberDataUpdated(UserID steamId)
		{
			this.UpdateUser(steamId);
		}

		// Token: 0x06004D33 RID: 19763 RVA: 0x0013E877 File Offset: 0x0013CA77
		private void OnLobbyChanged()
		{
			this.Refresh();
		}

		// Token: 0x04004A28 RID: 18984
		public TextMeshProUGUI lobbyStateText;

		// Token: 0x04004A29 RID: 18985
		public GameObject lobbyControlPanel;

		// Token: 0x04004A2A RID: 18986
		public GameObject contentArea;

		// Token: 0x04004A2B RID: 18987
		public RectTransform playerListContainer;

		// Token: 0x04004A2C RID: 18988
		[Tooltip("The panel which acts as a container for UI that's only valid if there's an active lobby, like the lobby type or the copy-ID-to-clipboard button.")]
		public MPButton createLobbyButton;

		// Token: 0x04004A2D RID: 18989
		public MPButton leaveLobbyButton;

		// Token: 0x04004A2E RID: 18990
		public MPButton copyLobbyButton;

		// Token: 0x04004A2F RID: 18991
		public MPButton joinLobbyButton;

		// Token: 0x04004A30 RID: 18992
		public MPDropdown lobbyTypeDropdown;

		// Token: 0x04004A31 RID: 18993
		private List<LobbyUserList.UserElement> userList = new List<LobbyUserList.UserElement>();

		// Token: 0x04004A32 RID: 18994
		public static readonly LobbyUserList.LobbyStateChoice[] lobbyStateChoices = new LobbyUserList.LobbyStateChoice[]
		{
			new LobbyUserList.LobbyStateChoice
			{
				lobbyType = LobbyType.Private,
				token = "STEAM_LOBBY_PRIVATE"
			},
			new LobbyUserList.LobbyStateChoice
			{
				lobbyType = LobbyType.FriendsOnly,
				token = "STEAM_LOBBY_FRIENDSONLY"
			},
			new LobbyUserList.LobbyStateChoice
			{
				lobbyType = LobbyType.Public,
				token = "STEAM_LOBBY_PUBLIC"
			}
		};

		// Token: 0x04004A33 RID: 18995
		private static readonly List<string> optionsBuffer = new List<string>();

		// Token: 0x02000D39 RID: 3385
		public struct LobbyStateChoice
		{
			// Token: 0x04004A34 RID: 18996
			public LobbyType lobbyType;

			// Token: 0x04004A35 RID: 18997
			public string token;
		}

		// Token: 0x02000D3A RID: 3386
		private class UserElement
		{
			// Token: 0x06004D36 RID: 19766 RVA: 0x0013EDC6 File Offset: 0x0013CFC6
			public void SetUser(UserID playerUserID, int subPlayerIndex)
			{
				this.id = playerUserID;
				this.userIcon.RefreshWithUser(playerUserID);
				this.usernameLabel.userId = playerUserID;
				this.usernameLabel.subPlayerIndex = subPlayerIndex;
				this.Refresh();
			}

			// Token: 0x06004D37 RID: 19767 RVA: 0x0013EDFC File Offset: 0x0013CFFC
			public void Refresh()
			{
				if (this.id == default(UserID))
				{
					this.elementChildLocator.FindChild("UserIcon").gameObject.SetActive(false);
					this.elementChildLocator.FindChild("InviteButton").gameObject.SetActive(PlatformSystems.lobbyManager.HasMPLobbyFeature(MPLobbyFeatures.Invite));
				}
				else
				{
					this.elementChildLocator.FindChild("UserIcon").gameObject.SetActive(PlatformSystems.lobbyManager.HasMPLobbyFeature(MPLobbyFeatures.UserIcon));
					this.elementChildLocator.FindChild("InviteButton").gameObject.SetActive(false);
				}
				this.userIcon.Refresh();
				this.usernameLabel.Refresh();
				this.RefreshCrownAndPromoteButton();
			}

			// Token: 0x06004D38 RID: 19768 RVA: 0x0013EEC0 File Offset: 0x0013D0C0
			private void RefreshCrownAndPromoteButton()
			{
				if (!PlatformSystems.lobbyManager.isInLobby)
				{
					return;
				}
				bool flag = PlatformSystems.lobbyManager.IsLobbyOwner(this.id);
				if (this.lobbyLeaderCrown != flag)
				{
					if (flag)
					{
						this.lobbyLeaderCrown = UnityEngine.Object.Instantiate<GameObject>(LegacyResourcesAPI.Load<GameObject>("Prefabs/UI/LobbyLeaderCrown"), this.gameObject.transform);
					}
					else
					{
						UnityEngine.Object.Destroy(this.lobbyLeaderCrown);
						this.lobbyLeaderCrown = null;
					}
				}
				if (this.elementChildLocator && PlatformSystems.lobbyManager.HasMPLobbyFeature(MPLobbyFeatures.HostPromotion))
				{
					bool flag2 = !PlatformSystems.lobbyManager.ShouldShowPromoteButton() && !flag && PlatformSystems.lobbyManager.ownsLobby && this.id != default(UserID) && !NetworkSession.instance;
					GameObject gameObject = this.elementChildLocator.FindChild("PromoteButton").gameObject;
					if (gameObject)
					{
						gameObject.SetActive(flag2);
						if (flag2)
						{
							MPButton component = gameObject.GetComponent<MPButton>();
							component.onClick.RemoveAllListeners();
							component.onClick.AddListener(delegate()
							{
								Console.instance.SubmitCmd(null, string.Format(CultureInfo.InvariantCulture, "steam_lobby_assign_owner {0}", this.id), false);
							});
						}
					}
				}
			}

			// Token: 0x04004A36 RID: 18998
			public UserID id;

			// Token: 0x04004A37 RID: 18999
			public GameObject gameObject;

			// Token: 0x04004A38 RID: 19000
			public SocialUserIcon userIcon;

			// Token: 0x04004A39 RID: 19001
			public SocialUsernameLabel usernameLabel;

			// Token: 0x04004A3A RID: 19002
			public GameObject lobbyLeaderCrown;

			// Token: 0x04004A3B RID: 19003
			public ChildLocator elementChildLocator;
		}
	}
}
