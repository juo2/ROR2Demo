using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x0200013D RID: 317
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct SessionSearchSetSessionIdOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x17000202 RID: 514
		// (set) Token: 0x060008BF RID: 2239 RVA: 0x000096BB File Offset: 0x000078BB
		public string SessionId
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_SessionId, value);
			}
		}

		// Token: 0x060008C0 RID: 2240 RVA: 0x000096CA File Offset: 0x000078CA
		public void Set(SessionSearchSetSessionIdOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.SessionId = other.SessionId;
			}
		}

		// Token: 0x060008C1 RID: 2241 RVA: 0x000096E2 File Offset: 0x000078E2
		public void Set(object other)
		{
			this.Set(other as SessionSearchSetSessionIdOptions);
		}

		// Token: 0x060008C2 RID: 2242 RVA: 0x000096F0 File Offset: 0x000078F0
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_SessionId);
		}

		// Token: 0x04000427 RID: 1063
		private int m_ApiVersion;

		// Token: 0x04000428 RID: 1064
		private IntPtr m_SessionId;
	}
}
