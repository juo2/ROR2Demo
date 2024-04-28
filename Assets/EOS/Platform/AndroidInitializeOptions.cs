using System;

namespace Epic.OnlineServices.Platform
{
	// Token: 0x020005D8 RID: 1496
	public class AndroidInitializeOptions
	{
		// Token: 0x17000B3C RID: 2876
		// (get) Token: 0x0600240D RID: 9229 RVA: 0x00026039 File Offset: 0x00024239
		// (set) Token: 0x0600240E RID: 9230 RVA: 0x00026041 File Offset: 0x00024241
		public IntPtr AllocateMemoryFunction { get; set; }

		// Token: 0x17000B3D RID: 2877
		// (get) Token: 0x0600240F RID: 9231 RVA: 0x0002604A File Offset: 0x0002424A
		// (set) Token: 0x06002410 RID: 9232 RVA: 0x00026052 File Offset: 0x00024252
		public IntPtr ReallocateMemoryFunction { get; set; }

		// Token: 0x17000B3E RID: 2878
		// (get) Token: 0x06002411 RID: 9233 RVA: 0x0002605B File Offset: 0x0002425B
		// (set) Token: 0x06002412 RID: 9234 RVA: 0x00026063 File Offset: 0x00024263
		public IntPtr ReleaseMemoryFunction { get; set; }

		// Token: 0x17000B3F RID: 2879
		// (get) Token: 0x06002413 RID: 9235 RVA: 0x0002606C File Offset: 0x0002426C
		// (set) Token: 0x06002414 RID: 9236 RVA: 0x00026074 File Offset: 0x00024274
		public string ProductName { get; set; }

		// Token: 0x17000B40 RID: 2880
		// (get) Token: 0x06002415 RID: 9237 RVA: 0x0002607D File Offset: 0x0002427D
		// (set) Token: 0x06002416 RID: 9238 RVA: 0x00026085 File Offset: 0x00024285
		public string ProductVersion { get; set; }

		// Token: 0x17000B41 RID: 2881
		// (get) Token: 0x06002417 RID: 9239 RVA: 0x0002608E File Offset: 0x0002428E
		// (set) Token: 0x06002418 RID: 9240 RVA: 0x00026096 File Offset: 0x00024296
		public IntPtr Reserved { get; set; }

		// Token: 0x17000B42 RID: 2882
		// (get) Token: 0x06002419 RID: 9241 RVA: 0x0002609F File Offset: 0x0002429F
		// (set) Token: 0x0600241A RID: 9242 RVA: 0x000260A7 File Offset: 0x000242A7
		public AndroidInitializeOptionsSystemInitializeOptions SystemInitializeOptions { get; set; }

		// Token: 0x17000B43 RID: 2883
		// (get) Token: 0x0600241B RID: 9243 RVA: 0x000260B0 File Offset: 0x000242B0
		// (set) Token: 0x0600241C RID: 9244 RVA: 0x000260B8 File Offset: 0x000242B8
		public InitializeThreadAffinity OverrideThreadAffinity { get; set; }
	}
}
