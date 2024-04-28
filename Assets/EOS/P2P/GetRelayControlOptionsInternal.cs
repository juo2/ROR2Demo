using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.P2P
{
	// Token: 0x02000293 RID: 659
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct GetRelayControlOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x060010A7 RID: 4263 RVA: 0x00011BB5 File Offset: 0x0000FDB5
		public void Set(GetRelayControlOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
			}
		}

		// Token: 0x060010A8 RID: 4264 RVA: 0x00011BC1 File Offset: 0x0000FDC1
		public void Set(object other)
		{
			this.Set(other as GetRelayControlOptions);
		}

		// Token: 0x060010A9 RID: 4265 RVA: 0x000038B7 File Offset: 0x00001AB7
		public void Dispose()
		{
		}

		// Token: 0x040007D4 RID: 2004
		private int m_ApiVersion;
	}
}
