using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.AntiCheatClient
{
	// Token: 0x020005D7 RID: 1495
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct UnregisterPeerOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x17000B3B RID: 2875
		// (set) Token: 0x06002409 RID: 9225 RVA: 0x00025FFC File Offset: 0x000241FC
		public IntPtr PeerHandle
		{
			set
			{
				this.m_PeerHandle = value;
			}
		}

		// Token: 0x0600240A RID: 9226 RVA: 0x00026005 File Offset: 0x00024205
		public void Set(UnregisterPeerOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.PeerHandle = other.PeerHandle;
			}
		}

		// Token: 0x0600240B RID: 9227 RVA: 0x0002601D File Offset: 0x0002421D
		public void Set(object other)
		{
			this.Set(other as UnregisterPeerOptions);
		}

		// Token: 0x0600240C RID: 9228 RVA: 0x0002602B File Offset: 0x0002422B
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_PeerHandle);
		}

		// Token: 0x04001109 RID: 4361
		private int m_ApiVersion;

		// Token: 0x0400110A RID: 4362
		private IntPtr m_PeerHandle;
	}
}
