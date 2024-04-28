using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Ecom
{
	// Token: 0x02000454 RID: 1108
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct CopyTransactionByIndexOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x1700082F RID: 2095
		// (set) Token: 0x06001B00 RID: 6912 RVA: 0x0001C691 File Offset: 0x0001A891
		public EpicAccountId LocalUserId
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_LocalUserId, value);
			}
		}

		// Token: 0x17000830 RID: 2096
		// (set) Token: 0x06001B01 RID: 6913 RVA: 0x0001C6A0 File Offset: 0x0001A8A0
		public uint TransactionIndex
		{
			set
			{
				this.m_TransactionIndex = value;
			}
		}

		// Token: 0x06001B02 RID: 6914 RVA: 0x0001C6A9 File Offset: 0x0001A8A9
		public void Set(CopyTransactionByIndexOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.LocalUserId = other.LocalUserId;
				this.TransactionIndex = other.TransactionIndex;
			}
		}

		// Token: 0x06001B03 RID: 6915 RVA: 0x0001C6CD File Offset: 0x0001A8CD
		public void Set(object other)
		{
			this.Set(other as CopyTransactionByIndexOptions);
		}

		// Token: 0x06001B04 RID: 6916 RVA: 0x0001C6DB File Offset: 0x0001A8DB
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_LocalUserId);
		}

		// Token: 0x04000C8F RID: 3215
		private int m_ApiVersion;

		// Token: 0x04000C90 RID: 3216
		private IntPtr m_LocalUserId;

		// Token: 0x04000C91 RID: 3217
		private uint m_TransactionIndex;
	}
}
