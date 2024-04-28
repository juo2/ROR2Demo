using System;

namespace Epic.OnlineServices.RTCAudio
{
	// Token: 0x020001AA RID: 426
	public class UpdateReceivingOptions
	{
		// Token: 0x170002D5 RID: 725
		// (get) Token: 0x06000B52 RID: 2898 RVA: 0x0000C6A8 File Offset: 0x0000A8A8
		// (set) Token: 0x06000B53 RID: 2899 RVA: 0x0000C6B0 File Offset: 0x0000A8B0
		public ProductUserId LocalUserId { get; set; }

		// Token: 0x170002D6 RID: 726
		// (get) Token: 0x06000B54 RID: 2900 RVA: 0x0000C6B9 File Offset: 0x0000A8B9
		// (set) Token: 0x06000B55 RID: 2901 RVA: 0x0000C6C1 File Offset: 0x0000A8C1
		public string RoomName { get; set; }

		// Token: 0x170002D7 RID: 727
		// (get) Token: 0x06000B56 RID: 2902 RVA: 0x0000C6CA File Offset: 0x0000A8CA
		// (set) Token: 0x06000B57 RID: 2903 RVA: 0x0000C6D2 File Offset: 0x0000A8D2
		public ProductUserId ParticipantId { get; set; }

		// Token: 0x170002D8 RID: 728
		// (get) Token: 0x06000B58 RID: 2904 RVA: 0x0000C6DB File Offset: 0x0000A8DB
		// (set) Token: 0x06000B59 RID: 2905 RVA: 0x0000C6E3 File Offset: 0x0000A8E3
		public bool AudioEnabled { get; set; }
	}
}
