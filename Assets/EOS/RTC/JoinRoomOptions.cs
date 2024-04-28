using System;

namespace Epic.OnlineServices.RTC
{
	// Token: 0x020001D6 RID: 470
	public class JoinRoomOptions
	{
		// Token: 0x17000352 RID: 850
		// (get) Token: 0x06000C78 RID: 3192 RVA: 0x0000D900 File Offset: 0x0000BB00
		// (set) Token: 0x06000C79 RID: 3193 RVA: 0x0000D908 File Offset: 0x0000BB08
		public ProductUserId LocalUserId { get; set; }

		// Token: 0x17000353 RID: 851
		// (get) Token: 0x06000C7A RID: 3194 RVA: 0x0000D911 File Offset: 0x0000BB11
		// (set) Token: 0x06000C7B RID: 3195 RVA: 0x0000D919 File Offset: 0x0000BB19
		public string RoomName { get; set; }

		// Token: 0x17000354 RID: 852
		// (get) Token: 0x06000C7C RID: 3196 RVA: 0x0000D922 File Offset: 0x0000BB22
		// (set) Token: 0x06000C7D RID: 3197 RVA: 0x0000D92A File Offset: 0x0000BB2A
		public string ClientBaseUrl { get; set; }

		// Token: 0x17000355 RID: 853
		// (get) Token: 0x06000C7E RID: 3198 RVA: 0x0000D933 File Offset: 0x0000BB33
		// (set) Token: 0x06000C7F RID: 3199 RVA: 0x0000D93B File Offset: 0x0000BB3B
		public string ParticipantToken { get; set; }

		// Token: 0x17000356 RID: 854
		// (get) Token: 0x06000C80 RID: 3200 RVA: 0x0000D944 File Offset: 0x0000BB44
		// (set) Token: 0x06000C81 RID: 3201 RVA: 0x0000D94C File Offset: 0x0000BB4C
		public ProductUserId ParticipantId { get; set; }

		// Token: 0x17000357 RID: 855
		// (get) Token: 0x06000C82 RID: 3202 RVA: 0x0000D955 File Offset: 0x0000BB55
		// (set) Token: 0x06000C83 RID: 3203 RVA: 0x0000D95D File Offset: 0x0000BB5D
		public JoinRoomFlags Flags { get; set; }

		// Token: 0x17000358 RID: 856
		// (get) Token: 0x06000C84 RID: 3204 RVA: 0x0000D966 File Offset: 0x0000BB66
		// (set) Token: 0x06000C85 RID: 3205 RVA: 0x0000D96E File Offset: 0x0000BB6E
		public bool ManualAudioInputEnabled { get; set; }

		// Token: 0x17000359 RID: 857
		// (get) Token: 0x06000C86 RID: 3206 RVA: 0x0000D977 File Offset: 0x0000BB77
		// (set) Token: 0x06000C87 RID: 3207 RVA: 0x0000D97F File Offset: 0x0000BB7F
		public bool ManualAudioOutputEnabled { get; set; }
	}
}
