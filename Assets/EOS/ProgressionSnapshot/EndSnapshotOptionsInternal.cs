using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.ProgressionSnapshot
{
	// Token: 0x020001F9 RID: 505
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct EndSnapshotOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x1700039E RID: 926
		// (set) Token: 0x06000D53 RID: 3411 RVA: 0x0000E659 File Offset: 0x0000C859
		public uint SnapshotId
		{
			set
			{
				this.m_SnapshotId = value;
			}
		}

		// Token: 0x06000D54 RID: 3412 RVA: 0x0000E662 File Offset: 0x0000C862
		public void Set(EndSnapshotOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.SnapshotId = other.SnapshotId;
			}
		}

		// Token: 0x06000D55 RID: 3413 RVA: 0x0000E67A File Offset: 0x0000C87A
		public void Set(object other)
		{
			this.Set(other as EndSnapshotOptions);
		}

		// Token: 0x06000D56 RID: 3414 RVA: 0x000038B7 File Offset: 0x00001AB7
		public void Dispose()
		{
		}

		// Token: 0x0400064E RID: 1614
		private int m_ApiVersion;

		// Token: 0x0400064F RID: 1615
		private uint m_SnapshotId;
	}
}
