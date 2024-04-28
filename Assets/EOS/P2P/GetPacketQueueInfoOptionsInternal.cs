using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.P2P
{
	// Token: 0x0200028F RID: 655
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct GetPacketQueueInfoOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x0600109F RID: 4255 RVA: 0x00011B81 File Offset: 0x0000FD81
		public void Set(GetPacketQueueInfoOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
			}
		}

		// Token: 0x060010A0 RID: 4256 RVA: 0x00011B8D File Offset: 0x0000FD8D
		public void Set(object other)
		{
			this.Set(other as GetPacketQueueInfoOptions);
		}

		// Token: 0x060010A1 RID: 4257 RVA: 0x000038B7 File Offset: 0x00001AB7
		public void Dispose()
		{
		}

		// Token: 0x040007D2 RID: 2002
		private int m_ApiVersion;
	}
}
