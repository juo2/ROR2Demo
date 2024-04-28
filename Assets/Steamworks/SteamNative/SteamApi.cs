using System;

namespace SteamNative
{
	// Token: 0x02000058 RID: 88
	internal class SteamApi : IDisposable
	{
		// Token: 0x0600001C RID: 28 RVA: 0x00002474 File Offset: 0x00000674
		internal SteamApi()
		{
			if (Platform.IsWindows64)
			{
				this.platform = new Platform.Win64((IntPtr)1);
				return;
			}
			if (Platform.IsWindows32)
			{
				this.platform = new Platform.Win32((IntPtr)1);
				return;
			}
			if (Platform.IsLinux32)
			{
				this.platform = new Platform.Linux32((IntPtr)1);
				return;
			}
			if (Platform.IsLinux64)
			{
				this.platform = new Platform.Linux64((IntPtr)1);
				return;
			}
			if (Platform.IsOsx)
			{
				this.platform = new Platform.Mac((IntPtr)1);
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600001D RID: 29 RVA: 0x00002503 File Offset: 0x00000703
		public bool IsValid
		{
			get
			{
				return this.platform != null && this.platform.IsValid;
			}
		}

		// Token: 0x0600001E RID: 30 RVA: 0x0000251A File Offset: 0x0000071A
		public virtual void Dispose()
		{
			if (this.platform != null)
			{
				this.platform.Dispose();
				this.platform = null;
			}
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002536 File Offset: 0x00000736
		public HSteamPipe SteamAPI_GetHSteamPipe()
		{
			return this.platform.SteamApi_SteamAPI_GetHSteamPipe();
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00002543 File Offset: 0x00000743
		public HSteamUser SteamAPI_GetHSteamUser()
		{
			return this.platform.SteamApi_SteamAPI_GetHSteamUser();
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002550 File Offset: 0x00000750
		public bool SteamAPI_Init()
		{
			return this.platform.SteamApi_SteamAPI_Init();
		}

		// Token: 0x06000022 RID: 34 RVA: 0x0000255D File Offset: 0x0000075D
		public void SteamAPI_RegisterCallback(IntPtr pCallback, int callback)
		{
			this.platform.SteamApi_SteamAPI_RegisterCallback(pCallback, callback);
		}

		// Token: 0x06000023 RID: 35 RVA: 0x0000256C File Offset: 0x0000076C
		public void SteamAPI_RegisterCallResult(IntPtr pCallback, SteamAPICall_t callback)
		{
			this.platform.SteamApi_SteamAPI_RegisterCallResult(pCallback, callback.Value);
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002580 File Offset: 0x00000780
		public bool SteamAPI_RestartAppIfNecessary(uint unOwnAppID)
		{
			return this.platform.SteamApi_SteamAPI_RestartAppIfNecessary(unOwnAppID);
		}

		// Token: 0x06000025 RID: 37 RVA: 0x0000258E File Offset: 0x0000078E
		public void SteamAPI_RunCallbacks()
		{
			this.platform.SteamApi_SteamAPI_RunCallbacks();
		}

		// Token: 0x06000026 RID: 38 RVA: 0x0000259B File Offset: 0x0000079B
		public void SteamAPI_Shutdown()
		{
			this.platform.SteamApi_SteamAPI_Shutdown();
		}

		// Token: 0x06000027 RID: 39 RVA: 0x000025A8 File Offset: 0x000007A8
		public void SteamAPI_UnregisterCallback(IntPtr pCallback)
		{
			this.platform.SteamApi_SteamAPI_UnregisterCallback(pCallback);
		}

		// Token: 0x06000028 RID: 40 RVA: 0x000025B6 File Offset: 0x000007B6
		public void SteamAPI_UnregisterCallResult(IntPtr pCallback, SteamAPICall_t callback)
		{
			this.platform.SteamApi_SteamAPI_UnregisterCallResult(pCallback, callback.Value);
		}

		// Token: 0x06000029 RID: 41 RVA: 0x000025CA File Offset: 0x000007CA
		public HSteamPipe SteamGameServer_GetHSteamPipe()
		{
			return this.platform.SteamApi_SteamGameServer_GetHSteamPipe();
		}

		// Token: 0x0600002A RID: 42 RVA: 0x000025D7 File Offset: 0x000007D7
		public HSteamUser SteamGameServer_GetHSteamUser()
		{
			return this.platform.SteamApi_SteamGameServer_GetHSteamUser();
		}

		// Token: 0x0600002B RID: 43 RVA: 0x000025E4 File Offset: 0x000007E4
		public void SteamGameServer_RunCallbacks()
		{
			this.platform.SteamApi_SteamGameServer_RunCallbacks();
		}

		// Token: 0x0600002C RID: 44 RVA: 0x000025F1 File Offset: 0x000007F1
		public void SteamGameServer_Shutdown()
		{
			this.platform.SteamApi_SteamGameServer_Shutdown();
		}

		// Token: 0x0600002D RID: 45 RVA: 0x000025FE File Offset: 0x000007FE
		public IntPtr SteamInternal_CreateInterface(string version)
		{
			return this.platform.SteamApi_SteamInternal_CreateInterface(version);
		}

		// Token: 0x0600002E RID: 46 RVA: 0x0000260C File Offset: 0x0000080C
		public bool SteamInternal_GameServer_Init(uint unIP, ushort usPort, ushort usGamePort, ushort usQueryPort, int eServerMode, string pchVersionString)
		{
			return this.platform.SteamApi_SteamInternal_GameServer_Init(unIP, usPort, usGamePort, usQueryPort, eServerMode, pchVersionString);
		}

		// Token: 0x04000461 RID: 1121
		internal Platform.Interface platform;
	}
}
