using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Auth
{
	// Token: 0x0200054F RID: 1359
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct IOSCredentialsInternal : ISettable, IDisposable
	{
		// Token: 0x17000A2E RID: 2606
		// (get) Token: 0x06002103 RID: 8451 RVA: 0x00022C80 File Offset: 0x00020E80
		// (set) Token: 0x06002104 RID: 8452 RVA: 0x00022C9C File Offset: 0x00020E9C
		public string Id
		{
			get
			{
				string result;
				Helper.TryMarshalGet(this.m_Id, out result);
				return result;
			}
			set
			{
				Helper.TryMarshalSet(ref this.m_Id, value);
			}
		}

		// Token: 0x17000A2F RID: 2607
		// (get) Token: 0x06002105 RID: 8453 RVA: 0x00022CAC File Offset: 0x00020EAC
		// (set) Token: 0x06002106 RID: 8454 RVA: 0x00022CC8 File Offset: 0x00020EC8
		public string Token
		{
			get
			{
				string result;
				Helper.TryMarshalGet(this.m_Token, out result);
				return result;
			}
			set
			{
				Helper.TryMarshalSet(ref this.m_Token, value);
			}
		}

		// Token: 0x17000A30 RID: 2608
		// (get) Token: 0x06002107 RID: 8455 RVA: 0x00022CD7 File Offset: 0x00020ED7
		// (set) Token: 0x06002108 RID: 8456 RVA: 0x00022CDF File Offset: 0x00020EDF
		public LoginCredentialType Type
		{
			get
			{
				return this.m_Type;
			}
			set
			{
				this.m_Type = value;
			}
		}

		// Token: 0x17000A31 RID: 2609
		// (get) Token: 0x06002109 RID: 8457 RVA: 0x00022CE8 File Offset: 0x00020EE8
		// (set) Token: 0x0600210A RID: 8458 RVA: 0x00022D04 File Offset: 0x00020F04
		public IOSCredentialsSystemAuthCredentialsOptions SystemAuthCredentialsOptions
		{
			get
			{
				IOSCredentialsSystemAuthCredentialsOptions result;
				Helper.TryMarshalGet<IOSCredentialsSystemAuthCredentialsOptionsInternal, IOSCredentialsSystemAuthCredentialsOptions>(this.m_SystemAuthCredentialsOptions, out result);
				return result;
			}
			set
			{
				Helper.TryMarshalSet<IOSCredentialsSystemAuthCredentialsOptionsInternal, IOSCredentialsSystemAuthCredentialsOptions>(ref this.m_SystemAuthCredentialsOptions, value);
			}
		}

		// Token: 0x17000A32 RID: 2610
		// (get) Token: 0x0600210B RID: 8459 RVA: 0x00022D13 File Offset: 0x00020F13
		// (set) Token: 0x0600210C RID: 8460 RVA: 0x00022D1B File Offset: 0x00020F1B
		public ExternalCredentialType ExternalType
		{
			get
			{
				return this.m_ExternalType;
			}
			set
			{
				this.m_ExternalType = value;
			}
		}

		// Token: 0x0600210D RID: 8461 RVA: 0x00022D24 File Offset: 0x00020F24
		public void Set(IOSCredentials other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 3;
				this.Id = other.Id;
				this.Token = other.Token;
				this.Type = other.Type;
				this.SystemAuthCredentialsOptions = other.SystemAuthCredentialsOptions;
				this.ExternalType = other.ExternalType;
			}
		}

		// Token: 0x0600210E RID: 8462 RVA: 0x00022D77 File Offset: 0x00020F77
		public void Set(object other)
		{
			this.Set(other as IOSCredentials);
		}

		// Token: 0x0600210F RID: 8463 RVA: 0x00022D85 File Offset: 0x00020F85
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_Id);
			Helper.TryMarshalDispose(ref this.m_Token);
			Helper.TryMarshalDispose(ref this.m_SystemAuthCredentialsOptions);
		}

		// Token: 0x04000F38 RID: 3896
		private int m_ApiVersion;

		// Token: 0x04000F39 RID: 3897
		private IntPtr m_Id;

		// Token: 0x04000F3A RID: 3898
		private IntPtr m_Token;

		// Token: 0x04000F3B RID: 3899
		private LoginCredentialType m_Type;

		// Token: 0x04000F3C RID: 3900
		private IntPtr m_SystemAuthCredentialsOptions;

		// Token: 0x04000F3D RID: 3901
		private ExternalCredentialType m_ExternalType;
	}
}
