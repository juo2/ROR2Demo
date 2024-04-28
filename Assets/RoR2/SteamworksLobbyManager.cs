using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using Facepunch.Steamworks;
using HG;
using RoR2.Networking;
using RoR2.UI;
using UnityEngine;
using UnityEngine.Networking;

namespace RoR2
{
	// Token: 0x020009D2 RID: 2514
	public class SteamworksLobbyManager : PCLobbyManager
	{
		// Token: 0x06003999 RID: 14745 RVA: 0x00014F2E File Offset: 0x0001312E
		public override MPFeatures GetPlatformMPFeatureFlags()
		{
			return MPFeatures.HostGame | MPFeatures.FindGame;
		}

		// Token: 0x0600399A RID: 14746 RVA: 0x000EAE88 File Offset: 0x000E9088
		public override MPLobbyFeatures GetPlatformMPLobbyFeatureFlags()
		{
			return MPLobbyFeatures.CreateLobby | MPLobbyFeatures.SocialIcon | MPLobbyFeatures.HostPromotion | MPLobbyFeatures.Clipboard | MPLobbyFeatures.Invite | MPLobbyFeatures.UserIcon | MPLobbyFeatures.LeaveLobby | MPLobbyFeatures.LobbyDropdownOptions;
		}

		// Token: 0x0600399B RID: 14747 RVA: 0x000F04C2 File Offset: 0x000EE6C2
		public static SteamworksLobbyManager GetFromPlatformSystems()
		{
			return PlatformSystems.lobbyManager as SteamworksLobbyManager;
		}

		// Token: 0x1700055F RID: 1375
		// (get) Token: 0x0600399C RID: 14748 RVA: 0x000F04CE File Offset: 0x000EE6CE
		// (set) Token: 0x0600399D RID: 14749 RVA: 0x000F04D8 File Offset: 0x000EE6D8
		public UserID pendingSteamworksLobbyId
		{
			get
			{
				return this._pendingSteamworksLobbyId;
			}
			set
			{
				if (this._pendingSteamworksLobbyId.Equals(value))
				{
					return;
				}
				if (this._pendingSteamworksLobbyId.CID.value != null)
				{
					RoR2Application.onUpdate -= this.AttemptToJoinPendingSteamworksLobby;
				}
				this._pendingSteamworksLobbyId = value;
				if (this._pendingSteamworksLobbyId.CID.value != null)
				{
					RoR2Application.onUpdate += this.AttemptToJoinPendingSteamworksLobby;
				}
			}
		}

		// Token: 0x17000560 RID: 1376
		// (get) Token: 0x0600399E RID: 14750 RVA: 0x000F054C File Offset: 0x000EE74C
		private Client client
		{
			get
			{
				return Client.Instance;
			}
		}

		// Token: 0x17000561 RID: 1377
		// (get) Token: 0x0600399F RID: 14751 RVA: 0x000F0553 File Offset: 0x000EE753
		// (set) Token: 0x060039A0 RID: 14752 RVA: 0x000026ED File Offset: 0x000008ED
		public override bool isInLobby
		{
			get
			{
				return this.client != null && this.client.Lobby != null && this.client.Lobby.IsValid;
			}
			protected set
			{
			}
		}

		// Token: 0x17000562 RID: 1378
		// (get) Token: 0x060039A1 RID: 14753 RVA: 0x000F057C File Offset: 0x000EE77C
		// (set) Token: 0x060039A2 RID: 14754 RVA: 0x000F0584 File Offset: 0x000EE784
		public override bool ownsLobby
		{
			get
			{
				return this._ownsLobby;
			}
			protected set
			{
				if (value != this._ownsLobby)
				{
					this._ownsLobby = value;
					if (this._ownsLobby)
					{
						this.OnLobbyOwnershipGained();
						this.UpdatePlayerCount();
						return;
					}
					this.OnLobbyOwnershipLost();
				}
			}
		}

		// Token: 0x060039A3 RID: 14755 RVA: 0x000F05B1 File Offset: 0x000EE7B1
		private void UpdateOwnsLobby()
		{
			this.ownsLobby = this.client.Lobby.IsOwner;
		}

		// Token: 0x17000563 RID: 1379
		// (get) Token: 0x060039A4 RID: 14756 RVA: 0x000F05C9 File Offset: 0x000EE7C9
		public override bool hasMinimumPlayerCount
		{
			get
			{
				return this.newestLobbyData.totalPlayerCount >= this.minimumPlayerCount;
			}
		}

		// Token: 0x17000564 RID: 1380
		// (get) Token: 0x060039A5 RID: 14757 RVA: 0x000F05E1 File Offset: 0x000EE7E1
		// (set) Token: 0x060039A6 RID: 14758 RVA: 0x000F05E9 File Offset: 0x000EE7E9
		public int remoteMachineCount { get; private set; }

		// Token: 0x17000565 RID: 1381
		// (get) Token: 0x060039A7 RID: 14759 RVA: 0x000EAF65 File Offset: 0x000E9165
		public UserID serverId
		{
			get
			{
				return this.newestLobbyData.serverId;
			}
		}

		// Token: 0x17000566 RID: 1382
		// (get) Token: 0x060039A8 RID: 14760 RVA: 0x000F05F2 File Offset: 0x000EE7F2
		// (set) Token: 0x060039A9 RID: 14761 RVA: 0x000F05FA File Offset: 0x000EE7FA
		public override LobbyManager.LobbyData newestLobbyData { get; protected set; }

		// Token: 0x060039AA RID: 14762 RVA: 0x000F0604 File Offset: 0x000EE804
		public void Init()
		{
			Client instance = Client.Instance;
			instance.Lobby.OnChatMessageRecieved = new Action<ulong, byte[], int>(this.OnChatMessageReceived);
			instance.Lobby.OnLobbyCreated = new Action<bool>(this.OnLobbyCreated);
			instance.Lobby.OnLobbyDataUpdated = new Action(this.OnLobbyDataUpdated);
			instance.Lobby.OnLobbyJoined = new Action<bool>(this.OnLobbyJoined);
			instance.Lobby.OnLobbyMemberDataUpdated = new Action<ulong>(this.OnLobbyMemberDataUpdated);
			instance.Lobby.OnLobbyStateChanged = new Action<Lobby.MemberStateChange, ulong, ulong>(this.OnLobbyStateChanged);
			instance.Lobby.OnLobbyKicked = new Action<bool, ulong, ulong>(this.OnLobbyKicked);
			instance.Lobby.OnLobbyLeave = new Action<ulong>(this.OnLobbyLeave);
			instance.Lobby.OnUserInvitedToLobby = new Action<ulong, ulong>(this.OnUserInvitedToLobby);
			instance.Lobby.OnLobbyJoinRequested = new Action<ulong>(this.OnLobbyJoinRequested);
			instance.LobbyList.OnLobbiesUpdated = new Action(this.OnLobbiesUpdated);
			RoR2Application.onUpdate += this.StaticUpdate;
			this.newestLobbyData = new LobbyManager.LobbyData();
			LocalUserManager.onLocalUsersUpdated += this.UpdatePlayerCount;
			NetworkManagerSystem.onStartServerGlobal += this.OnStartHostingServer;
			NetworkManagerSystem.onStopServerGlobal += this.OnStopHostingServer;
			NetworkManagerSystem.onStopClientGlobal += this.OnStopClient;
			NetworkManagerSystem.onStopClientGlobal += delegate()
			{
				this.SetStartingIfOwner(false);
			};
			this.onLobbyOwnershipGained = (Action)Delegate.Combine(this.onLobbyOwnershipGained, new Action(delegate()
			{
				this.SetStartingIfOwner(false);
			}));
			this.SetStartingIfOwner(false);
			this.pendingSteamworksLobbyId = new UserID(this.GetLaunchParamsLobbyId());
		}

