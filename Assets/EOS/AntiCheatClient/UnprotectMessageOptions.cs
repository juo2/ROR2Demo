using System;

namespace Epic.OnlineServices.AntiCheatClient
{
	// Token: 0x020005D4 RID: 1492
	public class UnprotectMessageOptions
	{
		// Token: 0x17000B36 RID: 2870
		// (get) Token: 0x060023FC RID: 9212 RVA: 0x00025F6B File Offset: 0x0002416B
		// (set) Token: 0x060023FD RID: 9213 RVA: 0x00025F73 File Offset: 0x00024173
		public byte[] Data { get; set; }

		// Token: 0x17000B37 RID: 2871
		// (get) Token: 0x060023FE RID: 9214 RVA: 0x00025F7C File Offset: 0x0002417C
		// (set) Token: 0x060023FF RID: 9215 RVA: 0x00025F84 File Offset: 0x00024184
		public uint OutBufferSizeBytes { get; set; }
	}
}
