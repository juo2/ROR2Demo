using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Ecom
{
	// Token: 0x02000490 RID: 1168
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct TransactionCopyEntitlementByIndexOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x170008A5 RID: 2213
		// (set) Token: 0x06001C70 RID: 7280 RVA: 0x0001E0C7 File Offset: 0x0001C2C7
		public uint EntitlementIndex
		{
			set
			{
				this.m_EntitlementIndex = value;
			}
		}

		// Token: 0x06001C71 RID: 7281 RVA: 0x0001E0D0 File Offset: 0x0001C2D0
		public void Set(TransactionCopyEntitlementByIndexOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.EntitlementIndex = other.EntitlementIndex;
			}
		}

		// Token: 0x06001C72 RID: 7282 RVA: 0x0001E0E8 File Offset: 0x0001C2E8
		public void Set(object other)
		{
			this.Set(other as TransactionCopyEntitlementByIndexOptions);
		}

		// Token: 0x06001C73 RID: 7283 RVA: 0x000038B7 File Offset: 0x00001AB7
		public void Dispose()
		{
		}

		// Token: 0x04000D49 RID: 3401
		private int m_ApiVersion;

		// Token: 0x04000D4A RID: 3402
		private uint m_EntitlementIndex;
	}
}
