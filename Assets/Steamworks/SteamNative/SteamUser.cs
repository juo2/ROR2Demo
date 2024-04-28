using System;
using System.Text;
using Facepunch.Steamworks;

namespace SteamNative
{
	// Token: 0x0200006C RID: 108
	internal class SteamUser : IDisposable
	{
		// Token: 0x060002AC RID: 684 RVA: 0x00007664 File Offset: 0x00005864
		internal SteamUser(BaseSteamworks steamworks, IntPtr pointer)
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

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x060002AD RID: 685 RVA: 0x000076E1 File Offset: 0x000058E1
		public bool IsValid
		{
			get
			{
				return this.platform != null && this.platform.IsValid;
			}
		}

		// Token: 0x060002AE RID: 686 RVA: 0x000076F8 File Offset: 0x000058F8
		public virtual void Dispose()
		{
			if (this.platform != null)
			{
				this.platform.Dispose();
				this.platform = null;
			}
		}

		// Token: 0x060002AF RID: 687 RVA: 0x00007714 File Offset: 0x00005914
		public void AdvertiseGame(CSteamID steamIDGameServer, uint unIPServer, ushort usPortServer)
		{
			this.platform.ISteamUser_AdvertiseGame(steamIDGameServer.Value, unIPServer, usPortServer);
		}

		// Token: 0x060002B0 RID: 688 RVA: 0x00007729 File Offset: 0x00005929
		public BeginAuthSessionResult BeginAuthSession(IntPtr pAuthTicket, int cbAuthTicket, CSteamID steamID)
		{
			return this.platform.ISteamUser_BeginAuthSession(pAuthTicket, cbAuthTicket, steamID.Value);
		}

		// Token: 0x060002B1 RID: 689 RVA: 0x0000773E File Offset: 0x0000593E
		public bool BIsBehindNAT()
		{
			return this.platform.ISteamUser_BIsBehindNAT();
		}

		// Token: 0x060002B2 RID: 690 RVA: 0x0000774B File Offset: 0x0000594B
		public bool BIsPhoneIdentifying()
		{
			return this.platform.ISteamUser_BIsPhoneIdentifying();
		}

		// Token: 0x060002B3 RID: 691 RVA: 0x00007758 File Offset: 0x00005958
		public bool BIsPhoneRequiringVerification()
		{
			return this.platform.ISteamUser_BIsPhoneRequiringVerification();
		}

		// Token: 0x060002B4 RID: 692 RVA: 0x00007765 File Offset: 0x00005965
		public bool BIsPhoneVerified()
		{
			return this.platform.ISteamUser_BIsPhoneVerified();
		}

		// Token: 0x060002B5 RID: 693 RVA: 0x00007772 File Offset: 0x00005972
		public bool BIsTwoFactorEnabled()
		{
			return this.platform.ISteamUser_BIsTwoFactorEnabled();
		}

		// Token: 0x060002B6 RID: 694 RVA: 0x0000777F File Offset: 0x0000597F
		public bool BLoggedOn()
		{
			return this.platform.ISteamUser_BLoggedOn();
		}

		// Token: 0x060002B7 RID: 695 RVA: 0x0000778C File Offset: 0x0000598C
		public void CancelAuthTicket(HAuthTicket hAuthTicket)
		{
			this.platform.ISteamUser_CancelAuthTicket(hAuthTicket.Value);
		}

		// Token: 0x060002B8 RID: 696 RVA: 0x0000779F File Offset: 0x0000599F
		public VoiceResult DecompressVoice(IntPtr pCompressed, uint cbCompressed, IntPtr pDestBuffer, uint cbDestBufferSize, out uint nBytesWritten, uint nDesiredSampleRate)
		{
			return this.platform.ISteamUser_DecompressVoice(pCompressed, cbCompressed, pDestBuffer, cbDestBufferSize, out nBytesWritten, nDesiredSampleRate);
		}

		// Token: 0x060002B9 RID: 697 RVA: 0x000077B5 File Offset: 0x000059B5
		public void EndAuthSession(CSteamID steamID)
		{
			this.platform.ISteamUser_EndAuthSession(steamID.Value);
		}

		// Token: 0x060002BA RID: 698 RVA: 0x000077C8 File Offset: 0x000059C8
		public HAuthTicket GetAuthSessionTicket(IntPtr pTicket, int cbMaxTicket, out uint pcbTicket)
		{
			return this.platform.ISteamUser_GetAuthSessionTicket(pTicket, cbMaxTicket, out pcbTicket);
		}

		// Token: 0x060002BB RID: 699 RVA: 0x000077D8 File Offset: 0x000059D8
		public VoiceResult GetAvailableVoice(out uint pcbCompressed, out uint pcbUncompressed_Deprecated, uint nUncompressedVoiceDesiredSampleRate_Deprecated)
		{
			return this.platform.ISteamUser_GetAvailableVoice(out pcbCompressed, out pcbUncompressed_Deprecated, nUncompressedVoiceDesiredSampleRate_Deprecated);
		}

		// Token: 0x060002BC RID: 700 RVA: 0x000077E8 File Offset: 0x000059E8
		public bool GetEncryptedAppTicket(IntPtr pTicket, int cbMaxTicket, out uint pcbTicket)
		{
			return this.platform.ISteamUser_GetEncryptedAppTicket(pTicket, cbMaxTicket, out pcbTicket);
		}

		// Token: 0x060002BD RID: 701 RVA: 0x000077F8 File Offset: 0x000059F8
		public int GetGameBadgeLevel(int nSeries, bool bFoil)
		{
			return this.platform.ISteamUser_GetGameBadgeLevel(nSeries, bFoil);
		}

