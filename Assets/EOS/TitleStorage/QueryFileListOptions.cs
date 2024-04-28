using System;

namespace Epic.OnlineServices.TitleStorage
{
	// Token: 0x02000081 RID: 129
	public class QueryFileListOptions
	{
		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x060004D7 RID: 1239 RVA: 0x00005A6C File Offset: 0x00003C6C
		// (set) Token: 0x060004D8 RID: 1240 RVA: 0x00005A74 File Offset: 0x00003C74
		public ProductUserId LocalUserId { get; set; }

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x060004D9 RID: 1241 RVA: 0x00005A7D File Offset: 0x00003C7D
		// (set) Token: 0x060004DA RID: 1242 RVA: 0x00005A85 File Offset: 0x00003C85
		public string[] ListOfTags { get; set; }
	}
}
