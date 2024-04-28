using System;

namespace Epic.OnlineServices.AntiCheatServer
{
	// Token: 0x0200055B RID: 1371
	public class BeginSessionOptions
	{
		// Token: 0x17000A39 RID: 2617
		// (get) Token: 0x06002151 RID: 8529 RVA: 0x00023548 File Offset: 0x00021748
		// (set) Token: 0x06002152 RID: 8530 RVA: 0x00023550 File Offset: 0x00021750
		public uint RegisterTimeoutSeconds { get; set; }

		// Token: 0x17000A3A RID: 2618
		// (get) Token: 0x06002153 RID: 8531 RVA: 0x00023559 File Offset: 0x00021759
		// (set) Token: 0x06002154 RID: 8532 RVA: 0x00023561 File Offset: 0x00021761
		public string ServerName { get; set; }

		// Token: 0x17000A3B RID: 2619
		// (get) Token: 0x06002155 RID: 8533 RVA: 0x0002356A File Offset: 0x0002176A
		// (set) Token: 0x06002156 RID: 8534 RVA: 0x00023572 File Offset: 0x00021772
		public bool EnableGameplayData { get; set; }

		// Token: 0x17000A3C RID: 2620
		// (get) Token: 0x06002157 RID: 8535 RVA: 0x0002357B File Offset: 0x0002177B
		// (set) Token: 0x06002158 RID: 8536 RVA: 0x00023583 File Offset: 0x00021783
		public ProductUserId LocalUserId { get; set; }
	}
}
