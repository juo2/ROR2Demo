using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Connect
{
	// Token: 0x020004AB RID: 1195
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct AddNotifyAuthExpirationOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x06001D02 RID: 7426 RVA: 0x0001E9B8 File Offset: 0x0001CBB8
		public void Set(AddNotifyAuthExpirationOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
			}
		}

		// Token: 0x06001D03 RID: 7427 RVA: 0x0001E9C4 File Offset: 0x0001CBC4
		public void Set(object other)
		{
			this.Set(other as AddNotifyAuthExpirationOptions);
		}

		// Token: 0x06001D04 RID: 7428 RVA: 0x000038B7 File Offset: 0x00001AB7
		public void Dispose()
		{
		}

		// Token: 0x04000D85 RID: 3461
		private int m_ApiVersion;
	}
}
