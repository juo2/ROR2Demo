using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.AntiCheatServer
{
	// Token: 0x02000555 RID: 1365
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct AddNotifyClientActionRequiredOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x06002125 RID: 8485 RVA: 0x00022EB9 File Offset: 0x000210B9
		public void Set(AddNotifyClientActionRequiredOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
			}
		}

		// Token: 0x06002126 RID: 8486 RVA: 0x00022EC5 File Offset: 0x000210C5
		public void Set(object other)
		{
			this.Set(other as AddNotifyClientActionRequiredOptions);
		}

		// Token: 0x06002127 RID: 8487 RVA: 0x000038B7 File Offset: 0x00001AB7
		public void Dispose()
		{
		}

		// Token: 0x04000F46 RID: 3910
		private int m_ApiVersion;
	}
}
