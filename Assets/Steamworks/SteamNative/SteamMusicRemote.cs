using System;
using Facepunch.Steamworks;

namespace SteamNative
{
	// Token: 0x02000066 RID: 102
	internal class SteamMusicRemote : IDisposable
	{
		// Token: 0x060001D4 RID: 468 RVA: 0x0000569C File Offset: 0x0000389C
		internal SteamMusicRemote(BaseSteamworks steamworks, IntPtr pointer)
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

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x060001D5 RID: 469 RVA: 0x00005719 File Offset: 0x00003919
		public bool IsValid
		{
			get
			{
				return this.platform != null && this.platform.IsValid;
			}
		}

		// Token: 0x060001D6 RID: 470 RVA: 0x00005730 File Offset: 0x00003930
		public virtual void Dispose()
		{
			if (this.platform != null)
			{
				this.platform.Dispose();
				this.platform = null;
			}
		}

		// Token: 0x060001D7 RID: 471 RVA: 0x0000574C File Offset: 0x0000394C
		public bool BActivationSuccess(bool bValue)
		{
			return this.platform.ISteamMusicRemote_BActivationSuccess(bValue);
		}

		// Token: 0x060001D8 RID: 472 RVA: 0x0000575A File Offset: 0x0000395A
		public bool BIsCurrentMusicRemote()
		{
			return this.platform.ISteamMusicRemote_BIsCurrentMusicRemote();
		}

		// Token: 0x060001D9 RID: 473 RVA: 0x00005767 File Offset: 0x00003967
		public bool CurrentEntryDidChange()
		{
			return this.platform.ISteamMusicRemote_CurrentEntryDidChange();
		}

		// Token: 0x060001DA RID: 474 RVA: 0x00005774 File Offset: 0x00003974
		public bool CurrentEntryIsAvailable(bool bAvailable)
		{
			return this.platform.ISteamMusicRemote_CurrentEntryIsAvailable(bAvailable);
		}

		// Token: 0x060001DB RID: 475 RVA: 0x00005782 File Offset: 0x00003982
		public bool CurrentEntryWillChange()
		{
			return this.platform.ISteamMusicRemote_CurrentEntryWillChange();
		}

		// Token: 0x060001DC RID: 476 RVA: 0x0000578F File Offset: 0x0000398F
		public bool DeregisterSteamMusicRemote()
		{
			return this.platform.ISteamMusicRemote_DeregisterSteamMusicRemote();
		}

		// Token: 0x060001DD RID: 477 RVA: 0x0000579C File Offset: 0x0000399C
		public bool EnableLooped(bool bValue)
		{
			return this.platform.ISteamMusicRemote_EnableLooped(bValue);
		}

		// Token: 0x060001DE RID: 478 RVA: 0x000057AA File Offset: 0x000039AA
		public bool EnablePlaylists(bool bValue)
		{
			return this.platform.ISteamMusicRemote_EnablePlaylists(bValue);
		}

		// Token: 0x060001DF RID: 479 RVA: 0x000057B8 File Offset: 0x000039B8
		public bool EnablePlayNext(bool bValue)
		{
			return this.platform.ISteamMusicRemote_EnablePlayNext(bValue);
		}

		// Token: 0x060001E0 RID: 480 RVA: 0x000057C6 File Offset: 0x000039C6
		public bool EnablePlayPrevious(bool bValue)
		{
			return this.platform.ISteamMusicRemote_EnablePlayPrevious(bValue);
		}

		// Token: 0x060001E1 RID: 481 RVA: 0x000057D4 File Offset: 0x000039D4
		public bool EnableQueue(bool bValue)
		{
			return this.platform.ISteamMusicRemote_EnableQueue(bValue);
		}

		// Token: 0x060001E2 RID: 482 RVA: 0x000057E2 File Offset: 0x000039E2
		public bool EnableShuffled(bool bValue)
		{
			return this.platform.ISteamMusicRemote_EnableShuffled(bValue);
		}

		// Token: 0x060001E3 RID: 483 RVA: 0x000057F0 File Offset: 0x000039F0
		public bool PlaylistDidChange()
		{
			return this.platform.ISteamMusicRemote_PlaylistDidChange();
		}

		// Token: 0x060001E4 RID: 484 RVA: 0x000057FD File Offset: 0x000039FD
		public bool PlaylistWillChange()
		{
			return this.platform.ISteamMusicRemote_PlaylistWillChange();
		}

		// Token: 0x060001E5 RID: 485 RVA: 0x0000580A File Offset: 0x00003A0A
		public bool QueueDidChange()
		{
			return this.platform.ISteamMusicRemote_QueueDidChange();
		}

		// Token: 0x060001E6 RID: 486 RVA: 0x00005817 File Offset: 0x00003A17
		public bool QueueWillChange()
		{
			return this.platform.ISteamMusicRemote_QueueWillChange();
		}

