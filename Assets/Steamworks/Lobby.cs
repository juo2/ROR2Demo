using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using SteamNative;

namespace Facepunch.Steamworks
{
	// Token: 0x02000168 RID: 360
	public class Lobby : IDisposable
	{
		// Token: 0x06000AB3 RID: 2739 RVA: 0x00035898 File Offset: 0x00033A98
		public Lobby(Client c)
		{
			this.client = c;
			this.OnLobbyJoinRequested = new Action<ulong>(this.Join);
			this.client.RegisterCallback<LobbyDataUpdate_t>(new Action<LobbyDataUpdate_t>(this.OnLobbyDataUpdatedAPI));
			this.client.RegisterCallback<LobbyChatMsg_t>(new Action<LobbyChatMsg_t>(this.OnLobbyChatMessageRecievedAPI));
			this.client.RegisterCallback<LobbyChatUpdate_t>(new Action<LobbyChatUpdate_t>(this.OnLobbyStateUpdatedAPI));
			this.client.RegisterCallback<GameLobbyJoinRequested_t>(new Action<GameLobbyJoinRequested_t>(this.OnLobbyJoinRequestedAPI));
			this.client.RegisterCallback<LobbyInvite_t>(new Action<LobbyInvite_t>(this.OnUserInvitedToLobbyAPI));
			this.client.RegisterCallback<PersonaStateChange_t>(new Action<PersonaStateChange_t>(this.OnLobbyMemberPersonaChangeAPI));
			this.client.RegisterCallback<LobbyKicked_t>(new Action<LobbyKicked_t>(this.OnLobbyKickedAPI));
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x06000AB4 RID: 2740 RVA: 0x00035965 File Offset: 0x00033B65
		// (set) Token: 0x06000AB5 RID: 2741 RVA: 0x0003596D File Offset: 0x00033B6D
		public ulong CurrentLobby { get; private set; }

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x06000AB6 RID: 2742 RVA: 0x00035976 File Offset: 0x00033B76
		// (set) Token: 0x06000AB7 RID: 2743 RVA: 0x0003597E File Offset: 0x00033B7E
		public Lobby.LobbyData CurrentLobbyData { get; private set; }

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x06000AB8 RID: 2744 RVA: 0x00035987 File Offset: 0x00033B87
		public bool IsValid
		{
			get
			{
				return this.CurrentLobby > 0UL;
			}
		}

		// Token: 0x06000AB9 RID: 2745 RVA: 0x00035993 File Offset: 0x00033B93
		public void Join(ulong lobbyID)
		{
			this.Leave();
			this.client.native.matchmaking.JoinLobby(lobbyID, new Action<LobbyEnter_t, bool>(this.OnLobbyJoinedAPI));
		}

		// Token: 0x06000ABA RID: 2746 RVA: 0x000359C4 File Offset: 0x00033BC4
		private void OnLobbyJoinedAPI(LobbyEnter_t callback, bool error)
		{
			if (error || callback.EChatRoomEnterResponse != 1U)
			{
				if (this.OnLobbyJoined != null)
				{
					this.OnLobbyJoined(false);
				}
				return;
			}
			if (this.CurrentLobby != 0UL)
			{
				this.Leave();
			}
			this.CurrentLobby = callback.SteamIDLobby;
			this.UpdateLobbyData();
			if (this.OnLobbyJoined != null)
			{
				this.OnLobbyJoined(true);
			}
		}

		// Token: 0x06000ABB RID: 2747 RVA: 0x00035A26 File Offset: 0x00033C26
		public void Create(Lobby.Type lobbyType, int maxMembers)
		{
			this.client.native.matchmaking.CreateLobby((LobbyType)lobbyType, maxMembers, new Action<LobbyCreated_t, bool>(this.OnLobbyCreatedAPI));
			this.createdLobbyType = lobbyType;
		}

		// Token: 0x06000ABC RID: 2748 RVA: 0x00035A54 File Offset: 0x00033C54
		internal void OnLobbyCreatedAPI(LobbyCreated_t callback, bool error)
		{
			if (error || callback.Result != Result.OK)
			{
				if (this.OnLobbyCreated != null)
				{
					this.OnLobbyCreated(false);
				}
				return;
			}
			if (this.CurrentLobby != 0UL)
			{
				this.Leave();
			}
			this.Owner = this.client.SteamId;
			this.CurrentLobby = callback.SteamIDLobby;
			this.CurrentLobbyData = new Lobby.LobbyData(this.client, this.CurrentLobby);
			this.Name = this.client.Username + "'s Lobby";
			this.CurrentLobbyData.SetData("appid", this.client.AppId.ToString());
			this.LobbyType = this.createdLobbyType;
			this.CurrentLobbyData.SetData("lobbytype", this.LobbyType.ToString());
			this.Joinable = true;
			if (this.OnLobbyCreated != null)
			{
				this.OnLobbyCreated(true);
			}
		}

		// Token: 0x06000ABD RID: 2749 RVA: 0x00035B4F File Offset: 0x00033D4F
		public void SetMemberData(string key, string value)
		{
			if (this.CurrentLobby == 0UL)
			{
				return;
			}
			this.client.native.matchmaking.SetLobbyMemberData(this.CurrentLobby, key, value);
		}

		// Token: 0x06000ABE RID: 2750 RVA: 0x00035B7C File Offset: 0x00033D7C
		public string GetMemberData(ulong steamID, string key)
		{
			if (this.CurrentLobby == 0UL)
			{
				return "ERROR: NOT IN ANY LOBBY";
			}
			return this.client.native.matchmaking.GetLobbyMemberData(this.CurrentLobby, steamID, key);
		}

		// Token: 0x06000ABF RID: 2751 RVA: 0x00035BB4 File Offset: 0x00033DB4
		internal void OnLobbyDataUpdatedAPI(LobbyDataUpdate_t callback)
		{
			if (callback.SteamIDLobby != this.CurrentLobby)
			{
				return;
			}
			if (callback.SteamIDLobby == this.CurrentLobby)
			{
				this.UpdateLobbyData();
			}
			if (this.UserIsInCurrentLobby(callback.SteamIDMember) && this.OnLobbyMemberDataUpdated != null)
			{
				this.OnLobbyMemberDataUpdated(callback.SteamIDMember);
			}
		}

		// Token: 0x06000AC0 RID: 2752 RVA: 0x00035C0C File Offset: 0x00033E0C
		internal void UpdateLobbyData()
		{
			int lobbyDataCount = this.client.native.matchmaking.GetLobbyDataCount(this.CurrentLobby);
			this.CurrentLobbyData = new Lobby.LobbyData(this.client, this.CurrentLobby);
			for (int i = 0; i < lobbyDataCount; i++)
			{
				string k;
				string v;
				if (this.client.native.matchmaking.GetLobbyDataByIndex(this.CurrentLobby, i, out k, out v))
				{
					this.CurrentLobbyData.SetDataRaw(k, v);
				}
			}
			if (this.OnLobbyDataUpdated != null)
			{
				this.OnLobbyDataUpdated();
			}
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x06000AC1 RID: 2753 RVA: 0x00035CA4 File Offset: 0x00033EA4
		// (set) Token: 0x06000AC2 RID: 2754 RVA: 0x00035D0C File Offset: 0x00033F0C
		public Lobby.Type LobbyType
		{
			get
			{
				if (!this.IsValid)
				{
					return Lobby.Type.Error;
				}
				string data = this.CurrentLobbyData.GetData("lobbytype");
				if (data == "Private")
				{
					return Lobby.Type.Private;
				}
				if (data == "FriendsOnly")
				{
					return Lobby.Type.FriendsOnly;
				}
				if (data == "Invisible")
				{
					return Lobby.Type.Invisible;
				}
				if (!(data == "Public"))
				{
					return Lobby.Type.Error;
				}
				return Lobby.Type.Public;
			}
			set
			{
				if (!this.IsValid)
				{
					return;
				}
				if (this.client.native.matchmaking.SetLobbyType(this.CurrentLobby, (LobbyType)value))
				{
					this.CurrentLobbyData.SetData("lobbytype", value.ToString());
				}
			}
		}

		// Token: 0x06000AC3 RID: 2755 RVA: 0x00035D64 File Offset: 0x00033F64
		private unsafe void OnLobbyChatMessageRecievedAPI(LobbyChatMsg_t callback)
		{
			if (callback.SteamIDLobby != this.CurrentLobby)
			{
				return;
			}
			CSteamID value = 1UL;
			byte[] array;
			byte* value2;
			if ((array = Lobby.chatMessageData) == null || array.Length == 0)
			{
				value2 = null;
			}
			else
			{
				value2 = &array[0];
			}
			ChatEntryType chatEntryType;
			int lobbyChatEntry = this.client.native.matchmaking.GetLobbyChatEntry(this.CurrentLobby, (int)callback.ChatID, out value, (IntPtr)((void*)value2), Lobby.chatMessageData.Length, out chatEntryType);
			array = null;
			Action<ulong, byte[], int> onChatMessageRecieved = this.OnChatMessageRecieved;
			if (onChatMessageRecieved != null)
			{
				onChatMessageRecieved(value, Lobby.chatMessageData, lobbyChatEntry);
			}
			if (lobbyChatEntry > 0)
			{
				Action<ulong, string> onChatStringRecieved = this.OnChatStringRecieved;
				if (onChatStringRecieved == null)
				{
					return;
				}
				onChatStringRecieved(value, Encoding.UTF8.GetString(Lobby.chatMessageData, 0, lobbyChatEntry));
			}
		}

		// Token: 0x06000AC4 RID: 2756 RVA: 0x00035E2C File Offset: 0x0003402C
		public unsafe bool SendChatMessage(string message)
		{
			byte[] bytes = Encoding.UTF8.GetBytes(message);
			byte[] array;
			byte* value;
			if ((array = bytes) == null || array.Length == 0)
			{
				value = null;
			}
			else
			{
				value = &array[0];
			}
			return this.client.native.matchmaking.SendLobbyChatMsg(this.CurrentLobby, (IntPtr)((void*)value), bytes.Length);
		}

		// Token: 0x06000AC5 RID: 2757 RVA: 0x00035E88 File Offset: 0x00034088
		internal void OnLobbyStateUpdatedAPI(LobbyChatUpdate_t callback)
		{
			if (callback.SteamIDLobby != this.CurrentLobby)
			{
				return;
			}
			Lobby.MemberStateChange gfChatMemberStateChange = (Lobby.MemberStateChange)callback.GfChatMemberStateChange;
			ulong steamIDMakingChange = callback.SteamIDMakingChange;
			ulong steamIDUserChanged = callback.SteamIDUserChanged;
			Action<Lobby.MemberStateChange, ulong, ulong> onLobbyStateChanged = this.OnLobbyStateChanged;
			if (onLobbyStateChanged == null)
			{
				return;
			}
			onLobbyStateChanged(gfChatMemberStateChange, steamIDMakingChange, steamIDUserChanged);
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x06000AC6 RID: 2758 RVA: 0x00035ECC File Offset: 0x000340CC
		// (set) Token: 0x06000AC7 RID: 2759 RVA: 0x00035EEC File Offset: 0x000340EC
		public string Name
		{
			get
			{
				if (!this.IsValid)
				{
					return "";
				}
				return this.CurrentLobbyData.GetData("name");
			}
			set
			{
				if (!this.IsValid)
				{
					return;
				}
				this.CurrentLobbyData.SetData("name", value);
			}
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x06000AC8 RID: 2760 RVA: 0x00035F09 File Offset: 0x00034109
		public bool IsOwner
		{
			get
			{
				return this.Owner == this.client.SteamId;
			}
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x06000AC9 RID: 2761 RVA: 0x00035F1E File Offset: 0x0003411E
		// (set) Token: 0x06000ACA RID: 2762 RVA: 0x00035F4B File Offset: 0x0003414B
		public ulong Owner
		{
			get
			{
				if (this.IsValid)
				{
					return this.client.native.matchmaking.GetLobbyOwner(this.CurrentLobby);
				}
				return 0UL;
			}
			set
			{
				if (this.Owner == value)
				{
					return;
				}
				this.client.native.matchmaking.SetLobbyOwner(this.CurrentLobby, value);
			}
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x06000ACB RID: 2763 RVA: 0x00035F80 File Offset: 0x00034180
		// (set) Token: 0x06000ACC RID: 2764 RVA: 0x00035FCC File Offset: 0x000341CC
		public bool Joinable
		{
			get
			{
				if (!this.IsValid)
				{
					return false;
				}
				string data = this.CurrentLobbyData.GetData("joinable");
				return data == "true" || (!(data == "false") && false);
			}
			set
			{
				if (!this.IsValid)
				{
					return;
				}
				if (this.client.native.matchmaking.SetLobbyJoinable(this.CurrentLobby, value))
				{
					this.CurrentLobbyData.SetData("joinable", value.ToString());
				}
			}
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x06000ACD RID: 2765 RVA: 0x0003601D File Offset: 0x0003421D
		// (set) Token: 0x06000ACE RID: 2766 RVA: 0x00036049 File Offset: 0x00034249
		public int MaxMembers
		{
			get
			{
				if (!this.IsValid)
				{
					return 0;
				}
				return this.client.native.matchmaking.GetLobbyMemberLimit(this.CurrentLobby);
			}
			set
			{
				if (!this.IsValid)
				{
					return;
				}
				this.client.native.matchmaking.SetLobbyMemberLimit(this.CurrentLobby, value);
			}
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x06000ACF RID: 2767 RVA: 0x00036076 File Offset: 0x00034276
		public int NumMembers
		{
			get
			{
				return this.client.native.matchmaking.GetNumLobbyMembers(this.CurrentLobby);
			}
		}

		// Token: 0x06000AD0 RID: 2768 RVA: 0x00036098 File Offset: 0x00034298
		public void Leave(ulong lobbyToLeave)
		{
			bool flag = lobbyToLeave > 0UL;
			if (flag)
			{
				this.client.native.matchmaking.LeaveLobby(lobbyToLeave);
			}
			if (lobbyToLeave == this.CurrentLobby)
			{
				this.CurrentLobby = 0UL;
				this.CurrentLobbyData = null;
			}
			if (flag && this.OnLobbyLeave != null)
			{
				this.OnLobbyLeave(lobbyToLeave);
			}
		}

		// Token: 0x06000AD1 RID: 2769 RVA: 0x000360F6 File Offset: 0x000342F6
		public void Leave()
		{
			this.Leave(this.CurrentLobby);
		}

		// Token: 0x06000AD2 RID: 2770 RVA: 0x00036104 File Offset: 0x00034304
		public void Dispose()
		{
			this.client = null;
		}

		// Token: 0x06000AD3 RID: 2771 RVA: 0x00036110 File Offset: 0x00034310
		public ulong[] GetMemberIDs()
		{
			ulong[] array = new ulong[this.NumMembers];
			for (int i = 0; i < this.NumMembers; i++)
			{
				array[i] = this.client.native.matchmaking.GetLobbyMemberByIndex(this.CurrentLobby, i);
			}
			return array;
		}

		// Token: 0x06000AD4 RID: 2772 RVA: 0x00036160 File Offset: 0x00034360
		public bool UserIsInCurrentLobby(ulong steamID)
		{
			if (this.CurrentLobby == 0UL)
			{
				return false;
			}
			ulong[] memberIDs = this.GetMemberIDs();
			for (int i = 0; i < memberIDs.Length; i++)
			{
				if (memberIDs[i] == steamID)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000AD5 RID: 2773 RVA: 0x00036195 File Offset: 0x00034395
		public bool InviteUserToLobby(ulong friendID)
		{
			return this.client.native.matchmaking.InviteUserToLobby(this.CurrentLobby, friendID);
		}

		// Token: 0x06000AD6 RID: 2774 RVA: 0x000361BD File Offset: 0x000343BD
		internal void OnUserInvitedToLobbyAPI(LobbyInvite_t callback)
		{
			if (callback.GameID != (ulong)this.client.AppId)
			{
				return;
			}
			if (this.OnUserInvitedToLobby != null)
			{
				this.OnUserInvitedToLobby(callback.SteamIDLobby, callback.SteamIDUser);
			}
		}

		// Token: 0x06000AD7 RID: 2775 RVA: 0x000361F3 File Offset: 0x000343F3
		public void OpenFriendInviteOverlay()
		{
			this.client.native.friends.ActivateGameOverlayInviteDialog(this.CurrentLobby);
		}

		// Token: 0x06000AD8 RID: 2776 RVA: 0x00036215 File Offset: 0x00034415
		internal void OnLobbyJoinRequestedAPI(GameLobbyJoinRequested_t callback)
		{
			if (this.OnLobbyJoinRequested != null)
			{
				this.OnLobbyJoinRequested(callback.SteamIDLobby);
			}
		}

		// Token: 0x06000AD9 RID: 2777 RVA: 0x00036230 File Offset: 0x00034430
		internal void OnLobbyMemberPersonaChangeAPI(PersonaStateChange_t callback)
		{
			if (!this.UserIsInCurrentLobby(callback.SteamID))
			{
				return;
			}
			if (this.OnLobbyMemberDataUpdated != null)
			{
				this.OnLobbyMemberDataUpdated(callback.SteamID);
			}
		}

		// Token: 0x06000ADA RID: 2778 RVA: 0x0003625A File Offset: 0x0003445A
		internal void OnLobbyKickedAPI(LobbyKicked_t callback)
		{
			if (this.OnLobbyKicked != null)
			{
				this.OnLobbyKicked(callback.KickedDueToDisconnect > 0, callback.SteamIDLobby, callback.SteamIDAdmin);
			}
		}

		// Token: 0x06000ADB RID: 2779 RVA: 0x00036284 File Offset: 0x00034484
		public bool SetGameServer(IPAddress ip, int port, ulong serverSteamId = 0UL)
		{
			if (!this.IsValid || !this.IsOwner)
			{
				return false;
			}
			long num = IPAddress.NetworkToHostOrder(ip.Address);
			this.client.native.matchmaking.SetLobbyGameServer(this.CurrentLobby, (uint)num, (ushort)port, serverSteamId);
			return true;
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x06000ADC RID: 2780 RVA: 0x000362DC File Offset: 0x000344DC
		public IPAddress GameServerIp
		{
			get
			{
				uint num;
				ushort num2;
				CSteamID csteamID;
				if (!this.client.native.matchmaking.GetLobbyGameServer(this.CurrentLobby, out num, out num2, out csteamID) || num == 0U)
				{
					return null;
				}
				return new IPAddress(IPAddress.HostToNetworkOrder((long)((ulong)num)));
			}
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x06000ADD RID: 2781 RVA: 0x00036324 File Offset: 0x00034524
		public int GameServerPort
		{
			get
			{
				uint num;
				ushort result;
				CSteamID csteamID;
				if (!this.client.native.matchmaking.GetLobbyGameServer(this.CurrentLobby, out num, out result, out csteamID))
				{
					return 0;
				}
				return (int)result;
			}
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x06000ADE RID: 2782 RVA: 0x0003635C File Offset: 0x0003455C
		public ulong GameServerSteamId
		{
			get
			{
				uint num;
				ushort num2;
				CSteamID value;
				if (!this.client.native.matchmaking.GetLobbyGameServer(this.CurrentLobby, out num, out num2, out value))
				{
					return 0UL;
				}
				return value;
			}
		}

		// Token: 0x04000801 RID: 2049
		internal Client client;

		// Token: 0x04000804 RID: 2052
		public Action<bool> OnLobbyJoined;

		// Token: 0x04000805 RID: 2053
		internal Lobby.Type createdLobbyType;

		// Token: 0x04000806 RID: 2054
		public Action<bool> OnLobbyCreated;

		// Token: 0x04000807 RID: 2055
		public Action OnLobbyDataUpdated;

		// Token: 0x04000808 RID: 2056
		public Action<ulong> OnLobbyMemberDataUpdated;

		// Token: 0x04000809 RID: 2057
		private static byte[] chatMessageData = new byte[4096];

		// Token: 0x0400080A RID: 2058
		public Action<ulong, byte[], int> OnChatMessageRecieved;

		// Token: 0x0400080B RID: 2059
		public Action<ulong, string> OnChatStringRecieved;

		// Token: 0x0400080C RID: 2060
		public Action<Lobby.MemberStateChange, ulong, ulong> OnLobbyStateChanged;

		// Token: 0x0400080D RID: 2061
		public Action<ulong> OnLobbyLeave;

		// Token: 0x0400080E RID: 2062
		public Action<ulong, ulong> OnUserInvitedToLobby;

		// Token: 0x0400080F RID: 2063
		public Action<ulong> OnLobbyJoinRequested;

		// Token: 0x04000810 RID: 2064
		public Action<bool, ulong, ulong> OnLobbyKicked;

		// Token: 0x0200026D RID: 621
		public enum Type
		{
			// Token: 0x04000BD1 RID: 3025
			Private,
			// Token: 0x04000BD2 RID: 3026
			FriendsOnly,
			// Token: 0x04000BD3 RID: 3027
			Public,
			// Token: 0x04000BD4 RID: 3028
			Invisible,
			// Token: 0x04000BD5 RID: 3029
			Error
		}

		// Token: 0x0200026E RID: 622
		public enum MemberStateChange
		{
			// Token: 0x04000BD7 RID: 3031
			Entered = 1,
			// Token: 0x04000BD8 RID: 3032
			Left,
			// Token: 0x04000BD9 RID: 3033
			Disconnected = 4,
			// Token: 0x04000BDA RID: 3034
			Kicked = 8,
			// Token: 0x04000BDB RID: 3035
			Banned = 16
		}

		// Token: 0x0200026F RID: 623
		public class LobbyData
		{
			// Token: 0x06001DD7 RID: 7639 RVA: 0x0006460E File Offset: 0x0006280E
			public LobbyData(Client c, ulong l)
			{
				this.client = c;
				this.lobby = l;
				this.data = new Dictionary<string, string>();
			}

			// Token: 0x06001DD8 RID: 7640 RVA: 0x0006462F File Offset: 0x0006282F
			public string GetData(string k)
			{
				if (this.data.ContainsKey(k))
				{
					return this.data[k];
				}
				return "ERROR: key not found";
			}

			// Token: 0x06001DD9 RID: 7641 RVA: 0x00064654 File Offset: 0x00062854
			public Dictionary<string, string> GetAllData()
			{
				Dictionary<string, string> dictionary = new Dictionary<string, string>();
				foreach (KeyValuePair<string, string> keyValuePair in this.data)
				{
					dictionary.Add(keyValuePair.Key, keyValuePair.Value);
				}
				return dictionary;
			}

			// Token: 0x06001DDA RID: 7642 RVA: 0x000646BC File Offset: 0x000628BC
			public bool SetData(string k, string v)
			{
				if (this.data.ContainsKey(k))
				{
					if (this.data[k] == v)
					{
						return true;
					}
					if (this.client.native.matchmaking.SetLobbyData(this.lobby, k, v))
					{
						this.data[k] = v;
						return true;
					}
				}
				else if (this.client.native.matchmaking.SetLobbyData(this.lobby, k, v))
				{
					this.data.Add(k, v);
					return true;
				}
				return false;
			}

			// Token: 0x06001DDB RID: 7643 RVA: 0x00064754 File Offset: 0x00062954
			internal void SetDataRaw(string k, string v)
			{
				this.data[k] = v;
			}

			// Token: 0x06001DDC RID: 7644 RVA: 0x00064764 File Offset: 0x00062964
			public bool RemoveData(string k)
			{
				if (this.data.ContainsKey(k) && this.client.native.matchmaking.DeleteLobbyData(this.lobby, k))
				{
					this.data.Remove(k);
					return true;
				}
				return false;
			}

			// Token: 0x04000BDC RID: 3036
			internal Client client;

			// Token: 0x04000BDD RID: 3037
			internal ulong lobby;

			// Token: 0x04000BDE RID: 3038
			internal Dictionary<string, string> data;
		}
	}
}
