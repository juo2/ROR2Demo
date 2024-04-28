using System;

namespace Epic.OnlineServices.AntiCheatClient
{
	// Token: 0x020005BA RID: 1466
	public class BeginSessionOptions
	{
		// Token: 0x17000B15 RID: 2837
		// (get) Token: 0x06002387 RID: 9095 RVA: 0x00025B04 File Offset: 0x00023D04
		// (set) Token: 0x06002388 RID: 9096 RVA: 0x00025B0C File Offset: 0x00023D0C
		public ProductUserId LocalUserId { get; set; }

		// Token: 0x17000B16 RID: 2838
		// (get) Token: 0x06002389 RID: 9097 RVA: 0x00025B15 File Offset: 0x00023D15
		// (set) Token: 0x0600238A RID: 9098 RVA: 0x00025B1D File Offset: 0x00023D1D
		public AntiCheatClientMode Mode { get; set; }
	}
}
