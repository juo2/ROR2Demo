using System;

namespace Epic.OnlineServices.RTCAudio
{
	// Token: 0x020001AE RID: 430
	public class UpdateSendingOptions
	{
		// Token: 0x170002E8 RID: 744
		// (get) Token: 0x06000B76 RID: 2934 RVA: 0x0000C8FC File Offset: 0x0000AAFC
		// (set) Token: 0x06000B77 RID: 2935 RVA: 0x0000C904 File Offset: 0x0000AB04
		public ProductUserId LocalUserId { get; set; }

		// Token: 0x170002E9 RID: 745
		// (get) Token: 0x06000B78 RID: 2936 RVA: 0x0000C90D File Offset: 0x0000AB0D
		// (set) Token: 0x06000B79 RID: 2937 RVA: 0x0000C915 File Offset: 0x0000AB15
		public string RoomName { get; set; }

		// Token: 0x170002EA RID: 746
		// (get) Token: 0x06000B7A RID: 2938 RVA: 0x0000C91E File Offset: 0x0000AB1E
		// (set) Token: 0x06000B7B RID: 2939 RVA: 0x0000C926 File Offset: 0x0000AB26
		public RTCAudioStatus AudioStatus { get; set; }
	}
}
