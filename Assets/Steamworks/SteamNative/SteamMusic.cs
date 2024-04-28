using System;
using Facepunch.Steamworks;

namespace SteamNative
{
	// Token: 0x02000065 RID: 101
	internal class SteamMusic : IDisposable
	{
		// Token: 0x060001C8 RID: 456 RVA: 0x00005574 File Offset: 0x00003774
		internal SteamMusic(BaseSteamworks steamworks, IntPtr pointer)
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

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x060001C9 RID: 457 RVA: 0x000055F1 File Offset: 0x000037F1
		public bool IsValid
		{
			get
			{
				return this.platform != null && this.platform.IsValid;
			}
		}

		// Token: 0x060001CA RID: 458 RVA: 0x00005608 File Offset: 0x00003808
		public virtual void Dispose()
		{
			if (this.platform != null)
			{
				this.platform.Dispose();
				this.platform = null;
			}
		}

		// Token: 0x060001CB RID: 459 RVA: 0x00005624 File Offset: 0x00003824
		public bool BIsEnabled()
		{
			return this.platform.ISteamMusic_BIsEnabled();
		}

		// Token: 0x060001CC RID: 460 RVA: 0x00005631 File Offset: 0x00003831
		public bool BIsPlaying()
		{
			return this.platform.ISteamMusic_BIsPlaying();
		}

		// Token: 0x060001CD RID: 461 RVA: 0x0000563E File Offset: 0x0000383E
		public AudioPlayback_Status GetPlaybackStatus()
		{
			return this.platform.ISteamMusic_GetPlaybackStatus();
		}

		// Token: 0x060001CE RID: 462 RVA: 0x0000564B File Offset: 0x0000384B
		public float GetVolume()
		{
			return this.platform.ISteamMusic_GetVolume();
		}

		// Token: 0x060001CF RID: 463 RVA: 0x00005658 File Offset: 0x00003858
		public void Pause()
		{
			this.platform.ISteamMusic_Pause();
		}

		// Token: 0x060001D0 RID: 464 RVA: 0x00005665 File Offset: 0x00003865
		public void Play()
		{
			this.platform.ISteamMusic_Play();
		}

		// Token: 0x060001D1 RID: 465 RVA: 0x00005672 File Offset: 0x00003872
		public void PlayNext()
		{
			this.platform.ISteamMusic_PlayNext();
		}

		// Token: 0x060001D2 RID: 466 RVA: 0x0000567F File Offset: 0x0000387F
		public void PlayPrevious()
		{
			this.platform.ISteamMusic_PlayPrevious();
		}

		// Token: 0x060001D3 RID: 467 RVA: 0x0000568C File Offset: 0x0000388C
		public void SetVolume(float flVolume)
		{
			this.platform.ISteamMusic_SetVolume(flVolume);
		}

		// Token: 0x0400047A RID: 1146
		internal Platform.Interface platform;

		// Token: 0x0400047B RID: 1147
		internal BaseSteamworks steamworks;
	}
}
