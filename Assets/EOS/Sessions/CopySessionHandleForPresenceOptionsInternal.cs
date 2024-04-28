using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x020000BF RID: 191
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct CopySessionHandleForPresenceOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x17000143 RID: 323
		// (set) Token: 0x0600064E RID: 1614 RVA: 0x00007619 File Offset: 0x00005819
		public ProductUserId LocalUserId
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_LocalUserId, value);
			}
		}

		// Token: 0x0600064F RID: 1615 RVA: 0x00007628 File Offset: 0x00005828
		public void Set(CopySessionHandleForPresenceOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.LocalUserId = other.LocalUserId;
			}
		}

		// Token: 0x06000650 RID: 1616 RVA: 0x00007640 File Offset: 0x00005840
		public void Set(object other)
		{
			this.Set(other as CopySessionHandleForPresenceOptions);
		}

		// Token: 0x06000651 RID: 1617 RVA: 0x0000764E File Offset: 0x0000584E
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_LocalUserId);
		}

		// Token: 0x04000322 RID: 802
		private int m_ApiVersion;

		// Token: 0x04000323 RID: 803
		private IntPtr m_LocalUserId;
	}
}
