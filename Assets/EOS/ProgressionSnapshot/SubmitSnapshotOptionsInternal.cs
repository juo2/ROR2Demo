using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.ProgressionSnapshot
{
	// Token: 0x02000202 RID: 514
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct SubmitSnapshotOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x170003A7 RID: 935
		// (set) Token: 0x06000D81 RID: 3457 RVA: 0x0000E8F5 File Offset: 0x0000CAF5
		public uint SnapshotId
		{
			set
			{
				this.m_SnapshotId = value;
			}
		}

		// Token: 0x06000D82 RID: 3458 RVA: 0x0000E8FE File Offset: 0x0000CAFE
		public void Set(SubmitSnapshotOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.SnapshotId = other.SnapshotId;
			}
		}

		// Token: 0x06000D83 RID: 3459 RVA: 0x0000E916 File Offset: 0x0000CB16
		public void Set(object other)
		{
			this.Set(other as SubmitSnapshotOptions);
		}

		// Token: 0x06000D84 RID: 3460 RVA: 0x000038B7 File Offset: 0x00001AB7
		public void Dispose()
		{
		}

		// Token: 0x0400065D RID: 1629
		private int m_ApiVersion;

		// Token: 0x0400065E RID: 1630
		private uint m_SnapshotId;
	}
}
