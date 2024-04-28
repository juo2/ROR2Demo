using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x020000B3 RID: 179
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct AddNotifySessionInviteReceivedOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x06000609 RID: 1545 RVA: 0x000070BB File Offset: 0x000052BB
		public void Set(AddNotifySessionInviteReceivedOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
			}
		}

		// Token: 0x0600060A RID: 1546 RVA: 0x000070C7 File Offset: 0x000052C7
		public void Set(object other)
		{
			this.Set(other as AddNotifySessionInviteReceivedOptions);
		}

		// Token: 0x0600060B RID: 1547 RVA: 0x000038B7 File Offset: 0x00001AB7
		public void Dispose()
		{
		}

		// Token: 0x04000308 RID: 776
		private int m_ApiVersion;
	}
}
