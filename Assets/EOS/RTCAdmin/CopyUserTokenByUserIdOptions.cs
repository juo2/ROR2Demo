using System;

namespace Epic.OnlineServices.RTCAdmin
{
	// Token: 0x020001B2 RID: 434
	public class CopyUserTokenByUserIdOptions
	{
		// Token: 0x170002F2 RID: 754
		// (get) Token: 0x06000B8D RID: 2957 RVA: 0x0000CA14 File Offset: 0x0000AC14
		// (set) Token: 0x06000B8E RID: 2958 RVA: 0x0000CA1C File Offset: 0x0000AC1C
		public ProductUserId TargetUserId { get; set; }

		// Token: 0x170002F3 RID: 755
		// (get) Token: 0x06000B8F RID: 2959 RVA: 0x0000CA25 File Offset: 0x0000AC25
		// (set) Token: 0x06000B90 RID: 2960 RVA: 0x0000CA2D File Offset: 0x0000AC2D
		public uint QueryId { get; set; }
	}
}
