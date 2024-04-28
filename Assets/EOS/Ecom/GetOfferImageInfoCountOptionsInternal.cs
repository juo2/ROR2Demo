using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Ecom
{
	// Token: 0x02000464 RID: 1124
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct GetOfferImageInfoCountOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x1700084F RID: 2127
		// (set) Token: 0x06001B76 RID: 7030 RVA: 0x0001D355 File Offset: 0x0001B555
		public EpicAccountId LocalUserId
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_LocalUserId, value);
			}
		}

		// Token: 0x17000850 RID: 2128
		// (set) Token: 0x06001B77 RID: 7031 RVA: 0x0001D364 File Offset: 0x0001B564
		public string OfferId
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_OfferId, value);
			}
		}

		// Token: 0x06001B78 RID: 7032 RVA: 0x0001D373 File Offset: 0x0001B573
		public void Set(GetOfferImageInfoCountOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.LocalUserId = other.LocalUserId;
				this.OfferId = other.OfferId;
			}
		}

		// Token: 0x06001B79 RID: 7033 RVA: 0x0001D397 File Offset: 0x0001B597
		public void Set(object other)
		{
			this.Set(other as GetOfferImageInfoCountOptions);
		}

		// Token: 0x06001B7A RID: 7034 RVA: 0x0001D3A5 File Offset: 0x0001B5A5
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_LocalUserId);
			Helper.TryMarshalDispose(ref this.m_OfferId);
		}

		// Token: 0x04000CE4 RID: 3300
		private int m_ApiVersion;

		// Token: 0x04000CE5 RID: 3301
		private IntPtr m_LocalUserId;

		// Token: 0x04000CE6 RID: 3302
		private IntPtr m_OfferId;
	}
}
