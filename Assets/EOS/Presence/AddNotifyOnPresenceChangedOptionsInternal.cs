using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Presence
{
	// Token: 0x02000206 RID: 518
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct AddNotifyOnPresenceChangedOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x06000D8A RID: 3466 RVA: 0x0000E93E File Offset: 0x0000CB3E
		public void Set(AddNotifyOnPresenceChangedOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
			}
		}

		// Token: 0x06000D8B RID: 3467 RVA: 0x0000E94A File Offset: 0x0000CB4A
		public void Set(object other)
		{
			this.Set(other as AddNotifyOnPresenceChangedOptions);
		}

		// Token: 0x06000D8C RID: 3468 RVA: 0x000038B7 File Offset: 0x00001AB7
		public void Dispose()
		{
		}

		// Token: 0x04000660 RID: 1632
		private int m_ApiVersion;
	}
}
