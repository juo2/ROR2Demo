using System;

namespace Epic.OnlineServices.RTCAudio
{
	// Token: 0x0200016E RID: 366
	public class AddNotifyParticipantUpdatedOptions
	{
		// Token: 0x17000260 RID: 608
		// (get) Token: 0x060009E6 RID: 2534 RVA: 0x0000AE95 File Offset: 0x00009095
		// (set) Token: 0x060009E7 RID: 2535 RVA: 0x0000AE9D File Offset: 0x0000909D
		public ProductUserId LocalUserId { get; set; }

		// Token: 0x17000261 RID: 609
		// (get) Token: 0x060009E8 RID: 2536 RVA: 0x0000AEA6 File Offset: 0x000090A6
		// (set) Token: 0x060009E9 RID: 2537 RVA: 0x0000AEAE File Offset: 0x000090AE
		public string RoomName { get; set; }
	}
}
