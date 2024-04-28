using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x020000CF RID: 207
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct GetInviteCountOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x17000165 RID: 357
		// (set) Token: 0x060006A0 RID: 1696 RVA: 0x00007A91 File Offset: 0x00005C91
		public ProductUserId LocalUserId
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_LocalUserId, value);
			}
		}

		// Token: 0x060006A1 RID: 1697 RVA: 0x00007AA0 File Offset: 0x00005CA0
		public void Set(GetInviteCountOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.LocalUserId = other.LocalUserId;
			}
		}

		// Token: 0x060006A2 RID: 1698 RVA: 0x00007AB8 File Offset: 0x00005CB8
		public void Set(object other)
		{
			this.Set(other as GetInviteCountOptions);
		}

		// Token: 0x060006A3 RID: 1699 RVA: 0x00007AC6 File Offset: 0x00005CC6
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_LocalUserId);
		}

		// Token: 0x04000348 RID: 840
		private int m_ApiVersion;

		// Token: 0x04000349 RID: 841
		private IntPtr m_LocalUserId;
	}
}
