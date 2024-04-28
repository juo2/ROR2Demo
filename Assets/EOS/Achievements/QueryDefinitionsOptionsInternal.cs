using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Achievements
{
	// Token: 0x02000629 RID: 1577
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct QueryDefinitionsOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x17000C30 RID: 3120
		// (set) Token: 0x060026B2 RID: 9906 RVA: 0x00029348 File Offset: 0x00027548
		public ProductUserId LocalUserId
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_LocalUserId, value);
			}
		}

		// Token: 0x17000C31 RID: 3121
		// (set) Token: 0x060026B3 RID: 9907 RVA: 0x00029357 File Offset: 0x00027557
		public EpicAccountId EpicUserId_DEPRECATED
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_EpicUserId_DEPRECATED, value);
			}
		}

		// Token: 0x17000C32 RID: 3122
		// (set) Token: 0x060026B4 RID: 9908 RVA: 0x00029366 File Offset: 0x00027566
		public string[] HiddenAchievementIds_DEPRECATED
		{
			set
			{
				Helper.TryMarshalSet<string>(ref this.m_HiddenAchievementIds_DEPRECATED, value, out this.m_HiddenAchievementsCount_DEPRECATED, true);
			}
		}

		// Token: 0x060026B5 RID: 9909 RVA: 0x0002937C File Offset: 0x0002757C
		public void Set(QueryDefinitionsOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 3;
				this.LocalUserId = other.LocalUserId;
				this.EpicUserId_DEPRECATED = other.EpicUserId_DEPRECATED;
				this.HiddenAchievementIds_DEPRECATED = other.HiddenAchievementIds_DEPRECATED;
			}
		}

		// Token: 0x060026B6 RID: 9910 RVA: 0x000293AC File Offset: 0x000275AC
		public void Set(object other)
		{
			this.Set(other as QueryDefinitionsOptions);
		}

		// Token: 0x060026B7 RID: 9911 RVA: 0x000293BA File Offset: 0x000275BA
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_LocalUserId);
			Helper.TryMarshalDispose(ref this.m_EpicUserId_DEPRECATED);
			Helper.TryMarshalDispose(ref this.m_HiddenAchievementIds_DEPRECATED);
		}

		// Token: 0x04001246 RID: 4678
		private int m_ApiVersion;

		// Token: 0x04001247 RID: 4679
		private IntPtr m_LocalUserId;

		// Token: 0x04001248 RID: 4680
		private IntPtr m_EpicUserId_DEPRECATED;

		// Token: 0x04001249 RID: 4681
		private IntPtr m_HiddenAchievementIds_DEPRECATED;

		// Token: 0x0400124A RID: 4682
		private uint m_HiddenAchievementsCount_DEPRECATED;
	}
}
