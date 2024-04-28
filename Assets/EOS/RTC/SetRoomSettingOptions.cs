using System;

namespace Epic.OnlineServices.RTC
{
	// Token: 0x020001EC RID: 492
	public class SetRoomSettingOptions
	{
		// Token: 0x17000380 RID: 896
		// (get) Token: 0x06000D0D RID: 3341 RVA: 0x0000E274 File Offset: 0x0000C474
		// (set) Token: 0x06000D0E RID: 3342 RVA: 0x0000E27C File Offset: 0x0000C47C
		public ProductUserId LocalUserId { get; set; }

		// Token: 0x17000381 RID: 897
		// (get) Token: 0x06000D0F RID: 3343 RVA: 0x0000E285 File Offset: 0x0000C485
		// (set) Token: 0x06000D10 RID: 3344 RVA: 0x0000E28D File Offset: 0x0000C48D
		public string RoomName { get; set; }

		// Token: 0x17000382 RID: 898
		// (get) Token: 0x06000D11 RID: 3345 RVA: 0x0000E296 File Offset: 0x0000C496
		// (set) Token: 0x06000D12 RID: 3346 RVA: 0x0000E29E File Offset: 0x0000C49E
		public string SettingName { get; set; }

		// Token: 0x17000383 RID: 899
		// (get) Token: 0x06000D13 RID: 3347 RVA: 0x0000E2A7 File Offset: 0x0000C4A7
		// (set) Token: 0x06000D14 RID: 3348 RVA: 0x0000E2AF File Offset: 0x0000C4AF
		public string SettingValue { get; set; }
	}
}
