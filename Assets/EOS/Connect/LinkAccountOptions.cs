using System;

namespace Epic.OnlineServices.Connect
{
	// Token: 0x020004D5 RID: 1237
	public class LinkAccountOptions
	{
		// Token: 0x17000926 RID: 2342
		// (get) Token: 0x06001E02 RID: 7682 RVA: 0x0001FF18 File Offset: 0x0001E118
		// (set) Token: 0x06001E03 RID: 7683 RVA: 0x0001FF20 File Offset: 0x0001E120
		public ProductUserId LocalUserId { get; set; }

		// Token: 0x17000927 RID: 2343
		// (get) Token: 0x06001E04 RID: 7684 RVA: 0x0001FF29 File Offset: 0x0001E129
		// (set) Token: 0x06001E05 RID: 7685 RVA: 0x0001FF31 File Offset: 0x0001E131
		public ContinuanceToken ContinuanceToken { get; set; }
	}
}
