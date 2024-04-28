using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.UI
{
	// Token: 0x02000057 RID: 87
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct PrePresentOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x1700007F RID: 127
		// (set) Token: 0x060003F0 RID: 1008 RVA: 0x00004DB1 File Offset: 0x00002FB1
		public IntPtr PlatformSpecificData
		{
			set
			{
				this.m_PlatformSpecificData = value;
			}
		}

		// Token: 0x060003F1 RID: 1009 RVA: 0x00004DBA File Offset: 0x00002FBA
		public void Set(PrePresentOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.PlatformSpecificData = other.PlatformSpecificData;
			}
		}

		// Token: 0x060003F2 RID: 1010 RVA: 0x00004DD2 File Offset: 0x00002FD2
		public void Set(object other)
		{
			this.Set(other as PrePresentOptions);
		}

		// Token: 0x060003F3 RID: 1011 RVA: 0x00004DE0 File Offset: 0x00002FE0
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_PlatformSpecificData);
		}

		// Token: 0x04000219 RID: 537
		private int m_ApiVersion;

		// Token: 0x0400021A RID: 538
		private IntPtr m_PlatformSpecificData;
	}
}
