using System;

namespace Epic.OnlineServices.RTC
{
	// Token: 0x020001CB RID: 459
	public class AddNotifyParticipantStatusChangedOptions
	{
		// Token: 0x17000327 RID: 807
		// (get) Token: 0x06000C25 RID: 3109 RVA: 0x0000D35B File Offset: 0x0000B55B
		// (set) Token: 0x06000C26 RID: 3110 RVA: 0x0000D363 File Offset: 0x0000B563
		public ProductUserId LocalUserId { get; set; }

		// Token: 0x17000328 RID: 808
		// (get) Token: 0x06000C27 RID: 3111 RVA: 0x0000D36C File Offset: 0x0000B56C
		// (set) Token: 0x06000C28 RID: 3112 RVA: 0x0000D374 File Offset: 0x0000B574
		public string RoomName { get; set; }
	}
}
