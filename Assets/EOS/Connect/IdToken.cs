using System;

namespace Epic.OnlineServices.Connect
{
	// Token: 0x020004D1 RID: 1233
	public class IdToken : ISettable
	{
		// Token: 0x1700091B RID: 2331
		// (get) Token: 0x06001DE6 RID: 7654 RVA: 0x0001FD0B File Offset: 0x0001DF0B
		// (set) Token: 0x06001DE7 RID: 7655 RVA: 0x0001FD13 File Offset: 0x0001DF13
		public ProductUserId ProductUserId { get; set; }

		// Token: 0x1700091C RID: 2332
		// (get) Token: 0x06001DE8 RID: 7656 RVA: 0x0001FD1C File Offset: 0x0001DF1C
		// (set) Token: 0x06001DE9 RID: 7657 RVA: 0x0001FD24 File Offset: 0x0001DF24
		public string JsonWebToken { get; set; }

		// Token: 0x06001DEA RID: 7658 RVA: 0x0001FD30 File Offset: 0x0001DF30
		internal void Set(IdTokenInternal? other)
		{
			if (other != null)
			{
				this.ProductUserId = other.Value.ProductUserId;
				this.JsonWebToken = other.Value.JsonWebToken;
			}
		}

		// Token: 0x06001DEB RID: 7659 RVA: 0x0001FD70 File Offset: 0x0001DF70
		public void Set(object other)
		{
			this.Set(other as IdTokenInternal?);
		}
	}
}
