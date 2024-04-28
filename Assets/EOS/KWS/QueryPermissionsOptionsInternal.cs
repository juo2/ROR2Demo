using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.KWS
{
	// Token: 0x02000402 RID: 1026
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct QueryPermissionsOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x1700074A RID: 1866
		// (set) Token: 0x060018BF RID: 6335 RVA: 0x0001A0FD File Offset: 0x000182FD
		public ProductUserId LocalUserId
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_LocalUserId, value);
			}
		}

		// Token: 0x060018C0 RID: 6336 RVA: 0x0001A10C File Offset: 0x0001830C
		public void Set(QueryPermissionsOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.LocalUserId = other.LocalUserId;
			}
		}

		// Token: 0x060018C1 RID: 6337 RVA: 0x0001A124 File Offset: 0x00018324
		public void Set(object other)
		{
			this.Set(other as QueryPermissionsOptions);
		}

		// Token: 0x060018C2 RID: 6338 RVA: 0x0001A132 File Offset: 0x00018332
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_LocalUserId);
		}

		// Token: 0x04000B86 RID: 2950
		private int m_ApiVersion;

		// Token: 0x04000B87 RID: 2951
		private IntPtr m_LocalUserId;
	}
}
