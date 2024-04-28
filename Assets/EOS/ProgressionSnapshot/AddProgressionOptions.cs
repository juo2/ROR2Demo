using System;

namespace Epic.OnlineServices.ProgressionSnapshot
{
	// Token: 0x020001F0 RID: 496
	public class AddProgressionOptions
	{
		// Token: 0x1700038C RID: 908
		// (get) Token: 0x06000D27 RID: 3367 RVA: 0x0000E3FC File Offset: 0x0000C5FC
		// (set) Token: 0x06000D28 RID: 3368 RVA: 0x0000E404 File Offset: 0x0000C604
		public uint SnapshotId { get; set; }

		// Token: 0x1700038D RID: 909
		// (get) Token: 0x06000D29 RID: 3369 RVA: 0x0000E40D File Offset: 0x0000C60D
		// (set) Token: 0x06000D2A RID: 3370 RVA: 0x0000E415 File Offset: 0x0000C615
		public string Key { get; set; }

		// Token: 0x1700038E RID: 910
		// (get) Token: 0x06000D2B RID: 3371 RVA: 0x0000E41E File Offset: 0x0000C61E
		// (set) Token: 0x06000D2C RID: 3372 RVA: 0x0000E426 File Offset: 0x0000C626
		public string Value { get; set; }
	}
}
