using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x020000F9 RID: 249
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct QueryInvitesOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x1700018A RID: 394
		// (set) Token: 0x0600075A RID: 1882 RVA: 0x00007F79 File Offset: 0x00006179
		public ProductUserId LocalUserId
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_LocalUserId, value);
			}
		}

		// Token: 0x0600075B RID: 1883 RVA: 0x00007F88 File Offset: 0x00006188
		public void Set(QueryInvitesOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.LocalUserId = other.LocalUserId;
			}
		}

		// Token: 0x0600075C RID: 1884 RVA: 0x00007FA0 File Offset: 0x000061A0
		public void Set(object other)
		{
			this.Set(other as QueryInvitesOptions);
		}

		// Token: 0x0600075D RID: 1885 RVA: 0x00007FAE File Offset: 0x000061AE
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_LocalUserId);
		}

		// Token: 0x0400037B RID: 891
		private int m_ApiVersion;

		// Token: 0x0400037C RID: 892
		private IntPtr m_LocalUserId;
	}
}
