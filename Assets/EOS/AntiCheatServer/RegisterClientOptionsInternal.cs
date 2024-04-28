using System;
using System.Runtime.InteropServices;
using Epic.OnlineServices.AntiCheatCommon;

namespace Epic.OnlineServices.AntiCheatServer
{
	// Token: 0x0200056C RID: 1388
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct RegisterClientOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x17000A52 RID: 2642
		// (set) Token: 0x060021A6 RID: 8614 RVA: 0x00023813 File Offset: 0x00021A13
		public IntPtr ClientHandle
		{
			set
			{
				this.m_ClientHandle = value;
			}
		}

		// Token: 0x17000A53 RID: 2643
		// (set) Token: 0x060021A7 RID: 8615 RVA: 0x0002381C File Offset: 0x00021A1C
		public AntiCheatCommonClientType ClientType
		{
			set
			{
				this.m_ClientType = value;
			}
		}

		// Token: 0x17000A54 RID: 2644
		// (set) Token: 0x060021A8 RID: 8616 RVA: 0x00023825 File Offset: 0x00021A25
		public AntiCheatCommonClientPlatform ClientPlatform
		{
			set
			{
				this.m_ClientPlatform = value;
			}
		}

		// Token: 0x17000A55 RID: 2645
		// (set) Token: 0x060021A9 RID: 8617 RVA: 0x0002382E File Offset: 0x00021A2E
		public string AccountId
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_AccountId, value);
			}
		}

		// Token: 0x17000A56 RID: 2646
		// (set) Token: 0x060021AA RID: 8618 RVA: 0x0002383D File Offset: 0x00021A3D
		public string IpAddress
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_IpAddress, value);
			}
		}

		// Token: 0x060021AB RID: 8619 RVA: 0x0002384C File Offset: 0x00021A4C
		public void Set(RegisterClientOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.ClientHandle = other.ClientHandle;
				this.ClientType = other.ClientType;
				this.ClientPlatform = other.ClientPlatform;
				this.AccountId = other.AccountId;
				this.IpAddress = other.IpAddress;
			}
		}

		// Token: 0x060021AC RID: 8620 RVA: 0x0002389F File Offset: 0x00021A9F
		public void Set(object other)
		{
			this.Set(other as RegisterClientOptions);
		}

		// Token: 0x060021AD RID: 8621 RVA: 0x000238AD File Offset: 0x00021AAD
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_ClientHandle);
			Helper.TryMarshalDispose(ref this.m_AccountId);
			Helper.TryMarshalDispose(ref this.m_IpAddress);
		}

		// Token: 0x04000F77 RID: 3959
		private int m_ApiVersion;

		// Token: 0x04000F78 RID: 3960
		private IntPtr m_ClientHandle;

		// Token: 0x04000F79 RID: 3961
		private AntiCheatCommonClientType m_ClientType;

		// Token: 0x04000F7A RID: 3962
		private AntiCheatCommonClientPlatform m_ClientPlatform;

		// Token: 0x04000F7B RID: 3963
		private IntPtr m_AccountId;

		// Token: 0x04000F7C RID: 3964
		private IntPtr m_IpAddress;
	}
}
