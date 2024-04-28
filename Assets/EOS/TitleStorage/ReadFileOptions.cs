using System;

namespace Epic.OnlineServices.TitleStorage
{
	// Token: 0x02000089 RID: 137
	public class ReadFileOptions
	{
		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x06000513 RID: 1299 RVA: 0x00005E8A File Offset: 0x0000408A
		// (set) Token: 0x06000514 RID: 1300 RVA: 0x00005E92 File Offset: 0x00004092
		public ProductUserId LocalUserId { get; set; }

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x06000515 RID: 1301 RVA: 0x00005E9B File Offset: 0x0000409B
		// (set) Token: 0x06000516 RID: 1302 RVA: 0x00005EA3 File Offset: 0x000040A3
		public string Filename { get; set; }

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x06000517 RID: 1303 RVA: 0x00005EAC File Offset: 0x000040AC
		// (set) Token: 0x06000518 RID: 1304 RVA: 0x00005EB4 File Offset: 0x000040B4
		public uint ReadChunkLengthBytes { get; set; }

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x06000519 RID: 1305 RVA: 0x00005EBD File Offset: 0x000040BD
		// (set) Token: 0x0600051A RID: 1306 RVA: 0x00005EC5 File Offset: 0x000040C5
		public OnReadFileDataCallback ReadFileDataCallback { get; set; }

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x0600051B RID: 1307 RVA: 0x00005ECE File Offset: 0x000040CE
		// (set) Token: 0x0600051C RID: 1308 RVA: 0x00005ED6 File Offset: 0x000040D6
		public OnFileTransferProgressCallback FileTransferProgressCallback { get; set; }
	}
}