		// Token: 0x060001E7 RID: 487 RVA: 0x00005824 File Offset: 0x00003A24
		public bool RegisterSteamMusicRemote(string pchName)
		{
			return this.platform.ISteamMusicRemote_RegisterSteamMusicRemote(pchName);
		}

		// Token: 0x060001E8 RID: 488 RVA: 0x00005832 File Offset: 0x00003A32
		public bool ResetPlaylistEntries()
		{
			return this.platform.ISteamMusicRemote_ResetPlaylistEntries();
		}

		// Token: 0x060001E9 RID: 489 RVA: 0x0000583F File Offset: 0x00003A3F
		public bool ResetQueueEntries()
		{
			return this.platform.ISteamMusicRemote_ResetQueueEntries();
		}

		// Token: 0x060001EA RID: 490 RVA: 0x0000584C File Offset: 0x00003A4C
		public bool SetCurrentPlaylistEntry(int nID)
		{
			return this.platform.ISteamMusicRemote_SetCurrentPlaylistEntry(nID);
		}

		// Token: 0x060001EB RID: 491 RVA: 0x0000585A File Offset: 0x00003A5A
		public bool SetCurrentQueueEntry(int nID)
		{
			return this.platform.ISteamMusicRemote_SetCurrentQueueEntry(nID);
		}

		// Token: 0x060001EC RID: 492 RVA: 0x00005868 File Offset: 0x00003A68
		public bool SetDisplayName(string pchDisplayName)
		{
			return this.platform.ISteamMusicRemote_SetDisplayName(pchDisplayName);
		}

		// Token: 0x060001ED RID: 493 RVA: 0x00005876 File Offset: 0x00003A76
		public bool SetPlaylistEntry(int nID, int nPosition, string pchEntryText)
		{
			return this.platform.ISteamMusicRemote_SetPlaylistEntry(nID, nPosition, pchEntryText);
		}

		// Token: 0x060001EE RID: 494 RVA: 0x00005886 File Offset: 0x00003A86
		public bool SetPNGIcon_64x64(IntPtr pvBuffer, uint cbBufferLength)
		{
			return this.platform.ISteamMusicRemote_SetPNGIcon_64x64(pvBuffer, cbBufferLength);
		}

		// Token: 0x060001EF RID: 495 RVA: 0x00005895 File Offset: 0x00003A95
		public bool SetQueueEntry(int nID, int nPosition, string pchEntryText)
		{
			return this.platform.ISteamMusicRemote_SetQueueEntry(nID, nPosition, pchEntryText);
		}

		// Token: 0x060001F0 RID: 496 RVA: 0x000058A5 File Offset: 0x00003AA5
		public bool UpdateCurrentEntryCoverArt(IntPtr pvBuffer, uint cbBufferLength)
		{
			return this.platform.ISteamMusicRemote_UpdateCurrentEntryCoverArt(pvBuffer, cbBufferLength);
		}

		// Token: 0x060001F1 RID: 497 RVA: 0x000058B4 File Offset: 0x00003AB4
		public bool UpdateCurrentEntryElapsedSeconds(int nValue)
		{
			return this.platform.ISteamMusicRemote_UpdateCurrentEntryElapsedSeconds(nValue);
		}

		// Token: 0x060001F2 RID: 498 RVA: 0x000058C2 File Offset: 0x00003AC2
		public bool UpdateCurrentEntryText(string pchText)
		{
			return this.platform.ISteamMusicRemote_UpdateCurrentEntryText(pchText);
		}

		// Token: 0x060001F3 RID: 499 RVA: 0x000058D0 File Offset: 0x00003AD0
		public bool UpdateLooped(bool bValue)
		{
			return this.platform.ISteamMusicRemote_UpdateLooped(bValue);
		}

		// Token: 0x060001F4 RID: 500 RVA: 0x000058DE File Offset: 0x00003ADE
		public bool UpdatePlaybackStatus(AudioPlayback_Status nStatus)
		{
			return this.platform.ISteamMusicRemote_UpdatePlaybackStatus(nStatus);
		}

		// Token: 0x060001F5 RID: 501 RVA: 0x000058EC File Offset: 0x00003AEC
		public bool UpdateShuffled(bool bValue)
		{
			return this.platform.ISteamMusicRemote_UpdateShuffled(bValue);
		}

		// Token: 0x060001F6 RID: 502 RVA: 0x000058FA File Offset: 0x00003AFA
		public bool UpdateVolume(float flValue)
		{
			return this.platform.ISteamMusicRemote_UpdateVolume(flValue);
		}

		// Token: 0x0400047C RID: 1148
		internal Platform.Interface platform;

		// Token: 0x0400047D RID: 1149
		internal BaseSteamworks steamworks;
	}
}
