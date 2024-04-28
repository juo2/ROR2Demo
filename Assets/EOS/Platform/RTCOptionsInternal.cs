using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Platform
{
	// Token: 0x020005E8 RID: 1512
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct RTCOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x17000B89 RID: 2953
		// (get) Token: 0x060024DA RID: 9434 RVA: 0x0002714A File Offset: 0x0002534A
		// (set) Token: 0x060024DB RID: 9435 RVA: 0x00027152 File Offset: 0x00025352
		public IntPtr PlatformSpecificOptions
		{
			get
			{
				return this.m_PlatformSpecificOptions;
			}
			set
			{
				this.m_PlatformSpecificOptions = value;
			}
		}

		// Token: 0x060024DC RID: 9436 RVA: 0x0002715B File Offset: 0x0002535B
		public void Set(RTCOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.PlatformSpecificOptions = other.PlatformSpecificOptions;
			}
		}

		// Token: 0x060024DD RID: 9437 RVA: 0x00027173 File Offset: 0x00025373
		public void Set(object other)
		{
			this.Set(other as RTCOptions);
		}

		// Token: 0x060024DE RID: 9438 RVA: 0x00027181 File Offset: 0x00025381
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_PlatformSpecificOptions);
		}

		// Token: 0x04001171 RID: 4465
		private int m_ApiVersion;

		// Token: 0x04001172 RID: 4466
		private IntPtr m_PlatformSpecificOptions;
	}
}
