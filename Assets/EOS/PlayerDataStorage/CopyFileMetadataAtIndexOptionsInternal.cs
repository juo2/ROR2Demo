using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.PlayerDataStorage
{
	// Token: 0x02000237 RID: 567
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct CopyFileMetadataAtIndexOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x17000402 RID: 1026
		// (set) Token: 0x06000EA5 RID: 3749 RVA: 0x0000FC76 File Offset: 0x0000DE76
		public ProductUserId LocalUserId
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_LocalUserId, value);
			}
		}

		// Token: 0x17000403 RID: 1027
		// (set) Token: 0x06000EA6 RID: 3750 RVA: 0x0000FC85 File Offset: 0x0000DE85
		public uint Index
		{
			set
			{
				this.m_Index = value;
			}
		}

		// Token: 0x06000EA7 RID: 3751 RVA: 0x0000FC8E File Offset: 0x0000DE8E
		public void Set(CopyFileMetadataAtIndexOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.LocalUserId = other.LocalUserId;
				this.Index = other.Index;
			}
		}

		// Token: 0x06000EA8 RID: 3752 RVA: 0x0000FCB2 File Offset: 0x0000DEB2
		public void Set(object other)
		{
			this.Set(other as CopyFileMetadataAtIndexOptions);
		}

		// Token: 0x06000EA9 RID: 3753 RVA: 0x0000FCC0 File Offset: 0x0000DEC0
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_LocalUserId);
		}

		// Token: 0x040006E7 RID: 1767
		private int m_ApiVersion;

		// Token: 0x040006E8 RID: 1768
		private IntPtr m_LocalUserId;

		// Token: 0x040006E9 RID: 1769
		private uint m_Index;
	}
}
