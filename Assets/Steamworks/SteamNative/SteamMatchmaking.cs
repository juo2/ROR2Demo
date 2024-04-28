using System;
using System.Runtime.InteropServices;
using System.Text;
using Facepunch.Steamworks;

namespace SteamNative
{
	// Token: 0x02000063 RID: 99
	internal class SteamMatchmaking : IDisposable
	{
		// Token: 0x0600018B RID: 395 RVA: 0x00004E88 File Offset: 0x00003088
		internal SteamMatchmaking(BaseSteamworks steamworks, IntPtr pointer)
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

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x0600018C RID: 396 RVA: 0x00004F05 File Offset: 0x00003105
		public bool IsValid
		{
			get
			{
				return this.platform != null && this.platform.IsValid;
			}
		}

		// Token: 0x0600018D RID: 397 RVA: 0x00004F1C File Offset: 0x0000311C
		public virtual void Dispose()
		{
			if (this.platform != null)
			{
				this.platform.Dispose();
				this.platform = null;
			}
		}

		// Token: 0x0600018E RID: 398 RVA: 0x00004F38 File Offset: 0x00003138
		public int AddFavoriteGame(AppId_t nAppID, uint nIP, ushort nConnPort, ushort nQueryPort, uint unFlags, uint rTime32LastPlayedOnServer)
		{
			return this.platform.ISteamMatchmaking_AddFavoriteGame(nAppID.Value, nIP, nConnPort, nQueryPort, unFlags, rTime32LastPlayedOnServer);
		}

		// Token: 0x0600018F RID: 399 RVA: 0x00004F53 File Offset: 0x00003153
		public void AddRequestLobbyListCompatibleMembersFilter(CSteamID steamIDLobby)
		{
			this.platform.ISteamMatchmaking_AddRequestLobbyListCompatibleMembersFilter(steamIDLobby.Value);
		}

		// Token: 0x06000190 RID: 400 RVA: 0x00004F66 File Offset: 0x00003166
		public void AddRequestLobbyListDistanceFilter(LobbyDistanceFilter eLobbyDistanceFilter)
		{
			this.platform.ISteamMatchmaking_AddRequestLobbyListDistanceFilter(eLobbyDistanceFilter);
		}

		// Token: 0x06000191 RID: 401 RVA: 0x00004F74 File Offset: 0x00003174
		public void AddRequestLobbyListFilterSlotsAvailable(int nSlotsAvailable)
		{
			this.platform.ISteamMatchmaking_AddRequestLobbyListFilterSlotsAvailable(nSlotsAvailable);
		}

		// Token: 0x06000192 RID: 402 RVA: 0x00004F82 File Offset: 0x00003182
		public void AddRequestLobbyListNearValueFilter(string pchKeyToMatch, int nValueToBeCloseTo)
		{
			this.platform.ISteamMatchmaking_AddRequestLobbyListNearValueFilter(pchKeyToMatch, nValueToBeCloseTo);
		}

		// Token: 0x06000193 RID: 403 RVA: 0x00004F91 File Offset: 0x00003191
		public void AddRequestLobbyListNumericalFilter(string pchKeyToMatch, int nValueToMatch, LobbyComparison eComparisonType)
		{
			this.platform.ISteamMatchmaking_AddRequestLobbyListNumericalFilter(pchKeyToMatch, nValueToMatch, eComparisonType);
		}

		// Token: 0x06000194 RID: 404 RVA: 0x00004FA1 File Offset: 0x000031A1
		public void AddRequestLobbyListResultCountFilter(int cMaxResults)
		{
			this.platform.ISteamMatchmaking_AddRequestLobbyListResultCountFilter(cMaxResults);
		}

		// Token: 0x06000195 RID: 405 RVA: 0x00004FAF File Offset: 0x000031AF
		public void AddRequestLobbyListStringFilter(string pchKeyToMatch, string pchValueToMatch, LobbyComparison eComparisonType)
		{
			this.platform.ISteamMatchmaking_AddRequestLobbyListStringFilter(pchKeyToMatch, pchValueToMatch, eComparisonType);
		}

