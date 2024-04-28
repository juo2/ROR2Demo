using System;

namespace Epic.OnlineServices.AntiCheatCommon
{
	// Token: 0x020005A3 RID: 1443
	public class RegisterEventOptions
	{
		// Token: 0x17000AF9 RID: 2809
		// (get) Token: 0x06002313 RID: 8979 RVA: 0x0002516C File Offset: 0x0002336C
		// (set) Token: 0x06002314 RID: 8980 RVA: 0x00025174 File Offset: 0x00023374
		public uint EventId { get; set; }

		// Token: 0x17000AFA RID: 2810
		// (get) Token: 0x06002315 RID: 8981 RVA: 0x0002517D File Offset: 0x0002337D
		// (set) Token: 0x06002316 RID: 8982 RVA: 0x00025185 File Offset: 0x00023385
		public string EventName { get; set; }

		// Token: 0x17000AFB RID: 2811
		// (get) Token: 0x06002317 RID: 8983 RVA: 0x0002518E File Offset: 0x0002338E
		// (set) Token: 0x06002318 RID: 8984 RVA: 0x00025196 File Offset: 0x00023396
		public AntiCheatCommonEventType EventType { get; set; }

		// Token: 0x17000AFC RID: 2812
		// (get) Token: 0x06002319 RID: 8985 RVA: 0x0002519F File Offset: 0x0002339F
		// (set) Token: 0x0600231A RID: 8986 RVA: 0x000251A7 File Offset: 0x000233A7
		public RegisterEventParamDef[] ParamDefs { get; set; }
	}
}
