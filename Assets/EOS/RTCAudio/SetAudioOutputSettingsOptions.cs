using System;

namespace Epic.OnlineServices.RTCAudio
{
	// Token: 0x020001A4 RID: 420
	public class SetAudioOutputSettingsOptions
	{
		// Token: 0x170002C0 RID: 704
		// (get) Token: 0x06000B27 RID: 2855 RVA: 0x0000C3EA File Offset: 0x0000A5EA
		// (set) Token: 0x06000B28 RID: 2856 RVA: 0x0000C3F2 File Offset: 0x0000A5F2
		public ProductUserId LocalUserId { get; set; }

		// Token: 0x170002C1 RID: 705
		// (get) Token: 0x06000B29 RID: 2857 RVA: 0x0000C3FB File Offset: 0x0000A5FB
		// (set) Token: 0x06000B2A RID: 2858 RVA: 0x0000C403 File Offset: 0x0000A603
		public string DeviceId { get; set; }

		// Token: 0x170002C2 RID: 706
		// (get) Token: 0x06000B2B RID: 2859 RVA: 0x0000C40C File Offset: 0x0000A60C
		// (set) Token: 0x06000B2C RID: 2860 RVA: 0x0000C414 File Offset: 0x0000A614
		public float Volume { get; set; }
	}
}
