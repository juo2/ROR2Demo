using System;

namespace Epic.OnlineServices.TitleStorage
{
	// Token: 0x02000087 RID: 135
	public class ReadFileDataCallbackInfo : ICallbackInfo, ISettable
	{
		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x060004FC RID: 1276 RVA: 0x00005CC0 File Offset: 0x00003EC0
		// (set) Token: 0x060004FD RID: 1277 RVA: 0x00005CC8 File Offset: 0x00003EC8
		public object ClientData { get; private set; }

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x060004FE RID: 1278 RVA: 0x00005CD1 File Offset: 0x00003ED1
		// (set) Token: 0x060004FF RID: 1279 RVA: 0x00005CD9 File Offset: 0x00003ED9
		public ProductUserId LocalUserId { get; private set; }

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x06000500 RID: 1280 RVA: 0x00005CE2 File Offset: 0x00003EE2
		// (set) Token: 0x06000501 RID: 1281 RVA: 0x00005CEA File Offset: 0x00003EEA
		public string Filename { get; private set; }

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x06000502 RID: 1282 RVA: 0x00005CF3 File Offset: 0x00003EF3
		// (set) Token: 0x06000503 RID: 1283 RVA: 0x00005CFB File Offset: 0x00003EFB
		public uint TotalFileSizeBytes { get; private set; }

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x06000504 RID: 1284 RVA: 0x00005D04 File Offset: 0x00003F04
		// (set) Token: 0x06000505 RID: 1285 RVA: 0x00005D0C File Offset: 0x00003F0C
		public bool IsLastChunk { get; private set; }

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x06000506 RID: 1286 RVA: 0x00005D15 File Offset: 0x00003F15
		// (set) Token: 0x06000507 RID: 1287 RVA: 0x00005D1D File Offset: 0x00003F1D
		public byte[] DataChunk { get; private set; }

		// Token: 0x06000508 RID: 1288 RVA: 0x00005D28 File Offset: 0x00003F28
		public Result? GetResultCode()
		{
			return null;
		}

		// Token: 0x06000509 RID: 1289 RVA: 0x00005D40 File Offset: 0x00003F40
		internal void Set(ReadFileDataCallbackInfoInternal? other)
		{
			if (other != null)
			{
				this.ClientData = other.Value.ClientData;
				this.LocalUserId = other.Value.LocalUserId;
				this.Filename = other.Value.Filename;
				this.TotalFileSizeBytes = other.Value.TotalFileSizeBytes;
				this.IsLastChunk = other.Value.IsLastChunk;
				this.DataChunk = other.Value.DataChunk;
			}
		}

		// Token: 0x0600050A RID: 1290 RVA: 0x00005DD4 File Offset: 0x00003FD4
		public void Set(object other)
		{
			this.Set(other as ReadFileDataCallbackInfoInternal?);
		}
	}
}
