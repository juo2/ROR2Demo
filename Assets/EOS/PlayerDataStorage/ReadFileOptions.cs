using System;

namespace Epic.OnlineServices.PlayerDataStorage
{
	// Token: 0x0200026E RID: 622
	public class ReadFileOptions
	{
		// Token: 0x1700046C RID: 1132
		// (get) Token: 0x06000FF5 RID: 4085 RVA: 0x000110E6 File Offset: 0x0000F2E6
		// (set) Token: 0x06000FF6 RID: 4086 RVA: 0x000110EE File Offset: 0x0000F2EE
		public ProductUserId LocalUserId { get; set; }

		// Token: 0x1700046D RID: 1133
		// (get) Token: 0x06000FF7 RID: 4087 RVA: 0x000110F7 File Offset: 0x0000F2F7
		// (set) Token: 0x06000FF8 RID: 4088 RVA: 0x000110FF File Offset: 0x0000F2FF
		public string Filename { get; set; }

		// Token: 0x1700046E RID: 1134
		// (get) Token: 0x06000FF9 RID: 4089 RVA: 0x00011108 File Offset: 0x0000F308
		// (set) Token: 0x06000FFA RID: 4090 RVA: 0x00011110 File Offset: 0x0000F310
		public uint ReadChunkLengthBytes { get; set; }

		// Token: 0x1700046F RID: 1135
		// (get) Token: 0x06000FFB RID: 4091 RVA: 0x00011119 File Offset: 0x0000F319
		// (set) Token: 0x06000FFC RID: 4092 RVA: 0x00011121 File Offset: 0x0000F321
		public OnReadFileDataCallback ReadFileDataCallback { get; set; }

		// Token: 0x17000470 RID: 1136
		// (get) Token: 0x06000FFD RID: 4093 RVA: 0x0001112A File Offset: 0x0000F32A
		// (set) Token: 0x06000FFE RID: 4094 RVA: 0x00011132 File Offset: 0x0000F332
		public OnFileTransferProgressCallback FileTransferProgressCallback { get; set; }
	}
}
