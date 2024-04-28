using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.PlayerDataStorage
{
	// Token: 0x02000276 RID: 630
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct WriteFileOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x1700048D RID: 1165
		// (set) Token: 0x06001035 RID: 4149 RVA: 0x00011525 File Offset: 0x0000F725
		public ProductUserId LocalUserId
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_LocalUserId, value);
			}
		}

		// Token: 0x1700048E RID: 1166
		// (set) Token: 0x06001036 RID: 4150 RVA: 0x00011534 File Offset: 0x0000F734
		public string Filename
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_Filename, value);
			}
		}

		// Token: 0x1700048F RID: 1167
		// (set) Token: 0x06001037 RID: 4151 RVA: 0x00011543 File Offset: 0x0000F743
		public uint ChunkLengthBytes
		{
			set
			{
				this.m_ChunkLengthBytes = value;
			}
		}

		// Token: 0x17000490 RID: 1168
		// (get) Token: 0x06001038 RID: 4152 RVA: 0x0001154C File Offset: 0x0000F74C
		public static OnWriteFileDataCallbackInternal WriteFileDataCallback
		{
			get
			{
				if (WriteFileOptionsInternal.s_WriteFileDataCallback == null)
				{
					WriteFileOptionsInternal.s_WriteFileDataCallback = new OnWriteFileDataCallbackInternal(PlayerDataStorageInterface.OnWriteFileDataCallbackInternalImplementation);
				}
				return WriteFileOptionsInternal.s_WriteFileDataCallback;
			}
		}

		// Token: 0x17000491 RID: 1169
		// (get) Token: 0x06001039 RID: 4153 RVA: 0x0001156B File Offset: 0x0000F76B
		public static OnFileTransferProgressCallbackInternal FileTransferProgressCallback
		{
			get
			{
				if (WriteFileOptionsInternal.s_FileTransferProgressCallback == null)
				{
					WriteFileOptionsInternal.s_FileTransferProgressCallback = new OnFileTransferProgressCallbackInternal(PlayerDataStorageInterface.OnFileTransferProgressCallbackInternalImplementation);
				}
				return WriteFileOptionsInternal.s_FileTransferProgressCallback;
			}
		}

		// Token: 0x0600103A RID: 4154 RVA: 0x0001158C File Offset: 0x0000F78C
		public void Set(WriteFileOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.LocalUserId = other.LocalUserId;
				this.Filename = other.Filename;
				this.ChunkLengthBytes = other.ChunkLengthBytes;
				this.m_WriteFileDataCallback = ((other.WriteFileDataCallback != null) ? Marshal.GetFunctionPointerForDelegate<OnWriteFileDataCallbackInternal>(WriteFileOptionsInternal.WriteFileDataCallback) : IntPtr.Zero);
				this.m_FileTransferProgressCallback = ((other.FileTransferProgressCallback != null) ? Marshal.GetFunctionPointerForDelegate<OnFileTransferProgressCallbackInternal>(WriteFileOptionsInternal.FileTransferProgressCallback) : IntPtr.Zero);
			}
		}

		// Token: 0x0600103B RID: 4155 RVA: 0x00011605 File Offset: 0x0000F805
		public void Set(object other)
		{
			this.Set(other as WriteFileOptions);
		}

		// Token: 0x0600103C RID: 4156 RVA: 0x00011613 File Offset: 0x0000F813
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_LocalUserId);
			Helper.TryMarshalDispose(ref this.m_Filename);
			Helper.TryMarshalDispose(ref this.m_WriteFileDataCallback);
			Helper.TryMarshalDispose(ref this.m_FileTransferProgressCallback);
		}

		// Token: 0x04000786 RID: 1926
		private int m_ApiVersion;

		// Token: 0x04000787 RID: 1927
		private IntPtr m_LocalUserId;

		// Token: 0x04000788 RID: 1928
		private IntPtr m_Filename;

		// Token: 0x04000789 RID: 1929
		private uint m_ChunkLengthBytes;

		// Token: 0x0400078A RID: 1930
		private IntPtr m_WriteFileDataCallback;

		// Token: 0x0400078B RID: 1931
		private IntPtr m_FileTransferProgressCallback;

		// Token: 0x0400078C RID: 1932
		private static OnWriteFileDataCallbackInternal s_WriteFileDataCallback;

		// Token: 0x0400078D RID: 1933
		private static OnFileTransferProgressCallbackInternal s_FileTransferProgressCallback;
	}
}
