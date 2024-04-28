using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x020003A4 RID: 932
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct RejectInviteOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x170006A4 RID: 1700
		// (set) Token: 0x060016B7 RID: 5815 RVA: 0x00018086 File Offset: 0x00016286
		public string InviteId
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_InviteId, value);
			}
		}

		// Token: 0x170006A5 RID: 1701
		// (set) Token: 0x060016B8 RID: 5816 RVA: 0x00018095 File Offset: 0x00016295
		public ProductUserId LocalUserId
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_LocalUserId, value);
			}
		}

		// Token: 0x060016B9 RID: 5817 RVA: 0x000180A4 File Offset: 0x000162A4
		public void Set(RejectInviteOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.InviteId = other.InviteId;
				this.LocalUserId = other.LocalUserId;
			}
		}

		// Token: 0x060016BA RID: 5818 RVA: 0x000180C8 File Offset: 0x000162C8
		public void Set(object other)
		{
			this.Set(other as RejectInviteOptions);
		}

		// Token: 0x060016BB RID: 5819 RVA: 0x000180D6 File Offset: 0x000162D6
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_InviteId);
			Helper.TryMarshalDispose(ref this.m_LocalUserId);
		}

		// Token: 0x04000AA6 RID: 2726
		private int m_ApiVersion;

		// Token: 0x04000AA7 RID: 2727
		private IntPtr m_InviteId;

		// Token: 0x04000AA8 RID: 2728
		private IntPtr m_LocalUserId;
	}
}
