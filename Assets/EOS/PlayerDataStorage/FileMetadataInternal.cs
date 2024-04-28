using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.PlayerDataStorage
{
	// Token: 0x02000247 RID: 583
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct FileMetadataInternal : ISettable, IDisposable
	{
		// Token: 0x1700042E RID: 1070
		// (get) Token: 0x06000F09 RID: 3849 RVA: 0x000102BA File Offset: 0x0000E4BA
		// (set) Token: 0x06000F0A RID: 3850 RVA: 0x000102C2 File Offset: 0x0000E4C2
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

		// Token: 0x1700042F RID: 1071
		// (get) Token: 0x06000F0B RID: 3851 RVA: 0x000102CC File Offset: 0x0000E4CC
		// (set) Token: 0x06000F0C RID: 3852 RVA: 0x000102E8 File Offset: 0x0000E4E8
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

		// Token: 0x17000430 RID: 1072
		// (get) Token: 0x06000F0D RID: 3853 RVA: 0x000102F8 File Offset: 0x0000E4F8
		// (set) Token: 0x06000F0E RID: 3854 RVA: 0x00010314 File Offset: 0x0000E514
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

		// Token: 0x17000431 RID: 1073
		// (get) Token: 0x06000F0F RID: 3855 RVA: 0x00010324 File Offset: 0x0000E524
		// (set) Token: 0x06000F10 RID: 3856 RVA: 0x00010340 File Offset: 0x0000E540
		public DateTimeOffset? LastModifiedTime
		{
			get
			{
				DateTimeOffset? result;
				Helper.TryMarshalGet(this.m_LastModifiedTime, out result);
				return result;
			}
			set
			{
				Helper.TryMarshalSet(ref this.m_LastModifiedTime, value);
			}
		}

		// Token: 0x17000432 RID: 1074
		// (get) Token: 0x06000F11 RID: 3857 RVA: 0x0001034F File Offset: 0x0000E54F
		// (set) Token: 0x06000F12 RID: 3858 RVA: 0x00010357 File Offset: 0x0000E557
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

		// Token: 0x06000F13 RID: 3859 RVA: 0x00010360 File Offset: 0x0000E560
		public void Set(FileMetadata other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 3;
				this.FileSizeBytes = other.FileSizeBytes;
				this.MD5Hash = other.MD5Hash;
				this.Filename = other.Filename;
				this.LastModifiedTime = other.LastModifiedTime;
				this.UnencryptedDataSizeBytes = other.UnencryptedDataSizeBytes;
			}
		}

		// Token: 0x06000F14 RID: 3860 RVA: 0x000103B3 File Offset: 0x0000E5B3
		public void Set(object other)
		{
			this.Set(other as FileMetadata);
		}

		// Token: 0x06000F15 RID: 3861 RVA: 0x000103C1 File Offset: 0x0000E5C1
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_MD5Hash);
			Helper.TryMarshalDispose(ref this.m_Filename);
		}

		// Token: 0x04000715 RID: 1813
		private int m_ApiVersion;

		// Token: 0x04000716 RID: 1814
		private uint m_FileSizeBytes;

		// Token: 0x04000717 RID: 1815
		private IntPtr m_MD5Hash;

		// Token: 0x04000718 RID: 1816
		private IntPtr m_Filename;

		// Token: 0x04000719 RID: 1817
		private long m_LastModifiedTime;

		// Token: 0x0400071A RID: 1818
		private uint m_UnencryptedDataSizeBytes;
	}
}
