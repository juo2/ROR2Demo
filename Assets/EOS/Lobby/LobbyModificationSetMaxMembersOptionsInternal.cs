using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x02000358 RID: 856
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct LobbyModificationSetMaxMembersOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x17000653 RID: 1619
		// (set) Token: 0x06001552 RID: 5458 RVA: 0x000172F7 File Offset: 0x000154F7
		public uint MaxMembers
		{
			set
			{
				this.m_MaxMembers = value;
			}
		}

		// Token: 0x06001553 RID: 5459 RVA: 0x00017300 File Offset: 0x00015500
		public void Set(LobbyModificationSetMaxMembersOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.MaxMembers = other.MaxMembers;
			}
		}

		// Token: 0x06001554 RID: 5460 RVA: 0x00017318 File Offset: 0x00015518
		public void Set(object other)
		{
			this.Set(other as LobbyModificationSetMaxMembersOptions);
		}

		// Token: 0x06001555 RID: 5461 RVA: 0x000038B7 File Offset: 0x00001AB7
		public void Dispose()
		{
		}

		// Token: 0x04000A42 RID: 2626
		private int m_ApiVersion;

		// Token: 0x04000A43 RID: 2627
		private uint m_MaxMembers;
	}
}
