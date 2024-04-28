using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Platform
{
	// Token: 0x020005E5 RID: 1509
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct OptionsInternal : ISettable, IDisposable
	{
		// Token: 0x17000B7B RID: 2939
		// (set) Token: 0x060024C5 RID: 9413 RVA: 0x00026EFA File Offset: 0x000250FA
		public IntPtr Reserved
		{
			set
			{
				this.m_Reserved = value;
			}
		}

		// Token: 0x17000B7C RID: 2940
		// (set) Token: 0x060024C6 RID: 9414 RVA: 0x00026F03 File Offset: 0x00025103
		public string ProductId
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_ProductId, value);
			}
		}

		// Token: 0x17000B7D RID: 2941
		// (set) Token: 0x060024C7 RID: 9415 RVA: 0x00026F12 File Offset: 0x00025112
		public string SandboxId
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_SandboxId, value);
			}
		}

		// Token: 0x17000B7E RID: 2942
		// (set) Token: 0x060024C8 RID: 9416 RVA: 0x00026F21 File Offset: 0x00025121
		public ClientCredentials ClientCredentials
		{
			set
			{
				Helper.TryMarshalSet<ClientCredentialsInternal>(ref this.m_ClientCredentials, value);
			}
		}

		// Token: 0x17000B7F RID: 2943
		// (set) Token: 0x060024C9 RID: 9417 RVA: 0x00026F30 File Offset: 0x00025130
		public bool IsServer
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_IsServer, value);
			}
		}

		// Token: 0x17000B80 RID: 2944
		// (set) Token: 0x060024CA RID: 9418 RVA: 0x00026F3F File Offset: 0x0002513F
		public string EncryptionKey
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_EncryptionKey, value);
			}
		}

		// Token: 0x17000B81 RID: 2945
		// (set) Token: 0x060024CB RID: 9419 RVA: 0x00026F4E File Offset: 0x0002514E
		public string OverrideCountryCode
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_OverrideCountryCode, value);
			}
		}

		// Token: 0x17000B82 RID: 2946
		// (set) Token: 0x060024CC RID: 9420 RVA: 0x00026F5D File Offset: 0x0002515D
		public string OverrideLocaleCode
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_OverrideLocaleCode, value);
			}
		}

		// Token: 0x17000B83 RID: 2947
		// (set) Token: 0x060024CD RID: 9421 RVA: 0x00026F6C File Offset: 0x0002516C
		public string DeploymentId
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_DeploymentId, value);
			}
		}

		// Token: 0x17000B84 RID: 2948
		// (set) Token: 0x060024CE RID: 9422 RVA: 0x00026F7B File Offset: 0x0002517B
		public PlatformFlags Flags
		{
			set
			{
				this.m_Flags = value;
			}
		}

		// Token: 0x17000B85 RID: 2949
		// (set) Token: 0x060024CF RID: 9423 RVA: 0x00026F84 File Offset: 0x00025184
		public string CacheDirectory
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_CacheDirectory, value);
			}
		}

		// Token: 0x17000B86 RID: 2950
		// (set) Token: 0x060024D0 RID: 9424 RVA: 0x00026F93 File Offset: 0x00025193
		public uint TickBudgetInMilliseconds
		{
			set
			{
				this.m_TickBudgetInMilliseconds = value;
			}
		}

		// Token: 0x17000B87 RID: 2951
		// (set) Token: 0x060024D1 RID: 9425 RVA: 0x00026F9C File Offset: 0x0002519C
		public RTCOptions RTCOptions
		{
			set
			{
				Helper.TryMarshalSet<RTCOptionsInternal, RTCOptions>(ref this.m_RTCOptions, value);
			}
		}

		// Token: 0x060024D2 RID: 9426 RVA: 0x00026FAC File Offset: 0x000251AC
		public void Set(Options other)
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

		// Token: 0x060024D3 RID: 9427 RVA: 0x00027063 File Offset: 0x00025263
		public void Set(object other)
		{
			this.Set(other as Options);
		}

		// Token: 0x060024D4 RID: 9428 RVA: 0x00027074 File Offset: 0x00025274
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

		// Token: 0x04001159 RID: 4441
		private int m_ApiVersion;

		// Token: 0x0400115A RID: 4442
		private IntPtr m_Reserved;

		// Token: 0x0400115B RID: 4443
		private IntPtr m_ProductId;

		// Token: 0x0400115C RID: 4444
		private IntPtr m_SandboxId;

		// Token: 0x0400115D RID: 4445
		private ClientCredentialsInternal m_ClientCredentials;

		// Token: 0x0400115E RID: 4446
		private int m_IsServer;

		// Token: 0x0400115F RID: 4447
		private IntPtr m_EncryptionKey;

		// Token: 0x04001160 RID: 4448
		private IntPtr m_OverrideCountryCode;

		// Token: 0x04001161 RID: 4449
		private IntPtr m_OverrideLocaleCode;

		// Token: 0x04001162 RID: 4450
		private IntPtr m_DeploymentId;

		// Token: 0x04001163 RID: 4451
		private PlatformFlags m_Flags;

		// Token: 0x04001164 RID: 4452
		private IntPtr m_CacheDirectory;

		// Token: 0x04001165 RID: 4453
		private uint m_TickBudgetInMilliseconds;

		// Token: 0x04001166 RID: 4454
		private IntPtr m_RTCOptions;
	}
}
