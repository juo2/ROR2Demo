using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x0200039E RID: 926
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct QueryInvitesOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x1700068F RID: 1679
		// (set) Token: 0x0600168C RID: 5772 RVA: 0x00017DAD File Offset: 0x00015FAD
		public ProductUserId LocalUserId
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_LocalUserId, value);
			}
		}

		// Token: 0x0600168D RID: 5773 RVA: 0x00017DBC File Offset: 0x00015FBC
		public void Set(QueryInvitesOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.LocalUserId = other.LocalUserId;
			}
		}

		// Token: 0x0600168E RID: 5774 RVA: 0x00017DD4 File Offset: 0x00015FD4
		public void Set(object other)
		{
			this.Set(other as QueryInvitesOptions);
		}

		// Token: 0x0600168F RID: 5775 RVA: 0x00017DE2 File Offset: 0x00015FE2
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_LocalUserId);
		}

		// Token: 0x04000A92 RID: 2706
		private int m_ApiVersion;

		// Token: 0x04000A93 RID: 2707
		private IntPtr m_LocalUserId;
	}
}
