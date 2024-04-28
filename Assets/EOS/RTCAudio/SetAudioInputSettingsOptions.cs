using System;

namespace Epic.OnlineServices.RTCAudio
{
	// Token: 0x020001A2 RID: 418
	public class SetAudioInputSettingsOptions
	{
		// Token: 0x170002B8 RID: 696
		// (get) Token: 0x06000B17 RID: 2839 RVA: 0x0000C30C File Offset: 0x0000A50C
		// (set) Token: 0x06000B18 RID: 2840 RVA: 0x0000C314 File Offset: 0x0000A514
		public ProductUserId LocalUserId { get; set; }

		// Token: 0x170002B9 RID: 697
		// (get) Token: 0x06000B19 RID: 2841 RVA: 0x0000C31D File Offset: 0x0000A51D
		// (set) Token: 0x06000B1A RID: 2842 RVA: 0x0000C325 File Offset: 0x0000A525
		public string DeviceId { get; set; }

		// Token: 0x170002BA RID: 698
		// (get) Token: 0x06000B1B RID: 2843 RVA: 0x0000C32E File Offset: 0x0000A52E
		// (set) Token: 0x06000B1C RID: 2844 RVA: 0x0000C336 File Offset: 0x0000A536
		public float Volume { get; set; }

		// Token: 0x170002BB RID: 699
		// (get) Token: 0x06000B1D RID: 2845 RVA: 0x0000C33F File Offset: 0x0000A53F
		// (set) Token: 0x06000B1E RID: 2846 RVA: 0x0000C347 File Offset: 0x0000A547
		public bool PlatformAEC { get; set; }
	}
}
