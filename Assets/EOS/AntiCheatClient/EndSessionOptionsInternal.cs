using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.AntiCheatClient
{
	// Token: 0x020005BD RID: 1469
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct EndSessionOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x06002392 RID: 9106 RVA: 0x00025B7E File Offset: 0x00023D7E
		public void Set(EndSessionOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
			}
		}

		// Token: 0x06002393 RID: 9107 RVA: 0x00025B8A File Offset: 0x00023D8A
		public void Set(object other)
		{
			this.Set(other as EndSessionOptions);
		}

		// Token: 0x06002394 RID: 9108 RVA: 0x000038B7 File Offset: 0x00001AB7
		public void Dispose()
		{
		}

		// Token: 0x040010DB RID: 4315
		private int m_ApiVersion;
	}
}
