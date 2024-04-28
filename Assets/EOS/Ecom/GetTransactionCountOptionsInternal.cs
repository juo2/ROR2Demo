using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Ecom
{
	// Token: 0x02000468 RID: 1128
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct GetTransactionCountOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x17000856 RID: 2134
		// (set) Token: 0x06001B88 RID: 7048 RVA: 0x0001D45C File Offset: 0x0001B65C
		public EpicAccountId LocalUserId
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_LocalUserId, value);
			}
		}

		// Token: 0x06001B89 RID: 7049 RVA: 0x0001D46B File Offset: 0x0001B66B
		public void Set(GetTransactionCountOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.LocalUserId = other.LocalUserId;
			}
		}

		// Token: 0x06001B8A RID: 7050 RVA: 0x0001D483 File Offset: 0x0001B683
		public void Set(object other)
		{
			this.Set(other as GetTransactionCountOptions);
		}

		// Token: 0x06001B8B RID: 7051 RVA: 0x0001D491 File Offset: 0x0001B691
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_LocalUserId);
		}

		// Token: 0x04000CED RID: 3309
		private int m_ApiVersion;

		// Token: 0x04000CEE RID: 3310
		private IntPtr m_LocalUserId;
	}
}
