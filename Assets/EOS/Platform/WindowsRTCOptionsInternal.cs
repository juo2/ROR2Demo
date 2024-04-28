using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Platform
{
	// Token: 0x020005EE RID: 1518
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct WindowsRTCOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x17000BA5 RID: 2981
		// (get) Token: 0x06002517 RID: 9495 RVA: 0x000274C0 File Offset: 0x000256C0
		// (set) Token: 0x06002518 RID: 9496 RVA: 0x000274DC File Offset: 0x000256DC
		public WindowsRTCOptionsPlatformSpecificOptions PlatformSpecificOptions
		{
			get
			{
				WindowsRTCOptionsPlatformSpecificOptions result;
				Helper.TryMarshalGet<WindowsRTCOptionsPlatformSpecificOptionsInternal, WindowsRTCOptionsPlatformSpecificOptions>(this.m_PlatformSpecificOptions, out result);
				return result;
			}
			set
			{
				Helper.TryMarshalSet<WindowsRTCOptionsPlatformSpecificOptionsInternal, WindowsRTCOptionsPlatformSpecificOptions>(ref this.m_PlatformSpecificOptions, value);
			}
		}

		// Token: 0x06002519 RID: 9497 RVA: 0x000274EB File Offset: 0x000256EB
		public void Set(WindowsRTCOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.PlatformSpecificOptions = other.PlatformSpecificOptions;
			}
		}

		// Token: 0x0600251A RID: 9498 RVA: 0x00027503 File Offset: 0x00025703
		public void Set(object other)
		{
			this.Set(other as WindowsRTCOptions);
		}

		// Token: 0x0600251B RID: 9499 RVA: 0x00027511 File Offset: 0x00025711
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_PlatformSpecificOptions);
		}

		// Token: 0x0400118F RID: 4495
		private int m_ApiVersion;

		// Token: 0x04001190 RID: 4496
		private IntPtr m_PlatformSpecificOptions;
	}
}