		// Token: 0x060039AB RID: 14763 RVA: 0x000F07BD File Offset: 0x000EE9BD
		public override int GetLobbyMemberPlayerCountByIndex(int memberIndex)
		{
			if (memberIndex >= this.playerCountsList.Count)
			{
				return 0;
			}
			return this.playerCountsList[memberIndex];
		}

		// Token: 0x17000567 RID: 1383
		// (get) Token: 0x060039AC RID: 14764 RVA: 0x000F07DB File Offset: 0x000EE9DB
		// (set) Token: 0x060039AD RID: 14765 RVA: 0x000F07E3 File Offset: 0x000EE9E3
		public override int calculatedTotalPlayerCount { get; protected set; }

		// Token: 0x17000568 RID: 1384
		// (get) Token: 0x060039AE RID: 14766 RVA: 0x000F07EC File Offset: 0x000EE9EC
		// (set) Token: 0x060039AF RID: 14767 RVA: 0x000F07F4 File Offset: 0x000EE9F4
		public override int calculatedExtraPlayersCount { get; protected set; }

		// Token: 0x17000569 RID: 1385
		// (get) Token: 0x060039B0 RID: 14768 RVA: 0x000F07FD File Offset: 0x000EE9FD
		// (set) Token: 0x060039B1 RID: 14769 RVA: 0x000026ED File Offset: 0x000008ED
		public override LobbyType currentLobbyType
		{
			get
			{
				return (LobbyType)Client.Instance.Lobby.LobbyType;
			}
			set
			{
			}
		}

		// Token: 0x1700056A RID: 1386
		// (get) Token: 0x060039B2 RID: 14770 RVA: 0x000F080E File Offset: 0x000EEA0E
		// (set) Token: 0x060039B3 RID: 14771 RVA: 0x000F0816 File Offset: 0x000EEA16
		public override bool IsBusy { get; set; }

		// Token: 0x060039B4 RID: 14772 RVA: 0x0008A315 File Offset: 0x00088515
		public override void CheckIfInitializedAndValid()
		{
			if (Client.Instance == null)
			{
				throw new ConCommandException("Steamworks client not available.");
			}
		}

		// Token: 0x060039B5 RID: 14773 RVA: 0x000F0820 File Offset: 0x000EEA20
		private void UpdatePlayerCount()
		{
			if (this.client != null && this.client.Lobby.IsValid)
			{
				int count = LocalUserManager.readOnlyLocalUsersList.Count;
				string memberData = this.client.Lobby.GetMemberData(this.client.SteamId, "player_count");
				int i = Math.Max(1, count);
				string @string = this.localPlayerCountToString.GetString(i);
				if (memberData != @string)
				{
					this.client.Lobby.SetMemberData("player_count", @string);
				}
				this.playerCountsList.Clear();
				this.calculatedTotalPlayerCount = 0;
				this.remoteMachineCount = 0;
				this.calculatedExtraPlayersCount = 0;
				ulong steamId = this.client.SteamId;
				foreach (ulong num in this.client.Lobby.GetMemberIDs())
				{
					int num2 = TextSerialization.TryParseInvariant(this.client.Lobby.GetMemberData(num, "player_count"), out num2) ? Math.Max(1, num2) : 1;
					if (num == steamId)
					{
						num2 = Math.Max(1, count);
					}
					else
					{
						int remoteMachineCount = this.remoteMachineCount + 1;
						this.remoteMachineCount = remoteMachineCount;
					}
					this.playerCountsList.Add(num2);
					this.calculatedTotalPlayerCount += num2;
					if (num2 > 1)
					{
						this.calculatedExtraPlayersCount += num2 - 1;
					}
				}
			}
			Action onPlayerCountUpdated = this.onPlayerCountUpdated;
			if (onPlayerCountUpdated == null)
			{
				return;
			}
			onPlayerCountUpdated();
		}

		// Token: 0x060039B6 RID: 14774 RVA: 0x000F0998 File Offset: 0x000EEB98
		private void OnLobbyChanged()
		{
			this.isInLobby = this.client.Lobby.IsValid;
			this.isInLobbyDelayed = this.isInLobby;
			this.UpdateOwnsLobby();
			this.UpdatePlayerCount();
			Action onLobbyChanged = this.onLobbyChanged;
			if (onLobbyChanged != null)
			{
				onLobbyChanged();
			}
			this.OnLobbyDataUpdated();
		}

		// Token: 0x060039B7 RID: 14775 RVA: 0x000F09EC File Offset: 0x000EEBEC
		public override void CreateLobby()
		{
			if (this.client == null)
			{
				return;
			}
			this.pendingSteamworksLobbyId = default(UserID);
			this.client.Lobby.Leave();
			base.awaitingCreate = true;
			this.client.Lobby.Create((Lobby.Type)this.preferredLobbyType, RoR2Application.maxPlayers);
		}

		// Token: 0x060039B8 RID: 14776 RVA: 0x000F0A44 File Offset: 0x000EEC44
		public override void JoinLobby(UserID uid)
		{
			CSteamID cid = uid.CID;
			if (this.client == null)
			{
				return;
			}
			if (!LocalUserManager.isAnyUserSignedIn)
			{
				return;
			}
			base.awaitingJoin = true;
			this.LeaveLobby();
			this.client.Lobby.Join(cid.steamValue);
		}

		// Token: 0x060039B9 RID: 14777 RVA: 0x000F0A8C File Offset: 0x000EEC8C
		public override void LeaveLobby()
		{
			Client client = this.client;
			if (client == null)
			{
				return;
			}
			client.Lobby.Leave();
		}

		// Token: 0x060039BA RID: 14778 RVA: 0x000F0AA4 File Offset: 0x000EECA4
		public override UserID[] GetLobbyMembers()
		{
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
			UserID[] array3 = null;
			if (array2 != null)
			{
				array3 = new UserID[array2.Length];
				for (int i = 0; i < array3.Length; i++)
				{
					array3[i] = new UserID(new CSteamID(array2[i]));
				}
			}
			return array3;
		}

