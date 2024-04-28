using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Ecom
{
	// Token: 0x02000444 RID: 1092
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct CopyItemByIdOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x17000807 RID: 2055
		// (set) Token: 0x06001AA4 RID: 6820 RVA: 0x0001C1AB File Offset: 0x0001A3AB
		public EpicAccountId LocalUserId
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_LocalUserId, value);
			}
		}

		// Token: 0x17000808 RID: 2056
		// (set) Token: 0x06001AA5 RID: 6821 RVA: 0x0001C1BA File Offset: 0x0001A3BA
		public string ItemId
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_ItemId, value);
			}
		}

		// Token: 0x06001AA6 RID: 6822 RVA: 0x0001C1C9 File Offset: 0x0001A3C9
		public void Set(CopyItemByIdOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.LocalUserId = other.LocalUserId;
				this.ItemId = other.ItemId;
			}
		}

		// Token: 0x06001AA7 RID: 6823 RVA: 0x0001C1ED File Offset: 0x0001A3ED
		public void Set(object other)
		{
			this.Set(other as CopyItemByIdOptions);
		}

		// Token: 0x06001AA8 RID: 6824 RVA: 0x0001C1FB File Offset: 0x0001A3FB
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_LocalUserId);
			Helper.TryMarshalDispose(ref this.m_ItemId);
		}

		// Token: 0x04000C5F RID: 3167
		private int m_ApiVersion;

		// Token: 0x04000C60 RID: 3168
		private IntPtr m_LocalUserId;

		// Token: 0x04000C61 RID: 3169
		private IntPtr m_ItemId;
	}
}
