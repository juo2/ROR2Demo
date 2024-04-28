using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Ecom
{
	// Token: 0x02000450 RID: 1104
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct CopyOfferItemByIndexOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x17000826 RID: 2086
		// (set) Token: 0x06001AEB RID: 6891 RVA: 0x0001C564 File Offset: 0x0001A764
		public EpicAccountId LocalUserId
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_LocalUserId, value);
			}
		}

		// Token: 0x17000827 RID: 2087
		// (set) Token: 0x06001AEC RID: 6892 RVA: 0x0001C573 File Offset: 0x0001A773
		public string OfferId
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_OfferId, value);
			}
		}

		// Token: 0x17000828 RID: 2088
		// (set) Token: 0x06001AED RID: 6893 RVA: 0x0001C582 File Offset: 0x0001A782
		public uint ItemIndex
		{
			set
			{
				this.m_ItemIndex = value;
			}
		}

		// Token: 0x06001AEE RID: 6894 RVA: 0x0001C58B File Offset: 0x0001A78B
		public void Set(CopyOfferItemByIndexOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.LocalUserId = other.LocalUserId;
				this.OfferId = other.OfferId;
				this.ItemIndex = other.ItemIndex;
			}
		}

		// Token: 0x06001AEF RID: 6895 RVA: 0x0001C5BB File Offset: 0x0001A7BB
		public void Set(object other)
		{
			this.Set(other as CopyOfferItemByIndexOptions);
		}

		// Token: 0x06001AF0 RID: 6896 RVA: 0x0001C5C9 File Offset: 0x0001A7C9
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_LocalUserId);
			Helper.TryMarshalDispose(ref this.m_OfferId);
		}

		// Token: 0x04000C84 RID: 3204
		private int m_ApiVersion;

		// Token: 0x04000C85 RID: 3205
		private IntPtr m_LocalUserId;

		// Token: 0x04000C86 RID: 3206
		private IntPtr m_OfferId;

		// Token: 0x04000C87 RID: 3207
		private uint m_ItemIndex;
	}
}
