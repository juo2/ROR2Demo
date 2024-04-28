using System;
using System.Runtime.InteropServices;
using Epic.OnlineServices.AntiCheatCommon;

namespace Epic.OnlineServices.AntiCheatClient
{
	// Token: 0x020005D3 RID: 1491
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct RegisterPeerOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x17000B31 RID: 2865
		// (set) Token: 0x060023F4 RID: 9204 RVA: 0x00025EA9 File Offset: 0x000240A9
		public IntPtr PeerHandle
		{
			set
			{
				this.m_PeerHandle = value;
			}
		}

		// Token: 0x17000B32 RID: 2866
		// (set) Token: 0x060023F5 RID: 9205 RVA: 0x00025EB2 File Offset: 0x000240B2
		public AntiCheatCommonClientType ClientType
		{
			set
			{
				this.m_ClientType = value;
			}
		}

		// Token: 0x17000B33 RID: 2867
		// (set) Token: 0x060023F6 RID: 9206 RVA: 0x00025EBB File Offset: 0x000240BB
		public AntiCheatCommonClientPlatform ClientPlatform
		{
			set
			{
				this.m_ClientPlatform = value;
			}
		}

		// Token: 0x17000B34 RID: 2868
		// (set) Token: 0x060023F7 RID: 9207 RVA: 0x00025EC4 File Offset: 0x000240C4
		public string AccountId
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_AccountId, value);
			}
		}

		// Token: 0x17000B35 RID: 2869
		// (set) Token: 0x060023F8 RID: 9208 RVA: 0x00025ED3 File Offset: 0x000240D3
		public string IpAddress
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_IpAddress, value);
			}
		}

		// Token: 0x060023F9 RID: 9209 RVA: 0x00025EE4 File Offset: 0x000240E4
		public void Set(RegisterPeerOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.PeerHandle = other.PeerHandle;
				this.ClientType = other.ClientType;
				this.ClientPlatform = other.ClientPlatform;
				this.AccountId = other.AccountId;
				this.IpAddress = other.IpAddress;
			}
		}

		// Token: 0x060023FA RID: 9210 RVA: 0x00025F37 File Offset: 0x00024137
		public void Set(object other)
		{
			this.Set(other as RegisterPeerOptions);
		}

		// Token: 0x060023FB RID: 9211 RVA: 0x00025F45 File Offset: 0x00024145
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_PeerHandle);
			Helper.TryMarshalDispose(ref this.m_AccountId);
			Helper.TryMarshalDispose(ref this.m_IpAddress);
		}

		// Token: 0x040010FC RID: 4348
		private int m_ApiVersion;

		// Token: 0x040010FD RID: 4349
		private IntPtr m_PeerHandle;

		// Token: 0x040010FE RID: 4350
		private AntiCheatCommonClientType m_ClientType;

		// Token: 0x040010FF RID: 4351
		private AntiCheatCommonClientPlatform m_ClientPlatform;

		// Token: 0x04001100 RID: 4352
		private IntPtr m_AccountId;

		// Token: 0x04001101 RID: 4353
		private IntPtr m_IpAddress;
	}
}
