using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x020003AC RID: 940
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct UpdateLobbyModificationOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x170006BC RID: 1724
		// (set) Token: 0x060016EA RID: 5866 RVA: 0x000183B6 File Offset: 0x000165B6
		public ProductUserId LocalUserId
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_LocalUserId, value);
			}
		}

		// Token: 0x170006BD RID: 1725
		// (set) Token: 0x060016EB RID: 5867 RVA: 0x000183C5 File Offset: 0x000165C5
		public string LobbyId
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_LobbyId, value);
			}
		}

		// Token: 0x060016EC RID: 5868 RVA: 0x000183D4 File Offset: 0x000165D4
		public void Set(UpdateLobbyModificationOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.LocalUserId = other.LocalUserId;
				this.LobbyId = other.LobbyId;
			}
		}

		// Token: 0x060016ED RID: 5869 RVA: 0x000183F8 File Offset: 0x000165F8
		public void Set(object other)
		{
			this.Set(other as UpdateLobbyModificationOptions);
		}

		// Token: 0x060016EE RID: 5870 RVA: 0x00018406 File Offset: 0x00016606
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_LocalUserId);
			Helper.TryMarshalDispose(ref this.m_LobbyId);
		}

		// Token: 0x04000ABE RID: 2750
		private int m_ApiVersion;

		// Token: 0x04000ABF RID: 2751
		private IntPtr m_LocalUserId;

		// Token: 0x04000AC0 RID: 2752
		private IntPtr m_LobbyId;
	}
}
