using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Achievements
{
	// Token: 0x02000605 RID: 1541
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct CopyUnlockedAchievementByIndexOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x17000BC2 RID: 3010
		// (set) Token: 0x0600258B RID: 9611 RVA: 0x00027E94 File Offset: 0x00026094
		public ProductUserId UserId
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_UserId, value);
			}
		}

		// Token: 0x17000BC3 RID: 3011
		// (set) Token: 0x0600258C RID: 9612 RVA: 0x00027EA3 File Offset: 0x000260A3
		public uint AchievementIndex
		{
			set
			{
				this.m_AchievementIndex = value;
			}
		}

		// Token: 0x0600258D RID: 9613 RVA: 0x00027EAC File Offset: 0x000260AC
		public void Set(CopyUnlockedAchievementByIndexOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.UserId = other.UserId;
				this.AchievementIndex = other.AchievementIndex;
			}
		}

		// Token: 0x0600258E RID: 9614 RVA: 0x00027ED0 File Offset: 0x000260D0
		public void Set(object other)
		{
			this.Set(other as CopyUnlockedAchievementByIndexOptions);
		}

		// Token: 0x0600258F RID: 9615 RVA: 0x00027EDE File Offset: 0x000260DE
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_UserId);
		}

		// Token: 0x040011D1 RID: 4561
		private int m_ApiVersion;

		// Token: 0x040011D2 RID: 4562
		private IntPtr m_UserId;

		// Token: 0x040011D3 RID: 4563
		private uint m_AchievementIndex;
	}
}
