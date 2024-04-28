using System;

namespace Epic.OnlineServices.PlayerDataStorage
{
	// Token: 0x02000275 RID: 629
	public class WriteFileOptions
	{
		// Token: 0x17000488 RID: 1160
		// (get) Token: 0x0600102A RID: 4138 RVA: 0x000114D0 File Offset: 0x0000F6D0
		// (set) Token: 0x0600102B RID: 4139 RVA: 0x000114D8 File Offset: 0x0000F6D8
		public ProductUserId LocalUserId { get; set; }

		// Token: 0x17000489 RID: 1161
		// (get) Token: 0x0600102C RID: 4140 RVA: 0x000114E1 File Offset: 0x0000F6E1
		// (set) Token: 0x0600102D RID: 4141 RVA: 0x000114E9 File Offset: 0x0000F6E9
		public string Filename { get; set; }

		// Token: 0x1700048A RID: 1162
		// (get) Token: 0x0600102E RID: 4142 RVA: 0x000114F2 File Offset: 0x0000F6F2
		// (set) Token: 0x0600102F RID: 4143 RVA: 0x000114FA File Offset: 0x0000F6FA
		public uint ChunkLengthBytes { get; set; }

		// Token: 0x1700048B RID: 1163
		// (get) Token: 0x06001030 RID: 4144 RVA: 0x00011503 File Offset: 0x0000F703
		// (set) Token: 0x06001031 RID: 4145 RVA: 0x0001150B File Offset: 0x0000F70B
		public OnWriteFileDataCallback WriteFileDataCallback { get; set; }

		// Token: 0x1700048C RID: 1164
		// (get) Token: 0x06001032 RID: 4146 RVA: 0x00011514 File Offset: 0x0000F714
		// (set) Token: 0x06001033 RID: 4147 RVA: 0x0001151C File Offset: 0x0000F71C
		public OnFileTransferProgressCallback FileTransferProgressCallback { get; set; }
	}
}
