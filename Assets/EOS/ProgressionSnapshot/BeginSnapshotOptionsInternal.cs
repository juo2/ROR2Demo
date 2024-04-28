using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.ProgressionSnapshot
{
	// Token: 0x020001F3 RID: 499
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct BeginSnapshotOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x17000393 RID: 915
		// (set) Token: 0x06000D37 RID: 3383 RVA: 0x0000E4BF File Offset: 0x0000C6BF
		public ProductUserId LocalUserId
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_LocalUserId, value);
			}
		}

		// Token: 0x06000D38 RID: 3384 RVA: 0x0000E4CE File Offset: 0x0000C6CE
		public void Set(BeginSnapshotOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.LocalUserId = other.LocalUserId;
			}
		}

		// Token: 0x06000D39 RID: 3385 RVA: 0x0000E4E6 File Offset: 0x0000C6E6
		public void Set(object other)
		{
			this.Set(other as BeginSnapshotOptions);
		}

		// Token: 0x06000D3A RID: 3386 RVA: 0x0000E4F4 File Offset: 0x0000C6F4
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_LocalUserId);
		}

		// Token: 0x04000642 RID: 1602
		private int m_ApiVersion;

		// Token: 0x04000643 RID: 1603
		private IntPtr m_LocalUserId;
	}
}
