using System;

namespace Epic.OnlineServices.Connect
{
	// Token: 0x020004B5 RID: 1205
	public class CopyProductUserExternalAccountByAccountTypeOptions
	{
		// Token: 0x170008E0 RID: 2272
		// (get) Token: 0x06001D4C RID: 7500 RVA: 0x0001F3D4 File Offset: 0x0001D5D4
		// (set) Token: 0x06001D4D RID: 7501 RVA: 0x0001F3DC File Offset: 0x0001D5DC
		public ProductUserId TargetUserId { get; set; }

		// Token: 0x170008E1 RID: 2273
		// (get) Token: 0x06001D4E RID: 7502 RVA: 0x0001F3E5 File Offset: 0x0001D5E5
		// (set) Token: 0x06001D4F RID: 7503 RVA: 0x0001F3ED File Offset: 0x0001D5ED
		public ExternalAccountType AccountIdType { get; set; }
	}
}