		// Token: 0x060039BB RID: 14779 RVA: 0x000F0B00 File Offset: 0x000EED00
		public override bool ShouldShowPromoteButton()
		{
			return SteamLobbyFinder.running;
		}

		// Token: 0x060039BC RID: 14780 RVA: 0x000F0B08 File Offset: 0x000EED08
		private void Update()
		{
			if (this.client == null)
			{
				return;
			}
			if (this.startingFadeSet != (this.newestLobbyData.starting && !ClientScene.ready))
			{
				if (this.startingFadeSet)
				{
					FadeToBlackManager.fadeCount--;
				}
				else
				{
					FadeToBlackManager.fadeCount++;
				}
				this.startingFadeSet = !this.startingFadeSet;
			}
		}

		// Token: 0x060039BD RID: 14781 RVA: 0x000F0B6F File Offset: 0x000EED6F
		private void StaticUpdate()
		{
			if (this.client == null)
			{
				return;
			}
			this.UpdateOwnsLobby();
		}

		// Token: 0x060039BE RID: 14782 RVA: 0x000F0B80 File Offset: 0x000EED80
		private CSteamID GetLaunchParamsLobbyId()
		{
			string[] commandLineArgs = Environment.GetCommandLineArgs();
			for (int i = 0; i < commandLineArgs.Length - 1; i++)
			{
				CSteamID result;
				if (string.Equals(commandLineArgs[i], "+connect_lobby", StringComparison.OrdinalIgnoreCase) && CSteamID.TryParse(ArrayUtils.GetSafe<string>(commandLineArgs, i + 1, string.Empty), out result))
				{
					return result;
				}
			}
			return CSteamID.nil;
		}

		// Token: 0x060039BF RID: 14783 RVA: 0x000F0BD4 File Offset: 0x000EEDD4
		public void ForceLobbyDataUpdate()
		{
			Client client = this.client;
			Lobby lobby = (client != null) ? client.Lobby : null;
			if (lobby != null)
			{
				this.updateLobbyDataMethodInfo.Invoke(lobby, Array.Empty<object>());
			}
		}

		// Token: 0x060039C0 RID: 14784 RVA: 0x000F0C0C File Offset: 0x000EEE0C
		public void SendLobbyMessage(LobbyManager.LobbyMessageType messageType, NetworkWriter writer)
		{
			byte[] array = new byte[(int)(1 + writer.Position)];
			array[0] = (byte)messageType;
			Array.Copy(writer.AsArray(), 0, array, 1, (int)writer.Position);
			Client.Instance.Lobby.SendChatMessage(Encoding.UTF8.GetString(array));
		}

		// Token: 0x060039C1 RID: 14785 RVA: 0x000026ED File Offset: 0x000008ED
		public override void SetQuickplayCutoffTime(double cutoffTime)
		{
		}

		// Token: 0x060039C2 RID: 14786 RVA: 0x000EB8C7 File Offset: 0x000E9AC7
		public override double GetQuickplayCutoffTime()
		{
			return 0.0;
		}

		// Token: 0x060039C3 RID: 14787 RVA: 0x000026ED File Offset: 0x000008ED
		public override void OnCutoffTimerComplete()
		{
		}

		// Token: 0x060039C4 RID: 14788 RVA: 0x000F0C5C File Offset: 0x000EEE5C
		private void OnChatMessageReceived(ulong senderId, byte[] buffer, int byteCount)
		{
			try
			{
				bool isOwner = Client.Instance.Lobby.IsOwner;
				bool flag = senderId == Client.Instance.Lobby.Owner;
				bool flag2 = senderId == Client.Instance.SteamId;
				NetworkReader networkReader = new NetworkReader(buffer);
				if (byteCount >= 1)
				{
					LobbyManager.LobbyMessageType lobbyMessageType = (LobbyManager.LobbyMessageType)networkReader.ReadByte();
					Debug.LogFormat("Received Steamworks Lobby Message from {0} ({1}B). messageType={2}", new object[]
					{
						senderId,
						byteCount,
						lobbyMessageType
					});
					if (lobbyMessageType != LobbyManager.LobbyMessageType.Chat)
					{
						if (lobbyMessageType == LobbyManager.LobbyMessageType.Password)
						{
							string @string = networkReader.ReadString();
							if (flag2)
							{
								Debug.Log("Ignoring password message from self.");
							}
							else if (flag)
							{
								NetworkManagerSystem.cvClPassword.SetString(@string);
								Debug.Log("Received password to endpoint from lobby leader.");
							}
							else
							{
								Debug.Log("Ignoring password message from non-leader.");
							}
						}
					}
					else
					{
						string arg = networkReader.ReadString();
						SteamFriend steamFriend = Client.Instance.Friends.Get(senderId);
						Chat.AddMessage(string.Format("{0}: {1}", ((steamFriend != null) ? steamFriend.Name : null) ?? "???", arg));
					}
				}
				else
				{
					Debug.LogWarningFormat("Received SteamworksLobbyMessage from {0}, but the message was empty.", Array.Empty<object>());
				}
			}
			catch (Exception message)
			{
				Debug.LogError(message);
			}
		}

		// Token: 0x060039C5 RID: 14789 RVA: 0x000F0D9C File Offset: 0x000EEF9C
		private void OnLobbyCreated(bool success)
		{
			base.awaitingCreate = false;
			if (success)
			{
				Debug.LogFormat("Steamworks lobby creation succeeded. Lobby id = {0}", new object[]
				{
					this.client.Lobby.CurrentLobby
				});
				this.OnLobbyChanged();
				return;
			}
			Debug.Log("Steamworks lobby creation failed.");
		}

		// Token: 0x060039C6 RID: 14790 RVA: 0x000F0DEC File Offset: 0x000EEFEC
		protected void OnLobbyDataUpdated()
		{
			Lobby lobby = this.client.Lobby;
			this.newestLobbyData = (lobby.IsValid ? new LobbyManager.LobbyData(lobby) : new LobbyManager.LobbyData());
			this.UpdateOwnsLobby();
			this.UpdatePlayerCount();
			CSteamID b = new CSteamID(lobby.CurrentLobby);
			if (lobby.IsValid && !this.ownsLobby)
			{
				if (this.newestLobbyData.serverId.isValid)
				{
					int num = (this.newestLobbyData.serverId == new UserID(NetworkManagerSystem.singleton.serverP2PId) || NetworkManagerSystem.singleton.IsConnectedToServer(this.newestLobbyData.serverId)) ? 1 : 0;
					bool flag = string.CompareOrdinal(RoR2Application.GetBuildId(), this.newestLobbyData.buildId) == 0;
					if (num == 0 && flag)
					{
						NetworkManagerSystem.singleton.desiredHost = new HostDescription(this.newestLobbyData.serverId, HostDescription.HostType.Steam);
						this.lastHostingLobbyId = b;
					}
				}
				else if (this.lastHostingLobbyId == b)
				{
					Debug.LogFormat("Intercepting bad or out-of-order lobby update to server id.", Array.Empty<object>());
				}
				else
				{
					NetworkManagerSystem.singleton.desiredHost = HostDescription.none;
				}
			}
			Action onLobbyDataUpdated = this.onLobbyDataUpdated;
			if (onLobbyDataUpdated == null)
			{
				return;
			}
			onLobbyDataUpdated();
		}

