using System;

namespace Epic.OnlineServices.Metrics
{
	// Token: 0x020002DC RID: 732
	public class BeginPlayerSessionOptions
	{
		// Token: 0x1700055F RID: 1375
		// (get) Token: 0x06001287 RID: 4743 RVA: 0x00013B74 File Offset: 0x00011D74
		// (set) Token: 0x06001288 RID: 4744 RVA: 0x00013B7C File Offset: 0x00011D7C
		public BeginPlayerSessionOptionsAccountId AccountId { get; set; }

		// Token: 0x17000560 RID: 1376
		// (get) Token: 0x06001289 RID: 4745 RVA: 0x00013B85 File Offset: 0x00011D85
		// (set) Token: 0x0600128A RID: 4746 RVA: 0x00013B8D File Offset: 0x00011D8D
		public string DisplayName { get; set; }

		// Token: 0x17000561 RID: 1377
		// (get) Token: 0x0600128B RID: 4747 RVA: 0x00013B96 File Offset: 0x00011D96
		// (set) Token: 0x0600128C RID: 4748 RVA: 0x00013B9E File Offset: 0x00011D9E
		public UserControllerType ControllerType { get; set; }

		// Token: 0x17000562 RID: 1378
		// (get) Token: 0x0600128D RID: 4749 RVA: 0x00013BA7 File Offset: 0x00011DA7
		// (set) Token: 0x0600128E RID: 4750 RVA: 0x00013BAF File Offset: 0x00011DAF
		public string ServerIp { get; set; }

		// Token: 0x17000563 RID: 1379
		// (get) Token: 0x0600128F RID: 4751 RVA: 0x00013BB8 File Offset: 0x00011DB8
		// (set) Token: 0x06001290 RID: 4752 RVA: 0x00013BC0 File Offset: 0x00011DC0
		public string GameSessionId { get; set; }
	}
}
