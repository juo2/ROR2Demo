using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Friends
{
	// Token: 0x02000428 RID: 1064
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct QueryFriendsOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x1700078B RID: 1931
		// (set) Token: 0x0600198A RID: 6538 RVA: 0x0001ACED File Offset: 0x00018EED
		public EpicAccountId LocalUserId
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_LocalUserId, value);
			}
		}

		// Token: 0x0600198B RID: 6539 RVA: 0x0001ACFC File Offset: 0x00018EFC
		public void Set(QueryFriendsOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.LocalUserId = other.LocalUserId;
			}
		}

		// Token: 0x0600198C RID: 6540 RVA: 0x0001AD14 File Offset: 0x00018F14
		public void Set(object other)
		{
			this.Set(other as QueryFriendsOptions);
		}

		// Token: 0x0600198D RID: 6541 RVA: 0x0001AD22 File Offset: 0x00018F22
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_LocalUserId);
		}

		// Token: 0x04000BD8 RID: 3032
		private int m_ApiVersion;

		// Token: 0x04000BD9 RID: 3033
		private IntPtr m_LocalUserId;
	}
}
