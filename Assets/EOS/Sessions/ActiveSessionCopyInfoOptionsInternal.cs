using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x020000A7 RID: 167
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct ActiveSessionCopyInfoOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x060005DC RID: 1500 RVA: 0x00006E49 File Offset: 0x00005049
		public void Set(ActiveSessionCopyInfoOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
			}
		}

		// Token: 0x060005DD RID: 1501 RVA: 0x00006E55 File Offset: 0x00005055
		public void Set(object other)
		{
			this.Set(other as ActiveSessionCopyInfoOptions);
		}

		// Token: 0x060005DE RID: 1502 RVA: 0x000038B7 File Offset: 0x00001AB7
		public void Dispose()
		{
		}

		// Token: 0x040002F8 RID: 760
		private int m_ApiVersion;
	}
}
