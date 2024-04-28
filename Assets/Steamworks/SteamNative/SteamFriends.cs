using System;
using System.Runtime.InteropServices;
using Facepunch.Steamworks;

namespace SteamNative
{
	// Token: 0x0200005D RID: 93
	internal class SteamFriends : IDisposable
	{
		// Token: 0x06000099 RID: 153 RVA: 0x000033BC File Offset: 0x000015BC
		internal SteamFriends(BaseSteamworks steamworks, IntPtr pointer)
		{
			this.steamworks = steamworks;
			if (Platform.IsWindows64)
			{
				this.platform = new Platform.Win64(pointer);
				return;
			}
			if (Platform.IsWindows32)
			{
				this.platform = new Platform.Win32(pointer);
				return;
			}
			if (Platform.IsLinux32)
			{
				this.platform = new Platform.Linux32(pointer);
				return;
			}
			if (Platform.IsLinux64)
			{
				this.platform = new Platform.Linux64(pointer);
				return;
			}
			if (Platform.IsOsx)
			{
				this.platform = new Platform.Mac(pointer);
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x0600009A RID: 154 RVA: 0x00003439 File Offset: 0x00001639
		public bool IsValid
		{
			get
			{
				return this.platform != null && this.platform.IsValid;
			}
		}

		// Token: 0x0600009B RID: 155 RVA: 0x00003450 File Offset: 0x00001650
		public virtual void Dispose()
		{
			if (this.platform != null)
			{
				this.platform.Dispose();
				this.platform = null;
			}
		}

		// Token: 0x0600009C RID: 156 RVA: 0x0000346C File Offset: 0x0000166C
		public void ActivateGameOverlay(string pchDialog)
		{
			this.platform.ISteamFriends_ActivateGameOverlay(pchDialog);
		}

		// Token: 0x0600009D RID: 157 RVA: 0x0000347A File Offset: 0x0000167A
		public void ActivateGameOverlayInviteDialog(CSteamID steamIDLobby)
		{
			this.platform.ISteamFriends_ActivateGameOverlayInviteDialog(steamIDLobby.Value);
		}

		// Token: 0x0600009E RID: 158 RVA: 0x0000348D File Offset: 0x0000168D
		public void ActivateGameOverlayToStore(AppId_t nAppID, OverlayToStoreFlag eFlag)
		{
			this.platform.ISteamFriends_ActivateGameOverlayToStore(nAppID.Value, eFlag);
		}

		// Token: 0x0600009F RID: 159 RVA: 0x000034A1 File Offset: 0x000016A1
		public void ActivateGameOverlayToUser(string pchDialog, CSteamID steamID)
		{
			this.platform.ISteamFriends_ActivateGameOverlayToUser(pchDialog, steamID.Value);
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x000034B5 File Offset: 0x000016B5
		public void ActivateGameOverlayToWebPage(string pchURL)
		{
			this.platform.ISteamFriends_ActivateGameOverlayToWebPage(pchURL);
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x000034C3 File Offset: 0x000016C3
		public void ClearRichPresence()
		{
			this.platform.ISteamFriends_ClearRichPresence();
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x000034D0 File Offset: 0x000016D0
		public bool CloseClanChatWindowInSteam(CSteamID steamIDClanChat)
		{
			return this.platform.ISteamFriends_CloseClanChatWindowInSteam(steamIDClanChat.Value);
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x000034E3 File Offset: 0x000016E3
		public SteamAPICall_t DownloadClanActivityCounts(IntPtr psteamIDClans, int cClansToRequest)
		{
			return this.platform.ISteamFriends_DownloadClanActivityCounts(psteamIDClans, cClansToRequest);
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x000034F4 File Offset: 0x000016F4
		public CallbackHandle EnumerateFollowingList(uint unStartIndex, Action<FriendsEnumerateFollowingList_t, bool> CallbackFunction = null)
		{
			SteamAPICall_t steamAPICall_t = 0UL;
			steamAPICall_t = this.platform.ISteamFriends_EnumerateFollowingList(unStartIndex);
			if (CallbackFunction == null)
			{
				return null;
			}
			if (steamAPICall_t == 0UL)
			{
				return null;
			}
			return FriendsEnumerateFollowingList_t.CallResult(this.steamworks, steamAPICall_t, CallbackFunction);
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x00003532 File Offset: 0x00001732
		public ulong GetChatMemberByIndex(CSteamID steamIDClan, int iUser)
		{
			return this.platform.ISteamFriends_GetChatMemberByIndex(steamIDClan.Value, iUser);
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x0000354B File Offset: 0x0000174B
		public bool GetClanActivityCounts(CSteamID steamIDClan, out int pnOnline, out int pnInGame, out int pnChatting)
		{
			return this.platform.ISteamFriends_GetClanActivityCounts(steamIDClan.Value, out pnOnline, out pnInGame, out pnChatting);
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x00003562 File Offset: 0x00001762
		public ulong GetClanByIndex(int iClan)
		{
			return this.platform.ISteamFriends_GetClanByIndex(iClan);
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x00003575 File Offset: 0x00001775
		public int GetClanChatMemberCount(CSteamID steamIDClan)
		{
			return this.platform.ISteamFriends_GetClanChatMemberCount(steamIDClan.Value);
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x00003588 File Offset: 0x00001788
		public int GetClanChatMessage(CSteamID steamIDClanChat, int iMessage, IntPtr prgchText, int cchTextMax, out ChatEntryType peChatEntryType, out CSteamID psteamidChatter)
		{
			return this.platform.ISteamFriends_GetClanChatMessage(steamIDClanChat.Value, iMessage, prgchText, cchTextMax, out peChatEntryType, out psteamidChatter.Value);
		}

		// Token: 0x060000AA RID: 170 RVA: 0x000035A8 File Offset: 0x000017A8
		public int GetClanCount()
		{
			return this.platform.ISteamFriends_GetClanCount();
		}

		// Token: 0x060000AB RID: 171 RVA: 0x000035B5 File Offset: 0x000017B5
		public string GetClanName(CSteamID steamIDClan)
		{
			return Marshal.PtrToStringAnsi(this.platform.ISteamFriends_GetClanName(steamIDClan.Value));
		}

		// Token: 0x060000AC RID: 172 RVA: 0x000035CD File Offset: 0x000017CD
		public ulong GetClanOfficerByIndex(CSteamID steamIDClan, int iOfficer)
		{
			return this.platform.ISteamFriends_GetClanOfficerByIndex(steamIDClan.Value, iOfficer);
		}

		// Token: 0x060000AD RID: 173 RVA: 0x000035E6 File Offset: 0x000017E6
		public int GetClanOfficerCount(CSteamID steamIDClan)
		{
			return this.platform.ISteamFriends_GetClanOfficerCount(steamIDClan.Value);
		}

		// Token: 0x060000AE RID: 174 RVA: 0x000035F9 File Offset: 0x000017F9
		public ulong GetClanOwner(CSteamID steamIDClan)
		{
			return this.platform.ISteamFriends_GetClanOwner(steamIDClan.Value);
		}

		// Token: 0x060000AF RID: 175 RVA: 0x00003611 File Offset: 0x00001811
		public string GetClanTag(CSteamID steamIDClan)
		{
			return Marshal.PtrToStringAnsi(this.platform.ISteamFriends_GetClanTag(steamIDClan.Value));
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x00003629 File Offset: 0x00001829
		public ulong GetCoplayFriend(int iCoplayFriend)
		{
			return this.platform.ISteamFriends_GetCoplayFriend(iCoplayFriend);
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x0000363C File Offset: 0x0000183C
		public int GetCoplayFriendCount()
		{
			return this.platform.ISteamFriends_GetCoplayFriendCount();
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x0000364C File Offset: 0x0000184C
		public CallbackHandle GetFollowerCount(CSteamID steamID, Action<FriendsGetFollowerCount_t, bool> CallbackFunction = null)
		{
			SteamAPICall_t steamAPICall_t = 0UL;
			steamAPICall_t = this.platform.ISteamFriends_GetFollowerCount(steamID.Value);
			if (CallbackFunction == null)
			{
				return null;
			}
			if (steamAPICall_t == 0UL)
			{
				return null;
			}
			return FriendsGetFollowerCount_t.CallResult(this.steamworks, steamAPICall_t, CallbackFunction);
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x0000368F File Offset: 0x0000188F
		public ulong GetFriendByIndex(int iFriend, int iFriendFlags)
		{
			return this.platform.ISteamFriends_GetFriendByIndex(iFriend, iFriendFlags);
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x000036A3 File Offset: 0x000018A3
		public AppId_t GetFriendCoplayGame(CSteamID steamIDFriend)
		{
			return this.platform.ISteamFriends_GetFriendCoplayGame(steamIDFriend.Value);
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x000036B6 File Offset: 0x000018B6
		public int GetFriendCoplayTime(CSteamID steamIDFriend)
		{
			return this.platform.ISteamFriends_GetFriendCoplayTime(steamIDFriend.Value);
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x000036C9 File Offset: 0x000018C9
		public int GetFriendCount(int iFriendFlags)
		{
			return this.platform.ISteamFriends_GetFriendCount(iFriendFlags);
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x000036D7 File Offset: 0x000018D7
		public int GetFriendCountFromSource(CSteamID steamIDSource)
		{
			return this.platform.ISteamFriends_GetFriendCountFromSource(steamIDSource.Value);
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x000036EA File Offset: 0x000018EA
		public ulong GetFriendFromSourceByIndex(CSteamID steamIDSource, int iFriend)
		{
			return this.platform.ISteamFriends_GetFriendFromSourceByIndex(steamIDSource.Value, iFriend);
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x00003703 File Offset: 0x00001903
		public bool GetFriendGamePlayed(CSteamID steamIDFriend, ref FriendGameInfo_t pFriendGameInfo)
		{
			return this.platform.ISteamFriends_GetFriendGamePlayed(steamIDFriend.Value, ref pFriendGameInfo);
		}

		// Token: 0x060000BA RID: 186 RVA: 0x00003717 File Offset: 0x00001917
		public int GetFriendMessage(CSteamID steamIDFriend, int iMessageID, IntPtr pvData, int cubData, out ChatEntryType peChatEntryType)
		{
			return this.platform.ISteamFriends_GetFriendMessage(steamIDFriend.Value, iMessageID, pvData, cubData, out peChatEntryType);
		}

		// Token: 0x060000BB RID: 187 RVA: 0x00003730 File Offset: 0x00001930
		public string GetFriendPersonaName(CSteamID steamIDFriend)
		{
			return Marshal.PtrToStringAnsi(this.platform.ISteamFriends_GetFriendPersonaName(steamIDFriend.Value));
		}

		// Token: 0x060000BC RID: 188 RVA: 0x00003748 File Offset: 0x00001948
		public string GetFriendPersonaNameHistory(CSteamID steamIDFriend, int iPersonaName)
		{
			return Marshal.PtrToStringAnsi(this.platform.ISteamFriends_GetFriendPersonaNameHistory(steamIDFriend.Value, iPersonaName));
		}

		// Token: 0x060000BD RID: 189 RVA: 0x00003761 File Offset: 0x00001961
		public PersonaState GetFriendPersonaState(CSteamID steamIDFriend)
		{
			return this.platform.ISteamFriends_GetFriendPersonaState(steamIDFriend.Value);
		}

		// Token: 0x060000BE RID: 190 RVA: 0x00003774 File Offset: 0x00001974
		public FriendRelationship GetFriendRelationship(CSteamID steamIDFriend)
		{
			return this.platform.ISteamFriends_GetFriendRelationship(steamIDFriend.Value);
		}

		// Token: 0x060000BF RID: 191 RVA: 0x00003787 File Offset: 0x00001987
		public string GetFriendRichPresence(CSteamID steamIDFriend, string pchKey)
		{
			return Marshal.PtrToStringAnsi(this.platform.ISteamFriends_GetFriendRichPresence(steamIDFriend.Value, pchKey));
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x000037A0 File Offset: 0x000019A0
		public string GetFriendRichPresenceKeyByIndex(CSteamID steamIDFriend, int iKey)
		{
			return Marshal.PtrToStringAnsi(this.platform.ISteamFriends_GetFriendRichPresenceKeyByIndex(steamIDFriend.Value, iKey));
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x000037B9 File Offset: 0x000019B9
		public int GetFriendRichPresenceKeyCount(CSteamID steamIDFriend)
		{
			return this.platform.ISteamFriends_GetFriendRichPresenceKeyCount(steamIDFriend.Value);
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x000037CC File Offset: 0x000019CC
		public int GetFriendsGroupCount()
		{
			return this.platform.ISteamFriends_GetFriendsGroupCount();
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x000037D9 File Offset: 0x000019D9
		public FriendsGroupID_t GetFriendsGroupIDByIndex(int iFG)
		{
			return this.platform.ISteamFriends_GetFriendsGroupIDByIndex(iFG);
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x000037E7 File Offset: 0x000019E7
		public int GetFriendsGroupMembersCount(FriendsGroupID_t friendsGroupID)
		{
			return this.platform.ISteamFriends_GetFriendsGroupMembersCount(friendsGroupID.Value);
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x000037FA File Offset: 0x000019FA
		public void GetFriendsGroupMembersList(FriendsGroupID_t friendsGroupID, IntPtr pOutSteamIDMembers, int nMembersCount)
		{
			this.platform.ISteamFriends_GetFriendsGroupMembersList(friendsGroupID.Value, pOutSteamIDMembers, nMembersCount);
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x0000380F File Offset: 0x00001A0F
		public string GetFriendsGroupName(FriendsGroupID_t friendsGroupID)
		{
			return Marshal.PtrToStringAnsi(this.platform.ISteamFriends_GetFriendsGroupName(friendsGroupID.Value));
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x00003827 File Offset: 0x00001A27
		public int GetFriendSteamLevel(CSteamID steamIDFriend)
		{
			return this.platform.ISteamFriends_GetFriendSteamLevel(steamIDFriend.Value);
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x0000383A File Offset: 0x00001A3A
		public int GetLargeFriendAvatar(CSteamID steamIDFriend)
		{
			return this.platform.ISteamFriends_GetLargeFriendAvatar(steamIDFriend.Value);
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x0000384D File Offset: 0x00001A4D
		public int GetMediumFriendAvatar(CSteamID steamIDFriend)
		{
			return this.platform.ISteamFriends_GetMediumFriendAvatar(steamIDFriend.Value);
		}

		// Token: 0x060000CA RID: 202 RVA: 0x00003860 File Offset: 0x00001A60
		public string GetPersonaName()
		{
			return Marshal.PtrToStringAnsi(this.platform.ISteamFriends_GetPersonaName());
		}

		// Token: 0x060000CB RID: 203 RVA: 0x00003872 File Offset: 0x00001A72
		public PersonaState GetPersonaState()
		{
			return this.platform.ISteamFriends_GetPersonaState();
		}

		// Token: 0x060000CC RID: 204 RVA: 0x0000387F File Offset: 0x00001A7F
		public string GetPlayerNickname(CSteamID steamIDPlayer)
		{
			return Marshal.PtrToStringAnsi(this.platform.ISteamFriends_GetPlayerNickname(steamIDPlayer.Value));
		}

		// Token: 0x060000CD RID: 205 RVA: 0x00003897 File Offset: 0x00001A97
		public int GetSmallFriendAvatar(CSteamID steamIDFriend)
		{
			return this.platform.ISteamFriends_GetSmallFriendAvatar(steamIDFriend.Value);
		}

		// Token: 0x060000CE RID: 206 RVA: 0x000038AA File Offset: 0x00001AAA
		public uint GetUserRestrictions()
		{
			return this.platform.ISteamFriends_GetUserRestrictions();
		}

		// Token: 0x060000CF RID: 207 RVA: 0x000038B7 File Offset: 0x00001AB7
		public bool HasFriend(CSteamID steamIDFriend, int iFriendFlags)
		{
			return this.platform.ISteamFriends_HasFriend(steamIDFriend.Value, iFriendFlags);
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x000038CB File Offset: 0x00001ACB
		public bool InviteUserToGame(CSteamID steamIDFriend, string pchConnectString)
		{
			return this.platform.ISteamFriends_InviteUserToGame(steamIDFriend.Value, pchConnectString);
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x000038DF File Offset: 0x00001ADF
		public bool IsClanChatAdmin(CSteamID steamIDClanChat, CSteamID steamIDUser)
		{
			return this.platform.ISteamFriends_IsClanChatAdmin(steamIDClanChat.Value, steamIDUser.Value);
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x000038F8 File Offset: 0x00001AF8
		public bool IsClanChatWindowOpenInSteam(CSteamID steamIDClanChat)
		{
			return this.platform.ISteamFriends_IsClanChatWindowOpenInSteam(steamIDClanChat.Value);
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x0000390B File Offset: 0x00001B0B
		public bool IsClanOfficialGameGroup(CSteamID steamIDClan)
		{
			return this.platform.ISteamFriends_IsClanOfficialGameGroup(steamIDClan.Value);
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x0000391E File Offset: 0x00001B1E
		public bool IsClanPublic(CSteamID steamIDClan)
		{
			return this.platform.ISteamFriends_IsClanPublic(steamIDClan.Value);
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x00003934 File Offset: 0x00001B34
		public CallbackHandle IsFollowing(CSteamID steamID, Action<FriendsIsFollowing_t, bool> CallbackFunction = null)
		{
			SteamAPICall_t steamAPICall_t = 0UL;
			steamAPICall_t = this.platform.ISteamFriends_IsFollowing(steamID.Value);
			if (CallbackFunction == null)
			{
				return null;
			}
			if (steamAPICall_t == 0UL)
			{
				return null;
			}
			return FriendsIsFollowing_t.CallResult(this.steamworks, steamAPICall_t, CallbackFunction);
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x00003977 File Offset: 0x00001B77
		public bool IsUserInSource(CSteamID steamIDUser, CSteamID steamIDSource)
		{
			return this.platform.ISteamFriends_IsUserInSource(steamIDUser.Value, steamIDSource.Value);
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x00003990 File Offset: 0x00001B90
		public CallbackHandle JoinClanChatRoom(CSteamID steamIDClan, Action<JoinClanChatRoomCompletionResult_t, bool> CallbackFunction = null)
		{
			SteamAPICall_t steamAPICall_t = 0UL;
			steamAPICall_t = this.platform.ISteamFriends_JoinClanChatRoom(steamIDClan.Value);
			if (CallbackFunction == null)
			{
				return null;
			}
			if (steamAPICall_t == 0UL)
			{
				return null;
			}
			return JoinClanChatRoomCompletionResult_t.CallResult(this.steamworks, steamAPICall_t, CallbackFunction);
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x000039D3 File Offset: 0x00001BD3
		public bool LeaveClanChatRoom(CSteamID steamIDClan)
		{
			return this.platform.ISteamFriends_LeaveClanChatRoom(steamIDClan.Value);
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x000039E6 File Offset: 0x00001BE6
		public bool OpenClanChatWindowInSteam(CSteamID steamIDClanChat)
		{
			return this.platform.ISteamFriends_OpenClanChatWindowInSteam(steamIDClanChat.Value);
		}

		// Token: 0x060000DA RID: 218 RVA: 0x000039F9 File Offset: 0x00001BF9
		public bool ReplyToFriendMessage(CSteamID steamIDFriend, string pchMsgToSend)
		{
			return this.platform.ISteamFriends_ReplyToFriendMessage(steamIDFriend.Value, pchMsgToSend);
		}

		// Token: 0x060000DB RID: 219 RVA: 0x00003A10 File Offset: 0x00001C10
		public CallbackHandle RequestClanOfficerList(CSteamID steamIDClan, Action<ClanOfficerListResponse_t, bool> CallbackFunction = null)
		{
			SteamAPICall_t steamAPICall_t = 0UL;
			steamAPICall_t = this.platform.ISteamFriends_RequestClanOfficerList(steamIDClan.Value);
			if (CallbackFunction == null)
			{
				return null;
			}
			if (steamAPICall_t == 0UL)
			{
				return null;
			}
			return ClanOfficerListResponse_t.CallResult(this.steamworks, steamAPICall_t, CallbackFunction);
		}

		// Token: 0x060000DC RID: 220 RVA: 0x00003A53 File Offset: 0x00001C53
		public void RequestFriendRichPresence(CSteamID steamIDFriend)
		{
			this.platform.ISteamFriends_RequestFriendRichPresence(steamIDFriend.Value);
		}

		// Token: 0x060000DD RID: 221 RVA: 0x00003A66 File Offset: 0x00001C66
		public bool RequestUserInformation(CSteamID steamIDUser, bool bRequireNameOnly)
		{
			return this.platform.ISteamFriends_RequestUserInformation(steamIDUser.Value, bRequireNameOnly);
		}

		// Token: 0x060000DE RID: 222 RVA: 0x00003A7A File Offset: 0x00001C7A
		public bool SendClanChatMessage(CSteamID steamIDClanChat, string pchText)
		{
			return this.platform.ISteamFriends_SendClanChatMessage(steamIDClanChat.Value, pchText);
		}

		// Token: 0x060000DF RID: 223 RVA: 0x00003A8E File Offset: 0x00001C8E
		public void SetInGameVoiceSpeaking(CSteamID steamIDUser, bool bSpeaking)
		{
			this.platform.ISteamFriends_SetInGameVoiceSpeaking(steamIDUser.Value, bSpeaking);
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x00003AA2 File Offset: 0x00001CA2
		public bool SetListenForFriendsMessages(bool bInterceptEnabled)
		{
			return this.platform.ISteamFriends_SetListenForFriendsMessages(bInterceptEnabled);
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x00003AB0 File Offset: 0x00001CB0
		public CallbackHandle SetPersonaName(string pchPersonaName, Action<SetPersonaNameResponse_t, bool> CallbackFunction = null)
		{
			SteamAPICall_t steamAPICall_t = 0UL;
			steamAPICall_t = this.platform.ISteamFriends_SetPersonaName(pchPersonaName);
			if (CallbackFunction == null)
			{
				return null;
			}
			if (steamAPICall_t == 0UL)
			{
				return null;
			}
			return SetPersonaNameResponse_t.CallResult(this.steamworks, steamAPICall_t, CallbackFunction);
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x00003AEE File Offset: 0x00001CEE
		public void SetPlayedWith(CSteamID steamIDUserPlayedWith)
		{
			this.platform.ISteamFriends_SetPlayedWith(steamIDUserPlayedWith.Value);
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x00003B01 File Offset: 0x00001D01
		public bool SetRichPresence(string pchKey, string pchValue)
		{
			return this.platform.ISteamFriends_SetRichPresence(pchKey, pchValue);
		}

		// Token: 0x0400046A RID: 1130
		internal Platform.Interface platform;

		// Token: 0x0400046B RID: 1131
		internal BaseSteamworks steamworks;
	}
}
