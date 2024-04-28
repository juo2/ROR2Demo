using System;

namespace Epic.OnlineServices.RTCAdmin
{
	// Token: 0x020001C0 RID: 448
	public class QueryJoinRoomTokenOptions
	{
		// Token: 0x1700030C RID: 780
		// (get) Token: 0x06000BDB RID: 3035 RVA: 0x0000CD5C File Offset: 0x0000AF5C
		// (set) Token: 0x06000BDC RID: 3036 RVA: 0x0000CD64 File Offset: 0x0000AF64
		public ProductUserId LocalUserId { get; set; }

		// Token: 0x1700030D RID: 781
		// (get) Token: 0x06000BDD RID: 3037 RVA: 0x0000CD6D File Offset: 0x0000AF6D
		// (set) Token: 0x06000BDE RID: 3038 RVA: 0x0000CD75 File Offset: 0x0000AF75
		public string RoomName { get; set; }

		// Token: 0x1700030E RID: 782
		// (get) Token: 0x06000BDF RID: 3039 RVA: 0x0000CD7E File Offset: 0x0000AF7E
		// (set) Token: 0x06000BE0 RID: 3040 RVA: 0x0000CD86 File Offset: 0x0000AF86
		public ProductUserId[] TargetUserIds { get; set; }

		// Token: 0x1700030F RID: 783
		// (get) Token: 0x06000BE1 RID: 3041 RVA: 0x0000CD8F File Offset: 0x0000AF8F
		// (set) Token: 0x06000BE2 RID: 3042 RVA: 0x0000CD97 File Offset: 0x0000AF97
		public string TargetUserIpAddresses { get; set; }
	}
}
