using System;

namespace Epic.OnlineServices.AntiCheatServer
{
	// Token: 0x0200056F RID: 1391
	public class UnprotectMessageOptions
	{
		// Token: 0x17000A5B RID: 2651
		// (get) Token: 0x060021B8 RID: 8632 RVA: 0x0002394D File Offset: 0x00021B4D
		// (set) Token: 0x060021B9 RID: 8633 RVA: 0x00023955 File Offset: 0x00021B55
		public IntPtr ClientHandle { get; set; }

		// Token: 0x17000A5C RID: 2652
		// (get) Token: 0x060021BA RID: 8634 RVA: 0x0002395E File Offset: 0x00021B5E
		// (set) Token: 0x060021BB RID: 8635 RVA: 0x00023966 File Offset: 0x00021B66
		public byte[] Data { get; set; }

		// Token: 0x17000A5D RID: 2653
		// (get) Token: 0x060021BC RID: 8636 RVA: 0x0002396F File Offset: 0x00021B6F
		// (set) Token: 0x060021BD RID: 8637 RVA: 0x00023977 File Offset: 0x00021B77
		public uint OutBufferSizeBytes { get; set; }
	}
}
