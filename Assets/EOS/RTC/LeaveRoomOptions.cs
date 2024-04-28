using System;

namespace Epic.OnlineServices.RTC
{
	// Token: 0x020001DA RID: 474
	public class LeaveRoomOptions
	{
		// Token: 0x1700036B RID: 875
		// (get) Token: 0x06000CA5 RID: 3237 RVA: 0x0000DBF4 File Offset: 0x0000BDF4
		// (set) Token: 0x06000CA6 RID: 3238 RVA: 0x0000DBFC File Offset: 0x0000BDFC
		public ProductUserId LocalUserId { get; set; }

		// Token: 0x1700036C RID: 876
		// (get) Token: 0x06000CA7 RID: 3239 RVA: 0x0000DC05 File Offset: 0x0000BE05
		// (set) Token: 0x06000CA8 RID: 3240 RVA: 0x0000DC0D File Offset: 0x0000BE0D
		public string RoomName { get; set; }
	}
}
