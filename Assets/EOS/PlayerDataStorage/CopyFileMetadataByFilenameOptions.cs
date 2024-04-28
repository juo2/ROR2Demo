using System;

namespace Epic.OnlineServices.PlayerDataStorage
{
	// Token: 0x02000238 RID: 568
	public class CopyFileMetadataByFilenameOptions
	{
		// Token: 0x17000404 RID: 1028
		// (get) Token: 0x06000EAA RID: 3754 RVA: 0x0000FCCE File Offset: 0x0000DECE
		// (set) Token: 0x06000EAB RID: 3755 RVA: 0x0000FCD6 File Offset: 0x0000DED6
		public ProductUserId LocalUserId { get; set; }

		// Token: 0x17000405 RID: 1029
		// (get) Token: 0x06000EAC RID: 3756 RVA: 0x0000FCDF File Offset: 0x0000DEDF
		// (set) Token: 0x06000EAD RID: 3757 RVA: 0x0000FCE7 File Offset: 0x0000DEE7
		public string Filename { get; set; }
	}
}
