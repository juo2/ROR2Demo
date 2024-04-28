using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.P2P
{
	// Token: 0x02000291 RID: 657
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct GetPortRangeOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x060010A3 RID: 4259 RVA: 0x00011B9B File Offset: 0x0000FD9B
		public void Set(GetPortRangeOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
			}
		}

		// Token: 0x060010A4 RID: 4260 RVA: 0x00011BA7 File Offset: 0x0000FDA7
		public void Set(object other)
		{
			this.Set(other as GetPortRangeOptions);
		}

		// Token: 0x060010A5 RID: 4261 RVA: 0x000038B7 File Offset: 0x00001AB7
		public void Dispose()
		{
		}

		// Token: 0x040007D3 RID: 2003
		private int m_ApiVersion;
	}
}
