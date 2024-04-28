using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x02000131 RID: 305
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct SessionSearchFindOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x170001F6 RID: 502
		// (set) Token: 0x06000891 RID: 2193 RVA: 0x00009519 File Offset: 0x00007719
		public ProductUserId LocalUserId
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_LocalUserId, value);
			}
		}

		// Token: 0x06000892 RID: 2194 RVA: 0x00009528 File Offset: 0x00007728
		public void Set(SessionSearchFindOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 2;
				this.LocalUserId = other.LocalUserId;
			}
		}

		// Token: 0x06000893 RID: 2195 RVA: 0x00009540 File Offset: 0x00007740
		public void Set(object other)
		{
			this.Set(other as SessionSearchFindOptions);
		}

		// Token: 0x06000894 RID: 2196 RVA: 0x0000954E File Offset: 0x0000774E
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_LocalUserId);
		}

		// Token: 0x04000416 RID: 1046
		private int m_ApiVersion;

		// Token: 0x04000417 RID: 1047
		private IntPtr m_LocalUserId;
	}
}
