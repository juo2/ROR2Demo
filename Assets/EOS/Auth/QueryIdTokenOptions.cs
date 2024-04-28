using System;

namespace Epic.OnlineServices.Auth
{
	// Token: 0x02000542 RID: 1346
	public class QueryIdTokenOptions
	{
		// Token: 0x170009ED RID: 2541
		// (get) Token: 0x06002079 RID: 8313 RVA: 0x00022228 File Offset: 0x00020428
		// (set) Token: 0x0600207A RID: 8314 RVA: 0x00022230 File Offset: 0x00020430
		public EpicAccountId LocalUserId { get; set; }

		// Token: 0x170009EE RID: 2542
		// (get) Token: 0x0600207B RID: 8315 RVA: 0x00022239 File Offset: 0x00020439
		// (set) Token: 0x0600207C RID: 8316 RVA: 0x00022241 File Offset: 0x00020441
		public EpicAccountId TargetAccountId { get; set; }
	}
}
