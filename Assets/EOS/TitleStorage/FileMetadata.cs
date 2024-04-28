using System;

namespace Epic.OnlineServices.TitleStorage
{
	// Token: 0x0200006B RID: 107
	public class FileMetadata : ISettable
	{
		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x06000457 RID: 1111 RVA: 0x000054F4 File Offset: 0x000036F4
		// (set) Token: 0x06000458 RID: 1112 RVA: 0x000054FC File Offset: 0x000036FC
		public uint FileSizeBytes { get; set; }

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x06000459 RID: 1113 RVA: 0x00005505 File Offset: 0x00003705
		// (set) Token: 0x0600045A RID: 1114 RVA: 0x0000550D File Offset: 0x0000370D
		public string MD5Hash { get; set; }

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x0600045B RID: 1115 RVA: 0x00005516 File Offset: 0x00003716
		// (set) Token: 0x0600045C RID: 1116 RVA: 0x0000551E File Offset: 0x0000371E
		public string Filename { get; set; }

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x0600045D RID: 1117 RVA: 0x00005527 File Offset: 0x00003727
		// (set) Token: 0x0600045E RID: 1118 RVA: 0x0000552F File Offset: 0x0000372F
		public uint UnencryptedDataSizeBytes { get; set; }

		// Token: 0x0600045F RID: 1119 RVA: 0x00005538 File Offset: 0x00003738
		internal void Set(FileMetadataInternal? other)
		{
			if (other != null)
			{
				this.FileSizeBytes = other.Value.FileSizeBytes;
				this.MD5Hash = other.Value.MD5Hash;
				this.Filename = other.Value.Filename;
				this.UnencryptedDataSizeBytes = other.Value.UnencryptedDataSizeBytes;
			}
		}

		// Token: 0x06000460 RID: 1120 RVA: 0x000055A2 File Offset: 0x000037A2
		public void Set(object other)
		{
			this.Set(other as FileMetadataInternal?);
		}
	}
}
