using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.PlayerDataStorage
{
	// Token: 0x0200023D RID: 573
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct DeleteCacheOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x17000410 RID: 1040
		// (set) Token: 0x06000EC5 RID: 3781 RVA: 0x0000FE5D File Offset: 0x0000E05D
		public ProductUserId LocalUserId
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_LocalUserId, value);
			}
		}

		// Token: 0x06000EC6 RID: 3782 RVA: 0x0000FE6C File Offset: 0x0000E06C
		public void Set(DeleteCacheOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.LocalUserId = other.LocalUserId;
			}
		}

		// Token: 0x06000EC7 RID: 3783 RVA: 0x0000FE84 File Offset: 0x0000E084
		public void Set(object other)
		{
			this.Set(other as DeleteCacheOptions);
		}

		// Token: 0x06000EC8 RID: 3784 RVA: 0x0000FE92 File Offset: 0x0000E092
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_LocalUserId);
		}

		// Token: 0x040006F6 RID: 1782
		private int m_ApiVersion;

		// Token: 0x040006F7 RID: 1783
		private IntPtr m_LocalUserId;
	}
}
