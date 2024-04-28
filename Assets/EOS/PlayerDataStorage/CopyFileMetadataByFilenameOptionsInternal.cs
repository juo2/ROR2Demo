using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.PlayerDataStorage
{
	// Token: 0x02000239 RID: 569
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct CopyFileMetadataByFilenameOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x17000406 RID: 1030
		// (set) Token: 0x06000EAF RID: 3759 RVA: 0x0000FCF0 File Offset: 0x0000DEF0
		public ProductUserId LocalUserId
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_LocalUserId, value);
			}
		}

		// Token: 0x17000407 RID: 1031
		// (set) Token: 0x06000EB0 RID: 3760 RVA: 0x0000FCFF File Offset: 0x0000DEFF
		public string Filename
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_Filename, value);
			}
		}

		// Token: 0x06000EB1 RID: 3761 RVA: 0x0000FD0E File Offset: 0x0000DF0E
		public void Set(CopyFileMetadataByFilenameOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.LocalUserId = other.LocalUserId;
				this.Filename = other.Filename;
			}
		}

		// Token: 0x06000EB2 RID: 3762 RVA: 0x0000FD32 File Offset: 0x0000DF32
		public void Set(object other)
		{
			this.Set(other as CopyFileMetadataByFilenameOptions);
		}

		// Token: 0x06000EB3 RID: 3763 RVA: 0x0000FD40 File Offset: 0x0000DF40
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_LocalUserId);
			Helper.TryMarshalDispose(ref this.m_Filename);
		}

		// Token: 0x040006EC RID: 1772
		private int m_ApiVersion;

		// Token: 0x040006ED RID: 1773
		private IntPtr m_LocalUserId;

		// Token: 0x040006EE RID: 1774
		private IntPtr m_Filename;
	}
}
