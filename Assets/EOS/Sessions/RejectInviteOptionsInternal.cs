using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x02000101 RID: 257
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct RejectInviteOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x1700019F RID: 415
		// (set) Token: 0x06000789 RID: 1929 RVA: 0x00008266 File Offset: 0x00006466
		public ProductUserId LocalUserId
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_LocalUserId, value);
			}
		}

		// Token: 0x170001A0 RID: 416
		// (set) Token: 0x0600078A RID: 1930 RVA: 0x00008275 File Offset: 0x00006475
		public string InviteId
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_InviteId, value);
			}
		}

		// Token: 0x0600078B RID: 1931 RVA: 0x00008284 File Offset: 0x00006484
		public void Set(RejectInviteOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.LocalUserId = other.LocalUserId;
				this.InviteId = other.InviteId;
			}
		}

		// Token: 0x0600078C RID: 1932 RVA: 0x000082A8 File Offset: 0x000064A8
		public void Set(object other)
		{
			this.Set(other as RejectInviteOptions);
		}

		// Token: 0x0600078D RID: 1933 RVA: 0x000082B6 File Offset: 0x000064B6
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_LocalUserId);
			Helper.TryMarshalDispose(ref this.m_InviteId);
		}

		// Token: 0x04000393 RID: 915
		private int m_ApiVersion;

		// Token: 0x04000394 RID: 916
		private IntPtr m_LocalUserId;

		// Token: 0x04000395 RID: 917
		private IntPtr m_InviteId;
	}
}