		// Token: 0x060039C7 RID: 14791 RVA: 0x000F0F20 File Offset: 0x000EF120
		private void OnLobbyJoined(bool success)
		{
			base.awaitingJoin = false;
			if (success)
			{
				if (this.client.Lobby.CurrentLobbyData != null)
				{
					string buildId = RoR2Application.GetBuildId();
					string data = this.client.Lobby.CurrentLobbyData.GetData("build_id");
					if (buildId != data)
					{
						Debug.LogFormat("Lobby build_id mismatch, leaving lobby. Ours=\"{0}\" Theirs=\"{1}\"", new object[]
						{
							buildId,
							data
						});
						SimpleDialogBox simpleDialogBox = SimpleDialogBox.Create(null);
						simpleDialogBox.AddCancelButton(CommonLanguageTokens.ok, Array.Empty<object>());
						simpleDialogBox.headerToken = new SimpleDialogBox.TokenParamsPair
						{
							token = "STEAM_LOBBY_VERSION_MISMATCH_DIALOG_TITLE",
							formatParams = Array.Empty<object>()
						};
						SimpleDialogBox.TokenParamsPair descriptionToken = default(SimpleDialogBox.TokenParamsPair);
						descriptionToken.token = "STEAM_LOBBY_VERSION_MISMATCH_DIALOG_DESCRIPTION";
						object[] formatParams = new string[]
						{
							buildId,
							data
						};
						descriptionToken.formatParams = formatParams;
						simpleDialogBox.descriptionToken = descriptionToken;
						this.client.Lobby.Leave();
						return;
					}
				}
				Debug.LogFormat("Steamworks lobby join succeeded. Lobby id = {0}", new object[]
				{
					this.client.Lobby.CurrentLobby
				});
				this.OnLobbyChanged();
			}
			else
			{
				Debug.Log("Steamworks lobby join failed.");
				Console.instance.SubmitCmd(null, "steam_lobby_create_if_none", true);
			}
			Action<bool> onLobbyJoined = this.onLobbyJoined;
			if (onLobbyJoined == null)
			{
				return;
			}
			onLobbyJoined(success);
		}

		// Token: 0x060039C8 RID: 14792 RVA: 0x000F1070 File Offset: 0x000EF270
		private void OnLobbyMemberDataUpdated(ulong memberId)
		{
			this.UpdateOwnsLobby();
			Action<UserID> onLobbyMemberDataUpdated = this.onLobbyMemberDataUpdated;
			if (onLobbyMemberDataUpdated == null)
			{
				return;
			}
			onLobbyMemberDataUpdated(new UserID(new CSteamID(memberId)));
		}

		// Token: 0x060039C9 RID: 14793 RVA: 0x000F1094 File Offset: 0x000EF294
		protected void OnLobbyStateChanged(Lobby.MemberStateChange memberStateChange, ulong initiatorUserId, ulong affectedUserId)
		{
			Debug.LogFormat("OnLobbyStateChanged memberStateChange={0} initiatorUserId={1} affectedUserId={2}", new object[]
			{
				memberStateChange,
				initiatorUserId,
				affectedUserId
			});
			this.OnLobbyChanged();
			Action onLobbyStateChanged = this.onLobbyStateChanged;
			if (onLobbyStateChanged == null)
			{
				return;
			}
			onLobbyStateChanged();
		}

		// Token: 0x060039CA RID: 14794 RVA: 0x000F10E2 File Offset: 0x000EF2E2
		private void OnLobbyKicked(bool kickedDueToDisconnect, ulong lobbyId, ulong adminId)
		{
			Debug.LogFormat("Kicked from lobby. kickedDueToDisconnect={0} lobbyId={1} adminId={2}", new object[]
			{
				kickedDueToDisconnect,
				lobbyId,
				adminId
			});
			this.OnLobbyChanged();
		}

		// Token: 0x060039CB RID: 14795 RVA: 0x000F1115 File Offset: 0x000EF315
		private void OnLobbyLeave(ulong lobbyId)
		{
			Debug.LogFormat("Left lobby {0}.", new object[]
			{
				lobbyId
			});
			Action<UserID> onLobbyLeave = this.onLobbyLeave;
			if (onLobbyLeave != null)
			{
				onLobbyLeave(new UserID(new CSteamID(lobbyId)));
			}
			this.OnLobbyChanged();
		}

		// Token: 0x060039CC RID: 14796 RVA: 0x000F1152 File Offset: 0x000EF352
		private void OnLobbyJoinRequested(ulong lobbyId)
		{
			Debug.LogFormat("Request to join lobby {0} received. Attempting to join lobby.", new object[]
			{
				lobbyId
			});
			this.pendingSteamworksLobbyId = new UserID(new CSteamID(lobbyId));
		}

		// Token: 0x060039CD RID: 14797 RVA: 0x000F117E File Offset: 0x000EF37E
		private void OnUserInvitedToLobby(ulong lobbyId, ulong senderId)
		{
			Debug.LogFormat("Received invitation to lobby {0} from sender {1}.", new object[]
			{
				lobbyId,
				senderId
			});
		}

		// Token: 0x060039CE RID: 14798 RVA: 0x000F11A4 File Offset: 0x000EF3A4
		public bool RequestLobbyList(object requester, LobbyList.Filter filter, Action<List<LobbyList.Lobby>> callback)
		{
			if (requester != null)
			{
				foreach (SteamworksLobbyManager.LobbyRefreshRequest lobbyRefreshRequest in this.lobbyRefreshRequests)
				{
					if (requester == lobbyRefreshRequest.requester)
					{
						return false;
					}
				}
			}
			SteamworksLobbyManager.LobbyRefreshRequest item = new SteamworksLobbyManager.LobbyRefreshRequest
			{
				requester = requester,
				filter = filter,
				callback = callback
			};
			this.lobbyRefreshRequests.Enqueue(item);
			this.UpdateRefreshRequestQueue();
			return true;
		}

