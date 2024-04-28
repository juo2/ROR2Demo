using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.AntiCheatClient
{
	// Token: 0x020005B6 RID: 1462
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct AddNotifyPeerAuthStatusChangedOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x0600236B RID: 9067 RVA: 0x000255CC File Offset: 0x000237CC
		public void Set(AddNotifyPeerAuthStatusChangedOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
			}
		}

		// Token: 0x0600236C RID: 9068 RVA: 0x000255D8 File Offset: 0x000237D8
		public void Set(object other)
		{
			this.Set(other as AddNotifyPeerAuthStatusChangedOptions);
		}

		// Token: 0x0600236D RID: 9069 RVA: 0x000038B7 File Offset: 0x00001AB7
		public void Dispose()
		{
		}

		// Token: 0x040010B0 RID: 4272
		private int m_ApiVersion;
	}
}
