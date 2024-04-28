using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x020000C3 RID: 195
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct CreateSessionSearchOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x17000153 RID: 339
		// (set) Token: 0x0600066E RID: 1646 RVA: 0x000077F4 File Offset: 0x000059F4
		public uint MaxSearchResults
		{
			set
			{
				this.m_MaxSearchResults = value;
			}
		}

		// Token: 0x0600066F RID: 1647 RVA: 0x000077FD File Offset: 0x000059FD
		public void Set(CreateSessionSearchOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.MaxSearchResults = other.MaxSearchResults;
			}
		}

		// Token: 0x06000670 RID: 1648 RVA: 0x00007815 File Offset: 0x00005A15
		public void Set(object other)
		{
			this.Set(other as CreateSessionSearchOptions);
		}

		// Token: 0x06000671 RID: 1649 RVA: 0x000038B7 File Offset: 0x00001AB7
		public void Dispose()
		{
		}

		// Token: 0x04000334 RID: 820
		private int m_ApiVersion;

		// Token: 0x04000335 RID: 821
		private uint m_MaxSearchResults;
	}
}
