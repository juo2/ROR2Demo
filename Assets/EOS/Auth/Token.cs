using System;

namespace Epic.OnlineServices.Auth
{
	// Token: 0x02000544 RID: 1348
	public class Token : ISettable
	{
		// Token: 0x170009F1 RID: 2545
		// (get) Token: 0x06002083 RID: 8323 RVA: 0x000222B4 File Offset: 0x000204B4
		// (set) Token: 0x06002084 RID: 8324 RVA: 0x000222BC File Offset: 0x000204BC
		public string App { get; set; }

		// Token: 0x170009F2 RID: 2546
		// (get) Token: 0x06002085 RID: 8325 RVA: 0x000222C5 File Offset: 0x000204C5
		// (set) Token: 0x06002086 RID: 8326 RVA: 0x000222CD File Offset: 0x000204CD
		public string ClientId { get; set; }

		// Token: 0x170009F3 RID: 2547
		// (get) Token: 0x06002087 RID: 8327 RVA: 0x000222D6 File Offset: 0x000204D6
		// (set) Token: 0x06002088 RID: 8328 RVA: 0x000222DE File Offset: 0x000204DE
		public EpicAccountId AccountId { get; set; }

		// Token: 0x170009F4 RID: 2548
		// (get) Token: 0x06002089 RID: 8329 RVA: 0x000222E7 File Offset: 0x000204E7
		// (set) Token: 0x0600208A RID: 8330 RVA: 0x000222EF File Offset: 0x000204EF
		public string AccessToken { get; set; }

		// Token: 0x170009F5 RID: 2549
		// (get) Token: 0x0600208B RID: 8331 RVA: 0x000222F8 File Offset: 0x000204F8
		// (set) Token: 0x0600208C RID: 8332 RVA: 0x00022300 File Offset: 0x00020500
		public double ExpiresIn { get; set; }

		// Token: 0x170009F6 RID: 2550
		// (get) Token: 0x0600208D RID: 8333 RVA: 0x00022309 File Offset: 0x00020509
		// (set) Token: 0x0600208E RID: 8334 RVA: 0x00022311 File Offset: 0x00020511
		public string ExpiresAt { get; set; }

		// Token: 0x170009F7 RID: 2551
		// (get) Token: 0x0600208F RID: 8335 RVA: 0x0002231A File Offset: 0x0002051A
		// (set) Token: 0x06002090 RID: 8336 RVA: 0x00022322 File Offset: 0x00020522
		public AuthTokenType AuthType { get; set; }

		// Token: 0x170009F8 RID: 2552
		// (get) Token: 0x06002091 RID: 8337 RVA: 0x0002232B File Offset: 0x0002052B
		// (set) Token: 0x06002092 RID: 8338 RVA: 0x00022333 File Offset: 0x00020533
		public string RefreshToken { get; set; }

		// Token: 0x170009F9 RID: 2553
		// (get) Token: 0x06002093 RID: 8339 RVA: 0x0002233C File Offset: 0x0002053C
		// (set) Token: 0x06002094 RID: 8340 RVA: 0x00022344 File Offset: 0x00020544
		public double RefreshExpiresIn { get; set; }

		// Token: 0x170009FA RID: 2554
		// (get) Token: 0x06002095 RID: 8341 RVA: 0x0002234D File Offset: 0x0002054D
		// (set) Token: 0x06002096 RID: 8342 RVA: 0x00022355 File Offset: 0x00020555
		public string RefreshExpiresAt { get; set; }

		// Token: 0x06002097 RID: 8343 RVA: 0x00022360 File Offset: 0x00020560
		internal void Set(TokenInternal? other)
		{
			if (other != null)
			{
				this.App = other.Value.App;
				this.ClientId = other.Value.ClientId;
				this.AccountId = other.Value.AccountId;
				this.AccessToken = other.Value.AccessToken;
				this.ExpiresIn = other.Value.ExpiresIn;
				this.ExpiresAt = other.Value.ExpiresAt;
				this.AuthType = other.Value.AuthType;
				this.RefreshToken = other.Value.RefreshToken;
				this.RefreshExpiresIn = other.Value.RefreshExpiresIn;
				this.RefreshExpiresAt = other.Value.RefreshExpiresAt;
			}
		}

		// Token: 0x06002098 RID: 8344 RVA: 0x0002244B File Offset: 0x0002064B
		public void Set(object other)
		{
			this.Set(other as TokenInternal?);
		}
	}
}
