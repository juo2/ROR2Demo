using System;

namespace Epic.OnlineServices.Platform
{
	// Token: 0x020005E4 RID: 1508
	public class Options
	{
		// Token: 0x17000B6E RID: 2926
		// (get) Token: 0x060024AA RID: 9386 RVA: 0x00026E1D File Offset: 0x0002501D
		// (set) Token: 0x060024AB RID: 9387 RVA: 0x00026E25 File Offset: 0x00025025
		public IntPtr Reserved { get; set; }

		// Token: 0x17000B6F RID: 2927
		// (get) Token: 0x060024AC RID: 9388 RVA: 0x00026E2E File Offset: 0x0002502E
		// (set) Token: 0x060024AD RID: 9389 RVA: 0x00026E36 File Offset: 0x00025036
		public string ProductId { get; set; }

		// Token: 0x17000B70 RID: 2928
		// (get) Token: 0x060024AE RID: 9390 RVA: 0x00026E3F File Offset: 0x0002503F
		// (set) Token: 0x060024AF RID: 9391 RVA: 0x00026E47 File Offset: 0x00025047
		public string SandboxId { get; set; }

		// Token: 0x17000B71 RID: 2929
		// (get) Token: 0x060024B0 RID: 9392 RVA: 0x00026E50 File Offset: 0x00025050
		// (set) Token: 0x060024B1 RID: 9393 RVA: 0x00026E58 File Offset: 0x00025058
		public ClientCredentials ClientCredentials { get; set; }

		// Token: 0x17000B72 RID: 2930
		// (get) Token: 0x060024B2 RID: 9394 RVA: 0x00026E61 File Offset: 0x00025061
		// (set) Token: 0x060024B3 RID: 9395 RVA: 0x00026E69 File Offset: 0x00025069
		public bool IsServer { get; set; }

		// Token: 0x17000B73 RID: 2931
		// (get) Token: 0x060024B4 RID: 9396 RVA: 0x00026E72 File Offset: 0x00025072
		// (set) Token: 0x060024B5 RID: 9397 RVA: 0x00026E7A File Offset: 0x0002507A
		public string EncryptionKey { get; set; }

		// Token: 0x17000B74 RID: 2932
		// (get) Token: 0x060024B6 RID: 9398 RVA: 0x00026E83 File Offset: 0x00025083
		// (set) Token: 0x060024B7 RID: 9399 RVA: 0x00026E8B File Offset: 0x0002508B
		public string OverrideCountryCode { get; set; }

		// Token: 0x17000B75 RID: 2933
		// (get) Token: 0x060024B8 RID: 9400 RVA: 0x00026E94 File Offset: 0x00025094
		// (set) Token: 0x060024B9 RID: 9401 RVA: 0x00026E9C File Offset: 0x0002509C
		public string OverrideLocaleCode { get; set; }

		// Token: 0x17000B76 RID: 2934
		// (get) Token: 0x060024BA RID: 9402 RVA: 0x00026EA5 File Offset: 0x000250A5
		// (set) Token: 0x060024BB RID: 9403 RVA: 0x00026EAD File Offset: 0x000250AD
		public string DeploymentId { get; set; }

		// Token: 0x17000B77 RID: 2935
		// (get) Token: 0x060024BC RID: 9404 RVA: 0x00026EB6 File Offset: 0x000250B6
		// (set) Token: 0x060024BD RID: 9405 RVA: 0x00026EBE File Offset: 0x000250BE
		public PlatformFlags Flags { get; set; }

		// Token: 0x17000B78 RID: 2936
		// (get) Token: 0x060024BE RID: 9406 RVA: 0x00026EC7 File Offset: 0x000250C7
		// (set) Token: 0x060024BF RID: 9407 RVA: 0x00026ECF File Offset: 0x000250CF
		public string CacheDirectory { get; set; }

		// Token: 0x17000B79 RID: 2937
		// (get) Token: 0x060024C0 RID: 9408 RVA: 0x00026ED8 File Offset: 0x000250D8
		// (set) Token: 0x060024C1 RID: 9409 RVA: 0x00026EE0 File Offset: 0x000250E0
		public uint TickBudgetInMilliseconds { get; set; }

		// Token: 0x17000B7A RID: 2938
		// (get) Token: 0x060024C2 RID: 9410 RVA: 0x00026EE9 File Offset: 0x000250E9
		// (set) Token: 0x060024C3 RID: 9411 RVA: 0x00026EF1 File Offset: 0x000250F1
		public RTCOptions RTCOptions { get; set; }
	}
}
