using System;

namespace Epic.OnlineServices.Ecom
{
	// Token: 0x0200048C RID: 1164
	public class RedeemEntitlementsOptions
	{
		// Token: 0x170008A0 RID: 2208
		// (get) Token: 0x06001C5D RID: 7261 RVA: 0x0001DF60 File Offset: 0x0001C160
		// (set) Token: 0x06001C5E RID: 7262 RVA: 0x0001DF68 File Offset: 0x0001C168
		public EpicAccountId LocalUserId { get; set; }

		// Token: 0x170008A1 RID: 2209
		// (get) Token: 0x06001C5F RID: 7263 RVA: 0x0001DF71 File Offset: 0x0001C171
		// (set) Token: 0x06001C60 RID: 7264 RVA: 0x0001DF79 File Offset: 0x0001C179
		public string[] EntitlementIds { get; set; }
	}
}
