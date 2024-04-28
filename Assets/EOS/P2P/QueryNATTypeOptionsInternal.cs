using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.P2P
{
	// Token: 0x020002AE RID: 686
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct QueryNATTypeOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x0600116D RID: 4461 RVA: 0x00012A66 File Offset: 0x00010C66
		public void Set(QueryNATTypeOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
			}
		}

		// Token: 0x0600116E RID: 4462 RVA: 0x00012A72 File Offset: 0x00010C72
		public void Set(object other)
		{
			this.Set(other as QueryNATTypeOptions);
		}

		// Token: 0x0600116F RID: 4463 RVA: 0x000038B7 File Offset: 0x00001AB7
		public void Dispose()
		{
		}

		// Token: 0x0400082F RID: 2095
		private int m_ApiVersion;
	}
}
