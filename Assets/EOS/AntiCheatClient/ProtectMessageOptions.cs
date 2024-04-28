using System;

namespace Epic.OnlineServices.AntiCheatClient
{
	// Token: 0x020005CC RID: 1484
	public class ProtectMessageOptions
	{
		// Token: 0x17000B22 RID: 2850
		// (get) Token: 0x060023CE RID: 9166 RVA: 0x00025CEE File Offset: 0x00023EEE
		// (set) Token: 0x060023CF RID: 9167 RVA: 0x00025CF6 File Offset: 0x00023EF6
		public byte[] Data { get; set; }

		// Token: 0x17000B23 RID: 2851
		// (get) Token: 0x060023D0 RID: 9168 RVA: 0x00025CFF File Offset: 0x00023EFF
		// (set) Token: 0x060023D1 RID: 9169 RVA: 0x00025D07 File Offset: 0x00023F07
		public uint OutBufferSizeBytes { get; set; }
	}
}
