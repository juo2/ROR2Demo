using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Ecom
{
	// Token: 0x0200044E RID: 1102
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct CopyOfferImageInfoByIndexOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x17000820 RID: 2080
		// (set) Token: 0x06001ADE RID: 6878 RVA: 0x0001C4B2 File Offset: 0x0001A6B2
		public EpicAccountId LocalUserId
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_LocalUserId, value);
			}
		}

		// Token: 0x17000821 RID: 2081
		// (set) Token: 0x06001ADF RID: 6879 RVA: 0x0001C4C1 File Offset: 0x0001A6C1
		public string OfferId
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_OfferId, value);
			}
		}

		// Token: 0x17000822 RID: 2082
		// (set) Token: 0x06001AE0 RID: 6880 RVA: 0x0001C4D0 File Offset: 0x0001A6D0
		public uint ImageInfoIndex
		{
			set
			{
				this.m_ImageInfoIndex = value;
			}
		}

		// Token: 0x06001AE1 RID: 6881 RVA: 0x0001C4D9 File Offset: 0x0001A6D9
		public void Set(CopyOfferImageInfoByIndexOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.LocalUserId = other.LocalUserId;
				this.OfferId = other.OfferId;
				this.ImageInfoIndex = other.ImageInfoIndex;
			}
		}

		// Token: 0x06001AE2 RID: 6882 RVA: 0x0001C509 File Offset: 0x0001A709
		public void Set(object other)
		{
			this.Set(other as CopyOfferImageInfoByIndexOptions);
		}

		// Token: 0x06001AE3 RID: 6883 RVA: 0x0001C517 File Offset: 0x0001A717
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_LocalUserId);
			Helper.TryMarshalDispose(ref this.m_OfferId);
		}

		// Token: 0x04000C7D RID: 3197
		private int m_ApiVersion;

		// Token: 0x04000C7E RID: 3198
		private IntPtr m_LocalUserId;

		// Token: 0x04000C7F RID: 3199
		private IntPtr m_OfferId;

		// Token: 0x04000C80 RID: 3200
		private uint m_ImageInfoIndex;
	}
}
