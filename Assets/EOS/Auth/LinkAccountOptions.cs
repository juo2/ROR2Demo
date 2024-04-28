using System;

namespace Epic.OnlineServices.Auth
{
	// Token: 0x02000521 RID: 1313
	public class LinkAccountOptions
	{
		// Token: 0x170009B1 RID: 2481
		// (get) Token: 0x06001FBB RID: 8123 RVA: 0x00021990 File Offset: 0x0001FB90
		// (set) Token: 0x06001FBC RID: 8124 RVA: 0x00021998 File Offset: 0x0001FB98
		public LinkAccountFlags LinkAccountFlags { get; set; }

		// Token: 0x170009B2 RID: 2482
		// (get) Token: 0x06001FBD RID: 8125 RVA: 0x000219A1 File Offset: 0x0001FBA1
		// (set) Token: 0x06001FBE RID: 8126 RVA: 0x000219A9 File Offset: 0x0001FBA9
		public ContinuanceToken ContinuanceToken { get; set; }

		// Token: 0x170009B3 RID: 2483
		// (get) Token: 0x06001FBF RID: 8127 RVA: 0x000219B2 File Offset: 0x0001FBB2
		// (set) Token: 0x06001FC0 RID: 8128 RVA: 0x000219BA File Offset: 0x0001FBBA
		public EpicAccountId LocalUserId { get; set; }
	}
}
