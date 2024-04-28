using System;

namespace Epic.OnlineServices.Ecom
{
	// Token: 0x02000445 RID: 1093
	public class CopyItemImageInfoByIndexOptions
	{
		// Token: 0x17000809 RID: 2057
		// (get) Token: 0x06001AA9 RID: 6825 RVA: 0x0001C215 File Offset: 0x0001A415
		// (set) Token: 0x06001AAA RID: 6826 RVA: 0x0001C21D File Offset: 0x0001A41D
		public EpicAccountId LocalUserId { get; set; }

		// Token: 0x1700080A RID: 2058
		// (get) Token: 0x06001AAB RID: 6827 RVA: 0x0001C226 File Offset: 0x0001A426
		// (set) Token: 0x06001AAC RID: 6828 RVA: 0x0001C22E File Offset: 0x0001A42E
		public string ItemId { get; set; }

		// Token: 0x1700080B RID: 2059
		// (get) Token: 0x06001AAD RID: 6829 RVA: 0x0001C237 File Offset: 0x0001A437
		// (set) Token: 0x06001AAE RID: 6830 RVA: 0x0001C23F File Offset: 0x0001A43F
		public uint ImageInfoIndex { get; set; }
	}
}
