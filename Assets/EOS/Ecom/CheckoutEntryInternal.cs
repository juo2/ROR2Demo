using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Ecom
{
	// Token: 0x0200043A RID: 1082
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct CheckoutEntryInternal : ISettable, IDisposable
	{
		// Token: 0x170007F0 RID: 2032
		// (get) Token: 0x06001A6C RID: 6764 RVA: 0x0001BEA8 File Offset: 0x0001A0A8
		// (set) Token: 0x06001A6D RID: 6765 RVA: 0x0001BEC4 File Offset: 0x0001A0C4
		public string OfferId
		{
			get
			{
				string result;
				Helper.TryMarshalGet(this.m_OfferId, out result);
				return result;
			}
			set
			{
				Helper.TryMarshalSet(ref this.m_OfferId, value);
			}
		}

		// Token: 0x06001A6E RID: 6766 RVA: 0x0001BED3 File Offset: 0x0001A0D3
		public void Set(CheckoutEntry other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.OfferId = other.OfferId;
			}
		}

		// Token: 0x06001A6F RID: 6767 RVA: 0x0001BEEB File Offset: 0x0001A0EB
		public void Set(object other)
		{
			this.Set(other as CheckoutEntry);
		}

		// Token: 0x06001A70 RID: 6768 RVA: 0x0001BEF9 File Offset: 0x0001A0F9
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_OfferId);
		}

		// Token: 0x04000C42 RID: 3138
		private int m_ApiVersion;

		// Token: 0x04000C43 RID: 3139
		private IntPtr m_OfferId;
	}
}
