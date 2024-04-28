using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x0200035A RID: 858
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct LobbyModificationSetPermissionLevelOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x17000655 RID: 1621
		// (set) Token: 0x06001559 RID: 5465 RVA: 0x00017337 File Offset: 0x00015537
		public LobbyPermissionLevel PermissionLevel
		{
			set
			{
				this.m_PermissionLevel = value;
			}
		}

		// Token: 0x0600155A RID: 5466 RVA: 0x00017340 File Offset: 0x00015540
		public void Set(LobbyModificationSetPermissionLevelOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.PermissionLevel = other.PermissionLevel;
			}
		}

		// Token: 0x0600155B RID: 5467 RVA: 0x00017358 File Offset: 0x00015558
		public void Set(object other)
		{
			this.Set(other as LobbyModificationSetPermissionLevelOptions);
		}

		// Token: 0x0600155C RID: 5468 RVA: 0x000038B7 File Offset: 0x00001AB7
		public void Dispose()
		{
		}

		// Token: 0x04000A45 RID: 2629
		private int m_ApiVersion;

		// Token: 0x04000A46 RID: 2630
		private LobbyPermissionLevel m_PermissionLevel;
	}
}
