using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.PlayerDataStorage
{
	// Token: 0x02000245 RID: 581
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct DuplicateFileOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x17000426 RID: 1062
		// (set) Token: 0x06000EF6 RID: 3830 RVA: 0x0001013F File Offset: 0x0000E33F
		public ProductUserId LocalUserId
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_LocalUserId, value);
			}
		}

		// Token: 0x17000427 RID: 1063
		// (set) Token: 0x06000EF7 RID: 3831 RVA: 0x0001014E File Offset: 0x0000E34E
		public string SourceFilename
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_SourceFilename, value);
			}
		}

		// Token: 0x17000428 RID: 1064
		// (set) Token: 0x06000EF8 RID: 3832 RVA: 0x0001015D File Offset: 0x0000E35D
		public string DestinationFilename
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_DestinationFilename, value);
			}
		}

		// Token: 0x06000EF9 RID: 3833 RVA: 0x0001016C File Offset: 0x0000E36C
		public void Set(DuplicateFileOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.LocalUserId = other.LocalUserId;
				this.SourceFilename = other.SourceFilename;
				this.DestinationFilename = other.DestinationFilename;
			}
		}

		// Token: 0x06000EFA RID: 3834 RVA: 0x0001019C File Offset: 0x0000E39C
		public void Set(object other)
		{
			this.Set(other as DuplicateFileOptions);
		}

		// Token: 0x06000EFB RID: 3835 RVA: 0x000101AA File Offset: 0x0000E3AA
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_LocalUserId);
			Helper.TryMarshalDispose(ref this.m_SourceFilename);
			Helper.TryMarshalDispose(ref this.m_DestinationFilename);
		}

		// Token: 0x0400070C RID: 1804
		private int m_ApiVersion;

		// Token: 0x0400070D RID: 1805
		private IntPtr m_LocalUserId;

		// Token: 0x0400070E RID: 1806
		private IntPtr m_SourceFilename;

		// Token: 0x0400070F RID: 1807
		private IntPtr m_DestinationFilename;
	}
}
