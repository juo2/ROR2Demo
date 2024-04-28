using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.RTCAudio
{
	// Token: 0x0200019F RID: 415
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct RegisterPlatformAudioUserOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x170002B1 RID: 689
		// (set) Token: 0x06000B06 RID: 2822 RVA: 0x0000C205 File Offset: 0x0000A405
		public string UserId
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_UserId, value);
			}
		}

		// Token: 0x06000B07 RID: 2823 RVA: 0x0000C214 File Offset: 0x0000A414
		public void Set(RegisterPlatformAudioUserOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.UserId = other.UserId;
			}
		}

		// Token: 0x06000B08 RID: 2824 RVA: 0x0000C22C File Offset: 0x0000A42C
		public void Set(object other)
		{
			this.Set(other as RegisterPlatformAudioUserOptions);
		}

		// Token: 0x06000B09 RID: 2825 RVA: 0x0000C23A File Offset: 0x0000A43A
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_UserId);
		}

		// Token: 0x0400053C RID: 1340
		private int m_ApiVersion;

		// Token: 0x0400053D RID: 1341
		private IntPtr m_UserId;
	}
}
