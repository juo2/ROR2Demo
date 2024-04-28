using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Connect
{
	// Token: 0x020004AD RID: 1197
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct AddNotifyLoginStatusChangedOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x06001D06 RID: 7430 RVA: 0x0001E9D2 File Offset: 0x0001CBD2
		public void Set(AddNotifyLoginStatusChangedOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
			}
		}

		// Token: 0x06001D07 RID: 7431 RVA: 0x0001E9DE File Offset: 0x0001CBDE
		public void Set(object other)
		{
			this.Set(other as AddNotifyLoginStatusChangedOptions);
		}

		// Token: 0x06001D08 RID: 7432 RVA: 0x000038B7 File Offset: 0x00001AB7
		public void Dispose()
		{
		}

		// Token: 0x04000D86 RID: 3462
		private int m_ApiVersion;
	}
}
