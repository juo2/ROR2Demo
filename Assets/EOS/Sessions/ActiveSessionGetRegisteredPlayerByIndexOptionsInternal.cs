using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x020000A9 RID: 169
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct ActiveSessionGetRegisteredPlayerByIndexOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x17000126 RID: 294
		// (set) Token: 0x060005E2 RID: 1506 RVA: 0x00006E74 File Offset: 0x00005074
		public uint PlayerIndex
		{
			set
			{
				this.m_PlayerIndex = value;
			}
		}

		// Token: 0x060005E3 RID: 1507 RVA: 0x00006E7D File Offset: 0x0000507D
		public void Set(ActiveSessionGetRegisteredPlayerByIndexOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.PlayerIndex = other.PlayerIndex;
			}
		}

		// Token: 0x060005E4 RID: 1508 RVA: 0x00006E95 File Offset: 0x00005095
		public void Set(object other)
		{
			this.Set(other as ActiveSessionGetRegisteredPlayerByIndexOptions);
		}

		// Token: 0x060005E5 RID: 1509 RVA: 0x000038B7 File Offset: 0x00001AB7
		public void Dispose()
		{
		}

		// Token: 0x040002FA RID: 762
		private int m_ApiVersion;

		// Token: 0x040002FB RID: 763
		private uint m_PlayerIndex;
	}
}
