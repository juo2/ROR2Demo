using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Connect
{
	// Token: 0x020004C8 RID: 1224
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct DeleteDeviceIdOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x06001DA8 RID: 7592 RVA: 0x0001F908 File Offset: 0x0001DB08
		public void Set(DeleteDeviceIdOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
			}
		}

		// Token: 0x06001DA9 RID: 7593 RVA: 0x0001F914 File Offset: 0x0001DB14
		public void Set(object other)
		{
			this.Set(other as DeleteDeviceIdOptions);
		}

		// Token: 0x06001DAA RID: 7594 RVA: 0x000038B7 File Offset: 0x00001AB7
		public void Dispose()
		{
		}

		// Token: 0x04000DD8 RID: 3544
		private int m_ApiVersion;
	}
}
