using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.TitleStorage
{
	// Token: 0x0200006C RID: 108
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct FileMetadataInternal : ISettable, IDisposable
	{
		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x06000462 RID: 1122 RVA: 0x000055B5 File Offset: 0x000037B5
		// (set) Token: 0x06000463 RID: 1123 RVA: 0x000055BD File Offset: 0x000037BD
		public uint FileSizeBytes
		{
			get
			{
				return this.m_FileSizeBytes;
			}
			set
			{
				this.m_FileSizeBytes = value;
			}
		}

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x06000464 RID: 1124 RVA: 0x000055C8 File Offset: 0x000037C8
		// (set) Token: 0x06000465 RID: 1125 RVA: 0x000055E4 File Offset: 0x000037E4
		public string MD5Hash
		{
			get
			{
				string result;
				Helper.TryMarshalGet(this.m_MD5Hash, out result);
				return result;
			}
			set
			{
				Helper.TryMarshalSet(ref this.m_MD5Hash, value);
			}
		}

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x06000466 RID: 1126 RVA: 0x000055F4 File Offset: 0x000037F4
		// (set) Token: 0x06000467 RID: 1127 RVA: 0x00005610 File Offset: 0x00003810
		public string Filename
		{
			get
			{
				string result;
				Helper.TryMarshalGet(this.m_Filename, out result);
				return result;
			}
			set
			{
				Helper.TryMarshalSet(ref this.m_Filename, value);
			}
		}

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x06000468 RID: 1128 RVA: 0x0000561F File Offset: 0x0000381F
		// (set) Token: 0x06000469 RID: 1129 RVA: 0x00005627 File Offset: 0x00003827
		public uint UnencryptedDataSizeBytes
		{
			get
			{
				return this.m_UnencryptedDataSizeBytes;
			}
			set
			{
				this.m_UnencryptedDataSizeBytes = value;
			}
		}

		// Token: 0x0600046A RID: 1130 RVA: 0x00005630 File Offset: 0x00003830
		public void Set(FileMetadata other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 2;
				this.FileSizeBytes = other.FileSizeBytes;
				this.MD5Hash = other.MD5Hash;
				this.Filename = other.Filename;
				this.UnencryptedDataSizeBytes = other.UnencryptedDataSizeBytes;
			}
		}

		// Token: 0x0600046B RID: 1131 RVA: 0x0000566C File Offset: 0x0000386C
		public void Set(object other)
		{
			this.Set(other as FileMetadata);
		}

		// Token: 0x0600046C RID: 1132 RVA: 0x0000567A File Offset: 0x0000387A
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_MD5Hash);
			Helper.TryMarshalDispose(ref this.m_Filename);
		}

		// Token: 0x04000250 RID: 592
		private int m_ApiVersion;

		// Token: 0x04000251 RID: 593
		private uint m_FileSizeBytes;

		// Token: 0x04000252 RID: 594
		private IntPtr m_MD5Hash;

		// Token: 0x04000253 RID: 595
		private IntPtr m_Filename;

		// Token: 0x04000254 RID: 596
		private uint m_UnencryptedDataSizeBytes;
	}
}
