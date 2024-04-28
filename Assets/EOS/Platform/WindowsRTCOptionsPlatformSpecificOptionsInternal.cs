using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Platform
{
	// Token: 0x020005F0 RID: 1520
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct WindowsRTCOptionsPlatformSpecificOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x17000BA7 RID: 2983
		// (get) Token: 0x06002521 RID: 9505 RVA: 0x00027570 File Offset: 0x00025770
		// (set) Token: 0x06002522 RID: 9506 RVA: 0x0002758C File Offset: 0x0002578C
		public string XAudio29DllPath
		{
			get
			{
				string result;
				Helper.TryMarshalGet(this.m_XAudio29DllPath, out result);
				return result;
			}
			set
			{
				Helper.TryMarshalSet(ref this.m_XAudio29DllPath, value);
			}
		}

		// Token: 0x06002523 RID: 9507 RVA: 0x0002759B File Offset: 0x0002579B
		public void Set(WindowsRTCOptionsPlatformSpecificOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.XAudio29DllPath = other.XAudio29DllPath;
			}
		}

		// Token: 0x06002524 RID: 9508 RVA: 0x000275B3 File Offset: 0x000257B3
		public void Set(object other)
		{
			this.Set(other as WindowsRTCOptionsPlatformSpecificOptions);
		}

		// Token: 0x06002525 RID: 9509 RVA: 0x000275C1 File Offset: 0x000257C1
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_XAudio29DllPath);
		}

		// Token: 0x04001192 RID: 4498
		private int m_ApiVersion;

		// Token: 0x04001193 RID: 4499
		private IntPtr m_XAudio29DllPath;
	}
}
