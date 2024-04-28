using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x02000303 RID: 771
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct CopyLobbyDetailsHandleByInviteIdOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x17000591 RID: 1425
		// (set) Token: 0x0600133D RID: 4925 RVA: 0x000148AD File Offset: 0x00012AAD
		public string InviteId
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_InviteId, value);
			}
		}

		// Token: 0x0600133E RID: 4926 RVA: 0x000148BC File Offset: 0x00012ABC
		public void Set(CopyLobbyDetailsHandleByInviteIdOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.InviteId = other.InviteId;
			}
		}

		// Token: 0x0600133F RID: 4927 RVA: 0x000148D4 File Offset: 0x00012AD4
		public void Set(object other)
		{
			this.Set(other as CopyLobbyDetailsHandleByInviteIdOptions);
		}

		// Token: 0x06001340 RID: 4928 RVA: 0x000148E2 File Offset: 0x00012AE2
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_InviteId);
		}

		// Token: 0x04000927 RID: 2343
		private int m_ApiVersion;

		// Token: 0x04000928 RID: 2344
		private IntPtr m_InviteId;
	}
}
