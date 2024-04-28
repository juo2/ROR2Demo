using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x0200014C RID: 332
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct UpdateSessionModificationOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x17000221 RID: 545
		// (set) Token: 0x06000932 RID: 2354 RVA: 0x0000A419 File Offset: 0x00008619
		public string SessionName
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_SessionName, value);
			}
		}

		// Token: 0x06000933 RID: 2355 RVA: 0x0000A428 File Offset: 0x00008628
		public void Set(UpdateSessionModificationOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.SessionName = other.SessionName;
			}
		}

		// Token: 0x06000934 RID: 2356 RVA: 0x0000A440 File Offset: 0x00008640
		public void Set(object other)
		{
			this.Set(other as UpdateSessionModificationOptions);
		}

		// Token: 0x06000935 RID: 2357 RVA: 0x0000A44E File Offset: 0x0000864E
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_SessionName);
		}

		// Token: 0x0400046B RID: 1131
		private int m_ApiVersion;

		// Token: 0x0400046C RID: 1132
		private IntPtr m_SessionName;
	}
}
