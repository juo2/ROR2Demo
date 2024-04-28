using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.AntiCheatCommon
{
	// Token: 0x02000588 RID: 1416
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct LogGameRoundEndOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x17000A7D RID: 2685
		// (set) Token: 0x06002216 RID: 8726 RVA: 0x000241F7 File Offset: 0x000223F7
		public uint WinningTeamId
		{
			set
			{
				this.m_WinningTeamId = value;
			}
		}

		// Token: 0x06002217 RID: 8727 RVA: 0x00024200 File Offset: 0x00022400
		public void Set(LogGameRoundEndOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.WinningTeamId = other.WinningTeamId;
			}
		}

		// Token: 0x06002218 RID: 8728 RVA: 0x00024218 File Offset: 0x00022418
		public void Set(object other)
		{
			this.Set(other as LogGameRoundEndOptions);
		}

		// Token: 0x06002219 RID: 8729 RVA: 0x000038B7 File Offset: 0x00001AB7
		public void Dispose()
		{
		}

		// Token: 0x04001009 RID: 4105
		private int m_ApiVersion;

		// Token: 0x0400100A RID: 4106
		private uint m_WinningTeamId;
	}
}
