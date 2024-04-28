using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x0200032B RID: 811
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct LobbyDetailsCopyAttributeByIndexOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x170005FD RID: 1533
		// (set) Token: 0x06001435 RID: 5173 RVA: 0x000158EE File Offset: 0x00013AEE
		public uint AttrIndex
		{
			set
			{
				this.m_AttrIndex = value;
			}
		}

		// Token: 0x06001436 RID: 5174 RVA: 0x000158F7 File Offset: 0x00013AF7
		public void Set(LobbyDetailsCopyAttributeByIndexOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.AttrIndex = other.AttrIndex;
			}
		}

		// Token: 0x06001437 RID: 5175 RVA: 0x0001590F File Offset: 0x00013B0F
		public void Set(object other)
		{
			this.Set(other as LobbyDetailsCopyAttributeByIndexOptions);
		}

		// Token: 0x06001438 RID: 5176 RVA: 0x000038B7 File Offset: 0x00001AB7
		public void Dispose()
		{
		}

		// Token: 0x040009A8 RID: 2472
		private int m_ApiVersion;

		// Token: 0x040009A9 RID: 2473
		private uint m_AttrIndex;
	}
}
