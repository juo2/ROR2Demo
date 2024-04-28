using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.RTCAdmin
{
	// Token: 0x020001C8 RID: 456
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct UserTokenInternal : ISettable, IDisposable
	{
		// Token: 0x17000321 RID: 801
		// (get) Token: 0x06000C14 RID: 3092 RVA: 0x0000D22C File Offset: 0x0000B42C
		// (set) Token: 0x06000C15 RID: 3093 RVA: 0x0000D248 File Offset: 0x0000B448
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

		// Token: 0x17000322 RID: 802
		// (get) Token: 0x06000C16 RID: 3094 RVA: 0x0000D258 File Offset: 0x0000B458
		// (set) Token: 0x06000C17 RID: 3095 RVA: 0x0000D274 File Offset: 0x0000B474
		public string Token
		{
			get
			{
				string result;
				Helper.TryMarshalGet(this.m_Token, out result);
				return result;
			}
			set
			{
				Helper.TryMarshalSet(ref this.m_Token, value);
			}
		}

		// Token: 0x06000C18 RID: 3096 RVA: 0x0000D283 File Offset: 0x0000B483
		public void Set(UserToken other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.ProductUserId = other.ProductUserId;
				this.Token = other.Token;
			}
		}

		// Token: 0x06000C19 RID: 3097 RVA: 0x0000D2A7 File Offset: 0x0000B4A7
		public void Set(object other)
		{
			this.Set(other as UserToken);
		}

		// Token: 0x06000C1A RID: 3098 RVA: 0x0000D2B5 File Offset: 0x0000B4B5
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_ProductUserId);
			Helper.TryMarshalDispose(ref this.m_Token);
		}

		// Token: 0x040005BA RID: 1466
		private int m_ApiVersion;

		// Token: 0x040005BB RID: 1467
		private IntPtr m_ProductUserId;

		// Token: 0x040005BC RID: 1468
		private IntPtr m_Token;
	}
}