		// Token: 0x060039CF RID: 14799 RVA: 0x000F1238 File Offset: 0x000EF438
		private void UpdateRefreshRequestQueue()
		{
			if (this.currentRefreshRequest != null)
			{
				return;
			}
			if (this.lobbyRefreshRequests.Count == 0)
			{
				return;
			}
			this.currentRefreshRequest = new SteamworksLobbyManager.LobbyRefreshRequest?(this.lobbyRefreshRequests.Dequeue());
			this.client.LobbyList.Refresh(this.currentRefreshRequest.Value.filter);
		}

		// Token: 0x060039D0 RID: 14800 RVA: 0x000F1298 File Offset: 0x000EF498
		private void OnLobbiesUpdated()
		{
			if (this.currentRefreshRequest != null)
			{
				ref SteamworksLobbyManager.LobbyRefreshRequest value = this.currentRefreshRequest.Value;
				this.currentRefreshRequest = null;
				List<LobbyList.Lobby> lobbies = this.client.LobbyList.Lobbies;
				value.callback(lobbies);
			}
			Action onLobbiesUpdated = this.onLobbiesUpdated;
			if (onLobbiesUpdated != null)
			{
				onLobbiesUpdated();
			}
			this.UpdateRefreshRequestQueue();
		}

		// Token: 0x060039D1 RID: 14801 RVA: 0x000F12FC File Offset: 0x000EF4FC
		private void OnStopClient()
		{
			if (NetworkManagerSystem.singleton && NetworkManagerSystem.singleton.client != null && NetworkManagerSystem.singleton.client.connection != null)
			{
				NetworkConnection connection = NetworkManagerSystem.singleton.client.connection;
				bool flag = Util.ConnectionIsLocal(connection);
				bool flag2;
				if (connection is SteamNetworkConnection)
				{
					flag2 = (((SteamNetworkConnection)connection).steamId == this.newestLobbyData.serverId);
				}
				else
				{
					flag2 = (connection.address == this.newestLobbyData.serverAddressPortPair.address);
				}
				if (flag && this.ownsLobby)
				{
					this.client.Lobby.CurrentLobbyData.RemoveData("server_id");
				}
				if (!flag && flag2)
				{
					this.client.Lobby.Leave();
				}
			}
		}

		// Token: 0x060039D2 RID: 14802 RVA: 0x000F13D4 File Offset: 0x000EF5D4
		private void OnStartHostingServer()
		{
			this.hostingServer = true;
		}

		// Token: 0x060039D3 RID: 14803 RVA: 0x000F13DD File Offset: 0x000EF5DD
		private void OnStopHostingServer()
		{
			this.hostingServer = false;
		}

		// Token: 0x060039D4 RID: 14804 RVA: 0x000F13E6 File Offset: 0x000EF5E6
		public void JoinOrStartMigrate(UserID newLobbyId)
		{
			if (this.ownsLobby)
			{
				this.StartMigrateLobby(newLobbyId.CID);
				return;
			}
			this.client.Lobby.Leave();
			this.JoinLobby(newLobbyId);
		}

		// Token: 0x060039D5 RID: 14805 RVA: 0x000F1414 File Offset: 0x000EF614
		public void StartMigrateLobby(CSteamID newLobbyId)
		{
			this.client.Lobby.Joinable = false;
			this.client.Lobby.CurrentLobbyData.SetData("migration_id", TextSerialization.ToStringInvariant(newLobbyId.steamValue));
		}

		// Token: 0x060039D6 RID: 14806 RVA: 0x000F1450 File Offset: 0x000EF650
		private void AttemptToJoinPendingSteamworksLobby()
		{
			if (!LocalUserManager.isAnyUserSignedIn)
			{
				return;
			}
			if (this.pendingSteamworksLobbyId != default(UserID))
			{
				this.JoinLobby(this.pendingSteamworksLobbyId);
				this.pendingSteamworksLobbyId = default(UserID);
			}
		}

		// Token: 0x060039D7 RID: 14807 RVA: 0x000F1498 File Offset: 0x000EF698
		public void SetLobbyQuickPlayQueuedIfOwner(bool quickplayQueuedState)
		{
			Lobby lobby = this.client.Lobby;
			if (((lobby != null) ? lobby.CurrentLobbyData : null) == null)
			{
				return;
			}
			lobby.CurrentLobbyData.SetData("qp", quickplayQueuedState ? "1" : "0");
			if (!quickplayQueuedState)
			{
				lobby.LobbyType = (Lobby.Type)this.preferredLobbyType;
			}
		}

		// Token: 0x060039D8 RID: 14808 RVA: 0x000F14F0 File Offset: 0x000EF6F0
		public void SetLobbyQuickPlayCutoffTimeIfOwner(uint? timestamp)
		{
			Lobby lobby = this.client.Lobby;
			if (((lobby != null) ? lobby.CurrentLobbyData : null) == null)
			{
				return;
			}
			if (timestamp == null)
			{
				lobby.CurrentLobbyData.RemoveData("qp_cutoff_time");
				return;
			}
			string v = TextSerialization.ToStringInvariant(timestamp.Value);
			lobby.CurrentLobbyData.SetData("qp_cutoff_time", v);
		}

		// Token: 0x060039D9 RID: 14809 RVA: 0x000F1554 File Offset: 0x000EF754
		public void SetStartingIfOwner(bool startingState)
		{
			Lobby lobby = this.client.Lobby;
			if (((lobby != null) ? lobby.CurrentLobbyData : null) == null)
			{
				return;
			}
			Lobby.LobbyData currentLobbyData = lobby.CurrentLobbyData;
			if (currentLobbyData == null)
			{
				return;
			}
			currentLobbyData.SetData("starting", startingState ? "1" : "0");
		}

		// Token: 0x060039DA RID: 14810 RVA: 0x000EC5CA File Offset: 0x000EA7CA
		protected void OnLobbyOwnershipGained()
		{
			Action onLobbyOwnershipGained = this.onLobbyOwnershipGained;
			if (onLobbyOwnershipGained == null)
			{
				return;
			}
			onLobbyOwnershipGained();
		}

		// Token: 0x060039DB RID: 14811 RVA: 0x000EC5DC File Offset: 0x000EA7DC
		private void OnLobbyOwnershipLost()
		{
			Action onLobbyOwnershipLost = this.onLobbyOwnershipLost;
			if (onLobbyOwnershipLost == null)
			{
				return;
			}
			onLobbyOwnershipLost();
		}

		// Token: 0x060039DC RID: 14812 RVA: 0x000F15A4 File Offset: 0x000EF7A4
		public static void CreateCannotJoinSteamLobbyPopup()
		{
			SimpleDialogBox simpleDialogBox = SimpleDialogBox.Create(null);
			simpleDialogBox.headerToken = new SimpleDialogBox.TokenParamsPair
			{
				token = "EOS_CANNOT_JOIN_STEAM_LOBBY_HEADER",
				formatParams = Array.Empty<object>()
			};
			simpleDialogBox.descriptionToken = new SimpleDialogBox.TokenParamsPair
			{
				token = "EOS_CANNOT_JOIN_STEAM_LOBBY_MESSAGE",
				formatParams = Array.Empty<object>()
			};
			simpleDialogBox.AddCancelButton(CommonLanguageTokens.ok, Array.Empty<object>());
		}

