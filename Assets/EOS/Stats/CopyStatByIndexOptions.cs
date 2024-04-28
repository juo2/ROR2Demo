using System;

namespace Epic.OnlineServices.Stats
{
	// Token: 0x0200008E RID: 142
	public class CopyStatByIndexOptions
	{
		// Token: 0x170000ED RID: 237
		// (get) Token: 0x0600053B RID: 1339 RVA: 0x0000637B File Offset: 0x0000457B
		// (set) Token: 0x0600053C RID: 1340 RVA: 0x00006383 File Offset: 0x00004583
		public ProductUserId TargetUserId { get; set; }

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x0600053D RID: 1341 RVA: 0x0000638C File Offset: 0x0000458C
		// (set) Token: 0x0600053E RID: 1342 RVA: 0x00006394 File Offset: 0x00004594
		public uint StatIndex { get; set; }
	}
}
