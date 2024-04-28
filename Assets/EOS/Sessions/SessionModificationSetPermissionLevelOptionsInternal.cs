using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x0200012A RID: 298
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct SessionModificationSetPermissionLevelOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x170001ED RID: 493
		// (set) Token: 0x0600086C RID: 2156 RVA: 0x00009203 File Offset: 0x00007403
		public OnlineSessionPermissionLevel PermissionLevel
		{
			set
			{
				this.m_PermissionLevel = value;
			}
		}

		// Token: 0x0600086D RID: 2157 RVA: 0x0000920C File Offset: 0x0000740C
		public void Set(SessionModificationSetPermissionLevelOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.PermissionLevel = other.PermissionLevel;
			}
		}

		// Token: 0x0600086E RID: 2158 RVA: 0x00009224 File Offset: 0x00007424
		public void Set(object other)
		{
			this.Set(other as SessionModificationSetPermissionLevelOptions);
		}

		// Token: 0x0600086F RID: 2159 RVA: 0x000038B7 File Offset: 0x00001AB7
		public void Dispose()
		{
		}

		// Token: 0x04000404 RID: 1028
		private int m_ApiVersion;

		// Token: 0x04000405 RID: 1029
		private OnlineSessionPermissionLevel m_PermissionLevel;
	}
}
