using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x02000144 RID: 324
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct StartSessionOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x1700020B RID: 523
		// (set) Token: 0x06000902 RID: 2306 RVA: 0x0000A105 File Offset: 0x00008305
		public string SessionName
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_SessionName, value);
			}
		}

		// Token: 0x06000903 RID: 2307 RVA: 0x0000A114 File Offset: 0x00008314
		public void Set(StartSessionOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.SessionName = other.SessionName;
			}
		}

		// Token: 0x06000904 RID: 2308 RVA: 0x0000A12C File Offset: 0x0000832C
		public void Set(object other)
		{
			this.Set(other as StartSessionOptions);
		}

		// Token: 0x06000905 RID: 2309 RVA: 0x0000A13A File Offset: 0x0000833A
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_SessionName);
		}

		// Token: 0x04000453 RID: 1107
		private int m_ApiVersion;

		// Token: 0x04000454 RID: 1108
		private IntPtr m_SessionName;
	}
}
