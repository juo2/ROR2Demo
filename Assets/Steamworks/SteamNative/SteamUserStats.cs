using System;
using System.Runtime.InteropServices;
using System.Text;
using Facepunch.Steamworks;

namespace SteamNative
{
	// Token: 0x0200006D RID: 109
	internal class SteamUserStats : IDisposable
	{
		// Token: 0x060002CC RID: 716 RVA: 0x0000798C File Offset: 0x00005B8C
		internal SteamUserStats(BaseSteamworks steamworks, IntPtr pointer)
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

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x060002CD RID: 717 RVA: 0x00007A09 File Offset: 0x00005C09
		public bool IsValid
		{
			get
			{
				return this.platform != null && this.platform.IsValid;
			}
		}

		// Token: 0x060002CE RID: 718 RVA: 0x00007A20 File Offset: 0x00005C20
		public virtual void Dispose()
		{
			if (this.platform != null)
			{
				this.platform.Dispose();
				this.platform = null;
			}
		}

		// Token: 0x060002CF RID: 719 RVA: 0x00007A3C File Offset: 0x00005C3C
		public CallbackHandle AttachLeaderboardUGC(SteamLeaderboard_t hSteamLeaderboard, UGCHandle_t hUGC, Action<LeaderboardUGCSet_t, bool> CallbackFunction = null)
		{
			SteamAPICall_t steamAPICall_t = 0UL;
			steamAPICall_t = this.platform.ISteamUserStats_AttachLeaderboardUGC(hSteamLeaderboard.Value, hUGC.Value);
			if (CallbackFunction == null)
			{
				return null;
			}
			if (steamAPICall_t == 0UL)
			{
				return null;
			}
			return LeaderboardUGCSet_t.CallResult(this.steamworks, steamAPICall_t, CallbackFunction);
		}

		// Token: 0x060002D0 RID: 720 RVA: 0x00007A85 File Offset: 0x00005C85
		public bool ClearAchievement(string pchName)
		{
			return this.platform.ISteamUserStats_ClearAchievement(pchName);
		}

		// Token: 0x060002D1 RID: 721 RVA: 0x00007A94 File Offset: 0x00005C94
		public CallbackHandle DownloadLeaderboardEntries(SteamLeaderboard_t hSteamLeaderboard, LeaderboardDataRequest eLeaderboardDataRequest, int nRangeStart, int nRangeEnd, Action<LeaderboardScoresDownloaded_t, bool> CallbackFunction = null)
		{
			SteamAPICall_t steamAPICall_t = 0UL;
			steamAPICall_t = this.platform.ISteamUserStats_DownloadLeaderboardEntries(hSteamLeaderboard.Value, eLeaderboardDataRequest, nRangeStart, nRangeEnd);
			if (CallbackFunction == null)
			{
				return null;
			}
			if (steamAPICall_t == 0UL)
			{
				return null;
			}
			return LeaderboardScoresDownloaded_t.CallResult(this.steamworks, steamAPICall_t, CallbackFunction);
		}

		// Token: 0x060002D2 RID: 722 RVA: 0x00007AE0 File Offset: 0x00005CE0
		public CallbackHandle DownloadLeaderboardEntriesForUsers(SteamLeaderboard_t hSteamLeaderboard, IntPtr prgUsers, int cUsers, Action<LeaderboardScoresDownloaded_t, bool> CallbackFunction = null)
		{
			SteamAPICall_t steamAPICall_t = 0UL;
			steamAPICall_t = this.platform.ISteamUserStats_DownloadLeaderboardEntriesForUsers(hSteamLeaderboard.Value, prgUsers, cUsers);
			if (CallbackFunction == null)
			{
				return null;
			}
			if (steamAPICall_t == 0UL)
			{
				return null;
			}
			return LeaderboardScoresDownloaded_t.CallResult(this.steamworks, steamAPICall_t, CallbackFunction);
		}

