using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x0200012D RID: 301
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct SessionSearchCopySearchResultByIndexOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x170001EF RID: 495
		// (set) Token: 0x0600087F RID: 2175 RVA: 0x00009429 File Offset: 0x00007629
		public uint SessionIndex
		{
			set
			{
				this.m_SessionIndex = value;
			}
		}

		// Token: 0x06000880 RID: 2176 RVA: 0x00009432 File Offset: 0x00007632
		public void Set(SessionSearchCopySearchResultByIndexOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.SessionIndex = other.SessionIndex;
			}
		}

		// Token: 0x06000881 RID: 2177 RVA: 0x0000944A File Offset: 0x0000764A
		public void Set(object other)
		{
			this.Set(other as SessionSearchCopySearchResultByIndexOptions);
		}

		// Token: 0x06000882 RID: 2178 RVA: 0x000038B7 File Offset: 0x00001AB7
		public void Dispose()
		{
		}

		// Token: 0x0400040F RID: 1039
		private int m_ApiVersion;

		// Token: 0x04000410 RID: 1040
		private uint m_SessionIndex;
	}
}
