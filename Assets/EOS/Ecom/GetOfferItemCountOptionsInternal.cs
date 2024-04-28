using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Ecom
{
	// Token: 0x02000466 RID: 1126
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct GetOfferItemCountOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x17000853 RID: 2131
		// (set) Token: 0x06001B80 RID: 7040 RVA: 0x0001D3E1 File Offset: 0x0001B5E1
		public EpicAccountId LocalUserId
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_LocalUserId, value);
			}
		}

		// Token: 0x17000854 RID: 2132
		// (set) Token: 0x06001B81 RID: 7041 RVA: 0x0001D3F0 File Offset: 0x0001B5F0
		public string OfferId
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_OfferId, value);
			}
		}

		// Token: 0x06001B82 RID: 7042 RVA: 0x0001D3FF File Offset: 0x0001B5FF
		public void Set(GetOfferItemCountOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.LocalUserId = other.LocalUserId;
				this.OfferId = other.OfferId;
			}
		}

		// Token: 0x06001B83 RID: 7043 RVA: 0x0001D423 File Offset: 0x0001B623
		public void Set(object other)
		{
			this.Set(other as GetOfferItemCountOptions);
		}

		// Token: 0x06001B84 RID: 7044 RVA: 0x0001D431 File Offset: 0x0001B631
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_LocalUserId);
			Helper.TryMarshalDispose(ref this.m_OfferId);
		}

		// Token: 0x04000CE9 RID: 3305
		private int m_ApiVersion;

		// Token: 0x04000CEA RID: 3306
		private IntPtr m_LocalUserId;

		// Token: 0x04000CEB RID: 3307
		private IntPtr m_OfferId;
	}
}
