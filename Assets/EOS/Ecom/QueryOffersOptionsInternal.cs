using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Ecom
{
	// Token: 0x02000481 RID: 1153
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct QueryOffersOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x17000879 RID: 2169
		// (set) Token: 0x06001C0E RID: 7182 RVA: 0x0001D9FA File Offset: 0x0001BBFA
		public EpicAccountId LocalUserId
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_LocalUserId, value);
			}
		}

		// Token: 0x1700087A RID: 2170
		// (set) Token: 0x06001C0F RID: 7183 RVA: 0x0001DA09 File Offset: 0x0001BC09
		public string OverrideCatalogNamespace
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_OverrideCatalogNamespace, value);
			}
		}

		// Token: 0x06001C10 RID: 7184 RVA: 0x0001DA18 File Offset: 0x0001BC18
		public void Set(QueryOffersOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.LocalUserId = other.LocalUserId;
				this.OverrideCatalogNamespace = other.OverrideCatalogNamespace;
			}
		}

		// Token: 0x06001C11 RID: 7185 RVA: 0x0001DA3C File Offset: 0x0001BC3C
		public void Set(object other)
		{
			this.Set(other as QueryOffersOptions);
		}

		// Token: 0x06001C12 RID: 7186 RVA: 0x0001DA4A File Offset: 0x0001BC4A
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_LocalUserId);
			Helper.TryMarshalDispose(ref this.m_OverrideCatalogNamespace);
		}

		// Token: 0x04000D16 RID: 3350
		private int m_ApiVersion;

		// Token: 0x04000D17 RID: 3351
		private IntPtr m_LocalUserId;

		// Token: 0x04000D18 RID: 3352
		private IntPtr m_OverrideCatalogNamespace;
	}
}
