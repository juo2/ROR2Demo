using System;

namespace Epic.OnlineServices.PlayerDataStorage
{
	// Token: 0x02000268 RID: 616
	public class QueryFileOptions
	{
		// Token: 0x17000452 RID: 1106
		// (get) Token: 0x06000FC3 RID: 4035 RVA: 0x00010D58 File Offset: 0x0000EF58
		// (set) Token: 0x06000FC4 RID: 4036 RVA: 0x00010D60 File Offset: 0x0000EF60
		public ProductUserId LocalUserId { get; set; }

		// Token: 0x17000453 RID: 1107
		// (get) Token: 0x06000FC5 RID: 4037 RVA: 0x00010D69 File Offset: 0x0000EF69
		// (set) Token: 0x06000FC6 RID: 4038 RVA: 0x00010D71 File Offset: 0x0000EF71
		public string Filename { get; set; }
	}
}
