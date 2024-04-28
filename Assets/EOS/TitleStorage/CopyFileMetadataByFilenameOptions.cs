using System;

namespace Epic.OnlineServices.TitleStorage
{
	// Token: 0x02000065 RID: 101
	public class CopyFileMetadataByFilenameOptions
	{
		// Token: 0x17000093 RID: 147
		// (get) Token: 0x06000438 RID: 1080 RVA: 0x00005322 File Offset: 0x00003522
		// (set) Token: 0x06000439 RID: 1081 RVA: 0x0000532A File Offset: 0x0000352A
		public ProductUserId LocalUserId { get; set; }

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x0600043A RID: 1082 RVA: 0x00005333 File Offset: 0x00003533
		// (set) Token: 0x0600043B RID: 1083 RVA: 0x0000533B File Offset: 0x0000353B
		public string Filename { get; set; }
	}
}
