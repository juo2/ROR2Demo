using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x02000307 RID: 775
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct CopyLobbyDetailsHandleOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x17000596 RID: 1430
		// (set) Token: 0x0600134D RID: 4941 RVA: 0x00014952 File Offset: 0x00012B52
		public string LobbyId
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_LobbyId, value);
			}
		}

		// Token: 0x17000597 RID: 1431
		// (set) Token: 0x0600134E RID: 4942 RVA: 0x00014961 File Offset: 0x00012B61
		public ProductUserId LocalUserId
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_LocalUserId, value);
			}
		}

		// Token: 0x0600134F RID: 4943 RVA: 0x00014970 File Offset: 0x00012B70
		public void Set(CopyLobbyDetailsHandleOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.LobbyId = other.LobbyId;
				this.LocalUserId = other.LocalUserId;
			}
		}

		// Token: 0x06001350 RID: 4944 RVA: 0x00014994 File Offset: 0x00012B94
		public void Set(object other)
		{
			this.Set(other as CopyLobbyDetailsHandleOptions);
		}

		// Token: 0x06001351 RID: 4945 RVA: 0x000149A2 File Offset: 0x00012BA2
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_LobbyId);
			Helper.TryMarshalDispose(ref this.m_LocalUserId);
		}

		// Token: 0x0400092E RID: 2350
		private int m_ApiVersion;

		// Token: 0x0400092F RID: 2351
		private IntPtr m_LobbyId;

		// Token: 0x04000930 RID: 2352
		private IntPtr m_LocalUserId;
	}
}
