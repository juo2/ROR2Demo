using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Achievements
{
	// Token: 0x02000625 RID: 1573
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct PlayerAchievementInternal : ISettable, IDisposable
	{
		// Token: 0x17000C1F RID: 3103
		// (get) Token: 0x06002686 RID: 9862 RVA: 0x00028FB4 File Offset: 0x000271B4
		// (set) Token: 0x06002687 RID: 9863 RVA: 0x00028FD0 File Offset: 0x000271D0
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

		// Token: 0x17000C20 RID: 3104
		// (get) Token: 0x06002688 RID: 9864 RVA: 0x00028FDF File Offset: 0x000271DF
		// (set) Token: 0x06002689 RID: 9865 RVA: 0x00028FE7 File Offset: 0x000271E7
		public double Progress
		{
			get
			{
				return this.m_Progress;
			}
			set
			{
				this.m_Progress = value;
			}
		}

		// Token: 0x17000C21 RID: 3105
		// (get) Token: 0x0600268A RID: 9866 RVA: 0x00028FF0 File Offset: 0x000271F0
		// (set) Token: 0x0600268B RID: 9867 RVA: 0x0002900C File Offset: 0x0002720C
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

		// Token: 0x17000C22 RID: 3106
		// (get) Token: 0x0600268C RID: 9868 RVA: 0x0002901C File Offset: 0x0002721C
		// (set) Token: 0x0600268D RID: 9869 RVA: 0x0002903E File Offset: 0x0002723E
		public PlayerStatInfo[] StatInfo
		{
			get
			{
				PlayerStatInfo[] result;
				Helper.TryMarshalGet<PlayerStatInfoInternal, PlayerStatInfo>(this.m_StatInfo, out result, this.m_StatInfoCount);
				return result;
			}
			set
			{
				Helper.TryMarshalSet<PlayerStatInfoInternal, PlayerStatInfo>(ref this.m_StatInfo, value, out this.m_StatInfoCount);
			}
		}

		// Token: 0x17000C23 RID: 3107
		// (get) Token: 0x0600268E RID: 9870 RVA: 0x00029054 File Offset: 0x00027254
		// (set) Token: 0x0600268F RID: 9871 RVA: 0x00029070 File Offset: 0x00027270
		public string DisplayName
		{
			get
			{
				string result;
				Helper.TryMarshalGet(this.m_DisplayName, out result);
				return result;
			}
			set
			{
				Helper.TryMarshalSet(ref this.m_DisplayName, value);
			}
		}

		// Token: 0x17000C24 RID: 3108
		// (get) Token: 0x06002690 RID: 9872 RVA: 0x00029080 File Offset: 0x00027280
		// (set) Token: 0x06002691 RID: 9873 RVA: 0x0002909C File Offset: 0x0002729C
		public string Description
		{
			get
			{
				string result;
				Helper.TryMarshalGet(this.m_Description, out result);
				return result;
			}
			set
			{
				Helper.TryMarshalSet(ref this.m_Description, value);
			}
		}

		// Token: 0x17000C25 RID: 3109
		// (get) Token: 0x06002692 RID: 9874 RVA: 0x000290AC File Offset: 0x000272AC
		// (set) Token: 0x06002693 RID: 9875 RVA: 0x000290C8 File Offset: 0x000272C8
		public string IconURL
		{
			get
			{
				string result;
				Helper.TryMarshalGet(this.m_IconURL, out result);
				return result;
			}
			set
			{
				Helper.TryMarshalSet(ref this.m_IconURL, value);
			}
		}

		// Token: 0x17000C26 RID: 3110
		// (get) Token: 0x06002694 RID: 9876 RVA: 0x000290D8 File Offset: 0x000272D8
		// (set) Token: 0x06002695 RID: 9877 RVA: 0x000290F4 File Offset: 0x000272F4
		public string FlavorText
		{
			get
			{
				string result;
				Helper.TryMarshalGet(this.m_FlavorText, out result);
				return result;
			}
			set
			{
				Helper.TryMarshalSet(ref this.m_FlavorText, value);
			}
		}

		// Token: 0x06002696 RID: 9878 RVA: 0x00029104 File Offset: 0x00027304
		public void Set(PlayerAchievement other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 2;
				this.AchievementId = other.AchievementId;
				this.Progress = other.Progress;
				this.UnlockTime = other.UnlockTime;
				this.StatInfo = other.StatInfo;
				this.DisplayName = other.DisplayName;
				this.Description = other.Description;
				this.IconURL = other.IconURL;
				this.FlavorText = other.FlavorText;
			}
		}

		// Token: 0x06002697 RID: 9879 RVA: 0x0002917B File Offset: 0x0002737B
		public void Set(object other)
		{
			this.Set(other as PlayerAchievement);
		}

		// Token: 0x06002698 RID: 9880 RVA: 0x0002918C File Offset: 0x0002738C
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_AchievementId);
			Helper.TryMarshalDispose(ref this.m_StatInfo);
			Helper.TryMarshalDispose(ref this.m_DisplayName);
			Helper.TryMarshalDispose(ref this.m_Description);
			Helper.TryMarshalDispose(ref this.m_IconURL);
			Helper.TryMarshalDispose(ref this.m_FlavorText);
		}

		// Token: 0x04001232 RID: 4658
		private int m_ApiVersion;

		// Token: 0x04001233 RID: 4659
		private IntPtr m_AchievementId;

		// Token: 0x04001234 RID: 4660
		private double m_Progress;

		// Token: 0x04001235 RID: 4661
		private long m_UnlockTime;

		// Token: 0x04001236 RID: 4662
		private int m_StatInfoCount;

		// Token: 0x04001237 RID: 4663
		private IntPtr m_StatInfo;

		// Token: 0x04001238 RID: 4664
		private IntPtr m_DisplayName;

		// Token: 0x04001239 RID: 4665
		private IntPtr m_Description;

		// Token: 0x0400123A RID: 4666
		private IntPtr m_IconURL;

		// Token: 0x0400123B RID: 4667
		private IntPtr m_FlavorText;
	}
}
