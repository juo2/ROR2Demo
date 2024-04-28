using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.P2P
{
	// Token: 0x0200028B RID: 651
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct GetNATTypeOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x06001091 RID: 4241 RVA: 0x00011ADB File Offset: 0x0000FCDB
		public void Set(GetNATTypeOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
			}
		}

		// Token: 0x06001092 RID: 4242 RVA: 0x00011AE7 File Offset: 0x0000FCE7
		public void Set(object other)
		{
			this.Set(other as GetNATTypeOptions);
		}

		// Token: 0x06001093 RID: 4243 RVA: 0x000038B7 File Offset: 0x00001AB7
		public void Dispose()
		{
		}

		// Token: 0x040007CC RID: 1996
		private int m_ApiVersion;
	}
}
