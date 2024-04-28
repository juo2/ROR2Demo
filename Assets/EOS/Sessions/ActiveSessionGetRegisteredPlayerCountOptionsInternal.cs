using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x020000AB RID: 171
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct ActiveSessionGetRegisteredPlayerCountOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x060005E7 RID: 1511 RVA: 0x00006EA3 File Offset: 0x000050A3
		public void Set(ActiveSessionGetRegisteredPlayerCountOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
			}
		}

		// Token: 0x060005E8 RID: 1512 RVA: 0x00006EAF File Offset: 0x000050AF
		public void Set(object other)
		{
			this.Set(other as ActiveSessionGetRegisteredPlayerCountOptions);
		}

		// Token: 0x060005E9 RID: 1513 RVA: 0x000038B7 File Offset: 0x00001AB7
		public void Dispose()
		{
		}

		// Token: 0x040002FC RID: 764
		private int m_ApiVersion;
	}
}
