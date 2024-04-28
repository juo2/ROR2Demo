using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.TitleStorage
{
	// Token: 0x02000070 RID: 112
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct GetFileMetadataCountOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x170000B4 RID: 180
		// (set) Token: 0x06000484 RID: 1156 RVA: 0x00005815 File Offset: 0x00003A15
		public ProductUserId LocalUserId
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_LocalUserId, value);
			}
		}

		// Token: 0x06000485 RID: 1157 RVA: 0x00005824 File Offset: 0x00003A24
		public void Set(GetFileMetadataCountOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.LocalUserId = other.LocalUserId;
			}
		}

		// Token: 0x06000486 RID: 1158 RVA: 0x0000583C File Offset: 0x00003A3C
		public void Set(object other)
		{
			this.Set(other as GetFileMetadataCountOptions);
		}

		// Token: 0x06000487 RID: 1159 RVA: 0x0000584A File Offset: 0x00003A4A
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_LocalUserId);
		}

		// Token: 0x04000260 RID: 608
		private int m_ApiVersion;

		// Token: 0x04000261 RID: 609
		private IntPtr m_LocalUserId;
	}
}
