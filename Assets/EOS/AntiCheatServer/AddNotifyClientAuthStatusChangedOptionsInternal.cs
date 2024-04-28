using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.AntiCheatServer
{
	// Token: 0x02000557 RID: 1367
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct AddNotifyClientAuthStatusChangedOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x06002129 RID: 8489 RVA: 0x00022ED3 File Offset: 0x000210D3
		public void Set(AddNotifyClientAuthStatusChangedOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
			}
		}

		// Token: 0x0600212A RID: 8490 RVA: 0x00022EDF File Offset: 0x000210DF
		public void Set(object other)
		{
			this.Set(other as AddNotifyClientAuthStatusChangedOptions);
		}

		// Token: 0x0600212B RID: 8491 RVA: 0x000038B7 File Offset: 0x00001AB7
		public void Dispose()
		{
		}

		// Token: 0x04000F47 RID: 3911
		private int m_ApiVersion;
	}
}
