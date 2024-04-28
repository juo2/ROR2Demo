using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Ecom
{
	// Token: 0x02000492 RID: 1170
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct TransactionGetEntitlementsCountOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x06001C75 RID: 7285 RVA: 0x0001E0F6 File Offset: 0x0001C2F6
		public void Set(TransactionGetEntitlementsCountOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
			}
		}

		// Token: 0x06001C76 RID: 7286 RVA: 0x0001E102 File Offset: 0x0001C302
		public void Set(object other)
		{
			this.Set(other as TransactionGetEntitlementsCountOptions);
		}

		// Token: 0x06001C77 RID: 7287 RVA: 0x000038B7 File Offset: 0x00001AB7
		public void Dispose()
		{
		}

		// Token: 0x04000D4B RID: 3403
		private int m_ApiVersion;
	}
}
