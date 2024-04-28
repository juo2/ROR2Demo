using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.UI
{
	// Token: 0x02000059 RID: 89
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct ReportKeyEventOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x17000081 RID: 129
		// (set) Token: 0x060003F7 RID: 1015 RVA: 0x00004DFF File Offset: 0x00002FFF
		public IntPtr PlatformSpecificInputData
		{
			set
			{
				this.m_PlatformSpecificInputData = value;
			}
		}

		// Token: 0x060003F8 RID: 1016 RVA: 0x00004E08 File Offset: 0x00003008
		public void Set(ReportKeyEventOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.PlatformSpecificInputData = other.PlatformSpecificInputData;
			}
		}

		// Token: 0x060003F9 RID: 1017 RVA: 0x00004E20 File Offset: 0x00003020
		public void Set(object other)
		{
			this.Set(other as ReportKeyEventOptions);
		}

		// Token: 0x060003FA RID: 1018 RVA: 0x00004E2E File Offset: 0x0000302E
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_PlatformSpecificInputData);
		}

		// Token: 0x0400021C RID: 540
		private int m_ApiVersion;

		// Token: 0x0400021D RID: 541
		private IntPtr m_PlatformSpecificInputData;
	}
}
