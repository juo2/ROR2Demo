using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.AntiCheatClient
{
	// Token: 0x020005B0 RID: 1456
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct AddNotifyMessageToPeerOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x0600235F RID: 9055 RVA: 0x0002557E File Offset: 0x0002377E
		public void Set(AddNotifyMessageToPeerOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
			}
		}

		// Token: 0x06002360 RID: 9056 RVA: 0x0002558A File Offset: 0x0002378A
		public void Set(object other)
		{
			this.Set(other as AddNotifyMessageToPeerOptions);
		}

		// Token: 0x06002361 RID: 9057 RVA: 0x000038B7 File Offset: 0x00001AB7
		public void Dispose()
		{
		}

		// Token: 0x040010AD RID: 4269
		private int m_ApiVersion;
	}
}
