using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x02000139 RID: 313
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct SessionSearchSetMaxResultsOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x170001FC RID: 508
		// (set) Token: 0x060008AE RID: 2222 RVA: 0x00009601 File Offset: 0x00007801
		public uint MaxSearchResults
		{
			set
			{
				this.m_MaxSearchResults = value;
			}
		}

		// Token: 0x060008AF RID: 2223 RVA: 0x0000960A File Offset: 0x0000780A
		public void Set(SessionSearchSetMaxResultsOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.MaxSearchResults = other.MaxSearchResults;
			}
		}

		// Token: 0x060008B0 RID: 2224 RVA: 0x00009622 File Offset: 0x00007822
		public void Set(object other)
		{
			this.Set(other as SessionSearchSetMaxResultsOptions);
		}

		// Token: 0x060008B1 RID: 2225 RVA: 0x000038B7 File Offset: 0x00001AB7
		public void Dispose()
		{
		}

		// Token: 0x0400041F RID: 1055
		private int m_ApiVersion;

		// Token: 0x04000420 RID: 1056
		private uint m_MaxSearchResults;
	}
}
