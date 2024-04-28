using System;

namespace Epic.OnlineServices.RTCAudio
{
	// Token: 0x020001A0 RID: 416
	public class SendAudioOptions
	{
		// Token: 0x170002B2 RID: 690
		// (get) Token: 0x06000B0A RID: 2826 RVA: 0x0000C248 File Offset: 0x0000A448
		// (set) Token: 0x06000B0B RID: 2827 RVA: 0x0000C250 File Offset: 0x0000A450
		public ProductUserId LocalUserId { get; set; }

		// Token: 0x170002B3 RID: 691
		// (get) Token: 0x06000B0C RID: 2828 RVA: 0x0000C259 File Offset: 0x0000A459
		// (set) Token: 0x06000B0D RID: 2829 RVA: 0x0000C261 File Offset: 0x0000A461
		public string RoomName { get; set; }

		// Token: 0x170002B4 RID: 692
		// (get) Token: 0x06000B0E RID: 2830 RVA: 0x0000C26A File Offset: 0x0000A46A
		// (set) Token: 0x06000B0F RID: 2831 RVA: 0x0000C272 File Offset: 0x0000A472
		public AudioBuffer Buffer { get; set; }
	}
}
