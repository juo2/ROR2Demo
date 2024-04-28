using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.AntiCheatServer
{
	// Token: 0x02000559 RID: 1369
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct AddNotifyMessageToClientOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x0600212D RID: 8493 RVA: 0x00022EED File Offset: 0x000210ED
		public void Set(AddNotifyMessageToClientOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
			}
		}

		// Token: 0x0600212E RID: 8494 RVA: 0x00022EF9 File Offset: 0x000210F9
		public void Set(object other)
		{
			this.Set(other as AddNotifyMessageToClientOptions);
		}

		// Token: 0x0600212F RID: 8495 RVA: 0x000038B7 File Offset: 0x00001AB7
		public void Dispose()
		{
		}

		// Token: 0x04000F48 RID: 3912
		private int m_ApiVersion;
	}
}
