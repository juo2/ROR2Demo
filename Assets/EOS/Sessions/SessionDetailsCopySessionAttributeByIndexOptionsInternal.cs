using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x0200010D RID: 269
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct SessionDetailsCopySessionAttributeByIndexOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x170001B1 RID: 433
		// (set) Token: 0x060007C2 RID: 1986 RVA: 0x00008677 File Offset: 0x00006877
		public uint AttrIndex
		{
			set
			{
				this.m_AttrIndex = value;
			}
		}

		// Token: 0x060007C3 RID: 1987 RVA: 0x00008680 File Offset: 0x00006880
		public void Set(SessionDetailsCopySessionAttributeByIndexOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.AttrIndex = other.AttrIndex;
			}
		}

		// Token: 0x060007C4 RID: 1988 RVA: 0x00008698 File Offset: 0x00006898
		public void Set(object other)
		{
			this.Set(other as SessionDetailsCopySessionAttributeByIndexOptions);
		}

		// Token: 0x060007C5 RID: 1989 RVA: 0x000038B7 File Offset: 0x00001AB7
		public void Dispose()
		{
		}

		// Token: 0x040003B2 RID: 946
		private int m_ApiVersion;

		// Token: 0x040003B3 RID: 947
		private uint m_AttrIndex;
	}
}
