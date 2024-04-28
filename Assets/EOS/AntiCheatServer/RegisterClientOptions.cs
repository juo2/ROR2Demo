using System;
using Epic.OnlineServices.AntiCheatCommon;

namespace Epic.OnlineServices.AntiCheatServer
{
	// Token: 0x0200056B RID: 1387
	public class RegisterClientOptions
	{
		// Token: 0x17000A4D RID: 2637
		// (get) Token: 0x0600219B RID: 8603 RVA: 0x000237BE File Offset: 0x000219BE
		// (set) Token: 0x0600219C RID: 8604 RVA: 0x000237C6 File Offset: 0x000219C6
		public IntPtr ClientHandle { get; set; }

		// Token: 0x17000A4E RID: 2638
		// (get) Token: 0x0600219D RID: 8605 RVA: 0x000237CF File Offset: 0x000219CF
		// (set) Token: 0x0600219E RID: 8606 RVA: 0x000237D7 File Offset: 0x000219D7
		public AntiCheatCommonClientType ClientType { get; set; }

		// Token: 0x17000A4F RID: 2639
		// (get) Token: 0x0600219F RID: 8607 RVA: 0x000237E0 File Offset: 0x000219E0
		// (set) Token: 0x060021A0 RID: 8608 RVA: 0x000237E8 File Offset: 0x000219E8
		public AntiCheatCommonClientPlatform ClientPlatform { get; set; }

		// Token: 0x17000A50 RID: 2640
		// (get) Token: 0x060021A1 RID: 8609 RVA: 0x000237F1 File Offset: 0x000219F1
		// (set) Token: 0x060021A2 RID: 8610 RVA: 0x000237F9 File Offset: 0x000219F9
		public string AccountId { get; set; }

		// Token: 0x17000A51 RID: 2641
		// (get) Token: 0x060021A3 RID: 8611 RVA: 0x00023802 File Offset: 0x00021A02
		// (set) Token: 0x060021A4 RID: 8612 RVA: 0x0002380A File Offset: 0x00021A0A
		public string IpAddress { get; set; }
	}
}
