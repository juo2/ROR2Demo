using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.CustomInvites
{
	// Token: 0x02000496 RID: 1174
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct AddNotifyCustomInviteReceivedOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x06001C7D RID: 7293 RVA: 0x0001E12A File Offset: 0x0001C32A
		public void Set(AddNotifyCustomInviteReceivedOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
			}
		}

		// Token: 0x06001C7E RID: 7294 RVA: 0x0001E136 File Offset: 0x0001C336
		public void Set(object other)
		{
			this.Set(other as AddNotifyCustomInviteReceivedOptions);
		}

		// Token: 0x06001C7F RID: 7295 RVA: 0x000038B7 File Offset: 0x00001AB7
		public void Dispose()
		{
		}

		// Token: 0x04000D4D RID: 3405
		private int m_ApiVersion;
	}
}