		// Token: 0x060039DD RID: 14813 RVA: 0x000F1615 File Offset: 0x000EF815
		public override string GetLobbyID()
		{
			this.CheckIfInitializedAndValid();
			return TextSerialization.ToStringInvariant(this.client.Lobby.CurrentLobby);
		}

		// Token: 0x060039DE RID: 14814 RVA: 0x000F1632 File Offset: 0x000EF832
		public override bool CheckLobbyIdValidity(string lobbyID)
		{
			return SteamworksLobbyManager.<CheckLobbyIdValidity>g__IsDigitsOnly|124_0(lobbyID);
		}

		// Token: 0x060039DF RID: 14815 RVA: 0x000F1640 File Offset: 0x000EF840
		public override void JoinLobby(ConCommandArgs args)
		{
			string text = args[0];
			CSteamID csteamID;
			if (CSteamID.TryParse(text, out csteamID))
			{
				this.CheckIfInitializedAndValid();
				Debug.LogFormat("Enqueuing join for lobby {0}...", new object[]
				{
					text
				});
				this.pendingSteamworksLobbyId = new UserID(csteamID.steamValue);
				return;
			}
			Debug.LogFormat("Failed parsing lobby ID from {0}...", new object[]
			{
				text
			});
		}

		// Token: 0x060039E0 RID: 14816 RVA: 0x000F16A0 File Offset: 0x000EF8A0
		public override void LobbyCreate(ConCommandArgs args)
		{
			SteamworksLobbyManager steamworksLobbyManager = PlatformSystems.lobbyManager as SteamworksLobbyManager;
			steamworksLobbyManager.CheckIfInitializedAndValid();
			if (!LocalUserManager.isAnyUserSignedIn)
			{
				throw new ConCommandException("Cannot create a Steamworks lobby without any local users signed in.");
			}
			steamworksLobbyManager.CreateLobby();
		}

		// Token: 0x060039E1 RID: 14817 RVA: 0x000F16CC File Offset: 0x000EF8CC
		public override void LobbyCreateIfNone(ConCommandArgs args)
		{
			SteamworksLobbyManager steamworksLobbyManager = PlatformSystems.lobbyManager as SteamworksLobbyManager;
			Client client = steamworksLobbyManager.client;
			steamworksLobbyManager.CheckIfInitializedAndValid();
			if (!LocalUserManager.isAnyUserSignedIn)
			{
				throw new ConCommandException("Cannot create a Steamworks lobby without any local users signed in.");
			}
			if (!client.Lobby.IsValid)
			{
				steamworksLobbyManager.CreateLobby();
			}
		}

		// Token: 0x060039E2 RID: 14818 RVA: 0x000F1718 File Offset: 0x000EF918
		public override void LobbyLeave(ConCommandArgs args)
		{
			SteamworksLobbyManager steamworksLobbyManager = PlatformSystems.lobbyManager as SteamworksLobbyManager;
			steamworksLobbyManager.CheckIfInitializedAndValid();
			steamworksLobbyManager.pendingSteamworksLobbyId = default(UserID);
			steamworksLobbyManager.LeaveLobby();
		}

		// Token: 0x060039E3 RID: 14819 RVA: 0x000F174C File Offset: 0x000EF94C
		public override void LobbyAssignOwner(ConCommandArgs args)
		{
			SteamworksLobbyManager steamworksLobbyManager = PlatformSystems.lobbyManager as SteamworksLobbyManager;
			Client client = steamworksLobbyManager.client;
			steamworksLobbyManager.CheckIfInitializedAndValid();
			Debug.LogFormat("Promoting {0} to lobby leader...", new object[]
			{
				args[0]
			});
			client.Lobby.Owner = args.GetArgSteamID(0).steamValue;
		}

		// Token: 0x060039E4 RID: 14820 RVA: 0x000F17A4 File Offset: 0x000EF9A4
		public override void LobbyInvite(ConCommandArgs args)
		{
			SteamworksLobbyManager steamworksLobbyManager = PlatformSystems.lobbyManager as SteamworksLobbyManager;
			Client client = steamworksLobbyManager.client;
			steamworksLobbyManager.CheckIfInitializedAndValid();
			client.Lobby.InviteUserToLobby(args.GetArgSteamID(0).steamValue);
		}

		// Token: 0x060039E5 RID: 14821 RVA: 0x000F17E0 File Offset: 0x000EF9E0
		public static void DoSteamLobbyOpenOverlay()
		{
			Client instance = Client.Instance;
			NetworkManagerSystemSteam.CheckSteamworks();
			instance.Overlay.OpenInviteDialog(instance.Lobby.CurrentLobby);
		}

		// Token: 0x060039E6 RID: 14822 RVA: 0x000F180E File Offset: 0x000EFA0E
		public override void LobbyOpenInviteOverlay(ConCommandArgs args)
		{
			(PlatformSystems.lobbyManager as SteamworksLobbyManager).CheckIfInitializedAndValid();
			SteamworksLobbyManager.DoSteamLobbyOpenOverlay();
		}

		// Token: 0x060039E7 RID: 14823 RVA: 0x000F1824 File Offset: 0x000EFA24
		public override void LobbyCopyToClipboard(ConCommandArgs args)
		{
			string lobbyID = this.GetLobbyID();
			if (!string.IsNullOrWhiteSpace(lobbyID))
			{
				GUIUtility.systemCopyBuffer = lobbyID;
			}
			Chat.AddMessage(Language.GetString("STEAM_COPY_LOBBY_TO_CLIPBOARD_MESSAGE"));
		}

		// Token: 0x060039E8 RID: 14824 RVA: 0x000F1858 File Offset: 0x000EFA58
		public override void LobbyPrintData(ConCommandArgs args)
		{
			SteamworksLobbyManager steamworksLobbyManager = PlatformSystems.lobbyManager as SteamworksLobbyManager;
			Client client = steamworksLobbyManager.client;
			steamworksLobbyManager.CheckIfInitializedAndValid();
			if (client.Lobby.IsValid)
			{
				List<string> list = new List<string>();
				foreach (KeyValuePair<string, string> keyValuePair in client.Lobby.CurrentLobbyData.GetAllData())
				{
					list.Add(string.Format("\"{0}\" = \"{1}\"", keyValuePair.Key, keyValuePair.Value));
				}
				Debug.Log(string.Join("\n", list.ToArray()));
			}
		}

