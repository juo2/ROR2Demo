using System;

namespace Epic.OnlineServices.AntiCheatCommon
{
	// Token: 0x02000589 RID: 1417
	public class LogGameRoundStartOptions
	{
		// Token: 0x17000A7E RID: 2686
		// (get) Token: 0x0600221A RID: 8730 RVA: 0x00024226 File Offset: 0x00022426
		// (set) Token: 0x0600221B RID: 8731 RVA: 0x0002422E File Offset: 0x0002242E
		public string SessionIdentifier { get; set; }

		// Token: 0x17000A7F RID: 2687
		// (get) Token: 0x0600221C RID: 8732 RVA: 0x00024237 File Offset: 0x00022437
		// (set) Token: 0x0600221D RID: 8733 RVA: 0x0002423F File Offset: 0x0002243F
		public string LevelName { get; set; }

		// Token: 0x17000A80 RID: 2688
		// (get) Token: 0x0600221E RID: 8734 RVA: 0x00024248 File Offset: 0x00022448
		// (set) Token: 0x0600221F RID: 8735 RVA: 0x00024250 File Offset: 0x00022450
		public string ModeName { get; set; }

		// Token: 0x17000A81 RID: 2689
		// (get) Token: 0x06002220 RID: 8736 RVA: 0x00024259 File Offset: 0x00022459
		// (set) Token: 0x06002221 RID: 8737 RVA: 0x00024261 File Offset: 0x00022461
		public uint RoundTimeSeconds { get; set; }
	}
}
