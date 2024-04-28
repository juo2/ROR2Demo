using System;

namespace Epic.OnlineServices.PlayerDataStorage
{
	// Token: 0x02000246 RID: 582
	public class FileMetadata : ISettable
	{
		// Token: 0x17000429 RID: 1065
		// (get) Token: 0x06000EFC RID: 3836 RVA: 0x000101D0 File Offset: 0x0000E3D0
		// (set) Token: 0x06000EFD RID: 3837 RVA: 0x000101D8 File Offset: 0x0000E3D8
		public uint FileSizeBytes { get; set; }

		// Token: 0x1700042A RID: 1066
		// (get) Token: 0x06000EFE RID: 3838 RVA: 0x000101E1 File Offset: 0x0000E3E1
		// (set) Token: 0x06000EFF RID: 3839 RVA: 0x000101E9 File Offset: 0x0000E3E9
		public string MD5Hash { get; set; }

		// Token: 0x1700042B RID: 1067
		// (get) Token: 0x06000F00 RID: 3840 RVA: 0x000101F2 File Offset: 0x0000E3F2
		// (set) Token: 0x06000F01 RID: 3841 RVA: 0x000101FA File Offset: 0x0000E3FA
		public string Filename { get; set; }

		// Token: 0x1700042C RID: 1068
		// (get) Token: 0x06000F02 RID: 3842 RVA: 0x00010203 File Offset: 0x0000E403
		// (set) Token: 0x06000F03 RID: 3843 RVA: 0x0001020B File Offset: 0x0000E40B
		public DateTimeOffset? LastModifiedTime { get; set; }

		// Token: 0x1700042D RID: 1069
		// (get) Token: 0x06000F04 RID: 3844 RVA: 0x00010214 File Offset: 0x0000E414
		// (set) Token: 0x06000F05 RID: 3845 RVA: 0x0001021C File Offset: 0x0000E41C
		public uint UnencryptedDataSizeBytes { get; set; }

		// Token: 0x06000F06 RID: 3846 RVA: 0x00010228 File Offset: 0x0000E428
		internal void Set(FileMetadataInternal? other)
		{
			if (other != null)
			{
				this.FileSizeBytes = other.Value.FileSizeBytes;
				this.MD5Hash = other.Value.MD5Hash;
				this.Filename = other.Value.Filename;
				this.LastModifiedTime = other.Value.LastModifiedTime;
				this.UnencryptedDataSizeBytes = other.Value.UnencryptedDataSizeBytes;
			}
		}

		// Token: 0x06000F07 RID: 3847 RVA: 0x000102A7 File Offset: 0x0000E4A7
		public void Set(object other)
		{
			this.Set(other as FileMetadataInternal?);
		}
	}
}
