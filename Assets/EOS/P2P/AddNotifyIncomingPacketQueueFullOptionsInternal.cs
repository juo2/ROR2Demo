using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.P2P
{
	// Token: 0x0200027B RID: 635
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct AddNotifyIncomingPacketQueueFullOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x0600104B RID: 4171 RVA: 0x00011709 File Offset: 0x0000F909
		public void Set(AddNotifyIncomingPacketQueueFullOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
			}
		}

		// Token: 0x0600104C RID: 4172 RVA: 0x00011715 File Offset: 0x0000F915
		public void Set(object other)
		{
			this.Set(other as AddNotifyIncomingPacketQueueFullOptions);
		}

		// Token: 0x0600104D RID: 4173 RVA: 0x000038B7 File Offset: 0x00001AB7
		public void Dispose()
		{
		}

		// Token: 0x0400079A RID: 1946
		private int m_ApiVersion;
	}
}
