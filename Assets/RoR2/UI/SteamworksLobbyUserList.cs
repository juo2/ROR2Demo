using System;
using System.Collections.Generic;
using System.Globalization;
using Facepunch.Steamworks;
using RoR2.Networking;
using UnityEngine;
using UnityEngine.Events;

namespace RoR2.UI
{
	// Token: 0x02000D97 RID: 3479
	public class SteamworksLobbyUserList : MonoBehaviour
	{
		// Token: 0x06004F9E RID: 20382 RVA: 0x0014943D File Offset: 0x0014763D
		private void Awake()
		{
			this.lobbyTypeDropdown.onValueChanged.AddListener(new UnityAction<int>(this.OnLobbyStateDropdownValueChanged));
			if (Client.Instance == null)
			{
				base.enabled = false;
			}
		}

		// Token: 0x06004F9F RID: 20383 RVA: 0x0014946C File Offset: 0x0014766C
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

		// Token: 0x06004FA0 RID: 20384 RVA: 0x00149544 File Offset: 0x00147744
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

		// Token: 0x06004FA1 RID: 20385 RVA: 0x00149610 File Offset: 0x00147810
		public void Refresh()
		{
			if (this.lobbyControlPanel.activeSelf != this.validLobbyExists)
			{
				this.lobbyControlPanel.SetActive(this.validLobbyExists);
			}
			if (this.createLobbyButton && this.createLobbyButton.gameObject.activeSelf == this.validLobbyExists)
			{
				this.createLobbyButton.gameObject.SetActive(!this.validLobbyExists);
			}
			if (this.leaveLobbyButton && this.leaveLobbyButton.gameObject.activeSelf != this.validLobbyExists)
			{
				this.leaveLobbyButton.gameObject.SetActive(this.validLobbyExists);
			}
			if (this.copyLobbyButton && this.copyLobbyButton.gameObject.activeSelf != this.validLobbyExists)
			{
				this.copyLobbyButton.gameObject.SetActive(this.validLobbyExists);
			}
			if (this.lobbyControlPanel.activeInHierarchy)
			{
				this.lobbyTypeDropdown.interactable = PlatformSystems.lobbyManager.ownsLobby;
				Lobby.Type lobbyType = Client.Instance.Lobby.LobbyType;
				for (int i = 0; i < SteamworksLobbyUserList.lobbyStateChoices.Length; i++)
				{
					if (lobbyType == SteamworksLobbyUserList.lobbyStateChoices[i].lobbyType)
					{
						this.lobbyTypeDropdown.SetValueWithoutNotify(i);
						break;
					}
				}
			}
			this.RebuildPlayers();
		}

		// Token: 0x06004FA2 RID: 20386 RVA: 0x00149760 File Offset: 0x00147960
		private void RebuildLobbyStateDropdownOptions()
		{
			for (int i = 0; i < SteamworksLobbyUserList.lobbyStateChoices.Length; i++)
			{
				SteamworksLobbyUserList.optionsBuffer.Add(Language.GetString(SteamworksLobbyUserList.lobbyStateChoices[i].token));
			}
			this.lobbyTypeDropdown.ClearOptions();
			this.lobbyTypeDropdown.AddOptions(SteamworksLobbyUserList.optionsBuffer);
			SteamworksLobbyUserList.optionsBuffer.Clear();
		}

		// Token: 0x06004FA3 RID: 20387 RVA: 0x001497C3 File Offset: 0x001479C3
		private void OnLobbyStateDropdownValueChanged(int newValue)
		{
			if (!this.validLobbyExists)
			{
				return;
			}
			Client.Instance.Lobby.LobbyType = SteamworksLobbyUserList.lobbyStateChoices[newValue].lobbyType;
			this.Refresh();
		}

		// Token: 0x1700073E RID: 1854
		// (get) Token: 0x06004FA4 RID: 20388 RVA: 0x0013E6A8 File Offset: 0x0013C8A8
		private bool validLobbyExists
		{
			get
			{
				return PlatformSystems.lobbyManager.isInLobby;
			}
		}

