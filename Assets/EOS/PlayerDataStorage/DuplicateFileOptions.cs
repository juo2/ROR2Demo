using System;

namespace Epic.OnlineServices.PlayerDataStorage
{
	// Token: 0x02000244 RID: 580
	public class DuplicateFileOptions
	{
		// Token: 0x17000423 RID: 1059
		// (get) Token: 0x06000EEF RID: 3823 RVA: 0x0001010C File Offset: 0x0000E30C
		// (set) Token: 0x06000EF0 RID: 3824 RVA: 0x00010114 File Offset: 0x0000E314
		public ProductUserId LocalUserId { get; set; }

		// Token: 0x17000424 RID: 1060
		// (get) Token: 0x06000EF1 RID: 3825 RVA: 0x0001011D File Offset: 0x0000E31D
		// (set) Token: 0x06000EF2 RID: 3826 RVA: 0x00010125 File Offset: 0x0000E325
		public string SourceFilename { get; set; }

		// Token: 0x17000425 RID: 1061
		// (get) Token: 0x06000EF3 RID: 3827 RVA: 0x0001012E File Offset: 0x0000E32E
		// (set) Token: 0x06000EF4 RID: 3828 RVA: 0x00010136 File Offset: 0x0000E336
		public string DestinationFilename { get; set; }
	}
}
