using System;

namespace Epic.OnlineServices.Platform
{
	// Token: 0x020005EB RID: 1515
	public class WindowsOptions
	{
		// Token: 0x17000B8A RID: 2954
		// (get) Token: 0x060024E7 RID: 9447 RVA: 0x0002718F File Offset: 0x0002538F
		// (set) Token: 0x060024E8 RID: 9448 RVA: 0x00027197 File Offset: 0x00025397
		public IntPtr Reserved { get; set; }

		// Token: 0x17000B8B RID: 2955
		// (get) Token: 0x060024E9 RID: 9449 RVA: 0x000271A0 File Offset: 0x000253A0
		// (set) Token: 0x060024EA RID: 9450 RVA: 0x000271A8 File Offset: 0x000253A8
		public string ProductId { get; set; }

		// Token: 0x17000B8C RID: 2956
		// (get) Token: 0x060024EB RID: 9451 RVA: 0x000271B1 File Offset: 0x000253B1
		// (set) Token: 0x060024EC RID: 9452 RVA: 0x000271B9 File Offset: 0x000253B9
		public string SandboxId { get; set; }

		// Token: 0x17000B8D RID: 2957
		// (get) Token: 0x060024ED RID: 9453 RVA: 0x000271C2 File Offset: 0x000253C2
		// (set) Token: 0x060024EE RID: 9454 RVA: 0x000271CA File Offset: 0x000253CA
		public ClientCredentials ClientCredentials { get; set; }

		// Token: 0x17000B8E RID: 2958
		// (get) Token: 0x060024EF RID: 9455 RVA: 0x000271D3 File Offset: 0x000253D3
		// (set) Token: 0x060024F0 RID: 9456 RVA: 0x000271DB File Offset: 0x000253DB
		public bool IsServer { get; set; }

		// Token: 0x17000B8F RID: 2959
		// (get) Token: 0x060024F1 RID: 9457 RVA: 0x000271E4 File Offset: 0x000253E4
		// (set) Token: 0x060024F2 RID: 9458 RVA: 0x000271EC File Offset: 0x000253EC
		public string EncryptionKey { get; set; }

		// Token: 0x17000B90 RID: 2960
		// (get) Token: 0x060024F3 RID: 9459 RVA: 0x000271F5 File Offset: 0x000253F5
		// (set) Token: 0x060024F4 RID: 9460 RVA: 0x000271FD File Offset: 0x000253FD
		public string OverrideCountryCode { get; set; }

		// Token: 0x17000B91 RID: 2961
		// (get) Token: 0x060024F5 RID: 9461 RVA: 0x00027206 File Offset: 0x00025406
		// (set) Token: 0x060024F6 RID: 9462 RVA: 0x0002720E File Offset: 0x0002540E
		public string OverrideLocaleCode { get; set; }

		// Token: 0x17000B92 RID: 2962
		// (get) Token: 0x060024F7 RID: 9463 RVA: 0x00027217 File Offset: 0x00025417
		// (set) Token: 0x060024F8 RID: 9464 RVA: 0x0002721F File Offset: 0x0002541F
		public string DeploymentId { get; set; }

		// Token: 0x17000B93 RID: 2963
		// (get) Token: 0x060024F9 RID: 9465 RVA: 0x00027228 File Offset: 0x00025428
		// (set) Token: 0x060024FA RID: 9466 RVA: 0x00027230 File Offset: 0x00025430
		public PlatformFlags Flags { get; set; }

		// Token: 0x17000B94 RID: 2964
		// (get) Token: 0x060024FB RID: 9467 RVA: 0x00027239 File Offset: 0x00025439
		// (set) Token: 0x060024FC RID: 9468 RVA: 0x00027241 File Offset: 0x00025441
		public string CacheDirectory { get; set; }

		// Token: 0x17000B95 RID: 2965
		// (get) Token: 0x060024FD RID: 9469 RVA: 0x0002724A File Offset: 0x0002544A
		// (set) Token: 0x060024FE RID: 9470 RVA: 0x00027252 File Offset: 0x00025452
		public uint TickBudgetInMilliseconds { get; set; }

		// Token: 0x17000B96 RID: 2966
		// (get) Token: 0x060024FF RID: 9471 RVA: 0x0002725B File Offset: 0x0002545B
		// (set) Token: 0x06002500 RID: 9472 RVA: 0x00027263 File Offset: 0x00025463
		public WindowsRTCOptions RTCOptions { get; set; }
	}
}
