using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.TitleStorage
{
	// Token: 0x0200008A RID: 138
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct ReadFileOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x170000E8 RID: 232
		// (set) Token: 0x0600051E RID: 1310 RVA: 0x00005EDF File Offset: 0x000040DF
		public ProductUserId LocalUserId
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_LocalUserId, value);
			}
		}

		// Token: 0x170000E9 RID: 233
		// (set) Token: 0x0600051F RID: 1311 RVA: 0x00005EEE File Offset: 0x000040EE
		public string Filename
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_Filename, value);
			}
		}

		// Token: 0x170000EA RID: 234
		// (set) Token: 0x06000520 RID: 1312 RVA: 0x00005EFD File Offset: 0x000040FD
		public uint ReadChunkLengthBytes
		{
			set
			{
				this.m_ReadChunkLengthBytes = value;
			}
		}

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x06000521 RID: 1313 RVA: 0x00005F06 File Offset: 0x00004106
		public static OnReadFileDataCallbackInternal ReadFileDataCallback
		{
			get
			{
				if (ReadFileOptionsInternal.s_ReadFileDataCallback == null)
				{
					ReadFileOptionsInternal.s_ReadFileDataCallback = new OnReadFileDataCallbackInternal(TitleStorageInterface.OnReadFileDataCallbackInternalImplementation);
				}
				return ReadFileOptionsInternal.s_ReadFileDataCallback;
			}
		}

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x06000522 RID: 1314 RVA: 0x00005F25 File Offset: 0x00004125
		public static OnFileTransferProgressCallbackInternal FileTransferProgressCallback
		{
			get
			{
				if (ReadFileOptionsInternal.s_FileTransferProgressCallback == null)
				{
					ReadFileOptionsInternal.s_FileTransferProgressCallback = new OnFileTransferProgressCallbackInternal(TitleStorageInterface.OnFileTransferProgressCallbackInternalImplementation);
				}
				return ReadFileOptionsInternal.s_FileTransferProgressCallback;
			}
		}

		// Token: 0x06000523 RID: 1315 RVA: 0x00005F44 File Offset: 0x00004144
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

		// Token: 0x06000524 RID: 1316 RVA: 0x00005FBD File Offset: 0x000041BD
		public void Set(object other)
		{
			this.Set(other as ReadFileOptions);
		}

		// Token: 0x06000525 RID: 1317 RVA: 0x00005FCB File Offset: 0x000041CB
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_LocalUserId);
			Helper.TryMarshalDispose(ref this.m_Filename);
			Helper.TryMarshalDispose(ref this.m_ReadFileDataCallback);
			Helper.TryMarshalDispose(ref this.m_FileTransferProgressCallback);
		}

		// Token: 0x04000295 RID: 661
		private int m_ApiVersion;

		// Token: 0x04000296 RID: 662
		private IntPtr m_LocalUserId;

		// Token: 0x04000297 RID: 663
		private IntPtr m_Filename;

		// Token: 0x04000298 RID: 664
		private uint m_ReadChunkLengthBytes;

		// Token: 0x04000299 RID: 665
		private IntPtr m_ReadFileDataCallback;

		// Token: 0x0400029A RID: 666
		private IntPtr m_FileTransferProgressCallback;

		// Token: 0x0400029B RID: 667
		private static OnReadFileDataCallbackInternal s_ReadFileDataCallback;

		// Token: 0x0400029C RID: 668
		private static OnFileTransferProgressCallbackInternal s_FileTransferProgressCallback;
	}
}
