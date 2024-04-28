using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Achievements
{
	// Token: 0x02000631 RID: 1585
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct UnlockedAchievementInternal : ISettable, IDisposable
	{
		// Token: 0x17000C41 RID: 3137
		// (get) Token: 0x060026E1 RID: 9953 RVA: 0x0002966C File Offset: 0x0002786C
		// (set) Token: 0x060026E2 RID: 9954 RVA: 0x00029688 File Offset: 0x00027888
		public string AchievementId
		{
			get
			{
				string result;
				Helper.TryMarshalGet(this.m_AchievementId, out result);
				return result;
			}
			set
			{
				Helper.TryMarshalSet(ref this.m_AchievementId, value);
			}
		}

		// Token: 0x17000C42 RID: 3138
		// (get) Token: 0x060026E3 RID: 9955 RVA: 0x00029698 File Offset: 0x00027898
		// (set) Token: 0x060026E4 RID: 9956 RVA: 0x000296B4 File Offset: 0x000278B4
		public DateTimeOffset? UnlockTime
		{
			get
			{
				DateTimeOffset? result;
				Helper.TryMarshalGet(this.m_UnlockTime, out result);
				return result;
			}
			set
			{
				Helper.TryMarshalSet(ref this.m_UnlockTime, value);
			}
		}

		// Token: 0x060026E5 RID: 9957 RVA: 0x000296C3 File Offset: 0x000278C3
		public void Set(UnlockedAchievement other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.AchievementId = other.AchievementId;
				this.UnlockTime = other.UnlockTime;
			}
		}

		// Token: 0x060026E6 RID: 9958 RVA: 0x000296E7 File Offset: 0x000278E7
		public void Set(object other)
		{
			this.Set(other as UnlockedAchievement);
		}

		// Token: 0x060026E7 RID: 9959 RVA: 0x000296F5 File Offset: 0x000278F5
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_AchievementId);
		}

		// Token: 0x0400125D RID: 4701
		private int m_ApiVersion;

		// Token: 0x0400125E RID: 4702
		private IntPtr m_AchievementId;

		// Token: 0x0400125F RID: 4703
		private long m_UnlockTime;
	}
}
