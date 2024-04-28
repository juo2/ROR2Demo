using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Auth
{
	// Token: 0x0200050E RID: 1294
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct AddNotifyLoginStatusChangedOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x06001F43 RID: 8003 RVA: 0x00020DCB File Offset: 0x0001EFCB
		public void Set(AddNotifyLoginStatusChangedOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
			}
		}

		// Token: 0x06001F44 RID: 8004 RVA: 0x00020DD7 File Offset: 0x0001EFD7
		public void Set(object other)
		{
			this.Set(other as AddNotifyLoginStatusChangedOptions);
		}

		// Token: 0x06001F45 RID: 8005 RVA: 0x000038B7 File Offset: 0x00001AB7
		public void Dispose()
		{
		}

		// Token: 0x04000E6D RID: 3693
		private int m_ApiVersion;
	}
}
