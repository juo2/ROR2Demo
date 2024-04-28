using System;
using Facepunch.Steamworks;

namespace SteamNative
{
	// Token: 0x0200005E RID: 94
	internal class SteamGameServer : IDisposable
	{
		// Token: 0x060000E4 RID: 228 RVA: 0x00003B10 File Offset: 0x00001D10
		internal SteamGameServer(BaseSteamworks steamworks, IntPtr pointer)
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

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x060000E5 RID: 229 RVA: 0x00003B8D File Offset: 0x00001D8D
		public bool IsValid
		{
			get
			{
				return this.platform != null && this.platform.IsValid;
			}
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x00003BA4 File Offset: 0x00001DA4
		public virtual void Dispose()
		{
			if (this.platform != null)
			{
				this.platform.Dispose();
				this.platform = null;
			}
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x00003BC0 File Offset: 0x00001DC0
		public CallbackHandle AssociateWithClan(CSteamID steamIDClan, Action<AssociateWithClanResult_t, bool> CallbackFunction = null)
		{
			SteamAPICall_t steamAPICall_t = 0UL;
			steamAPICall_t = this.platform.ISteamGameServer_AssociateWithClan(steamIDClan.Value);
			if (CallbackFunction == null)
			{
				return null;
			}
			if (steamAPICall_t == 0UL)
			{
				return null;
			}
			return AssociateWithClanResult_t.CallResult(this.steamworks, steamAPICall_t, CallbackFunction);
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x00003C03 File Offset: 0x00001E03
		public BeginAuthSessionResult BeginAuthSession(IntPtr pAuthTicket, int cbAuthTicket, CSteamID steamID)
		{
			return this.platform.ISteamGameServer_BeginAuthSession(pAuthTicket, cbAuthTicket, steamID.Value);
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x00003C18 File Offset: 0x00001E18
		public bool BLoggedOn()
		{
			return this.platform.ISteamGameServer_BLoggedOn();
		}

		// Token: 0x060000EA RID: 234 RVA: 0x00003C25 File Offset: 0x00001E25
		public bool BSecure()
		{
			return this.platform.ISteamGameServer_BSecure();
		}

		// Token: 0x060000EB RID: 235 RVA: 0x00003C32 File Offset: 0x00001E32
		public bool BUpdateUserData(CSteamID steamIDUser, string pchPlayerName, uint uScore)
		{
			return this.platform.ISteamGameServer_BUpdateUserData(steamIDUser.Value, pchPlayerName, uScore);
		}

		// Token: 0x060000EC RID: 236 RVA: 0x00003C47 File Offset: 0x00001E47
		public void CancelAuthTicket(HAuthTicket hAuthTicket)
		{
			this.platform.ISteamGameServer_CancelAuthTicket(hAuthTicket.Value);
		}

		// Token: 0x060000ED RID: 237 RVA: 0x00003C5A File Offset: 0x00001E5A
		public void ClearAllKeyValues()
		{
			this.platform.ISteamGameServer_ClearAllKeyValues();
		}

		// Token: 0x060000EE RID: 238 RVA: 0x00003C68 File Offset: 0x00001E68
		public CallbackHandle ComputeNewPlayerCompatibility(CSteamID steamIDNewPlayer, Action<ComputeNewPlayerCompatibilityResult_t, bool> CallbackFunction = null)
		{
			SteamAPICall_t steamAPICall_t = 0UL;
			steamAPICall_t = this.platform.ISteamGameServer_ComputeNewPlayerCompatibility(steamIDNewPlayer.Value);
			if (CallbackFunction == null)
			{
				return null;
			}
			if (steamAPICall_t == 0UL)
			{
				return null;
			}
			return ComputeNewPlayerCompatibilityResult_t.CallResult(this.steamworks, steamAPICall_t, CallbackFunction);
		}

		// Token: 0x060000EF RID: 239 RVA: 0x00003CAB File Offset: 0x00001EAB
		public ulong CreateUnauthenticatedUserConnection()
		{
			return this.platform.ISteamGameServer_CreateUnauthenticatedUserConnection();
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x00003CBD File Offset: 0x00001EBD
		public void EnableHeartbeats(bool bActive)
		{
			this.platform.ISteamGameServer_EnableHeartbeats(bActive);
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x00003CCB File Offset: 0x00001ECB
		public void EndAuthSession(CSteamID steamID)
		{
			this.platform.ISteamGameServer_EndAuthSession(steamID.Value);
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x00003CDE File Offset: 0x00001EDE
		public void ForceHeartbeat()
		{
			this.platform.ISteamGameServer_ForceHeartbeat();
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x00003CEB File Offset: 0x00001EEB
		public HAuthTicket GetAuthSessionTicket(IntPtr pTicket, int cbMaxTicket, out uint pcbTicket)
		{
			return this.platform.ISteamGameServer_GetAuthSessionTicket(pTicket, cbMaxTicket, out pcbTicket);
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x00003CFB File Offset: 0x00001EFB
		public void GetGameplayStats()
		{
			this.platform.ISteamGameServer_GetGameplayStats();
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x00003D08 File Offset: 0x00001F08
		public int GetNextOutgoingPacket(IntPtr pOut, int cbMaxOut, out uint pNetAdr, out ushort pPort)
		{
			return this.platform.ISteamGameServer_GetNextOutgoingPacket(pOut, cbMaxOut, out pNetAdr, out pPort);
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x00003D1A File Offset: 0x00001F1A
		public uint GetPublicIP()
		{
			return this.platform.ISteamGameServer_GetPublicIP();
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x00003D28 File Offset: 0x00001F28
		public CallbackHandle GetServerReputation(Action<GSReputation_t, bool> CallbackFunction = null)
		{
			SteamAPICall_t steamAPICall_t = 0UL;
			steamAPICall_t = this.platform.ISteamGameServer_GetServerReputation();
			if (CallbackFunction == null)
			{
				return null;
			}
			if (steamAPICall_t == 0UL)
			{
				return null;
			}
			return GSReputation_t.CallResult(this.steamworks, steamAPICall_t, CallbackFunction);
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x00003D65 File Offset: 0x00001F65
		public ulong GetSteamID()
		{
			return this.platform.ISteamGameServer_GetSteamID();
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x00003D77 File Offset: 0x00001F77
		public bool HandleIncomingPacket(IntPtr pData, int cbData, uint srcIP, ushort srcPort)
		{
			return this.platform.ISteamGameServer_HandleIncomingPacket(pData, cbData, srcIP, srcPort);
		}

		// Token: 0x060000FA RID: 250 RVA: 0x00003D89 File Offset: 0x00001F89
		public bool InitGameServer(uint unIP, ushort usGamePort, ushort usQueryPort, uint unFlags, AppId_t nGameAppId, string pchVersionString)
		{
			return this.platform.ISteamGameServer_InitGameServer(unIP, usGamePort, usQueryPort, unFlags, nGameAppId.Value, pchVersionString);
		}

		// Token: 0x060000FB RID: 251 RVA: 0x00003DA4 File Offset: 0x00001FA4
		public void LogOff()
		{
			this.platform.ISteamGameServer_LogOff();
		}

		// Token: 0x060000FC RID: 252 RVA: 0x00003DB1 File Offset: 0x00001FB1
		public void LogOn(string pszToken)
		{
			this.platform.ISteamGameServer_LogOn(pszToken);
		}

		// Token: 0x060000FD RID: 253 RVA: 0x00003DBF File Offset: 0x00001FBF
		public void LogOnAnonymous()
		{
			this.platform.ISteamGameServer_LogOnAnonymous();
		}

		// Token: 0x060000FE RID: 254 RVA: 0x00003DCC File Offset: 0x00001FCC
		public bool RequestUserGroupStatus(CSteamID steamIDUser, CSteamID steamIDGroup)
		{
			return this.platform.ISteamGameServer_RequestUserGroupStatus(steamIDUser.Value, steamIDGroup.Value);
		}

		// Token: 0x060000FF RID: 255 RVA: 0x00003DE5 File Offset: 0x00001FE5
		public bool SendUserConnectAndAuthenticate(uint unIPClient, IntPtr pvAuthBlob, uint cubAuthBlobSize, out CSteamID pSteamIDUser)
		{
			return this.platform.ISteamGameServer_SendUserConnectAndAuthenticate(unIPClient, pvAuthBlob, cubAuthBlobSize, out pSteamIDUser.Value);
		}

		// Token: 0x06000100 RID: 256 RVA: 0x00003DFC File Offset: 0x00001FFC
		public void SendUserDisconnect(CSteamID steamIDUser)
		{
			this.platform.ISteamGameServer_SendUserDisconnect(steamIDUser.Value);
		}

		// Token: 0x06000101 RID: 257 RVA: 0x00003E0F File Offset: 0x0000200F
		public void SetBotPlayerCount(int cBotplayers)
		{
			this.platform.ISteamGameServer_SetBotPlayerCount(cBotplayers);
		}

		// Token: 0x06000102 RID: 258 RVA: 0x00003E1D File Offset: 0x0000201D
		public void SetDedicatedServer(bool bDedicated)
		{
			this.platform.ISteamGameServer_SetDedicatedServer(bDedicated);
		}

		// Token: 0x06000103 RID: 259 RVA: 0x00003E2B File Offset: 0x0000202B
		public void SetGameData(string pchGameData)
		{
			this.platform.ISteamGameServer_SetGameData(pchGameData);
		}

		// Token: 0x06000104 RID: 260 RVA: 0x00003E39 File Offset: 0x00002039
		public void SetGameDescription(string pszGameDescription)
		{
			this.platform.ISteamGameServer_SetGameDescription(pszGameDescription);
		}

		// Token: 0x06000105 RID: 261 RVA: 0x00003E47 File Offset: 0x00002047
		public void SetGameTags(string pchGameTags)
		{
			this.platform.ISteamGameServer_SetGameTags(pchGameTags);
		}

		// Token: 0x06000106 RID: 262 RVA: 0x00003E55 File Offset: 0x00002055
		public void SetHeartbeatInterval(int iHeartbeatInterval)
		{
			this.platform.ISteamGameServer_SetHeartbeatInterval(iHeartbeatInterval);
		}

		// Token: 0x06000107 RID: 263 RVA: 0x00003E63 File Offset: 0x00002063
		public void SetKeyValue(string pKey, string pValue)
		{
			this.platform.ISteamGameServer_SetKeyValue(pKey, pValue);
		}

		// Token: 0x06000108 RID: 264 RVA: 0x00003E72 File Offset: 0x00002072
		public void SetMapName(string pszMapName)
		{
			this.platform.ISteamGameServer_SetMapName(pszMapName);
		}

		// Token: 0x06000109 RID: 265 RVA: 0x00003E80 File Offset: 0x00002080
		public void SetMaxPlayerCount(int cPlayersMax)
		{
			this.platform.ISteamGameServer_SetMaxPlayerCount(cPlayersMax);
		}

		// Token: 0x0600010A RID: 266 RVA: 0x00003E8E File Offset: 0x0000208E
		public void SetModDir(string pszModDir)
		{
			this.platform.ISteamGameServer_SetModDir(pszModDir);
		}

		// Token: 0x0600010B RID: 267 RVA: 0x00003E9C File Offset: 0x0000209C
		public void SetPasswordProtected(bool bPasswordProtected)
		{
			this.platform.ISteamGameServer_SetPasswordProtected(bPasswordProtected);
		}

		// Token: 0x0600010C RID: 268 RVA: 0x00003EAA File Offset: 0x000020AA
		public void SetProduct(string pszProduct)
		{
			this.platform.ISteamGameServer_SetProduct(pszProduct);
		}

		// Token: 0x0600010D RID: 269 RVA: 0x00003EB8 File Offset: 0x000020B8
		public void SetRegion(string pszRegion)
		{
			this.platform.ISteamGameServer_SetRegion(pszRegion);
		}

		// Token: 0x0600010E RID: 270 RVA: 0x00003EC6 File Offset: 0x000020C6
		public void SetServerName(string pszServerName)
		{
			this.platform.ISteamGameServer_SetServerName(pszServerName);
		}

		// Token: 0x0600010F RID: 271 RVA: 0x00003ED4 File Offset: 0x000020D4
		public void SetSpectatorPort(ushort unSpectatorPort)
		{
			this.platform.ISteamGameServer_SetSpectatorPort(unSpectatorPort);
		}

		// Token: 0x06000110 RID: 272 RVA: 0x00003EE2 File Offset: 0x000020E2
		public void SetSpectatorServerName(string pszSpectatorServerName)
		{
			this.platform.ISteamGameServer_SetSpectatorServerName(pszSpectatorServerName);
		}

		// Token: 0x06000111 RID: 273 RVA: 0x00003EF0 File Offset: 0x000020F0
		public UserHasLicenseForAppResult UserHasLicenseForApp(CSteamID steamID, AppId_t appID)
		{
			return this.platform.ISteamGameServer_UserHasLicenseForApp(steamID.Value, appID.Value);
		}

		// Token: 0x06000112 RID: 274 RVA: 0x00003F09 File Offset: 0x00002109
		public bool WasRestartRequested()
		{
			return this.platform.ISteamGameServer_WasRestartRequested();
		}

		// Token: 0x0400046C RID: 1132
		internal Platform.Interface platform;

		// Token: 0x0400046D RID: 1133
		internal BaseSteamworks steamworks;
	}
}