		// Token: 0x060039E9 RID: 14825 RVA: 0x000F190C File Offset: 0x000EFB0C
		public override void DisplayId(ConCommandArgs args)
		{
			SteamworksLobbyManager steamworksLobbyManager = PlatformSystems.lobbyManager as SteamworksLobbyManager;
			Client client = steamworksLobbyManager.client;
			steamworksLobbyManager.CheckIfInitializedAndValid();
			Debug.LogFormat("Steam id = {0}", new object[]
			{
				client.SteamId
			});
		}

		// Token: 0x060039EA RID: 14826 RVA: 0x000F1950 File Offset: 0x000EFB50
		public override void DisplayLobbyId(ConCommandArgs args)
		{
			SteamworksLobbyManager steamworksLobbyManager = PlatformSystems.lobbyManager as SteamworksLobbyManager;
			Client client = steamworksLobbyManager.client;
			steamworksLobbyManager.CheckIfInitializedAndValid();
			Debug.LogFormat("Lobby id = {0}", new object[]
			{
				client.Lobby.CurrentLobby
			});
		}

		// Token: 0x060039EB RID: 14827 RVA: 0x000F1998 File Offset: 0x000EFB98
		public override void LobbyPrintMembers(ConCommandArgs args)
		{
			SteamworksLobbyManager steamworksLobbyManager = PlatformSystems.lobbyManager as SteamworksLobbyManager;
			Client client = steamworksLobbyManager.client;
			steamworksLobbyManager.CheckIfInitializedAndValid();
			ulong[] memberIDs = client.Lobby.GetMemberIDs();
			string[] array = new string[memberIDs.Length];
			for (int i = 0; i < memberIDs.Length; i++)
			{
				array[i] = string.Format("[{0}]{1} id={2} name={3}", new object[]
				{
					i,
					(client.Lobby.Owner == memberIDs[i]) ? "*" : " ",
					memberIDs[i],
					client.Friends.GetName(memberIDs[i])
				});
			}
			Debug.Log(string.Join("\n", array));
		}

		// Token: 0x060039EC RID: 14828 RVA: 0x000F1A48 File Offset: 0x000EFC48
		public override void ClearLobbies(ConCommandArgs args)
		{
			SteamworksLobbyManager steamworksLobbyManager = PlatformSystems.lobbyManager as SteamworksLobbyManager;
			Client client = steamworksLobbyManager.client;
			LobbyList.Filter filter = new LobbyList.Filter();
			filter.MaxResults = new int?(50);
			filter.DistanceFilter = LobbyList.Filter.Distance.Worldwide;
			steamworksLobbyManager.RequestLobbyList(null, filter, delegate(List<LobbyList.Lobby> lobbies)
			{
				foreach (LobbyList.Lobby message in lobbies)
				{
					Debug.Log(message);
				}
			});
		}

		// Token: 0x060039ED RID: 14829 RVA: 0x000F1AA7 File Offset: 0x000EFCA7
		public override void LobbyUpdatePlayerCount(ConCommandArgs args)
		{
			(PlatformSystems.lobbyManager as SteamworksLobbyManager).UpdatePlayerCount();
		}

		// Token: 0x060039EE RID: 14830 RVA: 0x000026ED File Offset: 0x000008ED
		public override void LobbyForceUpdateData(ConCommandArgs args)
		{
		}

		// Token: 0x060039EF RID: 14831 RVA: 0x000F1AB8 File Offset: 0x000EFCB8
		public override void LobbyPrintList(ConCommandArgs args)
		{
			SteamworksLobbyManager steamworksLobbyManager = PlatformSystems.lobbyManager as SteamworksLobbyManager;
			Client client = steamworksLobbyManager.client;
			steamworksLobbyManager.CheckIfInitializedAndValid();
			List<LobbyList.Lobby> lobbies = client.LobbyList.Lobbies;
			string[] array = new string[lobbies.Count];
			for (int i = 0; i < lobbies.Count; i++)
			{
				array[i] = string.Format("[{0}] id={1}\n      players={2}/{3},\n      owner=\"{4}\"", new object[]
				{
					i,
					lobbies[i].LobbyID,
					lobbies[i].NumMembers,
					lobbies[i].MemberLimit,
					client.Friends.GetName(lobbies[i].Owner)
				});
			}
			Debug.Log(string.Join("\n", array));
		}

		// Token: 0x060039F0 RID: 14832 RVA: 0x000F1B88 File Offset: 0x000EFD88
		public override bool IsLobbyOwner(UserID user)
		{
			return Client.Instance.Lobby.Owner == user.CID.steamValue && user != default(UserID);
		}

		// Token: 0x060039F1 RID: 14833 RVA: 0x000026ED File Offset: 0x000008ED
		public override void AutoMatchmake()
		{
		}

		// Token: 0x060039F2 RID: 14834 RVA: 0x000F1BC2 File Offset: 0x000EFDC2
		public override bool IsLobbyOwner()
		{
			return this.isInLobby && this._ownsLobby;
		}

		// Token: 0x060039F3 RID: 14835 RVA: 0x000EC633 File Offset: 0x000EA833
		public override bool CanInvite()
		{
			return !this.IsBusy;
		}

		// Token: 0x060039F4 RID: 14836 RVA: 0x000F1BD4 File Offset: 0x000EFDD4
		public override void OnStartPrivateGame()
		{
			this.CreateLobby();
		}

		// Token: 0x060039F5 RID: 14837 RVA: 0x000F1BDC File Offset: 0x000EFDDC
		public override void ToggleQuickplay()
		{
			Console.instance.SubmitCmd(null, SteamLobbyFinder.running ? "steam_quickplay_stop" : "steam_quickplay_start", false);
		}

		// Token: 0x060039F6 RID: 14838 RVA: 0x000026ED File Offset: 0x000008ED
		public override void CheckIfInvited()
		{
		}

		// Token: 0x060039F7 RID: 14839 RVA: 0x000026ED File Offset: 0x000008ED
		public override void CheckBusyTimer()
		{
		}

		// Token: 0x060039F8 RID: 14840 RVA: 0x0000CF8A File Offset: 0x0000B18A
		public override bool ShouldEnableQuickplayButton()
		{
			return false;
		}

		// Token: 0x060039F9 RID: 14841 RVA: 0x000F1BFD File Offset: 0x000EFDFD
		public override bool ShouldEnableStartPrivateGameButton()
		{
			return !this.newestLobbyData.quickplayQueued && this.ownsLobby;
		}

		// Token: 0x060039FA RID: 14842 RVA: 0x000F1C14 File Offset: 0x000EFE14
		public override string GetUserDisplayName(UserID user)
		{
			Client instance = Client.Instance;
			string result = "none";
			if (instance != null)
			{
				result = instance.Friends.GetName(user.CID.steamValue);
			}
			return result;
		}