		// Token: 0x06000196 RID: 406 RVA: 0x00004FC0 File Offset: 0x000031C0
		public CallbackHandle CreateLobby(LobbyType eLobbyType, int cMaxMembers, Action<LobbyCreated_t, bool> CallbackFunction = null)
		{
			SteamAPICall_t steamAPICall_t = 0UL;
			steamAPICall_t = this.platform.ISteamMatchmaking_CreateLobby(eLobbyType, cMaxMembers);
			if (CallbackFunction == null)
			{
				return null;
			}
			if (steamAPICall_t == 0UL)
			{
				return null;
			}
			return LobbyCreated_t.CallResult(this.steamworks, steamAPICall_t, CallbackFunction);
		}

		// Token: 0x06000197 RID: 407 RVA: 0x00004FFF File Offset: 0x000031FF
		public bool DeleteLobbyData(CSteamID steamIDLobby, string pchKey)
		{
			return this.platform.ISteamMatchmaking_DeleteLobbyData(steamIDLobby.Value, pchKey);
		}

		// Token: 0x06000198 RID: 408 RVA: 0x00005013 File Offset: 0x00003213
		public bool GetFavoriteGame(int iGame, ref AppId_t pnAppID, out uint pnIP, out ushort pnConnPort, out ushort pnQueryPort, out uint punFlags, out uint pRTime32LastPlayedOnServer)
		{
			return this.platform.ISteamMatchmaking_GetFavoriteGame(iGame, ref pnAppID.Value, out pnIP, out pnConnPort, out pnQueryPort, out punFlags, out pRTime32LastPlayedOnServer);
		}

		// Token: 0x06000199 RID: 409 RVA: 0x00005030 File Offset: 0x00003230
		public int GetFavoriteGameCount()
		{
			return this.platform.ISteamMatchmaking_GetFavoriteGameCount();
		}

		// Token: 0x0600019A RID: 410 RVA: 0x0000503D File Offset: 0x0000323D
		public ulong GetLobbyByIndex(int iLobby)
		{
			return this.platform.ISteamMatchmaking_GetLobbyByIndex(iLobby);
		}

		// Token: 0x0600019B RID: 411 RVA: 0x00005050 File Offset: 0x00003250
		public int GetLobbyChatEntry(CSteamID steamIDLobby, int iChatID, out CSteamID pSteamIDUser, IntPtr pvData, int cubData, out ChatEntryType peChatEntryType)
		{
			return this.platform.ISteamMatchmaking_GetLobbyChatEntry(steamIDLobby.Value, iChatID, out pSteamIDUser.Value, pvData, cubData, out peChatEntryType);
		}

		// Token: 0x0600019C RID: 412 RVA: 0x00005070 File Offset: 0x00003270
		public string GetLobbyData(CSteamID steamIDLobby, string pchKey)
		{
			return Marshal.PtrToStringAnsi(this.platform.ISteamMatchmaking_GetLobbyData(steamIDLobby.Value, pchKey));
		}

		// Token: 0x0600019D RID: 413 RVA: 0x0000508C File Offset: 0x0000328C
		public bool GetLobbyDataByIndex(CSteamID steamIDLobby, int iLobbyData, out string pchKey, out string pchValue)
		{
			pchKey = string.Empty;
			StringBuilder stringBuilder = Helpers.TakeStringBuilder();
			int cchKeyBufferSize = 4096;
			pchValue = string.Empty;
			StringBuilder stringBuilder2 = Helpers.TakeStringBuilder();
			int cchValueBufferSize = 4096;
			bool flag = this.platform.ISteamMatchmaking_GetLobbyDataByIndex(steamIDLobby.Value, iLobbyData, stringBuilder, cchKeyBufferSize, stringBuilder2, cchValueBufferSize);
			if (!flag)
			{
				return flag;
			}
			pchValue = stringBuilder2.ToString();
			if (!flag)
			{
				return flag;
			}
			pchKey = stringBuilder.ToString();
			return flag;
		}

		// Token: 0x0600019E RID: 414 RVA: 0x000050F7 File Offset: 0x000032F7
		public int GetLobbyDataCount(CSteamID steamIDLobby)
		{
			return this.platform.ISteamMatchmaking_GetLobbyDataCount(steamIDLobby.Value);
		}

