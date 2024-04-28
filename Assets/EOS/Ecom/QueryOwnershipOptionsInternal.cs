using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Ecom
{
	// Token: 0x02000485 RID: 1157
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct QueryOwnershipOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x17000887 RID: 2183
		// (set) Token: 0x06001C2B RID: 7211 RVA: 0x0001DBD5 File Offset: 0x0001BDD5
		public EpicAccountId LocalUserId
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_LocalUserId, value);
			}
		}

		// Token: 0x17000888 RID: 2184
		// (set) Token: 0x06001C2C RID: 7212 RVA: 0x0001DBE4 File Offset: 0x0001BDE4
		public string[] CatalogItemIds
		{
			set
			{
				Helper.TryMarshalSet<string>(ref this.m_CatalogItemIds, value, out this.m_CatalogItemIdCount);
			}
		}

		// Token: 0x17000889 RID: 2185
		// (set) Token: 0x06001C2D RID: 7213 RVA: 0x0001DBF9 File Offset: 0x0001BDF9
		public string CatalogNamespace
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_CatalogNamespace, value);
			}
		}

		// Token: 0x06001C2E RID: 7214 RVA: 0x0001DC08 File Offset: 0x0001BE08
		public void Set(QueryOwnershipOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 2;
				this.LocalUserId = other.LocalUserId;
				this.CatalogItemIds = other.CatalogItemIds;
				this.CatalogNamespace = other.CatalogNamespace;
			}
		}

		// Token: 0x06001C2F RID: 7215 RVA: 0x0001DC38 File Offset: 0x0001BE38
		public void Set(object other)
		{
			this.Set(other as QueryOwnershipOptions);
		}

		// Token: 0x06001C30 RID: 7216 RVA: 0x0001DC46 File Offset: 0x0001BE46
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_LocalUserId);
			Helper.TryMarshalDispose(ref this.m_CatalogItemIds);
			Helper.TryMarshalDispose(ref this.m_CatalogNamespace);
		}

		// Token: 0x04000D25 RID: 3365
		private int m_ApiVersion;

		// Token: 0x04000D26 RID: 3366
		private IntPtr m_LocalUserId;

		// Token: 0x04000D27 RID: 3367
		private IntPtr m_CatalogItemIds;

		// Token: 0x04000D28 RID: 3368
		private uint m_CatalogItemIdCount;

		// Token: 0x04000D29 RID: 3369
		private IntPtr m_CatalogNamespace;
	}
}
