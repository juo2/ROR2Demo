using System;

namespace Epic.OnlineServices.RTCAdmin
{
	// Token: 0x020001C5 RID: 453
	public class SetParticipantHardMuteOptions
	{
		// Token: 0x17000319 RID: 793
		// (get) Token: 0x06000C00 RID: 3072 RVA: 0x0000D0FC File Offset: 0x0000B2FC
		// (set) Token: 0x06000C01 RID: 3073 RVA: 0x0000D104 File Offset: 0x0000B304
		public string RoomName { get; set; }

		// Token: 0x1700031A RID: 794
		// (get) Token: 0x06000C02 RID: 3074 RVA: 0x0000D10D File Offset: 0x0000B30D
		// (set) Token: 0x06000C03 RID: 3075 RVA: 0x0000D115 File Offset: 0x0000B315
		public ProductUserId TargetUserId { get; set; }

		// Token: 0x1700031B RID: 795
		// (get) Token: 0x06000C04 RID: 3076 RVA: 0x0000D11E File Offset: 0x0000B31E
		// (set) Token: 0x06000C05 RID: 3077 RVA: 0x0000D126 File Offset: 0x0000B326
		public bool Mute { get; set; }
	}
}
