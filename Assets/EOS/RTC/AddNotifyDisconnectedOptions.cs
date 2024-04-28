using System;

namespace Epic.OnlineServices.RTC
{
	// Token: 0x020001C9 RID: 457
	public class AddNotifyDisconnectedOptions
	{
		// Token: 0x17000323 RID: 803
		// (get) Token: 0x06000C1B RID: 3099 RVA: 0x0000D2CF File Offset: 0x0000B4CF
		// (set) Token: 0x06000C1C RID: 3100 RVA: 0x0000D2D7 File Offset: 0x0000B4D7
		public ProductUserId LocalUserId { get; set; }

		// Token: 0x17000324 RID: 804
		// (get) Token: 0x06000C1D RID: 3101 RVA: 0x0000D2E0 File Offset: 0x0000B4E0
		// (set) Token: 0x06000C1E RID: 3102 RVA: 0x0000D2E8 File Offset: 0x0000B4E8
		public string RoomName { get; set; }
	}
}
