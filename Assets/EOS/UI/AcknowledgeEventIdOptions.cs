using System;

namespace Epic.OnlineServices.UI
{
	// Token: 0x02000040 RID: 64
	public class AcknowledgeEventIdOptions
	{
		// Token: 0x17000068 RID: 104
		// (get) Token: 0x06000399 RID: 921 RVA: 0x00004A5C File Offset: 0x00002C5C
		// (set) Token: 0x0600039A RID: 922 RVA: 0x00004A64 File Offset: 0x00002C64
		public ulong UiEventId { get; set; }

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x0600039B RID: 923 RVA: 0x00004A6D File Offset: 0x00002C6D
		// (set) Token: 0x0600039C RID: 924 RVA: 0x00004A75 File Offset: 0x00002C75
		public Result Result { get; set; }
	}
}
