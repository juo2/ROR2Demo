using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.PlayerDataStorage
{
	// Token: 0x0200026F RID: 623
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct ReadFileOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x17000471 RID: 1137
		// (set) Token: 0x06001000 RID: 4096 RVA: 0x0001113B File Offset: 0x0000F33B
		public ProductUserId LocalUserId
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_LocalUserId, value);
			}
		}

		// Token: 0x17000472 RID: 1138
		// (set) Token: 0x06001001 RID: 4097 RVA: 0x0001114A File Offset: 0x0000F34A
		public string Filename
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_Filename, value);
			}
		}

		// Token: 0x17000473 RID: 1139
		// (set) Token: 0x06001002 RID: 4098 RVA: 0x00011159 File Offset: 0x0000F359
		public uint ReadChunkLengthBytes
		{
			set
			{
				this.m_ReadChunkLengthBytes = value;
			}
		}

		// Token: 0x17000474 RID: 1140
		// (get) Token: 0x06001003 RID: 4099 RVA: 0x00011162 File Offset: 0x0000F362
		public static OnReadFileDataCallbackInternal ReadFileDataCallback
		{
			get
			{
				if (ReadFileOptionsInternal.s_ReadFileDataCallback == null)
				{
					ReadFileOptionsInternal.s_ReadFileDataCallback = new OnReadFileDataCallbackInternal(PlayerDataStorageInterface.OnReadFileDataCallbackInternalImplementation);
				}
				return ReadFileOptionsInternal.s_ReadFileDataCallback;
			}
		}

		// Token: 0x17000475 RID: 1141
		// (get) Token: 0x06001004 RID: 4100 RVA: 0x00011181 File Offset: 0x0000F381
		public static OnFileTransferProgressCallbackInternal FileTransferProgressCallback
		{
			get
			{
				if (ReadFileOptionsInternal.s_FileTransferProgressCallback == null)
				{
					ReadFileOptionsInternal.s_FileTransferProgressCallback = new OnFileTransferProgressCallbackInternal(PlayerDataStorageInterface.OnFileTransferProgressCallbackInternalImplementation);
				}
				return ReadFileOptionsInternal.s_FileTransferProgressCallback;
			}
		}

		// Token: 0x06001005 RID: 4101 RVA: 0x000111A0 File Offset: 0x0000F3A0
		public void Set(ReadFileOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.LocalUserId = other.LocalUserId;
				this.Filename = other.Filename;
				this.ReadChunkLengthBytes = other.ReadChunkLengthBytes;
				this.m_ReadFileDataCallback = ((other.ReadFileDataCallback != null) ? Marshal.GetFunctionPointerForDelegate<OnReadFileDataCallbackInternal>(ReadFileOptionsInternal.ReadFileDataCallback) : IntPtr.Zero);
				this.m_FileTransferProgressCallback = ((other.FileTransferProgressCallback != null) ? Marshal.GetFunctionPointerForDelegate<OnFileTransferProgressCallbackInternal>(ReadFileOptionsInternal.FileTransferProgressCallback) : IntPtr.Zero);
			}
		}

		// Token: 0x06001006 RID: 4102 RVA: 0x00011219 File Offset: 0x0000F419
		public void Set(object other)
		{
			this.Set(other as ReadFileOptions);
		}

		// Token: 0x06001007 RID: 4103 RVA: 0x00011227 File Offset: 0x0000F427
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_LocalUserId);
			Helper.TryMarshalDispose(ref this.m_Filename);
			Helper.TryMarshalDispose(ref this.m_ReadFileDataCallback);
			Helper.TryMarshalDispose(ref this.m_FileTransferProgressCallback);
		}

		// Token: 0x04000765 RID: 1893
		private int m_ApiVersion;

		// Token: 0x04000766 RID: 1894
		private IntPtr m_LocalUserId;

		// Token: 0x04000767 RID: 1895
		private IntPtr m_Filename;

		// Token: 0x04000768 RID: 1896
		private uint m_ReadChunkLengthBytes;

		// Token: 0x04000769 RID: 1897
		private IntPtr m_ReadFileDataCallback;

		// Token: 0x0400076A RID: 1898
		private IntPtr m_FileTransferProgressCallback;

		// Token: 0x0400076B RID: 1899
		private static OnReadFileDataCallbackInternal s_ReadFileDataCallback;

		// Token: 0x0400076C RID: 1900
		private static OnFileTransferProgressCallbackInternal s_FileTransferProgressCallback;
	}
}