		// Token: 0x06004FA5 RID: 20389 RVA: 0x001497F4 File Offset: 0x001479F4
		public void RebuildPlayers()
		{
			if (!this.playerListContainer || !this.playerListContainer.gameObject || !this.playerListContainer.gameObject.activeInHierarchy)
			{
				return;
			}
			bool validLobbyExists = this.validLobbyExists;
			Client instance = Client.Instance;
			ulong[] array;
			if (instance == null)
			{
				array = null;
			}
			else
			{
				Lobby lobby = instance.Lobby;
				array = ((lobby != null) ? lobby.GetMemberIDs() : null);
			}
			ulong[] array2 = array;
			int num = Math.Max(validLobbyExists ? RoR2Application.maxPlayers : 0, 0);
			while (this.elements.Count > num)
			{
				int index = this.elements.Count - 1;
				UnityEngine.Object.Destroy(this.elements[index].gameObject);
				this.elements.RemoveAt(index);
			}
			while (this.elements.Count < num)
			{
				GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(LegacyResourcesAPI.Load<GameObject>("Prefabs/UI/SteamLobbyUserListElement"), this.playerListContainer);
				gameObject.SetActive(true);
				SocialUserIcon componentInChildren = gameObject.GetComponentInChildren<SocialUserIcon>();
				SteamUsernameLabel componentInChildren2 = gameObject.GetComponentInChildren<SteamUsernameLabel>();
				ChildLocator component = gameObject.GetComponent<ChildLocator>();
				this.elements.Add(new SteamworksLobbyUserList.Element
				{
					gameObject = gameObject,
					userIcon = componentInChildren,
					usernameLabel = componentInChildren2,
					elementChildLocator = component
				});
			}
			if (array2 != null)
			{
				int num2 = 0;
				for (int i = 0; i < array2.Length; i++)
				{
					int lobbyMemberPlayerCountByIndex = PlatformSystems.lobbyManager.GetLobbyMemberPlayerCountByIndex(i);
					int num3 = 0;
					while (num3 < lobbyMemberPlayerCountByIndex && num2 < this.elements.Count)
					{
						this.elements[num2++].SetUser(array2[i], num3);
						num3++;
					}
				}
				while (num2 < num && num2 < this.elements.Count)
				{
					this.elements[num2].SetUser(0UL, 0);
					num2++;
				}
			}
		}

		// Token: 0x06004FA6 RID: 20390 RVA: 0x001499B4 File Offset: 0x00147BB4
		private void UpdateUser(ulong userId)
		{
			for (int i = 0; i < this.elements.Count; i++)
			{
				if (this.elements[i].steamId == userId)
				{
					this.elements[i].Refresh();
				}
			}
		}

		// Token: 0x06004FA7 RID: 20391 RVA: 0x001499FC File Offset: 0x00147BFC
		private void OnLobbyStateChanged()
		{
			this.lobbyTypeDropdown.interactable = PlatformSystems.lobbyManager.ownsLobby;
			this.Refresh();
		}

		// Token: 0x06004FA8 RID: 20392 RVA: 0x00149A19 File Offset: 0x00147C19
		private void OnLobbyMemberDataUpdated(UserID steamId)
		{
			this.UpdateUser(steamId.ID);
		}

		// Token: 0x06004FA9 RID: 20393 RVA: 0x00149A28 File Offset: 0x00147C28
		private void OnLobbyChanged()
		{
			this.Refresh();
		}

		// Token: 0x04004C3E RID: 19518
		[Tooltip("The panel which acts as a container for UI that's only valid if there's an active lobby, like the lobby type or the copy-ID-to-clipboard button.")]
		public GameObject lobbyControlPanel;

		// Token: 0x04004C3F RID: 19519
		public MPButton createLobbyButton;

		// Token: 0x04004C40 RID: 19520
		public MPButton leaveLobbyButton;

		// Token: 0x04004C41 RID: 19521
		public MPButton copyLobbyButton;

		// Token: 0x04004C42 RID: 19522
		public MPDropdown lobbyTypeDropdown;

		// Token: 0x04004C43 RID: 19523
		public RectTransform playerListContainer;

