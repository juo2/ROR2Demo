using System;

namespace Epic.OnlineServices.RTC
{
	// Token: 0x020001CF RID: 463
	public class BlockParticipantOptions
	{
		// Token: 0x17000338 RID: 824
		// (get) Token: 0x06000C46 RID: 3142 RVA: 0x0000D5A0 File Offset: 0x0000B7A0
		// (set) Token: 0x06000C47 RID: 3143 RVA: 0x0000D5A8 File Offset: 0x0000B7A8
		public ProductUserId LocalUserId { get; set; }

		// Token: 0x17000339 RID: 825
		// (get) Token: 0x06000C48 RID: 3144 RVA: 0x0000D5B1 File Offset: 0x0000B7B1
		// (set) Token: 0x06000C49 RID: 3145 RVA: 0x0000D5B9 File Offset: 0x0000B7B9
		public string RoomName { get; set; }

		// Token: 0x1700033A RID: 826
		// (get) Token: 0x06000C4A RID: 3146 RVA: 0x0000D5C2 File Offset: 0x0000B7C2
		// (set) Token: 0x06000C4B RID: 3147 RVA: 0x0000D5CA File Offset: 0x0000B7CA
		public ProductUserId ParticipantId { get; set; }

		// Token: 0x1700033B RID: 827
		// (get) Token: 0x06000C4C RID: 3148 RVA: 0x0000D5D3 File Offset: 0x0000B7D3
		// (set) Token: 0x06000C4D RID: 3149 RVA: 0x0000D5DB File Offset: 0x0000B7DB
		public bool Blocked { get; set; }
	}
}
