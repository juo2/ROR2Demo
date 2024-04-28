using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Presence
{
	// Token: 0x02000204 RID: 516
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct AddNotifyJoinGameAcceptedOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x06000D86 RID: 3462 RVA: 0x0000E924 File Offset: 0x0000CB24
		public void Set(AddNotifyJoinGameAcceptedOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 2;
			}
		}

		// Token: 0x06000D87 RID: 3463 RVA: 0x0000E930 File Offset: 0x0000CB30
		public void Set(object other)
		{
			this.Set(other as AddNotifyJoinGameAcceptedOptions);
		}

		// Token: 0x06000D88 RID: 3464 RVA: 0x000038B7 File Offset: 0x00001AB7
		public void Dispose()
		{
		}

		// Token: 0x0400065F RID: 1631
		private int m_ApiVersion;
	}
}