		// Token: 0x060002D3 RID: 723 RVA: 0x00007B28 File Offset: 0x00005D28
		public CallbackHandle FindLeaderboard(string pchLeaderboardName, Action<LeaderboardFindResult_t, bool> CallbackFunction = null)
		{
			SteamAPICall_t steamAPICall_t = 0UL;
			steamAPICall_t = this.platform.ISteamUserStats_FindLeaderboard(pchLeaderboardName);
			if (CallbackFunction == null)
			{
				return null;
			}
			if (steamAPICall_t == 0UL)
			{
				return null;
			}
			return LeaderboardFindResult_t.CallResult(this.steamworks, steamAPICall_t, CallbackFunction);
		}

		// Token: 0x060002D4 RID: 724 RVA: 0x00007B68 File Offset: 0x00005D68
		public CallbackHandle FindOrCreateLeaderboard(string pchLeaderboardName, LeaderboardSortMethod eLeaderboardSortMethod, LeaderboardDisplayType eLeaderboardDisplayType, Action<LeaderboardFindResult_t, bool> CallbackFunction = null)
		{
			SteamAPICall_t steamAPICall_t = 0UL;
			steamAPICall_t = this.platform.ISteamUserStats_FindOrCreateLeaderboard(pchLeaderboardName, eLeaderboardSortMethod, eLeaderboardDisplayType);
			if (CallbackFunction == null)
			{
				return null;
			}
			if (steamAPICall_t == 0UL)
			{
				return null;
			}
			return LeaderboardFindResult_t.CallResult(this.steamworks, steamAPICall_t, CallbackFunction);
		}

		// Token: 0x060002D5 RID: 725 RVA: 0x00007BAA File Offset: 0x00005DAA
		public bool GetAchievement(string pchName, ref bool pbAchieved)
		{
			return this.platform.ISteamUserStats_GetAchievement(pchName, ref pbAchieved);
		}

		// Token: 0x060002D6 RID: 726 RVA: 0x00007BB9 File Offset: 0x00005DB9
		public bool GetAchievementAchievedPercent(string pchName, out float pflPercent)
		{
			return this.platform.ISteamUserStats_GetAchievementAchievedPercent(pchName, out pflPercent);
		}

		// Token: 0x060002D7 RID: 727 RVA: 0x00007BC8 File Offset: 0x00005DC8
		public bool GetAchievementAndUnlockTime(string pchName, ref bool pbAchieved, out uint punUnlockTime)
		{
			return this.platform.ISteamUserStats_GetAchievementAndUnlockTime(pchName, ref pbAchieved, out punUnlockTime);
		}

		// Token: 0x060002D8 RID: 728 RVA: 0x00007BD8 File Offset: 0x00005DD8
		public string GetAchievementDisplayAttribute(string pchName, string pchKey)
		{
			return Marshal.PtrToStringAnsi(this.platform.ISteamUserStats_GetAchievementDisplayAttribute(pchName, pchKey));
		}

		// Token: 0x060002D9 RID: 729 RVA: 0x00007BEC File Offset: 0x00005DEC
		public int GetAchievementIcon(string pchName)
		{
			return this.platform.ISteamUserStats_GetAchievementIcon(pchName);
		}

		// Token: 0x060002DA RID: 730 RVA: 0x00007BFA File Offset: 0x00005DFA
		public string GetAchievementName(uint iAchievement)
		{
			return Marshal.PtrToStringAnsi(this.platform.ISteamUserStats_GetAchievementName(iAchievement));
		}

		// Token: 0x060002DB RID: 731 RVA: 0x00007C0D File Offset: 0x00005E0D
		public bool GetDownloadedLeaderboardEntry(SteamLeaderboardEntries_t hSteamLeaderboardEntries, int index, ref LeaderboardEntry_t pLeaderboardEntry, IntPtr pDetails, int cDetailsMax)
		{
			return this.platform.ISteamUserStats_GetDownloadedLeaderboardEntry(hSteamLeaderboardEntries.Value, index, ref pLeaderboardEntry, pDetails, cDetailsMax);
		}

		// Token: 0x060002DC RID: 732 RVA: 0x00007C26 File Offset: 0x00005E26
		public bool GetGlobalStat(string pchStatName, out long pData)
		{
			return this.platform.ISteamUserStats_GetGlobalStat(pchStatName, out pData);
		}