		// Token: 0x060039FB RID: 14843 RVA: 0x000F1C48 File Offset: 0x000EFE48
		public override void OpenInviteOverlay()
		{
			SteamworksLobbyManager steamworksLobbyManager = PlatformSystems.lobbyManager as SteamworksLobbyManager;
			Client client = steamworksLobbyManager.client;
			steamworksLobbyManager.CheckIfInitializedAndValid();
			NetworkManagerSystemSteam.CheckSteamworks();
			client.Overlay.OpenInviteDialog(client.Lobby.CurrentLobby);
		}

		// Token: 0x060039FC RID: 14844 RVA: 0x000F1C88 File Offset: 0x000EFE88
		public override void SetLobbyTypeConVarString(string newValue)
		{
			(PlatformSystems.lobbyManager as SteamworksLobbyManager).CheckIfInitializedAndValid();
			if (Client.Instance.Lobby.LobbyType == Lobby.Type.Error || !Client.Instance.Lobby.IsOwner)
			{
				throw new ConCommandException("Lobby type cannot be set while not the owner of a valid lobby.");
			}
			Lobby.Type type = Lobby.Type.Error;
			PCLobbyManager.SteamLobbyTypeConVar.instance.GetEnumValueAbstract<Lobby.Type>(newValue, ref type);
			if (type == Lobby.Type.Error)
			{
				throw new ConCommandException("Lobby type \"Error\" is not allowed.");
			}
			Client.Instance.Lobby.LobbyType = type;
		}

		// Token: 0x060039FD RID: 14845 RVA: 0x000F1D00 File Offset: 0x000EFF00
		public override string GetLobbyTypeConVarString()
		{
			Client instance = Client.Instance;
			return ((instance != null) ? instance.Lobby.LobbyType.ToString() : null) ?? string.Empty;
		}

		// Token: 0x06003A01 RID: 14849 RVA: 0x000F1DA8 File Offset: 0x000EFFA8
		[CompilerGenerated]
		internal static bool <CheckLobbyIdValidity>g__IsDigitsOnly|124_0(string str)
		{
			foreach (char c in str)
			{
				if (c < '0' || c > '9')
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x040038FC RID: 14588
		public bool isInLobbyDelayed;

		// Token: 0x040038FD RID: 14589
		private const MPFeatures PlatformFeatureFlags = MPFeatures.HostGame | MPFeatures.FindGame;

		// Token: 0x040038FE RID: 14590
		private const MPLobbyFeatures PlatformLobbyUIFlags = MPLobbyFeatures.CreateLobby | MPLobbyFeatures.SocialIcon | MPLobbyFeatures.HostPromotion | MPLobbyFeatures.Clipboard | MPLobbyFeatures.Invite | MPLobbyFeatures.UserIcon | MPLobbyFeatures.LeaveLobby | MPLobbyFeatures.LobbyDropdownOptions;

		// Token: 0x040038FF RID: 14591
		private UserID _pendingSteamworksLobbyId;

		// Token: 0x04003900 RID: 14592
		private bool _ownsLobby;

		// Token: 0x04003901 RID: 14593
		private int minimumPlayerCount = 2;

		// Token: 0x04003903 RID: 14595
		public const string mdEdition = "v";

		// Token: 0x04003904 RID: 14596
		public const string mdAppId = "appid";

		// Token: 0x04003905 RID: 14597
		public const string mdTotalMaxPlayers = "total_max_players";

		// Token: 0x04003906 RID: 14598
		public const string mdPlayerCount = "player_count";

		// Token: 0x04003907 RID: 14599
		public const string mdQuickplayQueued = "qp";

		// Token: 0x04003908 RID: 14600
		public const string mdQuickplayCutoffTime = "qp_cutoff_time";

		// Token: 0x04003909 RID: 14601
		public const string mdStarting = "starting";

		// Token: 0x0400390A RID: 14602
		public const string mdBuildId = "build_id";

		// Token: 0x0400390B RID: 14603
		public const string mdServerId = "server_id";

		// Token: 0x0400390C RID: 14604
		public const string mdServerAddress = "server_address";

		// Token: 0x0400390D RID: 14605
		public const string mdMap = "_map";

		// Token: 0x0400390E RID: 14606
		public const string mdRuleBook = "rulebook";

		// Token: 0x0400390F RID: 14607
		public const string mdMigrationId = "migration_id";

		// Token: 0x04003910 RID: 14608
		public const string mdHasPassword = "_pw";

		// Token: 0x04003911 RID: 14609
		public const string mdIsDedicatedServer = "_ds";

		// Token: 0x04003912 RID: 14610
		public const string mdServerName = "_svnm";

		// Token: 0x04003913 RID: 14611
		public const string mdServerTags = "_svtags";

		// Token: 0x04003914 RID: 14612
		public const string mdServerMaxPlayers = "_svmpl";

		// Token: 0x04003915 RID: 14613
		public const string mdServerPlayerCount = "_svplc";

		// Token: 0x04003916 RID: 14614
		public const string mdGameModeName = "_svgm";

		// Token: 0x04003917 RID: 14615
		public const string mdModHash = "_mh";

		// Token: 0x04003919 RID: 14617
		private readonly List<int> playerCountsList = new List<int>();

		// Token: 0x0400391D RID: 14621
		private MemoizedToString<int, ToStringImplementationInvariant> localPlayerCountToString = MemoizedToString<int, ToStringImplementationInvariant>.GetNew();

		// Token: 0x0400391E RID: 14622
		private bool startingFadeSet;

		// Token: 0x0400391F RID: 14623
		private readonly MethodInfo updateLobbyDataMethodInfo = typeof(Lobby).GetMethod("UpdateLobbyData", BindingFlags.Instance | BindingFlags.NonPublic);

		// Token: 0x04003920 RID: 14624
		private CSteamID lastHostingLobbyId;

		// Token: 0x04003921 RID: 14625
		private Queue<SteamworksLobbyManager.LobbyRefreshRequest> lobbyRefreshRequests = new Queue<SteamworksLobbyManager.LobbyRefreshRequest>();

		// Token: 0x04003922 RID: 14626
		private SteamworksLobbyManager.LobbyRefreshRequest? currentRefreshRequest;

		// Token: 0x04003923 RID: 14627
		private bool hostingServer;

		// Token: 0x04003924 RID: 14628
		private UserID currentServerId;

		// Token: 0x04003925 RID: 14629
		private MemoizedToString<CSteamID, ToStringDefault<CSteamID>> currentServerIdString = MemoizedToString<CSteamID, ToStringDefault<CSteamID>>.GetNew();

		// Token: 0x020009D3 RID: 2515
		private struct LobbyRefreshRequest
		{
			// Token: 0x04003926 RID: 14630
			public object requester;

			// Token: 0x04003927 RID: 14631
			public LobbyList.Filter filter;

			// Token: 0x04003928 RID: 14632
			public Action<List<LobbyList.Lobby>> callback;
		}
	}
}