		// Token: 0x060002BE RID: 702 RVA: 0x00007807 File Offset: 0x00005A07
		public HSteamUser GetHSteamUser()
		{
			return this.platform.ISteamUser_GetHSteamUser();
		}

		// Token: 0x060002BF RID: 703 RVA: 0x00007814 File Offset: 0x00005A14
		public int GetPlayerSteamLevel()
		{
			return this.platform.ISteamUser_GetPlayerSteamLevel();
		}

		// Token: 0x060002C0 RID: 704 RVA: 0x00007821 File Offset: 0x00005A21
		public ulong GetSteamID()
		{
			return this.platform.ISteamUser_GetSteamID();
		}

		// Token: 0x060002C1 RID: 705 RVA: 0x00007834 File Offset: 0x00005A34
		public string GetUserDataFolder()
		{
			StringBuilder stringBuilder = Helpers.TakeStringBuilder();
			int cubBuffer = 4096;
			if (!this.platform.ISteamUser_GetUserDataFolder(stringBuilder, cubBuffer))
			{
				return null;
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060002C2 RID: 706 RVA: 0x00007864 File Offset: 0x00005A64
		public VoiceResult GetVoice(bool bWantCompressed, IntPtr pDestBuffer, uint cbDestBufferSize, out uint nBytesWritten, bool bWantUncompressed_Deprecated, IntPtr pUncompressedDestBuffer_Deprecated, uint cbUncompressedDestBufferSize_Deprecated, out uint nUncompressBytesWritten_Deprecated, uint nUncompressedVoiceDesiredSampleRate_Deprecated)
		{
			return this.platform.ISteamUser_GetVoice(bWantCompressed, pDestBuffer, cbDestBufferSize, out nBytesWritten, bWantUncompressed_Deprecated, pUncompressedDestBuffer_Deprecated, cbUncompressedDestBufferSize_Deprecated, out nUncompressBytesWritten_Deprecated, nUncompressedVoiceDesiredSampleRate_Deprecated);
		}

		// Token: 0x060002C3 RID: 707 RVA: 0x0000788B File Offset: 0x00005A8B
		public uint GetVoiceOptimalSampleRate()
		{
			return this.platform.ISteamUser_GetVoiceOptimalSampleRate();
		}

		// Token: 0x060002C4 RID: 708 RVA: 0x00007898 File Offset: 0x00005A98
		public int InitiateGameConnection(IntPtr pAuthBlob, int cbMaxAuthBlob, CSteamID steamIDGameServer, uint unIPServer, ushort usPortServer, bool bSecure)
		{
			return this.platform.ISteamUser_InitiateGameConnection(pAuthBlob, cbMaxAuthBlob, steamIDGameServer.Value, unIPServer, usPortServer, bSecure);
		}

		// Token: 0x060002C5 RID: 709 RVA: 0x000078B4 File Offset: 0x00005AB4
		public CallbackHandle RequestEncryptedAppTicket(IntPtr pDataToInclude, int cbDataToInclude, Action<EncryptedAppTicketResponse_t, bool> CallbackFunction = null)
		{
			SteamAPICall_t steamAPICall_t = 0UL;
			steamAPICall_t = this.platform.ISteamUser_RequestEncryptedAppTicket(pDataToInclude, cbDataToInclude);
			if (CallbackFunction == null)
			{
				return null;
			}
			if (steamAPICall_t == 0UL)
			{
				return null;
			}
			return EncryptedAppTicketResponse_t.CallResult(this.steamworks, steamAPICall_t, CallbackFunction);
		}

		// Token: 0x060002C6 RID: 710 RVA: 0x000078F4 File Offset: 0x00005AF4
		public CallbackHandle RequestStoreAuthURL(string pchRedirectURL, Action<StoreAuthURLResponse_t, bool> CallbackFunction = null)
		{
			SteamAPICall_t steamAPICall_t = 0UL;
			steamAPICall_t = this.platform.ISteamUser_RequestStoreAuthURL(pchRedirectURL);
			if (CallbackFunction == null)
			{
				return null;
			}
			if (steamAPICall_t == 0UL)
			{
				return null;
			}
			return StoreAuthURLResponse_t.CallResult(this.steamworks, steamAPICall_t, CallbackFunction);
		}

		// Token: 0x060002C7 RID: 711 RVA: 0x00007932 File Offset: 0x00005B32
		public void StartVoiceRecording()
		{
			this.platform.ISteamUser_StartVoiceRecording();
		}

		// Token: 0x060002C8 RID: 712 RVA: 0x0000793F File Offset: 0x00005B3F
		public void StopVoiceRecording()
		{
			this.platform.ISteamUser_StopVoiceRecording();
		}

		// Token: 0x060002C9 RID: 713 RVA: 0x0000794C File Offset: 0x00005B4C
		public void TerminateGameConnection(uint unIPServer, ushort usPortServer)
		{
			this.platform.ISteamUser_TerminateGameConnection(unIPServer, usPortServer);
		}

		// Token: 0x060002CA RID: 714 RVA: 0x0000795B File Offset: 0x00005B5B
		public void TrackAppUsageEvent(CGameID gameID, int eAppUsageEvent, string pchExtraInfo)
		{
			this.platform.ISteamUser_TrackAppUsageEvent(gameID.Value, eAppUsageEvent, pchExtraInfo);
		}

		// Token: 0x060002CB RID: 715 RVA: 0x00007970 File Offset: 0x00005B70
		public UserHasLicenseForAppResult UserHasLicenseForApp(CSteamID steamID, AppId_t appID)
		{
			return this.platform.ISteamUser_UserHasLicenseForApp(steamID.Value, appID.Value);
		}

		// Token: 0x04000488 RID: 1160
		internal Platform.Interface platform;

		// Token: 0x04000489 RID: 1161
		internal BaseSteamworks steamworks;
	}
}
