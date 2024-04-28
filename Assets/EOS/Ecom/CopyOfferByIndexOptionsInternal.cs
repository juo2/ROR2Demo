using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Ecom
{
	// Token: 0x0200044C RID: 1100
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct CopyOfferByIndexOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x1700081B RID: 2075
		// (set) Token: 0x06001AD2 RID: 6866 RVA: 0x0001C427 File Offset: 0x0001A627
		public EpicAccountId LocalUserId
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_LocalUserId, value);
			}
		}

		// Token: 0x1700081C RID: 2076
		// (set) Token: 0x06001AD3 RID: 6867 RVA: 0x0001C436 File Offset: 0x0001A636
		public uint OfferIndex
		{
			set
			{
				this.m_OfferIndex = value;
			}
		}

		// Token: 0x06001AD4 RID: 6868 RVA: 0x0001C43F File Offset: 0x0001A63F
		public void Set(CopyOfferByIndexOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 2;
				this.LocalUserId = other.LocalUserId;
				this.OfferIndex = other.OfferIndex;
			}
		}

		// Token: 0x06001AD5 RID: 6869 RVA: 0x0001C463 File Offset: 0x0001A663
		public void Set(object other)
		{
			this.Set(other as CopyOfferByIndexOptions);
		}

		// Token: 0x06001AD6 RID: 6870 RVA: 0x0001C471 File Offset: 0x0001A671
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_LocalUserId);
		}

		// Token: 0x04000C77 RID: 3191
		private int m_ApiVersion;

		// Token: 0x04000C78 RID: 3192
		private IntPtr m_LocalUserId;

		// Token: 0x04000C79 RID: 3193
		private uint m_OfferIndex;
	}
}
