using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Achievements
{
	// Token: 0x0200062F RID: 1583
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct UnlockAchievementsOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x17000C3D RID: 3133
		// (set) Token: 0x060026D5 RID: 9941 RVA: 0x00029582 File Offset: 0x00027782
		public ProductUserId UserId
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_UserId, value);
			}
		}

		// Token: 0x17000C3E RID: 3134
		// (set) Token: 0x060026D6 RID: 9942 RVA: 0x00029591 File Offset: 0x00027791
		public string[] AchievementIds
		{
			set
			{
				Helper.TryMarshalSet<string>(ref this.m_AchievementIds, value, out this.m_AchievementsCount, true);
			}
		}

		// Token: 0x060026D7 RID: 9943 RVA: 0x000295A7 File Offset: 0x000277A7
		public void Set(UnlockAchievementsOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.UserId = other.UserId;
				this.AchievementIds = other.AchievementIds;
			}
		}

		// Token: 0x060026D8 RID: 9944 RVA: 0x000295CB File Offset: 0x000277CB
		public void Set(object other)
		{
			this.Set(other as UnlockAchievementsOptions);
		}

		// Token: 0x060026D9 RID: 9945 RVA: 0x000295D9 File Offset: 0x000277D9
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_UserId);
			Helper.TryMarshalDispose(ref this.m_AchievementIds);
		}

		// Token: 0x04001257 RID: 4695
		private int m_ApiVersion;

		// Token: 0x04001258 RID: 4696
		private IntPtr m_UserId;

		// Token: 0x04001259 RID: 4697
		private IntPtr m_AchievementIds;

		// Token: 0x0400125A RID: 4698
		private uint m_AchievementsCount;
	}
}
