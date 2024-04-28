using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Platform
{
	// Token: 0x020005EC RID: 1516
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct WindowsOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x17000B97 RID: 2967
		// (set) Token: 0x06002502 RID: 9474 RVA: 0x0002726C File Offset: 0x0002546C
		public IntPtr Reserved
		{
			set
			{
				this.m_Reserved = value;
			}
		}

		// Token: 0x17000B98 RID: 2968
		// (set) Token: 0x06002503 RID: 9475 RVA: 0x00027275 File Offset: 0x00025475
		public string ProductId
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_ProductId, value);
			}
		}

		// Token: 0x17000B99 RID: 2969
		// (set) Token: 0x06002504 RID: 9476 RVA: 0x00027284 File Offset: 0x00025484
		public string SandboxId
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_SandboxId, value);
			}
		}

		// Token: 0x17000B9A RID: 2970
		// (set) Token: 0x06002505 RID: 9477 RVA: 0x00027293 File Offset: 0x00025493
		public ClientCredentials ClientCredentials
		{
			set
			{
				Helper.TryMarshalSet<ClientCredentialsInternal>(ref this.m_ClientCredentials, value);
			}
		}

		// Token: 0x17000B9B RID: 2971
		// (set) Token: 0x06002506 RID: 9478 RVA: 0x000272A2 File Offset: 0x000254A2
		public bool IsServer
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_IsServer, value);
			}
		}

		// Token: 0x17000B9C RID: 2972
		// (set) Token: 0x06002507 RID: 9479 RVA: 0x000272B1 File Offset: 0x000254B1
		public string EncryptionKey
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_EncryptionKey, value);
			}
		}

		// Token: 0x17000B9D RID: 2973
		// (set) Token: 0x06002508 RID: 9480 RVA: 0x000272C0 File Offset: 0x000254C0
		public string OverrideCountryCode
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_OverrideCountryCode, value);
			}
		}

		// Token: 0x17000B9E RID: 2974
		// (set) Token: 0x06002509 RID: 9481 RVA: 0x000272CF File Offset: 0x000254CF
		public string OverrideLocaleCode
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_OverrideLocaleCode, value);
			}
		}

		// Token: 0x17000B9F RID: 2975
		// (set) Token: 0x0600250A RID: 9482 RVA: 0x000272DE File Offset: 0x000254DE
		public string DeploymentId
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_DeploymentId, value);
			}
		}

		// Token: 0x17000BA0 RID: 2976
		// (set) Token: 0x0600250B RID: 9483 RVA: 0x000272ED File Offset: 0x000254ED
		public PlatformFlags Flags
		{
			set
			{
				this.m_Flags = value;
			}
		}

		// Token: 0x17000BA1 RID: 2977
		// (set) Token: 0x0600250C RID: 9484 RVA: 0x000272F6 File Offset: 0x000254F6
		public string CacheDirectory
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_CacheDirectory, value);
			}
		}

		// Token: 0x17000BA2 RID: 2978
		// (set) Token: 0x0600250D RID: 9485 RVA: 0x00027305 File Offset: 0x00025505
		public uint TickBudgetInMilliseconds
		{
			set
			{
				this.m_TickBudgetInMilliseconds = value;
			}
		}

		// Token: 0x17000BA3 RID: 2979
		// (set) Token: 0x0600250E RID: 9486 RVA: 0x0002730E File Offset: 0x0002550E
		public WindowsRTCOptions RTCOptions
		{
			set
			{
				Helper.TryMarshalSet<WindowsRTCOptionsInternal, WindowsRTCOptions>(ref this.m_RTCOptions, value);
			}
		}

		// Token: 0x0600250F RID: 9487 RVA: 0x00027320 File Offset: 0x00025520
		public void Set(WindowsOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 11;
				this.Reserved = other.Reserved;
				this.ProductId = other.ProductId;
				this.SandboxId = other.SandboxId;
				this.ClientCredentials = other.ClientCredentials;
				this.IsServer = other.IsServer;
				this.EncryptionKey = other.EncryptionKey;
				this.OverrideCountryCode = other.OverrideCountryCode;
				this.OverrideLocaleCode = other.OverrideLocaleCode;
				this.DeploymentId = other.DeploymentId;
				this.Flags = other.Flags;
				this.CacheDirectory = other.CacheDirectory;
				this.TickBudgetInMilliseconds = other.TickBudgetInMilliseconds;
				this.RTCOptions = other.RTCOptions;
			}
		}

		// Token: 0x06002510 RID: 9488 RVA: 0x000273D7 File Offset: 0x000255D7
		public void Set(object other)
		{
			this.Set(other as WindowsOptions);
		}

		// Token: 0x06002511 RID: 9489 RVA: 0x000273E8 File Offset: 0x000255E8
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_Reserved);
			Helper.TryMarshalDispose(ref this.m_ProductId);
			Helper.TryMarshalDispose(ref this.m_SandboxId);
			Helper.TryMarshalDispose<ClientCredentialsInternal>(ref this.m_ClientCredentials);
			Helper.TryMarshalDispose(ref this.m_EncryptionKey);
			Helper.TryMarshalDispose(ref this.m_OverrideCountryCode);
			Helper.TryMarshalDispose(ref this.m_OverrideLocaleCode);
			Helper.TryMarshalDispose(ref this.m_DeploymentId);
			Helper.TryMarshalDispose(ref this.m_CacheDirectory);
			Helper.TryMarshalDispose(ref this.m_RTCOptions);
		}

		// Token: 0x04001180 RID: 4480
		private int m_ApiVersion;

		// Token: 0x04001181 RID: 4481
		private IntPtr m_Reserved;

		// Token: 0x04001182 RID: 4482
		private IntPtr m_ProductId;

		// Token: 0x04001183 RID: 4483
		private IntPtr m_SandboxId;

		// Token: 0x04001184 RID: 4484
		private ClientCredentialsInternal m_ClientCredentials;

		// Token: 0x04001185 RID: 4485
		private int m_IsServer;

		// Token: 0x04001186 RID: 4486
		private IntPtr m_EncryptionKey;

		// Token: 0x04001187 RID: 4487
		private IntPtr m_OverrideCountryCode;

		// Token: 0x04001188 RID: 4488
		private IntPtr m_OverrideLocaleCode;

		// Token: 0x04001189 RID: 4489
		private IntPtr m_DeploymentId;

		// Token: 0x0400118A RID: 4490
		private PlatformFlags m_Flags;

		// Token: 0x0400118B RID: 4491
		private IntPtr m_CacheDirectory;

		// Token: 0x0400118C RID: 4492
		private uint m_TickBudgetInMilliseconds;

		// Token: 0x0400118D RID: 4493
		private IntPtr m_RTCOptions;
	}
}