		// Token: 0x0600019F RID: 415 RVA: 0x0000510A File Offset: 0x0000330A
		public bool GetLobbyGameServer(CSteamID steamIDLobby, out uint punGameServerIP, out ushort punGameServerPort, out CSteamID psteamIDGameServer)
		{
			return this.platform.ISteamMatchmaking_GetLobbyGameServer(steamIDLobby.Value, out punGameServerIP, out punGameServerPort, out psteamIDGameServer.Value);
		}

		// Token: 0x060001A0 RID: 416 RVA: 0x00005126 File Offset: 0x00003326
		public ulong GetLobbyMemberByIndex(CSteamID steamIDLobby, int iMember)
		{
			return this.platform.ISteamMatchmaking_GetLobbyMemberByIndex(steamIDLobby.Value, iMember);
		}

		// Token: 0x060001A1 RID: 417 RVA: 0x0000513F File Offset: 0x0000333F
		public string GetLobbyMemberData(CSteamID steamIDLobby, CSteamID steamIDUser, string pchKey)
		{
			return Marshal.PtrToStringAnsi(this.platform.ISteamMatchmaking_GetLobbyMemberData(steamIDLobby.Value, steamIDUser.Value, pchKey));
		}

		// Token: 0x060001A2 RID: 418 RVA: 0x0000515E File Offset: 0x0000335E
		public int GetLobbyMemberLimit(CSteamID steamIDLobby)
		{
			return this.platform.ISteamMatchmaking_GetLobbyMemberLimit(steamIDLobby.Value);
		}

		// Token: 0x060001A3 RID: 419 RVA: 0x00005171 File Offset: 0x00003371
		public ulong GetLobbyOwner(CSteamID steamIDLobby)
		{
			return this.platform.ISteamMatchmaking_GetLobbyOwner(steamIDLobby.Value);
		}

		// Token: 0x060001A4 RID: 420 RVA: 0x00005189 File Offset: 0x00003389
		public int GetNumLobbyMembers(CSteamID steamIDLobby)
		{
			return this.platform.ISteamMatchmaking_GetNumLobbyMembers(steamIDLobby.Value);
		}

		// Token: 0x060001A5 RID: 421 RVA: 0x0000519C File Offset: 0x0000339C
		public bool InviteUserToLobby(CSteamID steamIDLobby, CSteamID steamIDInvitee)
		{
			return this.platform.ISteamMatchmaking_InviteUserToLobby(steamIDLobby.Value, steamIDInvitee.Value);
		}

		// Token: 0x060001A6 RID: 422 RVA: 0x000051B8 File Offset: 0x000033B8
		public CallbackHandle JoinLobby(CSteamID steamIDLobby, Action<LobbyEnter_t, bool> CallbackFunction = null)
		{
			SteamAPICall_t steamAPICall_t = 0UL;
			steamAPICall_t = this.platform.ISteamMatchmaking_JoinLobby(steamIDLobby.Value);
			if (CallbackFunction == null)
			{
				return null;
			}
			if (steamAPICall_t == 0UL)
			{
				return null;
			}
			return LobbyEnter_t.CallResult(this.steamworks, steamAPICall_t, CallbackFunction);
		}

		// Token: 0x060001A7 RID: 423 RVA: 0x000051FB File Offset: 0x000033FB
		public void LeaveLobby(CSteamID steamIDLobby)
		{
			this.platform.ISteamMatchmaking_LeaveLobby(steamIDLobby.Value);
		}

		// Token: 0x060001A8 RID: 424 RVA: 0x0000520E File Offset: 0x0000340E
		public bool RemoveFavoriteGame(AppId_t nAppID, uint nIP, ushort nConnPort, ushort nQueryPort, uint unFlags)
		{
			return this.platform.ISteamMatchmaking_RemoveFavoriteGame(nAppID.Value, nIP, nConnPort, nQueryPort, unFlags);
		}

		// Token: 0x060001A9 RID: 425 RVA: 0x00005227 File Offset: 0x00003427
		public bool RequestLobbyData(CSteamID steamIDLobby)
		{
			return this.platform.ISteamMatchmaking_RequestLobbyData(steamIDLobby.Value);
		}

