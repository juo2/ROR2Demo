using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x0200033B RID: 827
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct LobbyDetailsGetMemberByIndexOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x1700060B RID: 1547
		// (set) Token: 0x0600146A RID: 5226 RVA: 0x00015B2A File Offset: 0x00013D2A
		public uint MemberIndex
		{
			set
			{
				this.m_MemberIndex = value;
			}
		}

		// Token: 0x0600146B RID: 5227 RVA: 0x00015B33 File Offset: 0x00013D33
		public void Set(LobbyDetailsGetMemberByIndexOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.MemberIndex = other.MemberIndex;
			}
		}

		// Token: 0x0600146C RID: 5228 RVA: 0x00015B4B File Offset: 0x00013D4B
		public void Set(object other)
		{
			this.Set(other as LobbyDetailsGetMemberByIndexOptions);
		}

		// Token: 0x0600146D RID: 5229 RVA: 0x000038B7 File Offset: 0x00001AB7
		public void Dispose()
		{
		}

		// Token: 0x040009BE RID: 2494
		private int m_ApiVersion;

		// Token: 0x040009BF RID: 2495
		private uint m_MemberIndex;
	}
}
