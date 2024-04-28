using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Achievements
{
	// Token: 0x0200062B RID: 1579
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct QueryPlayerAchievementsOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x17000C35 RID: 3125
		// (set) Token: 0x060026BD RID: 9917 RVA: 0x00029402 File Offset: 0x00027602
		public ProductUserId TargetUserId
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_TargetUserId, value);
			}
		}

		// Token: 0x17000C36 RID: 3126
		// (set) Token: 0x060026BE RID: 9918 RVA: 0x00029411 File Offset: 0x00027611
		public ProductUserId LocalUserId
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_LocalUserId, value);
			}
		}

		// Token: 0x060026BF RID: 9919 RVA: 0x00029420 File Offset: 0x00027620
		public void Set(QueryPlayerAchievementsOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 2;
				this.TargetUserId = other.TargetUserId;
				this.LocalUserId = other.LocalUserId;
			}
		}

		// Token: 0x060026C0 RID: 9920 RVA: 0x00029444 File Offset: 0x00027644
		public void Set(object other)
		{
			this.Set(other as QueryPlayerAchievementsOptions);
		}

		// Token: 0x060026C1 RID: 9921 RVA: 0x00029452 File Offset: 0x00027652
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_TargetUserId);
			Helper.TryMarshalDispose(ref this.m_LocalUserId);
		}

		// Token: 0x0400124D RID: 4685
		private int m_ApiVersion;

		// Token: 0x0400124E RID: 4686
		private IntPtr m_TargetUserId;

		// Token: 0x0400124F RID: 4687
		private IntPtr m_LocalUserId;
	}
}
