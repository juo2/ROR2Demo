using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Connect
{
	// Token: 0x020004D2 RID: 1234
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct IdTokenInternal : ISettable, IDisposable
	{
		// Token: 0x1700091D RID: 2333
		// (get) Token: 0x06001DED RID: 7661 RVA: 0x0001FD84 File Offset: 0x0001DF84
		// (set) Token: 0x06001DEE RID: 7662 RVA: 0x0001FDA0 File Offset: 0x0001DFA0
		public ProductUserId ProductUserId
		{
			get
			{
				ProductUserId result;
				Helper.TryMarshalGet<ProductUserId>(this.m_ProductUserId, out result);
				return result;
			}
			set
			{
				Helper.TryMarshalSet(ref this.m_ProductUserId, value);
			}
		}

		// Token: 0x1700091E RID: 2334
		// (get) Token: 0x06001DEF RID: 7663 RVA: 0x0001FDB0 File Offset: 0x0001DFB0
		// (set) Token: 0x06001DF0 RID: 7664 RVA: 0x0001FDCC File Offset: 0x0001DFCC
		public string JsonWebToken
		{
			get
			{
				string result;
				Helper.TryMarshalGet(this.m_JsonWebToken, out result);
				return result;
			}
			set
			{
				Helper.TryMarshalSet(ref this.m_JsonWebToken, value);
			}
		}

		// Token: 0x06001DF1 RID: 7665 RVA: 0x0001FDDB File Offset: 0x0001DFDB
		public void Set(IdToken other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.ProductUserId = other.ProductUserId;
				this.JsonWebToken = other.JsonWebToken;
			}
		}

		// Token: 0x06001DF2 RID: 7666 RVA: 0x0001FDFF File Offset: 0x0001DFFF
		public void Set(object other)
		{
			this.Set(other as IdToken);
		}

		// Token: 0x06001DF3 RID: 7667 RVA: 0x0001FE0D File Offset: 0x0001E00D
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_ProductUserId);
			Helper.TryMarshalDispose(ref this.m_JsonWebToken);
		}

		// Token: 0x04000DF7 RID: 3575
		private int m_ApiVersion;

		// Token: 0x04000DF8 RID: 3576
		private IntPtr m_ProductUserId;

		// Token: 0x04000DF9 RID: 3577
		private IntPtr m_JsonWebToken;
	}
}
