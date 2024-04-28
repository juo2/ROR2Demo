using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Connect
{
	// Token: 0x020004F8 RID: 1272
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct QueryExternalAccountMappingsOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x1700094A RID: 2378
		// (set) Token: 0x06001EAD RID: 7853 RVA: 0x000203B7 File Offset: 0x0001E5B7
		public ProductUserId LocalUserId
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_LocalUserId, value);
			}
		}

		// Token: 0x1700094B RID: 2379
		// (set) Token: 0x06001EAE RID: 7854 RVA: 0x000203C6 File Offset: 0x0001E5C6
		public ExternalAccountType AccountIdType
		{
			set
			{
				this.m_AccountIdType = value;
			}
		}

		// Token: 0x1700094C RID: 2380
		// (set) Token: 0x06001EAF RID: 7855 RVA: 0x000203CF File Offset: 0x0001E5CF
		public string[] ExternalAccountIds
		{
			set
			{
				Helper.TryMarshalSet<string>(ref this.m_ExternalAccountIds, value, out this.m_ExternalAccountIdCount, true);
			}
		}

		// Token: 0x06001EB0 RID: 7856 RVA: 0x000203E5 File Offset: 0x0001E5E5
		public void Set(QueryExternalAccountMappingsOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.LocalUserId = other.LocalUserId;
				this.AccountIdType = other.AccountIdType;
				this.ExternalAccountIds = other.ExternalAccountIds;
			}
		}

		// Token: 0x06001EB1 RID: 7857 RVA: 0x00020415 File Offset: 0x0001E615
		public void Set(object other)
		{
			this.Set(other as QueryExternalAccountMappingsOptions);
		}

		// Token: 0x06001EB2 RID: 7858 RVA: 0x00020423 File Offset: 0x0001E623
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_LocalUserId);
			Helper.TryMarshalDispose(ref this.m_ExternalAccountIds);
		}

		// Token: 0x04000E23 RID: 3619
		private int m_ApiVersion;

		// Token: 0x04000E24 RID: 3620
		private IntPtr m_LocalUserId;

		// Token: 0x04000E25 RID: 3621
		private ExternalAccountType m_AccountIdType;

		// Token: 0x04000E26 RID: 3622
		private IntPtr m_ExternalAccountIds;

		// Token: 0x04000E27 RID: 3623
		private uint m_ExternalAccountIdCount;
	}
}