		// Token: 0x04004C44 RID: 19524
		public static readonly SteamworksLobbyUserList.LobbyStateChoice[] lobbyStateChoices = new SteamworksLobbyUserList.LobbyStateChoice[]
		{
			new SteamworksLobbyUserList.LobbyStateChoice
			{
				lobbyType = Lobby.Type.Private,
				token = "STEAM_LOBBY_PRIVATE"
			},
			new SteamworksLobbyUserList.LobbyStateChoice
			{
				lobbyType = Lobby.Type.FriendsOnly,
				token = "STEAM_LOBBY_FRIENDSONLY"
			},
			new SteamworksLobbyUserList.LobbyStateChoice
			{
				lobbyType = Lobby.Type.Public,
				token = "STEAM_LOBBY_PUBLIC"
			}
		};

		// Token: 0x04004C45 RID: 19525
		private static readonly List<string> optionsBuffer = new List<string>();

		// Token: 0x04004C46 RID: 19526
		private List<SteamworksLobbyUserList.Element> elements = new List<SteamworksLobbyUserList.Element>();

		// Token: 0x02000D98 RID: 3480
		public struct LobbyStateChoice
		{
			// Token: 0x04004C47 RID: 19527
			public Lobby.Type lobbyType;

			// Token: 0x04004C48 RID: 19528
			public string token;
		}

		// Token: 0x02000D99 RID: 3481
		private class Element
		{
			// Token: 0x06004FAC RID: 20396 RVA: 0x00149AD2 File Offset: 0x00147CD2
			public void SetUser(ulong steamId, int subPlayerIndex)
			{
				this.steamId = steamId;
				this.userIcon.RefreshWithUser(new UserID(steamId));
				this.usernameLabel.subPlayerIndex = subPlayerIndex;
				this.Refresh();
			}

			// Token: 0x06004FAD RID: 20397 RVA: 0x00149B00 File Offset: 0x00147D00
			public void Refresh()
			{
				if (this.steamId == 0UL)
				{
					this.elementChildLocator.FindChild("UserIcon").gameObject.SetActive(false);
					this.elementChildLocator.FindChild("InviteButton").gameObject.SetActive(true);
				}
				else
				{
					this.elementChildLocator.FindChild("UserIcon").gameObject.SetActive(true);
					this.elementChildLocator.FindChild("InviteButton").gameObject.SetActive(false);
				}
				this.userIcon.Refresh();
				this.usernameLabel.Refresh();
				this.RefreshCrownAndPromoteButton();
			}

			// Token: 0x06004FAE RID: 20398 RVA: 0x00149BA0 File Offset: 0x00147DA0
			private void RefreshCrownAndPromoteButton()
			{
				if (Client.Instance == null)
				{
					return;
				}
				bool flag = Client.Instance.Lobby.Owner == this.steamId && this.steamId > 0UL;
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
				if (this.elementChildLocator)
				{
					bool flag2 = !flag && PlatformSystems.lobbyManager.ownsLobby && this.steamId != 0UL && !SteamLobbyFinder.running && !NetworkSession.instance;
					GameObject gameObject = this.elementChildLocator.FindChild("PromoteButton").gameObject;
					if (gameObject)
					{
						gameObject.SetActive(flag2);
						if (flag2)
						{
							MPButton component = gameObject.GetComponent<MPButton>();
							if (component)
							{
								component.onClick.RemoveAllListeners();
								component.onClick.AddListener(delegate()
								{
									Console.instance.SubmitCmd(null, string.Format(CultureInfo.InvariantCulture, "steam_lobby_assign_owner {0}", TextSerialization.ToStringInvariant(this.steamId)), false);
								});
							}
						}
					}
				}
			}

			// Token: 0x04004C49 RID: 19529
			public ulong steamId;

			// Token: 0x04004C4A RID: 19530
			public GameObject gameObject;

			// Token: 0x04004C4B RID: 19531
			public SocialUserIcon userIcon;

			// Token: 0x04004C4C RID: 19532
			public SteamUsernameLabel usernameLabel;

			// Token: 0x04004C4D RID: 19533
			public GameObject lobbyLeaderCrown;

			// Token: 0x04004C4E RID: 19534
			public ChildLocator elementChildLocator;
		}
	}
}
