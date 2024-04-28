using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x020003AE RID: 942
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct UpdateLobbyOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x170006BF RID: 1727
		// (set) Token: 0x060016F2 RID: 5874 RVA: 0x00018431 File Offset: 0x00016631
		public LobbyModification LobbyModificationHandle
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_LobbyModificationHandle, value);
			}
		}

		// Token: 0x060016F3 RID: 5875 RVA: 0x00018440 File Offset: 0x00016640
		public void Set(UpdateLobbyOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.LobbyModificationHandle = other.LobbyModificationHandle;
			}
		}

		// Token: 0x060016F4 RID: 5876 RVA: 0x00018458 File Offset: 0x00016658
		public void Set(object other)
		{
			this.Set(other as UpdateLobbyOptions);
		}

		// Token: 0x060016F5 RID: 5877 RVA: 0x00018466 File Offset: 0x00016666
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_LobbyModificationHandle);
		}

		// Token: 0x04000AC2 RID: 2754
		private int m_ApiVersion;

		// Token: 0x04000AC3 RID: 2755
		private IntPtr m_LobbyModificationHandle;
	}
}
