using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Ecom
{
	// Token: 0x0200043C RID: 1084
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct CheckoutOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x170007F4 RID: 2036
		// (set) Token: 0x06001A78 RID: 6776 RVA: 0x0001BF3A File Offset: 0x0001A13A
		public EpicAccountId LocalUserId
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_LocalUserId, value);
			}
		}

		// Token: 0x170007F5 RID: 2037
		// (set) Token: 0x06001A79 RID: 6777 RVA: 0x0001BF49 File Offset: 0x0001A149
		public string OverrideCatalogNamespace
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_OverrideCatalogNamespace, value);
			}
		}

		// Token: 0x170007F6 RID: 2038
		// (set) Token: 0x06001A7A RID: 6778 RVA: 0x0001BF58 File Offset: 0x0001A158
		public CheckoutEntry[] Entries
		{
			set
			{
				Helper.TryMarshalSet<CheckoutEntryInternal, CheckoutEntry>(ref this.m_Entries, value, out this.m_EntryCount);
			}
		}

		// Token: 0x06001A7B RID: 6779 RVA: 0x0001BF6D File Offset: 0x0001A16D
		public void Set(CheckoutOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.LocalUserId = other.LocalUserId;
				this.OverrideCatalogNamespace = other.OverrideCatalogNamespace;
				this.Entries = other.Entries;
			}
		}

		// Token: 0x06001A7C RID: 6780 RVA: 0x0001BF9D File Offset: 0x0001A19D
		public void Set(object other)
		{
			this.Set(other as CheckoutOptions);
		}

		// Token: 0x06001A7D RID: 6781 RVA: 0x0001BFAB File Offset: 0x0001A1AB
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_LocalUserId);
			Helper.TryMarshalDispose(ref this.m_OverrideCatalogNamespace);
			Helper.TryMarshalDispose(ref this.m_Entries);
		}

		// Token: 0x04000C47 RID: 3143
		private int m_ApiVersion;

		// Token: 0x04000C48 RID: 3144
		private IntPtr m_LocalUserId;

		// Token: 0x04000C49 RID: 3145
		private IntPtr m_OverrideCatalogNamespace;

		// Token: 0x04000C4A RID: 3146
		private uint m_EntryCount;

		// Token: 0x04000C4B RID: 3147
		private IntPtr m_Entries;
	}
}
