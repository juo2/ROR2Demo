using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.UI
{
	// Token: 0x02000061 RID: 97
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct ShowFriendsOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x1700008E RID: 142
		// (set) Token: 0x0600041A RID: 1050 RVA: 0x00004FBD File Offset: 0x000031BD
		public EpicAccountId LocalUserId
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_LocalUserId, value);
			}
		}

		// Token: 0x0600041B RID: 1051 RVA: 0x00004FCC File Offset: 0x000031CC
		public void Set(ShowFriendsOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.LocalUserId = other.LocalUserId;
			}
		}

		// Token: 0x0600041C RID: 1052 RVA: 0x00004FE4 File Offset: 0x000031E4
		public void Set(object other)
		{
			this.Set(other as ShowFriendsOptions);
		}

		// Token: 0x0600041D RID: 1053 RVA: 0x00004FF2 File Offset: 0x000031F2
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_LocalUserId);
		}

		// Token: 0x0400022B RID: 555
		private int m_ApiVersion;

		// Token: 0x0400022C RID: 556
		private IntPtr m_LocalUserId;
	}
}
