using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.ProgressionSnapshot
{
	// Token: 0x020001F7 RID: 503
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct DeleteSnapshotOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x1700039C RID: 924
		// (set) Token: 0x06000D4C RID: 3404 RVA: 0x0000E605 File Offset: 0x0000C805
		public ProductUserId LocalUserId
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_LocalUserId, value);
			}
		}

		// Token: 0x06000D4D RID: 3405 RVA: 0x0000E614 File Offset: 0x0000C814
		public void Set(DeleteSnapshotOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.LocalUserId = other.LocalUserId;
			}
		}

		// Token: 0x06000D4E RID: 3406 RVA: 0x0000E62C File Offset: 0x0000C82C
		public void Set(object other)
		{
			this.Set(other as DeleteSnapshotOptions);
		}

		// Token: 0x06000D4F RID: 3407 RVA: 0x0000E63A File Offset: 0x0000C83A
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_LocalUserId);
		}

		// Token: 0x0400064B RID: 1611
		private int m_ApiVersion;

		// Token: 0x0400064C RID: 1612
		private IntPtr m_LocalUserId;
	}
}
