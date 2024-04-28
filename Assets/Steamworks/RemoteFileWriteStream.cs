using System;
using System.IO;
using SteamNative;

namespace Facepunch.Steamworks
{
	// Token: 0x0200016E RID: 366
	public class RemoteFileWriteStream : Stream
	{
		// Token: 0x06000B32 RID: 2866 RVA: 0x000372EE File Offset: 0x000354EE
		internal RemoteFileWriteStream(RemoteStorage r, RemoteFile file)
		{
			this.remoteStorage = r;
			this._handle = this.remoteStorage.native.FileWriteStreamOpen(file.FileName);
			this._file = file;
		}

		// Token: 0x06000B33 RID: 2867 RVA: 0x00037320 File Offset: 0x00035520
		public override void Flush()
		{
		}

		// Token: 0x06000B34 RID: 2868 RVA: 0x00037322 File Offset: 0x00035522
		public override int Read(byte[] buffer, int offset, int count)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000B35 RID: 2869 RVA: 0x00037329 File Offset: 0x00035529
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000B36 RID: 2870 RVA: 0x00037330 File Offset: 0x00035530
		public override void SetLength(long value)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000B37 RID: 2871 RVA: 0x00037338 File Offset: 0x00035538
		public unsafe override void Write(byte[] buffer, int offset, int count)
		{
			if (this._closed)
			{
				throw new ObjectDisposedException(this.ToString());
			}
			fixed (byte[] array = buffer)
			{
				byte* ptr;
				if (buffer == null || array.Length == 0)
				{
					ptr = null;
				}
				else
				{
					ptr = &array[0];
				}
				if (this.remoteStorage.native.FileWriteStreamWriteChunk(this._handle, (IntPtr)((void*)(ptr + offset)), count))
				{
					this._written += count;
				}
			}
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x06000B38 RID: 2872 RVA: 0x000373A2 File Offset: 0x000355A2
		public override bool CanRead
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x06000B39 RID: 2873 RVA: 0x000373A5 File Offset: 0x000355A5
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x06000B3A RID: 2874 RVA: 0x000373A8 File Offset: 0x000355A8
		public override bool CanWrite
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x06000B3B RID: 2875 RVA: 0x000373AB File Offset: 0x000355AB
		public override long Length
		{
			get
			{
				return (long)this._written;
			}
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x06000B3C RID: 2876 RVA: 0x000373B4 File Offset: 0x000355B4
		// (set) Token: 0x06000B3D RID: 2877 RVA: 0x000373BD File Offset: 0x000355BD
		public override long Position
		{
			get
			{
				return (long)this._written;
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x06000B3E RID: 2878 RVA: 0x000373C4 File Offset: 0x000355C4
		public void Cancel()
		{
			if (this._closed)
			{
				return;
			}
			this._closed = true;
			this.remoteStorage.native.FileWriteStreamCancel(this._handle);
		}

		// Token: 0x06000B3F RID: 2879 RVA: 0x000373ED File Offset: 0x000355ED
		public override void Close()
		{
			if (this._closed)
			{
				return;
			}
			this._closed = true;
			this.remoteStorage.native.FileWriteStreamClose(this._handle);
			this._file.remoteStorage.OnWrittenNewFile(this._file);
		}

		// Token: 0x06000B40 RID: 2880 RVA: 0x0003742C File Offset: 0x0003562C
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.Close();
			}
			base.Dispose(disposing);
		}

		// Token: 0x04000829 RID: 2089
		internal readonly RemoteStorage remoteStorage;

		// Token: 0x0400082A RID: 2090
		private readonly UGCFileWriteStreamHandle_t _handle;

		// Token: 0x0400082B RID: 2091
		private readonly RemoteFile _file;

		// Token: 0x0400082C RID: 2092
		private int _written;

		// Token: 0x0400082D RID: 2093
		private bool _closed;
	}
}