		// Token: 0x060001AA RID: 426 RVA: 0x0000523C File Offset: 0x0000343C
		public CallbackHandle RequestLobbyList(Action<LobbyMatchList_t, bool> CallbackFunction = null)
		{
			SteamAPICall_t steamAPICall_t = 0UL;
			steamAPICall_t = this.platform.ISteamMatchmaking_RequestLobbyList();
			if (CallbackFunction == null)
			{
				return null;
			}
			if (steamAPICall_t == 0UL)
			{
				return null;
			}
			return LobbyMatchList_t.CallResult(this.steamworks, steamAPICall_t, CallbackFunction);
		}

		// Token: 0x060001AB RID: 427 RVA: 0x00005279 File Offset: 0x00003479
		public bool SendLobbyChatMsg(CSteamID steamIDLobby, IntPtr pvMsgBody, int cubMsgBody)
		{
			return this.platform.ISteamMatchmaking_SendLobbyChatMsg(steamIDLobby.Value, pvMsgBody, cubMsgBody);
		}

		// Token: 0x060001AC RID: 428 RVA: 0x0000528E File Offset: 0x0000348E
		public bool SetLinkedLobby(CSteamID steamIDLobby, CSteamID steamIDLobbyDependent)
		{
			return this.platform.ISteamMatchmaking_SetLinkedLobby(steamIDLobby.Value, steamIDLobbyDependent.Value);
		}

		// Token: 0x060001AD RID: 429 RVA: 0x000052A7 File Offset: 0x000034A7
		public bool SetLobbyData(CSteamID steamIDLobby, string pchKey, string pchValue)
		{
			return this.platform.ISteamMatchmaking_SetLobbyData(steamIDLobby.Value, pchKey, pchValue);
		}

		// Token: 0x060001AE RID: 430 RVA: 0x000052BC File Offset: 0x000034BC
		public void SetLobbyGameServer(CSteamID steamIDLobby, uint unGameServerIP, ushort unGameServerPort, CSteamID steamIDGameServer)
		{
			this.platform.ISteamMatchmaking_SetLobbyGameServer(steamIDLobby.Value, unGameServerIP, unGameServerPort, steamIDGameServer.Value);
		}

		// Token: 0x060001AF RID: 431 RVA: 0x000052D8 File Offset: 0x000034D8
		public bool SetLobbyJoinable(CSteamID steamIDLobby, bool bLobbyJoinable)
		{
			return this.platform.ISteamMatchmaking_SetLobbyJoinable(steamIDLobby.Value, bLobbyJoinable);
		}

		// Token: 0x060001B0 RID: 432 RVA: 0x000052EC File Offset: 0x000034EC
		public void SetLobbyMemberData(CSteamID steamIDLobby, string pchKey, string pchValue)
		{
			this.platform.ISteamMatchmaking_SetLobbyMemberData(steamIDLobby.Value, pchKey, pchValue);
		}

		// Token: 0x060001B1 RID: 433 RVA: 0x00005301 File Offset: 0x00003501
		public bool SetLobbyMemberLimit(CSteamID steamIDLobby, int cMaxMembers)
		{
			return this.platform.ISteamMatchmaking_SetLobbyMemberLimit(steamIDLobby.Value, cMaxMembers);
		}

		// Token: 0x060001B2 RID: 434 RVA: 0x00005315 File Offset: 0x00003515
		public bool SetLobbyOwner(CSteamID steamIDLobby, CSteamID steamIDNewOwner)
		{
			return this.platform.ISteamMatchmaking_SetLobbyOwner(steamIDLobby.Value, steamIDNewOwner.Value);
		}

		// Token: 0x060001B3 RID: 435 RVA: 0x0000532E File Offset: 0x0000352E
		public bool SetLobbyType(CSteamID steamIDLobby, LobbyType eLobbyType)
		{
			return this.platform.ISteamMatchmaking_SetLobbyType(steamIDLobby.Value, eLobbyType);
		}

		// Token: 0x04000476 RID: 1142
		internal Platform.Interface platform;

		// Token: 0x04000477 RID: 1143
		internal BaseSteamworks steamworks;
	}
}
