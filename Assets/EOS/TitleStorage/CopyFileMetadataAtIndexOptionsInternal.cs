using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.TitleStorage
{
	// Token: 0x02000064 RID: 100
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct CopyFileMetadataAtIndexOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x17000091 RID: 145
		// (set) Token: 0x06000433 RID: 1075 RVA: 0x000052CA File Offset: 0x000034CA
		public ProductUserId LocalUserId
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_LocalUserId, value);
			}
		}

		// Token: 0x17000092 RID: 146
		// (set) Token: 0x06000434 RID: 1076 RVA: 0x000052D9 File Offset: 0x000034D9
		public uint Index
		{
			set
			{
				this.m_Index = value;
			}
		}

		// Token: 0x06000435 RID: 1077 RVA: 0x000052E2 File Offset: 0x000034E2
		public void Set(CopyFileMetadataAtIndexOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.LocalUserId = other.LocalUserId;
				this.Index = other.Index;
			}
		}

		// Token: 0x06000436 RID: 1078 RVA: 0x00005306 File Offset: 0x00003506
		public void Set(object other)
		{
			this.Set(other as CopyFileMetadataAtIndexOptions);
		}

		// Token: 0x06000437 RID: 1079 RVA: 0x00005314 File Offset: 0x00003514
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_LocalUserId);
		}

		// Token: 0x0400023B RID: 571
		private int m_ApiVersion;

		// Token: 0x0400023C RID: 572
		private IntPtr m_LocalUserId;

		// Token: 0x0400023D RID: 573
		private uint m_Index;
	}
}
