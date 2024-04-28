using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.AntiCheatServer
{
	// Token: 0x02000572 RID: 1394
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct UnregisterClientOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x17000A62 RID: 2658
		// (set) Token: 0x060021C8 RID: 8648 RVA: 0x00023A10 File Offset: 0x00021C10
		public IntPtr ClientHandle
		{
			set
			{
				this.m_ClientHandle = value;
			}
		}

		// Token: 0x060021C9 RID: 8649 RVA: 0x00023A19 File Offset: 0x00021C19
		public void Set(UnregisterClientOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.ClientHandle = other.ClientHandle;
			}
		}

		// Token: 0x060021CA RID: 8650 RVA: 0x00023A31 File Offset: 0x00021C31
		public void Set(object other)
		{
			this.Set(other as UnregisterClientOptions);
		}

		// Token: 0x060021CB RID: 8651 RVA: 0x00023A3F File Offset: 0x00021C3F
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_ClientHandle);
		}

		// Token: 0x04000F8B RID: 3979
		private int m_ApiVersion;

		// Token: 0x04000F8C RID: 3980
		private IntPtr m_ClientHandle;
	}
}
