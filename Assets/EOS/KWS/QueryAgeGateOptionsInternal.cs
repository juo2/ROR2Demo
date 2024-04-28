using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.KWS
{
	// Token: 0x020003FE RID: 1022
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct QueryAgeGateOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x060018A2 RID: 6306 RVA: 0x00019F18 File Offset: 0x00018118
		public void Set(QueryAgeGateOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
			}
		}

		// Token: 0x060018A3 RID: 6307 RVA: 0x00019F24 File Offset: 0x00018124
		public void Set(object other)
		{
			this.Set(other as QueryAgeGateOptions);
		}

		// Token: 0x060018A4 RID: 6308 RVA: 0x000038B7 File Offset: 0x00001AB7
		public void Dispose()
		{
		}

		// Token: 0x04000B78 RID: 2936
		private int m_ApiVersion;
	}
}
