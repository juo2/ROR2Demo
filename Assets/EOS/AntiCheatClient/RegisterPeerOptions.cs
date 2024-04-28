using System;
using Epic.OnlineServices.AntiCheatCommon;

namespace Epic.OnlineServices.AntiCheatClient
{
	// Token: 0x020005D2 RID: 1490
	public class RegisterPeerOptions
	{
		// Token: 0x17000B2C RID: 2860
		// (get) Token: 0x060023E9 RID: 9193 RVA: 0x00025E54 File Offset: 0x00024054
		// (set) Token: 0x060023EA RID: 9194 RVA: 0x00025E5C File Offset: 0x0002405C
		public IntPtr PeerHandle { get; set; }

		// Token: 0x17000B2D RID: 2861
		// (get) Token: 0x060023EB RID: 9195 RVA: 0x00025E65 File Offset: 0x00024065
		// (set) Token: 0x060023EC RID: 9196 RVA: 0x00025E6D File Offset: 0x0002406D
		public AntiCheatCommonClientType ClientType { get; set; }

		// Token: 0x17000B2E RID: 2862
		// (get) Token: 0x060023ED RID: 9197 RVA: 0x00025E76 File Offset: 0x00024076
		// (set) Token: 0x060023EE RID: 9198 RVA: 0x00025E7E File Offset: 0x0002407E
		public AntiCheatCommonClientPlatform ClientPlatform { get; set; }

		// Token: 0x17000B2F RID: 2863
		// (get) Token: 0x060023EF RID: 9199 RVA: 0x00025E87 File Offset: 0x00024087
		// (set) Token: 0x060023F0 RID: 9200 RVA: 0x00025E8F File Offset: 0x0002408F
		public string AccountId { get; set; }

		// Token: 0x17000B30 RID: 2864
		// (get) Token: 0x060023F1 RID: 9201 RVA: 0x00025E98 File Offset: 0x00024098
		// (set) Token: 0x060023F2 RID: 9202 RVA: 0x00025EA0 File Offset: 0x000240A0
		public string IpAddress { get; set; }
	}
}
