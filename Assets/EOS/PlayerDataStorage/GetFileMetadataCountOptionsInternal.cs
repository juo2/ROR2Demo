using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.PlayerDataStorage
{
	// Token: 0x0200024B RID: 587
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct GetFileMetadataCountOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x1700043F RID: 1087
		// (set) Token: 0x06000F2D RID: 3885 RVA: 0x00010559 File Offset: 0x0000E759
		public ProductUserId LocalUserId
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_LocalUserId, value);
			}
		}

		// Token: 0x06000F2E RID: 3886 RVA: 0x00010568 File Offset: 0x0000E768
		public void Set(GetFileMetadataCountOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.LocalUserId = other.LocalUserId;
			}
		}

		// Token: 0x06000F2F RID: 3887 RVA: 0x00010580 File Offset: 0x0000E780
		public void Set(object other)
		{
			this.Set(other as GetFileMetadataCountOptions);
		}

		// Token: 0x06000F30 RID: 3888 RVA: 0x0001058E File Offset: 0x0000E78E
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_LocalUserId);
		}

		// Token: 0x04000726 RID: 1830
		private int m_ApiVersion;

		// Token: 0x04000727 RID: 1831
		private IntPtr m_LocalUserId;
	}
}
