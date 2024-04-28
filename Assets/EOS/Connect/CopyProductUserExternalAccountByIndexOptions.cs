using System;

namespace Epic.OnlineServices.Connect
{
	// Token: 0x020004B7 RID: 1207
	public class CopyProductUserExternalAccountByIndexOptions
	{
		// Token: 0x170008E4 RID: 2276
		// (get) Token: 0x06001D56 RID: 7510 RVA: 0x0001F44E File Offset: 0x0001D64E
		// (set) Token: 0x06001D57 RID: 7511 RVA: 0x0001F456 File Offset: 0x0001D656
		public ProductUserId TargetUserId { get; set; }

		// Token: 0x170008E5 RID: 2277
		// (get) Token: 0x06001D58 RID: 7512 RVA: 0x0001F45F File Offset: 0x0001D65F
		// (set) Token: 0x06001D59 RID: 7513 RVA: 0x0001F467 File Offset: 0x0001D667
		public uint ExternalAccountInfoIndex { get; set; }
	}
}
