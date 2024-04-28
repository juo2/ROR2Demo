using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.PlayerDataStorage
{
	// Token: 0x0200026D RID: 621
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct ReadFileDataCallbackInfoInternal : ICallbackInfoInternal
	{
		// Token: 0x17000465 RID: 1125
		// (get) Token: 0x06000FEE RID: 4078 RVA: 0x00011044 File Offset: 0x0000F244
		public object ClientData
		{
			get
			{
				object result;
				Helper.TryMarshalGet(this.m_ClientData, out result);
				return result;
			}
		}

		// Token: 0x17000466 RID: 1126
		// (get) Token: 0x06000FEF RID: 4079 RVA: 0x00011060 File Offset: 0x0000F260
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x17000467 RID: 1127
		// (get) Token: 0x06000FF0 RID: 4080 RVA: 0x00011068 File Offset: 0x0000F268
		public ProductUserId LocalUserId
		{
			get
			{
				ProductUserId result;
				Helper.TryMarshalGet<ProductUserId>(this.m_LocalUserId, out result);
				return result;
			}
		}

		// Token: 0x17000468 RID: 1128
		// (get) Token: 0x06000FF1 RID: 4081 RVA: 0x00011084 File Offset: 0x0000F284
		public string Filename
		{
			get
			{
				string result;
				Helper.TryMarshalGet(this.m_Filename, out result);
				return result;
			}
		}

		// Token: 0x17000469 RID: 1129
		// (get) Token: 0x06000FF2 RID: 4082 RVA: 0x000110A0 File Offset: 0x0000F2A0
		public uint TotalFileSizeBytes
		{
			get
			{
				return this.m_TotalFileSizeBytes;
			}
		}

		// Token: 0x1700046A RID: 1130
		// (get) Token: 0x06000FF3 RID: 4083 RVA: 0x000110A8 File Offset: 0x0000F2A8
		public bool IsLastChunk
		{
			get
			{
				bool result;
				Helper.TryMarshalGet(this.m_IsLastChunk, out result);
				return result;
			}
		}

		// Token: 0x1700046B RID: 1131
		// (get) Token: 0x06000FF4 RID: 4084 RVA: 0x000110C4 File Offset: 0x0000F2C4
		public byte[] DataChunk
		{
			get
			{
				byte[] result;
				Helper.TryMarshalGet<byte>(this.m_DataChunk, out result, this.m_DataChunkLengthBytes);
				return result;
			}
		}

		// Token: 0x04000759 RID: 1881
		private IntPtr m_ClientData;

		// Token: 0x0400075A RID: 1882
		private IntPtr m_LocalUserId;

		// Token: 0x0400075B RID: 1883
		private IntPtr m_Filename;

		// Token: 0x0400075C RID: 1884
		private uint m_TotalFileSizeBytes;

		// Token: 0x0400075D RID: 1885
		private int m_IsLastChunk;

		// Token: 0x0400075E RID: 1886
		private uint m_DataChunkLengthBytes;

		// Token: 0x0400075F RID: 1887
		private IntPtr m_DataChunk;
	}
}
