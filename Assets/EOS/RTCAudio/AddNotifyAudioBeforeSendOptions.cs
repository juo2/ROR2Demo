using System;

namespace Epic.OnlineServices.RTCAudio
{
	// Token: 0x02000166 RID: 358
	public class AddNotifyAudioBeforeSendOptions
	{
		// Token: 0x17000254 RID: 596
		// (get) Token: 0x060009C4 RID: 2500 RVA: 0x0000ACD7 File Offset: 0x00008ED7
		// (set) Token: 0x060009C5 RID: 2501 RVA: 0x0000ACDF File Offset: 0x00008EDF
		public ProductUserId LocalUserId { get; set; }

		// Token: 0x17000255 RID: 597
		// (get) Token: 0x060009C6 RID: 2502 RVA: 0x0000ACE8 File Offset: 0x00008EE8
		// (set) Token: 0x060009C7 RID: 2503 RVA: 0x0000ACF0 File Offset: 0x00008EF0
		public string RoomName { get; set; }
	}
}
