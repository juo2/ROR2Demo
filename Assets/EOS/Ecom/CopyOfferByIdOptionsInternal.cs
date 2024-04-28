using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Ecom
{
	// Token: 0x0200044A RID: 1098
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct CopyOfferByIdOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x17000817 RID: 2071
		// (set) Token: 0x06001AC8 RID: 6856 RVA: 0x0001C39B File Offset: 0x0001A59B
		public EpicAccountId LocalUserId
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_LocalUserId, value);
			}
		}

		// Token: 0x17000818 RID: 2072
		// (set) Token: 0x06001AC9 RID: 6857 RVA: 0x0001C3AA File Offset: 0x0001A5AA
		public string OfferId
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_OfferId, value);
			}
		}

		// Token: 0x06001ACA RID: 6858 RVA: 0x0001C3B9 File Offset: 0x0001A5B9
		public void Set(CopyOfferByIdOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 2;
				this.LocalUserId = other.LocalUserId;
				this.OfferId = other.OfferId;
			}
		}

		// Token: 0x06001ACB RID: 6859 RVA: 0x0001C3DD File Offset: 0x0001A5DD
		public void Set(object other)
		{
			this.Set(other as CopyOfferByIdOptions);
		}

		// Token: 0x06001ACC RID: 6860 RVA: 0x0001C3EB File Offset: 0x0001A5EB
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_LocalUserId);
			Helper.TryMarshalDispose(ref this.m_OfferId);
		}

		// Token: 0x04000C72 RID: 3186
		private int m_ApiVersion;

		// Token: 0x04000C73 RID: 3187
		private IntPtr m_LocalUserId;

		// Token: 0x04000C74 RID: 3188
		private IntPtr m_OfferId;
	}
}
