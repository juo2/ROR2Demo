using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x020000B1 RID: 177
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct AddNotifySessionInviteAcceptedOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x06000605 RID: 1541 RVA: 0x000070A1 File Offset: 0x000052A1
		public void Set(AddNotifySessionInviteAcceptedOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
			}
		}

		// Token: 0x06000606 RID: 1542 RVA: 0x000070AD File Offset: 0x000052AD
		public void Set(object other)
		{
			this.Set(other as AddNotifySessionInviteAcceptedOptions);
		}

		// Token: 0x06000607 RID: 1543 RVA: 0x000038B7 File Offset: 0x00001AB7
		public void Dispose()
		{
		}

		// Token: 0x04000307 RID: 775
		private int m_ApiVersion;
	}
}
