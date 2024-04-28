using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.TitleStorage
{
	// Token: 0x02000088 RID: 136
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct ReadFileDataCallbackInfoInternal : ICallbackInfoInternal
	{
		// Token: 0x170000DC RID: 220
		// (get) Token: 0x0600050C RID: 1292 RVA: 0x00005DE8 File Offset: 0x00003FE8
		public object ClientData
		{
			get
			{
				object result;
				Helper.TryMarshalGet(this.m_ClientData, out result);
				return result;
			}
		}

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x0600050D RID: 1293 RVA: 0x00005E04 File Offset: 0x00004004
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x0600050E RID: 1294 RVA: 0x00005E0C File Offset: 0x0000400C
		public ProductUserId LocalUserId
		{
			get
			{
				ProductUserId result;
				Helper.TryMarshalGet<ProductUserId>(this.m_LocalUserId, out result);
				return result;
			}
		}

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x0600050F RID: 1295 RVA: 0x00005E28 File Offset: 0x00004028
		public string Filename
		{
			get
			{
				string result;
				Helper.TryMarshalGet(this.m_Filename, out result);
				return result;
			}
		}

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x06000510 RID: 1296 RVA: 0x00005E44 File Offset: 0x00004044
		public uint TotalFileSizeBytes
		{
			get
			{
				return this.m_TotalFileSizeBytes;
			}
		}

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x06000511 RID: 1297 RVA: 0x00005E4C File Offset: 0x0000404C
		public bool IsLastChunk
		{
			get
			{
				bool result;
				Helper.TryMarshalGet(this.m_IsLastChunk, out result);
				return result;
			}
		}

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x06000512 RID: 1298 RVA: 0x00005E68 File Offset: 0x00004068
		public byte[] DataChunk
		{
			get
			{
				byte[] result;
				Helper.TryMarshalGet<byte>(this.m_DataChunk, out result, this.m_DataChunkLengthBytes);
				return result;
			}
		}

		// Token: 0x04000289 RID: 649
		private IntPtr m_ClientData;

		// Token: 0x0400028A RID: 650
		private IntPtr m_LocalUserId;

		// Token: 0x0400028B RID: 651
		private IntPtr m_Filename;

		// Token: 0x0400028C RID: 652
		private uint m_TotalFileSizeBytes;

		// Token: 0x0400028D RID: 653
		private int m_IsLastChunk;

		// Token: 0x0400028E RID: 654
		private uint m_DataChunkLengthBytes;

		// Token: 0x0400028F RID: 655
		private IntPtr m_DataChunk;
	}
}
