using System;

namespace Epic.OnlineServices.Platform
{
	// Token: 0x020005E0 RID: 1504
	public class InitializeOptions
	{
		// Token: 0x17000B56 RID: 2902
		// (get) Token: 0x06002477 RID: 9335 RVA: 0x00026AA4 File Offset: 0x00024CA4
		// (set) Token: 0x06002478 RID: 9336 RVA: 0x00026AAC File Offset: 0x00024CAC
		public IntPtr AllocateMemoryFunction { get; set; }

		// Token: 0x17000B57 RID: 2903
		// (get) Token: 0x06002479 RID: 9337 RVA: 0x00026AB5 File Offset: 0x00024CB5
		// (set) Token: 0x0600247A RID: 9338 RVA: 0x00026ABD File Offset: 0x00024CBD
		public IntPtr ReallocateMemoryFunction { get; set; }

		// Token: 0x17000B58 RID: 2904
		// (get) Token: 0x0600247B RID: 9339 RVA: 0x00026AC6 File Offset: 0x00024CC6
		// (set) Token: 0x0600247C RID: 9340 RVA: 0x00026ACE File Offset: 0x00024CCE
		public IntPtr ReleaseMemoryFunction { get; set; }

		// Token: 0x17000B59 RID: 2905
		// (get) Token: 0x0600247D RID: 9341 RVA: 0x00026AD7 File Offset: 0x00024CD7
		// (set) Token: 0x0600247E RID: 9342 RVA: 0x00026ADF File Offset: 0x00024CDF
		public string ProductName { get; set; }

		// Token: 0x17000B5A RID: 2906
		// (get) Token: 0x0600247F RID: 9343 RVA: 0x00026AE8 File Offset: 0x00024CE8
		// (set) Token: 0x06002480 RID: 9344 RVA: 0x00026AF0 File Offset: 0x00024CF0
		public string ProductVersion { get; set; }

		// Token: 0x17000B5B RID: 2907
		// (get) Token: 0x06002481 RID: 9345 RVA: 0x00026AF9 File Offset: 0x00024CF9
		// (set) Token: 0x06002482 RID: 9346 RVA: 0x00026B01 File Offset: 0x00024D01
		public IntPtr SystemInitializeOptions { get; set; }

		// Token: 0x17000B5C RID: 2908
		// (get) Token: 0x06002483 RID: 9347 RVA: 0x00026B0A File Offset: 0x00024D0A
		// (set) Token: 0x06002484 RID: 9348 RVA: 0x00026B12 File Offset: 0x00024D12
		public InitializeThreadAffinity OverrideThreadAffinity { get; set; }
	}
}
