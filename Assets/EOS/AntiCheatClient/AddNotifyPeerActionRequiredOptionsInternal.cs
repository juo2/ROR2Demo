using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.AntiCheatClient
{
	// Token: 0x020005B4 RID: 1460
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct AddNotifyPeerActionRequiredOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x06002367 RID: 9063 RVA: 0x000255B2 File Offset: 0x000237B2
		public void Set(AddNotifyPeerActionRequiredOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
			}
		}

		// Token: 0x06002368 RID: 9064 RVA: 0x000255BE File Offset: 0x000237BE
		public void Set(object other)
		{
			this.Set(other as AddNotifyPeerActionRequiredOptions);
		}

		// Token: 0x06002369 RID: 9065 RVA: 0x000038B7 File Offset: 0x00001AB7
		public void Dispose()
		{
		}

		// Token: 0x040010AF RID: 4271
		private int m_ApiVersion;
	}
}