		// Token: 0x060002DD RID: 733 RVA: 0x00007C35 File Offset: 0x00005E35
		public bool GetGlobalStat0(string pchStatName, out double pData)
		{
			return this.platform.ISteamUserStats_GetGlobalStat0(pchStatName, out pData);
		}

		// Token: 0x060002DE RID: 734 RVA: 0x00007C44 File Offset: 0x00005E44
		public int GetGlobalStatHistory(string pchStatName, out long pData, uint cubData)
		{
			return this.platform.ISteamUserStats_GetGlobalStatHistory(pchStatName, out pData, cubData);
		}

		// Token: 0x060002DF RID: 735 RVA: 0x00007C54 File Offset: 0x00005E54
		public int GetGlobalStatHistory0(string pchStatName, out double pData, uint cubData)
		{
			return this.platform.ISteamUserStats_GetGlobalStatHistory0(pchStatName, out pData, cubData);
		}

		// Token: 0x060002E0 RID: 736 RVA: 0x00007C64 File Offset: 0x00005E64
		public LeaderboardDisplayType GetLeaderboardDisplayType(SteamLeaderboard_t hSteamLeaderboard)
		{
			return this.platform.ISteamUserStats_GetLeaderboardDisplayType(hSteamLeaderboard.Value);
		}

		// Token: 0x060002E1 RID: 737 RVA: 0x00007C77 File Offset: 0x00005E77
		public int GetLeaderboardEntryCount(SteamLeaderboard_t hSteamLeaderboard)
		{
			return this.platform.ISteamUserStats_GetLeaderboardEntryCount(hSteamLeaderboard.Value);
		}

		// Token: 0x060002E2 RID: 738 RVA: 0x00007C8A File Offset: 0x00005E8A
		public string GetLeaderboardName(SteamLeaderboard_t hSteamLeaderboard)
		{
			return Marshal.PtrToStringAnsi(this.platform.ISteamUserStats_GetLeaderboardName(hSteamLeaderboard.Value));
		}

		// Token: 0x060002E3 RID: 739 RVA: 0x00007CA2 File Offset: 0x00005EA2
		public LeaderboardSortMethod GetLeaderboardSortMethod(SteamLeaderboard_t hSteamLeaderboard)
		{
			return this.platform.ISteamUserStats_GetLeaderboardSortMethod(hSteamLeaderboard.Value);
		}

		// Token: 0x060002E4 RID: 740 RVA: 0x00007CB8 File Offset: 0x00005EB8
		public int GetMostAchievedAchievementInfo(out string pchName, out float pflPercent, ref bool pbAchieved)
		{
			pchName = string.Empty;
			StringBuilder stringBuilder = Helpers.TakeStringBuilder();
			uint unNameBufLen = 4096U;
			int num = this.platform.ISteamUserStats_GetMostAchievedAchievementInfo(stringBuilder, unNameBufLen, out pflPercent, ref pbAchieved);
			if (num <= 0)
			{
				return num;
			}
			pchName = stringBuilder.ToString();
			return num;
		}

		// Token: 0x060002E5 RID: 741 RVA: 0x00007CFC File Offset: 0x00005EFC
		public int GetNextMostAchievedAchievementInfo(int iIteratorPrevious, out string pchName, out float pflPercent, ref bool pbAchieved)
		{
			pchName = string.Empty;
			StringBuilder stringBuilder = Helpers.TakeStringBuilder();
			uint unNameBufLen = 4096U;
			int num = this.platform.ISteamUserStats_GetNextMostAchievedAchievementInfo(iIteratorPrevious, stringBuilder, unNameBufLen, out pflPercent, ref pbAchieved);
			if (num <= 0)
			{
				return num;
			}
			pchName = stringBuilder.ToString();
			return num;
		}

		// Token: 0x060002E6 RID: 742 RVA: 0x00007D3F File Offset: 0x00005F3F
		public uint GetNumAchievements()
		{
			return this.platform.ISteamUserStats_GetNumAchievements();
		}

