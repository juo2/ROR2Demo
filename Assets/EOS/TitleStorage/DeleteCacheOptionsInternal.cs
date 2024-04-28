using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.TitleStorage
{
	// Token: 0x0200006A RID: 106
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct DeleteCacheOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x1700009F RID: 159
		// (set) Token: 0x06000453 RID: 1107 RVA: 0x000054B1 File Offset: 0x000036B1
		public ProductUserId LocalUserId
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_LocalUserId, value);
			}
		}

		// Token: 0x06000454 RID: 1108 RVA: 0x000054C0 File Offset: 0x000036C0
		public void Set(DeleteCacheOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.LocalUserId = other.LocalUserId;
			}
		}

		// Token: 0x06000455 RID: 1109 RVA: 0x000054D8 File Offset: 0x000036D8
		public void Set(object other)
		{
			this.Set(other as DeleteCacheOptions);
		}

		// Token: 0x06000456 RID: 1110 RVA: 0x000054E6 File Offset: 0x000036E6
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_LocalUserId);
		}

		// Token: 0x0400024A RID: 586
		private int m_ApiVersion;

		// Token: 0x0400024B RID: 587
		private IntPtr m_LocalUserId;
	}
}
