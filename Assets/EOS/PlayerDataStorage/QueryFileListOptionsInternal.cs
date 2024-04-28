using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.PlayerDataStorage
{
	// Token: 0x02000267 RID: 615
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct QueryFileListOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x17000451 RID: 1105
		// (set) Token: 0x06000FBF RID: 4031 RVA: 0x00010D15 File Offset: 0x0000EF15
		public ProductUserId LocalUserId
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_LocalUserId, value);
			}
		}

		// Token: 0x06000FC0 RID: 4032 RVA: 0x00010D24 File Offset: 0x0000EF24
		public void Set(QueryFileListOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.LocalUserId = other.LocalUserId;
			}
		}

		// Token: 0x06000FC1 RID: 4033 RVA: 0x00010D3C File Offset: 0x0000EF3C
		public void Set(object other)
		{
			this.Set(other as QueryFileListOptions);
		}

		// Token: 0x06000FC2 RID: 4034 RVA: 0x00010D4A File Offset: 0x0000EF4A
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_LocalUserId);
		}

		// Token: 0x04000744 RID: 1860
		private int m_ApiVersion;

		// Token: 0x04000745 RID: 1861
		private IntPtr m_LocalUserId;
	}
}
