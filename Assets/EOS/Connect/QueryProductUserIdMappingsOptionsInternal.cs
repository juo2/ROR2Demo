using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Connect
{
	// Token: 0x020004FC RID: 1276
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct QueryProductUserIdMappingsOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x17000957 RID: 2391
		// (set) Token: 0x06001EC8 RID: 7880 RVA: 0x00020563 File Offset: 0x0001E763
		public ProductUserId LocalUserId
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_LocalUserId, value);
			}
		}

		// Token: 0x17000958 RID: 2392
		// (set) Token: 0x06001EC9 RID: 7881 RVA: 0x00020572 File Offset: 0x0001E772
		public ExternalAccountType AccountIdType_DEPRECATED
		{
			set
			{
				this.m_AccountIdType_DEPRECATED = value;
			}
		}

		// Token: 0x17000959 RID: 2393
		// (set) Token: 0x06001ECA RID: 7882 RVA: 0x0002057B File Offset: 0x0001E77B
		public ProductUserId[] ProductUserIds
		{
			set
			{
				Helper.TryMarshalSet<ProductUserId>(ref this.m_ProductUserIds, value, out this.m_ProductUserIdCount);
			}
		}

		// Token: 0x06001ECB RID: 7883 RVA: 0x00020590 File Offset: 0x0001E790
		public void Set(QueryProductUserIdMappingsOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 2;
				this.LocalUserId = other.LocalUserId;
				this.AccountIdType_DEPRECATED = other.AccountIdType_DEPRECATED;
				this.ProductUserIds = other.ProductUserIds;
			}
		}

		// Token: 0x06001ECC RID: 7884 RVA: 0x000205C0 File Offset: 0x0001E7C0
		public void Set(object other)
		{
			this.Set(other as QueryProductUserIdMappingsOptions);
		}

		// Token: 0x06001ECD RID: 7885 RVA: 0x000205CE File Offset: 0x0001E7CE
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_LocalUserId);
			Helper.TryMarshalDispose(ref this.m_ProductUserIds);
		}

		// Token: 0x04000E31 RID: 3633
		private int m_ApiVersion;

		// Token: 0x04000E32 RID: 3634
		private IntPtr m_LocalUserId;

		// Token: 0x04000E33 RID: 3635
		private ExternalAccountType m_AccountIdType_DEPRECATED;

		// Token: 0x04000E34 RID: 3636
		private IntPtr m_ProductUserIds;

		// Token: 0x04000E35 RID: 3637
		private uint m_ProductUserIdCount;
	}
}
