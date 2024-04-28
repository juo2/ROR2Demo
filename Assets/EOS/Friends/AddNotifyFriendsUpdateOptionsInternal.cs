using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Friends
{
	// Token: 0x02000410 RID: 1040
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct AddNotifyFriendsUpdateOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x0600190F RID: 6415 RVA: 0x0001A604 File Offset: 0x00018804
		public void Set(AddNotifyFriendsUpdateOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
			}
		}

		// Token: 0x06001910 RID: 6416 RVA: 0x0001A610 File Offset: 0x00018810
		public void Set(object other)
		{
			this.Set(other as AddNotifyFriendsUpdateOptions);
		}

		// Token: 0x06001911 RID: 6417 RVA: 0x000038B7 File Offset: 0x00001AB7
		public void Dispose()
		{
		}

		// Token: 0x04000BAC RID: 2988
		private int m_ApiVersion;
	}
}
