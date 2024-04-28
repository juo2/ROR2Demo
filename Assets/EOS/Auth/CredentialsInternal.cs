using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Auth
{
	// Token: 0x02000517 RID: 1303
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct CredentialsInternal : ISettable, IDisposable
	{
		// Token: 0x17000996 RID: 2454
		// (get) Token: 0x06001F7A RID: 8058 RVA: 0x000214E4 File Offset: 0x0001F6E4
		// (set) Token: 0x06001F7B RID: 8059 RVA: 0x00021500 File Offset: 0x0001F700
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

		// Token: 0x17000997 RID: 2455
		// (get) Token: 0x06001F7C RID: 8060 RVA: 0x00021510 File Offset: 0x0001F710
		// (set) Token: 0x06001F7D RID: 8061 RVA: 0x0002152C File Offset: 0x0001F72C
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

		// Token: 0x17000998 RID: 2456
		// (get) Token: 0x06001F7E RID: 8062 RVA: 0x0002153B File Offset: 0x0001F73B
		// (set) Token: 0x06001F7F RID: 8063 RVA: 0x00021543 File Offset: 0x0001F743
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

		// Token: 0x17000999 RID: 2457
		// (get) Token: 0x06001F80 RID: 8064 RVA: 0x0002154C File Offset: 0x0001F74C
		// (set) Token: 0x06001F81 RID: 8065 RVA: 0x00021554 File Offset: 0x0001F754
		public IntPtr SystemAuthCredentialsOptions
		{
			get
			{
				return this.m_SystemAuthCredentialsOptions;
			}
			set
			{
				this.m_SystemAuthCredentialsOptions = value;
			}
		}

		// Token: 0x1700099A RID: 2458
		// (get) Token: 0x06001F82 RID: 8066 RVA: 0x0002155D File Offset: 0x0001F75D
		// (set) Token: 0x06001F83 RID: 8067 RVA: 0x00021565 File Offset: 0x0001F765
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

		// Token: 0x06001F84 RID: 8068 RVA: 0x00021570 File Offset: 0x0001F770
		public void Set(Credentials other)
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

		// Token: 0x06001F85 RID: 8069 RVA: 0x000215C3 File Offset: 0x0001F7C3
		public void Set(object other)
		{
			this.Set(other as Credentials);
		}

		// Token: 0x06001F86 RID: 8070 RVA: 0x000215D1 File Offset: 0x0001F7D1
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_Id);
			Helper.TryMarshalDispose(ref this.m_Token);
			Helper.TryMarshalDispose(ref this.m_SystemAuthCredentialsOptions);
		}

		// Token: 0x04000E91 RID: 3729
		private int m_ApiVersion;

		// Token: 0x04000E92 RID: 3730
		private IntPtr m_Id;

		// Token: 0x04000E93 RID: 3731
		private IntPtr m_Token;

		// Token: 0x04000E94 RID: 3732
		private LoginCredentialType m_Type;

		// Token: 0x04000E95 RID: 3733
		private IntPtr m_SystemAuthCredentialsOptions;

		// Token: 0x04000E96 RID: 3734
		private ExternalCredentialType m_ExternalType;
	}
}
