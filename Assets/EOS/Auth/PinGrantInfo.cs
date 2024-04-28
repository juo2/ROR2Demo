using System;

namespace Epic.OnlineServices.Auth
{
	// Token: 0x0200053E RID: 1342
	public class PinGrantInfo : ISettable
	{
		// Token: 0x170009DC RID: 2524
		// (get) Token: 0x06002052 RID: 8274 RVA: 0x00021F2C File Offset: 0x0002012C
		// (set) Token: 0x06002053 RID: 8275 RVA: 0x00021F34 File Offset: 0x00020134
		public string UserCode { get; set; }

		// Token: 0x170009DD RID: 2525
		// (get) Token: 0x06002054 RID: 8276 RVA: 0x00021F3D File Offset: 0x0002013D
		// (set) Token: 0x06002055 RID: 8277 RVA: 0x00021F45 File Offset: 0x00020145
		public string VerificationURI { get; set; }

		// Token: 0x170009DE RID: 2526
		// (get) Token: 0x06002056 RID: 8278 RVA: 0x00021F4E File Offset: 0x0002014E
		// (set) Token: 0x06002057 RID: 8279 RVA: 0x00021F56 File Offset: 0x00020156
		public int ExpiresIn { get; set; }

		// Token: 0x170009DF RID: 2527
		// (get) Token: 0x06002058 RID: 8280 RVA: 0x00021F5F File Offset: 0x0002015F
		// (set) Token: 0x06002059 RID: 8281 RVA: 0x00021F67 File Offset: 0x00020167
		public string VerificationURIComplete { get; set; }

		// Token: 0x0600205A RID: 8282 RVA: 0x00021F70 File Offset: 0x00020170
		internal void Set(PinGrantInfoInternal? other)
		{
			if (other != null)
			{
				this.UserCode = other.Value.UserCode;
				this.VerificationURI = other.Value.VerificationURI;
				this.ExpiresIn = other.Value.ExpiresIn;
				this.VerificationURIComplete = other.Value.VerificationURIComplete;
			}
		}

		// Token: 0x0600205B RID: 8283 RVA: 0x00021FDA File Offset: 0x000201DA
		public void Set(object other)
		{
			this.Set(other as PinGrantInfoInternal?);
		}
	}
}
