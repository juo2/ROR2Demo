using System;

namespace Epic.OnlineServices.RTCAdmin
{
	// Token: 0x020001C7 RID: 455
	public class UserToken : ISettable
	{
		// Token: 0x1700031F RID: 799
		// (get) Token: 0x06000C0D RID: 3085 RVA: 0x0000D1B4 File Offset: 0x0000B3B4
		// (set) Token: 0x06000C0E RID: 3086 RVA: 0x0000D1BC File Offset: 0x0000B3BC
		public ProductUserId ProductUserId { get; set; }

		// Token: 0x17000320 RID: 800
		// (get) Token: 0x06000C0F RID: 3087 RVA: 0x0000D1C5 File Offset: 0x0000B3C5
		// (set) Token: 0x06000C10 RID: 3088 RVA: 0x0000D1CD File Offset: 0x0000B3CD
		public string Token { get; set; }

		// Token: 0x06000C11 RID: 3089 RVA: 0x0000D1D8 File Offset: 0x0000B3D8
		internal void Set(UserTokenInternal? other)
		{
			if (other != null)
			{
				this.ProductUserId = other.Value.ProductUserId;
				this.Token = other.Value.Token;
			}
		}

		// Token: 0x06000C12 RID: 3090 RVA: 0x0000D218 File Offset: 0x0000B418
		public void Set(object other)
		{
			this.Set(other as UserTokenInternal?);
		}
	}
}
