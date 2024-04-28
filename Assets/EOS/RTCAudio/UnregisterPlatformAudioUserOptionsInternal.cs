using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.RTCAudio
{
	// Token: 0x020001A7 RID: 423
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct UnregisterPlatformAudioUserOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x170002C7 RID: 711
		// (set) Token: 0x06000B37 RID: 2871 RVA: 0x0000C4AD File Offset: 0x0000A6AD
		public string UserId
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_UserId, value);
			}
		}

		// Token: 0x06000B38 RID: 2872 RVA: 0x0000C4BC File Offset: 0x0000A6BC
		public void Set(UnregisterPlatformAudioUserOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.UserId = other.UserId;
			}
		}

		// Token: 0x06000B39 RID: 2873 RVA: 0x0000C4D4 File Offset: 0x0000A6D4
		public void Set(object other)
		{
			this.Set(other as UnregisterPlatformAudioUserOptions);
		}

		// Token: 0x06000B3A RID: 2874 RVA: 0x0000C4E2 File Offset: 0x0000A6E2
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_UserId);
		}

		// Token: 0x04000556 RID: 1366
		private int m_ApiVersion;

		// Token: 0x04000557 RID: 1367
		private IntPtr m_UserId;
	}
}
