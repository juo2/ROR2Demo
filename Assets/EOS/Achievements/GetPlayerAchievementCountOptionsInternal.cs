using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Achievements
{
	// Token: 0x0200060D RID: 1549
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct GetPlayerAchievementCountOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x17000BEF RID: 3055
		// (set) Token: 0x060025F7 RID: 9719 RVA: 0x00028890 File Offset: 0x00026A90
		public ProductUserId UserId
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_UserId, value);
			}
		}

		// Token: 0x060025F8 RID: 9720 RVA: 0x0002889F File Offset: 0x00026A9F
		public void Set(GetPlayerAchievementCountOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.UserId = other.UserId;
			}
		}

		// Token: 0x060025F9 RID: 9721 RVA: 0x000288B7 File Offset: 0x00026AB7
		public void Set(object other)
		{
			this.Set(other as GetPlayerAchievementCountOptions);
		}

		// Token: 0x060025FA RID: 9722 RVA: 0x000288C5 File Offset: 0x00026AC5
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_UserId);
		}

		// Token: 0x04001204 RID: 4612
		private int m_ApiVersion;

		// Token: 0x04001205 RID: 4613
		private IntPtr m_UserId;
	}
}
