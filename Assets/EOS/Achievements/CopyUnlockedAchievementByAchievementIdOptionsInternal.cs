using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Achievements
{
	// Token: 0x02000603 RID: 1539
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct CopyUnlockedAchievementByAchievementIdOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x17000BBE RID: 3006
		// (set) Token: 0x06002581 RID: 9601 RVA: 0x00027E08 File Offset: 0x00026008
		public ProductUserId UserId
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_UserId, value);
			}
		}

		// Token: 0x17000BBF RID: 3007
		// (set) Token: 0x06002582 RID: 9602 RVA: 0x00027E17 File Offset: 0x00026017
		public string AchievementId
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_AchievementId, value);
			}
		}

		// Token: 0x06002583 RID: 9603 RVA: 0x00027E26 File Offset: 0x00026026
		public void Set(CopyUnlockedAchievementByAchievementIdOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.UserId = other.UserId;
				this.AchievementId = other.AchievementId;
			}
		}

		// Token: 0x06002584 RID: 9604 RVA: 0x00027E4A File Offset: 0x0002604A
		public void Set(object other)
		{
			this.Set(other as CopyUnlockedAchievementByAchievementIdOptions);
		}

		// Token: 0x06002585 RID: 9605 RVA: 0x00027E58 File Offset: 0x00026058
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_UserId);
			Helper.TryMarshalDispose(ref this.m_AchievementId);
		}

		// Token: 0x040011CC RID: 4556
		private int m_ApiVersion;

		// Token: 0x040011CD RID: 4557
		private IntPtr m_UserId;

		// Token: 0x040011CE RID: 4558
		private IntPtr m_AchievementId;
	}
}
