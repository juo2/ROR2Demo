using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.AntiCheatClient
{
	// Token: 0x020005B2 RID: 1458
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct AddNotifyMessageToServerOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x06002363 RID: 9059 RVA: 0x00025598 File Offset: 0x00023798
		public void Set(AddNotifyMessageToServerOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
			}
		}

		// Token: 0x06002364 RID: 9060 RVA: 0x000255A4 File Offset: 0x000237A4
		public void Set(object other)
		{
			this.Set(other as AddNotifyMessageToServerOptions);
		}

		// Token: 0x06002365 RID: 9061 RVA: 0x000038B7 File Offset: 0x00001AB7
		public void Dispose()
		{
		}

		// Token: 0x040010AE RID: 4270
		private int m_ApiVersion;
	}
}
