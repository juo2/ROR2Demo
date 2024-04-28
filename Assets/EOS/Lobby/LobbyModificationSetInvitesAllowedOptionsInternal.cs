using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x02000356 RID: 854
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct LobbyModificationSetInvitesAllowedOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x17000651 RID: 1617
		// (set) Token: 0x0600154B RID: 5451 RVA: 0x000172B1 File Offset: 0x000154B1
		public bool InvitesAllowed
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_InvitesAllowed, value);
			}
		}

		// Token: 0x0600154C RID: 5452 RVA: 0x000172C0 File Offset: 0x000154C0
		public void Set(LobbyModificationSetInvitesAllowedOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.InvitesAllowed = other.InvitesAllowed;
			}
		}

		// Token: 0x0600154D RID: 5453 RVA: 0x000172D8 File Offset: 0x000154D8
		public void Set(object other)
		{
			this.Set(other as LobbyModificationSetInvitesAllowedOptions);
		}

		// Token: 0x0600154E RID: 5454 RVA: 0x000038B7 File Offset: 0x00001AB7
		public void Dispose()
		{
		}

		// Token: 0x04000A3F RID: 2623
		private int m_ApiVersion;

		// Token: 0x04000A40 RID: 2624
		private int m_InvitesAllowed;
	}
}
