using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x020000AF RID: 175
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct AddNotifyJoinSessionAcceptedOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x06000601 RID: 1537 RVA: 0x00007087 File Offset: 0x00005287
		public void Set(AddNotifyJoinSessionAcceptedOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
			}
		}

		// Token: 0x06000602 RID: 1538 RVA: 0x00007093 File Offset: 0x00005293
		public void Set(object other)
		{
			this.Set(other as AddNotifyJoinSessionAcceptedOptions);
		}

		// Token: 0x06000603 RID: 1539 RVA: 0x000038B7 File Offset: 0x00001AB7
		public void Dispose()
		{
		}

		// Token: 0x04000306 RID: 774
		private int m_ApiVersion;
	}
}
