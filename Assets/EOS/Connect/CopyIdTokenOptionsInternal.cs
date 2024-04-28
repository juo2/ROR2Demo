using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Connect
{
	// Token: 0x020004B2 RID: 1202
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct CopyIdTokenOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x170008DB RID: 2267
		// (set) Token: 0x06001D3E RID: 7486 RVA: 0x0001F305 File Offset: 0x0001D505
		public ProductUserId LocalUserId
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_LocalUserId, value);
			}
		}

		// Token: 0x06001D3F RID: 7487 RVA: 0x0001F314 File Offset: 0x0001D514
		public void Set(CopyIdTokenOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.LocalUserId = other.LocalUserId;
			}
		}

		// Token: 0x06001D40 RID: 7488 RVA: 0x0001F32C File Offset: 0x0001D52C
		public void Set(object other)
		{
			this.Set(other as CopyIdTokenOptions);
		}

		// Token: 0x06001D41 RID: 7489 RVA: 0x0001F33A File Offset: 0x0001D53A
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_LocalUserId);
		}

		// Token: 0x04000DAB RID: 3499
		private int m_ApiVersion;

		// Token: 0x04000DAC RID: 3500
		private IntPtr m_LocalUserId;
	}
}
