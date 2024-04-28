using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.AntiCheatServer
{
	// Token: 0x0200055E RID: 1374
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct EndSessionOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x06002162 RID: 8546 RVA: 0x00023626 File Offset: 0x00021826
		public void Set(EndSessionOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
			}
		}

		// Token: 0x06002163 RID: 8547 RVA: 0x00023632 File Offset: 0x00021832
		public void Set(object other)
		{
			this.Set(other as EndSessionOptions);
		}

		// Token: 0x06002164 RID: 8548 RVA: 0x000038B7 File Offset: 0x00001AB7
		public void Dispose()
		{
		}

		// Token: 0x04000F60 RID: 3936
		private int m_ApiVersion;
	}
}
