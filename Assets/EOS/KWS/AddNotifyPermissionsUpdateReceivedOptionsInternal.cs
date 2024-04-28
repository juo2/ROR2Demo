using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.KWS
{
	// Token: 0x020003DE RID: 990
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct AddNotifyPermissionsUpdateReceivedOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x060017F6 RID: 6134 RVA: 0x000194F4 File Offset: 0x000176F4
		public void Set(AddNotifyPermissionsUpdateReceivedOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
			}
		}

		// Token: 0x060017F7 RID: 6135 RVA: 0x00019500 File Offset: 0x00017700
		public void Set(object other)
		{
			this.Set(other as AddNotifyPermissionsUpdateReceivedOptions);
		}

		// Token: 0x060017F8 RID: 6136 RVA: 0x000038B7 File Offset: 0x00001AB7
		public void Dispose()
		{
		}

		// Token: 0x04000B38 RID: 2872
		private int m_ApiVersion;
	}
}
