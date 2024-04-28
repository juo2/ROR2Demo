using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x02000122 RID: 290
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct SessionModificationSetHostAddressOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x170001E5 RID: 485
		// (set) Token: 0x06000850 RID: 2128 RVA: 0x000090E3 File Offset: 0x000072E3
		public string HostAddress
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_HostAddress, value);
			}
		}

		// Token: 0x06000851 RID: 2129 RVA: 0x000090F2 File Offset: 0x000072F2
		public void Set(SessionModificationSetHostAddressOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.HostAddress = other.HostAddress;
			}
		}

		// Token: 0x06000852 RID: 2130 RVA: 0x0000910A File Offset: 0x0000730A
		public void Set(object other)
		{
			this.Set(other as SessionModificationSetHostAddressOptions);
		}

		// Token: 0x06000853 RID: 2131 RVA: 0x00009118 File Offset: 0x00007318
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_HostAddress);
		}

		// Token: 0x040003F8 RID: 1016
		private int m_ApiVersion;

		// Token: 0x040003F9 RID: 1017
		private IntPtr m_HostAddress;
	}
}