		// Token: 0x060002E7 RID: 743 RVA: 0x00007D4C File Offset: 0x00005F4C
		public CallbackHandle GetNumberOfCurrentPlayers(Action<NumberOfCurrentPlayers_t, bool> CallbackFunction = null)
		{
			SteamAPICall_t steamAPICall_t = 0UL;
			steamAPICall_t = this.platform.ISteamUserStats_GetNumberOfCurrentPlayers();
			if (CallbackFunction == null)
			{
				return null;
			}
			if (steamAPICall_t == 0UL)
			{
				return null;
			}
			return NumberOfCurrentPlayers_t.CallResult(this.steamworks, steamAPICall_t, CallbackFunction);
		}

		// Token: 0x060002E8 RID: 744 RVA: 0x00007D89 File Offset: 0x00005F89
		public bool GetStat(string pchName, out int pData)
		{
			return this.platform.ISteamUserStats_GetStat(pchName, out pData);
		}

		// Token: 0x060002E9 RID: 745 RVA: 0x00007D98 File Offset: 0x00005F98
		public bool GetStat0(string pchName, out float pData)
		{
			return this.platform.ISteamUserStats_GetStat0(pchName, out pData);
		}

		// Token: 0x060002EA RID: 746 RVA: 0x00007DA7 File Offset: 0x00005FA7
		public bool GetUserAchievement(CSteamID steamIDUser, string pchName, ref bool pbAchieved)
		{
			return this.platform.ISteamUserStats_GetUserAchievement(steamIDUser.Value, pchName, ref pbAchieved);
		}

		// Token: 0x060002EB RID: 747 RVA: 0x00007DBC File Offset: 0x00005FBC
		public bool GetUserAchievementAndUnlockTime(CSteamID steamIDUser, string pchName, ref bool pbAchieved, out uint punUnlockTime)
		{
			return this.platform.ISteamUserStats_GetUserAchievementAndUnlockTime(steamIDUser.Value, pchName, ref pbAchieved, out punUnlockTime);
		}

		// Token: 0x060002EC RID: 748 RVA: 0x00007DD3 File Offset: 0x00005FD3
		public bool GetUserStat(CSteamID steamIDUser, string pchName, out int pData)
		{
			return this.platform.ISteamUserStats_GetUserStat(steamIDUser.Value, pchName, out pData);
		}

		// Token: 0x060002ED RID: 749 RVA: 0x00007DE8 File Offset: 0x00005FE8
		public bool GetUserStat0(CSteamID steamIDUser, string pchName, out float pData)
		{
			return this.platform.ISteamUserStats_GetUserStat0(steamIDUser.Value, pchName, out pData);
		}

		// Token: 0x060002EE RID: 750 RVA: 0x00007DFD File Offset: 0x00005FFD
		public bool IndicateAchievementProgress(string pchName, uint nCurProgress, uint nMaxProgress)
		{
			return this.platform.ISteamUserStats_IndicateAchievementProgress(pchName, nCurProgress, nMaxProgress);
		}

		// Token: 0x060002EF RID: 751 RVA: 0x00007E0D File Offset: 0x0000600D
		public bool RequestCurrentStats()
		{
			return this.platform.ISteamUserStats_RequestCurrentStats();
		}

		// Token: 0x060002F0 RID: 752 RVA: 0x00007E1C File Offset: 0x0000601C
		public CallbackHandle RequestGlobalAchievementPercentages(Action<GlobalAchievementPercentagesReady_t, bool> CallbackFunction = null)
		{
			SteamAPICall_t steamAPICall_t = 0UL;
			steamAPICall_t = this.platform.ISteamUserStats_RequestGlobalAchievementPercentages();
			if (CallbackFunction == null)
			{
				return null;
			}
			if (steamAPICall_t == 0UL)
			{
				return null;
			}
			return GlobalAchievementPercentagesReady_t.CallResult(this.steamworks, steamAPICall_t, CallbackFunction);
		}

