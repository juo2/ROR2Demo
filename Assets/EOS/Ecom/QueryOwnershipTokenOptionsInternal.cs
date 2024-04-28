using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Ecom
{
	// Token: 0x02000489 RID: 1161
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct QueryOwnershipTokenOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x17000896 RID: 2198
		// (set) Token: 0x06001C49 RID: 7241 RVA: 0x0001DDD7 File Offset: 0x0001BFD7
		public EpicAccountId LocalUserId
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_LocalUserId, value);
			}
		}

		// Token: 0x17000897 RID: 2199
		// (set) Token: 0x06001C4A RID: 7242 RVA: 0x0001DDE6 File Offset: 0x0001BFE6
		public string[] CatalogItemIds
		{
			set
			{
				Helper.TryMarshalSet<string>(ref this.m_CatalogItemIds, value, out this.m_CatalogItemIdCount);
			}
		}

		// Token: 0x17000898 RID: 2200
		// (set) Token: 0x06001C4B RID: 7243 RVA: 0x0001DDFB File Offset: 0x0001BFFB
		public string CatalogNamespace
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_CatalogNamespace, value);
			}
		}

		// Token: 0x06001C4C RID: 7244 RVA: 0x0001DE0A File Offset: 0x0001C00A
		public void Set(QueryOwnershipTokenOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 2;
				this.LocalUserId = other.LocalUserId;
				this.CatalogItemIds = other.CatalogItemIds;
				this.CatalogNamespace = other.CatalogNamespace;
			}
		}

		// Token: 0x06001C4D RID: 7245 RVA: 0x0001DE3A File Offset: 0x0001C03A
		public void Set(object other)
		{
			this.Set(other as QueryOwnershipTokenOptions);
		}

		// Token: 0x06001C4E RID: 7246 RVA: 0x0001DE48 File Offset: 0x0001C048
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_LocalUserId);
			Helper.TryMarshalDispose(ref this.m_CatalogItemIds);
			Helper.TryMarshalDispose(ref this.m_CatalogNamespace);
		}

		// Token: 0x04000D35 RID: 3381
		private int m_ApiVersion;

		// Token: 0x04000D36 RID: 3382
		private IntPtr m_LocalUserId;

		// Token: 0x04000D37 RID: 3383
		private IntPtr m_CatalogItemIds;

		// Token: 0x04000D38 RID: 3384
		private uint m_CatalogItemIdCount;

		// Token: 0x04000D39 RID: 3385
		private IntPtr m_CatalogNamespace;
	}
}