		// Token: 0x060002F1 RID: 753 RVA: 0x00007E5C File Offset: 0x0000605C
		public CallbackHandle RequestGlobalStats(int nHistoryDays, Action<GlobalStatsReceived_t, bool> CallbackFunction = null)
		{
			SteamAPICall_t steamAPICall_t = 0UL;
			steamAPICall_t = this.platform.ISteamUserStats_RequestGlobalStats(nHistoryDays);
			if (CallbackFunction == null)
			{
				return null;
			}
			if (steamAPICall_t == 0UL)
			{
				return null;
			}
			return GlobalStatsReceived_t.CallResult(this.steamworks, steamAPICall_t, CallbackFunction);
		}

		// Token: 0x060002F2 RID: 754 RVA: 0x00007E9C File Offset: 0x0000609C
		public CallbackHandle RequestUserStats(CSteamID steamIDUser, Action<UserStatsReceived_t, bool> CallbackFunction = null)
		{
			SteamAPICall_t steamAPICall_t = 0UL;
			steamAPICall_t = this.platform.ISteamUserStats_RequestUserStats(steamIDUser.Value);
			if (CallbackFunction == null)
			{
				return null;
			}
			if (steamAPICall_t == 0UL)
			{
				return null;
			}
			return UserStatsReceived_t.CallResult(this.steamworks, steamAPICall_t, CallbackFunction);
		}

		// Token: 0x060002F3 RID: 755 RVA: 0x00007EDF File Offset: 0x000060DF
		public bool ResetAllStats(bool bAchievementsToo)
		{
			return this.platform.ISteamUserStats_ResetAllStats(bAchievementsToo);
		}

		// Token: 0x060002F4 RID: 756 RVA: 0x00007EED File Offset: 0x000060ED
		public bool SetAchievement(string pchName)
		{
			return this.platform.ISteamUserStats_SetAchievement(pchName);
		}

		// Token: 0x060002F5 RID: 757 RVA: 0x00007EFB File Offset: 0x000060FB
		public bool SetStat(string pchName, int nData)
		{
			return this.platform.ISteamUserStats_SetStat(pchName, nData);
		}

		// Token: 0x060002F6 RID: 758 RVA: 0x00007F0A File Offset: 0x0000610A
		public bool SetStat0(string pchName, float fData)
		{
			return this.platform.ISteamUserStats_SetStat0(pchName, fData);
		}

		// Token: 0x060002F7 RID: 759 RVA: 0x00007F19 File Offset: 0x00006119
		public bool StoreStats()
		{
			return this.platform.ISteamUserStats_StoreStats();
		}

		// Token: 0x060002F8 RID: 760 RVA: 0x00007F26 File Offset: 0x00006126
		public bool UpdateAvgRateStat(string pchName, float flCountThisSession, double dSessionLength)
		{
			return this.platform.ISteamUserStats_UpdateAvgRateStat(pchName, flCountThisSession, dSessionLength);
		}

		// Token: 0x060002F9 RID: 761 RVA: 0x00007F38 File Offset: 0x00006138
		public CallbackHandle UploadLeaderboardScore(SteamLeaderboard_t hSteamLeaderboard, LeaderboardUploadScoreMethod eLeaderboardUploadScoreMethod, int nScore, int[] pScoreDetails, int cScoreDetailsCount, Action<LeaderboardScoreUploaded_t, bool> CallbackFunction = null)
		{
			SteamAPICall_t steamAPICall_t = 0UL;
			steamAPICall_t = this.platform.ISteamUserStats_UploadLeaderboardScore(hSteamLeaderboard.Value, eLeaderboardUploadScoreMethod, nScore, pScoreDetails, cScoreDetailsCount);
			if (CallbackFunction == null)
			{
				return null;
			}
			if (steamAPICall_t == 0UL)
			{
				return null;
			}
			return LeaderboardScoreUploaded_t.CallResult(this.steamworks, steamAPICall_t, CallbackFunction);
		}

		// Token: 0x0400048A RID: 1162
		internal Platform.Interface platform;

		// Token: 0x0400048B RID: 1163
		internal BaseSteamworks steamworks;
	}
}
